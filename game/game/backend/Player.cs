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



        /// <summary>
        /// New setter, calls the IdChange method.
        /// </summary>
        /// <param name="id">the new id</param>
        public void setID(int id)
        {
            base.setID(id);
            this.IdChange();
        }

        /// <summary>
        /// New setter, calls the IsBusyChange method.
        /// </summary>
        /// <param name="busy">the new busy status</param>
        public void setBusy(bool busy)
        {
            base.setBusy(busy);
            this.IsBusyChange();
        }

        /// <summary>
        /// New setter, calls the DescChange() method at the end.
        /// </summary>
        /// <param name="desc">the new description</param>
        public void setDesc(String desc)
        {
            base.setDesc(desc);
            this.DescChange();
        }

        /// <summary>
        /// New setter, calls the PosChange() method at the end.
        /// </summary>
        /// <param name="xPos">new x-Position</param>
        public void setXPos(int xPos)
        {
            base.setXPos(xPos);
            this.PosChange();
        }

        /// <summary>
        /// New setter, calls the PosChange() method at the end.
        /// </summary>
        /// <param name="yPos">the new y-Position</param>
        public void setYPos(int yPos)
        {
            base.setYPos(yPos);
            this.PosChange();
        }

        /// <summary>
        /// New setter, calls the PointChange() method at the end.
        /// </summary>
        /// <param name="points">the new points</param>
        public void setPoints(int points)
        {
            base.setPoints(points);
            this.PointChange();
        }

        /// <summary>
        /// Registers a PlayerObserver object.
        /// </summary>
        /// <param name="observer">the PlayerObserver object.</param>
        public void addObserver(PlayerObserver observer)
        {
            if (observer != null)
            {
                observerList.Add(observer);
            }

        }

        /// <summary>
        /// Delets a single PlayerObserver object.
        /// </summary>
        /// <param name="observer">The PlayerObserver object to be deleted.</param>
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

        /// <summary>
        /// Deletes all PlayerObserver objects.
        /// </summary>
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

        /// <summary>
        /// Notifies the observer about a new Player id.
        /// </summary>
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

        /// <summary>
        /// Notifies the observer about a new busy status.
        /// </summary>
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

        /// <summary>
        /// Notifies the observer about a new player description.
        /// </summary>
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

        /// <summary>
        /// Notifies the Observer about a change in player position.
        /// </summary>
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

        /// <summary>
        /// Notifies the observer about a change in points.
        /// </summary>
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
