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
            int width = 5;
            int height = 6;
            int[] map = new int[width*height];
            for(int i = 0; i < map.Length; i++) {
                map[i] = 1;
            }
            //map[0] = 0;
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
                IntPtr pointer = findPath(1, 11, map, width, height, 32);
                Marshal.Copy(pointer, path, 0, path.Length);
                Console.WriteLine("Returned Path:");
                foreach (int i in path)
                {
                    if (i <= width * height && i >= 0)
                    {
                        Console.Write(i + " ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
