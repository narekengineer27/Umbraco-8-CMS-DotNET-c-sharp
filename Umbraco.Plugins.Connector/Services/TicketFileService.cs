namespace Umbraco.Plugins.Connector.Services
{
    using System.Threading.Tasks;
    using System.Web;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Models;
    public class TicketFileService : BaseService, ITicketFileService
    {

        private string URL_API_UPLOAD;
        private string URL_API_UPLOAD_ANONYMOUS;

        public TicketFileService()
        {
            URL_API_UPLOAD = URL_HELPDESK_MANAGEMENT_UPLOAD_DOMAIN + "/api/file/upload";
            URL_API_UPLOAD_ANONYMOUS = URL_HELPDESK_MANAGEMENT_UPLOAD_DOMAIN + "/api/file/anonymous/upload";
        }

        public async Task<IResponseContent> Upload(string tenantUid, string token, string origin, HttpPostedFileBase file)
        {
            var response = await SubmitPostAsync(URL_API_UPLOAD, token, origin, file, tenantUid);
            return await AssertResponseContentAsync<TicketFileResponseContent>(response);
        }

        public async Task<IResponseContent> UploadAnonymous(string tenantUid, string token, string origin, HttpPostedFileBase file)
        {
            var response = await SubmitPostAsync(URL_API_UPLOAD_ANONYMOUS, token, origin, file, tenantUid);
            return await AssertResponseContentAsync<TicketFileResponseContent>(response);
        }
    }
}
