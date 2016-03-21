(function () {
    angular.module('app.component').controller('MCSCheckboxGroupController', [
        '$scope', function ($scope) {
            var vm = this;

            vm.checkresult = {
                selectedGrades: []
            };

            vm.dictionaries = {
                grades: [
                    { key: 'G1', value: '一年级' },
                    { key: 'G2', value: '二年级' },
                    { key: 'G3', value: '三年级' },
                    { key: 'G4', value: '四年级' },
                    { key: 'G5', value: '五年级' },
                    { key: 'G6', value: '六年级' },
                ]
            };

            vm.select = function (category, key, eventargs) {
                mcs.setSelectedItems(vm.checkresult[category], key, eventargs);
            };
        }
    ]);
})();