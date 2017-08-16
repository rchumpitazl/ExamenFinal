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