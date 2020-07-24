(function () {
    "use strict";

    function Controller($scope, notificationsService) {

        var vm = this;
        vm.processing = false;

        vm.close = function () {
            $scope.model.close();
        };

        vm.submit = function submit() {
            vm.processing = true;
            var data = {
                path: $scope.model.editorData.Path,
                content: $scope.editor.value
            };
            $.post("/umbraco/surface/apisettingssurface/saveerrorpagecontent",
                data,
                function (response) {
                    //console.log(response);
                    $scope.model.submit($scope.model);
                    vm.processing = false;
                }).fail(function (response) {
                    //console.error(response.responseText);
                    notificationsService.error(`Error saving Page ${$scope.model.editorData.Path}`);
                    vm.processing = false;
                    return false;
                });
        };

        vm.load = function () {
            vm.processing = true;
            $.get(`/umbraco/surface/apisettingssurface/geterrorpagecontent?path=${$scope.model.editorData.Path}`, function (response) {
                $scope.editor = {
                    value: response.Data
                };
                setTimeout(() => {
                    const flask = new CodeFlask('.editor', {
                        language: 'html',
                        lineNumbers: true
                    });
                    flask.addLanguage('js', null);
                    flask.updateCode($scope.editor.value);
                    flask.onUpdate((code) => {
                        $scope.editor.value = code;
                    });
                    window['flask'] = flask;
                }, 600);
                vm.processing = false;
            });
        };
        vm.load();
    }
    angular.module("umbraco").controller("WysiwygDialogController", Controller);
})();