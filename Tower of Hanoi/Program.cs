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
            int towers = 3;
            List<int> zero = new List<int>();
            List<int> one = new List<int>();
            List<int> Two = new List<int>();
            string line = "";
            string[] start = new string[2];
            int[] settings = new int[2];
            StreamReader sr = new StreamReader("Setup.ini");
            {
                while ((line = sr.ReadLine()) != null)
                {
                    start = line.Split(' ');
                    settings[0] = int.Parse(start[0]);
                    settings[1] = int.Parse(start[1]);
                }
            }
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
                zero.Add(i);
            }
            for (int x = zero.Count; x > 0; x--)
            {

                    Console.Write("=");
                for (int y = zero[x - 1]; y > 0; y--)
                {
                    Console.Write("==");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
