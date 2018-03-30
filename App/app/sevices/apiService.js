(function (app) {
    'use strict';

    app.factory('apiService', apiService);

    apiService.$inject = ['$http'];

    function apiService($http) {
        var service = {
            get: get,
            post: post,
            remove: remove
        };

        function get(url, config, success, failure) {
            return $http.get(url, config)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (failure != null) {
                        failure(error);
                    }
                });
        }

        function post(url, data, success, failure) {
            return $http.post(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (failure != null) {
                        failure(error);
                    }
                });
        }

        function remove(url, data, success, failure) {
            return $http.delete(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (failure != null) {
                        failure(error);
                    }
                });
        }

        return service;
    }

})(angular.module('myApp'));