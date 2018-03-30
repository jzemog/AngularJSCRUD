(function (app) {
    'use strict';

    app.controller('movieAddCtrl', movieAddCtrl);

    movieAddCtrl.$inject = ['$scope', '$location', 'appSettings', 'apiService', 'notificationService', 'fileUploadService'];

    function movieAddCtrl($scope, $location, appSettings, apiService, notificationService, fileUploadService) {

        $scope.add = add;
        $scope.movie = { GenreId: 1 };
        $scope.genres = [];
        var image = null;
        $scope.uploadFiles = uploadFiles;
        $scope.datepicker = {};
        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        

        function add() {
            console.log($scope.movie);
            apiService.post(appSettings.serverPath + 'api/movies/add', $scope.movie, addCompleted, addFailed);
        }

        function addCompleted(response) {
            $scope.movie = response.data;
            if (image) {
                fileUploadService.uploadImage(image, appSettings.serverPath + 'api/movies/images/upload?movieId=' + $scope.movie.ID, redirectToMovies);
            }
            else {
                redirectToMovies();
            }
        }

        function addFailed(response) {
            notificationService.displayError(response.data);
        }

        function uploadFiles($files) {
            image = $files;
        }

        function redirectToMovies() {
            notificationService.displaySuccess($scope.movie.Title + ' has been created');
            $location.path('/movies');
        }

        function load() {
            loadGenre();
        }

        function loadGenre() {
            apiService.get(appSettings.serverPath + 'api/genres/all', null, genreLoadCompleted, genreLoadFailed);
        }

        function genreLoadCompleted(response) {
            $scope.genres = response.data;
        }

        function genreLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };

        load();

    }

})(angular.module('myApp'));