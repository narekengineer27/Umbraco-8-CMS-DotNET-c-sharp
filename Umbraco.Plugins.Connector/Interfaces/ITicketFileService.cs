namespace Umbraco.Plugins.Connector.Interfaces
{
    using System.Threading.Tasks;
    using System.Web;
    public interface ITicketFileService
    {
        Task<IResponseContent> Upload(string tenantUid, string token, string origin, HttpPostedFileBase file);

        Task<IResponseContent> UploadAnonymous(string tenantUid, string token, string origin, HttpPostedFileBase file);
    }
}
