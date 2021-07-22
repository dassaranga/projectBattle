using battleshipTestNew.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models.PlayArea
{
    public class BattleBoard : PlayerBoard
    {
        public List<Locations> FireRandomPanels()// if last attacked cell is mised or ship destroyed get random cell to attack
        {
            return Panels.Where(x => x.cellType == cellType.Empty && x.AvailableForRandom).Select(x => x.Locations).ToList();
        }

        public List<Locations> CheckAroundHitCell()//if last attack was hit and ship not destroyed, check around the hit cell for attack
        {
            List<Panel> panels = new List<Panel>();
            var hits = Panels.Where(x => x.cellType == cellType.Hit);
            foreach (var hit in hits)
            {
                panels.AddRange(CheckAround(hit.Locations).ToList());
            }
            return panels.Distinct().Where(x => x.cellType == cellType.Empty).Select(x => x.Locations).ToList();
        }

        public List<Panel> CheckAround(Locations location)
        {
            int row = location.Row;
            int column = location.Column;
            List<Panel> panels = new List<Panel>();
            if (column > 1)
            {
                panels.Add(Panels.At(row, column - 1));
            }
            if (row > 1)
            {
                panels.Add(Panels.At(row - 1, column));
            }
            if (row < 10)
            {
                panels.Add(Panels.At(row + 1, column));
            }
            if (column < 10)
            {
                panels.Add(Panels.At(row, column + 1));
            }
            return panels;
        }
    }
}
