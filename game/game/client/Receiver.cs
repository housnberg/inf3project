using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace game.client
{
    class Receiver
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
            try
            {
                byte[] data = new byte[client.Available];
                client.GetStream().Read(data, 0, data.Length);
                return (Encoding.UTF8.GetString(data));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }
    }
}
