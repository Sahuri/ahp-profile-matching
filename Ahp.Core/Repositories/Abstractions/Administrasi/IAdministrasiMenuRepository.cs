using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using System.Collections.Generic;

namespace Ahp.Core.Repositories.Abstractions.Administrasi
{
    public interface IAdministrasiMenuRepository : IGenericDataRepository<AdministrasiMenu>
    {
        List<MenuItem> GetMenuByUser();
    }
}
