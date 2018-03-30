(function () {
    'use strict';

    //** ngRoute for routes
    //** ui.bootstrap for DatePicker
    //** ngFileUpload for upload the movies images
    angular.module('myApp', ['ngRoute', 'ui.bootstrap', 'ngFileUpload'])
        .config(config)   
        .constant("appSettings",
        {
            serverPath: "http://localhost:54012/"
        });

    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {

        $routeProvider
            .when("/", {
                templateUrl: "app/home/home.html",
                controller: ""
            })
            .when("/movies", {
                templateUrl: "app/movies/movies.html",
                controller: "moviesCtrl"
            })
            .when("/movies/add", {
                templateUrl: "app/movies/movieAdd.html",
                controller: "movieAddCtrl"
            }) 
            .when("/movies/:id", {
                templateUrl: "app/movies/movieEdit.html",
                controller: "movieEditCtrl"
            })   
            .otherwise({ redirectTo: "/" });

        /* this help to remove the symbols (#!) from the routes (url) */
        $locationProvider
            .html5Mode({
                enabled: true,
                requireBase: false
            })            
            .hashPrefix(''); 

    }

})();