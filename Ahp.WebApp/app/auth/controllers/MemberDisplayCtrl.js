'use strict';

angular.module('app.auth').controller('MemberDisplayController', function ($scope, $http, $state, Select2Helper, BASE_API, User, authSvc, DTOptionsBuilder, DTColumnBuilder, Language) {
    var me = $scope;
    var module = 'registrasicip/';
    var api = BASE_API + module;
    me.init = function (obj) {
        me.vm = obj;
        me.data = {};
        me.anggota = [];

        me.isAuth = authSvc.authenticationData.isAuth;

        if (me.isAuth) {
            me.loadData();

            me.vm.tableOptions = DTOptionsBuilder
                .newOptions()
                .withOption('ajax', {
                    // Either you specify the AjaxDataProp here
                    // dataSrc: 'data',
                    url: api + 'datatableregisterlog?koderegistrasi=' + authSvc.authenticationData.userName,
                    type: 'POST',
                    accepts: "application/json",
                    headers: authSvc.headers(),
                    error: function (xhr, ajaxOptions, thrownError) {
                        NotifBoxErrorTable("Historikal", xhr.responseText.Message, xhr.status, $state, authSvc);
                    }
                })
                .withDataProp('data')
                .withOption('processing', false)
                .withOption('serverSide', true)
                .withOption('fnPreDrawCallback', ShowLoader)
                .withOption('fnDrawCallback', HideLoader)

                .withOption('responsive', true)
                //.withOption('scrollX', true)
                .withOption('scrollCollapse', true)
                .withOption('autoWidth', true)
                .withOption('colReorder', true)

                .withPaginationType('full_numbers')
                //Add Bootstrap compatibility
                .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")
                .withOption('rowCallback', me.rowCallback)
                .withBootstrap()
                .withOption('order', [2, 'asc'])
                .withLanguageSource(Language.getLanguagePath());

            me.vm.tableColumns = [
                DTColumnBuilder.newColumn('KodeRegistrasi').withTitle('No. Gugus').withClass('text-primary').notSortable(),
                DTColumnBuilder.newColumn('Status').withTitle('Status'),
                DTColumnBuilder.newColumn('CreatedDate').withTitle('Created Date').renderWith(function (data, type, full, meta) {
                    return DateTimeFormat(data);
                }),
                DTColumnBuilder.newColumn('CreatedBy').withTitle('Created By')
            ];

            me.vm.dtInstance = {};
        }
        else {
            $state.go('login');
        }
    };

    me.refreshHistory = function () {
        me.vm.dtInstance.reloadData(function () { }, true);
    }

    me.loadData = function () {
        msgTitle = "Load Data Registrasi";
        $http.get(api + "dynamicdata?term=" + authSvc.authenticationData.userName)
            .success(function (result) {
                if (result.Success) {
                    me.data = result.Data;

                    me.data.IsUploadAbster = (me.data.IsUploadAbster === null || me.data.IsUploadAbster === undefined) ? true : me.data.IsUploadAbster;
                    me.data.IsUploadKOMET = (me.data.IsUploadKOMET === null || me.data.IsUploadKOMET === undefined) ? true : me.data.IsUploadKOMET;
                    me.data.IsUploadPresentasi = (me.data.IsUploadPresentasi === null || me.data.IsUploadPresentasi || undefined) ? true : me.data.IsUploadPresentasi;
                    me.data.IsUploadRisalah = (me.data.IsUploadRisalah === null || me.data.IsUploadRisalah === undefined) ? true : me.data.IsUploadRisalah;
                    me.data.IsUploadSTK = (me.data.IsUploadSTK === null || me.data.IsUploadSTK === undefined) ? true : me.data.IsUploadSTK;
                    me.data.IsUploadVerifikasiKeuangan = (me.data.IsUploadVerifikasiKeuangan === null || me.data.IsUploadVerifikasiKeuangan === undefined) ? true : me.data.IsUploadVerifikasiKeuangan;


                    $("#AnggotaCIP").val(me.data.Anggota);
                    var anggota = me.data.Anggota.split(',');
                    me.data.AnggotaList = anggota;
                    me.isSubmit = me.data.Status == "Registrasi Peserta CIP" ? false : true;
                }
                else {
                    NotifBoxWarning(msgTitle, result.Message);
                }
            })
            .error(function (error, status) {
                NotifBoxError(msgTitle, status + " - " + error.Message);
            });
    };

    me.goRegister = function (callback) {
        $http.post(BASE_API + 'masterkategori/dropdown')
            .success(function (result) {
                if (result.Success) {
                    me.listKategori = result.Dropdown;
                    $http.post(BASE_API + 'masterfungsi/dropdown')
                        .success(function (result) {
                            if (result.Success) {
                                me.listFungsi = result.Dropdown;
                                $http.post(BASE_API + 'masterlokasi/dropdown')
                                    .success(function (result) {
                                        if (result.Success) {
                                            me.listLokasi = result.Dropdown;
                                            $http.post(BASE_API + 'masterperiode/dropdown')
                                                .success(function (result) {
                                                    if (result.Success) {
                                                        me.listTahun = result.Dropdown;
                                                        $http.post(BASE_API + 'masterjeniscip/dropdown')
                                                            .success(function (result) {
                                                                if (result.Success) {
                                                                    me.listJenisCIP = result.Dropdown;
                                                                    if (callback != null) {
                                                                        callback();
                                                                    }
                                                                }
                                                                else {
                                                                    NotifBoxWarning(msgTitle, result.Message);
                                                                }
                                                            })
                                                            .error(function (error, status) {
                                                                NotifBoxError(msgTitle, status + " - " + error.Message);
                                                            });
                                                    }
                                                    else {
                                                        NotifBoxWarning(msgTitle, result.Message);
                                                    }
                                                })
                                                .error(function (error, status) {
                                                    NotifBoxError(msgTitle, status + " - " + error.Message);
                                                });
                                        }
                                        else {
                                            NotifBoxWarning(msgTitle, result.Message);
                                        }
                                    })
                                    .error(function (error, status) {
                                        NotifBoxError(msgTitle, status + " - " + error.Message);
                                    });
                            }
                            else {
                                NotifBoxWarning(msgTitle, result.Message);
                            }
                        })
                        .error(function (error, status) {
                            NotifBoxError(msgTitle, status + " - " + error.Message);
                        });
                }
                else {
                    NotifBoxWarning(msgTitle, result.Message);
                }
            })
            .error(function (error, status) {
                NotifBoxError(msgTitle, status + " - " + error.Message);
            });

    }

    me.goRegisterPage = function () {
        var dropdown = {
            listKategori: me.listKategori,
            listFungsi: me.listFungsi,
            listLokasi: me.listLokasi,
            listTahun: me.listTahun,
            listJenisCIP: me.listJenisCIP
        }

        $state.go('register', { data: me.data, dropdown: dropdown }); $state.go('register', { data: me.data, dropdown: dropdown });
    }

    me.submit = function () {
        var msgTitle = 'Submit data Registrasi';
        $http.post(api + "submitregister", me.data)
            .success(function (result) {
                if (result.Success) {
                    NotifBoxSuccess(msgTitle, result.Message);
                    me.loadData();
                }
                else {
                    NotifBoxWarning(msgTitle, result.Message);
                }
            })
            .error(function (error, status) {
                NotifBoxError(msgTitle, status + " - " + error.Message);
            });
    }

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

    me.uploadFile = function (el) {
        var fileDoc = el.files[0];
        //el.parentNode.nextSibling.value = el.value;
        el.parentNode.nextSibling.value = fileDoc.name;

        $scope.data[el.id] = $scope.data.Kode + "_" + el.id + "_" + fileDoc.name;
        var exts = "jpg, jpeg, png, doc, docx, xls, xlsx, ppt, pptx, pdf";
        var ext = fileDoc.name.split('.')[1];
        if (!exts.includes(ext)) {
            $.SmartMessageBox({
                title: "",
                content: "Unggah Dokumen harus tipe \"" + exts + "\"",
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
            fd.append("files", fileDoc);
            fd.append("model", JSON.stringify($scope.data));
            fd.append("docType", el.id);

            var msgTitle = 'Unggah Berkas ' + el.id;
            $http.post(api + "uploaddocument", fd, {
                withCredentials: false,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
                .success(function (result) {
                    if (result.Success) {
                        me.data = result.Data;
                        NotifBoxSuccess(msgTitle, result.Message);
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

    me.init(this);
});