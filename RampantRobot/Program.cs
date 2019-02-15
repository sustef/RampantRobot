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

            Factory factory = new Factory(10, 10, 6, 10, false);
            factory.Run(factory.Width, factory.Heigth, factory.Robots, factory.Turns, factory.RobotsMove);



            Console.ReadLine();
            
        }
    }
}
