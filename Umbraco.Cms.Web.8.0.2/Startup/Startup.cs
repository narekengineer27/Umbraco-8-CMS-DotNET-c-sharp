using Microsoft.Owin;
using Owin;
using Umbraco.Web;
[assembly: OwinStartup("TotalCodeSignalR", typeof(Umbraco.Cms.Web.Startup.Startup))]
namespace Umbraco.Cms.Web.Startup
{
    public class Startup : UmbracoDefaultOwinStartup
    {
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);
            app.MapSignalR();
        }
    }
}