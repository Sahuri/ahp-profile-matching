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
    public class AdministrasiHakAksesRoleRepository : GenericDataRepositoryExtended<AdministrasiHakAksesRole, IGenericContext>, IAdministrasiHakAksesRoleRepository
    {
        protected IGenericContext ctx;
        public AdministrasiHakAksesRoleRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        #region IAdministrasiHakAksesRoleDtlRepository Members
        public override List<Dropdown> Dropdown(string term)
        {
            IEnumerable<AdministrasiHakAksesRole> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiHakAksesRole>();
            }
            else
            {
                records = ctx.Set<AdministrasiHakAksesRole>()
                .Where(x => x.KodeRole.ToLower().Contains(term.ToLower()));
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeHakAkses,
                value = x.KodeHakAkses,
                text = x.KodeRole
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(string term)
        {
            IEnumerable<AdministrasiHakAksesRole> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiHakAksesRole>()
                .OrderBy(x => x.KodeHakAkses);
            }
            else
            {
                records = ctx.Set<AdministrasiHakAksesRole>()
                .Where(x => x.KodeHakAkses.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.KodeRole);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeHakAkses,
                value = x.KodeHakAkses,
                text = x.KodeRole
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public IQueryable<string> DualList(string kode)
        {
            if (string.IsNullOrEmpty(kode))
            {
                var records = ctx.Set<AdministrasiHakAksesRole>()
                    .Select(x => x.KodeRole);
                return records;
            }
            else
            {
                var records = ctx.Set<AdministrasiHakAksesRole>()
                .Where(x => x.KodeHakAkses == kode)
                .Select(x => x.KodeRole);

                return records;
            }
        }
        #endregion
    }
}
