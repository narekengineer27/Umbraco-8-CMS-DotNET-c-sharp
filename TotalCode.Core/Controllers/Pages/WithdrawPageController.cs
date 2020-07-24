using TotalCode.Core.Models.Pages;
using TotalCode.Core.Pages.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Plugins.Connector.Models;
using Umbraco.Core.Models.PublishedContent;
using TotalCode.Core.Models.NestedContents;
using Umbraco.Web;

namespace TotalCode.Core.Controllers.Pages
{
    public class TotalCodeWithdrawPageController : FinancialController
    {
        public ActionResult Index()
        {
            var model = GetModel<WithdrawPageViewModel>(CurrentPage);

            if (string.IsNullOrEmpty(model.Token))
            {
                return Redirect("/?login=y");
            }

            model.PredefinedAmounts = new string[] { };
            if (CurrentPage.HasValue("predefinedAmountsV2"))
            {
                var predefinedAmountsList = CurrentPage.Value<IEnumerable<IPublishedElement>>("predefinedAmountsV2")
                    .Select(x => new PredefinedAmount(x));

                var customerPredefinedAmount = predefinedAmountsList.FirstOrDefault(x => x.Currency == model.CustomerSummary.CurrencyCode);
                if (customerPredefinedAmount != null)
                {
                    model.PredefinedAmounts = customerPredefinedAmount.Amounts;
                }
            }

            var isConfirmed = Request.QueryString["isConfirmed"];

            model.ShowReceipt = isConfirmed != null && Convert.ToBoolean(isConfirmed);

            if (model.ShowReceipt)
            {
                model.Receipt = new ReceiptModel
                {
                    PaymentMethod = Umbraco.GetDictionaryValue("[Cards]CreditCard", "Credit Card"),
					PaymentSystemName = Session["t_name"]?.ToString(),
                    Name = model.CustomerSummary?.Username,
                    Amount = Session["t_amount"]?.ToString(),
                    Date = Session["t_date"]?.ToString(),
                    Status = isConfirmed == "true" ? Umbraco.GetDictionaryValue("[Cards]Accepted", "Accepted") : Umbraco.GetDictionaryValue("[Cards]Rejects", "Rejected"),
                    TransactionNumber = Session["t_id"]?.ToString()
                };
            }



            return CurrentTemplate(model);
        }
    }
}
