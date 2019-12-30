using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Ahp.WebApi.Startup))]

namespace Ahp.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
