using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobot
{
    class Mechanic
    {
        public Location MoveMechanic(int RowMech, int ColMech, int RowLength, int ColLength, char Direction)
        {
            Location NewLocation = new Location(RowMech, ColMech);
            if (Direction == 'a')
            {
                NewLocation.Col--;
                if (NewLocation.Col < 0)
                    NewLocation.Col = ColLength - 1;
            }
            else if (Direction == 'd')
            {
                NewLocation.Col++;
                if (NewLocation.Col > ColLength - 1)
                    NewLocation.Col = 0;
            }
            else if (Direction == 'w')
            {
                NewLocation.Row--;
                if (NewLocation.Row < 0)
                    NewLocation.Row = RowLength - 1;
            }
            else if (Direction == 's')
            {
                NewLocation.Row++;
                if (NewLocation.Row > RowLength - 1)
                    NewLocation.Row = 0;
            }
            return NewLocation;
        }
    }

}
