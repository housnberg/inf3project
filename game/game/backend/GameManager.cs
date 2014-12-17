using game.Backend;
using game.client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.Parser;
using game.gui;
using System.Windows.Forms;
using System.Threading;
using game.backend;
using System.Runtime.InteropServices;
using System.IO;

namespace game
{
    public class GameManager
    {


        [DllImport("PathFinder.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr findPath(int from, int to, int[] map, int mapw, int maph, int plength);

        private int numberOfPlayers;
        private Map map;
        private Connector connector;
        private List <Player> players = new List<Player>();
        private List <Dragon> dragons = new List<Dragon>();
        private static GameManager gameManager;
        private Gui gui;
        private String ip;
        private UInt16 port;
        private Player thisPlayer;
        private PathWalker pwInstance = PathWalker.getPathWalkerInstance();
        
   
        /// <summary>
        /// constructor creates only one game instance
        /// </summary>
        /// <param name="ip">ip address of the server</param>
        /// <param name="port">port number of the server</param>
        public GameManager(String ip, UInt16 port)
        {
            try
            {
                if (gameManager != null)
                {
                    throw new SystemException("there is already a game running");
                }
                else
                {
                    this.ip = ip;
                    this.port = port;
                    GameManager.gameManager = this;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
 
        }

        public void setMap(Map map)
        {
            if (map != null)
            {
                this.map = map;
            }
            else
            {
                throw new ArgumentException("Passed Map is null(GameManager.SetMap)");
            }
        }

        public int[] getOneDimensionalMap()
        {
            int[] oneDimensionalMap = new int[map.getWidth() * map.getHeight()];
            if (map != null)
            {
                int counter = 0;
                Field[,] fields = map.getFields();
                for (int row = 0; row < map.getHeight(); row++)
                {
                    for (int col = 0; col < map.getWidth(); col++)
                    {
                        if (fields[row, col].isWalkable())
                        {
                            oneDimensionalMap[counter] = 1;
                        }
                        else
                        {
                            oneDimensionalMap[counter] = 0;
                        }
                        counter++;
                    }
                }
            }
            return oneDimensionalMap;
            
        }

        /// <summary>
        /// Creates a default Game with a default Map and default Entities
        /// </summary>
        private void createDefaultGame()
        {
            createDefaultEntities();
        }

        /// <summary>
        /// Creates default entities, called by createDefaultGame()
        /// </summary>
        private void createDefaultEntities()
        {
            Dragon dragon = new Dragon(3, false, "Dragon 3", 5, 5);
            //Player playerOne = new Player(5, 1);
            //Player playerTwo = new Player(4, 1);

            //playerOne.setID(1);
            //playerTwo.setID(2);

            dragons.Add(dragon);
            //players.Add(playerOne);
            //players.Add(playerTwo);
        }

        /// <summary>
        /// Creates default Map with a WALL-Border and WALKABLE-Fields in the middle
        /// </summary>
        /// <returns>Default Map to illustrate on the Frontend</returns>
        private Map createDefaultMap()
        {
            Map map = new Map(10, 10);
            this.map = map;
            Field f;
            List<FieldType> attributes = new List<FieldType>();
            for (int i = 0; i < map.getHeight(); i++)
            {
                for (int j = 0; j < map.getWidth(); j++)
                {
                    attributes = new List<FieldType>();
                    if (i == 0 || j == 0 || i == map.getHeight() - 1 || j == map.getWidth() - 1)
                    {
                        attributes.Add(FieldType.WALL);
                        f = new Field(i, j, attributes);
                        map.setField(f);
                    }
                    else
                    {
                        attributes.Add(FieldType.WALKABLE);
                        f = new Field(i, j, attributes);
                        map.setField(f);
                    }
                }
            }
            return map;
        }

        /// <summary>
        /// Starts the Game by creating a new Connector
        /// </summary>
        /// <param name="ip">IP-Adress the Client wannts to connect to</param>
        /// <param name="port">Port Number of the IP-Adress</param>
        public void startGame()
        {
            connector = new Connector(this.ip, this.port);
            ParserGate parser = new ParserGate();
            Thread parserThread = new Thread(parser.extractMessage);
            parserThread.Start();
            gui = new Gui();
            this.sendCommand("get:map");
            this.sendCommand("get:ents");
            this.sendCommand("get:me");
           
        }

         /// <summary>
         /// Sends a message to the Connector to perform a certain action
         /// </summary>
         /// <param name="message">The message being sent to the server via the connector</param>
        public void sendCommand(String message){
            Contract.Requires(message != null);
            Contract.Requires(connector != null);
            try
            {
                switch (message)
                {
                    case "lft":
                        connector.sendServerMessage("ask:mv:lft");
                        break;
                    case "rgt":
                        connector.sendServerMessage("ask:mv:rgt");
                        break;
                    case "up":
                        connector.sendServerMessage("ask:mv:up");
                        break;
                    case "dwn":
                        connector.sendServerMessage("ask:mv:dwn");
                        break;
                    default:
                        connector.sendServerMessage(message);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Sends a message to the Connector to perform a certain action
        /// </summary>
        /// <param name="message">The message being sent to the server via the connector</param>
        public void sendMessage(String message)
        {
            Contract.Requires(message != null);
            Contract.Requires(connector != null);
            try
            {
                connector.sendServerMessage("ask:say:" + message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// this method can be invoked by the parser.
        /// it appends newly received chat-messages to the gui
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public void updateChatMessage(String source, String message)
        {
            gui.appendChatMessage(source, message);
        } 

        /// <summary>
        /// Adds a Player to the GameManager's ArrayList
        /// </summary>
        /// <param name="player">Player to be added</param>
        public void storePlayer(Player player) 
        {
            Contract.Requires(player != null);
            players.Add(player);
            numberOfPlayers++;
        }

        /// <summary>
        /// Removes a certain Player from the GameManager's ArrayList
        /// </summary>
        /// <param name="player">Player to be removed</param>
        public void deletePlayer(Player player)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }
            else
            {
                Console.Out.WriteLine("The Player does not exist, probably he has already been deleted!");
            }
        }

        public Token findToken(Token token)
        {
            Token wanted = null;
            if (token is Player)
            {
                foreach (Player p in players)
                {
                    if (p.getID() == token.getID())
                    {
                        wanted = p;
                        break;
                    }
                }
            }
            else
            {
                foreach (Dragon d in dragons)
                {
                    if (d.getID() == token.getID())
                    {
                        wanted = d;
                        break;
                    }
                }
            }
            return wanted;
            
        }

        public void storeDragon(Dragon d)
        {
            dragons.Add(d);
        }

        /// <summary>
        /// Removes a certain Player from the GameManager's ArrayList by searching for a Player's ID
        /// </summary>
        /// <param name="id">ID of the Player to be removed</param>
        public void deletePlayer(int id)
        {
            Contract.Requires(id > 0);
            Boolean found = false;
            int counter = 0;
            Player player = null;

            try
            {

                if (id <= 0)
                {
                    throw new ArgumentException("The ID " + id + " is not valid! Only IDs > 0 are allowed!");
                }
               
                do
                {
                    player = players.ElementAt(counter);
                    if (player.getID() == id)
                    {
                        found = true;
                        players.Remove(player);
                    }
                    else
                    {
                        counter++;
                    }
                    

                } while (found != true);

                if (found == false)
                {
                    throw new ArgumentException("The Player with the ID '" + id + "'  does not exist in this game, for he may have been already deleted!");

                }
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
            }
           
            
        }

        /// <summary>
        /// Removes a certain Token from the GameManager's ArrayList by searching for the specific Object
        /// </summary>
        /// <param name="token">Token to be removed from the game</param>
        public void deleteToken(Token token)
        {
            Contract.Requires(token != null);

        }

        /// <summary>
        /// Begins a certain minigame with two different players
        /// </summary>
        /// <param name="playerOne">Player One taking part in the minigame</param>
        /// <param name="playerTwo">Player Two taking part in the minigame</param>
        /// <param name="minigame">Ceratin Minigame to be started</param>
        public void beginChallenge(Minigame minigame, Player playerOne, Player playerTwo)
        {
            Contract.Requires(minigame != null);
            Contract.Requires(playerOne != null);
            Contract.Requires(playerTwo != null);
            Contract.Requires(!(playerOne.getBusy()));
            Contract.Requires(!(playerTwo.getBusy()));
        }

        /// <summary>
        /// Forces the Map to reload
        /// </summary>
        /// <param name="map">Map of the game</param>
        public void forceReload(Map map)
        {
            Contract.Requires(map != null);
        }

        public void deleteMap()
        {
            this.map = null;
        }

        /// <summary>
        /// Returns the number of Players participating in the game
        /// </summary>
        public int getNumberOfPlayers()
        {
            return numberOfPlayers;
        }

        public Map getMap()
        {
            return this.map;
        }
      
        public int getMapHeight()
        {
            return this.map.getHeight();
        }

        public int getMapWidth()
        {
            return this.map.getWidth();
        }

        public static GameManager getGameManagerInstance()
        {
            return GameManager.gameManager;
        }


        public List<Player> getPlayers()
        {
            return this.players;
        }

        public List<Dragon> getDragons()
        {
            return this.dragons;
        }

        public Connector getConnector()
        {
            return this.connector;
        }

        public Gui getGui()
        {
            return gui;
        }

        public void replaceToken(Token token)
        {
            Boolean found = false;
            int count = 0;
            if (token is Player)
            {
                Player player = (Player)token;
                while (found != true)
                {
                    if (count == players.Count)
                    {
                        this.storePlayer(player);
                        found = true;
                    }
                    else if (players.Count > 0 && players[count].getID() == player.getID())
                    {
                        //players.Remove(players[count]);
                        //players.Add(player);
                        //players[count] = (Player)token;
                        players[count].setXPos(player.getXPos());
                        players[count].setYPos(player.getYPos());
                        found = true;
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            else
            {
                Dragon dragon = (Dragon)token;
                while (found != true)
                {
                    if (count == dragons.Count)
                    {
                        this.storeDragon(dragon);
                        found = true;
                    }
                    else if (dragons.Count > 0 && dragons[count].getID() == dragon.getID())
                    {
                        //dragons.Remove(dragons[count]);
                        //dragons.Add(dragon);
                        //found = true;
                        //dragons[count] = dragon;
                        dragons[count].setXPos(dragon.getXPos());
                        dragons[count].setYPos(dragon.getYPos());
                        found = true;
                    }
                    else
                    {
                        count++;
                    }
                }   
            }
  
        }

        /// <summary>
        /// Forces the GUI to repaint
        /// </summary>
        public void refreshGui()
        {
           if (gui != null)
           {
               lock (gui)
               {
                   gui.Invoke(gui.myDelegate);
               }

            }

        }

        public void takePath(int row, int col)
        {
            try
            {
                int[] path = new int[32];
                Console.WriteLine("Found DLL: " + File.Exists("PathFinder.dll"));
                IntPtr pointer = findPath(1, 11, path, map.getWidth(), map.getHeight(), 32);
                Marshal.Copy(pointer, path, 0, path.Length);
                pwInstance.setPath(path);
                pwInstance.setMapWidth(map.getWidth());
                pwInstance.walk();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void startGui()
        {
            gui = new Gui();
            Thread t = new Thread(gui.start);
            t.Start();
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.players != null);
            Contract.Invariant(this.connector != null);
        }


        public void drawMessage(String message)
        {
            gui.appendChatMessage(message);
        }

        public void setThisPlayer(Player player)
        {
            Boolean found = false;
            int count = 0;
                while (found != true)
                {
                    if (count == players.Count)
                    {
                        found = true;
                    }
                    else if (players.Count > 0 && players[count].getID() == player.getID())
                    {
                        this.thisPlayer = players[count];
                        found = true;
                    }
                    else
                    {
                        count++;
                    }
              }    
        }
    }
}
