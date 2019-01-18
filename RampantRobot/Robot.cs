using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobot
{
    public class Robot
    {
        public Random random = new Random();

        public Location RandomMoveRobot(int RowLength, int ColLength, int RobotRow, int RobotCol)
        {
            int Directions;
            Location randommoverobot = new Location(RobotRow, RobotCol);

            Directions = random.Next(0, 3);

            // veld loopt van 0 tot ColLength - 1 en
            // 0 tot RowLength -1
            if (Directions == 0)
            {
                randommoverobot.Row++; // beweging naar boven
                if (randommoverobot.Row > RowLength - 1) // als je buiten het veld stapt
                    randommoverobot.Row = 0; // kom je terug aan de andere kant van het veld
            }
            else if (Directions == 1)
            {
                randommoverobot.Row--; // beweging naar beneden
                if (randommoverobot.Row < 0) // als je buiten veld stapt
                    randommoverobot.Row = RowLength - 1; // kom je terug aan de andere kant van het veld
            }
            else if (Directions == 2)
            {
                randommoverobot.Col--; // beweging naar links
                if (randommoverobot.Col < 0)
                    randommoverobot.Col = ColLength - 1; 
            }
            else if (Directions == 3)
            {
                randommoverobot.Col++; // beweging naar rechts
                if (randommoverobot.Col > ColLength - 1)
                    randommoverobot.Col = 0; 
            }

            return randommoverobot;

        }
        public Location StartPositionRobot(int RowLength, int ColLength)
        {
            Location startpositionrobot = new Location(0,0); // moet eerst een waarde invullen, om daarna random te laten kiezen

            startpositionrobot.Col = random.Next(0, ColLength - 1);
            startpositionrobot.Row = random.Next(0, RowLength - 1);
            return startpositionrobot;
            
        }
    }
}
