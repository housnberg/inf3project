using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.backend;

namespace game
{

    public class Player : Token, IAmObservable<PlayerObserver>
    {

        List<PlayerObserver> observerList = new List<PlayerObserver>();

        public Player(int id, Boolean isBusy, String desc, int x, int y, int points)
            : base(id, isBusy, desc, x, y, points)
        {
            this.setType("Player");
        }




        public override void setID(int id)
        {
            base.setID(id);
            this.onIdChange();
        }

        public override void setBusy(bool busy)
        {
            base.setBusy(busy);
            this.onIsBusyChange();
        }

        public override void setDesc(String desc)
        {
            base.setDesc(desc);
            this.onDescChange();
        }

        public override void setXPos(int xPos)
        {
            base.setXPos(xPos);
            this.onPosChange();
        }

        public override void setYPos(int yPos)
        {
            base.setYPos(yPos);
            this.onPosChange();
        }

        public override void setPoints(int points)
        {
            base.setPoints(points);
            this.onPointChange();
        }

        public void addObserver(PlayerObserver observer)
        {
            if (observer != null)
            {
                observerList.Add(observer);
            }

        }

        public void deleteObserver(PlayerObserver observer)
        {
            if (observer != null)
            {
                if (observerList.Contains(observer))
                {
                    observerList.Remove(observer);
                }
            }
        }

        public void deleteAllObservers()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    observerList.Clear();
                }
            }
        }

        public void onIdChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {

                    }
                }
            }
        }

        public void onIsBusyChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {

                    }
                }
            }
        }

        public void onDescChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {

                    }
                }
            }
        }

        public void onPosChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {

                    }
                }
            }
        }

        public void onPointChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {

                    }
                }
            }
        }



    }



}
