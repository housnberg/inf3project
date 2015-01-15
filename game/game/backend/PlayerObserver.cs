using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.backend
{
    public class PlayerObserver : IObserver
    {
        PathWalker pwInstance = PathWalker.getPathWalkerInstance();
        GameManager gameManager = GameManager.getGameManagerInstance();

        /// <summary>
        /// Takes action according to the new change
        /// </summary>
        public void onIdChange()
        {
            //Console.WriteLine("Player ID has changed.");
        }

        /// <summary>
        /// Takes action according to the new change
        /// </summary>
        public void onIsBusyChange()
        {
            //Console.WriteLine("Player isBusy has changed.");
        }

        /// <summary>
        /// Takes action according to the new change
        /// </summary>
        public void onDescChange()
        {
            //Console.WriteLine("Player description has changed");
        }

        /// <summary>
        /// Takes action according to the new change
        /// </summary>
        public void onPosChange()
        {
            //Console.WriteLine("Player position has changed.");
            try
            {
                if (gameManager.getThisPlayer() != null && pwInstance.isWalking())
                {
                    pwInstance.walk(gameManager.getThisPlayer().getYPos(), gameManager.getThisPlayer().getXPos());
                }                
            }
            catch (Exception e)
            {
                pwInstance.stopWalking();
                Console.WriteLine(e.Message);
            }
            
        }

        /// <summary>
        /// Takes action according to the new change
        /// </summary>
        public void onPointChange()
        {
            //Console.WriteLine("Player points have changed.");
        }
    }
}
