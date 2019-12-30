'use strict';
angular.module('app.auth').factory('privillegeSvc', function ($http, $q, BASE_API) {
    var privillegeServiceFactory = {};
    var _resUserButton = {};
    var _resUserMenu = {};

    var _getActionButton = function (state) {
        if (state !== 'login') {
            var params = {
                state: state
            }
            var deferred = $q.defer();
            $http.post(BASE_API + 'Privillege/UserButton', params)
                .success(function (result) {
                    if (result.success) {
                        _resUserButton = result.actionButton;
                        deferred.resolve(result.actionButton);
                    }
                })
                .error(function (status) {
                    //SmallBoxError("User Otorisasi", "Terjadi kesalahan teknis saat menghubungi server");
                    deferred.reject(status);
                });

            return deferred.promise;
        }
    };

    var _getMenus = function () {
        var deferred = $q.defer();
        $http.post(BASE_API + 'Privillege/UserMenu')
            .success(function (result) {
                if (result.success) {
                    _resUserMenu = result.privillegeMenus;
                    deferred.resolve(result.privillegeMenus);
                }
            })
            .error(function (status) {
                //SmallBoxError("Menu Otorisasi", "Terjadi kesalahan teknis saat menghubungi server");
                deferred.reject(status);
            });

        return deferred.promise;
    };

    privillegeServiceFactory.actionButton = _getActionButton;
    privillegeServiceFactory.userMenu = _getMenus;

    return privillegeServiceFactory;
});