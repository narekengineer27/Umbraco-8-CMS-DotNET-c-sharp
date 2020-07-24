angular.module("umbraco").controller("U8Muya.ColorPickerController",
    function spectrumColorPicker($scope, assetsService, angularHelper, $element) {

        // Check that rgb is selected if transparency is enabled.
        $scope.model.config.enableTransparency = convertToBoolean($scope.model.config.enableTransparency);
        if ($scope.model.config.enableTransparency) {
            $scope.model.config.preferredFormat = "rgb";
        }

        $scope.showPaletteOnly = convertToBoolean($scope.model.config.showPaletteOnly);

        $scope.showColorValue = convertToBoolean($scope.model.config.showColorValue);

        assetsService.loadJs("/App_Plugins/ColorPickerU8/lib/spectrum/spectrum.js", $scope).then(function () {
            var predefinedColor = [];
            for (i = 0; i < $scope.model.config.predefinedColor.length; i++) {
                predefinedColor.push($scope.model.config.predefinedColor[i].value);
            }
            $element.find("input").spectrum({
                containerClassName: 'sp-boxCustom',
                color: $scope.model.value,
                cancelText: $scope.cancelText,
                chooseText: "select",
                replacerClassName: "sp-replacerCustom",
                togglePaletteMoreText: $scope.moreText,
                togglePaletteLessText: $scope.lessText,
                clearText: $scope.clearText,
                noColorSelectedText: $scope.selectedText,
                preferredFormat: $scope.model.config.preferredFormat,
                showAlpha: $scope.model.config.enableTransparency, // Cannot be null.
                hideAfterPaletteSelect: true,
                showInitial: true,
                showInput: $scope.showColorValue,
                showPaletteOnly: $scope.showPaletteOnly,
                showPalette: true,
                allowEmpty: true,
                palette: predefinedColor,
                change: function (color) {
                    angularHelper.safeApply($scope, function () {
                        //Update model
                        $scope.model.value = color != null ? color.toString() : color;                        
                    });
                }
            });
        });
        //Umbraco returns null if the checkbox is not checked. 
        // Returns 1 if it is.
        function convertToBoolean(input) {
            switch (input) {
                case true: case "1": case "True": case "true": case 1: return true;
                default: return false;
            }
        }
    }
);
