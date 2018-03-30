(function (app) {
    'use strict';

    app.factory('fileUploadService', fileUploadService);

    fileUploadService.$inject = ['$rootScope', '$http', 'Upload', 'notificationService'];

    function fileUploadService($rootScope, $http, Upload, notificationService) {

        $rootScope.upload = [];

        var service = {
            uploadImage: uploadImage,
            getImage: getImage
        }               

        function uploadImage(files, apiUrl, callback) {
            $rootScope.files = files;
            if (files) {
                Upload.upload({
                    url: apiUrl,
                    data: {
                        files: files
                    }
                }).progress(function (evt) {
                }).success(function (response) {                    
                    notificationService.displaySuccess(response.FileName + ' uploaded successfully');
                    callback();                    
                }).error(function (response) {
                    notificationService.displayError('Error uploading the image');
                });
            }
        }        

        function getImage(apiUrl, callback) {
            return $http({
                method: 'GET',
                url: apiUrl,
                responseType: 'arraybuffer'
            }).then(function (response) {
                console.log(response);
                var str = _arrayBufferToBase64(response.data);
                callback(str);
                // str is base64 encoded.
            }, function (response) {
                notificationService.displayError('Error getting the image');
            });
        }

        function _arrayBufferToBase64(buffer) {
            var binary = '';
            var bytes = new Uint8Array(buffer);
            var len = bytes.byteLength;
            for (var i = 0; i < len; i++) {
                binary += String.fromCharCode(bytes[i]);
            }
            return window.btoa(binary);
        }

        return service;
    }

})(angular.module('myApp'));