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
using Ahp.Core.Repositories.Abstractions.Master;

namespace Ahp.Core.Repositories.Concretes.Master
{
    public class LowonganRepository : GenericDataRepository<Lowongan, IGenericContext>, ILowonganRepository
    {
        protected IGenericContext ctx;
        public LowonganRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        public override bool Create(Lowongan model)
        {
            model.Kode = DateTime.Now.ToString("yyMMddhhFFF");
            var result = base.Create(model);

            return result;
        }

        public override bool Update(Lowongan model)
        {
            
           
            var rec = base.GetSingle(model.Kode);
            if (rec != null)
            {
                rec.Jumlah = model.Jumlah;
                rec.Nama = model.Nama;
                rec.NoSurat = model.NoSurat;
                rec.CreatedBy = model.CreatedBy;
                rec.CreatedDate = model.CreatedDate;
                rec.Periode = model.Nama;
                rec.TglAkhir = model.TglAkhir;
                rec.TglMulai = model.TglMulai;
                rec.UpdatedBy = this.UserProfile.UserID;
                rec.UpdatedDate = DateTime.Now;
                
            }

            return base.Update(rec);
        }

        public override List<Dropdown> Dropdown(Lowongan model, string term)
        {
            IEnumerable<Lowongan> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<Lowongan>();
            }
            else
            {
                records = ctx.Set<Lowongan>()
                .Where(x => x.Nama.ToLower().Contains(term.ToLower()));
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public List<Dropdown> DropdownPeriode(string term)
        {
            Dictionary<int,string> d = new Dictionary<int, string>();
            d.Add(1,"Januari");
            d.Add(2, "Februari");
            d.Add( 3, "Maret");
            d.Add( 4, "April");
            d.Add(5, "Mei");
            d.Add(6, "Juni");
            d.Add(7, "Juli");
            d.Add(8, "Agustus");
            d.Add(9, "September");
            d.Add(10, "Oktober");
            d.Add(11, "November");
            d.Add(12, "Desember");
            var dropdown = new List<Dropdown>();
            for (int i = 1; i <= 12; i++) {
                dropdown.Add(new Dropdown() {
                    id = String.Format("{0} {1}", d[i], DateTime.Now.Year.ToString()),
                    text= String.Format("{0} {1}", d[i], DateTime.Now.Year.ToString()),
                    value= String.Format("{0} {1}", d[i], DateTime.Now.Year.ToString())
                }
            );
            }
            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(Lowongan model, string term)
        {
            IEnumerable<Lowongan> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<Lowongan>()
                .OrderBy(x => x.Kode);
            }
            else
            {
                records = ctx.Set<Lowongan>()
                .Where(x => x.Kode.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.Nama);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        


        public override FormatedList<Lowongan> DataTables()
        {
            var dataTables = base.DataTables();
            return dataTables;
        }

        public override IQueryable<Lowongan> FindBy(Expression<Func<Lowongan, bool>> predicate)
        {
            var recs = base.FindBy(predicate);
            return recs;
        }

        public override List<Lowongan> GetAll()
        {
            var recs = base.GetAll();
            return recs;
        }


        public override List<Lowongan> GetList(Func<Lowongan, bool> where, params Expression<Func<Lowongan, object>>[] navigationProperties)
        {
            var recs = base.GetList(where, navigationProperties);
            return recs;
        }

        public override Lowongan GetSingle(Func<Lowongan, bool> where, params Expression<Func<Lowongan, object>>[] navigationProperties)
        {
            var rec = base.GetSingle(where, navigationProperties);
 
            return rec;
        }

        public override Lowongan GetSingle(params object[] keyValues)
        {
            var rec = base.GetSingle(keyValues);
            
            return rec;
        }

        public override Lowongan GetSingle(string paramValues)
        {
            var rec = base.GetSingle(paramValues);
             return rec;
        }

    }
}
