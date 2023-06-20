using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;
using System.Data.SqlClient;

namespace Tower_of_Hanoi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Inititialization
            int discs = 0;
            int[,] towers = null;
            string line = "";
            string[] start = new string[2];
            int[] settings = new int[2];
            using (StreamReader sr = new StreamReader("Setup.ini"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    start = line.Split(' ');
                    settings[0] = int.Parse(start[0]);
                    settings[1] = int.Parse(start[1]);
                }
            }
            #endregion
            #region Discs
            int tower = settings[0] - 1;
            if (settings[1] == 1)
                discs = 3;
            else if (settings[1] == 2)
                discs = 5;
            else if (settings[1] == 3)
            {
                discs = 7;
            }
            towers = new int[3, discs];
            for (int i = discs; i > 0; i--)
            {
                towers[tower, discs - i] = i;
            }
            #endregion
            game(discs, tower, ref towers);
            Console.ReadKey();
        }

        static void display(int discs, int tower, ref int[,] towers)
        {
            for (int y = discs - 1; y >= 0; y--)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.Write("=");
                    for (int z = 0; z < towers[x, y]; z++)
                    {
                        Console.Write("==");
                    }
                    Console.Write("\t" + "\t");
                }
                Console.WriteLine();
            }
        }

        static void game(int discs, int tower, ref int[,] towers)
        {
            display(discs, tower, ref towers);
            int a = 0;
            int b = 0;
            Console.WriteLine("Put in 2 numbers, the first one is the tower from which you want to remove the disc, and the second one is the tower where you will put the disc.");
            Console.WriteLine("Tower of the disc you want to remove (0, 1, 2): ");
            a = int.Parse(Console.ReadLine());
            Console.WriteLine("Tower where you want to put the disc (0, 1, 2): ");
            b = int.Parse(Console.ReadLine());
            Console.Clear();
            if (a >= 3 || b >= 3 || a < 0 || b < 0)
            {
                Console.WriteLine("Invalid coordinates. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                game(discs, tower, ref towers);
            }
            else if (towers[a, 0] == 0)
            {
                Console.WriteLine("No disc to move. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                game(discs, tower, ref towers);
            }
            else
            {
                int temp = 0;
                for (int x = towers.GetLength(a) - 1; x >= 0; x--)
                {
                    if (towers[a,x] > 0)
                    {
                        temp = towers[a,x];
                        towers[a, x] = 0;
                        break;
                    }
                    else if (towers[a,x] == 0)
                    {
                        temp = towers[a, x];
                        towers[a, x] = 0;
                    }

                }
                for (int i = towers.GetLength(b) -1; i >= 0 ; i--)
                {
                    if (towers[b, i] > 0)
                    {
                        i += 1;
                        towers[b,i]  = temp;
                        break;
                    }
                    else if (i == 0)
                    {
                        towers[b,i] = temp;

                    }
                }
            }
            game(discs, tower, ref towers);

        }
    }
}
