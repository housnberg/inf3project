using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.client
using System.Threading;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ip for localhost, port 1024 for local server via eclips
            Connector client = new Connector("127.0.0.1", 1024);
            Console.WriteLine("success!");
            FakeParser parser = new FakeParser();
            Thread fakeParser = new Thread(parser.doWork);
            fakeParser.Start();
            Console.ReadKey();
            client.sendServerMessage("get:map");
            client.sendServerMessage("get:time");
            Console.ReadKey();

            //GameManager gm = new GameManager();
            //Player p = new Player();
            //gm.deletePlayer(null);

        }
    }
}
