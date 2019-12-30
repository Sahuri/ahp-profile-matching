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
    public class AdministrasiHakAksesTombolRepository : GenericDataRepositoryExtended<AdministrasiHakAksesTombol, IGenericContext>, IAdministrasiHakAksesTombolRepository
    {
        protected IGenericContext ctx;
        public AdministrasiHakAksesTombolRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        #region IAdministrasiHakAksesTombolDtlRepository Members
        public override List<Dropdown> Dropdown(string term)
        {
            IEnumerable<AdministrasiHakAksesTombol> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiHakAksesTombol>();
            }
            else
            {
                records = ctx.Set<AdministrasiHakAksesTombol>()
                .Where(x => x.KodeTombol.ToLower().Contains(term.ToLower()));
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeHakAkses,
                value = x.KodeHakAkses,
                text = x.KodeTombol
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(string term)
        {
            IEnumerable<AdministrasiHakAksesTombol> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiHakAksesTombol>()
                .OrderBy(x => x.KodeHakAkses);
            }
            else
            {
                records = ctx.Set<AdministrasiHakAksesTombol>()
                .Where(x => x.KodeHakAkses.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.KodeTombol);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.KodeHakAkses,
                value = x.KodeHakAkses,
                text = x.KodeTombol
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public IQueryable<string> DualList(string kode)
        {
            if (string.IsNullOrEmpty(kode))
            {
                var records = ctx.Set<AdministrasiHakAksesTombol>()
                    .Select(x => x.KodeTombol);
                return records;
            }
            else
            {
                var records = ctx.Set<AdministrasiHakAksesTombol>()
                .Where(x => x.KodeHakAkses == kode)
                .Select(x => x.KodeTombol);

                return records;
            }
        }
        #endregion
    }
}
