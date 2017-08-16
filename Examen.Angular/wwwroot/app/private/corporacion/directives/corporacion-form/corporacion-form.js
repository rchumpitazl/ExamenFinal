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