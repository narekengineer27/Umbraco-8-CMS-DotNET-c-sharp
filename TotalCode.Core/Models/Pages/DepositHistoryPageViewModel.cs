using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace TotalCode.Core.Models.Pages
{
    public class DepositHistoryPageViewModel : BasePageViewModel
    {
        public DepositHistoryPageViewModel(IPublishedContent content) : base(content)
        {
        }
    }
}
