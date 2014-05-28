using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Jitc.Jobs
{

    public class SendMailJob : IJob
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SendMailJob));

        /// <summary> The host name of the smtp server. REQUIRED.</summary>
        public const string PropertySmtpHost = "mail.bloggersonline.com";

        /// <summary> The port of the smtp server. Optional.</summary>
        public const string PropertySmtpPort = "587";

        /// <summary> Username for authenticated session. Password must also be set if username is used. Optional.</summary>
        public const string PropertyUsername = "Help@BloggersOnline.com";

        /// <summary> Password for authenticated session. Optional.</summary>
        public const string PropertyPassword = "bdavis28";

        /// <summary> The e-mail address to send the mail to. REQUIRED.</summary>
        public const string PropertyRecipient = "Kbdavis07@yahoo.com";

        /// <summary> The e-mail address to cc the mail to. Optional.</summary>
        public const string PropertyCcRecipient = "cc_recipient";

        /// <summary> The e-mail address to claim the mail is from. REQUIRED.</summary>
        public const string PropertySender = "Help@bloggersOnline.com";

        /// <summary> The e-mail address the message should say to reply to. Optional.</summary>
        public const string PropertyReplyTo = "Help@bloggersOnline.com";

        /// <summary> The subject to place on the e-mail. REQUIRED.</summary>
        public const string PropertySubject = "Testing From SendMail Job";

        /// <summary> The e-mail message body. REQUIRED.</summary>
        public const string PropertyMessage = "Testing From Send Mail Job 1";

        /// <summary> The message subject and body content type. Optional.</summary>
        public const string PropertyEncoding = "html";

        /// <summary>
        /// Executes the job.
        /// </summary>
        /// <param name="context">The job execution context.</param>
        public virtual void Execute(IJobExecutionContext context)
        {
            JobDataMap data = context.MergedJobDataMap;

            MailMessage message = BuildMessageFromParameters(data);

            try
            {
                string portString = GetOptionalParameter(data, PropertySmtpPort);
                int? port = null;
                if (!string.IsNullOrEmpty(portString))
                {
                    port = Int32.Parse(portString);
                }

                var info = new MailInfo
                {
                    MailMessage = message,
                    SmtpHost = GetRequiredParameter(data, PropertySmtpHost),
                    SmtpPort = port,
                    SmtpUserName = GetOptionalParameter(data, PropertyUsername),
                    SmtpPassword = GetOptionalParameter(data, PropertyPassword),
                };
                Send(info);
            }
            catch (Exception ex)
            {
                throw new JobExecutionException(string.Format(CultureInfo.InvariantCulture, "Unable to send mail: {0}", GetMessageDescription(message)), ex, false);
            }
        }

        protected virtual MailMessage BuildMessageFromParameters(JobDataMap data)
        {
            string to = GetRequiredParameter(data, PropertyRecipient);
            string from = GetRequiredParameter(data, PropertySender);
            string subject = GetRequiredParameter(data, PropertySubject);
            string message = GetRequiredParameter(data, PropertyMessage);

            string cc = GetOptionalParameter(data, PropertyCcRecipient);
            string replyTo = GetOptionalParameter(data, PropertyReplyTo);

            string encoding = GetOptionalParameter(data, PropertyEncoding);

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(to);

            if (!string.IsNullOrEmpty(cc))
            {
                mailMessage.CC.Add(cc);
            }
            mailMessage.From = new MailAddress(from);

            if (!string.IsNullOrEmpty(replyTo))
            {

                mailMessage.ReplyToList.Add(new MailAddress(replyTo));

            }

            mailMessage.Subject = subject;
            mailMessage.Body = message;

            if (!string.IsNullOrEmpty(encoding))
            {
                var encodingToUse = Encoding.GetEncoding(encoding);
                mailMessage.BodyEncoding = encodingToUse;
                mailMessage.SubjectEncoding = encodingToUse;
            }

            return mailMessage;
        }

        protected virtual string GetRequiredParameter(JobDataMap data, string propertyName)
        {
            string value = data.GetString(propertyName);
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(propertyName + " not specified.");
            }
            return value;
        }

        protected virtual string GetOptionalParameter(JobDataMap data, string propertyName)
        {
            string value = data.GetString(propertyName);

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value;
        }

        protected virtual void Send(MailInfo mailInfo)
        {
            log.Info(string.Format(CultureInfo.InvariantCulture, "Sending message {0}", GetMessageDescription(mailInfo.MailMessage)));

            var client = new SmtpClient(mailInfo.SmtpHost);

            if (mailInfo.SmtpUserName != null)
            {
                client.Credentials = new NetworkCredential(mailInfo.SmtpUserName, mailInfo.SmtpPassword);
            }

            if (mailInfo.SmtpPort != null)
            {
                client.Port = mailInfo.SmtpPort.Value;
            }

            // Do not remove this using. In .NET 4.0 SmtpClient implements IDisposable.
            using (client as IDisposable)
            {
                client.Send(mailInfo.MailMessage);
            }
        }

        private static string GetMessageDescription(MailMessage message)
        {
            string mailDesc = string.Format(CultureInfo.InvariantCulture, "'{0}' to: {1}", message.Subject, string.Join(", ", message.To.Select(x => x.Address).ToArray()));
            return mailDesc;
        }

        public class MailInfo
        {
            public MailMessage MailMessage { get; set; }

            public string SmtpHost { get; set; }

            public int? SmtpPort { get; set; }

            public string SmtpUserName { get; set; }

            public string SmtpPassword { get; set; }
        }
    }
}