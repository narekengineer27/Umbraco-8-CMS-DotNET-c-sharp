using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace TotalCode.Core.Models
{
    public class ArticleCategoryViewModel
    {
        public ArticleCategoryViewModel(IPublishedContent content)
        {
            Name = content.Name;
            Url = content.Url;
            TotalArticles = content.Children.Count();
            switch (content.Name)
            {
                case "Deposit & Withdraw":
                    Icon = "icon-money.svg";
                    break;
                case "Login Problems":
                    Icon = "login.svg";
                    break;
                default:
                    Icon = "link.svg";
                    break;
            }
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public int TotalArticles { get; set; }
        public string Icon { get; set; }
    }
}
