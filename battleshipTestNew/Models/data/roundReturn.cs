using battleshipTestNew.Models.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.data
{
    public class roundReturn
    {
        public string playerAttackResult { get; set; }
        public string playerAttackStatus { get; set; }
        public string playerAttackCellType { get; set; }
        public bool playerLoss { get; set; }
        public bool computorLoss { get; set; }
        public virtual ICollection<playerBoardData> playerShips { get; set; }

        public int computorAttackRow { get; set; }
        public int computorAttackCol { get; set; }
        public string computorAttackResult { get; set; }
        public string computorAttackStatus { get; set; }
        public string computorAttackCellType { get; set; }
        public virtual ICollection<playerBoardData> computorShips { get; set; }
    }
}
