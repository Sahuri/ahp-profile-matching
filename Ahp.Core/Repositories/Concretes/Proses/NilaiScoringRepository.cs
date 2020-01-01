using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using Ahp.Core.Security;
using System.Collections.Generic;
using System.Linq;
using DataTablesParser;
using System;
using System.Linq.Expressions;
using Ahp.Core.Repositories.Abstractions.Proses;

namespace Ahp.Core.Repositories.Concretes.Proses
{
    public class NilaiScoringRepository : GenericDataRepository<NilaiScoring, IGenericContext>, INilaiScoringRepository
    {
        protected IGenericContext ctx;
        protected Dictionary<double?, double?> BobotNilai = new Dictionary<double?, double?>() {
            {0,5},
            {1,4.5},
            {-1,4},
            {2,3.5},
            {-2,3},
            {3,2.5},
            {-3,2},
            {4,1.5},
            {-4,1}
        };

        public NilaiScoringRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        protected List<NilaiScoring> ScoringNcfNsf(List<NilaiScoring> nilaiScorings) {

            var kriteria = ctx.Set<Kriteria>().ToList();
            if (kriteria.Count() > 0) {
                var ncf = from a in nilaiScorings
                          join b in kriteria on a.KodeKriteria equals b.Kode
                          where b.Faktor == 1
                          select new
                          {
                              Kode = a.KodeKriteria,
                              ncfScore = a.NilaiBobot * b.EigenvectorScore
                          };

                var nsf = from a in nilaiScorings
                          join b in kriteria on a.KodeKriteria equals b.Kode
                          where b.Faktor == 2
                          select new
                          {
                              Kode = a.KodeKriteria,
                              nsfScore = a.NilaiBobot * b.EigenvectorScore
                          };

                var ncfScore = ncf.Count() > 0 ? ncf.Sum(m => m.ncfScore) / (double)ncf.Count() : 0;
                var ncfBobot = ncf.Count() > 0 ?  (double)ncf.Count() / (double)kriteria.Count() : 0;
                var nsfScore = nsf.Count() > 0 ? nsf.Sum(m => m.nsfScore) / (double)nsf.Count() : 0;
                var nsfBobot = nsf.Count() > 0 ? (double)nsf.Count() / kriteria.Count() : 0;

                nilaiScorings.ForEach(item =>
                {
                    item.Ncf = ncfScore;
                    item.Nsf = nsfScore;
                    item.NilaiTotal = (ncfScore * ncfBobot) + (nsfScore * nsfBobot);
                });
            }
            
            return nilaiScorings;
        }

        public bool Create(CalonKaryawanGap model)
        {
            List<NilaiScoring> nilaiScorings = new List<NilaiScoring>();
            foreach (var item in model.nilaiGapBobots) {
                NilaiScoring nilaiScoring = new NilaiScoring();
                nilaiScoring.KodeKaryawan = model.kodeKaryawan;
                nilaiScoring.KodeLowongan = model.kodeLowongan;
                nilaiScoring.KodeKriteria = item.kodeKriteria;
                nilaiScoring.NilaiAlternatif = (double?)item.nilaiAlternatif;
                nilaiScoring.NilaiGap = (double?)(item.nilaiAlternatif - item.nilaiTarget);
                nilaiScoring.NilaiBobot = BobotNilai[nilaiScoring.NilaiGap] ?? 0;

                nilaiScoring.CreatedBy = this.UserProfile.UserID;
                nilaiScoring.CreatedDate = DateTime.Now;
                nilaiScorings.Add(nilaiScoring);
            }
            
            ctx.Set<NilaiScoring>().AddRange(ScoringNcfNsf(nilaiScorings));

            var result = ctx.SaveChanges() > 0;

            return result;
        }

        public bool Update(CalonKaryawanGap model)
        {
            List<NilaiScoring> nilaiScorings = ctx.Set<NilaiScoring>().Where(x => x.KodeKaryawan.Equals(model.kodeKaryawan)).ToList();
            ctx.Set<NilaiScoring>().RemoveRange(nilaiScorings);
            return Create(model);
        }

        public List<Dropdown> DropdownPosisi(string term)
        {
            var sql = string.Format("select distinct Kode as id, Kode +' - '+Nama  as text, Kode as [value] from Lowongan " +
                "where lower(Periode) like '%{0}%' order by Kode +' - '+Nama asc", term.ToString().Equals("null")? "%" : term.ToLower());
            var list = ctx.Database.SqlQuery<Dropdown>(sql).ToList<Dropdown>().OrderBy(x => x.text).ToList();

            return list;
        }

        public List<Dropdown> DropdownPeriode(string term)
        {
            var sql = string.Format("select distinct Periode as id, Periode as text, Periode as [value] from Lowongan " +
                " order by Periode asc");
            var list = ctx.Database.SqlQuery<Dropdown>(sql).ToList<Dropdown>().OrderBy(x => x.text).ToList();

            return list;
        }

        public List<Dropdown> DropdownJumlah(string term)
        {
            List<Dropdown> list = new List<Dropdown>();
            var jumlah = ctx.Set<Lowongan>().SingleOrDefault(x => x.Kode.Equals(term)).Jumlah;
            for (var i = 1; i <= jumlah; i++) {
                var item = new Dropdown()
                {
                    id=i.ToString(),
                    text=i.ToString(),
                    value=i.ToString()
                };
                list.Add(item);
            }
            return list;
        }

        public List<Dropdown> DropdownCalonKaryawan(string term)
        {

            var dropdown =ctx.Set<CalonKaryawan>().Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.Kode + " - " +x.Nama
            }).Distinct<Dropdown>().OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public List<VwRankingPosisi> FindRankingPosisi(string periode,string posisi,string jumlah) {
            if (periode==null)
            {
                var lp = DropdownPeriode("");
                periode = lp[lp.Count() - 1].id;
            }

            if (posisi == null)
            {
                var ls = DropdownPosisi(periode);
                posisi = ls[ls.Count() - 1].id;
            }

            if (jumlah == null)
            {
                var lj = DropdownJumlah(posisi);
                jumlah =lj[lj.Count() - 1].id;
            }

            List<VwRankingPosisi> vwRankingPosisi = ctx.Set<VwRankingPosisi>().Where(x=>x.Periode.Equals(periode)
            && x.Posisi.Equals(posisi)).OrderByDescending(x => x.NilaiTotal).Take(int.Parse(jumlah)).ToList();


            return vwRankingPosisi;
        }

        public List<VwRankingScore> FindRankingScoring(string periode)
        {
            if (periode==null)
            {
                var lp = DropdownPeriode("");
                periode = lp[lp.Count() - 1].id;
            }
            List<VwRankingScore> vwRankingScore = ctx.Set<VwRankingScore>().Where(x => x.Periode.Equals(periode)).OrderByDescending(x=>x.NilaiTotal).ToList();
       
            return vwRankingScore;
        }

        public Gap Find(string Periode,string Posisi)
        {
            Gap gap = new Gap();
            using (var ctx = new AhpEntities()) {
                gap.prosesGap=ctx.SpProseGap(Posisi, Periode).ToList();
                gap.kriterias = ctx.Kriterias.OrderBy(x => x.Kode).ToList();
            }
            
            var calonKaryawanGaps = (from a in (gap.prosesGap.GroupBy(x => 
            new {
              x.KodeKaryawan,
              x.Nama,
              x.KodeLowongan,
            x.Periode}).Select(g => g.FirstOrDefault()).ToList())
              select new CalonKaryawanGap {
                  kodeKaryawan=a.KodeKaryawan,
                  kodeLowongan=a.KodeLowongan,
                  namaKaryawan=a.Nama,
                  periode=a.Periode
              }).ToList();

            foreach (var c in calonKaryawanGaps) {
                var prosesGap = gap.prosesGap.Where(x => x.KodeKaryawan.Equals(c.kodeKaryawan) 
                && x.KodeLowongan.Equals(c.kodeLowongan)).ToList();
                List<NilaiGapBobot> nilaiGapBobots = new List<NilaiGapBobot>();
                foreach (var item in prosesGap) {
                    NilaiGapBobot nilaiGapBobot = new NilaiGapBobot();
                    nilaiGapBobot.kodeKriteria = item.KodeKriteria;
                    nilaiGapBobot.kriteria = item.Kriteria;
                    nilaiGapBobot.nilaiBobot = (decimal)item.NilaiBobot;
                    nilaiGapBobot.nilaiAlternatif =(decimal) item.NilaiAlternatif;
                    nilaiGapBobot.nilaiGap= (decimal)item.NilaiGap;
                    nilaiGapBobot.ncf =Math.Round((decimal)item.Ncf+0.0000m,4);
                    nilaiGapBobot.nsf = Math.Round((decimal)item.Nsf + 0.0000m, 4);
                    nilaiGapBobot.nilaiTotal = Math.Round((decimal)item.NilaiTotal + 0.0000m, 4);
                    nilaiGapBobot.nilaiTarget = (decimal)gap.kriterias.SingleOrDefault(x => x.Kode.Equals(item.KodeKriteria)).NilaiTarget;
                    nilaiGapBobots.Add(nilaiGapBobot);
                }
                c.nilaiGapBobots = nilaiGapBobots;
            }

            gap.calonKaryawanGaps = calonKaryawanGaps;

            return gap;
        }

     


    }
}
