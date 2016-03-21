define(['customer', mcs.config.dataServiceConfig.studentDataService], function (customer) {
    customer.registerController('studentListController', [
        '$scope', 'blockUI', 'studentDataService',
        function ($scope, blockUI, studentDataService) {
            var vm = {};
            $scope.vm = vm;

            vm.criteria = { name: '', code: '', teacherZX: '', teacherXG: '', contact: '', pagedParam: { page: 1 } };

            var successCallback = function (result) {
                vm.pagedList = result.data.pagedList;
                vm.criteria.pagedParam = result.data.pagedList.pagedParam;
                blockUI.stop();
            }

            var errorCallback = function (result) {

            }

            vm.search = (function () {
                blockUI.start();
                studentDataService.getAllStudents($scope.vm.criteria, successCallback, errorCallback);
            })();


            vm.pageChanged = function () {
                blockUI.start();
                studentDataService.getAllStudents($scope.vm.criteria, successCallback, errorCallback);
            };
        }]);
});
