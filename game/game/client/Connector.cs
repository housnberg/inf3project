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
        private Thread receiverThread;
        private ClientBuffer buffer;
        private Sender sender;
        private Receiver receiver;

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
            connect(ip, port);
            sender = new Sender(client);
            receiver = new Receiver(client);
            receiverThread = new Thread(this.receiveServerMessage);
            receiverThread.Start();
            Contract.Ensures(client != null);
            Contract.Ensures(client.Connected);
            Contract.Ensures(client.GetStream() != null);
            Contract.Ensures(client.GetStream().CanWrite);
            Contract.Ensures(client.GetStream().CanRead);
            Contract.Ensures(receiverThread != null);
            Contract.Ensures(receiverThread.IsAlive);
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
            lock (buffer)
            
                //try
                //{
                    if (ip == null || ip.Length > 16 || ip.Length < 7)
                    {
                        throw new ArgumentException("parameter cannot be null and parameter length must be bigger 7 and smaller 16");
                    }
                    else
                    {
                        if (client == null || !client.Connected)
                        {
                            client = new TcpClient(ip, port);
                            buffer.clear();
                        }
                        else
                        {
                            throw new SystemException("the client is already connected!");
                        }
                        Console.WriteLine("client connected");
                    }
                //}
                //catch (Exception exeption)
                //{
                //    Console.WriteLine(exeption.Message);
                //}
            
            
            Contract.Ensures(client != null);
            Contract.Ensures(client.Connected);
            Contract.Ensures(receiverThread.IsAlive);
        }

        /// <summary>
        /// closes an ongoing tcp-connection
        /// </summary>
        public void disconnect()
        {
            Contract.Requires(client != null);
            Contract.Requires(client.Connected);
            Contract.Requires(client.GetStream() != null);
            Contract.Requires(receiverThread != null);
            Contract.Requires(receiverThread.IsAlive);
            //try
            //{
                if (client == null || receiverThread == null)
                {
                    throw new System.ArgumentNullException("Parameter cannot be null");
                }
                if (client.Connected)
                {
                    receiverThread.Abort();
                    client.GetStream().Close();
                    client.Close();
                    Console.WriteLine("client disconnected");
                }
                else
                {
                    throw new SystemException("the client is already disconnected!");
                }
            //}
            //catch (Exception exeption)
            //{
            //    Console.WriteLine(exeption.Message);
            //}
            Contract.Ensures(!client.Connected);
            Contract.Ensures(!receiverThread.IsAlive);
            Contract.Ensures(!client.GetStream().CanRead);
            Contract.Ensures(!client.GetStream().CanWrite);
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
            Contract.Requires(client.GetStream() != null);
            Contract.Requires(client.GetStream().CanWrite);
            sender.send(message);
            Contract.Ensures(client.GetStream().DataAvailable);
        }

        /// <summary>
        /// receives messages from server permanently
        /// </summary>
        private void receiveServerMessage()
        {
            Contract.Requires(client != null);
            Contract.Requires(client.Connected);
            Contract.Requires(client.GetStream() != null);
            Contract.Requires(client.GetStream().CanRead);
            Contract.Requires(receiverThread != null);
            Contract.Requires(receiverThread.IsAlive);
            Contract.Requires(buffer != null);
            String message;

            //try
            //{
                if (client == null)
                {
                    throw new ArgumentException("The client has no connection to the Server");
                }
                while (client.Connected)
                {
                    message = receiver.receive();
                    if (message != null && message.Length > 0)
                    {
                        //for test purposes only
                        Console.WriteLine(message);
                        buffer.put(message);
                    }
                }
            //}
            //catch (Exception exception)
            //{
            //    Console.Out.WriteLine(exception.Message);
            //}

            Contract.Ensures(client.GetStream().DataAvailable);
            Contract.Ensures(!buffer.isEmpty());
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
            lock (buffer) // Is that right?

            return buffer;
        }

        /// <summary>
        /// method mainly used for unit tests
        /// </summary>
        /// <returns>the used networkstream</returns>
        public NetworkStream getStream()
        {
            return client.GetStream();
        }

        /// <summary>
        /// method mainly used for unit tests
        /// </summary>
        /// <returns>the used receiver-thread</returns>
        public Thread getReceiver()
        {
            return receiverThread;
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.buffer != null);
            Contract.Invariant(this.client != null);
            Contract.Invariant(this.receiverThread != null);
        }
    }
}
