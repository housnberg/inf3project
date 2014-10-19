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
        //private Buffer buffer;

        /// <summary>
        /// establishes automatically a connection to the server when an 
        /// instance is created
        /// </summary>
        /// <param name="ip">ip-address of the server</param>
        /// <param name="port">applicationport</param>
        public Connector(String ip, UInt16 port)
        {
            this.connect(ip, port);
            //buffer = new Buffer();
            stream = this.getNetworkStream();
            receiver = new Thread(this.receiveServerMessage);
            receiver.Start();
        }

        /// <summary>
        /// opens a new tcp-connection 
        /// </summary>
        /// <param name="ip">ip-address of the server</param>
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
                        client = new TcpClient(ip, port);
                        receiver.Start();
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
                    receiver.Abort();
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
                Byte[] data = Encoding.UTF8.GetBytes(message + "\r\n");
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void receiveServerMessage()
        {
            String message;
            while (client.Connected)
            {
                message = "";
                byte[] data = new byte[client.Available];
                stream.Read(data, 0, data.Length);
                message = Encoding.UTF8.GetString(data);
                if (message != null && message.Length > 0)
                {
                    Console.WriteLine(message);
                    /*
                    * METHOD FOR SAVING THE MESSAGE IN THE BUFFER
                    */
                }
            }
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
