namespace Umbraco.Plugins.Connector.Controllers
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    [Obsolete]
    public class TotalCodeTenantHomeController : RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            return base.Index(model);
        }
    }
}
