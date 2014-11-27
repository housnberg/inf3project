using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace game.Parser
{
    class ParserToken
    {

        private ParserGate parserGate;
        private String message;
        private bool messageIsValid;
        private bool messageIsCut;

        public ParserToken(ParserGate parserGate, String message, bool validity, bool messageIsCut)
        {
            setParserGate(parserGate);
            setMessage(message);
            setMessageIsValid(validity);
            setMessageIsCut(messageIsCut);
        }

        /// <summary>
        /// Getter for the parserGate variable.
        /// </summary>
        /// <returns>Returns the value of the parserGate variable.</returns>
        private ParserGate getParserGate()
        {
            return this.parserGate;
        }

        /// <summary>
        /// Getter for the message variable.
        /// </summary>
        /// <returns>Returns the value of the message variable.</returns>
        private String getMessage()
        {
            return this.message;
        }

        /// <summary>
        /// Getter for the messageIsValid variable.
        /// </summary>
        /// <returns>Returns the value of the messageIsValid variable.</returns>
        private bool getMessageIsValid()
        {
            return this.messageIsValid;
        }

        private bool getMessageIsCut()
        {
            return this.messageIsCut;
        }

        /// <summary>
        /// Setter for the parserGate variable. The new value is only set if parameter != null.
        /// </summary>
        /// <param name="parserGate">The ParserGate object which created this object.</param>
        private void setParserGate(ParserGate parserGate)
        {
            if (parserGate != null)
                this.parserGate = parserGate;
        }

        /// <summary>
        /// Setter for the message variable. The new value is only set if parameter != null.
        /// </summary>
        /// <param name="message">The part of the original message passed down from the calling ParserGate object.</param>
        private void setMessage(String message)
        {
            if (message != null)
                this.message = message;
        }

        /// <summary>
        /// Setter for the messageIsValid variable. The new value is only set if parameter != null.
        /// </summary>
        /// <param name="validity">Expresses the current state (true = valid, false = invalid) of the message in the parsing process.</param>
        private void setMessageIsValid(bool validity)
        {
            
                this.messageIsValid = validity;
        }

        private void setMessageIsCut(bool messageIsCut)
        {
            this.messageIsCut = messageIsCut;
        }

        /// <summary>
        /// Parses the message applying the "PLAYER" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "PLAYER" rule:</param>
        public Player parsePlayer(String partOfMessage, bool messageIsCut)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                if (!messageIsCut)
                {
                    partOfMessage = parserGate.deleteLines("begin:player", "end:player", partOfMessage);
                }
                String[] dataPlayer = Regex.Split(partOfMessage, "\n");
                int id = Convert.ToInt32(dataPlayer[0]);
                bool busy = Convert.ToBoolean(dataPlayer[2]);
                String desc = dataPlayer[3];
                int x = Convert.ToInt32(dataPlayer[4]);
                int y = Convert.ToInt32(dataPlayer[5]);
                int points = Convert.ToInt32(dataPlayer[6]);

                Contract.Ensures(messageIsValid);
                return new Player(id, busy, desc, x, y, points);
                
            }
            else
            {
                throw new ArgumentNullException();
            }
            
        }

        /// <summary>
        /// Parses the message applying the "DRAGON" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "DRAGON" rule:</param>
        public Dragon parseDragon(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                if (!messageIsCut)
                {
                    partOfMessage = parserGate.deleteLines("begin:dragon", "end:dragon", partOfMessage);
                }
                String[] dataPlayer = Regex.Split(partOfMessage, "\n");
                int id = Convert.ToInt32(dataPlayer[0]);
                bool busy = Convert.ToBoolean(dataPlayer[2]);
                String desc = dataPlayer[3];
                int x = Convert.ToInt32(dataPlayer[4]);
                int y = Convert.ToInt32(dataPlayer[5]);

                Contract.Ensures(messageIsValid);
                return new Dragon(id, busy, desc, x, y);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(parserGate != null);
        }
    }
}
