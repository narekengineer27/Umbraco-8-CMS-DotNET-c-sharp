using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace TotalCode.Core.Models.Pages
{
    public class WithdrawHistoryPageViewModel : BasePageViewModel
    {
        public WithdrawHistoryPageViewModel(IPublishedContent content) : base(content)
        {
        }
    }
}
