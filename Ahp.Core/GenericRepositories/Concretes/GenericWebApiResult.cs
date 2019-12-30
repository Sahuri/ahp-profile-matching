using Ahp.Core.Common;
using Ahp.Core.Repositories.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Ahp.Core.Reposirories.Concretes
{
    public class GenericWebApiResult<T> : IDisposable, IGenericWebApiResult
    where T : class
    {
        public string ApiVersion
        {
            get
            {
                return "1.0";
            }
        }

        public bool Success { get; set; }
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public string Message { get; set; }
        public string MessageException { get; set; }
        public string StackTrace { get; set; }
        public dynamic More { get; set; }
        public List<Dropdown> Dropdown { get; set; }

        public GenericWebApiResult()
        {
            this.More = new ExpandoObject();
        }

        public GenericWebApiResult(T data)
        {
            this.Data = data;
        }

        public GenericWebApiResult(T data, bool isSuccess = true)
        {
            this.Data = data;
            this.Success = isSuccess;
        }

        public GenericWebApiResult(Exception ex)
        {
            this.Success = false;
            //var errMessage = ex.InnerException != null ? ex.InnerException.InnerException != null ? ex.InnerException.InnerException.InnerException != null
            //    ? ex.InnerException.InnerException.InnerException.Message : ex.InnerException.InnerException.Message : ex.InnerException.Message : ex.Message;

            this.Message = string.Join(",", ex.Messages()); //errMessage;
            this.StackTrace = ex.StackTrace;

            MessageException = this.Message; //errMessage;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
