/**
 * Created by darkspring on 04/06/2016.
 */


angular.module('app', [ 'ngMessages' ])
.controller('mainController', ['$scope', function($scope){

    $scope.shorten = function(form) {

        if(form.$valid){

        }
        else {
            form.$setDirty()
        }
    }

}])