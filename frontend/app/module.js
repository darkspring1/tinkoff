/**
 * Created by darkspring on 04/06/2016.
 */


angular.module('app', [ 'ngMessages', 'ngResource' ])
.controller('mainController', ['$scope', '$resource', '$log', function($scope, $resource, $log){


    var url = $resource('/api/url', null, {
        create: {method:'POST', isArray: true }
    });

    $scope.origin = "http://cribcy.com";
    $scope.shorten = function(form) {
        if(form.$valid){
            $scope.loading = true;
            $scope.urls = url.create({ originUrl: $scope.origin });
            $scope.urls.$promise
                .catch($log.error)
                .finally(function(){
                    $scope.loading = false;
                })
        }
        else {
            form.$setDirty()
        }
    }

}])