using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobot
{
    public class Field
    {
        public string[,] Grid { get; set; }

        public int RobotsLeft { get; set; }

        public int TurnsLeft { get; set; }

        public List<int> RobotCol { get; set; }

        public List<int> RobotRow { get; set; }

        public int RowMech { get; set; }

        public int ColMech { get; set; }

        public int RowLength { get; set; }

        public int ColLength { get; set; }

        public bool Win { get; set; }

        public Field(string[,] grid, int robotsleft, int turnsleft, List<int> robotcol, List<int> robotrow, int rowmech, int colmech, int rowlength, int collength, bool win)
        {
            Grid = grid;
            RobotsLeft = robotsleft;
            TurnsLeft = turnsleft;
            RobotRow = robotrow;
            RobotCol = robotcol;
            RowMech = rowmech;
            ColMech = colmech;
            ColLength = collength;
            RowLength = rowlength;
            Win = win;
        }
            
    }
}
