"use strict";

angular.module('app.auth').controller('LoginCtrl', function ($scope, $state, $window, $location, User, BASE_API, authSvc) {
    $scope.init = function () {
        $scope.loginData = {};

        $scope.loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false,
            r: 0
        };

        $scope.message = "";
    };

    $scope.init();

    $scope.login = function () {
        if (isNullorEmpty($scope.loginData.userName)) {
            NotifBoxWarning("Login", "Username wajib diisi.");
            return;
        }

        if (isNullorEmpty($scope.loginData.password)) {
            NotifBoxWarning("Login", "Password wajib diisi.");
            return;
        }

        authSvc.login($scope.loginData).then(function (response) {
            User.userName = response.userName;
            //$location.path('/dashboard');

            var url = $location.$$absUrl;
            url = url.replace("login", "dashboard");
            $window.location.href = url;
            $window.location.reload();


            //var params = {
            //    UserId: $scope.loginData.userName,
            //    state: 'app.dashboard'
            //}

            //$http.post(serviceBase + 'sys.api/Authorize/UserProfile', params)
            //.success(function (result) {
            //    if (!result.success) {
            //        $.SmartMessageBox({
            //            title: "",
            //            content: result.message,
            //            buttons: '[OK]'
            //        },
            //        function (action) {
            //            if (action === "OK") {
            //                return;
            //            }
            //        });
            //    }
            //    else {
            //        $location.path('/dashboard');
            //    }
            //})
            //.error(function (status) {

            //});
        }, function (err) {
            if (err.error == "invalid_authorize") {
                $scope.message = "";
                $.SmartMessageBox({
                    title: "Login Otorisasi",
                    content: err.error_description,
                    buttons: '[OK]',
                    theme: 'bg-warning'
                },
                    function (action) {
                        if (action === "OK") {
                            return;
                        }
                    });
            }
            else {
                $scope.message = "";
                $.SmartMessageBox({
                    title: "Login",
                    content: err.error_description,
                    buttons: '[OK]',
                    theme: 'bg-danger'
                },
                    function (action) {
                        if (action === "OK") {
                            return;
                        }
                    });
                //$scope.message = err.error_description;
            }
        });
    };
})
