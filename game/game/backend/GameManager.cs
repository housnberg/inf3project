using game.Backend;
using game.client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class GameManager
    {
        private Map map;
        private ArrayList players;
        private ArrayList dragons;
        private int numberOfPlayer;
        private Connector connector;
        public static int height;
        public static int width;

        public void startGame(String ipAdress, UInt16 port)
        {
            Contract.Requires(ipAdress != null);
            Contract.Requires(port != null);

            players = new ArrayList();
            dragons = new ArrayList();
            connector = new Connector(ipAdress, port);

        }

         /// <summary>
         /// Sends a message to the Connector to perform a certain action
         /// </summary>
         /// <param name="connector">The actual game connector</param>
         /// <param name="message">The message being sent to the server via the connector</param>
        public void sendCommand(String message){

            Contract.Requires(message != null);
            connector.sendServerMessage(message);
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
            Contract.Requires(player != null);
            Boolean isRemoved = false;

            foreach (Player p in players)
            {
                if (p.Equals(player))
                {
                    players.Remove(player);
                    isRemoved = true;
                    break;
                }
            }
            if (isRemoved == false)
            {
                Console.Out.WriteLine("Der Spieler existiert nicht in dem Spiel und ist damit bereits gelöscht!");
                isRemoved = true;
            }
            Contract.Ensures(isRemoved);
        }

        /// <summary>
        /// Removes a certain Player from the GameManager's ArrayList by searching for a Player's ID
        /// </summary>
        /// <param name="id">ID of the Player to be removed</param>
        public void deletePlayer(int id)
        {
            Contract.Requires(id != null && id > 0 && id < 100);
            Boolean isRemoved = false;

            foreach (Player p in players)
            {
                if (p.getID() == id)
                {
                    players.Remove(p);
                    break;
                }
            }
            if (isRemoved == false)
            {
                Console.Out.WriteLine("Der Spieler mit der ID '" + id + "'  existiert nicht in dem Spiel und ist damit bereits gelöscht!");
                isRemoved = true;
            }
            Contract.Ensures(isRemoved);
        }

        public void deleteToken(Token token)
        {
            Contract.Requires(token != null);

        }

        public void beginChallenge (Minigame minigame, Player playerOne, Player playerTwo)
        {
            Contract.Requires(minigame != null && playerOne != null && playerTwo != null);
            Contract.Requires(!(playerOne.getBusy()) && !(playerTwo.getBusy()));
        }

        public void forceReload(Map map)
        {
            Contract.Requires(map != null);
        }

        public int getNumberOfPlayers()
        {
            return numberOfPlayer;
        }

        public Map getMap()
        {
            return this.map;
        }
       
    }
}
