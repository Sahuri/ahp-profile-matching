'use strict';

/**
 * @ngdoc overview
 * @name app [smartadminApp]
 * @description
 * # app [smartadminApp]
 *
 * Main module of the application.
 */

angular.module('app', [
    'ngSanitize',
    'ngAnimate',
    'restangular',
    'ui.router',
    'ui.bootstrap',
    // Smartadmin Angular Common Module
    'SmartAdmin',

    // App
    'app.auth',
    'app.layout',
    'app.dashboard',
    'app.graphs',
    'app.tables',
    'app.forms',
    'app.ui',
    'app.laporan',
    'app.administrasi',
    'app.masterdata',
    'app.proses',
    'app.prosespengalihan'
])
    .config(function ($provide, $httpProvider, RestangularProvider) {
        $httpProvider.interceptors.push('authInterceptorSvc');

        $httpProvider.interceptors.push(function ($q, $injector, $location, $rootScope) {
            var $http;
            return {
                'request': function (config) {
                    if (AllowLoader) {
                        ShowLoader();
                    }
                    return config;
                },

                'requestError': function (rejection) {
                    //if (canRecover(rejection)) {
                    //    return responseOrNewPromise
                    //}

                    $http = $http || $injector.get('$http');
                    if ($http.pendingRequests.length < 1) {
                        HideLoader();
                    }
                    return $q.reject(rejection);
                },

                'response': function (response) {
                    $http = $http || $injector.get('$http');
                    if ($http.pendingRequests.length < 1) {
                        HideLoader();
                    }
                    return response;
                },

                'responseError': function (rejection) {
                    //if (canRecover(rejection)) {
                    //    return responseOrNewPromise
                    //}

                    $http = $http || $injector.get('$http');
                    if ($http.pendingRequests.length < 1) {
                        HideLoader();
                    }
                    return $q.reject(rejection);
                }
            };
        });

        RestangularProvider.setBaseUrl(location.pathname.replace(/[^\/]+?$/, ''));

        //Custom $http.delete
        $httpProvider.defaults.headers["delete"] = {
            'Content-Type': 'application/json;charset=utf-8'
        };
    })

    .constant('APP_CONFIG', window.appConfig)
    .constant('CLIENT_ID', 'Ahp_webapp')
    .constant('BASE_API', 'http://localhost:54474/api/') //LOCALHOST

    //.constant('BASE_API', 'http://10.90.1.128:8903/api/') //DEV

    .run(function ($rootScope, $state, $stateParams, $http, $location, $window, authSvc, BASE_API) {
        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;
        // editableOptions.theme = 'bs3';

        authSvc.fillAuthData();
        var isAuth = authSvc.authenticationData.isAuth;
        if (!isAuth) {
            $state.go('login');
            return;
        }

        $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            authSvc.fillAuthData();
            isAuth = authSvc.authenticationData.isAuth;
            if (toState.name === 'login') {
                if (isAuth) {
                    event.preventDefault();
                    $state.go('app.dashboard');
                }
            }
        });

    });