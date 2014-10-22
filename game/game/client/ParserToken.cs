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

        ParserGate pg;
        String msg;
        bool msg_is_vld;

        /// <summary>
        /// Parses the message applying the "PLAYER" rule.
        /// </summary>
        /// <param name="p_msg">Part of original message, is expected to fit the "PLAYER" rule:</param>
        void parsePlayer(String p_msg)
        {
            Contract.Requires(p_msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }

        /// <summary>
        /// Parses the message applying the "DRAGON" rule.
        /// </summary>
        /// <param name="p_msg">Part of original message, is expected to fit the "DRAGON" rule:</param>
        void parseDrgn(String p_msg)
        {
            Contract.Requires(p_msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }
    }
}
