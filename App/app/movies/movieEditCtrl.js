(function (app) {
    'use strict';

    app.controller('movieEditCtrl', movieEditCtrl);

    movieEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'appSettings', 'apiService', 'notificationService', 'fileUploadService']; 

    function movieEditCtrl($scope, $location, $routeParams, appSettings, apiService, notificationService, fileUploadService) {

        $scope.update = update;
        $scope.movie = {};
        var image = null;
        $scope.uploadFiles = uploadFiles;
        $scope.datepicker = {}; 
        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        function loadMovie() {            
            apiService.get(appSettings.serverPath + 'api/movies/details/' + $routeParams.id, null, movieLoadCompleted, movieLoadFailed);
        }

        function movieLoadCompleted(response) {
            $scope.movie = response.data;   
            $scope.movie.ReleaseDate = new Date($scope.movie.ReleaseDate);
            console.log($scope.movie);
            loadGenre();
            getImage();      
        }

        function movieLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function update() {
            console.log($scope.movie);
            apiService.post(appSettings.serverPath + 'api/movies/update/', $scope.movie, updateCompleted, updateFailed);

        }

        function updateCompleted(response) {
            console.log(response);
            if (image) {
                fileUploadService.uploadImage(image, appSettings.serverPath + 'api/movies/images/upload?movieId=' + $scope.movie.ID, redirectToMovies);
            }
            else {
                redirectToMovies();
            }            
        }

        function redirectToMovies() {
            notificationService.displaySuccess($scope.movie.Title + ' has been updated');            
            $location.path('/movies');
        }

        function updateFailed(response) {
            notificationService.displayError(response.data);
        }

        function getImage() {
            fileUploadService.getImage(appSettings.serverPath + 'api/movies/images/download?movieId=' + $scope.movie.ID, processImage);
        }      

        function processImage(response) {
            $scope.iimage = 'data:image/jpg;base64,' + response;
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

        function uploadFiles($files) {
            image = $files;
        }

        loadMovie();

    }
})(angular.module('myApp'));
