using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game.client
{
    public class ParserMap
    {

        public ParserGate parserGate;
        public String message;
        public bool messageIsValid;

        /// <summary>
        /// Parses the message applying the "MAP" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "MAP" rule.</param>
        public void parseMap(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "MAPCELL" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "MAPCELL" rule.</param>
        public void parseMapcell(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "PROPERTY" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "PROPERTY" rule.</param>
        public void parseProperty(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }
    }
}
