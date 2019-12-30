using System.Linq;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;

namespace Ahp.Core.Repositories.Abstractions.Administrasi
{
    public interface IAdministrasiHakAksesMenuRepository : IGenericDataRepository<AdministrasiHakAksesMenu>
    {
       IQueryable<string> DualList(string kode);
    }
}
