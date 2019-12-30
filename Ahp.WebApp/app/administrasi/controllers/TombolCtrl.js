'use strict';

angular.module('app.administrasi').controller('TombolController', function ($scope, $http, $state, authSvc, Select2Helper, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'administrasitombol/';
    var api = BASE_API + module;

    me.init = function (obj) {
        me.data = {};
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
                    NotifBoxErrorTable("Administrasi Aksi", xhr.responseText.Message, xhr.status, $state, authSvc);
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
            DTColumnBuilder.newColumn('Title').withTitle('Title').withClass('text-primary'),
            DTColumnBuilder.newColumn('Icon').withTitle('Icon'),
            DTColumnBuilder.newColumn('Class').withTitle('Class'),
            DTColumnBuilder.newColumn('KodeMenu').withTitle('Menu'),
            DTColumnBuilder.newColumn('Parameter').withTitle('Parameter')
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
        me.data.Aktif = true;
    };

    me.EditData = function (data) {
        me.isShowList = false;
        me.isNew = false;
        me.data = data;
    }

    me.Undo = function () {
        me.data = {};
        me.isNew = true;

        me.LoadCombo();
    };

    me.Save = function () {
        if (this.frmTombol.$valid) {
            var msgTitle = 'Simpan Data Aksi';
            if (me.isNew) {
                $http.post(api, me.data)
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
                         NotifBoxError(msgTitle, status + " - " + error.Message);
                    });
            }
            else {
                msgTitle = 'Update Data Aksi';
                $http.put(api, me.data)
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
                         NotifBoxError(msgTitle, status + " - " + error.Message);
                    });
            }
        }
    };

    me.Delete = function () {
        var msgTitle = 'Hapus Data Aksi';
        if (!me.isNew) {
            var kode = me.data.Kode;
            var msgContent = msgTitle + ": \"" + me.data.Title + "\" ?";
            $.SmartMessageBox({
                title: msgTitle,
                content: msgContent,
                buttons: '[OK][Batal]',
                theme: 'bg-warning'
            }, function (action) {
                if (action === "OK") {
                    $http.delete(api + "?kode=" + kode)
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
                             NotifBoxError(msgTitle, status + " - " + error.Message);
                        });
                }
                if (action === "Batal") {
                    return;
                }
            });
        }
    };

    me.LoadCombo = function () {
        Select2Helper.GetDataForCombo(BASE_API + 'administrasimenu/dropdown').then(function (result) {
            me.listMenu = result;
        });
    }

    me.init(this);
});