using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.PlayArea
{
    public class Locations
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Locations(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
