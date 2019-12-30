using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ahp.Core.Repositories.Concretes.Administrasi
{
    public class AdministrasiRoleDtlRepository : GenericDataRepositoryExtended<AdministrasiRoleDtl, IGenericContext>, IAdministrasiRoleDtlRepository
    {
        protected IGenericContext ctx;
        public AdministrasiRoleDtlRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        #region IAdministrasiRoleDtlDtlRepository Members
        public override List<Dropdown> Dropdown(string term)
        {
            IEnumerable<AdministrasiRoleDtl> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiRoleDtl>();
            }
            else
            {
                records = ctx.Set<AdministrasiRoleDtl>()
                .Where(x => x.KodeWilayahKerja.ToLower().Contains(term.ToLower()));
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeRole,
                value = x.KodeRole,
                text = x.KodeWilayahKerja
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(string term)
        {
            IEnumerable<AdministrasiRoleDtl> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiRoleDtl>()
                .OrderBy(x => x.KodeRole);
            }
            else
            {
                records = ctx.Set<AdministrasiRoleDtl>()
                .Where(x => x.KodeRole.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.KodeWilayahKerja);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeRole,
                value = x.KodeRole,
                text = x.KodeWilayahKerja
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public IQueryable<string> DualList(string kode)
        {
            if (string.IsNullOrEmpty(kode))
            {
                var records = ctx.Set<AdministrasiRoleDtl>()
                    .Select(x => x.KodeWilayahKerja);
                return records;
            }
            else
            {
                var records = ctx.Set<AdministrasiRoleDtl>()
                .Where(x => x.KodeRole == kode)
                .Select(x => x.KodeWilayahKerja);

                return records;
            }
        }
        #endregion
    }
}
