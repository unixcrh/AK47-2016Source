define(['angular', settings.modules.route], function (ng, route) {
    var ppts = ng.module('ppts', ['ui.router', 'ngResource']);

    ppts.controller('pptsController', ['$rootScope', '$scope', function ($rootScope, $scope) {
        //this part is for to authenticate current user when view state changed
        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams, options) {
            // $scope.authenicateUser($location.path(),
            //     $scope.authenicateUserComplete, $scope.authenicateUserError);

        });

        $scope.authenicateUser = function (route, successFunction, errorFunction) {
            var authenication = new Object();
            authenication.route = route;
            $scope.AjaxGet(authenication, "/api/main/AuthenicateUser",
                successFunction, errorFunction);
        };

        $scope.authenicateUserComplete = function (response) {
            if (response.IsAuthenicated == false) {
                window.location = "/index.html";
            }
        }



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

    }]);

    ppts.config(configure);
    configure.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider', '$controllerProvider', '$compileProvider', '$filterProvider', '$provide'];



    return ppts;


    function configure($stateProvider, $urlRouterProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        ppts.registerController = $controllerProvider.register;
        ppts.registerDirective = $compileProvider.directive;
        ppts.registerFilter = $filterProvider.register;
        ppts.registerFactory = $provide.factory;
        ppts.registerService = $provide.service;


        route.routeConfig(settings.routeConfigPaths, $stateProvider, $urlRouterProvider);
    }
});