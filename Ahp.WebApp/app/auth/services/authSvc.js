'use strict';
angular.module('app.auth').factory('authSvc', function ($http, $q, BASE_API, CLIENT_ID) {
    var authServiceFactory = {};

    var _authenticationData = {
        isAuth: false,
        userName: "",
        refreshToken: "",
        useRefreshTokens: false,
        expiresIn: "",
        role: "",
        avatar: "avatar.png"
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(BASE_API + 'account/register', registration).then(function (response) {
            return response;
        });
    };

    var _login = function (loginData) {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password + "&r=" + loginData.reg;
        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + CLIENT_ID;
        }

        var deferred = $q.defer();
        $http.post(BASE_API + 'login', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
            if (loginData.useRefreshTokens) {
                var value = JSON.stringify({
                    token: response.access_token,
                    userName: loginData.userName,
                    refreshToken: response.refresh_token,
                    useRefreshTokens: true,
                    expiresIn: response.expires_in,
                    role: response.UserRole,
                    avatar: response.Avatar
                });

                localStorage.setItem('authorizationData', value);
            }
            else {
                var value = JSON.stringify({
                    token: response.access_token,
                    userName: loginData.userName,
                    refreshToken: "",
                    useRefreshTokens: false,
                    expiresIn: response.expires_in,
                    role: response.UserRole,
                    avatar: response.Avatar
                }); 
                localStorage.setItem('authorizationData', value);
            }

            _authenticationData.isAuth = true;
            _authenticationData.userName = response.UserName;
            _authenticationData.useRefreshTokens = loginData.useRefreshTokens;
            _authenticationData.role = response.UserRole;
            _authenticationData.avatar = response.Avatar;

            deferred.resolve(response);
        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _logOut = function () {
        localStorage.removeItem('authorizationData');

        _authenticationData.isAuth = false;
        _authenticationData.userName = "";
        _authenticationData.useRefreshTokens = true;
        _authenticationData.role = "";
        _authenticationData.avatar = "male.png";
    };

    var _fillAuthData = function () {
        var authData = JSON.parse(localStorage.getItem('authorizationData'));
        if (authData) {
            _authenticationData.isAuth = true;
            _authenticationData.userName = authData.userName;
            _authenticationData.useRefreshTokens = authData.useRefreshTokens;
            _authenticationData.role = authData.role;
            _authenticationData.avatar = authData.avatar;
        }
    };

    var _refreshToken = function () {
        var deferred = $q.defer();

        var authData = JSON.parse(localStorage.getItem('authorizationData'));

        if (authData) {

            if (authData.useRefreshTokens) {

                var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + BASE_API.clientId;

                localStorage.removeItem('authorizationData');

                $http.post(BASE_API + 'login', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {
                    var value = JSON.stringify({
                        token: response.access_token,
                        userName: response.userName,
                        refreshToken: response.refresh_token,
                        useRefreshTokens: true,
                        expiresIn: response.expires_in
                    })
                    localStorage.setItem('authorizationData', value);

                    deferred.resolve(response);

                }).error(function (err, status) {
                    _logOut();
                    deferred.reject(err);
                });
            }
        }

        return deferred.promise;
    };

    var _headers = function () {
        var headers = {};
        var authData = JSON.parse(localStorage.getItem('authorizationData'));
        if (authData) {
            headers.Authorization = 'Bearer ' + authData.token;
        }

        headers.Accept = "application/json";
        return headers;
    }

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authenticationData = _authenticationData;
    authServiceFactory.refreshToken = _refreshToken;
    authServiceFactory.headers = _headers;

    return authServiceFactory;
});