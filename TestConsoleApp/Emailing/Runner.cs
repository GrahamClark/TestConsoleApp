using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace TestConsoleApp.Emailing
{
    class Runner : IRunner
    {
        public void RunProgram()
        {
            var message = new MailMessage();
            message.From = new MailAddress("a@a.com");
            message.To.Add(new MailAddress("graham.clark@assureweb.co.uk"));
            message.Subject = "test";

            SmtpClient client = new SmtpClient("mail.assureweb.co.uk", 25);
            client.Send(message);
        }
    }
}
