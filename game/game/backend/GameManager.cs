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

namespace game
{
    public class GameManager
    {
        private int numberOfPlayers;
        private Map map;
        private Connector connector;
        private List <Player> players = new List<Player>();
        private List <Dragon> dragons = new List<Dragon>();
        private static GameManager gameManager;
        private Gui gui;
   
        /// <summary>
        /// constructor creates only one game instance
        /// </summary>
        /// <param name="ip">ip address of the server</param>
        /// <param name="port">port number of the server</param>
        public GameManager(String ip, UInt16 port)
        {
            //try
            //{
                if (gameManager != null)
                {
                    throw new SystemException("there is already a game running");
                }
                else
                {
                    startGame(ip, port);
                    GameManager.gameManager = this;
                    createDefaultGame();
                    gui = new Gui();
                    Application.Run(gui);
            
                }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
 
        }

        /// <summary>
        /// Creates a default Game with a default Map and default Entities
        /// </summary>
        private void createDefaultGame()
        {
            this.map = createDefaultMap();
            createDefaultEntities();
        }

        /// <summary>
        /// Creates default entities, called by createDefaultGame()
        /// </summary>
        private void createDefaultEntities()
        {
            Dragon dragon = new Dragon(3, 3);
            Player playerOne = new Player(5, 1);
            Player playerTwo = new Player(4, 1);

            playerOne.setID(1);
            playerTwo.setID(2);

            dragons.Add(dragon);
            players.Add(playerOne);
            players.Add(playerTwo);
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
        private void startGame(String ip, UInt16 port)
        {
            connector = new Connector(ip, port);
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
                connector.sendServerMessage(message);
                Console.WriteLine("------->Message Send: " + message);
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
        /// Removes a certain Player from the GameManager's ArrayList by searching for a Player's ID
        /// </summary>
        /// <param name="id">ID of the Player to be removed</param>
        public void deletePlayer(int id)
        {
            Contract.Requires(id > 0);
            Boolean found = false;
            int counter = 0;
            Player player = null;

            //try{

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
            //}
            //catch (Exception exception)
            //{
            //    Console.Out.WriteLine(exception.Message);
            //}
           
            
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

        /// <summary>
        /// Forces the GUI to repaint
        /// </summary>
        public void refreshGui()
        {
            gui.Refresh();
        }

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.players != null);
            Contract.Invariant(this.connector != null);
        }
    }
}
