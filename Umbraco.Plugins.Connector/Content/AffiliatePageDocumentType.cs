
namespace Umbraco.Plugins.Connector.Content
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Composing;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Core.Services;
    using Umbraco.Plugins.Connector.Dictionaries;
    using Umbraco.Plugins.Connector.Helpers;
    using Umbraco.Plugins.Connector.Services;
    using Umbraco.Web.PropertyEditors;

    public class _34_AffiliatePageDocumentType : IComponent
    {
        private readonly ILocalizationService localizationService;
        private readonly IDomainService domainService;
        private readonly IFileService fileService;
        private readonly ILogger logger;
        private readonly IContentTypeService contentTypeService;
        private readonly IDataTypeService dataTypeService;

        public static string
                        DATA_TYPE_CONTAINER = "Total Code Data Types",
                        DOCUMENT_TYPE_ALIAS = "totalCodeAffiliatePage",
                        DOCUMENT_TYPE_NAME = "Affiliate Page",
                        DOCUMENT_TYPE_CONTAINER = "Total Code Container",
                        PARENT_TEMPLATE_ALIAS = "totalCodeLayout",
                        PARENT_NODE_DOCUMENT_TYPE_ALIAS = "totalCodeHomePage",
                        NESTED_DOCUMENT_TYPE_PARENT_ALIAS = "totalCodeBasePage",
                        TEMPLATE_ALIAS = "totalCodeAffiliatePageTemplate",
                        TEMPLATE_NAME = "Total Code Affiliate Page Template",
                        NESTED_FAQ_DOCUMENT_TYPE_ALIAS = "fAQAffiliate",
                        NESTED_FAQ_DOCUMENT_TYPE_NAME = "FAQ Affiliate",
                        NESTED_PARTNER_DOCUMENT_TYPE_ALIAS = "partnerAffiliate",
                        NESTED_PARTNER_DOCUMENT_TYPE_NAME = "Affiliate Partner",
                        NESTED_PARTNER_NUMBER_DOCUMENT_TYPE_ALIAS = "partnerNumbersAffiliate",
                        NESTED_PARTNER_NUMBER_DOCUMENT_TYPE_NAME = "Affiliate Partner Numbers",
                        NESTED_PROMOTIONS_NUMBER_DOCUMENT_TYPE_ALIAS = "promotionsAffiliate",
                        NESTED_PROMOTIONS_NUMBER_DOCUMENT_TYPE_NAME = "Affiliate Promotions",
                        NESTED_TESTIMONIALS_DOCUMENT_TYPE_ALIAS = "testimonialsAffiliate",
                        NESTED_TESTIMONIALS_DOCUMENT_TYPE_NAME = "Affiliate Testimonials",
                        NESTED_WHAT_PLAYER_GETS_DOCUMENT_TYPE_ALIAS = "whatPlayerGetsAffiliate",
                        NESTED_WHAT_PLAYER_GETS_DOCUMENT_TYPE_NAME = "Affiliate What Player Gets",
                        NESTED_WHAT_YOU_GET_DOCUMENT_TYPE_ALIAS = "whatYouGetAffiliate",
                        NESTED_WHAT_YOU_GET_DOCUMENT_TYPE_NAME = "Affiliate What You Get",




                        NESTED_DOCUMENT_TYPE_ICON = "icon-document",
                        NESTED_TAB_NAME = "Content";

        public _34_AffiliatePageDocumentType(ILocalizationService localizationService, IDomainService domainService, IFileService fileService, ILogger logger, IContentTypeService contentTypeService, IDataTypeService dataTypeService)
        {
            this.localizationService = localizationService;
            this.domainService = domainService;
            this.logger = logger;
            this.contentTypeService = contentTypeService;
            this.fileService = fileService;
            this.dataTypeService = dataTypeService;
        }

        private void Reconfigure()
        {
            try
            {
                #region Nested Document Type
                var container = contentTypeService.GetContainers(DOCUMENT_TYPE_CONTAINER, 1).FirstOrDefault();
                int containerId = -1;

                if (container != null)
                    containerId = container.Id;

                const string PartnerpropertyName = "Affiliate Partners Numbers";
                
                #region FAQ
                var contentType = contentTypeService.Get(NESTED_FAQ_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_FAQ_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_FAQ_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(NESTED_TAB_NAME);

                    #region Nested Document Type Properties
                    PropertyType QuestionPropType = new PropertyType(dataTypeService.GetDataType(-88), "question")
                    {
                        Name = "Question",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(QuestionPropType, NESTED_TAB_NAME);

                    PropertyType AnswerPropType = new PropertyType(dataTypeService.GetDataType(-87), "answer")
                    {
                        Name = "Answer",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(AnswerPropType, NESTED_TAB_NAME);

                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_FAQ_DOCUMENT_TYPE_ALIAS}' has been created");
                }
                #endregion

                #region Partner Number
                contentType = contentTypeService.Get(NESTED_PARTNER_NUMBER_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_PARTNER_NUMBER_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_PARTNER_NUMBER_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(NESTED_TAB_NAME);

                    #region Nested Document Type Properties
                    PropertyType NumberPropType = new PropertyType(dataTypeService.GetDataType(-88), "number")
                    {
                        Name = "Number",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(NumberPropType, NESTED_TAB_NAME);

                    PropertyType ItemNamePropType = new PropertyType(dataTypeService.GetDataType(-88), "itemName")
                    {
                        Name = "Item Name",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(ItemNamePropType, NESTED_TAB_NAME);

                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_PARTNER_NUMBER_DOCUMENT_TYPE_ALIAS}' has been created");


                    var dataTypeContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                    var dataTypeContainerId = -1;

                    if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                    var exists = dataTypeService.GetDataType(PartnerpropertyName) != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                        if (editor != null)
                        {
                            DataType bannerSliderNestedDataType = new DataType(editor, dataTypeContainerId)
                            {
                                Name = PartnerpropertyName,
                                ParentId = dataTypeContainerId,
                                Configuration = new NestedContentConfiguration
                                {
                                    MinItems = 0,
                                    MaxItems = 3,
                                    ConfirmDeletes = true,
                                    HideLabel = false,
                                    ShowIcons = true,
                                    ContentTypes = new[]
                                    {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_PARTNER_NUMBER_DOCUMENT_TYPE_ALIAS, TabAlias = NESTED_TAB_NAME }
                                }
                                }
                            };
                            dataTypeService.Save(bannerSliderNestedDataType);
                        }
                    }
                }
                #endregion

                #region Partner
                contentType = contentTypeService.Get(NESTED_PARTNER_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_PARTNER_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_PARTNER_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(NESTED_TAB_NAME);

                    #region Nested Document Type Properties
                    PropertyType MainImagePropType = new PropertyType(dataTypeService.GetDataType(1048), "mainImage")
                    {
                        Name = "Image",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(MainImagePropType, NESTED_TAB_NAME);

                    PropertyType TitlePropType = new PropertyType(dataTypeService.GetDataType(-88), "title")
                    {
                        Name = "Title",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(TitlePropType, NESTED_TAB_NAME);

                    PropertyType subTitle = new PropertyType(dataTypeService.GetDataType(-88), "subTitle")
                    {
                        Name = "Sub Title",
                        Description = "",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(TitlePropType, NESTED_TAB_NAME);

                    var partnerNumberNestedDataType = dataTypeService.GetDataType(PartnerpropertyName);
                    PropertyType PartnersNumbersPropType = new PropertyType(partnerNumberNestedDataType, "partnersNumbers")
                    {
                        Name = "Partners Numbers",
                        Description = "",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(PartnersNumbersPropType, NESTED_TAB_NAME);


                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_PARTNER_DOCUMENT_TYPE_ALIAS}' has been created");
                }

                #endregion

                #region Promotions
                contentType = contentTypeService.Get(NESTED_PROMOTIONS_NUMBER_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_PROMOTIONS_NUMBER_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_PROMOTIONS_NUMBER_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(NESTED_TAB_NAME);

                    #region Nested Document Type Properties
                    PropertyType MainImagePropType = new PropertyType(dataTypeService.GetDataType(1048), "mainImage")
                    {
                        Name = "Image",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(MainImagePropType, NESTED_TAB_NAME);

                    PropertyType ImageText1PropType = new PropertyType(dataTypeService.GetDataType(-88), "imageText1")
                    {
                        Name = "Image Text 1",
                        Description = "",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(ImageText1PropType, NESTED_TAB_NAME);

                    PropertyType ImageText2PropType = new PropertyType(dataTypeService.GetDataType(-88), "imageText2")
                    {
                        Name = "Image Text 2",
                        Description = "",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(ImageText2PropType, NESTED_TAB_NAME);

                    PropertyType ImageText3PropType = new PropertyType(dataTypeService.GetDataType(-88), "imageText3")
                    {
                        Name = "Image Text 3",
                        Description = "",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(ImageText3PropType, NESTED_TAB_NAME);

                    PropertyType PromotionText1PropType = new PropertyType(dataTypeService.GetDataType(-88), "promotionText1")
                    {
                        Name = "Promotion Text 1",
                        Description = "",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(PromotionText1PropType, NESTED_TAB_NAME);

                    PropertyType PromotionText2PropType = new PropertyType(dataTypeService.GetDataType(-88), "promotionText2")
                    {
                        Name = "Promotion Text 2",
                        Description = "",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(PromotionText2PropType, NESTED_TAB_NAME);



                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_PROMOTIONS_NUMBER_DOCUMENT_TYPE_ALIAS}' has been created");
                }

                #endregion

                #region Testimonials
                contentType = contentTypeService.Get(NESTED_TESTIMONIALS_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_TESTIMONIALS_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_TESTIMONIALS_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(NESTED_TAB_NAME);

                    #region Nested Document Type Properties
                    PropertyType TextPropType = new PropertyType(dataTypeService.GetDataType(-88), "text")
                    {
                        Name = "Text",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(TextPropType, NESTED_TAB_NAME);

                    PropertyType LinkTextPropType = new PropertyType(dataTypeService.GetDataType(-88), "linkText")
                    {
                        Name = "Link Text",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(LinkTextPropType, NESTED_TAB_NAME);

                    PropertyType LinkUrlPropType = new PropertyType(dataTypeService.GetDataType(-88), "linkUrl")
                    {
                        Name = "Link Url",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        ValidationRegExp= "https?://[a-zA-Z0-9-.]+.[a-zA-Z]{2,}"
                    };
                    docType.AddPropertyType(LinkUrlPropType, NESTED_TAB_NAME);

                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_TESTIMONIALS_DOCUMENT_TYPE_ALIAS}' has been created");
                }

                #endregion

                #region What Player Gets Affiliate
                contentType = contentTypeService.Get(NESTED_WHAT_PLAYER_GETS_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_WHAT_PLAYER_GETS_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_WHAT_PLAYER_GETS_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(NESTED_TAB_NAME);

                    #region Nested Document Type Properties
                    PropertyType NumberPropType = new PropertyType(dataTypeService.GetDataType(-88), "number")
                    {
                        Name = "Number",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(NumberPropType, NESTED_TAB_NAME);

                    PropertyType TitlePropType = new PropertyType(dataTypeService.GetDataType(-88), "title")
                    {
                        Name = "Title",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(TitlePropType, NESTED_TAB_NAME);

                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_WHAT_PLAYER_GETS_DOCUMENT_TYPE_ALIAS}' has been created");
                }

                #endregion

                #region What You Get Affiliate
                contentType = contentTypeService.Get(NESTED_WHAT_YOU_GET_DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = NESTED_WHAT_YOU_GET_DOCUMENT_TYPE_NAME,
                        Alias = NESTED_WHAT_YOU_GET_DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        IsElement = true,
                        SortOrder = 0,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        Variations = ContentVariation.Culture
                    };

                    docType.AddPropertyGroup(NESTED_TAB_NAME);

                    #region Nested Document Type Properties
                    PropertyType MainImagePropType = new PropertyType(dataTypeService.GetDataType(1048), "mainImage")
                    {
                        Name = "Main Image",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(MainImagePropType, NESTED_TAB_NAME);

                    PropertyType TitlePropType = new PropertyType(dataTypeService.GetDataType(-88), "title")
                    {
                        Name = "Title",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(TitlePropType, NESTED_TAB_NAME);


                    PropertyType SubTitlePropType = new PropertyType(dataTypeService.GetDataType(-88), "subTitle")
                    {
                        Name = "Sub Title",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(SubTitlePropType, NESTED_TAB_NAME);


                    PropertyType StakePropType = new PropertyType(dataTypeService.GetDataType(-88), "stake")
                    {
                        Name = "Stake",
                        Description = "",
                        Variations = ContentVariation.Culture,
                        Mandatory = true
                    };
                    docType.AddPropertyType(StakePropType, NESTED_TAB_NAME);



                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{NESTED_WHAT_YOU_GET_DOCUMENT_TYPE_ALIAS}' has been created");
                }

                #endregion

                #endregion

                contentType = contentTypeService.Get(DOCUMENT_TYPE_ALIAS);
                if (contentType == null)
                {
                    const string SLIDER_TAB = "SLIDER",
                        PARTNERS_TAB = "Partners",
                        WHAT_YOU_GET_TAB = "What you get",
                        PROMOTIONS_TAB = "Promotions",
                        TESTIMONIALS_TAB = "Testimonials",
                        WHAT_YOUR_PLAYERS_GET_TAB = "What Your Players Get",
                        FAQS_TAB = "FAQs";

                    ContentType docType = (ContentType)contentType ?? new ContentType(containerId)
                    {
                        Name = DOCUMENT_TYPE_NAME,
                        Alias = DOCUMENT_TYPE_ALIAS,
                        AllowedAsRoot = false,
                        Description = "",
                        Icon = NESTED_DOCUMENT_TYPE_ICON,
                        ParentId = contentTypeService.Get(NESTED_DOCUMENT_TYPE_PARENT_ALIAS).Id,
                        SortOrder = 0,
                        Variations = ContentVariation.Culture,
                    };

                    // Create the Template if it doesn't exist
                    if (fileService.GetTemplate(TEMPLATE_ALIAS) == null)
                    {
                        //then create the template
                        Template newTemplate = new Template(TEMPLATE_NAME, TEMPLATE_ALIAS);
                        ITemplate masterTemplate = fileService.GetTemplate(PARENT_TEMPLATE_ALIAS);
                        newTemplate.SetMasterTemplate(masterTemplate);
                        fileService.SaveTemplate(newTemplate);
                    }

                    // Set templates for document type
                    var template = fileService.GetTemplate(TEMPLATE_ALIAS);
                    docType.AllowedTemplates = new List<ITemplate> { template };
                    docType.SetDefaultTemplate(template);

                    docType.AddPropertyGroup(SLIDER_TAB);
                    docType.AddPropertyGroup(PARTNERS_TAB);
                    docType.AddPropertyGroup(WHAT_YOU_GET_TAB);
                    docType.AddPropertyGroup(PROMOTIONS_TAB);
                    docType.AddPropertyGroup(TESTIMONIALS_TAB);
                    docType.AddPropertyGroup(WHAT_YOUR_PLAYERS_GET_TAB);
                    docType.AddPropertyGroup(FAQS_TAB);

                    #region Slider Tab Content

                    var BannerSliderDataType = dataTypeService.GetDataType("Banner Slider");

                    PropertyType SliderPropType = new PropertyType(BannerSliderDataType, "slider")
                    {
                        Name = "Slider",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(SliderPropType, SLIDER_TAB);

                    #endregion

                    #region Partners

                    PropertyType PartnerHeadingPropType = new PropertyType(dataTypeService.GetDataType(-88), "partnerHeading")
                    {
                        Name = "Heading",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(PartnerHeadingPropType, PARTNERS_TAB);

                    PropertyType PartnerSubHeadingPropType = new PropertyType(dataTypeService.GetDataType(-88), "partnerSubHeading")
                    {
                        Name = "Sub Heading",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(PartnerSubHeadingPropType, PARTNERS_TAB);

                    #region Data Type Partner Affiliate
                    var dataTypeContainer = dataTypeService.GetContainers(DATA_TYPE_CONTAINER, 1).FirstOrDefault();
                    var dataTypeContainerId = -1;

                    if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                    var exists = dataTypeService.GetDataType("Affiliate Partners") != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                        if (editor != null)
                        {
                            DataType bannerSliderNestedDataType = new DataType(editor, dataTypeContainerId)
                            {
                                Name = "Affiliate Partners",
                                ParentId = dataTypeContainerId,
                                Configuration = new NestedContentConfiguration
                                {
                                    MinItems = 0,
                                    MaxItems = 999,
                                    ConfirmDeletes = true,
                                    HideLabel = false,
                                    ShowIcons = true,
                                    ContentTypes = new[]
                                    {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_PARTNER_DOCUMENT_TYPE_ALIAS, TabAlias = NESTED_TAB_NAME }
                                }
                                }
                            };
                            dataTypeService.Save(bannerSliderNestedDataType);
                        }
                    }
                    #endregion

                    var PartnerListDataType = dataTypeService.GetDataType("Affiliate Partners");
                    PropertyType partnerListPropType = new PropertyType(PartnerListDataType, "partnerList")
                    {
                        Name = "List",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(partnerListPropType, PARTNERS_TAB);

                    #endregion

                    #region What you get

                    PropertyType WhatYouGetHeadingPropType = new PropertyType(dataTypeService.GetDataType(-88), "whatYouGetHeading")
                    {
                        Name = "Heading",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(WhatYouGetHeadingPropType, WHAT_YOU_GET_TAB);

                    PropertyType WhatYouGetSubHeadingPropType = new PropertyType(dataTypeService.GetDataType(-88), "whatYouGetSubHeading")
                    {
                        Name = "Sub Heading",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(WhatYouGetSubHeadingPropType, WHAT_YOU_GET_TAB);


                    #region Data Type What You Get

                    if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                    exists = dataTypeService.GetDataType("Affiliate What you get") != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                        if (editor != null)
                        {
                            DataType NestedDataType = new DataType(editor, dataTypeContainerId)
                            {
                                Name = "Affiliate What you get",
                                ParentId = dataTypeContainerId,
                                Configuration = new NestedContentConfiguration
                                {
                                    MinItems = 0,
                                    MaxItems = 999,
                                    ConfirmDeletes = true,
                                    HideLabel = false,
                                    ShowIcons = true,
                                    ContentTypes = new[]
                                    {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_WHAT_YOU_GET_DOCUMENT_TYPE_ALIAS, TabAlias = NESTED_TAB_NAME }
                                }
                                }
                            };
                            dataTypeService.Save(NestedDataType);
                        }
                    }
                    #endregion
                    var whatYouGetDataType = dataTypeService.GetDataType("Affiliate What you get");
                    PropertyType WhatYouGetDataType = new PropertyType(whatYouGetDataType, "whatYouGet")
                    {
                        Name = "What you get",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(WhatYouGetDataType, WHAT_YOU_GET_TAB);

                    PropertyType whatYouGetPlanTextPropType = new PropertyType(dataTypeService.GetDataType(-87), "whatYouGetPlanText")
                    {
                        Name = "What You Get Plan Text",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(whatYouGetPlanTextPropType, WHAT_YOU_GET_TAB);


                    #endregion

                    #region Promotions

                    PropertyType PromotionHeadingPropType = new PropertyType(dataTypeService.GetDataType(-88), "promotionHeading")
                    {
                        Name = "Heading",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(PromotionHeadingPropType, PROMOTIONS_TAB);

                    #region Data Type Promotions

                    if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                    exists = dataTypeService.GetDataType("Affiliate Promotions") != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                        if (editor != null)
                        {
                            DataType NestedDataType = new DataType(editor, dataTypeContainerId)
                            {
                                Name = "Affiliate Promotions",
                                ParentId = dataTypeContainerId,
                                Configuration = new NestedContentConfiguration
                                {
                                    MinItems = 0,
                                    MaxItems = 999,
                                    ConfirmDeletes = true,
                                    HideLabel = false,
                                    ShowIcons = true,
                                    ContentTypes = new[]
                                    {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_PROMOTIONS_NUMBER_DOCUMENT_TYPE_ALIAS, TabAlias = NESTED_TAB_NAME }
                                }
                                }
                            };
                            dataTypeService.Save(NestedDataType);
                        }
                    }
                    #endregion
                    var PromotionsDataType = dataTypeService.GetDataType("Affiliate Promotions");
                    PropertyType PromotionDataType = new PropertyType(PromotionsDataType, "promotions")
                    {
                        Name = "Promotions",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(PromotionDataType, PROMOTIONS_TAB);



                    #endregion

                    #region Testimonials

                    PropertyType TestimonialsHeadingPropType = new PropertyType(dataTypeService.GetDataType(-88), "testimonialHeading")
                    {
                        Name = "Heading",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(TestimonialsHeadingPropType, TESTIMONIALS_TAB);

                    #region Data Type Testimonials

                    if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                    exists = dataTypeService.GetDataType("Affiliate Testimonials") != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                        if (editor != null)
                        {
                            DataType NestedDataType = new DataType(editor, dataTypeContainerId)
                            {
                                Name = "Affiliate Testimonials",
                                ParentId = dataTypeContainerId,
                                Configuration = new NestedContentConfiguration
                                {
                                    MinItems = 0,
                                    MaxItems = 999,
                                    ConfirmDeletes = true,
                                    HideLabel = false,
                                    ShowIcons = true,
                                    ContentTypes = new[]
                                    {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_TESTIMONIALS_DOCUMENT_TYPE_ALIAS, TabAlias = NESTED_TAB_NAME }
                                }
                                }
                            };
                            dataTypeService.Save(NestedDataType);
                        }
                    }
                    #endregion
                    var TestimonialsDataType = dataTypeService.GetDataType("Affiliate Testimonials");
                    PropertyType TestimonialDataType = new PropertyType(TestimonialsDataType, "testimonialList")
                    {
                        Name = "Testimonial List",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(TestimonialDataType, TESTIMONIALS_TAB);



                    #endregion


                    #region What Your Players Get

                    PropertyType WhatYourPlayerGetHeadingPropType = new PropertyType(dataTypeService.GetDataType(-88), "whatYourPlayerGetHeading")
                    {
                        Name = "Heading",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(WhatYourPlayerGetHeadingPropType, WHAT_YOUR_PLAYERS_GET_TAB);

                    #region Data Type What Your Players Get

                    if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                    exists = dataTypeService.GetDataType("Affiliate What Players Get") != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                        if (editor != null)
                        {
                            DataType NestedDataType = new DataType(editor, dataTypeContainerId)
                            {
                                Name = "Affiliate What Players Get",
                                ParentId = dataTypeContainerId,
                                Configuration = new NestedContentConfiguration
                                {
                                    MinItems = 0,
                                    MaxItems = 999,
                                    ConfirmDeletes = true,
                                    HideLabel = false,
                                    ShowIcons = true,
                                    ContentTypes = new[]
                                    {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_WHAT_PLAYER_GETS_DOCUMENT_TYPE_ALIAS, TabAlias = NESTED_TAB_NAME }
                                }
                                }
                            };
                            dataTypeService.Save(NestedDataType);
                        }
                    }
                    #endregion
                    var WhatPlayersGetDataType = dataTypeService.GetDataType("Affiliate What Players Get");
                    PropertyType WhatYourPlayersGetListDataType = new PropertyType(WhatPlayersGetDataType, "whatYourPlayersGetList")
                    {
                        Name = "Players Get List",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(WhatYourPlayersGetListDataType, WHAT_YOUR_PLAYERS_GET_TAB);

                    #endregion

                    #region FAQs

                    PropertyType fAQHeadingPropType = new PropertyType(dataTypeService.GetDataType(-88), "fAQHeading")
                    {
                        Name = "Heading",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(fAQHeadingPropType, FAQS_TAB);

                    #region Data Type FAQ

                    if (dataTypeContainer != null) dataTypeContainerId = dataTypeContainer.Id;

                    exists = dataTypeService.GetDataType("Affiliate FAQ Affiliate") != null;
                    if (!exists)
                    {
                        var created = Web.Composing.Current.PropertyEditors.TryGet("Umbraco.NestedContent", out IDataEditor editor);
                        if (editor != null)
                        {
                            DataType NestedDataType = new DataType(editor, dataTypeContainerId)
                            {
                                Name = "Affiliate FAQ Affiliate",
                                ParentId = dataTypeContainerId,
                                Configuration = new NestedContentConfiguration
                                {
                                    MinItems = 0,
                                    MaxItems = 999,
                                    ConfirmDeletes = true,
                                    HideLabel = false,
                                    ShowIcons = true,
                                    ContentTypes = new[]
                                    {
                                    new NestedContentConfiguration.ContentType { Alias = NESTED_FAQ_DOCUMENT_TYPE_ALIAS, TabAlias = NESTED_TAB_NAME }
                                }
                                }
                            };
                            dataTypeService.Save(NestedDataType);
                        }
                    }
                    #endregion
                    var FAQGetDataType = dataTypeService.GetDataType("Affiliate FAQ Affiliate");
                    PropertyType fAQListDataType = new PropertyType(FAQGetDataType, "fAQList")
                    {
                        Name = "FAQ List",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(fAQListDataType, FAQS_TAB);


                    PropertyType fAQTextPropType = new PropertyType(dataTypeService.GetDataType(-87), "fAQText")
                    {
                        Name = "FAQ Text",
                        Variations = ContentVariation.Culture
                    };
                    docType.AddPropertyType(fAQTextPropType, FAQS_TAB);

                    #endregion

                    contentTypeService.Save(docType);
                    ConnectorContext.AuditService.Add(AuditType.New, -1, docType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_NAME}' has been created");

                    ContentHelper.CopyPhysicalAssets(new AffiliatePageEmbeddedResources());

                    var parentDocType = contentTypeService.Get(PARENT_NODE_DOCUMENT_TYPE_ALIAS);
                    if (parentDocType.AllowedContentTypes.SingleOrDefault(x => x.Alias.Equals(DOCUMENT_TYPE_ALIAS)) == null)
                    {
                        // set as allowed content type in account home
                        ContentHelper.AddAllowedDocumentType(contentTypeService, PARENT_NODE_DOCUMENT_TYPE_ALIAS, DOCUMENT_TYPE_ALIAS);

                        ConnectorContext.AuditService.Add(AuditType.Move, -1, contentType.Id, "Document Type", $"Document Type '{DOCUMENT_TYPE_ALIAS}' has been updated");
                    }

                }
                else
                {
                    var partnerGroup = contentType.PropertyGroups.FirstOrDefault(x => x.Name == "Partners");

                    if (partnerGroup != null)
                    {
                        partnerGroup.Name = "Why partner with us";

                        contentType.PropertyGroups.Add(partnerGroup);
                        contentTypeService.Save(contentType);
                    }
                    var contentTypePartnerAffiliate = contentTypeService.Get(NESTED_PARTNER_DOCUMENT_TYPE_ALIAS);

                    if (contentTypePartnerAffiliate != null)
                    {
                        var partnersNumbers = contentTypePartnerAffiliate.PropertyTypes.ToList().SingleOrDefault(x => x.Alias == "partnersNumbers");
                        if (partnersNumbers != null)
                        {
                            if (partnersNumbers.Name == "Partners Numbers")
                            {
                                partnersNumbers.Name = "Components";
                                contentTypePartnerAffiliate.AddPropertyType(partnersNumbers);

                                contentTypeService.Save(contentTypePartnerAffiliate);
                            }
                        }
                    }

                }

                var language = new LanguageDictionaryService(ConnectorContext.LocalizationService, ConnectorContext.DomainService, ConnectorContext.Logger);
                var dictionaryItems = new List<Type>();

                // Check if parent Key exists, and skip if true
                if (!language.CheckExists(typeof(Common_ParentKey)))
                    dictionaryItems.Add(typeof(Common_ParentKey));

                if (!language.CheckExists(typeof(Common_JoinNow)))
                    dictionaryItems.Add(typeof(Common_JoinNow));

                if (dictionaryItems.Count > 0)
                {
                    language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items

                    ConnectorContext.AuditService.Add(AuditType.Save, -1, -1, "Dictionary Item", $"Common Dictionary Items have been created/updated");
                }
                //if (createDictionaryItems)
                //{
                //    var language = new LanguageDictionaryService(localizationService, domainService, logger);
                //    // Check if parent Key exists, and skip if true
                //    if (!language.CheckExists(typeof(Pages_ParentKey)))
                //    {
                //        // Add Dictionary Items
                //        var dictionaryItems = new List<Type>
                //    {
                //        typeof(Pages_ParentKey),
                //        typeof(Pages_SportsPage),
                //        typeof(Pages_SportEvents),
                //        typeof(Pages_SportEventsEventName),
                //        typeof(Pages_SportEventsEventScheduleTime),
                //        typeof(Pages_SportEventsEventStatusDescription),
                //        typeof(Pages_SportEventsEventTournament),
                //        typeof(Pages_SportEventsEventCategory),
                //        typeof(Pages_SportEventsEventCategorySport),
                //        typeof(Pages_SportEventsNoEvents)
                //    };
                //        language.CreateDictionaryItems(dictionaryItems); // Create Dictionary Items
                //    }
                //}
            }

            catch (Exception ex)
            {
                logger.Error(typeof(_34_AffiliatePageDocumentType), ex.Message);
                logger.Error(typeof(_34_AffiliatePageDocumentType), ex.StackTrace);
            }
        }

        public void Initialize()
        {
            Reconfigure();
        }

        public void Terminate()
        {
        }
    }
}