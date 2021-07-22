using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.Ships
{
    public class Destroyer1:Ship
    {
        public Destroyer1()
        {
            Name = "Destroyer1";
            Width = 4;
            cellType = cellType.Destroyer1;
            shipCount = 2;
        }
    }
}
