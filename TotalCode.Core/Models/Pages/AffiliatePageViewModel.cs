namespace TotalCode.Core.Models.Pages
{
    using System.Linq;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Plugins.Connector.Models;
    using Umbraco.Plugins.Connector.Content;
    using System.Collections.Generic;

    public class AffiliatePageViewModel : BasePageViewModel
    {
        public string Url { get; set; }
        public AffiliatePartner partners { get; set; }
        public AffiliateWhatYouGet whatYouGet { get; set; }
        public AffiliatePromotions promotions { get; set; }
        public AffiliateTestimonail testimonails { get; set; }
        public AffiliateWhatYourPlayersGet whatYourPlayersGet { get; set; }
        public AffiliateFAQ FAQ { get; set; }

        public string JoinNowLink { get; set; }
        public AffiliatePageViewModel(IPublishedContent content) : base(content)
        {
            Url = content.Url;

        }
    }

    public class AffiliatePartner
    {
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public List<IPublishedElement> PartnerList { get; set; }
    }

    public class AffiliateWhatYouGet
    {
        public string Heading { get; set; }
        public string SubHeading { get; set; }
        public List<IPublishedElement> WhatYouGetList { get; set; }
        public string PlanText { get; set; }
    }

    public class AffiliatePromotions
    {
        public string promotionHeading { get; set; }
        public List<IPublishedElement> promotionList { get; set; }
    }

    public class AffiliateTestimonail
    {
        public string Heading { get; set; }
        public List<IPublishedElement> testimonialList { get; set; }
    }

    public class AffiliateWhatYourPlayersGet
    {
        public string Heading { get; set; }
        public List<IPublishedElement> PlayerGetList { get; set; }
    }
    public class AffiliateFAQ
    {
        public string Heading { get; set; }
        public List<IPublishedElement> FaqList { get; set; }
        public string Text { get; set; }
    }
}