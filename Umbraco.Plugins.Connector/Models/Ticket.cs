using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Plugins.Connector.Models
{
    public class Ticket
    {
        public bool IsBlocked { get; set; }
        public string EmailAddresses { get; set; }
        public DateTime? LastAuthenticatedDate { get; set; }
        public int Id { get; set; }
        public Guid TicketGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime RespondDueDate { get; set; }
        public DateTime? RespondDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Guid TenantGuid { get; set; }
        public string PlatformGuid { get; set; }
        public Guid AccountId { get; set; }
        public Guid? AssignedAccountId { get; set; }
        public DateTime? LastUpdatedUserId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<TicketResponseMessage> TicketResponseMessages { get; set; }
        public string PersianDateTime { get; set; }
        public DateTime AdminGMTDateTime { get; set; }
        public string AdminPersianDateTime { get; set; }
        public DateTime LastUpdatedGMTDateTime { get; set; }
        public Category Category { get; set; }
        public DateTime? LastEscalated { get; set; }
        public DateTime? LastOpened { get; set; }

    }

    public class TicketResult
    {
        public int Id { get; set; }
        public Guid TicketGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? RespondDueDate { get; set; }
        public DateTime? RespondDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public Guid? TenantGuid { get; set; }
        public string PlatformGuid { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? AssignedAccountId { get; set; }
        public int? LastUpdatedUserId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<TicketResponseMessage> TicketResponseMessages { get; set; }
        public string PersianDateTime { get; set; }
        public DateTime? AdminGMTDateTime { get; set; }
        public string AdminPersianDateTime { get; set; }
        public DateTime? LastUpdatedGMTDateTime { get; set; }
        public Category Category { get; set; }
        public DateTime? LastEscalated { get; set; }
        public DateTime? LastOpened { get; set; }
    }

    public class TicketResponseMessage
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? AccountId { get; set; }
        public string UserName { get; set; }
        public int MessageSenderType { get; set; }
        public int TicketResponseType { get; set; }
        public string MessageText { get; set; }
        public int Status { get; set; }
        public string PersianDateTime { get; set; }
        public DateTime? AdminGMTDateTime { get; set; }
        public string AdminPersianDateTime { get; set; }
        public string Attachment { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string TenantGuid { get; set; }
        public string PlatformGuid { get; set; }
        public string OrderNo { get; set; }
    }
}
