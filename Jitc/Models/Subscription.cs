using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jitc.Models
{
    /// <summary>
    /// List of Subscriptions Each User Is subscribed to
    /// </summary>
    public partial class Subscription
    {
        [Key]
        public int SubscriptionId { get; set; }

        [Required]
        public int MailingListID { get; set; }

        [Required]
        public int MailRecipientID { get; set; }

        public virtual MailRecipient Recipients { get; set; }
        public virtual MailingList MailingLists { get; set; }

        

    }
}