using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactInfo.Startup))]
namespace ContactInfo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
