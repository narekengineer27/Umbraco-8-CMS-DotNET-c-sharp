(function () {
    "use strict";

    function Controller($http, $window, notificationsService, editorService) {
        var vm = this;
        vm.loading = false;
        vm.buttonState = "init";
        vm.dictionaries = [];
        vm.filter = '';
        vm.audit = [];
        vm.auditFilter = '';
        vm.content = {
            name: "Dictionary Manager",
            tabs: [
                {
                    id: 1,
                    label: "Import & Export",
                    alias: "tab1",
                    active: true
                },
                {
                    id: 2,
                    label: "Audit",
                    alias: "tab2",
                    active: false
                }
            ]
        };
        vm.changeTab = function (selectedTab) {
            vm.content.tabs.forEach(function (tab) {
                tab.active = false;
            });
            selectedTab.active = true;
        };
        vm.ExportXlsx = function () {
            $window.open('/umbraco/surface/DictionarySettingsSurface/ExportToExcel', '_blank');
            notificationsService.success("Exporting...");
        };

        vm.ImportXlsx = function () {
            var options = {
                title: "Import Dictionary Items",
                view: "/App_Plugins/DictionarySettingsSurface/backoffice/DictionarySettings/import.html",
                submit: function (model) {
                    editorService.close();
                    done();
                },
                close: function () {
                    editorService.close();
                    done();
                }
            };
            editorService.open(options);
        };

        vm.ClearAll = function () {
            var confirmed = confirm("Are you sure you want to delete all Dictionary Items?");
            if (confirmed) {
                loading();
                $http({
                    method: 'GET',
                    url: '/umbraco/surface/DictionarySettingsSurface/DeleteAll'
                }).then(function successCallback(response) {
                    loading();
                    load();
                    notificationsService.success("Dictionary Items Deleted");
                }, function errorCallback(response) {
                    //console.error(response.data);
                    notificationsService.error("Error Deleting Dictionary Items");
                    loading();
                    return false;
                });
            }
        };

        vm.RunAudit = function () {
            loading();
            $http({
                method: 'GET',
                url: '/umbraco/surface/DictionarySettingsSurface/RunAudit'
            }).then(function successCallback(response) {
                loading();
                vm.audit = response.data.results;
                notificationsService.success("Audit Successful");
            }, function errorCallback(response) {
                //console.error(response.data);
                notificationsService.error("Error Auditing");
                loading();
                return false;
            });
        };

        vm.Create = function (found) {
            loading();

            $http({
                method: 'POST',
                url: '/umbraco/surface/DictionarySettingsSurface/Create',
                data: {
                    'Key': found.Key,
                    'File': found.File,
                    'Registered': found.Registered,
                    'ParentKey': found.ParentKey
                }
            }).then(function successCallback(response) {
                loading();
                found.Registered = response.data.Created;
                notificationsService.success("Dictionary Item Created");
            }, function errorCallback(response) {
                //console.error(response.data);
                notificationsService.error("Error Creating Dictionary Items");
                loading();
                return false;
            });
        };

        function load() {
            loading();
            $http({
                method: 'GET',
                url: '/umbraco/surface/DictionarySettingsSurface/GetDictionaries'
            }).then(function successCallback(response) {
                vm.dictionaries = response.data.dictionaryItems;
                loading();
            }, function errorCallback(response) {
                //console.error(response.data);
                notificationsService.error("Error Loading Dictionary Items");
                loading();
                return false;
            });
        }

        function done(data) {
            load();
        }

        function loading() {
            vm.loading = !vm.loading;
        }

        load();
    }
    angular.module("umbraco").controller("DictionaryController", Controller);
})();
