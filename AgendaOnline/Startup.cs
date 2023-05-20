using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgendaOnline.Startup))]
namespace AgendaOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
