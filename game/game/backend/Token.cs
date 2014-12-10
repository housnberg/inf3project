using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public abstract class Token
    {
        private int id;
        private int xPos;
        private int yPos;
        private int points;
        private String desc;
        private String type;
        private Boolean busy;

        public Token(int id, Boolean isBusy, String desc, int x, int y, int points)
        {
            this.setID(id);
            this.setBusy(isBusy);
            this.setDesc(desc);
            this.setXPos(x);
            this.setYPos(y);
            this.setPoints(points);
        }

        public Token(int id, Boolean isBusy, String desc, int x, int y)
        {
            this.setID(id);
            this.setBusy(isBusy);
            this.setDesc(desc);
            this.setXPos(x);
            this.setYPos(y);
        }

        public override String ToString()
        {
            String data = "";
            data+= desc + " " +  ", X: " + xPos + ", Y: " + yPos + ", ID: " + id;
            return data;
        }

        public void setType(String type)
        {
            this.type = type;
        }

        public void setPoints(int points)
        {
            this.points = points;
        }

        public String getDesc()
        {
            return this.desc;
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
            return this.yPos;
        }

        public String getType()
        {
            return this.type;
        }

        /// <summary>
        /// Sets an ID for this Token
        /// </summary>
        /// <param name="id">The ID of the Token</param>
        public void setDesc(String desc)
        {
            Contract.Requires(desc != null);
            Contract.Requires(desc.Length >= 2);
            Contract.Requires(desc.Length <= 16);
            try
            {
                if (desc == null || desc.Length < 2 || desc.Length > 16)
                {
                    throw new ArgumentException("The name of a Token needs to be a least 2 characters and at most 16 characters long!");
                }
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
            }

            this.desc = desc;
        }

        /// <summary>
        /// Sets an ID for this Token
        /// </summary>
        /// <param name="id">The ID of the Token</param>
        public void setID(int id)
        {
            Contract.Requires(id >= 0);

            try
            {
                if (id < 0)
                {
                    throw new ArgumentException("The Token ID is not allowed to be smaller than 0!");
                }
                this.id = id;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
            }
           
        }

        
        /// <summary>
        /// Sets a token to busy state or undoes it
        /// </summary>
        /// <param name="busy">Boolean variable wether the player needs to be busy or not</param>
        public void setBusy(Boolean busy)
        {
            try
            {
                this.busy = busy;
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
            }
            
            
        }

        /// <summary>
        /// Sets a specified x-Coordinate to this Token
        /// </summary>
        /// <param name="xPos">x-Coordinate of this Token</param>
        public void setXPos(int xPos)
        {
            Contract.Requires(xPos >= 0 && xPos < GameManager.getGameManagerInstance().getMapHeight());

            this.xPos = xPos;
        }

        /// <summary>
        /// Sets a specified y-Coordinate to this Token
        /// </summary>
        /// <param name="yPos">y-Coordinate of this Token</param>
        public void setYPos(int yPos)
        {
            Contract.Requires(yPos >= 0 && yPos < GameManager.getGameManagerInstance().getMapWidth());

            this.yPos = yPos;
        }
    }
}
