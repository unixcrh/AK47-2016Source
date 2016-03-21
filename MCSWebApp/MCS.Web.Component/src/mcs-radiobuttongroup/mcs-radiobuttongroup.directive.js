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