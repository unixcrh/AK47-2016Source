define(['customer'], function (customer) {

    customer.registerFactory('customerDataService', ['$resource', 'pptsConfiguration', function ($resource, pptsConfiguration) {

        var resource = $resource(pptsConfiguration.customerApiServerPath + 'api/potentialcustomers/:operation/:id', { operation: '@operation', id: '@id' }, { 'post': { method: 'POST' } });

        resource.getAllCustomers = function (criteria, success, error) {
            resource.post({ operation: 'getAllCustomers' }, criteria, success, error);
        }

        resource.getCustomer = function (customerId) {
            resource.query({ operation: 'getCustomerBaseinfo', id: customerId }, function (customer) {

            });
        };

        resource.addCustomer = function () {
            resource.query({ operation: 'createCustomer' }, function (customer) {

            });
        }

        resource.editCustomer = function (customerId) {
            resource.query({ operation: 'updateCustomer', id: customerId }, function (customer) {

            });
        }

        resource.saveCustomer = function (customer, customerId) {
            if (!customerId) {
                resource.save({ operation: 'createCustomer' }, customer, function (result) {

                });
            } else {
                resource.save({ operation: 'updateCustomer', id: customerId }, customer, function (result) {

                });
            }
        };

        return resource;
    }]);
});