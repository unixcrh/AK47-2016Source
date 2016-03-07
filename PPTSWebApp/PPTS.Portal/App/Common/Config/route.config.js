define(['requireLoader'], function (loader) {

    var route = {};
    route.routeConfig = function (routeConfigPaths, $stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/main');

        $stateProvider.state('main', {
            url: '/main',
            templateUrl: './app/main/main.html',
            controller: 'mainController',
            resolve: loader(['app/main/main.controller.js'])
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
