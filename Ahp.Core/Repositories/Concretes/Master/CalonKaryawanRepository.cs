using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using System.Collections.Generic;
using System.Linq;
using DataTablesParser;
using System;
using System.Linq.Expressions;
using Ahp.Core.Repositories.Abstractions.Master;

namespace Ahp.Core.Repositories.Concretes.Master
{
    public class CalonKaryawanRepository : GenericDataRepository<CalonKaryawan, IGenericContext>, ICalonKaryawanRepository
    {
        protected IGenericContext ctx;
        public CalonKaryawanRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        public override bool Create(CalonKaryawan model)
        {
            model.Kode = Guid.NewGuid().ToString("N").Substring(0, 32);
            var result = base.Create(model);

            return result;
        }

        public override bool Update(CalonKaryawan model)
        {
            
           
            var rec = base.GetSingle(model.Kode);
            if (rec != null)
            {
                rec.Alamat = model.Alamat;
                rec.Email = model.Email;
                rec.Kelamin = model.Kelamin;
                rec.CreatedBy = model.CreatedBy;
                rec.CreatedDate = model.CreatedDate;
                rec.Nama = model.Nama;
                rec.Keterangan = model.Keterangan;
                rec.NilaiAkhir = model.NilaiAkhir;
                rec.UpdatedBy = this.UserProfile.UserID;
                rec.UpdatedDate = DateTime.Now;
                rec.Pendidikan = model.Pendidikan;
                rec.Telepon = model.Telepon;
                rec.Tgllahir = model.Tgllahir;
            }

            return base.Update(rec);
        }

        public override List<Dropdown> Dropdown(CalonKaryawan model, string term)
        {
            IEnumerable<CalonKaryawan> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<CalonKaryawan>();
            }
            else
            {
                records = ctx.Set<CalonKaryawan>()
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

        public List<Dropdown> DropdownKelamin(string term)
        {
            

            var laki = new Dropdown()
            {
                id = "Laki-Laki",
                value = "Laki-Laki",
                text = "Laki-Laki"
            };

            var perempuan = new Dropdown()
            {
                id = "Perempuan",
                value = "Perempuan",
                text = "Perempuan"
            };

            var dropdown = new List<Dropdown>() { laki, perempuan };

            return dropdown;
        }

        public List<Dropdown> DropdownPendidikan(string term)
        {


            var sd = new Dropdown()
            {
                id = "SD",
                value = "SD",
                text = "SD"
            };

            var smp = new Dropdown()
            {
                id = "SMP",
                value = "SMP",
                text = "SMP"
            };

            var sma = new Dropdown()
            {
                id = "SMA",
                value = "SMA",
                text = "SMA"
            };

            var diploma = new Dropdown()
            {
                id = "DIPLOMA",
                value = "DIPLOMA",
                text = "DIPLOMA"
            };

            var s1 = new Dropdown()
            {
                id = "S1",
                value = "S1",
                text = "S1"
            };

            var s2 = new Dropdown()
            {
                id = "S2",
                value = "S2",
                text = "S2"
            };

            var s3 = new Dropdown()
            {
                id = "S3",
                value = "S3",
                text = "S3"
            };
            var dropdown = new List<Dropdown>() { sd, smp,sma,diploma,s1,s2,s3 };

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(CalonKaryawan model, string term)
        {
            IEnumerable<CalonKaryawan> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<CalonKaryawan>()
                .OrderBy(x => x.Kode);
            }
            else
            {
                records = ctx.Set<CalonKaryawan>()
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

        


        public override FormatedList<CalonKaryawan> DataTables()
        {
            var dataTables = base.DataTables();
            return dataTables;
        }

        public override IQueryable<CalonKaryawan> FindBy(Expression<Func<CalonKaryawan, bool>> predicate)
        {
            var recs = base.FindBy(predicate);
            return recs;
        }

        public override List<CalonKaryawan> GetAll()
        {
            var recs = base.GetAll();
            return recs;
        }


        public override List<CalonKaryawan> GetList(Func<CalonKaryawan, bool> where, params Expression<Func<CalonKaryawan, object>>[] navigationProperties)
        {
            var recs = base.GetList(where, navigationProperties);
            return recs;
        }

        public override CalonKaryawan GetSingle(Func<CalonKaryawan, bool> where, params Expression<Func<CalonKaryawan, object>>[] navigationProperties)
        {
            var rec = base.GetSingle(where, navigationProperties);
 
            return rec;
        }

        public override CalonKaryawan GetSingle(params object[] keyValues)
        {
            var rec = base.GetSingle(keyValues);
            
            return rec;
        }

        public override CalonKaryawan GetSingle(string paramValues)
        {
            var rec = base.GetSingle(paramValues);
             return rec;
        }

    }
}
