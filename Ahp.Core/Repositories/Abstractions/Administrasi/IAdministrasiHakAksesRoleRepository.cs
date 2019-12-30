using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using System.Linq;


namespace Ahp.Core.Repositories.Abstractions.Administrasi
{
    public interface IAdministrasiHakAksesRoleRepository : IGenericDataRepository<AdministrasiHakAksesRole>
    {
        IQueryable<string> DualList(string kode);
    }
}
