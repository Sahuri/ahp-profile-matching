'use strict';

angular.module('app.administrasi').controller('HakAksesController', function ($scope, $http, $state, authSvc, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'AdministrasiHakAks/';
    var api = BASE_API + module;
    var apiAksesRole = BASE_API + 'AdministrasiHakAksrole/';
    var apiAksesMenu = BASE_API + 'AdministrasiHakAksmenu/';
    var apiAksesTombol = BASE_API + 'AdministrasiHakAkstombol/';


    me.init = function (obj) {
        me.data = {};
        me.detail = {};
        me.isShowList = true;
        me.isNew = false;

        me.Undo();

        obj.tableOptions = DTOptionsBuilder
            .newOptions()
            .withOption('ajax', {
                // Either you specify the AjaxDataProp here
                // dataSrc: 'data',
                url: api + 'datatables',
                type: 'POST',
                accepts: "application/json",
                headers: authSvc.headers(),
                error: function (xhr, ajaxOptions, thrownError) {
                    NotifBoxErrorTable("Administrasi HakAkses", xhr.responseText.Message, xhr.status, $state, authSvc);
                }
            })
            .withDataProp('data')
            .withOption('processing', false)
            .withOption('serverSide', true)
            //.withPaginationType('full_numbers')
            //Add Bootstrap compatibility
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")
            .withOption('rowCallback', me.rowCallback)
            .withBootstrap()
            .withLanguageSource(Language.getLanguagePath());

        obj.tableColumns = [
            DTColumnBuilder.newColumn('Kode').notVisible(),
            DTColumnBuilder.newColumn('Nama').withTitle('Nama')
        ];
    };

    me.rowCallback = function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('click');
        $('td', nRow).bind('click', function () {
            me.$apply(function () {
                me.EditData(aData);
                me.loadRole(me.loadRoleSelected);
                me.loadMenu(me.loadMenuSelected);
                //me.loadTombol(me.loadTombolSelected);
            });
        });

        return nRow;
    }

    me.ShowList = function () {
        me.isShowList = true;
        me.isNew = false;
    };

    me.NewData = function () {
        me.isShowList = false;
        me.Undo();

        me.loadRole(me.loadRoleSelected);
        me.loadMenu(me.loadMenuSelected);

    };

    me.EditData = function (data) {
        me.isShowList = false;
        me.isNew = false;
        me.data = data;
    }

    me.Undo = function () {
        me.data = {};
        me.detail = {};
        me.detail.lstRoleSelected = [];
        me.detail.lstMenuSelected = [];
        //me.detail.lstTombolSelected = [];
        me.isNew = true;
    };

    me.Save = function () {
        if (this.frmHakAkses.$valid) {
            var msgTitle = 'Simpan Data Hak Akses';
            var detailRole = [];
            angular.forEach(me.detail.lstRoleSelected, function (x) {
                var dtl = {
                    KodeHakAkses: me.data.Kode,
                    KodeRole: x
                }

                detailRole.push(dtl);
            });

            var detailMenu = [];
            angular.forEach(me.detail.lstMenuSelected, function (x) {
                var dtl = {
                    KodeHakAkses: me.data.Kode,
                    KodeMenu: x
                }

                detailMenu.push(dtl);
            });

            var detailTombol = [];
            angular.forEach(me.detail.lstTombolSelected, function (x) {
                var dtl = {
                    KodeHakAkses: me.data.Kode,
                    KodeTombol: x
                }

                detailTombol.push(dtl);
            })

            var data = {
                HakAkses: me.data,
                Roles: detailRole,
                Menus: detailMenu,
                Tombols: detailTombol
            }

            if (me.isNew) {
                $http.post(api + 'createwithdetail', data)
                    .success(function (result) {
                        if (result.Success) {
                            NotifBoxSuccess(msgTitle, result.Message);
                            me.ShowList();
                        }
                        else {
                            NotifBoxWarning(msgTitle, result.Message);
                        }
                    })
                    .error(function (error, status) {
                        NotifBoxError(msgTitle, status + " - " + error);
                    });
            }
            else {
                msgTitle = 'Update Data Hak Akses';
                $http.put(api + 'updatewithdetail', data)
                .success(function (result) {
                    if (result.Success) {
                        NotifBoxSuccess(msgTitle, result.Message);
                        me.ShowList();
                    }
                    else {
                        NotifBoxWarning(msgTitle, result.Message);
                    }
                })
                .error(function (error, status) {
                    NotifBoxError(msgTitle, status + " - " + error);
                });
            }
        }
    };

    me.Delete = function () {
        var msgTitle = 'Hapus Data Hak Akses';
        if (!me.isNew) {
            var kode = me.data.Kode;
            var msgContent = msgTitle + ": \"" + me.data.Nama + "\" ?"
            $.SmartMessageBox({
                title: msgTitle,
                content: msgContent,
                buttons: '[OK][Batal]',
                theme: 'bg-warning'
            }, function (action) {
                if (action === "OK") {
                    $http.delete(api + "deletewithdetail?kode=" + kode)
                        .success(function (result) {
                            if (result.Success) {
                                NotifBoxSuccess(msgTitle, result.Message);
                                me.ShowList();
                            }
                            else {
                                NotifBoxWarning(msgTitle, result.Message);
                            }
                        })
                        .error(function (error, status) {
                            NotifBoxError(msgTitle, status + " - " + error);
                        });
                }
                if (action === "Batal") {
                    return;
                }
            });
        }
    };

    me.loadRole = function (callback) {
        me.lstRole = [];
        me.detail.lstRoleSelected = [];
        $http.post(BASE_API + 'administrasirole/dropdown')
        .success(function (result) {
            me.lstRole = result.Dropdown;
            
            if (callback !== undefined) {
                callback();
            }
        })
        .error(function (error, status) {
            NotifBoxError2("Daftar Role", error, status);
        });
    }

    me.loadMenu = function (callback) {
        me.lstMenu = [];
        me.detail.lstMenuSelected = [];
        $http.post(BASE_API + 'administrasimenu/dropdown')
        .success(function (result) {
            me.lstMenu = result.Dropdown;
            if (callback !== undefined) {
                callback();
            }
        })
        .error(function (error, status) {
            NotifBoxError2("Daftar Menu", error, status);
        });
    }

    me.loadTombol = function (callback) {
        me.lstTombol = [];
        me.detail.lstTombolSelected = [];
        $http.post(BASE_API + 'administrasitombol/dropdown')
        .success(function (result) {
            me.lstTombol = result.Dropdown;
            if (callback !== undefined) {
                callback();
            }
        })
        .error(function (error, status) {
            NotifBoxError2("Daftar Tombol", error, status);
        });
    }


    me.loadRoleSelected = function () {
        if (!isNullOrEmpty(me.data.Kode)) {
            $http.get(apiAksesRole + 'duallist?kode=' + me.data.Kode)
            .success(function (result) {
                me.detail.lstRoleSelected = result;
            })
            .error(function (error, status) {
                NotifBoxError2("Daftar HakAkses Role", error, status);
            });
        }
    }

    me.loadRoleSelected = function () {
        if (!isNullOrEmpty(me.data.Kode)) {
            $http.get(apiAksesRole + 'duallist?kode=' + me.data.Kode)
            .success(function (result) {
                me.detail.lstRoleSelected = result;
            })
            .error(function (error, status) {
                NotifBoxError2("Daftar HakAkses Role", error, status);
            });
        }
    }

    me.loadMenuSelected = function () {
        if (!isNullOrEmpty(me.data.Kode)) {
            $http.get(apiAksesMenu + 'duallist?kode=' + me.data.Kode)
            .success(function (result) {
                me.detail.lstMenuSelected = result;
            })
            .error(function (error, status) {
                NotifBoxError2("Daftar HakAkses Menu", error, status);
            });
        }
    }

    me.loadTombolSelected = function () {
        if (!isNullOrEmpty(me.data.Kode)) {
            $http.get(apiAksesTombol + 'duallist?kode=' + me.data.Kode)
            .success(function (result) {
                me.detail.lstTombolSelected = result;
            })
            .error(function (error, status) {
                NotifBoxError2("Daftar HakAkses Tombol", error, status);
            });
        }
    }

    me.init(this);
});