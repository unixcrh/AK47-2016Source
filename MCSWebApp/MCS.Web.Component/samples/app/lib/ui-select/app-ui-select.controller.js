(function () {
    angular.module('app.lib').controller('AppUISelectController', [
        '$scope', '$http', function ($scope, $http) {
            var vm = this;

            vm.disabled = undefined;

            vm.enable = function () {
                vm.disabled = false;
            };

            vm.disable = function () {
                vm.disabled = true;
            };

            vm.clear = function () {
                vm.person.selected = null;
            };

            vm.person = {};

            vm.people = [
              { name: 'Adam', email: 'adam@email.com', age: 12, country: 'United States' },
              { name: 'Amalie', email: 'amalie@email.com', age: 12, country: 'Argentina' },
              { name: 'Estefanía', email: 'estefania@email.com', age: 21, country: 'Argentina' },
              { name: 'Adrian', email: 'adrian@email.com', age: 21, country: 'Ecuador' },
              { name: 'Wladimir', email: 'wladimir@email.com', age: 30, country: 'Ecuador' },
              { name: 'Samantha', email: 'samantha@email.com', age: 30, country: 'United States' },
              { name: 'Nicole', email: 'nicole@email.com', age: 43, country: 'Colombia' },
              { name: 'Natasha', email: 'natasha@email.com', age: 54, country: 'Ecuador' },
              { name: 'Michael', email: 'michael@email.com', age: 15, country: 'Colombia' },
              { name: 'Nicolás', email: 'nicolas@email.com', age: 43, country: 'Colombia' }
            ];

            //$http.get('http://localhost:1862/samples/app/lib/ui-select/people.json').then(function (result) {
            //    vm.people = result.data;
            //});

            vm.multiple = {};
            vm.multiple.colors = ['Blue', 'Red'];
            vm.availableColors = ['Red', 'Green', 'Blue', 'Yellow', 'Magenta', 'Maroon', 'Umbra', 'Turquoise'];

        }
    ]);
})();