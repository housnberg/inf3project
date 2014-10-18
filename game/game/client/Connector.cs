using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace game.client
{
    public class Connector
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiver;
        public Connector(String ip, UInt16 port)
        {
            this.connect(ip, port);
            stream = this.getNetworkStream();
        }

        /// <summary>
        /// opens a new tcp-connection 
        /// </summary>
        /// <param name="ip">ip-adress of the server</param>
        /// <param name="port">applicationport</param>
        public void connect(String ip, UInt16 port)
        {
            try
            {
                if (client == null)
                {
                    client = new TcpClient(ip, port);
                }
                else
                {
                    if (!client.Connected)
                    {
                        client.Connect(ip, port);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// closes an ongoing tcp-connection
        /// </summary>
        public void disconnect()
        {
            try
            {
                if (client != null && client.Connected)
                {
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// sends messages to the server
        /// </summary>
        /// <param name="stream">the message to send</param>
        public void sendServerMessage(String message)
        {
            try
            {
                Byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// receives messages from the server
        /// </summary>
        private void receiveServerMessage()
        {

        }

        /// <summary>
        /// returns the network stream if the tcp-client is connected
        /// </summary>
        /// <returns>network stream</returns>
        private NetworkStream getNetworkStream()
        {
            if (client != null && client.Connected)
            {
                return client.GetStream();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// method mainly used for unit tests
        /// </summary>
        /// <returns>the ongoig tcp-client</returns>
        public TcpClient getTcpClient()
        {
            return client;
        }
    }
}
