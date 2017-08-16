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