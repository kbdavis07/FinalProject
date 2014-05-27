using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jitc.Models
{
    public class Message
    {
        public MailRecipient Recipient { get; set; }
        public ApplicationUser User { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
    }
}