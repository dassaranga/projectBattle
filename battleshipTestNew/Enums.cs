using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew
{
    public enum cellType
    {
        [Description("o")]
        Empty,

        [Description("B")]
        Battleship,

        [Description("D")]
        Destroyer1,

        [Description("D")]
        Destroyer2,

        [Description("X")]
        Hit,

        [Description("M")]
        Miss
    }
    public enum attackResult
    {
        Miss,
        Hit
    }
}
