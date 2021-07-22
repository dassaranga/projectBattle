using battleshipTestNew.Ext;
using battleshipTestNew.Models.PlayArea;
using battleshipTestNew.Models.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Models
{
    public class Player
    {
        public string playerName { get; set; }
        public PlayerBoard playerBoard { get; set; }
        public BattleBoard battleBoard { get; set; }        
        //public Player player { get; set; }
        //public Player computor { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasDefeat
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }

        public Player(string name)
        {
            playerName = name;
            Ships = new List<Ship>()
            {
                new Destroyer1(),
                new Destroyer2(),
                new Battleship()
            };
            playerBoard = new PlayerBoard();
            battleBoard = new BattleBoard();
        }

        public void getGameBoards(string playerType)
        {

        }

        public void addShipsRandomly()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {
                bool loopOpen = true;
                while (loopOpen)
                {
                    List<int> panelNumber = new List<int>();
                    var beginCol = rand.Next(1, 11);
                    var beginRow = rand.Next(1, 11);
                    int endRow = beginRow;
                    int endCol = beginCol;
                    var direction = rand.Next(1, 11);

                    if (direction == 0) //horizontal
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endRow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endCol++;
                        }
                    }

                    //check ship place within the array
                    if (endRow > 10 || endCol > 10)
                    {
                        loopOpen = true;
                        continue;
                    }

                    var assignedCells = playerBoard.Panels.Range(beginRow, beginCol, endRow, endCol);
                    if (assignedCells.Any(x => x.IsLocated))
                    {
                        loopOpen = true;
                        continue;
                    }

                    foreach (var cell in assignedCells)
                    {
                        cell.cellType = ship.cellType;
                    }
                    loopOpen = false;
                }
            }
        }

        public Locations attackToCell()
        {
            //check for ships already hit and not destroyed
            Locations loc;
            var attackedNaighborCells = battleBoard.CheckAroundHitCell();
            if (attackedNaighborCells.Any())
            {
                loc = AttackDiscoveredShip();
            }
            else
            {
                loc = RandomAttack();
            }
            return loc;
        }

        private Locations RandomAttack()// if last attacked cell is mised or ship destroyed get random cell to attack
        {
            var availableCells = battleBoard.FireRandomPanels();
            Random random = new Random(Guid.NewGuid().GetHashCode());
            var cellId = random.Next(availableCells.Count);
            return availableCells[cellId];
        }

        private Locations AttackDiscoveredShip()//if last attack was hit and ship not destroyed, check around the hit cell for attack
        {
            var attackedNeighborCells = battleBoard.CheckAroundHitCell();
            Random random = new Random(Guid.NewGuid().GetHashCode());
            var neighborCellID = random.Next(attackedNeighborCells.Count);
            return attackedNeighborCells[neighborCellID];
        }

        public attackResult AnalyzeAttack(Locations loc)//check the result of player attacked cell
        {
            var cell = playerBoard.Panels.At(loc.Row, loc.Column);
            if (!cell.IsLocated)
            {
                return attackResult.Miss;
            }

            var ship = Ships.First(x => x.cellType == cell.cellType);
            ship.Hits++;
            if (ship.IsSunk) { }
            return attackResult.Hit;
        }

        public void ProcessAttackResult(Locations loc, attackResult result)//apply attack result to oponent game panel
        {
            var cell = battleBoard.Panels.At(loc.Row, loc.Column);
            switch (result)
            {
                case attackResult.Hit:
                    cell.cellType = cellType.Hit;
                    break;

                default:
                    cell.cellType = cellType.Miss;
                    break;
            }
        }
    }
}
