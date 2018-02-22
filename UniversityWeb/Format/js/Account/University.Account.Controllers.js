(function ($) {

    UA.Utilities.namespace('UniversityAccountController');
    UniversityAccountController = (function () {
        angular.module('UniversityApp.Account').controller('UniversityAccountController', ['$scope', '$location', function ($scope, $location) {
            $scope.CreateStudentRegistration = function () {                
                //If everything is hunky dory go here
                $location.path('/Account-Created-Success');
                //else
                //probably stay on the sam
            };
            $scope.NewAccountCreatedMessage = 'New Student Account Created';
        }]);

    }());//Pay attention to the empty "()" in this line. If you dont have this then angular will not load this js during bootstrap.
}(jQuery));