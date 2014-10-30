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
        private int xPos;
        private int yPos;
        private String desc;
        private String type;
        private String name;
        private Boolean busy;

        public Token()
        {

        }

        public Token(String name, int id)
        {
            this.setName(name);
            this.setID(id);
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
        /// Sets an ID for this Token
        /// </summary>
        /// <param name="id">The ID of the Token</param>
        public void setName(String name)
        {
            Contract.Requires(name != null);
            Contract.Requires(name.Length >= 2);
            Contract.Requires(name.Length <= 16);
            try
            {
                if (name == null || name.Length < 2 || name.Length > 16)
                {
                    throw new ArgumentException("The name of a Token needs to be a least 2 characters and at most 16 characters long!");
                }
            }
            catch (Exception exception)
            {
                Console.Out.WriteLine(exception.Message);
            }
           
            this.name = name;
        }

        /// <summary>
        /// Sets an ID for this Token
        /// </summary>
        /// <param name="id">The ID of the Token</param>
        public void setID(int id)
        {
            Contract.Requires(id > 0);
            Contract.Requires(id < 100);

            try
            {
                if (id < 0 || id > 100)
                {
                    throw new ArgumentException("The ID for a Token needs to be greater than 0 and smaller than 100");
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
