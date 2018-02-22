(function ($) {
    
    UA.Utilities.namespace('UniversityAccount');
    UniversityAccount = (function () {
       
        var accountmodule = angular.module('UniversityApp.Account', []);  
        accountmodule.config(['$routeProvider', '$locationProvider',function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/CreateAccount',
                {
                    controller: 'UniversityAccountController',
                    templateUrl: '/Account/Account/StudentAccount'
                    

                })
            .when('/Account-Created-Success',
                {
                    controller: 'UniversityAccountController',
                    templateUrl: '/Account/Account/CreateAccountSuccess'
                    

                });
        }]);

    }());//Pay attention to the empty "()" in this line. If you dont have this then angular will not load this js during bootstrap.
}(jQuery));