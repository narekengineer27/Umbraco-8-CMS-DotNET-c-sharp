angular.module("umbraco").controller("SvgIconPicker.Controller", function ($scope, $element, editorState) {

    // Initialize the configuration
    $scope.config = $scope.model.config ? $scope.model.config : {};
    $scope.config.contentTypeAlias = editorState.current.contentTypeAlias;
    $scope.config.propertyAlias = $scope.model.alias;

    // Append the class name from the prevalues
    if ($scope.config.className) $element[0].classList.add($scope.config.className);

    $scope.add = function () {
        $scope.overlay = {
            view: "/App_Plugins/SvgIconPicker/Views/IconPickerOverlay.html",
            show: true,
            title: "Select icon",
            config: $scope.config,
            submit: function (data) {
                $scope.model.value = data;
                $scope.overlay.show = false;
                $scope.overlay = null;
            }
        };
    };

    $scope.remove = function () {
        $scope.model.value = "";
    };

});