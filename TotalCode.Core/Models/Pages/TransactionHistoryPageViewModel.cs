using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace TotalCode.Core.Models.Pages
{
    public class TransactionHistoryPageViewModel : BasePageViewModel
    {
        public TransactionHistoryPageViewModel(IPublishedContent content) : base(content)
        {
        }
    }
}
