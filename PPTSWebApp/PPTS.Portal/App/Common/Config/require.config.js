(function () {
    var framework = mcs.config.componentBaseUrl;
    require.config({
        baseUrl: '.',
        paths: {
            angular:framework + 'libs/angular-1.5.0/angular.min',
            uiRouter:framework+ 'libs/angular-ui-router-0.2.18/release/angular-ui-router.min',
            ngResource: framework +'libs/angular-resource-1.5.0/angular-resource.min',
            blockUI:framework + 'libs/angular-block-ui-0.2.2/dist/angular-block-ui.min',
            jquery:framework + 'libs/jquery-2.2.1/dist/jquery.min',
            bootstrap: framework + 'libs/bootstrap-3.3.5/js/bootstrap.min',
            uiBootstrap: framework + 'libs/ui-bootstrap-1.1.0/ui-bootstrap-1.1.0.min',
            uiBootstrapTpls: framework + 'libs/ui-bootstrap-1.1.0/ui-bootstrap-tpls-1.1.0.min',
            ace:framework + 'libs/ace-1.2.3/ace.min',
            aceExtra: framework + 'libs/ace-1.2.3/ace-extra.min',
            mcsComponent: framework + 'libs/mcs-component-1.0.1/mcs.component',
            ppts: 'app/ppts',
            pptsRoute: 'app/common/route',
            dashboard: 'app/dashboard/ppts.dashboard',
            customer: 'app/customer/ppts.customer'
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
            },
            uiBootstrap: {
                exports: 'uiBootstrap',
                deps:['angular']
            },
            uiBootstrapTpls: {
                exports: 'uiBootstrapTpls',
                deps:['angular']
            },
            mcsComponent: {
                exports: 'mcsComponent',
                deps: ['angular']
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
                'uiBootstrap',
                'uiBootstrapTpls',
                'mcsComponent',
                'ppts'                
            ], function (ng) {
                    ng.element(document).ready(function () {
                        ng.bootstrap(document, ['ppts']);
                });
            });
        }
    });
})();

