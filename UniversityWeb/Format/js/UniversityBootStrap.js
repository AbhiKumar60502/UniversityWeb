(function ($) {

    UA.Utilities.namespace('UniversityBootStrap');
    UniversityBootStrap = (function () {
        
        var homemodule = angular.module('UniversityApp', ['ngRoute', 'UniversityApp.Home', 'UniversityApp.Account']);
    }());
}(jQuery));