using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace game.client
{
    public class ClientBuffer
    {
        private List<String> fifo;
        private const int MAX_SIZE = 5;
        private static ClientBuffer buffer = new ClientBuffer();
        private UInt16 messageCounter = 0;
        private String fullServerMessage = String.Empty;

        private ClientBuffer()
        {
            Contract.Requires(MAX_SIZE > 0);
            fifo = new List<String>();
            Contract.Ensures(ClientBuffer.buffer != null);
            Contract.Ensures(fifo != null);
        }

        /// <summary>
        /// method which concatenate the fullServerMessage string with a new received strring message
        /// </summary>
        /// <param name="message">messages to concatenate</param>
        public void put(String message)
        {
            Contract.Requires(message != null);
            Contract.Requires(message.Length > 0);
            try
            {
                Monitor.Enter(buffer);
                if ((message == null) || (message.Length < 0))
                {
                    throw new ArgumentException("The message is null or smaller 0, or the fifo is null");
                }

                fullServerMessage += message + "\r\n";
                Contract.Ensures(!buffer.isEmpty());

                while (fullServerMessage.Contains("begin:" + messageCounter) && fullServerMessage.Contains("end:" + messageCounter))
                {
                    while (isFull())
                    {
                        Console.WriteLine("the buffer is currently full");
                        Monitor.Wait(buffer);
                    }
                    String[] tmp = Regex.Split(fullServerMessage, "end:" + messageCounter);
                    fifo.Add(tmp[0].Trim() + "\r\nend:" + messageCounter);
                    fullServerMessage = tmp[1];
                    messageCounter++;

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Monitor.PulseAll(buffer);
                Monitor.Exit(buffer);
            }
        }

        /// <summary>
        /// method for removing and getting the first element of the buffer
        /// </summary>
        /// <returns>first element of the buffer</returns>
        public String getElement()
        {
            Contract.Requires(fifo != null);
            Contract.Requires(fifo.ElementAt(0) != null);
            Contract.Requires(!(this.isEmpty()));
            String message = "";
            try
            {
                Monitor.Enter(buffer);
                while (this.isEmpty())
                {
                    Monitor.Wait(buffer);
                }
                message = fifo.ElementAt(0);
                fifo.RemoveAt(0);
                Contract.Ensures(!(buffer.isFull()));

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Monitor.PulseAll(buffer);
                Monitor.Exit(buffer);
            }
            return message;
        }

        /// <summary>
        /// method returns whether the buffer is empty or not
        /// </summary>
        /// <returns>buffer empty or not</returns>
        public Boolean isEmpty()
        {
            Contract.Requires(fifo != null);
            Boolean isEmpty = false;
            if (fifo.Count == 0)
            {
                isEmpty = true;
            }
            return isEmpty;
        }

        /// <summary>
        /// method returns whether the buffer is full or not
        /// </summary>
        /// <returns>buffer full or not</returns>
        public Boolean isFull()
        {
            Contract.Requires(fifo != null);
            Boolean isFull = false;
            if (fifo.Count == MAX_SIZE)
            {
                isFull = true;
            }
            return isFull;
        }

        /// <summary>
        /// method returns the current size of the buffer
        /// </summary>
        /// <returns>current size</returns>
        public int getSize()
        {
            Contract.Requires(fifo != null);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return fifo.Count;
        }

        /// <summary>
        /// method clear the buffer
        /// </summary>
        public void clear()
        {
            Contract.Requires(fifo != null);
            fifo.Clear();
        }




        /// <summary>
        /// method returns a static instance of the buffer
        /// implemented visa singleton-architecture
        /// </summary>
        /// <returns>current buffer instance</returns>
        public static ClientBuffer getBufferInstance() 
        {
            return buffer;
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(buffer != null);
            Contract.Invariant(fifo != null);
            Contract.Invariant(this.getSize() >= 0);
            Contract.Invariant(this.getSize() <= MAX_SIZE);
        }

    }
}
