'use strict';
angular.module('scores.app').service('scoreService', ['$http', function($http) {

    var ScoreServiceUrl = "score_get.php";

    this.loadScores = function() {
        return $http.get(ScoreServiceUrl);
    };

}]);