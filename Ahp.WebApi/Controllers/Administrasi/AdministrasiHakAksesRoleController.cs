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
    [RoutePrefix(@"api/AdministrasiHakAksrole")]
    public class AdministrasiHakAksRoleController : BaseApiController<AdministrasiHakAksesRole>
    {
        private GenericContext ctx;
        private IAdministrasiHakAksesRoleRepository genRepo;

        public AdministrasiHakAksRoleController()
        {
            ctx = new GenericContext();
            genRepo = new AdministrasiHakAksesRoleRepository(ctx);
            base.repo = genRepo;

            
        }


        [HttpGet, Route("duallist")]
        public HttpResponseMessage DualList(string kode)
        {
            try
            {
                var data = genRepo.DualList(kode);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);

                return result;
            }
            catch (Exception ex)
            {
                var result = Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

                return result;
            }
        }
        

    }
}
