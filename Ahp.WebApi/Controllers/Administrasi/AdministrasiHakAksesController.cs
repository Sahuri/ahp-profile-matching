using Ahp.Core.Common;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using Ahp.Core.Repositories.Concretes.Administrasi;
using Ahp.WebApi.Controllers;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ahp.WebApi.Controllers.Administrasi
{
    [Authorize]
    [RoutePrefix(@"api/AdministrasiHakAks")]
    public class AdministrasiHakAksController : BaseApiController<AdministrasiHakAks>
    {
        private GenericContext ctx;
        private IAdministrasiHakAksesRepository genRepo;

        public AdministrasiHakAksController()
        {
            ctx = new GenericContext();
            genRepo = new AdministrasiHakAksesRepository(ctx);
            base.repo = genRepo;

            
        }


        [HttpPost, Route("createwithdetail")]
        public IGenericWebApiResult CreateWithDetail(ParamAdministrasiHakAks data)
        {
            using (var result = new GenericWebApiResult<AdministrasiHakAks>())
            {
                result.Success = genRepo.CreateWithDetail(data);
                result.Data = data.HakAkses;
                result.Message = result.Success ? BaseConstants.MESSAGE_CREATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                return result;
            }
        }

        [HttpPut, Route("updatewithdetail")]
        public IGenericWebApiResult UpdateWithDetail(ParamAdministrasiHakAks data)
        {
            using (var result = new GenericWebApiResult<AdministrasiHakAks>())
            {
                result.Success = genRepo.UpdateWithDetail(data);
                result.Data = data.HakAkses;
                result.Message = result.Success ? BaseConstants.MESSAGE_UPDATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                return result;
            }
        }

        [HttpDelete, Route("deletewithdetail")]
        public IGenericWebApiResult DeleteWithDetail(string kode)
        {
            using (var result = new GenericWebApiResult<AdministrasiHakAks>())
            {
                result.Success = genRepo.DeleteWithDetail(kode);
                result.Message = result.Success ? BaseConstants.MESSAGE_DELETE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                return result;
            }
        }
    }
}
