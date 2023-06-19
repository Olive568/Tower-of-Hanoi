using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Tower_of_Hanoi
{
    internal class Program
    {
         static void Main(string[] args)
        {
            int discs = 0;
            List<int>[] towers = new List<int>[3];
            string line = "";
            string[] start = new string[2];
            int[] settings = new int[2];
            for (int i = 0; i < 3; i++)
            {
                towers[i] = new List<int>();
            }
            StreamReader sr = new StreamReader("Setup.ini");
            {
                while ((line = sr.ReadLine()) != null)
                {
                    start = line.Split(' ');
                    settings[0] = int.Parse(start[0]);
                    settings[1] = int.Parse(start[1]);
                }
            }
            int tower = settings[0] - 1;
            if (settings[1] == 1)
                discs = 3;
            else if (settings[1] == 2)
                discs = 5;
            else if (settings[1] == 3)
            {
                discs = 7;
            }
            for (int i = discs; i > 0; i--)
            {
                towers[tower].Add(i); 
            }
            for (int x = towers[tower].Count; x > 0; x--)
            {
                Console.Write("=");
                for (int y = towers[tower][x - 1]; y > 0; y--)
                {
                    Console.Write("==");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
