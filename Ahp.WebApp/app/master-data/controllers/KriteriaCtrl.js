'use strict';

angular.module('app.masterdata').controller('KriteriaController', function ($scope, $http, $state, authSvc, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language, Select2Helper) {
    var me = $scope;
    var module = 'kriteria/';
    var api = BASE_API + module;

    me.init = function (obj) {
        me.data = {};
        me.isShowList = true;
        me.isNew = false;

        me.Undo();

        var headers = authSvc.headers();
        headers.Accept = "application/json";

        obj.tableOptions = DTOptionsBuilder
            .newOptions()
            .withOption('order', [1, 'asc'])
            .withOption('ajax', {
                // Either you specify the AjaxDataProp here
                // dataSrc: 'data',
                url: api + 'datatables',
                type: 'POST',
                accepts: "application/json",
                headers: headers,
                error: function (xhr, ajaxOptions, thrownError) {
                    NotifBoxErrorTable("Kriteria", xhr.responseText.Message, xhr.status, $state, authSvc);
                }
            })
            .withDataProp('data')
            .withOption('processing', false)
            .withOption('serverSide', true)
            .withOption('fnPreDrawCallback', ShowLoader)
            .withOption('fnDrawCallback', HideLoader)
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
            DTColumnBuilder.newColumn('Nama').withTitle('Nama'),
            DTColumnBuilder.newColumn('EigenvectorScore').withTitle('Eigenvector'),
            DTColumnBuilder.newColumn('NilaiTarget').withTitle('Nilai Target')
            
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
        me.LoadComboJb();
    };

    me.Undo = function () {
        me.data = {};
        me.isNew = true;
        Select2Helper.GetDataForCombo(api + 'DropdownNilaiTarget').then(function (result) {
            me.listNilaiTarget = result.Dropdown;
        });

    };

    
    me.Save = function () {
        if (this.frmKriteria.$valid) {
            var msgTitle = 'Simpan Data Kriteria';
            if (me.isNew) {
               // me.data.CreatedBy = authSvc.authenticationData.userName;
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
                msgTitle = 'Update Data Kriteria';
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
        var msgTitle = 'Delete Data Kriteria';
        if (!me.isNew) {
            var kode = me.data.Kode;
            var msgContent = msgTitle + ": \"" + kode + "\" ?"
            var data = [kode];
            $.SmartMessageBox({
                    title: msgTitle,
                    content: msgContent,
                buttons: '[OK][Batal]',
                theme: 'bg-warning'
            },function (action) {
                if (action === "OK") {
                    $http.delete(api, { data })
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

    me.init(this);
});