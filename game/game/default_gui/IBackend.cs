using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    /// <summary>
    /// Interface that should be implemented by the class that represents your backend.
    /// </summary>
    public interface IBackend
    {
        /// <summary>
        /// Method to get an arbitrary collection of dragons
        /// </summary>
        /// <returns>a list of dragons that are currently on the map</returns>
        List<IPositionable> getDragons();
        /// <summary>
        /// Method to get an arbitrary collection of players
        /// </summary>
        /// <returns>list of players that are currently on the map</returns>
        List<IPositionable> getPlayers();
        /// <summary>
        /// Method to get a 2d-grid-representation of the map. The map doesn't actually has to be a 2d-array, but you should
        /// somehow be able to convert it into one.
        /// </summary>
        /// <returns>a 2d-array of ITiles, representing the map</returns>
        ITile[][] getMap();
        /// <summary>
        /// Sends a command to the server (such as ask:mv:dwn)
        /// </summary>
        /// <param name="command">the command to send</param>
        void sendCommand(string command);
        /// <summary>
        /// Sends a chatmessage to broadcast. This is basically any text, wrapped in the chat-command. 
        /// So this method will ultimately call sendCommand() after forming a chat-command from the text.
        /// </summary>
        /// <param name="message">the text to send as chatmessage</param>
        void sendChat(string message);
    }
}
