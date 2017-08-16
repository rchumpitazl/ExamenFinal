(function () {
    angular
        .module('app')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$state', 'localStorageService', 'configService', '$q'];

    function authenticationService($http, $state, localStorageService, configService, $q) {
        var service = {};

        service.login = login;
        service.logout = logout;

        return service;

        function login(user) {

            var defer = $q.defer();
            var url = configService.getApiUrl() + '/Token';
            var data = "email=" + user.userName + "&contrasena=" + user.password;

            $http.post(url, data, {
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                }
            }).then(function (result) {
                $http.defaults.headers.common.Authorization = 'Bearer ' + result.data.access_token;
                localStorageService.set('userToken', {
                    token: result.data.access_token,
                    userName: user.userName
                });
                configService.setLogin(true);
                defer.resolve(true);
                }, function error(response) {
                    defer.reject(false);
                });

            return defer.promise;

        }

        function logout() {
            $http.defaults.headers.common.Authorization = '';
            localStorageService.remove('userToken');
            configService.setLogin(false);
        }
    }

})();
(function () {

    angular
        .module('app')
        .factory('dataService', dataService);

    dataService.$inject = ['$http'];

    function dataService($http) {

        var service = {};

        service.getData = getData;
        service.postData = postData;
        service.putData = putData;
        service.deleteData = deleteData;

        return service;

        function getData(url) {
            return $http.get(url);
        }

        function postData(url, data) {
            return $http.post(url, data);
        }

        function putData(url, data) {
            return $http.put(url, data);
        }

        function deleteData(url, data) {
            return $http.delete(url, data);
        }

    }



})();
(function () {
    'use strict';

    angular
        .module('app')
        .factory('configService', configService);

    function configService() {

        var service = {};

        var apiUrl = undefined;
        var isLogged = false;
        service.setLogin = setLogin;
        service.getLogin = getLogin;
        service.setApiUrl = setApiUrl;
        service.getApiUrl = getApiUrl;

        return service;

        function setLogin(state) {
            isLogged = state;
        }

        function getLogin() {
            return isLogged;
        }

        function getApiUrl() {
            return apiUrl;
        }

        function setApiUrl(url) {
            apiUrl = url;
        }

    }

})();
(function () {
    angular.module('app').directive('modalPanel', modalPanel);

    function modalPanel() {
        return {
            templateUrl: 'app/components/modal/modal-directive.html',
            restrict: 'E',
            transclude: true,
            scope: {
                title: '@',
                buttonTitle: '@',
                saveFunction: '=',
                closeFunction: '=',
                readOnly: '=',
                isDelete: '='
            }
        };
    }

})();
(function () {
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
(function () {
    'use strict';

    angular.module('app').controller('corporacionController', corporacionController);

    corporacionController.$inject = ['dataService', 'configService', '$state'];

    function corporacionController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;

        vm.corporacion = {};
        vm.corporacionList = [];
        vm.modalButtonTitle = '';
        vm.readOnly = false;
        vm.isDelete = false;
        vm.modalTitle = '';
        vm.showCreate = false;

        vm.totalRecords = 0;
        vm.itemsPerPage = 10;
        vm.currentPage = 1;
        vm.maxSize = 10;

        vm.getCorporacion = getCorporacion;
        vm.create = create;
        vm.edit = edit;
        vm.corporacionDelete = corporacionDelete;
        vm.list = list;
        vm.pageChanged = pageChanged;

        init();

        function init() {
            if (!configService.getLogin()) return $state.go('login');
            configurePagination();
        }
        
        function list() {
            dataService.getData(apiUrl + '/corporacion/' + vm.currentPage + '/' + vm.itemsPerPage)
                .then(function (result) {
                    vm.corporacionList = result.data;
                }, function (error) {
                    vm.corporacionList = [];
                    console.log(error);
                });
        }

        function getCorporacion(id) {
            vm.corporacion = null;
            dataService.getData(apiUrl + '/corporacion/' + id)
                .then(function (result) {
                    vm.corporacion = result.data;
                }, function (error) {
                    vm.corporacion = null;
                    console.log(error);
                });
        }

        function updateCorporacion() {
            if (!vm.corporacion) return;

            dataService.putData(apiUrl + '/corporacion', vm.corporacion)
                .then(function (result) {
                    vm.corporacion = {};
                    list();
                    closeModal();
                }, function (error) {
                    vm.corporacion = {};
                    console.log(error);
                });
        }
        
        function createCorporacion() {
            if (!vm.corporacion) return;

            dataService.postData(apiUrl + '/corporacion', vm.corporacion)
                .then(function (result) {
                    //getCorporacion(result.data.id)
                    vm.currentPage = 1;
                    pageChanged();
                    vm.showCreate = true;
                    closeModal();
                }, function (error) {          
                    console.log(error);
                });
        }

        function deleteCorporacion() {
            
            dataService.deleteData(apiUrl + '/corporacion/' + vm.corporacion.corp_no)
                .then(function (result) { 
                    list();
                    closeModal();
                }, function (error) {
                    console.log(error);
                });
        }

        function create() {
            vm.corporacion = {};
            vm.modalTitle = 'Nueva corporacion';
            vm.modalButtonTitle = 'Crear';
            vm.readOnly = false;
            vm.modalFunction = createCorporacion;
            vm.isDelete = false;
        }

        function edit() {
            vm.showCreate = false;
            vm.modalTitle = 'Editar Corporacion';
            vm.modalButtonTitle = 'Actualizar';
            vm.readOnly = false;
            vm.modalFunction = updateCorporacion;
            vm.isDelete = false;
        }

        function corporacionDelete() {
            vm.showCreate = false;

            vm.modalTitle = 'Borrar Corporacion';
            vm.modalButtonTitle = 'Borrar';
            vm.readOnly = false;
            vm.modalFunction = deleteCorporacion;
            vm.isDelete = true;
        }

        function closeModal() {
            angular.element('#modal-container').modal('hide');
        }

        function pageChanged() {       
            list();
        }

        function configurePagination() {
            var widthScreen = (window.innerWidth > 0) ? window.innerWidth : screen.width;
            if (widthScreen < 420) vm.maxSize = 5;
            getTotalRecords();
        }

        function getTotalRecords() {
            dataService.getData(apiUrl + '/corporacion/contar')
                .then(function (result) {
                    vm.totalRecords = result.data;
                    list();
                }, function (error) {
                    vm.totalRecords = 0;
                    console.log(error);
                });
        }
    }

})();
(function () {
    'use strict';

    angular.module('app').directive('corporacionCard', corporacionCard);

    function corporacionCard() {
        return {
            restrict: 'E',
            transclude: true,
            scope: {
                corpNo: '@',
                corpName: '@',
                street: '@',
                city: '@',
                country: '@',
                stateProv: '@',
                mailCode: '@',
                phoneNo: '@',
                exprDt: '@',
                corpCode: '@'
            },
            templateUrl: 'app/private/corporacion/directives/corporacion-card/corporacion-card.html',
            controller: directiveController
        };
    }

    function directiveController() {

    }

})();
(function () {
    'use strict';

    angular.module('app').directive('corporacionForm', corporacionForm);

    function corporacionForm() {
        return {
            restrict: 'E',
            scope: {
                corporacion: '='
            },
            templateUrl: 'app/private/corporacion/directives/corporacion-form/corporacion-form.html'
        };
    }

})();