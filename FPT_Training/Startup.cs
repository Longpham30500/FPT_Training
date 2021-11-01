using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPT_Training.Startup))]
namespace FPT_Training
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
