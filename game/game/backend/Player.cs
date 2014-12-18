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
            this.addObserver(new PlayerObserver()); //for test purpose
        }




        public new void setID(int id)
        {
            base.setID(id);
            this.IdChange();
        }

        public void setBusy(bool busy)
        {
            base.setBusy(busy);
            this.IsBusyChange();
        }

        public void setDesc(String desc)
        {
            base.setDesc(desc);
            this.DescChange();
        }

        public void setXPos(int xPos)
        {
            base.setXPos(xPos);
            this.PosChange();
        }

        public void setYPos(int yPos)
        {
            base.setYPos(yPos);
            this.PosChange();
        }

        public void setPoints(int points)
        {
            base.setPoints(points);
            this.PointChange();
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

        public void IdChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {
                        playerObserver.onIdChange();
                    }
                }
            }
        }

        public void IsBusyChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {
                        playerObserver.onIsBusyChange();
                    }
                }
            }
        }

        public void DescChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {
                        playerObserver.onDescChange();
                    }
                }
            }
        }

        public void PosChange()
        {
            if (this.observerList != null)
            {
                if (this.observerList.Count > 0)
                {
                    foreach (PlayerObserver playerObserver in this.observerList)
                    {
                        playerObserver.onPosChange();
                    }
                }
            }
        }

        public void PointChange()
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
