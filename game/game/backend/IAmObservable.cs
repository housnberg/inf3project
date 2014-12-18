using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend
{
    interface IAmObservable<PlayerObserver>
    {

        void addObserver(PlayerObserver observer);
        void deleteObserver(PlayerObserver observer);
        void deleteAllObservers();
        void IdChange();
        void IsBusyChange();
        void DescChange();
        void PosChange();
        void PointChange();
    }
}
