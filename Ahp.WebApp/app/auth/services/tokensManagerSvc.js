'use strict';
angular.module('app.auth').factory('tokensManagerSvc', function ($http, BASE_API) {
    var tokenManagerServiceFactory = {};

    var _getRefreshTokens = function () {
        return $http.get(BASE_API + 'refreshtokens').then(function (results) {
            return results;
        });
    };

    var _deleteRefreshTokens = function (tokenid) {
        return $http.delete(BASE_API + 'refreshtokens/?tokenid=' + tokenid).then(function (results) {
            return results;
        });
    };

    tokenManagerServiceFactory.deleteRefreshTokens = _deleteRefreshTokens;
    tokenManagerServiceFactory.getRefreshTokens = _getRefreshTokens;

    return tokenManagerServiceFactory;
});