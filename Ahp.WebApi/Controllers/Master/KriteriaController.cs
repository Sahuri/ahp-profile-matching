using Ahp.Core.Common;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Concretes.Master;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ahp.WebApi.Controllers.Master
{
    [Authorize]
    [RoutePrefix(@"api/kriteria")]
    public class KriteriaController : BaseApiController<Kriteria>
    {
        private GenericContext ctx;
        private KriteriaRepository genRepo;

        public KriteriaController()
        {
            ctx = new GenericContext();
            genRepo = new KriteriaRepository(ctx);
            base.repo = genRepo;
        }

        [HttpPost]
        public virtual IGenericWebApiResult DropdownNilaiTarget([FromBody]string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<Dropdown>())
                {
                    result.Dropdown = genRepo.DropdownNilaiTarget(term);
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<Dropdown>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;
                    result.Dropdown = new List<Dropdown>();

                    return result;
                }
            }
        }
    }
}
