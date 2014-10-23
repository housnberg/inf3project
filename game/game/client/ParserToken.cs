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
        /// <param name="p_msg">Part of original message, is expected to fit the "PLAYER" rule:</param>
        void parsePlayer(String p_msg)
        {
            Contract.Requires(p_msg != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "DRAGON" rule.
        /// </summary>
        /// <param name="p_msg">Part of original message, is expected to fit the "DRAGON" rule:</param>
        void parseDrgn(String p_msg)
        {
            Contract.Requires(p_msg != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }
    }
}
