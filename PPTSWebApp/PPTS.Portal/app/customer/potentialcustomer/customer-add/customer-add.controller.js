define(['customer', mcs.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('customerAddController', [
            '$scope',
            'customerDataService',
            function ($scope, customerDataService) {
                var vm = {};
                $scope.vm = vm;

            }]);
    });