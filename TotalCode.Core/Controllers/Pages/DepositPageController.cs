using TotalCode.Core.Models.Pages;
using TotalCode.Core.Pages.Controllers;
using Umbraco.Plugins.Connector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web;
using Umbraco.Core.Models.PublishedContent;
using TotalCode.Core.Models.NestedContents;

namespace TotalCode.Core.Controllers.Pages
{
    public class TotalCodeDepositPageController : FinancialController
    {
        public ActionResult Index()
        {
            var model = GetModel<DepositPageViewModel>(CurrentPage);

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

            var isConfirmed = Request.QueryString["isConfirmed"] != null && Convert.ToBoolean(Request.QueryString["isConfirmed"]);

            model.ShowReceipt = isConfirmed;

            if (model.ShowReceipt)
            {
                decimal gift;
                decimal bonus;
                model.Receipt = new ReceiptModel
                {
                    PaymentMethod = Umbraco.GetDictionaryValue("[Cards]CreditCard", "Credit Card"),
					PaymentSystemName = Session["t_name"]?.ToString(),
                    Name = model.CustomerSummary?.Username,
                    Amount = Session["t_amount"]?.ToString(),
                    Date = Session["t_date"]?.ToString(),
                    Status = isConfirmed ? Umbraco.GetDictionaryValue("[Cards]Accepted", "Accepted") : Umbraco.GetDictionaryValue("[Cards]Rejects", "Rejected"),
                    TransactionNumber = Session["t_id"]?.ToString(),
                    Gift = decimal.TryParse(Request.QueryString["gift"], out gift) ? String.Format("{0:0.0}", Math.Truncate(gift * 10) / 10): "0.0",
                    Bonus = decimal.TryParse(Request.QueryString["bonus"], out bonus) ? String.Format("{0:0.0}", Math.Truncate(bonus * 10) / 10) : "0.0"
                };
            }

            return CurrentTemplate(model);
        }
    }
}
