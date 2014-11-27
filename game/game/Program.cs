using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game.client;
using System.Threading;
using game.gui;
using System.Drawing;
using System.Text.RegularExpressions;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ip for localhost, port 1024 for local server via eclipse
            //Connector client = new Connector("127.0.0.1", 1024);
            //Console.WriteLine("success!");
            //FakeParser parser = new FakeParser();
            //Thread fakeParser = new Thread(parser.doWork);
            //fakeParser.Start();
            //Console.ReadKey();
            //client.sendServerMessage("get:map");
            //client.sendServerMessage("get:time");
            //Console.ReadKey();
            String id = "id:123a";
            if (Regex.IsMatch(id,"id:\\d+[^a-z]"))
            {
                Console.WriteLine("Yes!");
            }
            else
            {
                Console.WriteLine("No!");
            }

                Console.ReadKey();
            //new GameManager("127.0.0.1", 1024);

        }
    }

}
