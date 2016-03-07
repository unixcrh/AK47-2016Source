define([settings.modules.ppts, settings.modules.student], function (ppts, student) {
    ppts.registerController('studentListController', studentListController);

    studentListController.$inject = ['$scope'];

    function studentListController($scope) {
        $scope.name = 'hello';
    }
});
