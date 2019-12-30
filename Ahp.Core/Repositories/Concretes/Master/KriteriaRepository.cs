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
    public class KriteriaRepository : GenericDataRepository<Kriteria, IGenericContext>, IKriteriaRepository
    {
        protected IGenericContext ctx;
        public KriteriaRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        public override bool Create(Kriteria model)
        {
            var result = base.Create(model);

            return result;
        }

        public override bool Update(Kriteria model)
        {
            
           
            var rec = base.GetSingle(model.Kode);
            if (rec != null)
            {
                rec.EigenvectorScore = model.EigenvectorScore;
                rec.Faktor = model.Faktor;
                rec.JumlahBaris = model.JumlahBaris;
                rec.CreatedBy = model.CreatedBy;
                rec.CreatedDate = model.CreatedDate;
                rec.Nama = model.Nama;
                rec.NilaiKali = model.NilaiKali;
                rec.NilaiTarget = model.NilaiTarget;
                rec.UpdatedBy = this.UserProfile.UserID;
                rec.UpdatedDate = DateTime.Now;
            }

            return base.Update(rec);
        }

        public override List<Dropdown> Dropdown(Kriteria model, string term)
        {
            IEnumerable<Kriteria> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<Kriteria>();
            }
            else
            {
                records = ctx.Set<Kriteria>()
                .Where(x => x.Nama.ToLower().Contains(term.ToLower()));
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode.ToString(),
                value = x.Kode.ToString(),
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(Kriteria model, string term)
        {
            IEnumerable<Kriteria> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<Kriteria>()
                .OrderBy(x => x.Kode);
            }
            else
            {
                records = ctx.Set<Kriteria>()
                .Where(x => x.Kode.ToString().ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.Nama);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode.ToString(),
                value = x.Kode.ToString(),
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }


        public List<Dropdown> DropdownNilaiTarget(string term)
        {
            var dropdown = new List<Dropdown>();
            for (int i = 1; i <= 9; i++)
            {
                dropdown.Add(new Dropdown()
                {
                    id = String.Format("{0}", i),
                    text = String.Format("{0}", i),
                    value = String.Format("{0}", i)
                }
            );
            }
            return dropdown;
        }

        public override FormatedList<Kriteria> DataTables()
        {
            var dataTables = base.DataTables();
            return dataTables;
        }

        public override IQueryable<Kriteria> FindBy(Expression<Func<Kriteria, bool>> predicate)
        {
            var recs = base.FindBy(predicate);
            return recs;
        }

        public override List<Kriteria> GetAll()
        {
            var recs = base.GetAll();
            return recs;
        }


        public override List<Kriteria> GetList(Func<Kriteria, bool> where, params Expression<Func<Kriteria, object>>[] navigationProperties)
        {
            var recs = base.GetList(where, navigationProperties);
            return recs;
        }

        public override Kriteria GetSingle(Func<Kriteria, bool> where, params Expression<Func<Kriteria, object>>[] navigationProperties)
        {
            var rec = base.GetSingle(where, navigationProperties);
 
            return rec;
        }

        public override Kriteria GetSingle(params object[] keyValues)
        {
            var rec = base.GetSingle(keyValues);
            
            return rec;
        }

        public override Kriteria GetSingle(string paramValues)
        {
            var rec = base.GetSingle(paramValues);
             return rec;
        }

    }
}
