using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Plugins.Connector.Models;

namespace TotalCode.Core.Models.Pages
{
    public class WithdrawPageViewModel : TransactionPageViewModel
    {
        public WithdrawPageViewModel(IPublishedContent content) : base(content)
        {

        }
    }
}
