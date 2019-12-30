'use strict';

angular.module('app.administrasi').controller('RoleUserController', function ($scope, $http, $state, authSvc, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'administrasirole/';
    var moduleDtl = 'administrasiroleuser/';
    var api = BASE_API + module;
    var apiDtl = BASE_API + moduleDtl;
    var xRow = "";

    me.init = function (obj) {
        me.data = {};
        me.detail = {};
        me.detail.lstUserSelected = [];

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
                    NotifBoxErrorTable("Administrasi Role User", xhr.responseText.Message, xhr.status, $state, authSvc);
                }
            })
            .withOption('select', true)
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
                me.data = aData;
                me.loadUser(me.loadUserSelected);

                $(xRow).removeClass('highlight');
                $(nRow).addClass('highlight');
                xRow = nRow;
            });
        });

        return nRow;
    }

    me.Save = function () {
        var msgTitle = 'Simpan Data Role User';
        var detail = [];
        angular.forEach(me.detail.lstUserSelected, function (x) {
            var dtl = {
                KodeRole: me.data.Kode,
                KodeUser: x
            }

            detail.push(dtl);
        })
        var data = {
            header: me.data,
            detail: detail
        }

        $http.post(api + 'createwithdetailuser', data)
        .success(function (result) {
            if (result.Success) {
                NotifBoxSuccess(msgTitle, result.Message);
            }
            else {
                NotifBoxWarning(msgTitle, result.Message);
            }
        })
        .error(function (error, status) {
            NotifBoxError(msgTitle, status + " - " + error);
        });
    };

    me.loadUser = function (callback) {
        me.lstUser = [];
        me.detail.lstUserSelected = [];
        $http.post(BASE_API + 'administrasiuser/dropdown')
        .success(function (result) {
            me.lstUser = result.Dropdown;
            if (callback != undefined) {
                callback();
            }
        })
        .error(function (error, status) {
            NotifBoxError2("Daftar User", error, status);
        });
    }

    me.loadUserSelected = function () {
        if (!isNullOrEmpty(me.data.Kode)) {
            $http.get(apiDtl + 'duallist?kode=' + me.data.Kode)
            .success(function (result) {
                me.detail.lstUserSelected = result;
            })
            .error(function (error, status) {
                NotifBoxError2("Daftar Role User", error, status);
            });
        }
    }

    me.init(this);
});