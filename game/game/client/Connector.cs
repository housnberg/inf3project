using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics.Contracts;


namespace game.client
{
    public class Connector
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiver;
        private Cache buffer;
        //private Buffer buffer;

        /// <summary>
        /// establishes automatically a connection to the server when an 
        /// instance is created
        /// </summary>
        /// <param name="ip">ip-address of the server</param>
        /// <param name="port">applicationport</param>
        public Connector(String ip, UInt16 port)
        {
            Contract.Requires(ip != null);
            Contract.Requires(ip.Length > 6);
            Contract.Requires(ip.Length < 16);
            Contract.Requires(port >= 0);
            Contract.Requires(port <= 65535);
            /*this.connect(ip, port);
            //buffer = new Buffer();
            stream = this.getNetworkStream();
            receiver = new Thread(this.receiveServerMessage);
            receiver.Start();*/
            Contract.Ensures(client != null);
            Contract.Ensures(client.Connected);
            Contract.Ensures(stream != null);
            Contract.Ensures(stream.CanWrite);
            Contract.Ensures(stream.CanRead);
            Contract.Ensures(receiver != null);
            Contract.Ensures(receiver.IsAlive);
            Contract.Ensures(buffer != null);
            Contract.Ensures(buffer.isEmpty());
        }

        /// <summary>
        /// opens a new tcp-connection 
        /// </summary>
        /// <param name="ip">ip-address of the server</param>
        /// <param name="port">applicationport</param>
        public void connect(String ip, UInt16 port)
        {
            Contract.Requires(ip != null);
            Contract.Requires(ip.Length > 6);
            Contract.Requires(ip.Length < 16);
            Contract.Requires(port >= 0);
            Contract.Requires(port <= 65535);
            /*try
            {
                if (ip != null)
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
                    Console.WriteLine("client connected");
                }
                else
                {
                    throw new System.ArgumentException("Parameter 'ip' cannot be null");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/
            Contract.Ensures(client != null);
            Contract.Ensures(client.Connected);
            Contract.Ensures(receiver.IsAlive);
        }

        /// <summary>
        /// closes an ongoing tcp-connection
        /// </summary>
        public void disconnect()
        {
            Contract.Requires(client != null);
            Contract.Requires(client.Connected);
            Contract.Requires(stream != null);
            Contract.Requires(receiver != null);
            Contract.Requires(receiver.IsAlive);
            /*try
            {
                if (client == null || stream == null || receiver == null)
                {
                    throw new System.ArgumentException("Parameter cannot be null");
                }
                if (client.Connected)
                {
                    receiver.Abort();
                    stream.Close();
                    client.Close();
                    Console.WriteLine("client disconnected");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/
            Contract.Ensures(!client.Connected);
            Contract.Ensures(!receiver.IsAlive);
            Contract.Ensures(!stream.CanRead);
            Contract.Ensures(!stream.CanWrite);
        }

        /// <summary>
        /// sends messages to the server
        /// </summary>
        /// <param name="stream">the message to send</param>
        public void sendServerMessage(String message)
        {
            Contract.Requires(message != null);
            Contract.Requires(message.Length >= 1);
            Contract.Requires(client != null);
            Contract.Requires(client.Connected);
            Contract.Requires(stream != null);
            Contract.Requires(stream.CanWrite);
            /*try
            {
                if (stream == null || message == null || message.Length < 1)
                {
                    throw new System.ArgumentException("Parameter cannot be null or the length of the message cannot be < 1");
                }
                else 
                {
                    if (stream.CanWrite)
                    {
                        Byte[] data = Encoding.UTF8.GetBytes(message + "\r\n");
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/
            Contract.Ensures(stream.CanRead);
        }

        /// <summary>
        /// receives messages from server permanently
        /// </summary>
        private void receiveServerMessage()
        {
            Contract.Requires(client != null);
            Contract.Requires(client.Connected);
            Contract.Requires(stream != null);
            Contract.Requires(stream.CanRead);
            Contract.Requires(receiver != null);
            Contract.Requires(receiver.IsAlive);
            Contract.Requires(buffer != null);
            /*String message;
            while (client.Connected)
            {
                message = "";
                byte[] data = new byte[client.Available];
                stream.Read(data, 0, data.Length);
                message = Encoding.UTF8.GetString(data);
                if (message != null && message.Length > 0)
                {
                    Console.WriteLine(message);
                    //METHOD FOR SAVING THE MESSAGE IN THE BUFFER
                }
            }*/
            Contract.Ensures(!buffer.isEmpty());
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

        [ContractInvariantMethod]
        protected void ObjectInvariant () 
        {
            Contract.Invariant(this.buffer != null);
            Contract.Invariant(this.client != null);
            Contract.Invariant(this.stream != null);
            Contract.Invariant(this.receiver != null);
        }
    }
}
