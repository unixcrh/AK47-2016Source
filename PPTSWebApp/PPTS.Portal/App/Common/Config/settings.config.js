var settings = settings || {};

(function () {
    'use strict';

    settings.baseUrl = 'app/';

    settings.modules = {
        ppts: 'app/ppts.js',
        route: 'app/common/config/route.config.js',
        main: 'app/main/main.js',
        student: 'app/student/student.js'
    };

    //you have to add every config file of angular module in here
    settings.routeConfigPaths = [
        'app/main/route.config.js',
        'app/student/route.config.js'
    ];

    return settings;
})();
