using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Concretes.Administrasi;
using System.Web.Http;

namespace Ahp.WebApi.Controllers.Administrasi
{
    [Authorize]
    [RoutePrefix(@"api/administrasitombol")]
    public class AdministrasiTombolController : BaseApiController<AdministrasiTombol>
    {
        private GenericContext ctx;
        private IGenericDataRepository<AdministrasiTombol> genRepo;

        public AdministrasiTombolController()
        {
            ctx = new GenericContext();
            genRepo = new AdministrasiTombolRepository(ctx);
            base.repo = genRepo;

            
        }
    }
}
