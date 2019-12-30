using Newtonsoft.Json.Linq;
using Ahp.Core.Common;

using System;

namespace Ahp.Core.Repositories.Abstractions
{
    public interface IGenericWebApiController<T> : IDisposable
        where T : class
    {
        IGenericWebApiResult Get();
        IGenericWebApiResult Post(T data);
        IGenericWebApiResult Put(T data);
        IGenericWebApiResult Delete(params object[] keyValues);
    }
}
