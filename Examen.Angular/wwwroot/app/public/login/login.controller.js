﻿(function () {
    'use strict';

    angular.module('app').controller('loginController', loginController);

    loginController.$inject = ['$http', 'authenticationService', 'configService', '$state'];

    function loginController($http, authenticationService, configService, $state) {
        var vm = this;
        vm.user = {};
        vm.title = 'Iniciar sesión';
        vm.login = login;

        init();

        function init() {
            if (configService.getLogin()) $state.go("corporacion");
            authenticationService.logout();
        }

        function login() {
            authenticationService.login(vm.user).then(function (result) {
                vm.showError = false;
                $state.go("corporacion");
            }, function (error) {
                vm.showError = true;
            });
        }

    }

})();