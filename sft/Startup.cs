using Microsoft.Owin;
using Owin;
using sft;

[assembly: OwinStartup(typeof(Startup))]
namespace sft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
