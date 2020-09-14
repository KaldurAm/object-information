using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ObjectInformation.Startup))]
namespace ObjectInformation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
