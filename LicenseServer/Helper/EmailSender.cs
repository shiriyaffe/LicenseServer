using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using SendGridLib;
namespace LicenseServer.Helper
{
    public class EmailSender
    {
        //NOTES:
        //1. you should add mailKit nuget package to the project
        //2. you should add using System.Net and System.Net.Email
        //3. if you send the email via GMAIL:
        //   You should set the email sender account to allow applications with low security access the account
        //   It can be done by loggin into the google account, under security, set "Access to applications with low security level"
        public static void SendEmail2(string subject, string body, string to, string toName, string from, string fromName, string pswd, string smtpUrl)
        {
            var fromAddress = new MailAddress(from, fromName);
            var toAddress = new MailAddress(to, toName);
            string fromPassword = pswd;

            var smtp = new SmtpClient
            {
                Host = smtpUrl,
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        public static void SendEmail(string subject, string body, string to, string toName, string from, string fromName, string pswd, string smtpUrl)
        {
            MailSender.SendEmail(fromName, to, toName, subject, body, "");
        }
    }
}
