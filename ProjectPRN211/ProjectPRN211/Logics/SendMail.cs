using System;
using System.Net.Mail;
using System.Text;

namespace ProjectPRN211.Logics
{
    public class SendMail
    {
        public void SendingEmail(string to, string subject, string mailbody)
        {
            string from = "soytengheanexample@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            message.Subject = subject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("soytengheanexample@gmail.com", "uqxvxkmdokyqrbil");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
