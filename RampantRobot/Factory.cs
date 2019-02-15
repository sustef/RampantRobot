using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobot
{
    class Factory
    {
        public int Width;
        public int Heigth;
        public int Robots;
        public int Turns;
        public bool RobotsMove;

        public Factory(int width, int heigth, int robots, int turns, bool robotsmove)
        {
            Width = width;
            Heigth = heigth;
            Robots = robots;
            Turns = turns;
            RobotsMove = robotsmove;
        }

        public void Run(int width, int heigth, int robots, int turns, bool robotsmove)
        {
            Factory factory = new Factory(width, heigth, robots, turns, robotsmove);
            List<int> RobotRow = new List<int>();
            List<int> RobotCol = new List<int>();
            Location MechLocation = new Location(0, 0);
            bool win = false;
            Field Game = factory.CreateField(RobotRow, RobotCol, MechLocation, win);
            string directions = factory.Display(Game);

            while (Game.Win == false && Game.TurnsLeft > 0)
            {
                Game = factory.MoveMech(directions, Game);
                if (Game.Win == true)
                {
                    for (int i = 0; i < Width; i++)
                    {
                        for (int j = 0; j < Heigth; j++)
                        {
                            Console.Write(string.Format("{0} ", Game.Grid[i, j]));
                        }
                        Console.Write(Environment.NewLine + Environment.NewLine);
                    }
                        break;
                }
                directions = factory.Display(Game);

            }
            if (Game.Win == true)
            {
                Console.WriteLine("You have won!!!!!");
            }
            else
            {
                Console.WriteLine("You are out of turns, YOU LOST LOSER!!!!");
            }
        }

        public Field CreateField(List<int> RobotRow, List<int> RobotCol, Location MechLocation, bool Win)
        {
            // lege matrix wordt aangemaakt met RowLength aantal rijen en ColLength aantal kolommen
            string[,] grid = new string[Width, Heigth];
            Field field = new Field(grid, Robots, Turns, RobotRow, RobotCol, MechLocation, Win);

            // lege waarde opvullen met iets overzichtelijks
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Heigth; j++)
                    field.Grid[i, j] = ".";
            }
            // De mechanic begint altijd linksboven
            field.Grid[0, 0] = "M";


            //  Bepaal de beginlocaties van de robots
            Robot robot = new Robot();
            for (int i = 0; i < field.RobotsLeft; i++)
            {
                // voor een nieuwe poging weer op false zetten
                bool MLoc = false;
                while (MLoc == false)
                {
                    // hier worden iedere keer de startposities aangemaakt                    
                    Location startpositie = robot.StartPositionRobot(Width, Heigth);
                    // voor een nieuwe poging weer op false zetten
                    bool SameLocation = false;
                    if (startpositie.Row == MechLocation.Row && startpositie.Col == MechLocation.Col)
                    {
                        // wanneer een robot op de positie van de Mechanic komt, doet ie niks en gaat ie het nog een keer proberen in de while loop
                    }
                    else
                    {   // eerste robot kan vrij aangemaakt worden
                        if (i == 0)
                        {
                            field.Grid[startpositie.Row, startpositie.Col] = "R";
                            field.RobotRow.Add(startpositie.Row); // opslaan in list om te controleren of Robots niet op dezelfde plek komen
                            field.RobotCol.Add(startpositie.Col);
                            MLoc = true; // niet echt true maar stap is doorlopen en while loop kan stoppen                            
                        }
                        // de andere robots moeten gecontroleerd worden of ze niet op dezelfde plaats staan
                        else
                        {
                            // for alle robots die er tot dan toe aangemaakt zijn
                            for (int j = 0; j < i; j++)
                            {
                                // controleer of ze op precies dezeflde plaats staan
                                if (startpositie.Row == field.RobotRow[j] && startpositie.Col == field.RobotCol[j])
                                {
                                    SameLocation = true;
                                }
                                // niks invullen of onthouden maar de while loop blijft doorgaan, voor een nieuwe poging
                            }
                            if (SameLocation == false)
                            {
                                // invullen want gecheckt
                                field.Grid[startpositie.Row, startpositie.Col] = "R";
                                field.RobotRow.Add(startpositie.Row);
                                field.RobotCol.Add(startpositie.Col);
                                MLoc = true; // niet echt true maar stap is doorlopen en while loop kan stoppen
                            }
                        }
                    }
                }
            }
            return field;
        }



        public string Display(Field field)
        {
            // Er wordt laten zien hoeveel turns er nog over zijn
            Console.WriteLine("You have {0} turns remaining", field.TurnsLeft);

            // Matrix wordt laten zien. Hierin staan de robots en de mechanic.
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Heigth; j++)
                {
                    Console.Write(string.Format("{0} ", field.Grid[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            string directions = "";
            // Het aantal robots wat over is wordt laten zien. 
            Console.WriteLine("You have {0} robots remaining", field.RobotsLeft);
            
            if (field.TurnsLeft != 0)
            {
                Console.WriteLine("Use 'awsd' to move the mechanic");
                directions = Console.ReadLine();
                directions = CheckMove(directions);
            }
            return directions;
            
        }
        public string CheckMove(string directions)
        {            
            bool invalid_input = false;
            // controle correcte waarden            
            {
                for (int i=0; i<directions.Length; i++)
                {
                    if (directions[i] != 'a' && directions[i] != 's' && directions[i] != 'd' && directions[i] != 'w')
                        invalid_input = true;
                }
                if (invalid_input == true)
                {
                    Console.WriteLine("You have inserted an invalid statement. Please try again!");
                    directions = Console.ReadLine();
                    CheckMove(directions);
                }

            }
            return directions;
        }
        public Field MoveMech(string directions, Field field)
        {
            Mechanic mechanic = new Mechanic();
            field.TurnsLeft--;
            for (int i = 0; i < directions.Length; i++)
            {
                // Oude Mechanic wordt vervangen door niks.
                field.Grid[field.MechLocation.Row, field.MechLocation.Col] = ".";
                // Nieuwe locatie van de Mechanic wordt bepaald.
                Location newLocation = mechanic.MoveMechanic(field.MechLocation.Row, field.MechLocation.Col, Width, Heigth, directions[i]);
                field.MechLocation.Row = newLocation.Row;
                field.MechLocation.Col = newLocation.Col;
                field.Grid[field.MechLocation.Row, field.MechLocation.Col] = "M";
                for (int j = field.RobotsLeft -1 ; j > -1; j--)
                {
                    
                    if (field.MechLocation.Row == field.RobotRow[j] && field.MechLocation.Col == field.RobotCol[j])
                    {
                        field.RobotRow.RemoveAt(j);
                        field.RobotCol.RemoveAt(j);
                        field.RobotsLeft--;
                    }

                }
                if (RobotsMove == true)
                {
                    field = MoveRob(field);
                }
                
            }
            if(field.RobotsLeft == 0)
            {
                field.Win = true;
            }
            return field;
        }

        public Field MoveRob(Field field)
        {
            Robot robot = new Robot();
            
            for (int j = field.RobotsLeft-1; j > -1; j--)
            {
                bool RobotRamp = false;
                field.Grid[field.RobotRow[j], field.RobotCol[j]] = "."; // oude plaats verwijderen
                Location newLocation = robot.RandomMoveRobot(Width, Heigth, field.RobotRow[j], field.RobotCol[j]);

                for (int k = 0; k < field.RobotsLeft; k++)
                {
                    if (newLocation.Row == field.RobotRow[k] && newLocation.Col == field.RobotCol[k])
                    {
                        RobotRamp = true;
                        field.Grid[field.RobotRow[j], field.RobotCol[j]] = "R"; // oude plaats weer pakken
                    }
                }

                if (RobotRamp == false)
                {
                    if (newLocation.Row == field.MechLocation.Row && newLocation.Col == field.MechLocation.Col)
                    {
                        // so robot is captured end deleted from field
                        field.RobotRow.RemoveAt(j);
                        field.RobotCol.RemoveAt(j);
                        field.RobotsLeft--;
                    }
                    else
                    {
                        field.RobotRow[j] = newLocation.Row; // nieuwe plaats opslaan
                        field.RobotCol[j] = newLocation.Col;
                        field.Grid[field.RobotRow[j], field.RobotCol[j]] = "R"; // nieuwe plaats in Grid opslaan
                    }
                }
            }
            return field;
        }
    }

}


