using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using System.Collections.Generic;

namespace Ahp.Core.Repositories.Abstractions.Administrasi
{
    public interface IAdministrasiRoleRepository : IGenericDataRepository<AdministrasiRole>
    {
        bool CreateWithDetail(AdministrasiRole header, List<AdministrasiRoleDtl> detail);
        bool UpdateWithDetail(AdministrasiRole header, List<AdministrasiRoleDtl> detail);
        bool DeleteWithDetail(string kode);

        bool CreateWithDetailUser(AdministrasiRole header, List<AdministrasiRoleUser> detail);
    }
}
