(function () {
    "use strict";

    function Controller($scope) {

        var vm = this;
        vm.loading = false;
        vm.checked = false;
        vm.success = false;
        vm.processing = false;
        vm.results = [];
        vm.status = '';

        vm.toggle = function () {
            vm.checked = !vm.checked;
        };

        vm.close = function () {
            $scope.model.close();
        };

        vm.submit = function submit() {
            var upload = angular.element('#file');
            var file = upload[0].files[0];
            if (file === undefined) {
                vm.status = 'nothing to import!';
                return false;
            }
            else {
                vm.status = 'Processing, it may take a few minutes...';
                vm.loading = !vm.loading;
                vm.processing = !vm.processing;

                var data = new FormData();
                data.append('file', file, file.name);
                data.append('overrideExisting', vm.checked);

                var url = '/umbraco/surface/DictionarySettingsSurface/ImportDictionaries';
                $.ajax({
                    url: url,
                    type: 'post',
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        if (response.Success) {
                            vm.loading = !vm.loading;
                            vm.processing = !vm.processing;
                            vm.status = "Import Successful!";
                            vm.success = true;
                            vm.results = response.results;
                        }
                        else {
                            vm.loading = !vm.loading;
                            vm.processing = !vm.processing;
                            vm.success = false;
                            vm.status = "Error Importing, check your file.";
                        }
                    }
                });

            }

        };
    }
    angular.module("umbraco").controller("ImportDialog", Controller);
})();