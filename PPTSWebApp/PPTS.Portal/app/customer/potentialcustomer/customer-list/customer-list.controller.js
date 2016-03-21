define(['customer', mcs.config.dataServiceConfig.customerDataService],
        function (customer) {
            customer.registerController('customerListController', [
                '$scope', '$state', 'blockUI', 'customerDataService',
                function ($scope, $state, blockUI, customerDataService) {
                    var vm = {};
                    $scope.vm = vm;

                    //²éÑ¯Ìõ¼þ
                    vm.criteria = {
                        name: '',
                        customerCode: '',
                        startDate: '',
                        endDate: '',
                        grade: [],
                        gender: [0],
                        pagedParam: null
                    };

                    var successCallback = function (result) {
                        vm.pagedList = result.viewModelList;
                        vm.dictionaries = vm.dictionaries || result.dictionaries;
                        vm.criteria.pagedParam = result.viewModelList.pagedParam;
                        blockUI.stop();
                    };

                    var errorCallback = function (result) {

                    };

                    vm.search = (function () {
                        //vm.criteria.pagedParam = null;
                        //blockUI.start();
                        //customerDataService.getAllCustomers(vm.criteria, successCallback, errorCallback);

                        vm.dictionaries = {
                            c_codE_ABBR_ACDEMICYEAR: [
                                { key: 'G1', value: 'aaaa', checked: false },
                                { key: 'G2', value: 'bbbb', checked: false },
                                { key: 'G3', value: 'cccc', checked: false },
                                { key: 'G4', value: 'dddd', checked: false },
                                { key: 'G5', value: 'eeee', checked: false },
                                { key: 'G6', value: 'ffff', checked: false },
                            ]
                        };
                    })();

                    vm.pageChanged = function () {
                        blockUI.start();
                        customerDataService.getAllCustomers(vm.criteria, successCallback, errorCallback);
                    };

                    vm.createCustomer = function () {
                        $state.go('customerAdd');
                    };

                    vm.select = function (category, item, eventargs) {
                        mcs.setSelectedItems(vm.criteria[category], item, eventargs);
                    };

                    vm.close = function (category, dictionary) {
                        vm.criteria[category].length = 0;

                        vm.dictionaries[dictionary].forEach(function(item,value) {
                            item.checked = false;
                        });

                        
                        
                    };
                }]);
        });