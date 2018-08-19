'use strict';
angular.module('scores.app').service('scoresService', ['$http', function($http) {

    var ScoresServiceUrl = "score_get.php";

    this.loadScores = function() {
        return $http.get(ScoresServiceUrl);
    };

}]);