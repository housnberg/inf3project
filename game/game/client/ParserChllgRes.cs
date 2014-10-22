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
        ParserGate pg;
        String msg;
        bool msg_is_vld;

        /// <summary>
        /// Parses the message applying the "CHALLENGE" rule.
        /// </summary>
        /// <param name="msg">Part of original message, is expected to fit the "CHALLANGE" rule.</param>
        void parseChllg(String msg)
        {
            Contract.Requires(msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }

        /// <summary>
        /// Parses the message applying the "RESULT" rule. Will eventually call the parseOpp(String msg) method.
        /// </summary>
        /// <param name="msg">Part of original message, is expected to fit the "RESULT" rule.</param>
        void parseRes(String msg)
        {
            Contract.Requires(msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }

        /// <summary>
        /// Parses the message applying the "OPPONENT" rule. Will eventually call the ParserToken or ParserMap class.
        /// </summary>
        /// <param name="msg">Part of original message, is expected to fit the "OPPONENT" rule.</param>
        void parseOpp(String msg)
        {
            Contract.Requires(msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }
    }
}
