'use strict';
angular.module('scores.app').service('scoreService', ['$http', function($http) {

    var ScoreServiceUrl = "score_get.php";

    this.loadScores = function() {
        let ticks = ((new Date().getTime() * 10000) + 621355968000000000);
        return $http.get(ScoreServiceUrl + "?" + ticks);
    };

}]);