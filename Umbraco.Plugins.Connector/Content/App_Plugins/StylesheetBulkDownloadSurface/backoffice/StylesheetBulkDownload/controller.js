'use strict';
(function () {
    'use strict';
    function StylesheetBulkDownloadController($scope, $location, $timeout, $route, Upload, navigationService, formHelper, codefileResource) {
        var vm = this;
        var scope = $scope;

        vm.page = {
            title: 'Stylesheet Theme Folder Download',
            description: 'Theme Folders',
        };

        vm.items = [];
        vm.selectedItems = [];
        vm.options = {
            includeProperties: [
                { alias: 'folder', header: 'Theme Folders' }
            ]
        };

        vm.overlay = {
            view: "/App_Plugins/StylesheetBulkDownloadSurface/backoffice/StylesheetBulkDownload/overlay.html",
            show: false,
            submit: function (model) {
                vm.overlay.show = false;
                window.open('/umbraco/surface/StylesheetBulkDownloadSurface/DownloadFile?folderNames=' + JSON.stringify(vm.selectedItems), '_blank');
            },
            close: function (oldModel) {
                vm.overlay.show = false;
            },
            submitButtonLabel: "Yes",
            closeButtonLabel: "Cancel",
            Message: ""
        }

        vm.selectItem = selectItem;
        vm.clickItem = clickItem;
        vm.selectAll = selectAll;
        vm.isSelectedAll = isSelectedAll;
        vm.isSortDirection = isSortDirection;
        vm.sort = sort;
        vm.downloadFiles = downloadFiles;

        function downloadFiles() {
            console.log(vm.overlay);
            vm.overlay.Message = "Are you sure you want to download selected theme folder/s?";
            vm.overlay.show = true;
        }

        function selectItem(selectedItem, $index, $event) {
            if (selectedItem.selected) {
                selectedItem.selected = false;
                vm.selectedItems.pop(selectedItem.folder);
            } else {
                selectedItem.selected = true;
                vm.selectedItems.push(selectedItem.folder);
            }
        }

        function load() {
            $.post("/umbraco/surface/StylesheetBulkDownloadSurface/GetThemeFolders",
                function (response) {
                    if (response !== null) {
                        if (response.success !== null && response.status === "success") {
                            if (response.folders !== null) {
                                for (var x = 0; x < response.folders.length; x++) {
                                    var obj = {
                                        "folder": response.folders[x],
                                        "icon": "icon-folder"
                                    };
                                    vm.items.push(obj);
                                }
                            }
                        } else {
                            console.log(response.message);
                        }
                    }
                }).fail(function (response) {
                    console.log(response.data);
                });
        }


        load();


        /* UNUSED FUNCTIONS */

        function selectAll($event) {

        }
        function isSelectedAll() {

        }
        function clickItem(item) {
            
        }
        function isSortDirection(col, direction) {

        }
        function sort(field, allow, isSystem) {
            if (allow) {

            }
        }
    }
    angular.module("umbraco").controller("StylesheetBulkDownloadController", StylesheetBulkDownloadController);
})();
