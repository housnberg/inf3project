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
        public void doWork() {
            while (true)
            {
                Console.WriteLine("----- FAKE PARSER PROCESSES ----- \n" 
                    + buffer.getElement() 
                    + "----- FAKE PARSER PROCESS END ----- \n");
            }
        }
    }
}
