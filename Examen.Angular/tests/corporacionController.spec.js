
describe('corporacionController', function()
{
    var controller, service;

    beforeEach(module('app'));


    beforeEach(inject(function($controller, _dataService_,$q){
        service = _dataService_;

        spyOn(service, "list").and.callFake(function(currentPage,itemsPerPage){
            var deferred = $q.defer();
            deferred.resolve('Response');
            return deferred.promise;
        });

        controller = $controller('corporacionController' ,
        {
            _dataService_: service
        });

        describe('List Test', function() {

            it('List', inject(function()
            {
                controller.list(1,25);
                expect(controller.corporacionList.length).toBeGreaterThan(0);
            }));
        });

    })
    );

});