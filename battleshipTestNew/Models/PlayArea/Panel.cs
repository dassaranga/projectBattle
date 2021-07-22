using ConsoleApp2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.PlayArea
{
    public class Panel
    {
        public cellType cellType { get; set; }
        public Locations Locations { get; set; }

        public Panel(int row, int column)
        {
            Locations = new Locations(row, column);
            cellType = cellType.Empty;
        }

        public string cellStatus
        {
            get
            {
                return cellType.GetAttributeOfType<DescriptionAttribute>().Description;
            }
        }

        public bool IsLocated
        {
            get
            {
                return cellType == cellType.Battleship
                    || cellType == cellType.Destroyer1
                    || cellType == cellType.Destroyer2;
            }
        }

        public bool AvailableForRandom
        {
            get
            {
                return (Locations.Row % 2 == 0 && Locations.Column % 2 == 0)
                    || (Locations.Row % 2 == 1 && Locations.Column % 2 == 1);
            }
        }
    }
}
