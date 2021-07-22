using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.Ships
{
    public class Destroyer2:Ship
    {
        public Destroyer2()
        {
            Name = "Destroyer2";
            Width = 4;
            cellType = cellType.Destroyer2;
            shipCount = 2;
        }
    }
}
