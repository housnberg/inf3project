﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game.client
{
    class ParserMap
    {

        ParserGate pg;
        String msg;
        bool msg_is_vld;

        /// <summary>
        /// Parses the message applying the "MAP" rule.
        /// </summary>
        /// <param name="p_msg">Part of original message, is expected to fit the "MAP" rule.</param>
        void parseMap(String p_msg)
        {
            Contract.Requires(p_msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }

        /// <summary>
        /// Parses the message applying the "MAPCELL" rule.
        /// </summary>
        /// <param name="p_msg">Part of original message, is expected to fit the "MAPCELL" rule.</param>
        void parseMapcell(String p_msg)
        {
            Contract.Requires(p_msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }

        /// <summary>
        /// Parses the message applying the "PROPERTY" rule.
        /// </summary>
        /// <param name="p_msg">Part of original message, is expected to fit the "PROPERTY" rule.</param>
        void parsePrprty(String p_msg)
        {
            Contract.Requires(p_msg != null && msg_is_vld);
            Contract.Ensures(msg_is_vld);
        }
    }
}