define(['dashboard'], function (dashboard) {
    dashboard.registerController('dashboardController', ['$scope', function ($scope) {
        $scope.version = 'PPTS V2.0';
    }]);
});