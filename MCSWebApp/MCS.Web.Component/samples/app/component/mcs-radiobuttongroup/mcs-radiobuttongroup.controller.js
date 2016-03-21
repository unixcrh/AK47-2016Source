(function () {
    angular.module('app.component').controller('MCSRadiobuttonGroupController', [
        '$scope', function ($scope) {
            var vm = this;

            vm.checkresult = {
                selectedGender: []
            };

            vm.dictionaries = {
                genders: [
                    { key: 'G1', value: '男', checked: false },
                    { key: 'G2', value: '女', checked: true },
                    { key: 'G3', value: '未知', checked: false }
                ]
            };

            vm.select = function (category, key, eventargs) {
                mcs.setSelectedItems(vm.checkresult[category], key, eventargs);
            };
        }
    ]);
})();