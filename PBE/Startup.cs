using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PBE.Startup))]
namespace PBE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
