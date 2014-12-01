using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
   
    public class Player : Token
    {

        public Player(int id, Boolean isBusy, String desc, int x, int y, int points)
            : base(id, isBusy, desc, x, y, points)
        {
            this.setType("Player");
        }

        public Player(int xPos, int yPos) 
        {
            this.setXPos(xPos);
            this.setYPos(yPos);
           
        }

    }

 
    
}
