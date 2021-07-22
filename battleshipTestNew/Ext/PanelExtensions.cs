using battleshipTestNew.Models.PlayArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleshipTestNew.Ext
{
    public static class PanelExtensions
    {
        public static Panel At(this List<Panel> panels, int row, int column)
        {
            return panels.Where(x => x.Locations.Row == row && x.Locations.Column == column).First();
        }

        public static List<Panel> Range(this List<Panel> panels, int startRow, int startColumn, int endRow, int endColumn)
        {
            return panels.Where(x => x.Locations.Row >= startRow
                                     && x.Locations.Column >= startColumn
                                     && x.Locations.Row <= endRow
                                     && x.Locations.Column <= endColumn).ToList();
        }
    }
}
