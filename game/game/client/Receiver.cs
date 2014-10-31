using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace game.client
{
    public class Receiver
    {
        private TcpClient client;

        public Receiver(TcpClient client)
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
        /// receives messages from the server permanently
        /// </summary>
        public String receive()
        {
            return "";
        }
    }
}
