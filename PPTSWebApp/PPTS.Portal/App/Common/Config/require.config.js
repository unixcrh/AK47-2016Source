require.config({
    baseUrl: 'http://localhost/mcsweb/',
    paths: {
        angular: 'vendors/angular-1.5.0/angular.min',
        uiRouter: 'vendors/angular-ui-router-0.2.18/release/angular-ui-router.min',
        ngResource: 'vendors/angular-resource-1.5.0/angular-resource.min',
        blockUI: 'vendors/angular-block-ui-0.2.2/dist/angular-block-ui.min',
        jquery: 'vendors/jquery-2.2.1/dist/jquery.min',
        bootstrap: 'vendors/bootstrap-3.3.6/dist/js/bootstrap.min',
        ace: 'vendors/ace-1.2.3/ace.min',
        aceExtra: 'vendors/ace-1.2.3/ace-extra.min',
        requireLoader: 'assets/scripts/utils/mcs.requireLoader'
    },
    shim: {
        angular: {
            exports: 'angular'
        },
        blockUI: {
            exports: 'blockUI',
            deps: ['angular']
        },
        ngResource: {
            exports: 'ngResource',
            deps: ['angular']
        },
        uiRouter: {
            exports: 'uiRouter',
            deps: ['angular']
        },
        jquery: { exports: 'jquery' },
        bootstrap: {
            exports: 'bootstrap',
            deps: ['jquery']
        },
        ace: {
            exports: 'ace',
            deps: ['jquery']
        },
        aceExtra: {
            exports: 'aceExtra',
            deps: ['ace']
        }
    },
    callback: function () {
        require(['angular',
            'blockUI',
            'uiRouter',
            'ngResource',
            'jquery',
            'bootstrap',
            'ace',
            'aceExtra',
            'app/common/config/settings.config.js',
            'app/ppts.js'
        ], function (ng) {
                ng.element(document).ready(function () {
                    ng.bootstrap(document, ['ppts']);
            });
        });
    }
});