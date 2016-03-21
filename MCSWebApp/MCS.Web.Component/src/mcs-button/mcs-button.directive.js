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