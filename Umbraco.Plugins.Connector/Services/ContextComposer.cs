namespace Umbraco.Plugins.Connector.Services
{
    using Umbraco.Core;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Scoping;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Web;

    public class ContextComponentComposer : ComponentComposer<ContextComponent>
    {
        public override void Compose(Composition composition)
        {
            composition.Components().Append<ContextComponent>();
            base.Compose(composition);
        }
    }

    public class ContextComponent : IComponent
    {
        public ContextComponent(IUmbracoContextFactory context, IScopeProvider scopeProvider, IContentService contentService, IContentTypeService contentTypeService, IDataTypeService dataTypeService, IFileService fileService, IMediaTypeService mediaTypeService, IMediaService mediaService, IUserService userService, IDomainService domainService, IPublicAccessService publicAccessService, IAuditService auditService, ILocalizationService localizationService, ILocalizedTextService localizedTextService, ITagService tagService, IMemberService memberService, IUmbracoContextFactory contextFactory, ILogger logger)
        {
            ConnectorContext.ScopeProvider = scopeProvider;
            ConnectorContext.ContentService = contentService;
            ConnectorContext.ContentTypeService = contentTypeService;
            ConnectorContext.DataTypeService = dataTypeService;
            ConnectorContext.FileService = fileService;
            ConnectorContext.MediaTypeService = mediaTypeService;
            ConnectorContext.MediaService = mediaService;
            ConnectorContext.UserService = userService;
            ConnectorContext.DomainService = domainService;
            ConnectorContext.PublicAccessService = publicAccessService;
            ConnectorContext.AuditService = auditService;
            ConnectorContext.LocalizationService = localizationService;
            ConnectorContext.LocalizedTextService = localizedTextService;
            ConnectorContext.TagService = tagService;
            ConnectorContext.MemberService = memberService;
            ConnectorContext.UmbracoContext = context.EnsureUmbracoContext().UmbracoContext;
            ConnectorContext.ContextFactory = contextFactory;
            ConnectorContext.Logger = logger;
        }

        public void Initialize() {
            ConfigurationService helper = new ConfigurationService();
            helper.AddJsonRpcHandler();
            helper.AddApiSettings();
            var settings = helper.GetApiSettings();
            settings.LoadConfigurationsIntoMemory();
        }

        public void Terminate() { }
    }
}
