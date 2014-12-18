using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend
{
    interface Observer
    {
        Player observedPlayer;
        public void getUpdate();
    }
}
