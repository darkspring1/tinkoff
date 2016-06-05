/**
 * Created by darkspring on 05/06/2016.
 */

angular.module('app')
    .factory("api", ['$resource',
        function($resource) {
            return $resource('/api/url', null, {
                create: {method:'POST', isArray: true }
            });
        }]);