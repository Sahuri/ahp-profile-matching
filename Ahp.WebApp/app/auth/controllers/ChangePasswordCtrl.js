'use strict';

angular.module('app.auth').controller('ChangePasswordController', function ($scope, $http, $state, Select2Helper, BASE_API, User, authSvc) {
    var me = $scope;
    var module = 'administrasiuser/';
    var api = BASE_API + module;
  
    me.init = function (obj) {
        me.data = {};
        me.isAuth = authSvc.authenticationData.isAuth;

        if (me.isAuth) {
            me.loadData();
        }
        else {
            $state.go('login');
        }
    };

    me.loadData = function () {
        msgTitle = "Load Data Akun";
        $http.get(api + "getsimpledata?keyvalues=" + authSvc.authenticationData.userName)
            .success(function (result) {
                if (result.Success) {
                    me.data = result.Data;
                }
                else {
                    NotifBoxWarning(msgTitle, result.Message);
                }
            })
            .error(function (error, status) {
                NotifBoxError(msgTitle, status + " - " + error.Message);
            });
    };

    me.uploadAvatar = function (el) {
        if (this.frmUploadAvatar.$valid) {
            var fileAvatar = el.files[0];
            me.data.AvatarName = fileAvatar.name;
            var exts = "jpg, jpeg, png, bmp";
            var ext = me.data.AvatarName.split('.')[1];
            if (!exts.includes(ext)) {
                $.SmartMessageBox({
                    title: "",
                    content: "Unggah Foto harus tipe \"" + exts + "\"",
                    buttons: '[OK]',
                    theme: 'bg-warning'
                },
                    function (action) {
                        if (action === "OK") {
                            return;
                        }
                        else {
                            return;
                        }
                    });
            }
            else {
                var fd = new FormData();
                fd.append("file", fileAvatar);
                fd.append("avatar", JSON.stringify(me.data.AvatarName));

                var msgTitle = 'Unggah Foto';
                $http.post(api + "uploadavatar", fd, {
                    withCredentials: false,
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity
                })
                    .success(function (result) {
                        if (result.Success) {
                            NotifBoxSuccess(msgTitle, result.Message);
                            me.data.Avatar = result.Data.Avatar;
                        }
                        else {
                            NotifBoxWarning(msgTitle, result.Message);
                        }
                    })
                    .error(function (error, status) {
                        NotifBoxError(msgTitle, status + " - " + error.Message);
                    });
            }
        }
    };

    me.changePassword = function () {
        if (this.frmChangePass.$valid) {
            var msgTitle = 'Ubah Password';
            if (me.data.newPass != me.data.confirmPass) {
                $.SmartMessageBox({
                    title: "",
                    content: "Password Baru tidak sama dengan Konfirmasi Password",
                    buttons: '[OK]',
                    theme: 'bg-warning'
                },
                    function (action) {
                        if (action === "OK") {
                            $("#newPass").focus();
                            return;
                        }
                        else {
                            return;
                        }
                    });
            }
            else {
                $http.post(api + "changepassword?oldPass=" + me.data.oldPass + "&newPass=" + me.data.newPass)
                    .success(function (result) {
                        if (result.Success) {
                            NotifBoxSuccess(msgTitle, result.Message);
                            location.reload();
                        }
                        else {
                            NotifBoxWarning(msgTitle, result.Message);
                        }
                    })
                    .error(function (error, status) {
                        NotifBoxError(msgTitle, status + " - " + error.Message);
                    });
            }
        }
    };

    me.logOut = function () {
        $.SmartMessageBox({
            title: "",
            content: "Anda yakin akan keluar dari Sistem Continuous Improvement Program?",
            buttons: '[OK][Batal]',
            theme: 'bg-danger'
        },
            function (action) {
                if (action === "OK") {
                    User.initialized = "";
                    User.userName = "";

                    authSvc.logOut();
                    $state.go('login');
                }
                else {
                    return;
                }
            });
    }
    me.init(this);
});