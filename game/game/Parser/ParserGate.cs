using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using game.client;
using game.backend;
using System.Collections;
using game.gui;

namespace game.Parser
{
    public class ParserGate
    {
        private ClientBuffer buffer;
        private String message;
        private bool messageIsValid;
        private static UInt16 messageCounter = 0;
        private static GameManager gameManager;

        /// <summary>
        /// Receives incoming messages from the buffer.
        /// Ensures that incoming messages from the server contain valid information.
        /// The information is then extracted and passed down to the GameManager
        /// </summary>
        public ParserGate()
        {
            gameManager = GameManager.getGameManagerInstance();
            buffer = ClientBuffer.getBufferInstance();
        }

        /// <summary>
        /// Getter for the buffer variable.
        /// </summary>
        /// <returns>Returns the value of the buffer variable.</returns>
        ClientBuffer getBuffer()
        {
            
            return this.buffer;
        }

        /// <summary>
        /// Getter for the messageIsValid variable.
        /// </summary>
        /// <returns>Returns the value of the messageIsValid variable.</returns>
        bool getMessageIsValid()
        {
            return this.messageIsValid;
        }

        /// <summary>
        /// Getter for the message variable.
        /// </summary>
        /// <returns>Returns the value of the message variable.</returns>
        String getMessage()
        {
            return this.message;
        }

        /// <summary>
        /// Setter for the buffer variable. The new value is only set if buffer != null.
        /// </summary>
        /// <param name="buffer">The ClientBuffer Object to which the parser will listen to.</param>
        private void setBuffer(ClientBuffer buffer)
        {
            lock(buffer)
            if (buffer != null)
                this.buffer = buffer;
        }

        /// <summary>
        /// Setter for the messageIsValid variable. The new value is only set if validity != null.
        /// </summary>
        /// <param name="validity">Bool variable expressing the current state (true = valid, false = invalid) of the message is the parsing process.</param>
        private void setMessageIsValid(bool validity)
        {
          
                this.messageIsValid = validity;
        }

        /// <summary>
        /// Gets one full message from the buffer, if one is available, and stores it in the message variable.
        /// </summary>
        public void extractMessage()
        {
            while (true)//while client.connected()
            {
                this.message = buffer.getElement();
                //try
                //{
                    Console.WriteLine("Begin parsing!");
                    this.parse();
                //}
                //catch (ArgumentException e)
                //{
                //    Console.WriteLine(e.Message);
                //}

                Contract.Ensures(message != null);
        
            }
           }

        /// <summary>
        /// This method will be constantly asking the buffer is a message is available. If there is one, it will call extractMessage().
        /// </summary>
        private void listenToBuffer()
        {
            
        }

        public String deleteLines(String line0, String line1, String message)
        {
            String toReturn;
            toReturn = message.TrimStart(line0.ToCharArray());
            toReturn = toReturn.TrimEnd(line1.ToCharArray());
            toReturn = toReturn.Trim();
            return toReturn;
        }

        private void chooseParser(String message)
        {
            Console.WriteLine("Choosing Parser!(l.123)");
            if (message.Contains("begin:upd") && message.Contains("end:upd"))
            {
                Console.WriteLine("Update Parser chosen!");
                ParserUpdateDelete parserUpdateDelete = new ParserUpdateDelete(this, message, this.messageIsValid);
                if (message.Contains("begin:dragon") || message.Contains("begin:player"))
                {
                    Console.WriteLine("Token Parser in Update Parser!");
                    Token token = parserUpdateDelete.parseUpdateToken(message);

                    Console.WriteLine(token.ToString());

                    this.passInformation("Update", token);
                }
                else if (message.Contains("begin:mapcell"))
                {
                    Console.WriteLine("Map Cell Parser in Update Parser!");
                    Field mapCell = parserUpdateDelete.parseUpdateMapcell(message);
                    this.passInformation("Update", mapCell);
                }
            }
            else if (message.Contains("begin:del") && message.Contains("end:del"))
            {
                Console.WriteLine("Deletion Parser chosen!");
                ParserUpdateDelete parserUpdateDelete = new ParserUpdateDelete(this, message, this.messageIsValid);
                Token token = parserUpdateDelete.parseDelete(message);
                this.passInformation("Delete", token);
            }
            else if (message.Contains("begin:map") && message.Contains("end:map"))
            {
                Console.WriteLine("Map Parser chosen!");
                ParserMap parserMap = new ParserMap(this, message, this.messageIsValid);
                Map map = parserMap.parseMap(message);
                this.passInformation("CreateMap", map);
            }
            else if (message.Contains("begin:msg") && message.Contains("end:msg"))
            {
                Console.WriteLine("Message Parser chosen!");
                this.parseMessage(message);
            }
            else if (message.Contains("begin:result") && message.Contains("end:result"))
            {
                Console.WriteLine("Result Parser chosen!");
                ParserChallengeResult parserChallengeResult = new ParserChallengeResult(this, message, this.messageIsValid);
                parserChallengeResult.parseResult(message);
            }
            else if (message.Contains("begin:challenge") && message.Contains("end:challenge"))
            {
                Console.WriteLine("Challenge Parser chosen!");
                ParserChallengeResult parserChallengeResult = new ParserChallengeResult(this, message, this.messageIsValid);
                parserChallengeResult.parseChallenge(message);
            }
            else if (message.Contains("begin:player") && message.Contains("end:player"))
            {
                Console.WriteLine("Player Parser chosen!");
                ParserToken parserToken = new ParserToken(this, message, this.messageIsValid, false);
                Player player = parserToken.parsePlayer(message, false);
                this.passInformation("Player", player);
            }
            else if (message.Contains("begin:yourid") && message.Contains("end:yourid"))
            {
                Console.WriteLine("ID chosen!");
                this.parseYourId(message);
            }
            else if (message.Contains("begin:time") && message.Contains("end:time"))
            {
                Console.WriteLine("Time Parser chosen!");
                this.parseTime(message);
            }
            else if (message.Contains("begin:online") && message.Contains("end:online"))
            {
                Console.WriteLine("Online Parser chosen!");
                this.parseOnline(message);
            }
            else if (message.Contains("begin:ents") && message.Contains("end:ents"))
            {
                Console.WriteLine("Entities Parser chosen!");
                List<Token> tokenList = this.parseEntities(message);
                this.passInformation("Entities", tokenList);
            }
            else if (message.Contains("begin:players") && message.Contains("end:players"))
            {
                Console.WriteLine("PlayerS Parser chosen!");
                List<Player> playerList = this.parsePlayers(message);
                this.passInformation("Players", playerList);
            }
            else if (message.Contains("begin:server") && message.Contains("end:server"))
            {
                Console.WriteLine("Server Parser chosen!");
                this.parseServer(message);
            }
            else
            {
                Console.WriteLine("No parser chosen");
            }
        }


        /// <summary>
        /// Parses the message and extracts information from it. Will call other methods, choosing based on message content.
        /// </summary>
        /// <param name="message">The message extracted from the buffer.</param>
        private void parse()
        {
            Contract.Requires(message != null);
            if (this.message != null)
            {
                if(this.message.Contains("begin:" + messageCounter) && this.message.Contains("end:" + messageCounter)){
                    this.message = this.deleteLines("begin:" + messageCounter, "end:" + messageCounter, this.message);
                    messageCounter++;
                    this.setMessageIsValid(true);
                    this.chooseParser(message);
                }
            }
            else
            {
                this.setMessageIsValid(false);
                throw new SystemException("Message is invalid. ParserGate, parse");
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "MESSAGE" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "MESSAGE" rule.</param>
        private void parseMessage(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                
                    partOfMessage = this.deleteLines("begin:mes", "end:mes", partOfMessage);
                    String[] messageArray = Regex.Split(partOfMessage, "\n");
                    for (int i = 0; i < messageArray.Length; i++)
                    {
                        messageArray[i] = messageArray[i].Substring(messageArray[i].IndexOf(":") + 1);
                        messageArray[i] = messageArray[i].Trim();
                    }

                    int srcid = Convert.ToInt32(messageArray[0]);
                    String src = messageArray[1];
                    String txt = messageArray[2];
                
            }
            else
            {
                messageIsValid = false;
                throw new SystemException("Message is invalid. ParserGate, parseMessage");
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ANSWER" rule, which further applies the "OKAY", "DENY", "UNKNOWN" and "INVALID" rules.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ANSWER" rule</param>
        private void parseAnswer(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {               
                partOfMessage = partOfMessage.Substring(partOfMessage.IndexOf(":") + 1);
                partOfMessage = partOfMessage.Trim();
                String answer = partOfMessage;                
            }
            else
            {
                messageIsValid = false;
                throw new SystemException("Message is invalid. ParserGate, parseAnswer");
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "YOURID" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "YOURID" rule.</param>
        private void parseYourId(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                partOfMessage = this.deleteLines("begin:yourid", "end:yourid", partOfMessage);
                partOfMessage = partOfMessage.Trim();
                int id = Convert.ToInt32(partOfMessage);
            }
            else
            {
                messageIsValid = false;
                throw new SystemException("Message is invalid. ParserGate, parseYourId");
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "TIME" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "TIME" rule.</param>
        private void parseTime(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                partOfMessage = this.deleteLines("begin:time", "end:time", partOfMessage);
                partOfMessage = partOfMessage.Trim();
                long time = Convert.ToInt64(partOfMessage);
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ONLINE" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ONLINE" rule.</param>
        private void parseOnline(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                partOfMessage = this.deleteLines("begin:online", "end:online", partOfMessage);
                partOfMessage = partOfMessage.Trim();
                int online = Convert.ToInt32(partOfMessage);
            }
            else
            {
                messageIsValid = false;
                throw new SystemException("Message is invalid. ParserGate, parseOnline");
            }
            Contract.Ensures(messageIsValid);
        }

        /// <summary>
        /// Parses the message applying the "ENTITIES" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "ENTITIES" rule.</param>
        private List<Token> parseEntities(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                List<Token> tokenList = new List<Token>();
                partOfMessage = this.deleteLines("begin:ents", "end:ents", partOfMessage);
                while (partOfMessage.Contains("begin:player") && partOfMessage.Contains("end:player") || partOfMessage.Contains("begin:dragon") && partOfMessage.Contains("end:dragon"))
                {
                    if (partOfMessage.Contains("begin:player") && partOfMessage.Contains("end:player"))
                    {
                        partOfMessage = partOfMessage.Replace("begin:player", "#");
                        partOfMessage = partOfMessage.Replace("end:player", "#");
                    }
                    else if (partOfMessage.Contains("begin:dragon") && partOfMessage.Contains("end:dragon"))
                    {
                        partOfMessage = partOfMessage.Replace("begin:dragon", "#");
                        partOfMessage = partOfMessage.Replace("end:dragon", "#");
                    }
                }
                String[] entities = Regex.Split(partOfMessage, "#");
                ParserToken parserToken = new ParserToken(this, partOfMessage, this.messageIsValid, true);
                foreach (String s in entities)
                {
                    if (s.Contains("type:Player"))
                    {
                        tokenList.Add(parserToken.parsePlayer(s, true));
                    }
                    else if (s.Contains("type:Dragon"))
                    {
                        tokenList.Add(parserToken.parseDragon(s, true));
                    }
                }

                Contract.Ensures(messageIsValid);
                return tokenList;
                
            }
            else
            {
                messageIsValid = false;
                throw new SystemException("Message is invalid. ParserGate, parseEntities");
            }
            
        }

        /// <summary>
        /// Parses the message applying the "PLAYERS" rule.
        /// </summary>
        /// <param name="partOfMessage">Part of original message, is expected to fit the "PLAYERS" rule.</param>
        private List<Player> parsePlayers(String partOfMessage)
        {
            Contract.Requires(partOfMessage != null && messageIsValid);
            if (partOfMessage != null && messageIsValid)
            {
                if (partOfMessage.Contains("begin:players") && partOfMessage.Contains("end:players"))
                {
                    partOfMessage = this.deleteLines("begin:players", "end:players", partOfMessage);
                    if (partOfMessage.Contains("begin:player") && partOfMessage.Contains("end:player"))
                    {
                        partOfMessage = partOfMessage.Replace("begin:player", "#");
                        partOfMessage = partOfMessage.Replace("end:player", "#");
                        String[] players = Regex.Split(partOfMessage, "#");
                        List<Player> playersList = new List<Player>();
                        ParserToken parserToken = new ParserToken(this, partOfMessage, this.messageIsValid, true);
                        foreach (String s in players)
                        {
                            if (s.Contains("type:Player"))
                            {
                                playersList.Add(parserToken.parsePlayer(s, true));
                            }
                        }
                        Contract.Ensures(messageIsValid);
                        return playersList;
                    }
                    else
                    {
                        this.setMessageIsValid(false);
                        throw new SystemException("Message is invalid.0 the partOfMessage contain no begin:player and end:player. ParserGate, parsePlayer");
                    }
                }
                else
                {
                    this.setMessageIsValid(false);
                    throw new SystemException("Message is invalid. the partOfMessage contain no begin:player and end:player. ParserGate, parsePlayer");
                }
            }
            else
            {
                this.setMessageIsValid(false);
                throw new SystemException("Message is invalid. ParserGate, parsePlayer");
            }
            
        }

        private void parseServer(String message)
        {
            Contract.Requires(message != null && messageIsValid);
            if (message != null && messageIsValid)
            {
                message = this.deleteLines("begin:server", "end:server", message);
                int ver = Convert.ToInt32(message.Substring(message.IndexOf(":") + 1));
            }
            Contract.Ensures(messageIsValid);
        }


        /// <summary>
        /// After parsing the message, this method passes the extracted information on to methods required to process the information.
        /// </summary>
        /// <param name="toDo">Describes what the backend is supposed to do with the given object</param>
        /// <param name="value">An object creating from parsing buffer messages</param>
        private void passInformation(String toDo, Object value)
        {
            Contract.Requires(messageIsValid);
            Contract.Requires(toDo != null);
            Console.WriteLine("Object arrived in pass Information! - " + toDo);
            if (this.messageIsValid)
            {
                if (toDo.Equals("Players")&& value != null)
                {
                    if (value != null && value is List<Player>)
                    {
                        List<Player> players = (List<Player>)value;
                        foreach(Player p in players){
                            gameManager.storePlayer(p);
                        }
                    }
                }
                else if (toDo.Equals("CreateMap") && value != null)
                {
                    Console.Out.WriteLine("Is in Create Map");
                    if (value is Map)
                    {
                        Map map = (Map)value;
                        gameManager.setMap(map);
                        Gui gui = gameManager.getGui();
                        gui = new Gui();
                        gui.start();
                    }
                }
                else if (toDo.Equals("Update")&& value!=null)
                {
                    if (value is Token)
                    {
                        Token tok = (Token)value;
                        Token t = gameManager.findToken(tok);
                        
                        {
                            if (t != null)
                            {
                                t = tok;
                            }
                            else
                            {
                                if (tok.getType().Equals("Player")){
                                    gameManager.storePlayer((Player)tok);
                                }else{
                                    gameManager.storeDragon((Dragon)tok);
                                } 
                            }
                        }      
                        
                    }
                    else
                    {
                        //TODO
                    }
                   
                }
                else if(toDo.Equals("Delete") && value != null){
                    if (value is Token)
                    {
                        if (value != null)
                        {
                        Token tok = (Token)value;
                        
                            gameManager.deleteToken(tok);
                        }
                    }
                    else
                    {
                        if (value != null)
                        {
                            gameManager.deleteMap();
                        }
                    }
                }
                else if (value != null && toDo.Equals("Player"))
                {
                    if (value is Player)
                    {
                        Player player = (Player)value;
                        gameManager.storePlayer(player);
                    }
                }
                else if (toDo.Equals("Entities") && value != null)
                {
                    if (value is List<Token>)
                    {
                        List<Token> tokenList = (List<Token>)value;
                        foreach (Token t in tokenList)
                        {
                            if (t is Player)
                            {
                                Player p = (Player)t;
                                gameManager.storePlayer(p);
                            }
                            else
                            {
                                Dragon d = (Dragon)t;
                                gameManager.storeDragon(d);
                            }
                        }
                    }
                }
                else
                {
                    Console.Out.WriteLine("String was not recognized!");
                }
            }
            
        }

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
            Contract.Invariant(buffer != null);
        }
    }
}
