using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCTest.Startup))]
namespace MVCTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
