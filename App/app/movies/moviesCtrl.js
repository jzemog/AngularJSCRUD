(function (app) {
    'use strict';

    app.controller('moviesCtrl', moviesCtrl);

    moviesCtrl.$inject = ['$scope', '$http', '$route', 'appSettings', 'apiService', 'notificationService'];

    function moviesCtrl($scope, $http, $route, appSettings, apiService, notificationService) {
                
        $scope.movies = [];        

        $scope.remove = function (movie) {
            var deleteMovie = confirm('Are you sure you want to delete this movie?');
            if (deleteMovie) {
                apiService.remove(appSettings.serverPath + 'api/movies/remove/' + movie.ID, null, removeComplete, removeFailed);
            }
        }

        function removeComplete(response) {
            console.log(response);
            notificationService.displaySuccess('The movie has been deleted');
            $route.reload();    
        }

        function removeFailed(response) {
            notificationService.displayError(response.data);
        }

        function search() {
            apiService.get(appSettings.serverPath + 'api/movies/all/', null, moviesLoadSuccess, moviesLoadFailed);
        }

        function moviesLoadSuccess(response) {
            $scope.movies = response.data;
        }

        function moviesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        search()
    }
})(angular.module('myApp'));
