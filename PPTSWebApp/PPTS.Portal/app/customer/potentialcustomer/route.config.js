define([], function () {
    var states = {
        'customer': {
            url: '/customer',
            templateUrl: 'app/customer/potentialcustomer/customer-list/customer-list.html',
            controller: 'customerListController',
            dependencies: ['app/customer/potentialcustomer/customer-list/customer-list.controller']
        },
        'customerAdd': {
            url: '/customer/add',
            templateUrl: 'app/customer/potentialcustomer/customer-add/customer-add.html',
            controller: 'customerAddController',
            dependencies: ['app/customer/potentialcustomer/customer-add/customer-add.controller']
        }
    };

    return states;
});