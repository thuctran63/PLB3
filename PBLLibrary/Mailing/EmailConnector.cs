using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBLLibrary.Mailing
{
    internal class EmailConnector
    {
        private SmtpClient _client;

        public EmailConnector()
        {
            // Create a new SmtpClient object
            _client = new SmtpClient();

            // Set the SMTP server address and port number
            _client.Host = "smtp.gmail.com";
            _client.Port = 587;

            //Set the username and password for the SMTP server(if required)
            _client.UseDefaultCredentials = false;
            _client.Credentials = new NetworkCredential(GlobalConfig.ConnectionString("Email"),
                GlobalConfig.ConnectionString("Password"));

            // Enable SSL encryption (if required)
            _client.EnableSsl = true;

        }

        public void SendMessage(string desEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();

            // Set the sender, recipient, subject, and body of the message
            message.From = new MailAddress(GlobalConfig.ConnectionString("Email"));
            message.To.Add(new MailAddress(desEmail));
            message.Subject = subject;
            message.Body = body;

            // Send the message
            _client.Send(message);
        }
    }
}
