using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.Ships
{
    public class Battleship:Ship
    {
        public Battleship()
        {
            Name = "Battleship";
            Width = 5;
            cellType = cellType.Battleship;
            shipCount = 1;
        }
    }
}
