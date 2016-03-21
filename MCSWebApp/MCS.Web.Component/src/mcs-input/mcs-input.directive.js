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