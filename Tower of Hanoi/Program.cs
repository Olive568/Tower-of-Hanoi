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
using System.CodeDom.Compiler;

namespace Tower_of_Hanoi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string diff = "";
            int score = 0;
            List<string> history = new List<string>();
            int attempt = 0;
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

            if (settings[1] == 1)
            {
                discs = 3;
                diff = "easy";
            }
            if (settings[1] == 2)
            {
                discs = 5;
                diff = "medium";
            }
            if (settings[1] == 3)
            {
                discs = 7;
                diff = "hard";
            }
            discs = 7;
            towers = new int[3, discs];
            
            for (int i = discs; i > 0; i--)
            {
                towers[0, discs - i] = i;
            }
            using(StreamWriter sr = new StreamWriter("moves.txt",true))
            {
                sr.WriteLine("this is the move history for the last played game of " + diff + " difficulty");
            }
            game(discs, ref towers, ref attempt);
            Console.ReadKey();
        }
        static void display(int discs, ref int[,] towers, ref int attempt)
        {
            Console.WriteLine("welcome to tower of Hanoi.");
            Console.WriteLine("current move count : " + attempt);
            Console.WriteLine("\n" + "\n" + "\n" + "\n");
            for (int y = discs - 1; y >= 0; y--)
            {
                for (int x = 0; x < 3; x++)
                {
                    ConsoleColor diskColor = GetDiskColor(y);
                    Console.ForegroundColor = diskColor;
                    Console.Write("=");
                    
                    for (int z = 0; z < towers[x, y]; z++)
                    {
                        Console.Write("==");
                    }
                    if (towers[x, y] <= 3)
                        Console.Write("       ");
                    Console.Write("\t" + "\t");
                }
                Console.WriteLine();

            }

        }
        static void game(int discs, ref int[,] towers, ref int attempt)
        {
            display(discs, ref towers,ref attempt);
            Console.ForegroundColor = ConsoleColor.Gray;
            string[] ans = new string[2];
            int a = 0;
            int b = 0;
            int save = 0;
            Console.WriteLine("\n" + "What would you like your move to be?");
            Console.WriteLine("\n" +"\n" +"\n");
            Console.WriteLine("move format is X-Y");
            Console.WriteLine("X is the number of the tower the disc will come from");
            Console.WriteLine("Y is the number of the tower the disc will go to");
            Console.WriteLine("Rules to Remember");
            Console.WriteLine("A larger disc cannot be on top of a smaller disk");
            Console.WriteLine("The goal of this game is to transfer discs from tower 0 to tower 2");
            Console.SetCursorPosition(37, Console.CursorTop - 11);
            string answer = Console.ReadLine();
            ans = answer.Split('-');
            a = int.Parse(ans[0]);
            b = int.Parse(ans[1]);
            Console.Clear();
            if (a >= 3 || b >= 3 || a < 0 || b < 0)
            {
                Console.WriteLine("Invalid coordinates. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                game(discs, ref towers,ref attempt);
            }
            else if (towers[a, 0] == 0)
            {
                Console.WriteLine("No disc to move. Press any key to continue.");
                Console.ReadKey();
                Console.Clear();
                game(discs, ref towers, ref attempt);
            }
            else
            {
                attempt++;
                int temp = 0;
                print(a, b, discs);
                for (int x = discs - 1; x >= 0; x--)
                {
                    if (towers[a, x] > 0)
                    {
                        temp = towers[a, x];
                        towers[a, x] = 0;
                        save = x;
                        break;
                    }
                    else if (towers[a, x] == 0)
                    {
                        temp = towers[a, x];
                        towers[a, x] = 0;
                        save = x;
                    }
                }
                for (int i = discs - 1; i >= 0; i--)
                {
                    if (towers[b, i] > 0 && towers[b, i] < temp)
                    {
                        towers[a, save] = temp;
                        Console.WriteLine("Ring is too big. Press any key to continue.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    else if (i == 0 && towers[b, i] == 0)
                    {
                        towers[b, i] = temp;
                        break;
                    }
                    else if (towers[b, i] > 0)
                    {
                        towers[b, i + 1] = temp;
                        break;
                    }
                }
            }
            checking(ref towers, discs, ref attempt);
        }
        static void checking(ref int[,] towers, int discs,ref int attempt)
        {
            int sum = 0;
            for (int x = 0; x < discs; x++)
            {
                sum = towers[2, x] + sum;
            }

            if (discs == 3)
            {
                if (sum == 6)
                    win(ref attempt,ref towers, discs);
                else
                    game(discs, ref towers,ref attempt);
            }
            else if (discs == 5)
            {
                if (sum == 15)
                    win(ref attempt,ref towers, discs);
                else
                    game(discs, ref towers,ref attempt);
            }
            else if (discs == 7)
            {
                if (sum == 28)
                    win(ref attempt,ref towers, discs);
                else
                    game(discs, ref towers,ref attempt);
            }
        }
        static void win(ref int attempt, ref int[,] towers, int discs)
        {         
            double score = 0;
            double perfscore = 0;
            if (discs == 3)
                perfscore = 7;
            else if (discs == 5)
                perfscore = 31;
            else if (discs == 7)
                perfscore = 127;
            Console.Clear();
            display(discs, ref towers,ref attempt);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n" + "Congratulations! You finished the game in " +attempt +" moves");
            Console.WriteLine("The perfect scorse is " + perfscore);
            if(attempt == perfscore)
            {
                Console.WriteLine("Wow! You finished with a perfect score of 100!");
            }
            else
            {
                score = perfscore / attempt * 100;
                Console.WriteLine("You completed in " + attempt + " moves. your score is " + score);
            }
            Console.WriteLine("\n");
            Console.WriteLine("move format is X-Y");
            Console.WriteLine("X is the number of the tower the disc will come from");
            Console.WriteLine("Y is the number of the tower the disc will go to");
            Console.WriteLine("Rules to Remember");
            Console.WriteLine("A larger disc cannot be on top of a smaller disk");
            Console.WriteLine("The goal of this game is to transfer discs from tower 0 to tower 2");
            Console.ReadKey();
        }
        static void print(int a, int b, int discs)
        {
            using (StreamWriter sr = new StreamWriter("moves.txt",true))
            {
                sr.WriteLine("move disk from tower " + a + " to tower " + b);
            }
        }
        static ConsoleColor GetDiskColor(int level)
        {
            ConsoleColor[] colors = { ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Green, ConsoleColor.Red };
            return colors[level % colors.Length];
        }
    }
}
