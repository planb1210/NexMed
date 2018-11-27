using NexMed.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NexMed.Messaging
{
    public class MessageService
    {
        private readonly string emailFrom = "eisbrecher007@mail.ru";
        private readonly string emailPassword = "u356tMbF";

        public void SendEmail(string emailTo, Weather weather)
        {
            MailAddress from = new MailAddress(emailFrom);
            MailAddress to = new MailAddress(emailTo);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Weather Info";
            m.Body = $"<h2>New Weather</h2><div>Temperature {weather.Temperature}</div>div>Wind Speed {weather.WindSpeed}</div>div>Pressure {weather.Pressure}</div>";
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 25);
            smtp.Credentials = new NetworkCredential(emailFrom, emailPassword);
            smtp.EnableSsl = true;
            smtp.Send(m);
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
