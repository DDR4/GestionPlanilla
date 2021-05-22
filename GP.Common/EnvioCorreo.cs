using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common
{
    public class EnvioCorreo
    {
        public static void Send(string destino,string asunto,string body,byte[] arraybytes,string nombrearchivo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                var email = EncriptacionBase64.Base64Decode(ConfigurationUtilities.GetAppSettings("Email"));

                mail.From = new MailAddress(destino);
                mail.To.Add(email);
                mail.Subject = asunto;
                mail.Body = body;

                mail.Attachments.Add(new Attachment(new MemoryStream(arraybytes), nombrearchivo));
       
                var clave = EncriptacionBase64.Base64Decode(ConfigurationUtilities.GetAppSettings("Clave"));

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email, clave);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }









    }
}
