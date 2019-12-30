'use strict';

angular.module('app.administrasi').controller('RoleController', function ($scope, $http, $state, authSvc, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'administrasirole/';
    var moduleDtl = 'administrasiroledtl/';
    var api = BASE_API + module;
    var apiDtl = BASE_API + moduleDtl;


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
                    NotifBoxErrorTable("Administrasi Role", xhr.responseText.Message, xhr.status, $state, authSvc);
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
            DTColumnBuilder.newColumn('Kode').withTitle('Kode').withClass('text-primary'),
            DTColumnBuilder.newColumn('Nama').withTitle('Nama')
        ];
    };

    me.rowCallback = function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('click');
        $('td', nRow).bind('click', function () {
            me.$apply(function () {
                me.EditData(aData);
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
    };

    me.EditData = function (data) {
        me.isShowList = false;
        me.isNew = false;
        me.data = data;
    }

    me.Undo = function () {
        me.data = {};
        me.detail = {};
        me.detail.lstWilayahKerjaSelected = [];
        me.isNew = true;

    };

    me.Save = function () {
        if (this.frmRole.$valid) {
            var msgTitle = 'Simpan Data Role';
            var detail = [];
            angular.forEach(me.detail.lstWilayahKerjaSelected, function (x) {
                var dtl = {
                    KodeRole: me.data.Kode,
                    KodeWilayahKerja: x
                }

                detail.push(dtl);
            })
            var data = {
                header: me.data,
                detail: detail
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
                msgTitle = 'Update Data Role';
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
        var msgTitle = 'Hapus Data Role';
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

    me.init(this);
});