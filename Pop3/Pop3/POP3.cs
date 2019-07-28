using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pop3
{
    public interface IPOP3
    {
        Dictionary<int, Message> FetchAllMessages();
        void DeleteMessage(int messageNumber);
    }

    public class POP3
    {
        private readonly string host;
        private readonly int port;
        private readonly string mailServerUserName;
        private readonly string mailServerPassword;
        public POP3(string host, int port, string mailServerUserName, string mailServerPassword)
        {
            this.host = host;
            this.port = port;
            this.mailServerUserName = mailServerUserName;
            this.mailServerPassword = mailServerPassword;
        }

        public Dictionary<int, Message> FetchAllMessages()
        {
            var result = new Dictionary<int, Message>();

            using (Pop3Client client = new Pop3Client())
            {
                client.Connect(host, port, false);

                client.Authenticate(mailServerUserName, mailServerPassword, AuthenticationMethod.UsernameAndPassword);

                int messageCount = client.GetMessageCount();

                for (int i = messageCount; i > 0; i--)
                {
                    result.Add(i, client.GetMessage(i));
                }
            }

            return result;
        }

        private void DeleteMessage(int messageNumber)
        {
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(host, port, false);

                // Authenticate ourselves towards the server
                client.Authenticate(mailServerUserName, mailServerPassword, AuthenticationMethod.UsernameAndPassword);

                client.DeleteMessage(messageNumber);

                client.Disconnect();
            }
        }
    }
}
