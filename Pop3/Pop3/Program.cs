using System;
using System.Configuration;

namespace Pop3
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = ConfigurationManager.AppSettings["Host"];
            var portValue = ConfigurationManager.AppSettings["Port"];
            var port = 0;
            if (!int.TryParse(portValue, out port))
            {
                throw new Exception("Invalid port in app configuration");
            }

            var mailServerUserName = ConfigurationManager.AppSettings["MailServerUserName"];
            var mailServerPassword = ConfigurationManager.AppSettings["MailServerPassword"];
            var pop3 = new POP3(host, port, mailServerUserName, mailServerPassword);
        }
    }
}
