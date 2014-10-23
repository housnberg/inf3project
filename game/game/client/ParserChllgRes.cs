using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game.client
{
    class ParserChllgRes
    {
        ParserGate parserGate;
        String message;
        bool messageIsValid;

        /// <summary>
        /// Parses the message applying the "CHALLENGE" rule.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "CHALLANGE" rule.</param>
        void parseChallenge(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "RESULT" rule. Will eventually call the parseOpp(String message) method.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "RESULT" rule.</param>
        void parseResult(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "OPPONENT" rule. Will eventually call the ParserToken or ParserMap class.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "OPPONENT" rule.</param>
        void parseOpponent(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }
    }
}
