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
            List<string> history = new List<string>();
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
                towers[0, discs - i] = i;
            }
            #endregion
            game(discs,  ref towers, ref history);
            Console.ReadKey();
        }

        static void display(int discs, ref int[,] towers)
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

        static void game(int discs, ref int[,] towers, ref List<string> history)
        {
            display(discs, ref towers);
            int a = 0;
            int b = 0;
            int save = 0;
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
                game(discs, ref towers);
            }
            else if (towers[a, 0] == 0)
            {
                Console.WriteLine("No disc to move. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                game(discs, ref towers);
            }
            else
            {    
                
                int temp = 0;
                for (int x = discs - 1; x >= 0; x--)
                {
                    if (towers[a,x] > 0)
                    {
                        temp = towers[a,x];
                        towers[a, x] = 0;
                        save = x;
                        break;
                    }
                    else if (towers[a,x] == 0)
                    {
                        temp = towers[a, x];
                        towers[a, x] = 0;
                        save = x;
                    }

                }
                for (int i = discs -1; i >= 0 ; i--)
                {

                    if (towers[b, i] > 0 && towers[b, i] < temp)
                    {
                        towers[a, save] = temp;
                        Console.WriteLine("ring is too big, press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    else if(i == 0 && towers[b, i] == 0)
                    {
                        towers[b,i] = temp;
                        break;
                    }
                    else if(towers[b,i] > 0)
                        {
                        towers[b,i +1] = temp;
                        break;
                    }
                    
                    
                }
            }
            checking(towers,discs);
            game(discs, ref towers, ref history);

        }
        static void checking(ref int[,] towers, int discs)
        {
            int sum = 0;
            for(int x = 0; x < discs; x++)
            {
                sum = towers[2,x] + sum;
            }
            if(discs == 3)
            {
                if(sum == 6)
                    win();
                else
                    game();
            }
            else if(discs == 5)
            {
                if(sum == 15)
                    win();
                else
                    game();
            }
            else if(discs == 7)
            {
                if(sum == 28)
                    win();
                else
                    game();
            }
        }
        static void win()
        {
            Console.Clear();
            Console.WriteLine("you win");
            Console.ReadKey();
        }
    }
}
