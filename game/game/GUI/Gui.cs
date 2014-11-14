using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Gui
    {
        public Gui()
        {

        }

        /// <summary>
        /// Forces GUI to reload
        /// </summary>
        /// <param name="map">Map of the game</param>
        public void reloadGui(Map map)
        {
            Contract.Requires(map != null);
        }
    }
}
