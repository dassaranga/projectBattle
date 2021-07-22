// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var myapp = angular.module('myapp', ["ngRoute"]);
myapp.controller('mainController', function ($scope, $http) {
    var pLoss = false;
    var cLoss = false;
    $scope.rowArr = [];
    $scope.initGame = function () {
        var Player = {};
        Player.name = "player";
        //$http({
        //    method: 'POST',
        //    headers: { 'Content-Type': 'application/jason' },
        //    url: initGameLink,
        //    data: $.param(Player)
        //})
        if (!pLoss && !cLoss) {
            $http({
                method: "POST",
                url: initGameLink,
                dataType: 'json',
                data: Player,
                headers: { "Content-Type": "application/json" }
            })
                .success(function (data) {
                    //alert();
                    $scope.rowArr = [].concat(data);
                }).error(function (data) {

                });
        }

    };
    $scope.initGame();

    $scope.attackButtonClicked = function (row, col) {
        var Player = {};
        Player.row = row.row;
        Player.name = String(col.col);
        $http({
            method: "POST",
            url: playerAttackLink,
            dataType: 'json',
            data: Player,
            headers: { "Content-Type": "application/json" }
        })
            .success(function (data) {
                $scope.playerShips = [].concat(data.playerShips);
                $scope.computorShips = [].concat(data.computorShips);
                if (!pLoss && !cLoss) {
                    $scope.rowArr[row.row - 1].playerBoardData[col.col + 9].cellStatus = data.playerAttackStatus;
                    $scope.rowArr[row.row - 1].playerBoardData[col.col + 9].cellType = data.playerAttackCellType;

                    $scope.rowArr[data.computorAttackRow - 1].playerBoardData[data.computorAttackCol - 1].cellStatus = data.computorAttackStatus;
                    $scope.rowArr[data.computorAttackRow - 1].playerBoardData[data.computorAttackCol - 1].cellType = data.computorAttackCellType;
                }
                pLoss = data.playerLoss;
                cLoss = data.computorLoss;
                if (pLoss) { alert("Loss !") }
                else if (cLoss) { alert("Win !") }

            }).error(function (data) {

            });
    }
});
