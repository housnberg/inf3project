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

        private PathWalker() {}

        /// <summary>
        /// singleton
        /// </summary>
        /// <returns>returns the only possible PathWalker instance</returns>
        public static PathWalker getPathWalkerInstance() 
        {
            return PathWalkerInstance;
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
        /// returns whether the PathWalker is running or not
        /// </summary>
        /// <returns></returns>
        public bool isWalking()
        {
            return walking;
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
                    int[] coord = gameManager.pointToCoordinate(path[index++]);
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
    }
}
