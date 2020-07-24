namespace Umbraco.Plugins.Connector
{
    using System.Collections.Generic;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    public class EmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "package.manifest", ResourceLocation = "Content.App_Plugins.ApiSettingsSurface", OutputDirectory = "App_Plugins\\ApiSettingsSurface", ResourceType = ResourceType.Other },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.App_Plugins.ApiSettingsSurface.backoffice.ApiSettings", OutputDirectory = "App_Plugins\\ApiSettingsSurface\\backoffice\\ApiSettings", ResourceType = ResourceType.Directory },
            new EmbeddedResource{ FileName  = "TotalCodeTenantHomeTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.NonTemplateView, Name = "Total Code Tenant Home", Alias = "TotalCodeTenantHome", Replace = true },
            new EmbeddedResource{ FileName  = "Global.asax", ResourceLocation = "Content", OutputDirectory = "", ResourceType = ResourceType.Other, Replace = true, CreateBackup = true },
            new EmbeddedResource{ FileName  = "Global.asax.cs", ResourceLocation = "Content", OutputDirectory = "", ResourceType = ResourceType.Other, AddToVisualStudioProject = true, DependentUpon = true, DependentUponFile = "Global.asax" }
        };
    }
    public class TenantPreferencesEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.App_Plugins.TenantPreferences", OutputDirectory = "App_Plugins\\TenantPreferences", ResourceType = ResourceType.Directory }
        };
    }

    public class RegisterUpdateHomeEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            //new EmbeddedResource{ FileName  = "Layout.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.NonTemplateView, Replace = true },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.Views.Partials", OutputDirectory = "Views\\Partials", ResourceType = ResourceType.Directory },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.css", OutputDirectory = "css", ResourceType = ResourceType.Directory },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.scripts", OutputDirectory = "scripts", ResourceType = ResourceType.Directory },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.Media.img", OutputDirectory = "Media\\img", ResourceType = ResourceType.Directory },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.Media.img.favicon", OutputDirectory = "Media\\img\\favicon", ResourceType = ResourceType.Directory },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.Media.icons", OutputDirectory = "Media\\icons", ResourceType = ResourceType.Directory },
            //new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.fonts", OutputDirectory = "fonts", ResourceType = ResourceType.CreateDirectoryOnly },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.fonts.InterExtraBold", OutputDirectory = "fonts\\Inter-ExtraBold", ResourceType = ResourceType.Directory },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.fonts.InterRegular", OutputDirectory = "fonts\\Inter-Regular", ResourceType = ResourceType.Directory },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.fonts.InterSemiBold", OutputDirectory = "fonts\\Inter-SemiBold", ResourceType = ResourceType.Directory },
        };
    }

    public class ConfirmEmailEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "TotalCodeTenantConfirmEmail.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Tenant Confirm Email", Alias = "TotalCodeTenantConfirmEmail" }
        };
    }

    public class LoginEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "login.js", ResourceLocation = "Content.scripts", OutputDirectory = "scripts", ResourceType = ResourceType.Script }
        };
    }

    public class Milestone7EmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "TotalCodeTenantResetPassword.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Tenant Reset Password", Alias = "TotalCodeTenantResetPassword", Replace = true },
            new EmbeddedResource{ FileName  = "_server-errors.cshtml", ResourceLocation = "Content.Views.Partials", OutputDirectory = "Views\\Partials", ResourceType = ResourceType.Partial },
            new EmbeddedResource{ FileName  = "_reset-password-popup.cshtml", ResourceLocation = "Content.Views.Partials", OutputDirectory = "Views\\Partials", ResourceType = ResourceType.Partial },
            new EmbeddedResource{ FileName  = "_forgot-username-popup.cshtml", ResourceLocation = "Content.Views.Partials", OutputDirectory = "Views\\Partials", ResourceType = ResourceType.Partial },
            new EmbeddedResource{ FileName  = "_hidden-values.cshtml", ResourceLocation = "Content.Views.Partials", OutputDirectory = "Views\\Partials", ResourceType = ResourceType.Partial }
        };
    }

    public class ReconfigureSportsPageEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "totalCodeSportsFeedPageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Sports Feed Template", Alias = "totalCodeSportsFeedPageTemplate" , Replace = true, CreateBackup = true},
            new EmbeddedResource{ FileName  = "sports-feed.controller.js", ResourceLocation = "Content.scripts", OutputDirectory = "scripts", ResourceType = ResourceType.Script },
        };
    }

    public class EditAccountDetailsEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "totalCodeAccountEditPageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Tenant Edit Account Details", Alias = "totalCodeAccountEditPageTemplate", Replace = true },
            new EmbeddedResource{ FileName  = "edit-customer.js", ResourceLocation = "Content.scripts", OutputDirectory = "scripts", ResourceType = ResourceType.Script, Replace = true },
            new EmbeddedResource{ FileName  = "edit-customer-popups.js", ResourceLocation = "Content.scripts", OutputDirectory = "scripts", ResourceType = ResourceType.Script, Replace = true }
        };
    }

    public class ReconfigureCasinoPageEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "totalCodeCasinoPageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Casino Template", Alias = "totalCodeCasinoPageTemplate" , Replace = true}
        };
    }
    public class ReconfigureBettingHistoryPageEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "totalCodeBettingHistoryPageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Betting History Template", Alias = "totalCodeBettingHistoryPageTemplate" , Replace = true}
        };
    }
    public class PaymentSettingsEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.App_Plugins.PaymentSettings", OutputDirectory = "App_Plugins\\PaymentSettings", ResourceType = ResourceType.Directory }
        };
    }

    public class SvgViewerEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "package.manifest", ResourceLocation = "Content.App_Plugins.ApiSettingsSurface", OutputDirectory = "App_Plugins\\SvgCustomViewer", ResourceType = ResourceType.Other },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.App_Plugins.SvgCustomViewer", OutputDirectory = "App_Plugins\\SvgCustomViewer", ResourceType = ResourceType.Directory }
        };
    }

    public class GamesPagesEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "totalCodeGamePageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Game Page", Alias = "totalCodeGamePageTemplate", Replace = true }
        };
    }

    public class ErrorPageEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "GenericErrorTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Error Page Template", Alias = "GenericErrorTemplate", Replace = true }
        };
    }

    public class AffiliatePageEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "totalCodeAffiliatePageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Affiliate Page Template", Alias = "totalCodeAffiliatePageTemplate" , Replace = true}
        };
    }

    public class BonusHistoryPageEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName = "totalCodeBonusHistoryPageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Bonus History Page Template", Alias = "totalCodeBonusHistoryPageTemplate" , Replace = true },
            new EmbeddedResource{ FileName  = "bootstrap-3.4.1.min.js", ResourceLocation = "Content.scripts", OutputDirectory = "scripts", ResourceType = ResourceType.Script , Replace = true }
        };
    }

    public class StylesheetBulkUploadEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "package.manifest", ResourceLocation = "Content.App_Plugins.StylesheetBulkUploadSurface", OutputDirectory = "App_Plugins\\StylesheetBulkUploadSurface", ResourceType = ResourceType.Other },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.App_Plugins.StylesheetBulkUploadSurface.backoffice.StylesheetBulkUpload", OutputDirectory = "App_Plugins\\StylesheetBulkUploadSurface\\backoffice\\StylesheetBulkUpload", ResourceType = ResourceType.Directory },
        };
    }

    public class StylesheetBulkDownloadEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "package.manifest", ResourceLocation = "Content.App_Plugins.StylesheetBulkDownloadSurface", OutputDirectory = "App_Plugins\\StylesheetBulkDownloadSurface", ResourceType = ResourceType.Other },
            new EmbeddedResource{ FileName  = "*.*", ResourceLocation = "Content.App_Plugins.StylesheetBulkDownloadSurface.backoffice.StylesheetBulkDownload", OutputDirectory = "App_Plugins\\StylesheetBulkDownloadSurface\\backoffice\\StylesheetBulkDownload", ResourceType = ResourceType.Directory },
        };
    }

    public class GenericInfoPageEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "totalCodeGenericInfoPageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Generic Info Page Template", Alias = "totalCodeGenericInfoPageTemplate" , Replace = true}
        };
    }
    public class LoginPageEmbeddedResources : IEmbeddedResource
    {
        public List<EmbeddedResource> Resources => new List<EmbeddedResource>
        {
            new EmbeddedResource{ FileName  = "totalCodeLoginPageTemplate.cshtml", ResourceLocation = "Content.Views", OutputDirectory = "Views", ResourceType = ResourceType.Template, Name = "Total Code Login Page Template", Alias = "totalCodeLoginPageTemplate" , Replace = true}
        };
    }
}