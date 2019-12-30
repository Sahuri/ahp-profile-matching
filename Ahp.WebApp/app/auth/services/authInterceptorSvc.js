'use strict';
angular.module('app.auth').factory('authInterceptorSvc', function ($q, $injector, $location) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        var authData = JSON.parse(localStorage.getItem('authorizationData'));
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }
        else {

        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            var authService = $injector.get('authSvc');
            var authData = JSON.parse(localStorage.getItem('authorizationData'));

            if (authData) {
                if (authData.useRefreshTokens) {
                    $location.path('/login');
                    return $q.reject(rejection);
                }
            }

            authService.logOut();
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
});