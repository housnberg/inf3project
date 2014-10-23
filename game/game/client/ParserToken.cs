using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game.client
{
    class ParserToken
    {

        ParserGate parserGate;
        String message;
        bool messageIsValid;

        /// <summary>
        /// Parses the message applying the "PLAYER" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "PLAYER" rule:</param>
        void parsePlayer(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "DRAGON" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "DRAGON" rule:</param>
        void parseDragon(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }
    }
}
