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
(function () {
    'use strict';

    mcs.form.constant('buttonConfig', {
        types: ['add', 'edit', 'delete', 'search', 'save', 'cancel'],
        defaultTexts: ['新 增', '编 辑', '删 除', '查 询', '保 存', '取 消'],
        sizes: ['mini', 'medium', 'large'],
        sizeCss: ['btn-xs', 'btn-sm', 'btn-lg'],
        //buttonClass: ['btn-primary', 'btn-success', 'btn-danger', 'btn-info'],
        iconClass: ['fa-plus', 'fa-pencil', 'fa-trash-o', 'fa-search', 'fa-save', 'fa-undo']
    })
    .directive('mcsButton', function (buttonConfig) {

        return {
            restrict: 'E',
            scope: {
                type: '@',
                text: '@',
                icon: '@',
                size: '@',
                css: '@',
                click: '&'
            },

            template: '<button class="btn mcs-width-130" ng-click="click()"><i class="ace-icon fa"></i><span></span></button>',
            replace: true,
            transclude: true,
            link: function ($scope, $elem, $attrs, $ctrl) {
                var index = buttonConfig.types.indexOf($scope.type);
                var buttonCss = 'btn-info';
                switch ($scope.type) {
                    case 'search':
                        buttonCss = 'btn-primary';
                        break;
                    case 'delete':
                        buttonCss = 'btn-danger';
                        break;
                    case 'save':
                        buttonCss = 'btn-success';
                        break;
                    case 'cancel':
                        buttonCss = 'btn-light';
                        break;
                }
                $elem.addClass(buttonCss);
                if ($scope.size) {
                    var sizeIndex = buttonConfig.sizes.indexOf($scope.size);
                    $elem.addClass(buttonConfig.sizeCss[sizeIndex]);
                }
                if ($scope.icon && index === -1) {
                    if ($scope.icon.indexOf('fa-') == 0) {
                        $elem.find('i').addClass($scope.icon);
                    } else {
                        $elem.find('i').addClass('fa-' + $scope.icon);
                    }
                } else {
                    $elem.find('i').addClass(buttonConfig.iconClass[index]);
                }
                if ($scope.css) {
                    $elem.addClass($scope.css);
                }
                $elem.find('span').text($scope.text || buttonConfig.defaultTexts[index]);
            }
        };
    });
})();
(function() {
    mcs.form
        .directive('mcsLinkList', function($compile) {
            return {
                restrict: 'A',
                scope: {
                    data: '=',
                    click: '&'

                },
                template: '<button type="button" class="btn btn-link" data-toggle="button"  ng-repeat="item in data" ng-click="click({eventArgs:$event})">{{item.id}}</button>',

                link: function($scope, ele, attrs, ctrl) {


                }

            }
        })

    .directive('mcsCascadingSelect', function($compile) {

    return {
        restrict: 'E',
        scope: {
            division: '=data',
            ngModel: '=',
            required: '@'
        },

        templateUrl: 'province-temp.html',
        replace: true,

        link: function($scope, iElm, iAttrs, controller) {


        }
    };
});
})();

(function () {
    'use strict';

    mcs.form.directive('mcsCheckboxGroup', function () {
        return {
            restrict: 'EA',
            scope: {
                data: '=',
                click: '&',
                category: '@'
            },
            template: '<label class="checkbox-inline"  ng-repeat="item in data"><input ng-click="click({category:category,key:item.key,eventargs:$event})" type="checkbox" class="ace" ng-checked="item.checked"><span class="lbl"></span> {{item.value}}</label>',
            link: function ($scope, $elem, $attrs, $ctrl) {
            }
        }
    });

})();
(function () {
    mcs.form
        .directive('mcsDataTable', function() {

            return {
                restrict: 'E',
                templateUrl: 'data-table.html',
                replace: true,
                scope: {
                    paging: '@',
                    data: '=',
                    noDataMessage: '@',
                    canSelect: '@',
                    specialField: '=',
                    pageIndex: '=',
                    totalCount: '='
                },
                link: function($scope, iElm, iAttrs, controller) {
                    $scope.filterField = function(key) {
                        return ($scope.specialField.fields.indexOf(key) == -1);
                    }
                }
            };
        });

})();
(function () {
    'use strict';

    mcs.form.constant('inputConfig', {
        types: ['text']
    }).directive('mcsInput', ['inputConfig', 'mcsComponentConfig', function (config, mcsComponentConfig) {
        return {
            restrict: 'E',
            replace: false,
            scope: {
                type: '@',
                label: '@',
                placeholder: '@',
                model: '='
            },
            template: '<label class="mcs-margin-right-5"></label><input class="mcs-default-size-input mcs-margin-right-20" ng-model="model" />',
            //templateUrl: mcsComponentConfig.rootUrl + 'src/mcs-input/mcs-input.tpl.html',
            link: function ($scope, $elem, $attrs) {
                if (!$scope.label) return;
                if (!$scope.placeholder) $scope.placeholder = $scope.label;
                $scope.type = $scope.type || 'text';
                $elem.find('input').attr({ 'type': $scope.type, 'placeholder': $scope.placeholder });
                $elem.find('label').text($scope.label);
            }
        };
    }]);
})();
(function () {
    'use strict';

    mcs.form.directive('mcsRadiobuttonGroup', function () {
        var groupName = 'radio-' + mcs.newGuid();
        return {
            restrict: 'EA',
            scope: {
                data: '=',
                click: '&',
                category: '@',
                defaultValue: '='
            },
            template: '<label class="radio-inline"  ng-repeat="item in data"><input name="' + groupName + '" ng-click="click({category:category,key:item.key,eventargs:$event})" type="radio" class="ace" ng-checked="item.key==defaultValue || item.checked"><span class="lbl"></span> {{item.value}}</label>',
            link: function ($scope, $elem, $attrs, $ctrl) {
            }
        }
    });
})();