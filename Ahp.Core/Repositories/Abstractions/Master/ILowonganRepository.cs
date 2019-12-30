using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using System.Collections.Generic;

namespace Ahp.Core.Repositories.Abstractions.Master
{
    interface ILowonganRepository: IGenericDataRepository<Lowongan>
    {
        List<Dropdown> DropdownPeriode(string term);
    }
}
