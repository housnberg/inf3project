using System;
using System.Collections.Generic;
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

        public int getXPos()
        {
            return this.yPos;
        }

        public String getDesc()
        {
            return this.desc;
        }

        public void setXPos(int xPos)
        {
            if (xPos < 0 || xPos > GameManager.height)
            {
                throw new  Exception ("Falsche X-Koordinate für die Figur! Der Wert darf nicht kleiner als 0 und  nicht größer als " + GameManager.height + " sein!");
            }
            this.xPos = xPos;
        }

        public void setYPos(int yPos)
        {
            if (yPos < 0 || yPos > GameManager.width)
            {
                throw new  Exception ("Falsche Y-Koordinate für die Figur! Der Wert darf nicht kleiner als 0 und  nicht größer als " + GameManager.width + " sein!");
            }
            this.yPos = yPos;
        }
    }
}
