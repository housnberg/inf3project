using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    /// <summary>
    /// Interface your positionable objects should implement. Such as the dragons and players.
    /// This enables the frontend to determine the current position of said objects to render them at the correct space.
    /// </summary>
    public interface IPositionable
    {
        /// <summary>
        /// Getter for the x-position
        /// </summary>
        /// <returns>the x-position</returns>
        int getXPosition();
        /// <summary>
        /// Getter for the y-position
        /// </summary>
        /// <returns>the y-position</returns>
        int getYPosition();
    }
}
