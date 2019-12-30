using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using Ahp.Core.Repositories.Concretes.Administrasi;
using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ahp.WebApi.Controllers.Administrasi
{
    [Authorize]
    [RoutePrefix(@"api/administrasimenu")]
    public class AdministrasiMenuController : BaseApiController<AdministrasiMenu>
    {
        private GenericContext ctx;
        private IAdministrasiMenuRepository genRepo;

        public AdministrasiMenuController()
        {
            ctx = new GenericContext();
            genRepo = new AdministrasiMenuRepository(ctx);
            base.repo = genRepo;
        }

        [HttpGet, Route("getmenubyuser")]
        public HttpResponseMessage GetMenuByUser()
        {
            try
            {
                var menuItem = genRepo.GetMenuByUser();
                dynamic rootMenuItem = new ExpandoObject();
                rootMenuItem.items = menuItem;

                var result = Request.CreateResponse(HttpStatusCode.OK, (object)rootMenuItem);

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
