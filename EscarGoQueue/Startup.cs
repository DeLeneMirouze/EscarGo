using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EscarGoQueue.Startup))]
namespace EscarGoQueue
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
