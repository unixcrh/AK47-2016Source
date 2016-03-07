define([settings.modules.ppts, settings.modules.student], function (ppts, student) {
    ppts.registerController('studentAddController', studentAddController);

    studentAddController.$inject = ['$scope'];

    function studentAddController($scope) {
        $scope.name = 'hello';
    }
});
