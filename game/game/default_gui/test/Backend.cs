using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    /// <summary>
    /// All classes in the test-directory are just classes to illustrate the concept of the frontend and its interfaces.
    /// They are undocumented and not very well written and are using dummy-data with no connection to the actual server.
    /// On purpose. Because you should come up with your own implementation. :)
    /// </summary>
    public class Backend : IBackend
    {
        public void sendCommand(string command)
        {
            Console.WriteLine("received command " + command);
        }

        public void sendChat(string message)
        {
            Console.WriteLine("received chatmessage " + message);
        }

        public List<IPositionable> getDragons() {
            List<IPositionable> dragons = new List<IPositionable>();
            dragons.Add(new Entity(0,1));
            return dragons;

        }
        public List<IPositionable> getPlayers() {
            List<IPositionable> players = new List<IPositionable>();
            players.Add(new Entity(1, 1));
            return players;
        }

        public ITile[][] getMap()
        {
            int size = 10;
            // init
            ITile[][] map = new ITile[size][];
            for (int i = 0; i < size; i++)
            {
                map[i] = new ITile[size];
            }
            Random r = new Random();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    List<MapCellAttribute> attr = new List<MapCellAttribute>();
                    switch (r.Next(0, 5))
                    {
                        case 0:
                            attr.Add(MapCellAttribute.WATER);
                            break;
                        case 1:
                            attr.Add(MapCellAttribute.HUNTABLE);
                            attr.Add(MapCellAttribute.FOREST);
                            break;
                        case 2:
                            attr.Add(MapCellAttribute.FOREST);
                            break;
                        case 3:
                            attr.Add(MapCellAttribute.UNWALKABLE);
                            break;
                        case 4:
                            break;

                    }
                    map[x][y] = new MapCell(x, y, attr);
                }
            }
            return map;
        }

    }
}
