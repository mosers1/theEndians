using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_5051.Startup))]
namespace _5051
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
