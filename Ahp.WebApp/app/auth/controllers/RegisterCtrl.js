'use strict';

angular.module('app.auth').controller('RegisterController', function ($scope, $http, $state, $stateParams, $window, $location, Select2Helper, BASE_API, User, authSvc) {
    var me = $scope;
    var module = 'registrasicip/';
    var api = BASE_API + module;
    me.init = function (obj) {
        me.isAuth = authSvc.authenticationData.isAuth;
        me.disabledAnggota = false;
        me.isSave = false;

        if (me.isAuth) {
            me.isNew = false;
            me.isSave = true;

            me.data = $stateParams.data;
            $("#AnggotaCIP").val(me.data.Anggota);
            me.listKategori = $stateParams.dropdown.listKategori;
            me.listFungsi = $stateParams.dropdown.listFungsi;
            me.listLokasi = $stateParams.dropdown.listLokasi;
            me.listTahun = $stateParams.dropdown.listTahun;
            me.listJenisCIP = $stateParams.dropdown.listJenisCIP;
        }
        else {
            me.isNew = true;
            me.data = {};

            //me.data.Kode = "AUTO";


            Select2Helper.GetDataForCombo(BASE_API + 'masterkategori/dropdown').then(function (result) {
                me.listKategori = result.Dropdown;
            });

            Select2Helper.GetDataForCombo(BASE_API + 'masterfungsi/dropdown').then(function (result) {
                me.listFungsi = result.Dropdown;
            });

            Select2Helper.GetDataForCombo(BASE_API + 'masterlokasi/dropdown').then(function (result) {
                me.listLokasi = result.Dropdown;
            });

            Select2Helper.GetDataForCombo(BASE_API + 'masterperiode/dropdown').then(function (result) {
                me.listTahun = result.Dropdown;
            });

            Select2Helper.GetDataForCombo(BASE_API + 'masterjeniscip/dropdown').then(function (result) {
                me.listJenisCIP = result.Dropdown;
            });
        }


    };

    me.register = function () {
        if (this.frmRegistrasi.$valid) {
            var arrAnggota = me.data.Anggota.split(',');
            var objAnggota = [];
            angular.forEach(arrAnggota, function (k) {
                var obj = {
                    Nama: k
                };
                objAnggota.push(obj);
            })
            var fd = new FormData();
            fd.append("model", JSON.stringify(me.data));
            fd.append("anggotaCIP", JSON.stringify(objAnggota));
            fd.append("isNew", JSON.stringify(me.isNew));

            var msgTitle = 'Simpan data Registrasi';
            $http.post(api + "register", fd, {
                withCredentials: false,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
            .success(function (result) {
                if (result.Success) {
                    NotifBoxSuccess(msgTitle, result.Message);
                    me.data = result.Data;
                    setTimeout(function () {
                        var url = $location.$$absUrl;
                        url = url.replace("registrasi-anggota", "login");
                        $window.location.href = url;
                        $window.location.reload();
                    }, 1000);

                    //if (me.isNew) {
                    //    me.login(result.Data);
                    //}
                    //else {
                    //    $state.go('displaymember');
                    //}
                }
                else {
                    NotifBoxWarning(msgTitle, result.Message);
                }
            })
            .error(function (error, status) {
                NotifBoxError(msgTitle, status + " - " + error.Message);
            });
        }
    };

    me.submit = function () {
        var msgTitle = 'Submit data Registrasi';
        $http.post(api + "submitregister", me.data)
            .success(function (result) {
                if (result.Success) {
                    NotifBoxSuccess(msgTitle, result.Message);
                    $state.go('displaymember');
                }
                else {
                    NotifBoxWarning(msgTitle, result.Message);
                }
            })
            .error(function (error, status) {
                NotifBoxError(msgTitle, status + " - " + error.Message);
            });
    }


    me.login = function (data) {
        if (isNullorEmpty(data.Kode)) {
            NotifBoxWarning("Login", "Username wajib diisi.");
            return;
        }

        var loginData = {
            userName: data.Kode,
            password: "",
            useRefreshTokens: false,
            r: 1
        };

        authSvc.login(loginData).then(function (response) {
            User.userName = response.userName;

            var url = $location.$$absUrl;
            url = url.replace("registrasi-anggota", "profil-anggota");
            $window.location.href = url;
            $window.location.reload();
        }, function (err) {
            if (err.error == "invalid_authorize") {
                me.message = "";
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
                me.message = "";
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
                //me.message = err.error_description;
            }
        });
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


    me.$watch('data.Anggota', function (n, o) {
        var count = 0;
        if (n != undefined || n != null) {
            count = n.split(',').length;
            me.data.JumlahAnggota = count;
        }
        else {
            me.data.JumlahAnggota = 0;
            me.disabledAnggota = false;
        }
    });
});