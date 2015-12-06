using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Quilt4.Service.Startup))]

namespace Quilt4.Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
