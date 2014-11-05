using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace game.client
{
    public class ClientBuffer
    {
        private List<String> fifo;
        private const int MAX_SIZE = 49;
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
            if ((message == null) || (message.Length < 0))
            {
                throw new ArgumentException("The message is null or smaller 0, or the fifo is null");
            }
            else
            {
                fullServerMessage += message + "\r\n";    
            }
            Contract.Ensures(!this.isEmpty());
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
            if ((this.isEmpty()))
            {
                throw new ArgumentException("the buffer is empty");
            }
            else
            {
                String message = fifo.ElementAt(0);
                fifo.RemoveAt(0);
                Contract.Ensures(!this.isFull());
                return message;
            }
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

        /// <summary>
        /// splits a full message before storing it in the buffer 
        /// </summary>
        public void splitAndStore()
        {
            try
            {
                if (isFull())
                {
                    throw new SystemException("the buffer is currently full!");
                }
                else
                {
                    while (fullServerMessage.Contains("begin:" + messageCounter) && fullServerMessage.Contains("end:" + messageCounter))
                    {
                        String[] tmp = Regex.Split(fullServerMessage, "end:" + messageCounter);
                        fifo.Add(tmp[0].Trim());
                        fullServerMessage = tmp[1];
                        //for test purposes only
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\test_" + (messageCounter) + ".txt", true))
                        {
                            file.WriteLine(getElement());
                        }
                        messageCounter++;
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
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
