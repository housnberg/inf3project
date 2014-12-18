using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend
{
    interface IAmObservable<PlayerObserver>
    {

        public void addObserver(PlayerObserver observer);
        public void deleteObserver(PlayerObserver observer);
        public void deleteAllObservers();
        public void onIdChange();
        public void onIsBusyChange();
        public void onDescChange();
        public void onPosChange();
        public void onPointChange();
    }
}
