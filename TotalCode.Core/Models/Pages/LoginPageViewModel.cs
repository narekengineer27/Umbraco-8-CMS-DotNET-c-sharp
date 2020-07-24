using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace TotalCode.Core.Models.Pages
{
    public class LoginPageViewModel : BasePageViewModel
    {
        public string LoginPageTitle { get; set; }
        public LoginPageViewModel(IPublishedContent content) : base(content)
        {
        }
    }
}