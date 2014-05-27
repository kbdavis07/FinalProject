using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jitc.Models;
using System.Net.Mail;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Jitc
{
    public class MailSender
    {
        //http://msdn.microsoft.com/en-us/library/system.net.mail.sendcompletedeventhandler%28v=vs.110%29.aspx

        static bool mailSent = false;

        SentMail model = new SentMail();



        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.

            var model = new SentMail();


            int token = (int)e.UserState;


            if (e.Cancelled)
            {
                model.Status = ("Send Canceled" + token);
            }
            if (e.Error != null)
            {
                model.Status = ("Error:" + token + e.Error.ToString());
            }
            else
            {
                model.Status = ("Sent Successfully!");
            }

            mailSent = true;
        }

        //ToDo Add await operators for async

        public IEnumerable<SentMail> SendMail(IEnumerable<Message> mailMessages)
        {
            var output = new List<SentMail>();



            // Modify this to suit your business case:
            string mailUser = "help@Bloggersonline.com";
            string mailUserPwd = "bdavis28";
            SmtpClient client = new SmtpClient("mail.Bloggersonline.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(mailUser, mailUserPwd);
            client.EnableSsl = false;
            client.Credentials = credentials;


            foreach (var msg in mailMessages)
            {
                var mail = new MailMessage(msg.User.Email.Trim(), msg.Recipient.Email.Trim());

                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay;
                mail.Headers.Add("Disposition-Notification-To", "help@bloggersonline.com");

                mail.Subject = msg.Subject;
                mail.Body = msg.MessageBody;



                try
                {

                    // Set the method that is called back when the send operation ends.
                    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

                    int userState = msg.Recipient.MailRecipientId;
                    client.SendAsync(mail, userState);

                    var sentMessage = new SentMail()
                    {
                        MailRecipientId = msg.Recipient.MailRecipientId,
                        SentToMail = msg.Recipient.Email,
                        SentFromMail = msg.User.Email,
                        SentDate = DateTime.Now,
                        Status = "Sent Successfully!"

                    };

                    output.Add(sentMessage);



                    //Break for 5 Secs before sending next email out
                    System.Threading.Thread.Sleep(5000);



                    //// Clean up.
                    //mail.Dispose();

                }


                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {

                            int userState = msg.Recipient.MailRecipientId;

                            model.MailRecipientId = userState;
                            model.Status = ("Delivery failed - retrying in 5 seconds.");

                            System.Threading.Thread.Sleep(5000);


                            client.SendAsync(mail, userState);



                        }
                        else
                        {
                            //Console.WriteLine("Failed to deliver message to {0}",
                            //    ex.InnerExceptions[i].FailedRecipient);
                        }
                    }
                }



                catch (SmtpException e)
                {
                    throw e;
                    // Or, more likely, do some logging or something

                    //Console.WriteLine("Error: {0}", e.StatusCode);

                    //ToDo Add Error Report here

                    //http://msdn.microsoft.com/en-us/library/system.net.mail.smtpexception.aspx

                }


                finally
                {
                    mail.Dispose();
                }



            }
            return output;
        }
    }
}