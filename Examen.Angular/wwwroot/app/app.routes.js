(function () {
    'use strict';

    angular.module('app').config(routeConfig);

    routeConfig.$inject = ['$stateProvider', '$urlRouterProvider'];

    function routeConfig($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state("login", {
                url: '/login',
                templateUrl: 'app/public/login/index.html'
            })
            .state("miembro", {
                url: '/miembro',
                templateUrl: 'app/private/miembro/index.html'
            })
            .state("corporacion", {
                url: '/corporacion',
                templateUrl: 'app/private/corporacion/index.html'
            })
            .state("otherwise", {
                url: '*path',
                templateUrl: 'app/home.html'
            });
    }

})();