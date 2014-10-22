using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game
{
    class ParserGate : System.Threading.Thread
    {
        String msg;
        bool msg_is_vld;
        bool msg_is_av;

        /// <summary>
        /// Receives incoming messages from the buffer.
        /// Ensures that incoming messages from the server contain valid information.
        /// The information is then extracted and passed down to the GameManager
        /// </summary>
        ParserGate()
        {

        }

        /// <summary>
        /// Gets one full message from the buffer, if one is available, and stores it in the msg variable.
        /// </summary>
        void getMsg()
        {
            Contract.Requires(msg_is_av);
            Contract.Ensures(msg != null);
        }

        /// <summary>
        /// Parses the message and extracts information from it.
        /// </summary>
        /// <param name="msg">The message extracted from the buffer.</param>
        void parse(String msg)
        {
            Contract.Requires(msg != null);
            Contract.Ensures(msg_is_vld);
        }

        /// <summary>
        /// After parsing the message, this method passes the extracted information on to methods required to process the information.
        /// </summary>
        void passInf()
        {
            Contract.Requires(msg_is_vld);
            Contract.Ensures(msg == null);
        }
    }
}
