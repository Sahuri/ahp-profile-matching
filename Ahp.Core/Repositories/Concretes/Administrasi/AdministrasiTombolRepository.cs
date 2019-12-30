using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Ahp.Core.Repositories.Concretes.Administrasi
{
    public class AdministrasiTombolRepository : GenericDataRepository<AdministrasiTombol, IGenericContext>
    {
        protected IGenericContext ctx;
        public AdministrasiTombolRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        public override List<Dropdown> Dropdown(string term)
        {
            IEnumerable<AdministrasiTombol> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiTombol>();
            }
            else
            {
                records = ctx.Set<AdministrasiTombol>()
                .Where(x => x.Title.ToLower().Contains(term.ToLower()));
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.Title
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(string term)
        {
            IEnumerable<AdministrasiTombol> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiTombol>()
                .OrderBy(x => x.Kode);
            }
            else
            {
                records = ctx.Set<AdministrasiTombol>()
                .Where(x => x.Kode.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.Title);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.Title
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }
    }
}
