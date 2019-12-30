'use strict';

angular.module('app.auth').controller('MemberActivationController', function ($scope, $http, $state, $window, $location, BASE_API, User, authSvc) {
    var me = $scope;
    var module = 'registrasicip/';
    var api = BASE_API + module;
  
    me.init = function (obj) {
        me.data = {};
        me.data.Kode = "";
    };

    me.activation = function () {
        console.log(me.data.Kode);

        var msgTitle = 'Aktivasi Peserta CIP';
        $http.post(api + "activation", JSON.stringify(me.data.Kode))
            .success(function (result) {
                if (result.Success) {
                    NotifBoxSuccess(msgTitle, result.Message);

                    setTimeout(function () {
                        var url = $location.$$absUrl;
                        url = url.replace("aktivasi-anggota", "login");
                        $window.location.href = url;
                        $window.location.reload();
                    }, 1000);
                }
                else {
                    NotifBoxWarning(msgTitle, result.Message);
                }
            })
            .error(function (error, status) {
                NotifBoxError(msgTitle, status + " - " + error.Message);
            });

    }

    me.init(this);
});