using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ahp.Core.Repositories.Concretes.Administrasi
{
    public class AdministrasiHakAksesMenuRepository : GenericDataRepositoryExtended<AdministrasiHakAksesMenu, IGenericContext>, IAdministrasiHakAksesMenuRepository
    {
        protected IGenericContext ctx;
        public AdministrasiHakAksesMenuRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        #region IAdministrasiHakAksesMenuDtlRepository Members
        public override List<Dropdown> Dropdown(string term)
        {
            IEnumerable<AdministrasiHakAksesMenu> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiHakAksesMenu>();
            }
            else
            {
                records = ctx.Set<AdministrasiHakAksesMenu>()
                .Where(x => x.KodeMenu.ToLower().Contains(term.ToLower()));
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeHakAkses,
                value = x.KodeHakAkses,
                text = x.KodeMenu
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(string term)
        {
            IEnumerable<AdministrasiHakAksesMenu> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiHakAksesMenu>()
                .OrderBy(x => x.KodeHakAkses);
            }
            else
            {
                records = ctx.Set<AdministrasiHakAksesMenu>()
                .Where(x => x.KodeHakAkses.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.KodeMenu);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeHakAkses,
                value = x.KodeHakAkses,
                text = x.KodeMenu
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public IQueryable<string> DualList(string kode)
        {
            if (string.IsNullOrEmpty(kode))
            {
                var records = ctx.Set<AdministrasiHakAksesMenu>()
                    .Select(x => x.KodeMenu);
                return records;
            }
            else
            {
                var records = ctx.Set<AdministrasiHakAksesMenu>()
                .Where(x => x.KodeHakAkses == kode)
                .Select(x => x.KodeMenu);

                return records;
            }
        }
        #endregion
    }
}
