using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using game.client;

namespace game.Parser
{
    public class ParserGate
    {
        private ClientBuffer buffer;
        private String message;
        private bool messageIsValid;
        private bool messageIsAvailable;

        /// <summary>
        /// Receives incoming messages from the buffer.
        /// Ensures that incoming messages from the server contain valid information.
        /// The information is then extracted and passed down to the GameManager
        /// </summary>
        public ParserGate()
        {

        }

        /// <summary>
        /// Gets one full message from the buffer, if one is available, and stores it in the message variable.
        /// </summary>
        public void getMessage()
        {
            Contract.Requires(messageIsAvailable);
            Contract.Ensures(message != null);
        }

        /// <summary>
        /// Parses the message and extracts information from it. Will call other methods, choosing based on message content.
        /// </summary>
        /// <param name="message">The message extracted from the buffer.</param>
        public void parse(String message)
        {
            Contract.Requires(message != null);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "MESSAGE" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "MESSAGE" rule.</param>
        public void parseMessage(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ANSWER" rule, which further applies the "OKAY", "DENY", "UNKNOWN" and "INVALID" rules.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ANSWER" rule</param>
        public void parseAnswer(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "YOURID" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "YOURID" rule.</param>
        public void parseYourId(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "TIME" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "TIME" rule.</param>
        public void parseTime(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ONLINE" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ONLINE" rule.</param>
        public void parseOnline(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ENTITIES" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ENTITIES" rule.</param>
        public void parseEntities(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "PLAYERS" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "PLAYERS" rule.</param>
        public void parsePlayers(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }


        /// <summary>
        /// After parsing the message, this method passes the extracted information on to methods required to process the information.
        /// </summary>
        public void passInformation()
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
