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
    [RoutePrefix(@"api/lowongan")]
    public class LowonganController : BaseApiController<Lowongan>
    {
        private GenericContext ctx;
        private LowonganRepository genRepo;

        public LowonganController()
        {
            ctx = new GenericContext();
            genRepo = new LowonganRepository(ctx);
            base.repo = genRepo;
        }

        [HttpPost]
        public virtual IGenericWebApiResult DropdownPeriode([FromBody]string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<Dropdown>())
                {
                    result.Dropdown = genRepo.DropdownPeriode(term);
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
