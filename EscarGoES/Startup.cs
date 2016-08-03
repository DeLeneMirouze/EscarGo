using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EscarGoES.Startup))]
namespace EscarGoES
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
