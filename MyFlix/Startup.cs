using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFlix.Startup))]
namespace MyFlix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
