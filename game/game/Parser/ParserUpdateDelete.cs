using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game.Parser
{
    public class ParserUpdateDelete
    {

        public ParserGate parserGate;
        public String message;
        public bool messageIsValid;

        /// <summary>
        /// Parses the message applying the "UPDATE" rule. Will eventually call the ParserToken or ParserMap class.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "UPDATE" rule</param>
        public void parseUpdate(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "DELETE" rule. Will eventually call the ParserToken class.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "DELETE" rule</param>
        public void parseDelete(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(parserGate != null);
        }
    }
}
