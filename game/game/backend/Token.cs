using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    abstract class Token
    {
        private int id;
        private String desc;
        private Boolean busy;
        private String type;
        private int xPos;
        private int yPos;

        public Token()
        {

        }

        /// <summary>
        /// Sets an ID for this Token
        /// </summary>
        /// <param name="id">The ID of the Token</param>
        private void setID(int id)
        {
            Contract.Requires(id > 0);
            Contract.Requires(id < 100);
            if (id < 0 || id > 100)
            {
                throw new ArgumentException("The ID for a Token needs to be greater than 0 and smaller than 100");
            }
            this.id = id;
        }

        public int getID()
        {
            return this.id;
        }

        public Boolean getBusy()
        {
            return this.busy;
        }

        public int getXPos()
        {
            return this.xPos;
        }

        public int getYPos()
        {
            return this.xPos;
        }

        public String getDesc()
        {
            return desc;
        }

        /// <summary>
        /// Sets a token to busy state or undoes it
        /// </summary>
        /// <param name="busy">Boolean variable wether the player needs to be busy or not</param>
        public void setBusy(Boolean busy)
        {
            if (this.busy == true && busy == true)
            {
                throw new ArgumentException("The Token is already in busy mode!");
            }

            if (this.busy == false && busy == false)
            {
                throw new ArgumentException("The Token is already not in busy mode!");
            }

            this.busy = busy;
            
        }

        public void setXPos(int xPos)
        {
            Contract.Requires(xPos >= 0 && xPos < GameManager.getGameManagerInstance().getMapHeight());

            this.xPos = xPos;
        }

        public void setYPos(int yPos)
        {
            Contract.Requires(yPos >= 0 && yPos < GameManager.getGameManagerInstance().getMapWidth());

            this.yPos = yPos;
        }
    }
}
