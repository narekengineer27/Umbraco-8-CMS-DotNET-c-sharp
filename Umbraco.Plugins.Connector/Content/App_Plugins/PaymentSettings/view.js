(function () {
    "use strict";

    function Controller($scope) {
        var vm = this;
        vm.loading = false;
        vm.settings = {};

        loading();

        if ($scope.model.value) {
            if ($scope.model.value !== "") {
                vm.settings = $scope.model.value;
            }
            loading();
        }

        function loading() {
            vm.loading = !vm.loading;
        }

    }
    angular.module("umbraco").controller("PaymentMethodsController", Controller);
})();
