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

namespace game
{
    public class GameManager
    {
        private int numberOfPlayers;
        private Map map;
        private Connector connector;
        private List <Player> players = new List<Player>();
        private List <Dragon> dragons = new List<Dragon>();
        private ClientBuffer buffer;
        private static GameManager gameManager = new GameManager();
   
        public GameManager()
        {
            this.startGame();
        }

        private void startGame()
        {
            

            //players = new ArrayList();
            //dragons = new ArrayList();
            //connector = new Connector(ipAdress, port);

        }

         /// <summary>
         /// Sends a message to the Connector to perform a certain action
         /// </summary>
         /// <param name="connector">The actual game connector</param>
         /// <param name="message">The message being sent to the server via the connector</param>
        public void sendCommand(String message){

            Contract.Requires(message != null);
            Contract.Requires(connector != null);
            //connector.sendServerMessage(message);
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
                Console.Out.WriteLine("Der Spieler existiert nicht in dem Spiel und ist damit bereits gelöscht!");
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

            try{

                if (id <= 0)
                {
                    throw new ArgumentException("Die ID " + id + " ist nich gültig! Es sind nur IDs > 0 elaubt sein!");
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
                    throw new ArgumentException("Der Spieler mit der ID '" + id + "'  existiert nicht in dem Spiel und ist damit bereits gelöscht!");

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
      

        [ContractInvariantMethod]
        protected void ObjectInvariant()
        {
            Contract.Invariant(this.players != null);
            Contract.Invariant(this.connector != null);
        }

        public int getMapHeight()
        {
            if (this.map != null)
            {
                return this.map.getHeight();
            }
            else
            {
                return -1;
            }
        }

        public int getMapWidth()
        {
            if (this.map != null)
            {
                return this.map.getWidth();
            }
            else
            {
                return -1;
            }
        }

        public static GameManager getGameManagerInstance()
        {
            return GameManager.gameManager;
        }
       
    }
}
