namespace Jitc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SentMail
    {
        [Key]
        [Required]
        public int MailId { get; set; }

        [Required]
        public int MailRecipientId { get; set; }

        [Required]
        public string SentToMail { get; set; }

        [Required]
        public string SentFromMail { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public System.DateTime SentDate { get; set; }

        public bool MailOpened { get; set; }

        public virtual MailRecipient Recipient { get; set; }
    }
}
