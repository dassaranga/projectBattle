using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.data
{
    public class playerData
    {
        public string name { get; set; }
        public int row { get; set; }
        public virtual ICollection<playerBoardData> playerBoardData { get; set; }
    }
}
