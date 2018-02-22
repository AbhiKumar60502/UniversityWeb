(function ($) {
   
    UA.Utilities.namespace('UnivHome');
    UnivHome = (function () {
       
        var homemodule = angular.module('UniversityApp.Home', []);
        homemodule.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            //$stateProvider
            //    .state('AccountCreatedSuccess', {
            //        url: '#/Account-Created-Success',
            //        templateUrl: '/Account/Account/CreateAccountSuccess'
            //    });

            $routeProvider
                .when('/StartHome',
                    {
                        controller: 'UniversityHomeController',
                        templateUrl: '/UniversityHome/Home/Index'
                    })
                .when('/About',
                {
                    controller: 'UniversityHomeController',
                    templateUrl: '/UniversityHome/Home/About'
                })               

        }]);
        homemodule.controller('UniversityHomeController', function ($scope, $location) {
            $scope.customers = [
                {"id":1,"name":"Abhijit", "Total": 5.996},
                { "id": 2, "name": "Michelle", "Total": 10.996 },
                { "id": 3, "name": "Rochelle", "Total": 20.996 },
                { "id": 4, "name": "Abhishek", "Total": 25.996 }
            ];
            $scope.addStudent = function () {
                $scope.customers.push({ id: 5, name: $scope.newStudent.name, Total: $scope.newStudent.Total });
            };
            $scope.removeStudent = function () {
                $scope.customers.pop({});
            };
            
        });      

    }());//Pay attention to the empty "()" in this line. If you dont have this then angular will not load this js during bootstrap.
}(jQuery));