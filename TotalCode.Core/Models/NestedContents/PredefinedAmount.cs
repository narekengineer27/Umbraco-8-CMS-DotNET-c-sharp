using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace TotalCode.Core.Models.NestedContents
{
    public class PredefinedAmount
    {
        public PredefinedAmount(IPublishedElement content)
        {
            Currency = content.Value<string>("currency");
            Amounts = content.Value<string[]>("amounts");
        }

        public string Currency { get; set; }
        public string[] Amounts { get; set; }
    }
}
