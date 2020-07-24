(function () {
    "use strict";

    function Controller($scope, $document, $http, notificationsService, editorService) {
        var vm = this;
        vm.loading = false;
        vm.settings = [];
        vm.errorFiles = [];
        vm.cacheTenantUids = [];
        vm.cacheNames = [];
        vm.selectedCacheTenantUid = "";
        vm.selectedCacheName = "";
        vm.isSelectedCacheNameLoading = false;
        vm.isSelectedCacheTenantUidLoading = false;
        vm.content = {
            name: "Tenant & Api Configurations",
            icon: 'list',
            tabs: [
                {
                    id: 1,
                    label: "Settings",
                    alias: "tab1",
                    active: true
                }
            ]
        };

        vm.setting = function (key) {
            return vm.settings.find(function (element) { return element.Key === key; });
        };

        vm.toggle = function toggle(key) {
            switch (key) {
                case 'TotalCode.Admin.SaveAndPublish': vm.setting('TotalCode.Admin.SaveAndPublish').Value = vm.setting('TotalCode.Admin.SaveAndPublish').Value === 'true' ? "false" : "true";
                    break;
                case 'TotalCode.Admin.SecureUrls': vm.setting('TotalCode.Admin.SecureUrls').Value = vm.setting('TotalCode.Admin.SecureUrls').Value === 'true' ? "false" : "true";
                    break;
                case 'TotalCode.Admin.SetupLocalUrls': vm.setting('TotalCode.Admin.SetupLocalUrls').Value = vm.setting('TotalCode.Admin.SetupLocalUrls').Value === 'true' ? "false" : "true";
                    break;
            }
        };

        vm.Save = function () {
            loading();
            var data = {
                Settings: vm.settings
            };

            $.post("/umbraco/surface/apisettingssurface/savesettings",
                data,
                function (response) {
                    loading();
                    notificationsService.success("Api Settings Saved " + response.status);

                }).fail(function (response) {
                    //console.error(response.data);
                    notificationsService.error("Error saving Api Settings");
                    loading();
                    return false;
                });
        };

        vm.toggle = function (event) {
            $(event.currentTarget).parents('h5').siblings('.info-box:first').toggle();
        };

        vm.getErrorPages = function () {
            loading();
            $.get("/umbraco/surface/apisettingssurface/geterrorpages",
                function (response) {
                    vm.errorFiles = response.files;
                    loading();
                }).fail(function (response) {
                    //console.error(response.data);
                    loading();
                    return false;
                });
        };

        vm.openEditor = function (path) {

            var editorData = {
                "Path": path
            };
            var options = {
                title: `Editing ${path}`,
                view: "/App_Plugins/apisettingssurface/backoffice/apisettings/wysiwyg.html",
                editorData: editorData,
                submit: function (response) {
                    //console.log(response);
                    editorService.close();
                    notificationsService.success(`Page ${editorData.Path} Saved`);
                },
                close: function () {
                    editorService.close();
                }
            };
            editorService.open(options);
        };

        vm.deleteErrorPage = function (path) {
            loading();
            var confirmed = confirm(`Are you sure you want to delete ${path}?`);
            if (confirmed) {
                var data = {
                    path: path
                };
                $.post("/umbraco/surface/apisettingssurface/deleteerrorpage",
                    data,
                    function (response) {
                        loading();
                        //console.log(response);
                        if (response.Success) {
                            notificationsService.success(`Error Page ${path} Deleted`);
                            vm.getErrorPages();
                        }
                        else {
                            notificationsService.error(`Error deleting ${path}`);
                        }
                    }).fail(function (response) {
                        //console.error(response.data);
                        notificationsService.error(`Error deleting ${path}`);
                        loading();
                        return false;
                    });
            }
        };

        vm.newErrorPage = function () {
            loading();
            var pageName = prompt('Set new error page name (no extension)');
            if (pageName !== undefined) {
                var data = {
                    pageName: pageName
                };
                $.post("/umbraco/surface/apisettingssurface/savenewerrorpagecontent",
                    data,
                    function (response) {
                        loading();
                        //console.log(response);
                        if (response.Success) {
                            notificationsService.success(`New Error Page ${pageName} Saved`);
                            vm.getErrorPages();
                        }
                        else {
                            notificationsService.error(`Error saving ${pageName} - ${response.Status}`);
                        }
                    }).fail(function (response) {
                        //console.error(response.data);
                        notificationsService.error(`Error saving ${pageName} - ${response.data}`);
                        loading();
                        return false;
                    });
            }
        };

        vm.clearCache = function () {
            var version = parseInt(vm.setting('clientDependency').Value);
            vm.setting('clientDependency').Value = ++version;
            vm.Save();
        };

        vm.clearApiCache = function () {
            loading();
            $.post("/umbraco/surface/apisettingssurface/clearapicache",
                function (response) {
                    loading();
                    notificationsService.success("Api Cache Cleared " + response.status);
                    loadTenantSelect();
                    $scope.$digest();
                }).fail(function (response) {
                    //console.error(response.data);
                    notificationsService.error("Error Clearing Api Cache");
                    loading();
                    return false;
                });
        };

        function loadTenantSelect() {

            vm.selectedCacheTenantUid = "";
            vm.isSelectedCacheTenantUidLoading = true;

            $.post("/umbraco/surface/apisettingssurface/gettenantuidsoncache",
                function (response) {
                    console.log(response);
                    vm.cacheTenantUids = [];
                    if (response !== undefined) {
                        if (response.tenantUids !== undefined) {
                            for (var x = 0; x < response.tenantUids.length; x++) {
                                vm.cacheTenantUids.push(response.tenantUids[x]);
                            }
                        }
                    }
                    vm.isSelectedCacheTenantUidLoading = false;
                    $scope.$digest();
                }).fail(function (response) {

                    notificationsService.error("Error loadTenantSelect");
                    $scope.$digest();
                });
        };

        function loadCacheNamesSelect() {

            vm.selectedCacheName = "";
            vm.isSelectedCacheNameLoading = true;

            $.post("/umbraco/surface/apisettingssurface/GetCacheNamesOnCache",
                {
                    tenantUid: vm.selectedCacheTenantUid
                },
                function (response) {
                    console.log(response);
                    vm.cacheNames = [];
                    if (response !== undefined) {
                        if (response.cacheNames !== undefined) {
                            for (var x = 0; x < response.cacheNames.length; x++) {
                                vm.cacheNames.push(response.cacheNames[x]);
                            }
                        }
                    }

                    vm.isSelectedCacheNameLoading = false;
                    $scope.$digest();
                }).fail(function (response) {

                    notificationsService.error("Error loadCacheNamesSelect");
                    $scope.$digest();
                });
        };

        vm.tenantSelectChange = function () {
            if (vm.selectedCacheTenantUid !== "") {
                loadCacheNamesSelect();
            } else {
                vm.isValidTenantUidInCacheTypeSelect = false;
            }
        };

        vm.cacheNameSelectChange = function () {
            if (vm.selectedCacheName !== "") {

            } else {

            }
        };

        vm.clearCacheOfSelectedItems = function () {
            loading();
            var cacheInfo = {
                TenantUid: vm.selectedCacheTenantUid,
                CacheName: vm.selectedCacheName
            };
            $.post("/umbraco/surface/apisettingssurface/ClearSelectedApiCache",
                {
                    cacheInfo: cacheInfo
                },
                function (response) {
                    loading();

                    loadTenantSelect();

                    notificationsService.success("Api Cache Cleared " + response.status);

                    $scope.$digest();
                }).fail(function (response) {
                    //console.error(response.data);
                    notificationsService.error("Error Clearing Api Cache");
                    loading();
                    $scope.$digest();
                });
        };

        function load() {
            loading();
            $http({
                method: 'GET',
                url: '/umbraco/surface/apisettingssurface/readsettings'
            }).then(function successCallback(response) {
                vm.settings = response.data.settings;
                loading();
            }, function errorCallback(response) {
                //console.error(response.data);
                notificationsService.error("Error loading Api Settings");
                loading();
                return false;
            });
            vm.getErrorPages();
        }

        function loading() {
            vm.loading = !vm.loading;
        }

        load();
        loadTenantSelect();

        $document.ready(function () {
            setTimeout(() => {
                $('.info-box').toggle();
            }, 1 * 1000);
        });

    }
    angular.module("umbraco").controller("ApiSettingsController", Controller);
})();