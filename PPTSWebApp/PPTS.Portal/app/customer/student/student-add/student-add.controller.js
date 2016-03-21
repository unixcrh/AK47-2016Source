define(['customer', mcs.config.dataServiceConfig.studentDataService], function (customer) {
    customer.registerController('studentAddController',
        ['$scope', 'blockUI', 'studentDataService',
            function ($scope, blockUI, studentDataService) {
                var vm = {};
                $scope.vm = vm;
            }]);
});
