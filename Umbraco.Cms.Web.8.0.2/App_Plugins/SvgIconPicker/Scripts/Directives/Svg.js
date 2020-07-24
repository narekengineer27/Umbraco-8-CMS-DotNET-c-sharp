angular.module("umbraco").directive("skybrudSvg", function ($http) {
    return {
        scope: {
            value: "=",
            config: "="
        },
        restrict: "E",
        replace: true,
        template: "<div></div>",
        link: function (scope, element) {

            scope.$watch("value", function () {

                if (!scope.value) {
                    element[0].innerHTML = "";
                    return;
                }

                $http.get("/umbraco/backoffice/Plugins/SvgIconPicker/GetSvg/?icon=" + scope.value).then(function (r) {
                    element[0].innerHTML = r.data;
                });
                

            });

        }
    };
});