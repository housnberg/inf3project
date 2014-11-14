using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    /// <summary>
    /// Interface that should be implemented by your class that represents one field in the grid.
    /// It has to provide methods to check for its properties, which are independed from the way you actually store those properties.
    /// </summary>
    public interface ITile : IPositionable
    {
        bool isWalkable();
        bool isForest();
        bool isHuntable();
        bool isWater();
    }
}
