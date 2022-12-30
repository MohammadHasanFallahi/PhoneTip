using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhoneTipProject.Startup))]
namespace PhoneTipProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
