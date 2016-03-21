define(['angular'], function (ng) {
    var dashboard = ng.module('ppts.dashboard', []);

    dashboard.config(function ($stateProvider, $urlRouterProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        dashboard.registerController = $controllerProvider.register;
        dashboard.registerDirective = $compileProvider.directive;
        dashboard.registerFilter = $filterProvider.register;
        dashboard.registerFactory = $provide.factory;
        dashboard.registerService = $provide.service;
    });

    return dashboard;
});