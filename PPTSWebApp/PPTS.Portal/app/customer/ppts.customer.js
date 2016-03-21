define(['angular'], function (ng) {
    var customer = ng.module('ppts.customer', []);

    customer.config(function ($stateProvider, $urlRouterProvider, $locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        customer.registerController = $controllerProvider.register;
        customer.registerDirective = $compileProvider.directive;
        customer.registerFilter = $filterProvider.register;
        customer.registerFactory = $provide.factory;
        customer.registerService = $provide.service;
    });

    customer.constant('customerConfig', {
        customerDataService: 'app/customer/potentialcustomer/potentialcustomer.dataService',
        studentDataService: 'app/customer/student/student.dataService'
    });

    return customer;
});