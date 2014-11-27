using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace game.Parser
{
    class ParserUpdateDelete
    {

        private ParserGate parserGate;
        private String message;
        private bool messageIsValid;

        public ParserUpdateDelete(ParserGate parserGate, String message, bool validity)
        {
            setParserGate(parserGate);
            setMessage(message);
            setMessageIsValid(validity);
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

        /// <summary>
        /// Parses the message applying the "UPDATE" rule. Will eventually call the ParserToken or ParserMap class.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "UPDATE" rule</param>
        public Token parseUpdate(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            if (message != null && messageIsValid)
            {
                message = parserGate.deleteLines("begin:upd", "end:upd", message);
                if (message.StartsWith("begin:dragon") && message.EndsWith("end:dragon") || message.StartsWith("begin:player") && message.EndsWith("end:player"))
                {
                    Token token = null;
                    ParserToken parserToken = new ParserToken(this.parserGate, message, this.messageIsValid, true);
                    if (message.Contains("type:Dragon"))
                    {
                        token = parserToken.parseDragon(message, false);
                    }
                    else if (message.Contains("type:Player"))
                    {
                        token = parserToken.parsePlayer(message, false);
                    }
                    
                    Contract.Ensures(messageIsValid);
                    return token;
                }
                else
                {
                    this.messageIsValid = false;
                    throw new ArgumentException("Message is invalid. ParserUpdateDelete, parseUpdate");
                }
            }
            else
            {
                this.messageIsValid = false;
                throw new ArgumentException("Message is invalid. ParserUpdateDelte, parseUpdate.");
            }
            
        }

        /// <summary>
        /// Parses the message applying the "DELETE" rule. Will eventually call the ParserToken class.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "DELETE" rule</param>
        private void parseDelete(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            Contract.Ensures(messageIsValid);
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(parserGate != null);
        }
    }
}
