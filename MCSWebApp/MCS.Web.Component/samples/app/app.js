'use strict';

/*
angular.module('app.constants', []);
angular.module('app.services', []);
angular.module('app.controllers', []);
angular.module('app.directives', []);
angular.module('app.common.directives', []);
angular.module('app.common.services', []);*/

angular.module('app.common', []);

angular.module('app.main', []);

angular.module('app.component', []);

/*
angular.module('app.student.services', []);
angular.module('app.student.controllers', []);
*/

angular.module('app.lib', ['ngSanitize', 'ui.select', 'app.common']);

/*
angular.module('app', ['ngCookies', 'ngSanitize',
    'ui.router', 'ui.bootstrap', 'blockUI',
    'app.constants', 'app.services', 'app.controllers', 'app.directives',
    'app.common.controllers', 'app.common.directives', 'app.common.services',
    'app.dashboard.controllers', 'app.dashboard.services',
    'app.student.controllers', 'app.student.services'
])*/
angular.module('app', [
    'ngCookies', 'ngSanitize', 'ui.router', 'ui.bootstrap', 'blockUI', 'mcs',
    'app.common','app.main', 'app.lib', 'app.component'
])
.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise("/main");

    $stateProvider.state('main', {
        url: '/main',
        templateUrl: 'app/main/main.html'
    }).state('ui-select', {
        url: '/ui-select',
        templateUrl: 'app/lib/angular-ui-select/main.html',
        controller: 'AppUISelectController'
    }).state('vertical-tabs', {
        url: '/vertical-tabs',
        templateUrl: 'app/lib/vertical-tabs/main.html'
    }).state('buttons', {
        url: '/buttons',
        templateUrl: 'app/component/mcs-button/main.html',
        controller: 'MCSButtonController',
        controllerAs: 'vm'
    }).state('checkboxgroup', {
        url: '/checkboxgroup',
        templateUrl: 'app/component/mcs-checkboxgroup/main.html',
        controller: 'MCSCheckboxGroupController',
        controllerAs: 'vm'
    }).state('radiobuttongroup', {
        url: '/radiobuttongroup',
        templateUrl: 'app/component/mcs-radiobuttongroup/main.html',
        controller: 'MCSRadiobuttonGroupController',
        controllerAs: 'vm'
    });
}])
.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.put['Content-Type'] = 'application/x-www-form-urlencoded';
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

    // Override $http service's default transformRequest
    $httpProvider.defaults.transformRequest = [function (data) {
        /**
         * The workhorse; converts an object to x-www-form-urlencoded serialization.
         * @param {Object} obj
         * @return {String}
         */
        var param = function (obj) {
            var query = '';
            var name, value, fullSubName, subName, subValue, innerObj, i;

            for (name in obj) {
                value = obj[name];

                if (value instanceof Array) {
                    for (i = 0; i < value.length; ++i) {
                        subValue = value[i];
                        fullSubName = name + '[' + i + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value instanceof Object) {
                    for (subName in value) {
                        subValue = value[subName];
                        fullSubName = name + '[' + subName + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value !== undefined && value !== null) {
                    query += encodeURIComponent(name) + '='
                            + encodeURIComponent(value) + '&';
                }
            }

            return query.length ? query.substr(0, query.length - 1) : query;
        };

        return angular.isObject(data) && String(data) !== '[object File]'
                ? param(data)
                : data;
    }];
}])
.config(['blockUIConfig', function (blockUIConfig) {
    blockUIConfig.message = '正在加载数据 ...';
}]);