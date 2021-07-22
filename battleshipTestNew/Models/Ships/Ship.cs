using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.Ships
{
    public abstract class Ship
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Hits { get; set; }
        public int shipCount { get; set; }
        public cellType cellType { get; set; }
        public bool IsSunk
        {
            get
            {
                return Hits >= Width;
            }
        }
    }
}
