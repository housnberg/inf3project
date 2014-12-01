using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using game.backend;

namespace game.Parser
{
    class ParserMap
    {

        private ParserGate parserGate;
        private String message;
        private bool messageIsValid;

        public ParserMap(ParserGate parserGate, String message, bool validity)
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
            {
                this.parserGate = parserGate;
            }
                
        }

        /// <summary>
        /// Setter for the message variable. The new value is only set if parameter != null.
        /// </summary>
        /// <param name="message">The part of the original message passed down from the calling ParserGate object.</param>
        private void setMessage(String message)
        {
            if (message != null)
            {
                this.message = message;
            }
                
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
        /// Parses the message applying the "MAP" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "MAP" rule.</param>
        public Map parseMap(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (message != null && messageIsValid)
            {
                message = this.parserGate.deleteLines("begin:map", "end:map", message);
                String cells = message.Substring(message.IndexOf("begin:cells"));
                cells = cells.Trim();
                cells = this.parserGate.deleteLines("begin:cells", "end:cells", cells);
                cells = cells.Trim();
                cells = cells.Replace("begin:cell", "#");
                cells = cells.Replace("end:cell", "#");
                String[] cellArray = Regex.Split(cells, "#");

                String mapData = message.Remove(message.IndexOf("begin:cells"));
                mapData = mapData.Trim();
                String[] mapDataArray = Regex.Split(mapData, "\n");
                for (int i = 0; i < mapDataArray.Length; i++)
                {
                    mapDataArray[i] = mapDataArray[i].Substring(mapDataArray[i].IndexOf(":") + 1);
                }
                int width = Convert.ToInt32(mapDataArray[0]);
                int height = Convert.ToInt32(mapDataArray[1]);
                Map map = new Map(height, width);

                foreach (String s in cellArray)
                {
                    if(s.Contains("row:") && s.Contains("col:"))
                    {
                        map.setField(this.parseMapcell(s));
                    }
                }
                Contract.Ensures(messageIsValid);
                return map;
            }
            else
            {
                this.messageIsValid = false;
                throw new ArgumentException("Message is invalid. ParserMap, parseMap.");
            }
            
        }

        /// <summary>
        /// Parses the message applying the "MAPCELL" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "MAPCELL" rule.</param>
        public Field parseMapcell(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                partOfMessage = this.parserGate.deleteLines("begin:cell", "end:cell", partOfMessage);
                String properties = partOfMessage.Substring(partOfMessage.IndexOf("begin:props"));
                partOfMessage = partOfMessage.Remove(partOfMessage.IndexOf("begin:props"));
                partOfMessage = partOfMessage.Trim();
                String[] rowsAndColumns = Regex.Split(partOfMessage, "\n");
                int row = Convert.ToInt32(rowsAndColumns[0]);
                int column = Convert.ToInt32(rowsAndColumns[1]);
                List<FieldType> fieldTypes = this.parseProperty(properties);
                Field mapCell = new Field(row, column, fieldTypes);
                Contract.Ensures(messageIsValid);
                return mapCell;
            }
            else
            {
                this.messageIsValid = false;
                throw new ArgumentException("Message is invalid. ParserMap, parseMapcell.");
            }
            
        }

        /// <summary>
        /// Parses the message applying the "PROPERTY" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "PROPERTY" rule.</param>
        private List<FieldType> parseProperty(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                partOfMessage = this.parserGate.deleteLines("begin:props", "end:props", partOfMessage);
                partOfMessage = partOfMessage.Trim();
                String[] fieldTypes = Regex.Split(partOfMessage, "\n");
                List<FieldType> properties = new List<FieldType>();
                foreach (String s in fieldTypes)
                {
                    switch (s)
                    {
                        case "WALKABLE":
                            properties.Add(FieldType.WALKABLE);
                            break;
                        case "WALL":
                            properties.Add(FieldType.WALL);
                            break;
                        case "FOREST":
                            properties.Add(FieldType.FOREST);
                            break;
                        case "WATER":
                            properties.Add(FieldType.WATER);
                            break;
                        case "HUNTABLE":
                            properties.Add(FieldType.HUNTABLE);
                            break;

                    }
                }
                Contract.Ensures(messageIsValid);
                return properties;
            }
            else
            {
                this.messageIsValid = false;
                throw new ArgumentException("Message is invalid. ParserMap, parseProperties.");
            }
            
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(parserGate != null);
        }
    }
}
