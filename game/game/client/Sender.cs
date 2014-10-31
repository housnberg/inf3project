using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace game.client
{
    public class Sender
    {
        private TcpClient client;

        public Sender(TcpClient client)
        {
            setTcpClient(client);
        }

        private void setTcpClient(TcpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException("the parameter cannot be null");
            }
            this.client = client;
        }

        /// <summary>
        /// sends messages to the server
        /// </summary>
        /// <param name="message">the message to be send</param>
        public void send(String message)
        {

        }
    }
}
