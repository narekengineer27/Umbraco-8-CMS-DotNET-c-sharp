using TotalCode.Core.Models.Pages;
using TotalCode.Core.Pages.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Plugins.Connector.Models;

namespace TotalCode.Core.Controllers.Pages
{
    public class TotalCodeAffiliatePageController : BasePageController
    {
        public ActionResult Index()
        {
            var model = GetModel<AffiliatePageViewModel>(CurrentPage);
            model.Slider = CurrentPage.Value<IEnumerable<IPublishedElement>>("slider")
                        .Select(x => new SliderItem
                        {
                            Image = x.HasValue("sliderItemImage") ? x.GetProperty("sliderItemImage").Value<IPublishedContent>().Url : string.Empty,
                            ButtonLabel = x.HasValue("sliderItemButtonLabel") ? x.GetProperty("sliderItemButtonLabel").GetValue().ToString() : string.Empty,
                            Title = x.HasValue("sliderItemTitle") ? x.GetProperty("sliderItemTitle").GetValue().ToString() : string.Empty,
                            Subtitle = x.HasValue("sliderItemSubtitle") ? x.GetProperty("sliderItemSubtitle").GetValue().ToString() : string.Empty,
                            Url = x.HasValue("sliderItemUrl") ? x.GetProperty("sliderItemUrl").GetValue().ToString() : string.Empty,
                        })?.ToList();

            model.partners = new AffiliatePartner();
            model.partners.Heading = CurrentPage.Value<string>("partnerHeading");
            model.partners.SubHeading = CurrentPage.Value<string>("partnerSubHeading");
            model.partners.PartnerList = CurrentPage.Value<IEnumerable<IPublishedElement>>("partnerList")?.ToList();


            model.whatYouGet = new AffiliateWhatYouGet();
            model.whatYouGet.Heading = CurrentPage.Value<string>("whatYouGetHeading");
            model.whatYouGet.SubHeading = CurrentPage.Value<string>("whatYouGetSubHeading");
            model.whatYouGet.PlanText= CurrentPage.Value<string>("whatYouGetPlanText");
            model.whatYouGet.WhatYouGetList = CurrentPage.Value<IEnumerable<IPublishedElement>>("whatYouGet")?.ToList();

            model.promotions = new AffiliatePromotions();
            model.promotions.promotionHeading = CurrentPage.Value<string>("promotionHeading");
            model.promotions.promotionList = CurrentPage.Value<IEnumerable<IPublishedElement>>("promotions")?.ToList();

            model.testimonails = new AffiliateTestimonail();
            model.testimonails.Heading = CurrentPage.Value<string>("testimonialHeading");
            model.testimonails.testimonialList = CurrentPage.Value<IEnumerable<IPublishedElement>>("testimonialList")?.ToList();

            model.whatYourPlayersGet = new AffiliateWhatYourPlayersGet();
            model.whatYourPlayersGet.Heading = CurrentPage.Value<string>("whatYourPlayerGetHeading");
            model.whatYourPlayersGet.PlayerGetList = CurrentPage.Value<IEnumerable<IPublishedElement>>("whatYourPlayersGetList")?.ToList();

            model.FAQ = new AffiliateFAQ();
            model.FAQ.Heading = CurrentPage.Value<string>("fAQHeading");
            model.FAQ.FaqList = CurrentPage.Value<IEnumerable<IPublishedElement>>("fAQList")?.ToList();
            model.FAQ.Text = CurrentPage.Value<string>("fAQText");

            var url_absolute = new Uri(HttpContext.Request.Url.AbsoluteUri);
            var page = "affiliate";
            var url = string.Empty;
            var domain = url_absolute.Authority;
            if (!String.IsNullOrEmpty(model.Subdomain))
            {
                int count = domain.Count(f => f == '.');
                if (count == 1)
                {
                    url = "https://" + page + "." + domain;
                }
                if (count == 2)
                {
                    string output = domain.Substring(domain.IndexOf('.') + 1);
                    url = "https://" + page + "-" + model.Subdomain + "." + output;
                }
            }
            else
            {
                url = "https://" + page + "." + domain;
            }
            model.JoinNowLink = string.IsNullOrEmpty(url) ? "#" : url;

            //var tt = CurrentPage.Value("slider");
            return CurrentTemplate(model);
        }
    }
}