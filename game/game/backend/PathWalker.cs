using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend
{
    class PathWalker
    {
        private GameManager gameManager = GameManager.getGameManagerInstance();
        private static PathWalker PathWalkerInstance = new PathWalker();
        private int index = 1;
        private int[] path;
        private bool walking = false;
        private int width;

        private PathWalker() {}

        /// <summary>
        /// singleton
        /// </summary>
        /// <returns>returns the only possible PathWalker instance</returns>
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

        /// <summary>
        /// will be called by the GameManager and/or PlayerObserver when a user clicks on a map cell.
        /// finds the right server-command for a given destination.
        /// </summary>
        /// <param name="colPlayer">column of the player</param>
        /// <param name="rowPlayer">row of the player</param>
        public void walk(int colPlayer, int rowPlayer)
        {
            if (walking)
            {
                int anzPfade = path[0];
                if (index <= anzPfade)
                {
                    int[] coord = pointToCoordinate(path[index++], width);
                    int col = coord[0];
                    int row = coord[1];
                    if (row == rowPlayer)
                    {
                        if (col < colPlayer)
                        {
                            gameManager.sendCommand("ask:mv:up");
                        }
                        else
                        {
                            gameManager.sendCommand("ask:mv:dwn");
                        }
                    }
                    else if (col == colPlayer)
                    {
                        if (row < rowPlayer)
                        {
                            gameManager.sendCommand("ask:mv:lft");
                        }
                        else
                        {
                            gameManager.sendCommand("ask:mv:rgt");
                        }
                    }
                    else
                    {
                        throw new Exception("unable to move");
                    }
                }
                else
                {
                    stopWalking();
                }
            }

        }

        /// <summary>
        /// resets all needed attributes to default values after the PathWalker finishes walking.
        /// </summary>
        public void stopWalking()
        {
            index = 1;
            path = null;
            walking = false;
        }

        /// <summary>
        /// converts a two dimensional coordinate for a given point
        /// </summary>
        /// <param name="point">1 dimensional point</param>
        /// <param name="mapWidth">width of the map</param>
        /// <returns>array of the 2 dimensional coordinate (coord[0]=col, coord[1]=row)</returns>
        public int[] pointToCoordinate(int point, int mapWidth)
        {
            int[] coord = new int[2];
            coord[1] = point % mapWidth;
            coord[0] = point / mapWidth;
            return coord;
        }

        public void setMapWidth(int width)
        {
            this.width = width;
        }
    }
}
