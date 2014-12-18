using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.IO;

namespace DllTest
{
    class Program
    {

        [DllImport("PathFinder.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr findPath(int from, int to, int[] map, int mapw, int maph, int plength);

        static void Main(string[] args)
        {
            test(4, 1);
            test(1, 11);
            test(11, 1);
            Console.ReadKey();
        }

        public static int[] pointToCoordinate(int point, int mapWidth)
        {
            int[] coord = new int[2];
            coord[0] = point % mapWidth;
            coord[1] = point / mapWidth;
            return coord;
        }

        public static void test(int from, int to)  {
            int width = 5;
            int height = 6;
            int[] map = new int[width*height];
            for(int i = 0; i < map.Length; i++) {
                map[i] = 1;
            }

            map[0] = 0;
            map[6] = 0;
            map[7] = 0;
            map[9] = 0;
            map[12] = 0;
            map[16] = 0;
            map[17] = 0;
            map[18] = 0;
            //map[27] = 0;
            map[22] = 0;
            try
            {
                int[] path = new int[32];
                Console.WriteLine("Found DLL: " + File.Exists("PathFinder.dll"));
                IntPtr pointer = findPath(from, to, map, width, height, 32);
                Marshal.Copy(pointer, path, 0, path.Length);
                Console.WriteLine("Returned Path:");
                int anzPfade = path[0];
                for (int i = 1; i <= anzPfade; i++)
                {
                    int[] coord = pointToCoordinate(path[i], width);
                    Console.Write(coord[0] + "|" + coord[1] + " ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
