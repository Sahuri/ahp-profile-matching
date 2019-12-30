using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using DataTablesParser;
using System.Collections.Generic;

namespace Ahp.Core.Repositories.Abstractions.Proses
{
    interface IPerbandinganKriteriaRepository: IGenericDataRepository<PerbandinganKriteria>
    {
        Matrix Create(List<SpPerbandinganKriteria_Result> model);
        List<SpPerbandinganKriteria_Result> SpDataTables();
    }
}
