define([settings.modules.ppts, settings.modules.main], function (ppts, main) {
    ppts.registerController('mainController', mainController);

    mainController.$inject = ['$scope'];

    function mainController($scope) {
        $scope.name = 'hello';
    }

});