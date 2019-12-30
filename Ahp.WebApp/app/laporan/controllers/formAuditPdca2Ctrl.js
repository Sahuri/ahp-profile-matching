'use strict';

angular.module('app.laporan').controller('formAuditPdca2Controller', function ($scope, $http, $state, authSvc, Select2Helper, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'Audit/';
    var api = BASE_API + module;

    me.init = function (obj) {
        me.data = {};
        me.isShowList = true;
        me.user = authSvc.authenticationData;

        var headers = authSvc.headers();
        headers.Accept = "application/json";
        me.tahun = 0;
        me.tableOptions = DTOptionsBuilder
            .newOptions()
            .withOption('ajax', {
                // Either you specify the AjaxDataProp here
                // dataSrc: 'data',
                url: api + 'PdcaList?Periode=' + me.tahun,
                type: 'POST',
                accepts: "application/json",
                headers: headers,
                error: function (xhr, ajaxOptions, thrownError) {
                    NotifBoxErrorTable("Laporan PDCA II", xhr.responseText.Message, xhr.status, $state, authSvc);
                }
            })
            .withDataProp('data')
            .withOption('processing', false)
            .withOption('serverSide', true)
            .withOption('fnPreDrawCallback', ShowLoader)
            .withOption('fnDrawCallback', HideLoader)
            .withPaginationType('full_numbers')
            //Add Bootstrap compatibility
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")

            .withBootstrap()
            .withLanguageSource(Language.getLanguagePath());

        me.tableColumns = [
            DTColumnBuilder.newColumn('Tahun').withTitle('Tahun'),
            DTColumnBuilder.newColumn('KodeRegistrasi').withTitle('Kode Registrasi'),
            DTColumnBuilder.newColumn('NamaGugus').withTitle('Nama Gugus'),
            DTColumnBuilder.newColumn('JudulCIP').withTitle('Judul CIP'),
            DTColumnBuilder.newColumn(null).withTitle('Aksi').withClass('text-center').notSortable()
                .renderWith(actionsHtml)
        ];

        me.LoadComboTahun();
    
    };


    function actionsHtml(data, type, full, meta) {
        var btn = "";
        btn = '<button class="btn btn-xs btn-success" onclick="angular.element(this).scope().excelGeneratePdca(\'' + data.Tahun + '\',\'' + data.KodeRegistrasi + '\')"><i class="fa fa-play"></i> Download Form PDCA II</button>';

        return btn;

    }

    me.LoadComboTahun = function () {
        Select2Helper.GetDataForCombo(BASE_API + 'StreamJuri/DropdownTahun').then(function (result) {
            me.listTahun = result.Dropdown;
        });
    }

    me.excelGeneratePdca = function(tahun, kodeRegistrasi) {
        $http.get(api + 'GetPdca2Report?Periode=' + tahun + '&KodeRegistrasi=' + kodeRegistrasi, { responseType: 'arraybuffer' }).success(function (response) {
            if (response != null) {

                var blob = new Blob([response], { type: 'application/octet-stream' });
                saveAs(blob, 'FormPDCAII.xlsx');
            }
        }).error(function (err, status) {
            NotifBoxError('Warning', err.Message);
        });
        console.log(kodeRegistrasi);
    }

    me.excelGeneratePdcaList = function () {
        $http.get(api + 'GetPdcaList2Report?Periode=' + me.tahun, { responseType: 'arraybuffer' }).success(function (response) {
            if (response != null) {

                var blob = new Blob([response], { type: 'application/octet-stream' });
                saveAs(blob, 'PDCAIIList.xlsx');
            }
        }).error(function (err, status) {
            NotifBoxError('Warning', err.Message);
        });
        console.log(kodeRegistrasi);
    }

    me.find = function () {
        var headers = authSvc.headers();
        headers.Accept = "application/json";

        me.tableOptions = DTOptionsBuilder
            .withOption('ajax', {
                url: api + 'PdcaList?Periode=' + me.tahun,
                type: 'POST',
                accepts: "application/json",
                headers: headers,
                error: function (xhr, ajaxOptions, thrownError) {
                    NotifBoxErrorTable("Laporan PDCA II", xhr.responseText.Message, xhr.status, $state, authSvc);
                }
            })
            .withDataProp('data')
            .withOption('processing', false)
            .withOption('serverSide', true)
            .withOption('fnPreDrawCallback', ShowLoader)
            .withOption('fnDrawCallback', HideLoader)
            .withPaginationType('full_numbers')
            //Add Bootstrap compatibility
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")

            .withBootstrap()
            .withLanguageSource(Language.getLanguagePath());
    }

    me.init(this);
});