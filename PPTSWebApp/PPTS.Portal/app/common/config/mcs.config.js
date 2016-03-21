var mcs = mcs || {};

(function () {
    'use strict';

    var env = 'test', rootUrl = 'http://localhost';

    switch (env) {
        case 'dev':
            rootUrl = 'http://localhost:1399/';
            break;
        case 'local':
        default:
            rootUrl = 'http://localhost';
            break;
        case 'test':
            rootUrl = 'http://10.1.56.80';
            break;
        case 'prod':
            rootUrl = '';
            break;
    }

    mcs.config = mcs.config || {
        fileTypes: { css: 'css', javascript: 'js' },
        routeConfigPaths: [
            //'app/dashboard/route.config',
            'app/customer/student/route.config',
            'app/customer/potentialcustomer/route.config'
        ],
        dataServiceConfig: {
            customerDataService: 'app/customer/potentialcustomer/potentialcustomer.dataService',
            studentDataService: 'app/customer/student/student.dataService'
        },
        rootUrl: env == 'dev' ? "http://localhost/mcsweb/" : rootUrl + '/mcsweb/',
        componentBaseUrl: env == 'dev' ? rootUrl : rootUrl + '/mcsweb/',
        customerApiBaseUrl: env == 'dev' ? rootUrl : rootUrl + '/customerapi/'
    };

    return mcs;
})();