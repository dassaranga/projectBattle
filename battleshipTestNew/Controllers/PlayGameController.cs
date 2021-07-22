using battleshipTestNew.Ext;
using battleshipTestNew.Models;
using battleshipTestNew.Models.data;
using battleshipTestNew.Models.PlayArea;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace battleshipTestNew.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlayGameController : ControllerBase
    {
        public static Player Player { get; set; }
        public static Player Computor { get; set; }
        public Locations Locations { get; set; }

        //initialize the game, asign ships for user and computor randomly
        [HttpPost]
        public List<playerData> initGame(playerData pd)
        {
            List<playerData> pld = new List<playerData>();
            Player = new Player(pd.name);
            Computor = new Player("Computor");

            Player.addShipsRandomly();
            Computor.addShipsRandomly();

            //pld.playerBoardData = new List<playerBoardData>();
            for (int row = 1; row <= 10; row++)
            {
                playerData pldata = new playerData();
                pldata.playerBoardData = new List<playerBoardData>();
                pldata.row = row;                
                for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    playerBoardData cell = new playerBoardData();
                    //cell.row = row;
                    cell.col = ownColumn;
                    cell.cellStatus = Player.playerBoard.Panels.At(row, ownColumn).cellStatus;
                    cell.cellType = Player.playerBoard.Panels.At(row, ownColumn).cellType.ToString();
                    cell.isPlayer = true;
                    pldata.playerBoardData.Add(cell);
                    //Console.Write(playerBoard.Panels.At(row, ownColumn).cellStatus + " ");
                }
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    playerBoardData cell = new playerBoardData();
                    //cell.row = row;
                    cell.col = firingColumn;
                    cell.cellStatus = Player.battleBoard.Panels.At(row, firingColumn).cellStatus;
                    cell.cellType = Player.battleBoard.Panels.At(row, firingColumn).cellType.ToString();
                    cell.isPlayer = false;
                    pldata.playerBoardData.Add(cell);
                }
                pld.Add(pldata);
            }
            return pld;
        }

        //this method called when user clicked on cell to attack
        [HttpPost]
        public roundReturn playerAttack(playerData pd) {
            roundReturn roundState = new roundReturn();
            Locations = new Locations(Convert.ToInt32(pd.row), Convert.ToInt32(pd.name));
            if (!Player.HasDefeat && !Computor.HasDefeat) {
                var location = Locations;
                var result = Computor.AnalyzeAttack(location);//check the result of player attacked cell
                Player.ProcessAttackResult(location, result);//apply attack result to oponent game panel

                roundState.playerAttackResult = result.ToString();
                roundState.playerAttackStatus = Player.battleBoard.Panels.At(location.Row, location.Column).cellStatus;
                roundState.playerAttackCellType = Player.battleBoard.Panels.At(location.Row, location.Column).cellType.ToString();
                roundState.computorLoss = Computor.HasDefeat;
                roundState.playerShips = new List<playerBoardData>();

                if (!Player.HasDefeat)// genarate attack to the player from computor
                {
                    location = Computor.attackToCell();//get cell to be attacked
                    result = Player.AnalyzeAttack(location);//check the result of player attacked cell
                    Computor.ProcessAttackResult(location, result);//apply attack result to oponent game panel

                    roundState.computorAttackRow = location.Row;
                    roundState.computorAttackCol = location.Column;
                    roundState.computorAttackResult = result.ToString();
                    roundState.computorAttackStatus = Computor.battleBoard.Panels.At(location.Row, location.Column).cellStatus;
                    roundState.computorAttackCellType = Computor.battleBoard.Panels.At(location.Row, location.Column).cellType.ToString();
                    roundState.playerLoss = Player.HasDefeat;
                    roundState.computorShips = new List<playerBoardData>();
                }
            }
            foreach (var ship in Player.Ships)//this loop is used for get destroyed ships of player
            {
                playerBoardData s = new playerBoardData();
                if (ship.IsSunk)
                {
                    s.cellStatus = ship.Name;
                    roundState.playerShips.Add(s);
                }
            }
            foreach (var ship in Computor.Ships)//this loop is used for get destroyed ships of computor
            {
                playerBoardData s = new playerBoardData();
                if (ship.IsSunk)
                {
                    s.cellStatus = ship.Name;
                    roundState.computorShips.Add(s);
                }
            }
            return roundState;
        }

    }
}
