using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game.client
{
    class ParserUpdDel
    {

        ParserGate parserGate;
        String message;
        bool messageIsValid;

        /// <summary>
        /// Parses the message applying the "UPDATE" rule. Will eventually call the ParserToken or ParserMap class.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "UPDATE" rule</param>
        void parseUpd(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "DELETE" rule. Will eventually call the ParserToken class.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "DELETE" rule</param>
        void parseDel(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }
    }
}
