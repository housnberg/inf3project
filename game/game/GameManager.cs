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

        public void startGame()
        {

        }

         public int getNumberOfPlayers()
         {
             return numberOfPlayer;
         }

         public void setNumberOfPlayers()
         {

         }

         /// <summary>
         /// Sends a message to the Connector to perform a certain action
         /// </summary>
         /// <param name="connector">The actual game connector</param>
         /// <param name="message">The message being sent to the server via the connector</param>
        public void sendCommand(Connector connector,String message){
            connector.sendServerMessage(message);
        }
        
    }
}
