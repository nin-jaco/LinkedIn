using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReactMvc.Startup))]
namespace ReactMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
