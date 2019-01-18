using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's play RampantRobots!");
            Console.WriteLine("Try to catch all robots before you are out of turns");

            int rowLength = 10;
            int colLength = 10;
            int robots = 6;
            int turns = 10;
            int RowMech = 0;
            int ColMech = 0;
            bool win = false;
            List<int> Rrow = new List<int>();
            List<int> Rcol = new List<int>();

            Factory factory = new Factory();
            Field Game = factory.CreateField(rowLength, colLength, robots, turns, Rrow, Rcol, RowMech, ColMech,win);
            string directions = factory.Display(Game);
            
            while(Game.Win ==false && Game.TurnsLeft > 0)
            {
                Game = factory.MoveMech(directions, Game);
                if (Game.Win == true)
                {
                    break;
                }                
                directions = factory.Display(Game);

            }
            if(Game.Win == true)
            {
                Console.WriteLine("You have won!!!!!");
            }
            else
            {
                Console.WriteLine("You are out of turns, YOU LOST LOSER!!!!");
            }



            Console.ReadLine();
                
        }
    }
}
