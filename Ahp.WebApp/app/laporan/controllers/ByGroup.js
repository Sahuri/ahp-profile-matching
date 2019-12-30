'use strict';

angular.module('app.laporan').controller('ByGroupController', function ($scope, $http, $state, authSvc, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language, Select2Helper, $filter) {
    var me = $scope;
    var module = 'pengalihan/';
    var api = BASE_API + module;
    var msgTitle = 'Report Budget Transfer by Group';

    me.init = function (obj) {
        me.data = {};
        me.area = '';
        me.startDate = DateFormat(new Date());
        me.endDate = DateFormat(new Date());
        me.listArea = [];

        me.loadComboArea();

        me.vm = obj;
        me.vm.tableOptions = DTOptionsBuilder
            .newOptions()
            .withOption('ajax', me.dataSource())
            .withDataProp('data')
            .withOption('processing', false)
            .withOption('serverSide', true)
            .withOption('fnPreDrawCallback', ShowLoader)
            .withOption('fnDrawCallback', HideLoader)
            // .withPaginationType('full_numbers')

            .withOption('responsive', true)
            .withOption('scrollX', true)
            .withOption('scrollY', true)
            .withOption('scrollCollapse', true)
            .withOption('autoWidth', true)
            .withOption('colReorder', true)
            .withOption('order', [[7, 'asc']])

            //Add Bootstrap compatibility
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
                "t" +
                "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")

            .withBootstrap()
            .withLanguageSource(Language.getLanguagePath());

        me.vm.tableColumns = [
            DTColumnBuilder.newColumn('Nama').withTitle('Group').withClass('text-primary'),
            DTColumnBuilder.newColumn('ccSender').withTitle('Sender').withClass('text-right').renderWith(function (value) {
                return me.NumberFormatIDR(value);
            }),
            DTColumnBuilder.newColumn('ccReceiver').withTitle('Receiver').withClass('text-right').renderWith(function (value) {
                return me.NumberFormatIDR(value);
            }),
            DTColumnBuilder.newColumn('ccTot').withTitle('Total').withClass('text-right').renderWith(function (value) {
                return me.NumberFormatIDR(value);
            }),
            DTColumnBuilder.newColumn('ammountSender').withTitle('Sender').withClass('text-right').renderWith(function (value) {
                return me.NumberFormatIDR(value);
            }),
            DTColumnBuilder.newColumn('ammountReceiver').withTitle('Receiver').withClass('text-right').renderWith(function (value) {
                return me.NumberFormatIDR(value);
            }),
            DTColumnBuilder.newColumn('ammountTotPersen').withTitle('%').withClass('text-right').renderWith(function (value) {
                return IDNumberFormat(value, 1);
            }),
            DTColumnBuilder.newColumn('kode').withTitle('kode').notVisible().renderWith(function (value) {
                return value * 1;
            })
        ];

        me.vm.dtInstance = {};
    };

    me.downloadPDF = function () {
        var startDate = me.startDate.includes('.') ? DateReformat(me.startDate) : me.startDate;
        var endDate = me.endDate.includes('.') ? DateReformat(me.endDate) : me.endDate;

        $http.get(api + 'getreportbygrouppdf?area=' + me.area + '&startDate=' + startDate + '&endDate=' + endDate, { responseType: 'arraybuffer' }).success(function (response) {
            if (response !== null) {
                var file = new Blob([response], { type: 'application/pdf' });
                var fileURL = URL.createObjectURL(file);
                window.open(fileURL, '_blank');
            }
        }).error(function (err, status) {
            NotifBoxError(msgTitle, "Download document failed.");
        });
    };

    me.loadComboArea = function () {
        Select2Helper.GetDataForCombo(api + 'dropdownareaforreport').then(function (result) {
            me.listArea = result.Dropdown;

        });
    };

    me.find = function () {
        if (IsNullOrEmpty(me.startDate)) {
            NotifBoxWarning(msgTitle, "Start Date required.");
            return;
        }
        else {
            if (IsNullOrEmpty(me.endDate)) {
                NotifBoxWarning(msgTitle, "End Date required.");
                return;
            }
            else {
                me.vm.dtInstance.reloadData(function () {
                    me.vm.tableOptions.withOption('ajax', me.dataSource());
                }, true);
            }
        }
    };

    me.NumberFormatIDR = function (val) {
        var value = angular.copy(val);
        return IDNumberFormat(value, 0);
    };

    me.dataSource = function () {
        var startDate = me.startDate.includes('.') ? DateReformat(me.startDate) : me.startDate;
        var endDate = me.endDate.includes('.') ? DateReformat(me.endDate) : me.endDate;

        return {
            url: api + 'reportbygroupdatatable?area=' + me.area + '&startDate=' + startDate + '&endDate=' + endDate,
            type: 'POST',
            accepts: "application/json",
            headers: authSvc.headers(),
            error: function (xhr, ajaxOptions, thrownError) {
                var msg = "Loading data failed.";
                NotifBoxErrorTable(msgTitle, msg, xhr.status, $state, authSvc);
            }
        };
    };

    me.$watch("listArea", function (n, o) {
        var count = n.length;
        if (count == 1) {
            me.area = n[0].id;
            me.areaDesc = n[0].text;

            me.vm.tableOptions.withOption('ajax', me.dataSource());
        }
    }, true);

    me.init(this);
});