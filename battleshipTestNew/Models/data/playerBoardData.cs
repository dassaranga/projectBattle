using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.data
{
    public class playerBoardData
    {
        //public int row { get; set; }
        public int col { get; set; }
        public string cellStatus { get; set; }
        public string cellType { get; set; }
        public bool isPlayer { get; set; }
    }
}
