/**
 * Created by darkspring on 04/06/2016.
 */

angular.module('app', [ 'ngMessages', 'ngResource', 'ui.router' ])
    .config(['$httpProvider', '$locationProvider', '$urlRouterProvider', '$stateProvider',
        function ($httpProvider, $locationProvider, $urlRouterProvider, $stateProvider) {

            $locationProvider.html5Mode(true);
            $urlRouterProvider.otherwise('/');

            $stateProvider
                .state('main', {
                    url: '/',
                    templateUrl: 'partial/main.html',
                    controller: 'mainController'
                })
                .state('stat', {
                    url: '/stat/:id',
                    templateUrl: 'partial/stat.html',
                    controller: 'statController'
                })

        }])

    .controller('mainController', ['$scope', '$resource', '$log', 'api', function($scope, $resource, $log, api){
        $scope.shorten = function(form) {
            form.$setSubmitted();
            if(form.$valid){
                $scope.urls = api.create({ originUrl: $scope.origin });
                $scope.urls.$promise.catch($log.error);
            }
        }
    }])

    .controller('statController', ['$scope', 'api', '$log', '$stateParams', function($scope, api, $log, $stateParams){
        $scope.url = api.get({ id: $stateParams.id });
    }])