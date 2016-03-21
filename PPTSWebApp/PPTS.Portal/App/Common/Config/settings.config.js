var settings = settings || {};

(function () {
    'use strict';

    //you have to add every config file of angular module in here
    settings.routeConfigPaths = [
        'app/main/route.config.js',
        'app/student/route.config.js'
    ];

    return settings;
})();
