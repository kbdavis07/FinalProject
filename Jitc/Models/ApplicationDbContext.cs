using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Jitc.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection2")
        {

        }

        public System.Data.Entity.DbSet<Jitc.Models.MailRecipient> MailRecipients { get; set; }
        public System.Data.Entity.DbSet<Jitc.Models.SentMail> SentMails { get; set; }
        public System.Data.Entity.DbSet<Jitc.Models.Email> Emails { get; set; }
        public System.Data.Entity.DbSet<Jitc.Models.MailingList> MailingLists { get; set; }
        public System.Data.Entity.DbSet<Jitc.Models.Subscription> Subscriptions { get; set; }
 
    }
}