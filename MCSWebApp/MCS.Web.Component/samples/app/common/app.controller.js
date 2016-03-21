angular.module('app.common').controller('AppController', ['$scope',
    function ($scope) {
        var vm = {};

        $scope.changeSettingNavbar = function () {
            ace.settings.navbar_fixed(!ace.settings.is('navbar', 'fixed'), true);
        };
        $scope.changeSettingBreadcrumbs = function () {
            ace.settings.breadcrumbs_fixed(!ace.settings.is('breadcrumbs', 'fixed'), true);
        };
        $scope.changeSettingMainContainer = function () {
            ace.settings.main_container_fixed(!ace.settings.is('main-container', 'fixed'), true);
        };
        $scope.changeSettingSidebar = function () {
            ace.settings.sidebar_fixed(!ace.settings.is('sidebar', 'fixed'), true);
        };
        $scope.changeSettingCompact = function () {
            $('#sidebar li').addClass('hover').filter('.open').removeClass('open').find('> .submenu').css('display', 'none');
            $('#sidebar').toggleClass('compact');
        };

        //暂时未实现
        $scope.changeSettingSkin = function () {
            ace.settings.set('skin', 'skin-1');
        };
    }
]);