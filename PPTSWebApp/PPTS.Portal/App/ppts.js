define(['angular', 'pptsRoute', 'mcsComponent', 'dashboard', 'customer'], function (ng, route) {
    var ppts = ng.module('ppts', [
        'ui.router', 'ngResource', 'blockUI', 'ui.bootstrap', 'mcs',
        'ppts.dashboard', 'ppts.customer']);

    ppts.controller('appController', ['$rootScope', '$scope', function ($rootScope, $scope) {
      
    }]);

    //define all app constant value in here
    ppts.constant('pptsConfiguration', {
        customerApiServerPath: mcs.config.customerApiBaseUrl
    });


    ppts.run(['$rootScope', '$state', '$stateParams',
        function ($rootScope, $state, $stateParams) {
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ]);

    ppts.config(function ($stateProvider, $urlRouterProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, $httpProvider) {
        ppts.registerController = $controllerProvider.register;
        ppts.registerDirective = $compileProvider.directive;
        ppts.registerFilter = $filterProvider.register;
        ppts.registerFactory = $provide.factory;
        ppts.registerService = $provide.service;

        route.routeConfig(mcs.config.routeConfigPaths, $stateProvider, $urlRouterProvider);
    });

    return ppts;
});