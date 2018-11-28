using NexMed.Entities;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace NexMed.Messaging
{
    public class MailService
    {
        private const string emailFrom = "test@test.ru";
        private const string emailPassword = "test";
        private const string smtpMail = "smtp.mail.ru";
        private const int port = 25;

        public void SendEmail(string emailTo, Weather weather)
        {
            MailAddress from = new MailAddress(emailFrom);
            MailAddress to = new MailAddress(emailTo);
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = "Weather Info";
            mailMessage.Body = $"<h2>New Weather</h2><div>Temperature {weather.Temperature}</div><div>Wind Speed {weather.WindSpeed}</div><div>Pressure {weather.Pressure}</div>";
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpMail, port);
            smtp.Credentials = new NetworkCredential(emailFrom, emailPassword);
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mailMessage);
            }
            catch
            {
                //todo logging
            }
        }

        public void SendEmails(List<User> users, Weather weather)
        {
            foreach (var user in users)
            {
                SendEmail(user.Email, weather);
            }
        }
    }
}
