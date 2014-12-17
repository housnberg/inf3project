using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend
{
    class PathWalker
    {
        private static PathWalker PathWalkerInstance = new PathWalker();
        private int index = 0;
        private int[] path;
        private bool walking = false;

        private PathWalker() {}

        public static PathWalker getPathWalkerInstance() 
        {
            return PathWalkerInstance;
        }

        public bool isWalking()
        {
            return walking;
        }

        public void setPath(int[] path)
        {
            if (path != null)
            {
                this.path = path;
                walking = true;
            }
            else
            {
                throw new Exception("the path cannot be null");
            }
        }

        public void walk(int rowPlayer, int colPlayer)
        {
            if (walking)
            {
                int[] coord = pointToCoordinate(path[index++], 10);
                int col = coord[0];
                int row = coord[1];
                if (row == rowPlayer)
                {
                    if (col < colPlayer)
                    {
                        //move up
                    }
                    else
                    {
                        //move down
                    }
                }
                else if (col == colPlayer)
                {
                    if (row < rowPlayer)
                    {
                        //move left
                    }
                    else
                    {
                        //move right
                    }
                }
                else
                {
                    throw new Exception("unable to move");
                }
            }

        }

        /// <summary>
        /// converts a 2 dimensional coordinate for a given point
        /// </summary>
        /// <param name="point">1 dimensional point</param>
        /// <param name="mapWidth">width of the map</param>
        /// <returns>arra of the 2 dimensional coordinate (coord[0]=col=x, coord[1]=row=y)</returns>
        public int[] pointToCoordinate(int point, int mapWidth)
        {
            int[] coord = new int[2];
            coord[0] = point % mapWidth;
            coord[1] = point / mapWidth;
            return coord;
        }
    }
}
