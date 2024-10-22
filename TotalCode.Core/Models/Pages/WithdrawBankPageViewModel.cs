﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Plugins.Connector.Models;

namespace TotalCode.Core.Models.Pages
{
    public class WithdrawBankPageViewModel : BasePageViewModel
    {
        public WithdrawBankPageViewModel(IPublishedContent content) : base(content)
        {
            
        }

        public IEnumerable<Card> Cards { get; set; }
    }
}
