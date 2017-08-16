(function () {
    'use strict';

    angular.module('app', [
        'ui.router',
        'LocalStorageModule',
        'ui.bootstrap'
    ]);

})();
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
(function () {
    'use strict';

    angular.module('app').config(setup).run(run);

    setup.$inject = ['$compileProvider']

    function setup($compileProvider) {
        $compileProvider.debugInfoEnabled(false);
    }

    run.$inject = ['$http', '$state', 'localStorageService', 'configService'];

    function run($http, $state, localStorageService, configService) {
        var user = localStorageService.get('userToken');

        if (user && user.token) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + localStorageService.get('userToken').token;
            configService.setLogin(true);
        } else $state.go('login');

    }

})();
(function () {
    'use strict';

    angular.module('app').controller('applicationController', applicationController);

    applicationController.$inject = ['$state', '$scope', 'configService', 'authenticationService', 'localStorageService'];

    function applicationController($state, $scope, configService, authenticationService, localStorageService) {
        var vm = this;
        vm.validate = validate;
        vm.logout = logout;
        vm.iniciarSesion = iniciarSesion;
        vm.corporacion = corporacion;
        vm.miembro = miembro

        $scope.init = function (url) {
            configService.setApiUrl(url);
        }

        function validate() {
            return configService.getLogin();
        }

        function logout() {
            authenticationService.logout();
        }

        function corporacion() {
            $state.go("corporacion");
        }

        function miembro() {
            $state.go("miembro");
        }

        function iniciarSesion() {
            $state.go("login");
        }
        
    }


})();