using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics.Contracts;


namespace game.client
{
    public class Connector
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiver;
        private ClientBuffer buffer;

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
            buffer = ClientBuffer.getBufferInstance();
            this.connect(ip, port);
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
            try
            {
                if (ip == null || ip.Length > 16 || ip.Length < 7)
                {
                    throw new ArgumentException("parameter cannot be null and parameter length must be bigger 7 and smaller 16");
                }
                else
                {
                    if (client == null || !client.Connected)
                    {
                        client = new TcpClient(ip, port);
                        stream = this.getNetworkStream();
                        buffer.clear();
                        receiver = new Thread(this.receiveServerMessage);
                        receiver.Start();
                    }
                    else
                    {
                        throw new SystemException("the client is already connected!");
                    }
                    Console.WriteLine("client connected");
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
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
            try
            {
                if (client == null || stream == null || receiver == null)
                {
                    throw new System.ArgumentNullException("Parameter cannot be null");
                }
                if (client.Connected)
                {
                    receiver.Abort();
                    stream.Close();
                    client.Close();
                    Console.WriteLine("client disconnected");
                }
                else
                {
                    throw new SystemException("the client is already disconnected!");
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
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
            try
            {
                if (message == null || message.Length < 1)
                {
                    throw new System.ArgumentException("Parameter cannot be null or the parameter length cannot be < 1");
                }
                else
                {
                    if (stream.CanWrite)
                    {
                        Byte[] data = Encoding.UTF8.GetBytes(message + "\r\n");
                        stream.Write(data, 0, data.Length);
                        //StreamWriter sw = new StreamWriter(stream);
                        //sw.Write(message + "\r\n");
                        //sw.Flush();
                    }
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
            Contract.Ensures(stream.DataAvailable);
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
            String message;
            try
            {
                while (client.Connected)
                {
                    if (stream.CanRead)
                    {
                        message = "";
                        byte[] data = new byte[client.Available];
                        stream.Read(data, 0, data.Length);
                        message = Encoding.UTF8.GetString(data);
                        //StreamReader sr = new StreamReader(stream);
                        //message = sr.ReadLine();
                        if (message != null && message.Length > 0)
                        {
                            //for test cases only
                            Console.WriteLine(message);
                            buffer.put(message);
                        }
                    }
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine(exeption.Message);
            }
            //Contract.Ensures(stream.DataAvailable);
            //Contract.Ensures(!buffer.isEmpty());
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

        /// <summary>
        /// method mainly used for unit tests
        /// </summary>
        /// <returns>the used buffer</returns>
        public ClientBuffer getBuffer()
        {
            return buffer;
        }

        /// <summary>
        /// method mainly used for unit tests
        /// </summary>
        /// <returns>the used networkstream</returns>
        public NetworkStream getStream()
        {
            return stream;
        }

        /// <summary>
        /// method mainly used for unit tests
        /// </summary>
        /// <returns>the used receiver-thread</returns>
        public Thread getReceiver()
        {
            return receiver;
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.buffer != null);
            Contract.Invariant(this.client != null);
            Contract.Invariant(this.stream != null);
            Contract.Invariant(this.receiver != null);
        }
    }
}
