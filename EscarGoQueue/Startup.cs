using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EscarGoCache.Startup))]
namespace EscarGoCache
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
