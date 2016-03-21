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