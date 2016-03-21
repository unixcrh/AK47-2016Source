(function () {
    'use strict';

    mcs = mcs || {};
    mcs.module = mcs.module || angular.module('mcs', ['mcs.form']);
    mcs.module.constant('mcsComponentConfig', {
        rootUrl: mcs.config.rootUrl
    });
    mcs.form = mcs.form || angular.module('mcs.form', []);

    return mcs;

})();