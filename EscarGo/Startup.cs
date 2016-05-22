using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EscarGo.Startup))]
namespace EscarGo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
