angular.module("umbraco").controller("SvgIconPickerOverlay.Controller", function ($scope, $element, $http) {

    // Get the configuration
    $scope.config = $scope.model.config;

    // Append the class name from the prevalues
    if ($scope.config.className) $element[0].classList.add($scope.config.className);

    $scope.icons = [];
    $http.get("/umbraco/backoffice/Plugins/SvgIconPicker/GetIcons/").then(function (r) {
        $scope.icons = r.data.icons;
    });

    $scope.selectIcon = function (icon) {
        $scope.model.submit(icon);
    };

});