using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class Entity : IPositionable
    {
        int x, y;
        public Entity(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getXPosition()
        {
            return this.x;
        }

        public int getYPosition()
        {
            return this.y;
        }
    }
}
