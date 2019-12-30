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
    public class AdministrasiRoleUserRepository : GenericDataRepositoryExtended<AdministrasiRoleUser, IGenericContext>, IAdministrasiRoleUserRepository
    {
        protected IGenericContext ctx;
        public AdministrasiRoleUserRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        #region IAdministrasiRoleUserRepository Members
        public override List<Dropdown> Dropdown(string term)
        {
            IEnumerable<dynamic> records;
            if (string.IsNullOrEmpty(term))
            {
                records = (from a in ctx.Set<AdministrasiRoleUser>()
                           join b in ctx.Set<AdministrasiUser>() on new { a.KodeUser } equals new { KodeUser = b.Kode }
                           select new
                           {
                               a.KodeRole,
                               b.Nama
                           });


            }
            else
            {
                records = (from a in ctx.Set<AdministrasiRoleUser>()
                           join b in ctx.Set<AdministrasiUser>() on new { a.KodeUser } equals new { KodeUser = b.Kode }
                           where b.Nama.ToLower().Contains(term.ToLower())
                           select new
                           {
                               a.KodeRole,
                               b.Nama
                           });
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeRole,
                value = x.KodeRole,
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(string term)
        {
            IEnumerable<dynamic> records;
            if (string.IsNullOrEmpty(term))
            {
                records = (from a in ctx.Set<AdministrasiRoleUser>()
                           join b in ctx.Set<AdministrasiUser>() on new { a.KodeUser } equals new { KodeUser = b.Kode }
                           select new
                           {
                               a.KodeRole,
                               b.Nama
                           });


            }
            else
            {
                records = (from a in ctx.Set<AdministrasiRoleUser>()
                           join b in ctx.Set<AdministrasiUser>() on new { a.KodeUser } equals new { KodeUser = b.Kode }
                           where a.KodeRole.ToLower().Contains(term.ToLower())
                           select new
                           {
                               a.KodeRole,
                               b.Nama
                           });
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeRole,
                value = x.KodeRole,
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public IQueryable<string> DualList(string kode)
        {
            IQueryable<AdministrasiRoleUser> records;
            if (string.IsNullOrEmpty(kode))
            {
                records = ctx.Set<AdministrasiRoleUser>();
            }
            else
            {
                records = ctx.Set<AdministrasiRoleUser>()
                .Where(x => x.KodeRole.ToLower().Contains(kode.ToLower()));
            }

            var dropdown = records.Select(x => x.KodeUser);

            return dropdown;
        }
        #endregion
    }
}
