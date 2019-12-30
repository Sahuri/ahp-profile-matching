using Ahp.Core.Models;
using Ahp.Core.GenericRepositories.Abstractions;

namespace Ahp.Core.Repositories.Abstractions.Administrasi
{
    public interface IAdministrasiHakAksesRepository : IGenericDataRepository<AdministrasiHakAks>
    {
        bool CreateWithDetail(ParamAdministrasiHakAks data); 
        bool UpdateWithDetail(ParamAdministrasiHakAks data);
        bool DeleteWithDetail(string kode);
    }
}
