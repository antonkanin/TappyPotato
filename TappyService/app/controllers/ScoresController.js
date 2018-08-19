'use strict';

angular.module('scores.app')
    .controller('ScoresController', ['$scope', 'scoresService', 'uiGridConstants', function($scope, scoresService, uiGridConstants) {
        $scope.gridOptions = {
            enableSorting: true,
            columnDefs: [
                {
                    field: 'player_name',
                },
                {
                    field: 'score',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 0
                    },
                    suppressRemoveSort: true,
                    sortingAlgorithm: function(a, b, rowA, rowB, direction) {
                        return b - a;
                    }
                }
            ]
        };

        $scope.loadScores = function() {
            scoresService.loadScores().then(function(response) {
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