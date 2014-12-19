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
        [DllImport("PathFinder.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void freeArray(IntPtr pointer);

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
        private PathWalker pwInstance;
        
   
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
                    pwInstance = PathWalker.getPathWalkerInstance();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
 
        }

        /// <summary>
        /// Sets Map Attribute
        /// </summary>
        /// <param name="map">Map to be set the 'Game Map'</param>
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

        /// <summary>
        /// Converts the 2D-Map to a 1D Map
        /// </summary>
        /// <returns>int[] Map illustrated as an 1D Array</returns>
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
                        pwInstance.stopWalking();
                        break;
                    case "rgt":
                        connector.sendServerMessage("ask:mv:rgt");
                        pwInstance.stopWalking();
                        break;
                    case "up":
                        connector.sendServerMessage("ask:mv:up");
                        pwInstance.stopWalking();
                        break;
                    case "dwn":
                        connector.sendServerMessage("ask:mv:dwn");
                        pwInstance.stopWalking();
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

        /// <summary>
        /// Adds the dragon to the game
        /// </summary>
        /// <param name="dragon">Dragon to be stored in the 'dragons'-List</param>
        public void storeDragon(Dragon dragon)
        {
            dragons.Add(dragon);
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

        /// <summary>
        /// Deletes the current Map of the game. Called by the Parser.
        /// </summary>
        public void deleteMap()
        {
            this.map = null;
        }

        /// <summary>
        /// Returns the number of Players participating in the game
        /// </summary>
        /// <returns>Integer-Number of players participatinh in the game</returns>
        public int getNumberOfPlayers()
        {
            return numberOfPlayers;
        }

        /// <summary>
        /// Returns the Map of the game
        /// </summary>
        /// <returns>Map-Object which is the Map of the game</returns>
        public Map getMap()
        {
            return this.map;
        }
      
        /// <summary>
        /// Returns the Height of the Map
        /// </summary>
        /// <returns>Integer value representing the Height of the Map</returns>
        public int getMapHeight()
        {
            return this.map.getHeight();
        }

        /// <summary>
        /// Returns the Width of the Map
        /// </summary>
        /// <returns>Integer value representing the Width of the Map</returns>
        public int getMapWidth()
        {
            return this.map.getWidth();
        }

        /// <summary>
        /// Retuns the specific GameManagerObject; Adapted Singleton Architecture
        /// </summary>
        /// <returns>Singleton of GameManager</returns>
        public static GameManager getGameManagerInstance()
        {
            return GameManager.gameManager;
        }

        /// <summary>
        /// Returns the List of the Players participating in the game
        /// </summary>
        /// <returns>Players-List of the game</returns>
        public List<Player> getPlayers()
        {
            return this.players;
        }

        /// <summary>
        /// Returns the List of the Dragons participating in the game
        /// </summary>
        /// <returns>Dragons-List of the game</returns>
        public List<Dragon> getDragons()
        {
            return this.dragons;
        }

        /// <summary>
        /// Searches for a Token in this game. Modifies x/y Coordinates if Token already exists, else a new Token is stored; called by the Parser
        /// </summary>
        /// <param name="token">Token to be either modified or stored</param>
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
                        if (players[count].getXPos() != player.getXPos())
                        {
                            players[count].setXPos(player.getXPos());
                        }
                        else if (players[count].getYPos() != player.getYPos())
                        {
                           players[count].setYPos(player.getYPos());
                        }                       
                       
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

        /// <summary>
        /// will be called by the gui when the user clicks on a map cell.
        /// this method uses the PathFinder.dll
        /// </summary>
        /// <param name="row">row of the clicked cell</param>
        /// <param name="col">column of the clicked cell</param>
        public void takePath(int row, int col)
        {
            try
            {
                if (!pwInstance.isWalking()) {
                    int pathLength = map.getWidth() * map.getHeight() / 4;
                    int[] path = new int[pathLength];
                    int[] oneDimensionalMap = this.getOneDimensionalMap();
                    int from = coordinateToPoint(thisPlayer.getYPos(), thisPlayer.getXPos());
                    int to = coordinateToPoint(row, col);
                    IntPtr pointer = findPath(from, to, oneDimensionalMap, map.getWidth(), map.getHeight(), path.Length);
                    Marshal.Copy(pointer, path, 0, path.Length);
                    pwInstance.setPath(path);
                    pwInstance.walk(thisPlayer.getYPos(), thisPlayer.getXPos());
                }
            }
            catch (Exception e)
            {
                pwInstance.stopWalking();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// converts a two dimensioal coordinate to a one dimensional point
        /// </summary>
        /// <param name="row">row of the coordinate</param>
        /// <param name="col">column of the coordinate</param>
        /// <returns>converted coordinate</returns>
        private int coordinateToPoint(int row, int col) {
            return (row * map.getWidth() + col);
        }

        /// <summary>
        /// converts a two dimensional coordinate for a given point
        /// </summary>
        /// <param name="point">1 dimensional point</param>
        /// <param name="mapWidth">width of the map</param>
        /// <returns>array of the 2 dimensional coordinate (coord[0]=col, coord[1]=row)</returns>
        public int[] pointToCoordinate(int point)
        {
            int[] coord = new int[2];
            coord[1] = point % map.getWidth();
            coord[0] = point / map.getWidth();
            return coord;
        }

        /// <summary>
        /// Starts the Gui
        /// </summary>
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

        /// <summary>
        /// Calls GUI Function that appends the Message to the Text box
        /// </summary>
        /// <param name="message">Message to be appended on the Text Box</param>
        public void drawMessage(String message)
        {
            gui.appendChatMessage(message);
        }

        /// <summary>
        /// Sets the client started the game to thisPlayer
        /// </summary>
        /// <param name="player">Player-Representation of the client</param>
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

        /// <summary>
        /// Returns this Player
        /// </summary>
        /// <returns>Returns this Player</returns>
        public Player getThisPlayer()
        {
            return thisPlayer;
        }
    }
}
