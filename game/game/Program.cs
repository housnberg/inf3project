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
            String test = "begin:player\nid:123\ntype:Player\nbusy:false\ndesc:blah\nx:1\ny:1\npoints9001\nend:player\nbegin:dragon\nid:123\ntype:Player\nbusy:false\ndesc:blah\nx:1\ny:1\npoints9001\nend:dragon\nbegin:player\nid:123\ntype:Player\nbusy:false\ndesc:blah\nx:1\ny:1\npoints9001\nend:player";
            while (test.Contains("begin:player") || test.Contains("begin:dragon"))
            {
                if (test.Contains("begin:player"))
                {
                    test = test.Replace("begin:player\n", "splitHere");
                    test = test.Replace("end:player\n", "splitHere");
                    Console.WriteLine("replaced player");
                }
                if (test.Contains("begin:dragon"))
                {
                    test = test.Replace("begin:dragon\n", "splitHere");
                    test = test.Replace("end:dragon\n", "splitHere");
                    Console.WriteLine("replaced dragon");
                }
            }
            String[] test1 = Regex.Split(test, "splitHere");
            for (int i = 0; i < test1.Length; i++)
            {
                test1[i] = test1[i].Trim();
                Console.WriteLine(test1[i]);
            }

                Console.ReadKey();
            //new GameManager("127.0.0.1", 1024);

        }
    }

}
