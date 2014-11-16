using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.client
{
    class FakeParser
    {

        ClientBuffer buffer = ClientBuffer.getBufferInstance();
        GameManager gm = GameManager.getGameManagerInstance();
        public void doWork() {
            while (true)
            {
                gm.updateChatMessage("TEST", buffer.getElement());
                Console.WriteLine("------------------PROCESSES---------------");
            }
        }
    }
}
