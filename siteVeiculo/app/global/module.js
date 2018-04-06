var appModule = angular.module("appModule", ["ngRoute", "ngResource", 'ngAnimate', 'ngSanitize', 'ui.bootstrap']);


appModule.filter('range', function () {
    return function (n) {
        var res = [];
        for (var i = 1; i <= n; i++) {
            res.push(i);
        }
        return res;
    };
});