using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class Player : Token
    {
        private String name;
        private int ID;
        public Player(String name)
        {
            this.name = name;
        }

        public int getID()
        {
            return this.ID;
        }


    }
}
