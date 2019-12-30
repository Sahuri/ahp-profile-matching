using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using System.Linq;

namespace Ahp.Core.Repositories.Abstractions.Administrasi
{
    public interface IAdministrasiHakAksesTombolRepository : IGenericDataRepository<AdministrasiHakAksesTombol>
    {
        IQueryable<string> DualList(string kode);
    }
}
