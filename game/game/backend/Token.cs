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

        

        public void setXPos(int xPos)
        {
            Contract.Requires(xPos >= 0 && xPos < GameManager.height);

            this.xPos = xPos;
        }

        public void setYPos(int yPos)
        {
            Contract.Requires(yPos >= 0 && yPos < GameManager.width);

            this.yPos = yPos;
        }
    }
}
