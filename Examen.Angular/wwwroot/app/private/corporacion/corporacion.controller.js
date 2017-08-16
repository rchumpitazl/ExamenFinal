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