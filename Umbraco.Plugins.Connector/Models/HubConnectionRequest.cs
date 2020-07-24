namespace Umbraco.Plugins.Connector.Models
{
    using System.ComponentModel.DataAnnotations;
    public class HubConnectionRequest
    {
        [Required]
        public string UserId { get; set; }
    }
}
