using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace game.Parser
{
    public class ParserChallengeResult
    {
        private ParserGate parserGate;
        private String message;
        private bool messageIsValid;

        public ParserChallengeResult(ParserGate parserGate, String message, bool messageIsValid)
        {
            setParserGate(parserGate);
            setMessage(message);
            setMessageIsValid(messageIsValid);
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
        /// Parses the message applying the "CHALLENGE" rule.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "CHALLANGE" rule.</param>
        public void parseChallenge(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            if (message != null && messageIsValid)
            {
                message = this.parserGate.deleteLines("begin:challenge", "end:challenge", message);
                String[] data = Regex.Split(message, "\n");
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = data[i].Substring(data[i].IndexOf(":") + 1);
                }
                int id = Convert.ToInt32(data[0]);
                String minigame = data[1];
                bool accepted = Convert.ToBoolean(data[2]);
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "RESULT" rule. Will eventually call the parseOpp(String message) method.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "RESULT" rule.</param>
        public void parseResult(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            if (message != null && messageIsValid)
            {
                message = this.parserGate.deleteLines("begin:result", "end:result", message);
                String opponents = message.Substring(message.IndexOf("begin:opponents"));
                opponents = opponents.Trim();
                String resultData = message.Remove(message.IndexOf("begin:opponents"));
                resultData = resultData.Trim();
                String[] resultDataArray = Regex.Split(resultData, "\n");
                for (int i = 0; i < resultDataArray.Length; i++)
                {
                    resultDataArray[i] = resultDataArray[i].Substring(resultDataArray[i].IndexOf(":") + 1);
                }

                int round = Convert.ToInt32(resultDataArray[0]);
                bool running = Convert.ToBoolean(resultDataArray[1]);
                int delay = Convert.ToInt32(resultDataArray[2]);
            }
            else
            {
                this.messageIsValid = false;
                throw new ArgumentException("Message is invalid. ParserChallengeResult, parseResult.");
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "OPPONENT" rule. Will eventually call the ParserToken or ParserMap class.
        /// </summary>
        /// <param name="message">Part of original message, is expected to fit the "OPPONENT" rule.</param>
        private void parseOpponent(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            if (message != null && messageIsValid)
            {
                message = this.parserGate.deleteLines("begin:opponents", "end:opponents", message);
                message = message.Replace("begin:opponent", "#");
                message = message.Replace("end:opponent", "#");
                message = message.Trim();
                String[] opponents = Regex.Split(message, "#");
            }
            else
            {
                this.messageIsValid = false;
                throw new ArgumentException("Message is invalid. ParserChallengeResult, parseOpponent.");
            }
            Contract.Ensures(messageIsValid);
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(parserGate != null);
        }
    }
}
