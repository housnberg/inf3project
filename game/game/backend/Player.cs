﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    public class Player : Token
    {
        
        public Player(String name, int id)
        {
            this.setName(name);
            this.setID(id);
        }

    }
}
