using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdleIronman.Startup))]
namespace IdleIronman
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
