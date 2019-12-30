using Ahp.Core.Common;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Concretes.Proses;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ahp.WebApi.Controllers.Proses
{
    [Authorize]
    [RoutePrefix(@"api/perbandingankriteria")]
    public class PerbandinganKriteriaController : BaseApiController<PerbandinganKriteria>
    {
        private GenericContext ctx;
        private PerbandinganKriteriaRepository genRepo;

        public PerbandinganKriteriaController()
        {
            ctx = new GenericContext();
            genRepo = new PerbandinganKriteriaRepository(ctx);
            base.repo = genRepo;
        }

        [HttpGet]
        public virtual HttpResponseMessage SpDataTables()
        {
            try
            {
                var datatable = genRepo.SpDataTables();
                var result = Request.CreateResponse(System.Net.HttpStatusCode.OK, datatable);

                return result;
            }
            catch (Exception ex)
            {
                var result = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);

                return result;
            }
        }

        [HttpPost]
        public virtual Matrix Create(List<SpPerbandinganKriteria_Result> data)
        {
            try
            {
                
                    return genRepo.Create(data);
                
            }
            catch (Exception ex)
            {
                    return new Matrix();
                
            }
        }
    }
}
