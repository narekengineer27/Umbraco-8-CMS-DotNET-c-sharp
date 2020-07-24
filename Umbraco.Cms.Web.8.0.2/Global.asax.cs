namespace Umbraco.Cms.Web
{
    using Umbraco.Plugins.Connector.Controllers;
    using Umbraco.Web;
    public class Global : UmbracoApplication
    {
        private JsonRpcController service;

        public override void Init()
        {
            try
            {
                service = service ?? new JsonRpcController();
            }
            catch
            {
            }
        }
    }
}