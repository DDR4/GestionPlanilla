using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common
{
    public class EnvioCorreo
    {
        public static void Send(string destino,string asunto,string body,Attachment attachment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            var email = EncriptacionBase64.Base64Decode(ConfigurationUtilities.GetAppSettings("Email"));

            mail.From = new MailAddress(destino);
            mail.To.Add(email);
            mail.Subject = asunto;
            mail.Body = body;

            mail.Attachments.Add(attachment);
       
            var clave = EncriptacionBase64.Base64Decode(ConfigurationUtilities.GetAppSettings("Clave"));

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(email, clave);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }









    }
}
