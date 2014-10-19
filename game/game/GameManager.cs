using game.client;
using System;
using System.Collections;
using System.Collections.Generic;
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
            connector.sendServerMessage(message);
        }

        /// <summary>
        /// Adds a Player to the GameManager's ArrayList
        /// </summary>
        /// <param name="player">Player to be added</param>
        public void storePlayer(Player player)
        {
            if (player == null)
            {
                throw new Exception("Ungültige Wertübergabe. Ein Spieler kann nicht 'NULL' sein!");
            }
            players.Add(player);
        }

        /// <summary>
        /// Removes a certain Player from the GameManager's ArrayList
        /// </summary>
        /// <param name="player">Player to be removed</param>
        public void deletePlayer(Player player)
        {
            if (player == null)
            {
                throw new Exception("Ungültige Wertübergabe. Ein Spieler kann nicht 'NULL' sein!");
            }
            foreach (Player p in players)
            {
                if (p.Equals(player))
                {
                    players.Remove(player);
                    break;
                }
            }
            
        }

        /// <summary>
        /// Removes a certain Player from the GameManager's ArrayList by searching for a Player's ID
        /// </summary>
        /// <param name="id">ID of the Player to be removed</param>
        public void deletePlayer(int id)
        {
            if (id < 0)
            {
                throw new Exception("Spieler-ID kann nicht kleiner als 0 sein!");
            }
            foreach (Player p in players)
            {
                if (p.getID() == id)
                {
                    players.Remove(p);
                    break;
                }
            
            }

        }

        public int getNumberOfPlayers()
        {
            return numberOfPlayer;
        }

        public void setNumberOfPlayers()
        {

        }
    }
}
