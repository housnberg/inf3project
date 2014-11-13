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
                Console.WriteLine("FakeParser processes --------------------- " + buffer.getElement());
            }
        }
    }
}
