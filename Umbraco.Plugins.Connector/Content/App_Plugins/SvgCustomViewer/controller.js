angular.module("umbraco")
    .controller("SvgCustomViewer",
        function ($scope, $sce) {
            var vm = this;
            vm.raw = '';

            if ($scope.model.value) {
                if ($scope.model.value !== "") {
                    vm.raw = $sce.trustAsHtml($scope.model.value);
                }
            }
        });