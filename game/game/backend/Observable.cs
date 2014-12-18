using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend
{
    interface Observable
    {
        List<Observer> observerList;

        public void addObserver();
        public void deleteObserver();
        public void upDate();
    }
}
