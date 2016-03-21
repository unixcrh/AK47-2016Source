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