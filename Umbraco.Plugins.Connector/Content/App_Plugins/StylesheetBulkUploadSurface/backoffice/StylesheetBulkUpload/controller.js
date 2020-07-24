'use strict';
(function () {
    'use strict';
    function StylesheetBulkUploadController($scope, $location, $timeout, $route, Upload, navigationService, formHelper, codefileResource) {
        var vm = this;
        var scope = $scope;

        vm.page = {
            title: 'Stylesheet Theme Folder Upload',
            description: 'Choose or Drag CSS file/s to upload to /CSS/ directory, or Drag Theme folder below to upload to /CSS/ directory',
        };

        vm.handleFiles = handleFiles;

        scope.queue = [];
        scope.done = [];
        scope.rejected = [];
        scope.currentFile = undefined;
        scope.accept = '.css';
        scope.maxFileSize = '51200KB';
        scope.compact = '@';
        scope.hideDropzone = '@';
        scope.filesHolder = [];
        scope.filesQueued = '=';
        scope.filesUploaded = '=';

        function handleFiles(files, event) {
            _filesQueued(files, event);
        };
        function _filterFile(file) {
            var ignoreFileNames = ['Thumbs.db'];
            var ignoreFileTypes = ['directory'];
            // ignore files with names from the list
            // ignore files with types from the list
            // ignore files which starts with "."
            if (ignoreFileNames.indexOf(file.name) === -1 && ignoreFileTypes.indexOf(file.type) === -1 && file.name.indexOf('.') !== 0) {
                return true;
            } else {
                return false;
            }
        }
        function _filesQueued(files, event) {
            //Push into the queue
            angular.forEach(files, function (file) {
                if (_filterFile(file) === true) {
                    if (file.$error) {
                        scope.rejected.push(file);
                    } else {
                        scope.queue.push(file);
                    }
                }
            });
            //when queue is done, kick the uploader
            _processQueueItem();
        }

        function _processQueueItem() {
            if (scope.queue.length > 0) {
                scope.currentFile = scope.queue.shift();
                _upload(scope.currentFile);
            } else if (scope.done.length > 0) {
                /*
                //auto-clear the done queue after 3 secs
                var currentLength = scope.done.length;
                $timeout(function () {
                    scope.done.splice(0, currentLength);
                }, 3000);*/
            }
        }

        function _upload(file) {
            Upload.upload({
                url: '/umbraco/surface/StylesheetBulkUploadSurface/UploadFile',
                fields: {
                    'destinationFilePath': file.path !== undefined ? file.path.replace(file.name, '') : "/"
                },
                file: file
            }).progress(function (evt) {
                if (file.uploadStat !== 'done' && file.uploadStat !== 'error') {
                    // calculate progress in percentage
                    var progressPercentage = parseInt(100 * evt.loaded / evt.total, 10);
                    // set percentage property on file
                    file.uploadProgress = progressPercentage;
                    // set uploading status on file
                    file.uploadStatus = 'uploading';
                }
            }).success(function (data, status, headers, config) {
                $timeout(function () {
                    if (data.notifications && data.notifications.length > 0) {
                        // set error status on file
                        file.uploadStatus = 'error';
                        // Throw message back to user with the cause of the error
                        file.serverErrorMessage = data.notifications[0].message;
                        // Put the file in the rejected pool
                        scope.rejected.push(file);
                    } else {
                        // set done status on file
                        file.uploadStatus = 'done';
                        file.uploadProgress = 100;
                        // set date/time for when done - used for sorting
                        file.doneDate = new Date();
                        // set to check if file is already existing backend
                        file.isFileExisting = data.isFileExisting;
                        // Put the file in the done pool
                        scope.done.push(file);
                    }
                    scope.currentFile = undefined;
                    //after processing, test if everthing is done
                    _processQueueItem();

                    if (!data.isFileExisting) {
                        navigationService.syncTree({
                            tree: 'stylesheets',
                            path: [-1, -1],
                            forceReload: true,
                            activate: true,
                        }).then(() => { }, (err) => { });
                    }

                }, 500);
            }).error(function (evt, status, headers, config) {
                // set status done
                file.uploadStatus = 'error';
                //if the service returns a detailed error
                if (evt.InnerException) {
                    file.serverErrorMessage = evt.InnerException.ExceptionMessage;
                    //Check if its the common "too large file" exception
                    if (evt.InnerException.StackTrace && evt.InnerException.StackTrace.indexOf('ValidateRequestEntityLength') > 0) {
                        file.serverErrorMessage = 'File too large to upload';
                    }
                } else if (evt.Message) {
                    file.serverErrorMessage = evt.Message;
                }
                // If file not found, server will return a 404 and display this message
                if (status === 404) {
                    file.serverErrorMessage = 'File not found';
                }
                //after processing, test if everthing is done
                scope.rejected.push(file);
                scope.currentFile = undefined;
                _processQueueItem();
            });
        }

    }
    angular.module("umbraco").controller("StylesheetBulkUploadController", StylesheetBulkUploadController);
})();
