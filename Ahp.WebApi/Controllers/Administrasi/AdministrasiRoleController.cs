using Ahp.Core.Common;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using Ahp.Core.Repositories.Concretes.Administrasi;
using System.Web.Http;

namespace Ahp.WebApi.Controllers.Administrasi
{
    [Authorize]
    [RoutePrefix(@"api/administrasirole")]
    public class AdministrasiRoleController : BaseApiController<AdministrasiRole>
    {
        private GenericContext ctx;
        private IAdministrasiRoleRepository genRepo;

        public AdministrasiRoleController()
        {
            ctx = new GenericContext();
            genRepo = new AdministrasiRoleRepository(ctx);
            base.repo = genRepo;

            
        }

        [HttpPost, Route("createwithdetail")]
        public IGenericWebApiResult CreateWithDetail(ParamHeaderDetail<AdministrasiRole, AdministrasiRoleDtl> data)
        {
            using (var result = new GenericWebApiResult<AdministrasiRole>())
            {
                result.Success = genRepo.CreateWithDetail(data.header, data.detail);
                result.Data = data.header;
                result.Message = result.Success ? BaseConstants.MESSAGE_CREATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                return result;
            }
        }

        [HttpPut, Route("updatewithdetail")]
        public IGenericWebApiResult UpdateWithDetail(ParamHeaderDetail<AdministrasiRole, AdministrasiRoleDtl> data)
        {
            using (var result = new GenericWebApiResult<AdministrasiRole>())
            {
                result.Success = genRepo.UpdateWithDetail(data.header, data.detail);
                result.Data = data.header;
                result.Message = result.Success ? BaseConstants.MESSAGE_UPDATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                return result;
            }
        }

        [HttpDelete, Route("deletewithdetail")]
        public IGenericWebApiResult CreateWithDetail(string kode)
        {
            using (var result = new GenericWebApiResult<AdministrasiRole>())
            {
                result.Success = genRepo.DeleteWithDetail(kode);
                result.Message = result.Success ? BaseConstants.MESSAGE_DELETE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                return result;
            }
        }

        [HttpPost, Route("createwithdetailuser")]
        public IGenericWebApiResult CreateWithDetailUser(ParamHeaderDetail<AdministrasiRole, AdministrasiRoleUser> data)
        {
            using (var result = new GenericWebApiResult<AdministrasiRole>())
            {
                result.Success = genRepo.CreateWithDetailUser(data.header, data.detail);
                result.Data = data.header;
                result.Message = result.Success ? BaseConstants.MESSAGE_CREATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                return result;
            }
        }
    }
}
