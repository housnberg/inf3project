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

        ParserGate pg;
        String msg;
        bool msg_is_vld;

        /// <summary>
        /// Parses the message applying the "UPDATE" rule. Will eventually call the ParserToken or ParserMap class.
        /// </summary>
        /// <param name="msg">Part of original message, is expected to fit the "UPDATE" rule</param>
        void parseUpd(String msg)
        {
            Contract.Requires(msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }

        /// <summary>
        /// Parses the message applying the "DELETE" rule. Will eventually call the ParserToken class.
        /// </summary>
        /// <param name="msg">Part of original message, is expected to fit the "DELETE" rule</param>
        void parseDel(String msg)
        {
            Contract.Requires(msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }
    }
}
