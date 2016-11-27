using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eLargesse.Logic
{
    public class EmailFunctions
    {

        public static void Send(string recipient, string subject, string body)
        {
            MailMessage message = new MailMessage();

            message.To.Add(new MailAddress(recipient));
           
            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient();
            client.Send(message);

        }
       
    }
}
