'use strict';

angular.module('scores.app')
    .controller('ScoreController', ['$scope', 'scoreService', 'uiGridConstants', function($scope, scoreService, uiGridConstants) {
        $scope.gridOptions = {
            enableSorting: true,
            enableFiltering: true,
            columnDefs: [
                {
                    field: 'player_name',
                    suppressHideColumn: true,
                    enableHiding: false
                },
                {
                    field: 'score',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 0
                    },
                    sortingAlgorithm: function(a, b, rowA, rowB, direction) {
                        return b - a;
                    },
                    enableHiding: false
                }
            ]
        };

        $scope.loadScores = function() {
            scoreService.loadScores().then(function(response) {
                let data = response.data;
                for (let i = 0; i < data.length; i++)
                {
                    var scoreItem = data[i];
                    scoreItem.player_name = scoreItem.player_name.substring(0, scoreItem.player_name.lastIndexOf('('));
                    scoreItem.score = parseInt(scoreItem.score);
                    scoreItem.death_position = parseInt(scoreItem.death_position);
                }

                $scope.gridOptions.data = data;
            })
        };
    }]);