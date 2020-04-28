using ProjectBlog.Entities.ComplexTypes.Dtos.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ProjectBlog.Core.Extensions
{
    public  class SendMail
    {
        public static void Send(MailContent mailContent) {
            var mail = new SmtpClient("smtp.gmail.com",587);
            mail.EnableSsl=true;
            mail.Credentials = new System.Net.NetworkCredential("otuzgundeingilizce@gmail.com", "Myfirstdomain54");
            var message = new MailMessage();
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            message.From = new MailAddress("otuzgundeingilizce@gmail.com");
            message.To.Add(mailContent.To);
            message.Subject =mailContent.Title;
            message.Body =mailContent.Body;
            mail.Send(message);
            //try catch loglama yapılabilir
        }

    }
}
