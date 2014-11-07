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
using game.client;

namespace game
{
    public class GameManager
    {
        private Map map;
        private List <Player> players;
        private List <Dragon> dragons;
        private int numberOfPlayers;
        private Connector connector;
        private static GameManager gameManager;
        private ParserGate parserGate;
        private ClientBuffer buffer;

        public GameManager(String ipAdress, UInt16 port)
        {

            //this.startGame(ipAdress, port);
        }

        private void startGame(String ipAdress, UInt16 port)
        {
            Contract.Requires(ipAdress != null);
            Contract.Requires(port != null);

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
            //connector.sendServerMessage(message);
        }

        /// <summary>
        /// Method for creating the Parser object and starting it in a thread.
        /// </summary>
        public void createParser()
        {
            
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
            Boolean isRemoved = false;

            foreach (Player p in players)
            {
                if (p.Equals(player))
                {
                    players.Remove(player);
                    isRemoved = true;
                }
            }
            if (isRemoved == false)
            {
                Console.Out.WriteLine("Der Spieler existiert nicht in dem Spiel und ist damit bereits gelöscht!");
                isRemoved = true;
            }
        }

        /// <summary>
        /// Removes a certain Player from the GameManager's ArrayList by searching for a Player's ID
        /// </summary>
        /// <param name="id">ID of the Player to be removed</param>
        public void deletePlayer(int id)
        {
            Contract.Requires(id != null);
            Contract.Requires(id > 0);
            Contract.Requires( id < 100);
            Boolean isRemoved = false;

            foreach (Player p in players)
            {
                if (p.getID() == id)
                {
                    players.Remove(p);
                }
            }
            if (isRemoved == false)
            {
                Console.Out.WriteLine("Der Spieler mit der ID '" + id + "'  existiert nicht in dem Spiel und ist damit bereits gelöscht!");
                isRemoved = true;
            }
            Contract.Ensures(isRemoved);
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
