using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahp.Core.Repositories.Abstractions.Master
{
    interface ICalonKaryawanRepository: IGenericDataRepository<CalonKaryawan>
    {
    
        List<Dropdown> DropdownKelamin(string term);
        List<Dropdown> DropdownPendidikan(string term);
    }
}
