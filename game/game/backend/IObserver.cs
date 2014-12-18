using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend
{
    interface IObserver
    {
        void onIdChange();
        void onIsBusyChange();
        void onDescChange();
        void onPosChange();
        void onPointChange();
    }
}
