using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using game.client;

namespace game.Parser
{
    public class ParserGate
    {
        private ClientBuffer buffer;
        private String message;
        private bool messageIsValid;
        private UInt16 messageCounter = 0;

        /// <summary>
        /// Receives incoming messages from the buffer.
        /// Ensures that incoming messages from the server contain valid information.
        /// The information is then extracted and passed down to the GameManager
        /// </summary>
        public ParserGate()
        {
            buffer = ClientBuffer.getBufferInstance();
        }

        /// <summary>
        /// Getter for the buffer variable.
        /// </summary>
        /// <returns>Returns the value of the buffer variable.</returns>
        ClientBuffer getBuffer()
        {
            lock(buffer)
            return this.buffer;
        }

        /// <summary>
        /// Getter for the messageIsValid variable.
        /// </summary>
        /// <returns>Returns the value of the messageIsValid variable.</returns>
        bool getMessageIsValid()
        {
            return this.messageIsValid;
        }

        /// <summary>
        /// Getter for the message variable.
        /// </summary>
        /// <returns>Returns the value of the message variable.</returns>
        String getMessage()
        {
            return this.message;
        }

        /// <summary>
        /// Setter for the buffer variable. The new value is only set if buffer != null.
        /// </summary>
        /// <param name="buffer">The ClientBuffer Object to which the parser will listen to.</param>
        private void setBuffer(ClientBuffer buffer)
        {
            lock(buffer)
            if (buffer != null)
                this.buffer = buffer;
        }

        /// <summary>
        /// Setter for the messageIsValid variable. The new value is only set if validity != null.
        /// </summary>
        /// <param name="validity">Bool variable expressing the current state (true = valid, false = invalid) of the message is the parsing process.</param>
        private void setMessageIsValid(bool validity)
        {
          
                this.messageIsValid = validity;
        }

        /// <summary>
        /// Gets one full message from the buffer, if one is available, and stores it in the message variable.
        /// </summary>
        private void extractMessage()
        {
            this.message = buffer.getElement();
            Contract.Ensures(message != null);
        }

        /// <summary>
        /// This method will be constantly asking the buffer is a message is available. If there is one, it will call extractMessage().
        /// </summary>
        private void listenToBuffer()
        {
            
        }

        private String deleteLines(String line0, String line1, String message)
        {
            String toReturn;
            toReturn = message.TrimStart(line0.ToCharArray());
            toReturn = toReturn.TrimEnd(line1.ToCharArray());
            return toReturn;
        }

        private void chooseParser(String message)
        {
            if (message.Contains("begin:upd") && message.Contains("end:upd"))
            {

            }
            if (message.Contains("begin:del") && message.Contains("end:del"))
            {

            }
            if (message.Contains("begin:map") && message.Contains("end:map"))
            {

            }
            if (message.Contains("begin:msg") && message.Contains("end:msg"))
            {
                this.parseMessage(message);
            }
            if (message.Contains("begin:result") && message.Contains("end:result"))
            {

            }
            if (message.Contains("begin:challenge") && message.Contains("end:challenge"))
            {

            }
            if (message.Contains("begin:player") && message.Contains("end:player"))
            {

            }
            if (message.Contains("begin:yourid") && message.Contains("end:yourid"))
            {

            }
            if (message.Contains("begin:time") && message.Contains("end:time"))
            {

            }
            if (message.Contains("begin:online") && message.Contains("end:online"))
            {

            }
            if (message.Contains("begin:ents") && message.Contains("end:ents"))
            {

            }
            if (message.Contains("begin:players") && message.Contains("end:players"))
            {

            }
            if (message.Contains("begin:server") && message.Contains("end:server"))
            {

            }
        }


        /// <summary>
        /// Parses the message and extracts information from it. Will call other methods, choosing based on message content.
        /// </summary>
        /// <param name="message">The message extracted from the buffer.</param>
        private void parse()
        {
            Contract.Requires(message != null);
            if (this.message != null)
            {
                if(this.message.Contains("begin:" + this.messageCounter) && this.message.Contains("end:" + this.messageCounter)){
                    this.message = this.deleteLines("begin:" + this.messageCounter, "end:" + this.messageCounter, this.message);
                }
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "MESSAGE" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "MESSAGE" rule.</param>
        private void parseMessage(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            partOfMessage = this.deleteLines("begin:mes", "end:mes", partOfMessage);
            
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ANSWER" rule, which further applies the "OKAY", "DENY", "UNKNOWN" and "INVALID" rules.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ANSWER" rule</param>
        private void parseAnswer(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "YOURID" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "YOURID" rule.</param>
        private void parseYourId(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "TIME" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "TIME" rule.</param>
        private void parseTime(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ONLINE" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ONLINE" rule.</param>
        private void parseOnline(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ENTITIES" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ENTITIES" rule.</param>
        private void parseEntities(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "PLAYERS" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "PLAYERS" rule.</param>
        private void parsePlayers(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }


        /// <summary>
        /// After parsing the message, this method passes the extracted information on to methods required to process the information.
        /// </summary>
        private void passInformation()
        {
            Contract.Requires(messageIsValid);
            Contract.Ensures(message == null);
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(buffer != null);
        }
    }
}
