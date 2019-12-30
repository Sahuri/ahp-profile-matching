using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Repositories.Abstractions;

namespace Ahp.Core.Repositories.Concretes
{
    public class DataRepository<T> : GenericDataRepository<T, IGenericContext>
        where T : class
    {
        protected new IGenericContext ctx;
        
        public DataRepository(IGenericContext _ctx)
            : base(_ctx)
        {
            this.ctx = _ctx;
        }
    }
}
