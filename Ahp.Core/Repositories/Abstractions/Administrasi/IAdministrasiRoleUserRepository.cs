using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ahp.Core.Repositories.Abstractions.Administrasi
{
    public interface IAdministrasiRoleUserRepository : IGenericDataRepositoryExtended<AdministrasiRoleUser>
    {
        IQueryable<string> DualList(string kode);
    }
}
