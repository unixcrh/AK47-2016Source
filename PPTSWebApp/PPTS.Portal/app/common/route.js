define(['app/common/util/loader'], function (loader) {

    var route = {};
    route.routeConfig = function (routeConfigPaths, $stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/dashboard');

        $stateProvider.state('dashboard', {
            url: '/dashboard',
            templateUrl: 'app/dashboard/dashboard.html',
            controller: 'dashboardController',
            resolve: loader(['app/dashboard/dashboard.controller'])
        });

        $.each(routeConfigPaths, function (index) {
            require([routeConfigPaths[index]], function (states) {
                if (states != undefined) {
                    $.each(states, function (route, state) {
                        $stateProvider.state(route, {
                            url: state.url,
                            templateUrl: state.templateUrl,
                            controller: state.controller,
                            resolve: loader(state.dependencies)
                        });
                    });
                }
            });
        });
    }

    return route;
});
