'use strict';

angular.module('app.prosespengalihan').controller('HistoryController', function ($scope, $http, $state, authSvc, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language, Select2Helper, $filter) {
    var obj = this;
    var me = $scope;
    var module = 'history/';
    var api = BASE_API + module;
    var msgTitle = 'Budget Transfer History';

    me.init = function () {
        me.data = {};
        me.plant = '';
        me.startDate = DateFormat(new Date());
        me.endDate = DateFormat(new Date());
        me.listPlant = [];

        me.Kurs = 1;
        me.hdr = {};
        me.hdr.totalSender = 0;
        me.hdr.totalReceiver = 0;
        me.isShowList = true;
        me.listHistory = {};
        me.hr = {};
        me.hr.reasons = '';

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
            .withOption('order', ['0', 'desc'])

            .withFixedColumns({
                leftColumns: 2,
                rightColumns: 1
            })

            //Add Bootstrap compatibility
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")
            .withOption('rowCallback', me.rowCallback)
            .withBootstrap()
            .withLanguageSource(Language.getLanguagePath());


        me.vm.tableColumns = [
            DTColumnBuilder.newColumn('Kode').withTitle('No.').withClass('text-primary'),
            DTColumnBuilder.newColumn('TrxDate').withTitle('Date').renderWith(function (value) {
                return DateTimeFormat(value);
            }),
            //DTColumnBuilder.newColumn('TkoPengalihan').withTitle('TKO'),
            //DTColumnBuilder.newColumn('Plant').withTitle('Plant'),
            DTColumnBuilder.newColumn('Requester').withTitle('Requester'),
            DTColumnBuilder.newColumn('AmountReceiver').withTitle('Amount Receiver').withClass('text-right').renderWith(function (value) {
                return me.NumberFormatIDR(value);
            }),
            DTColumnBuilder.newColumn('AmountSender').withTitle('Amount Sender').withClass('text-right').renderWith(function (value) {
                return me.NumberFormatIDR(value);
            }),
            DTColumnBuilder.newColumn('ApprovalStatusName').withTitle('Approval Status'),
            DTColumnBuilder.newColumn('TypePengalihan').withTitle('Transfer Type'),

            //DTColumnBuilder.newColumn('Bh1Receiver').withTitle('BH1(Receiver)'),
            //DTColumnBuilder.newColumn('Bh2Receiver').withTitle('BH2(Receiver)'),
            //DTColumnBuilder.newColumn('Bh1Sender').withTitle('BH1(Sender)'),
            //DTColumnBuilder.newColumn('Bh2Sender').withTitle('BH2(Sender)'),
            //DTColumnBuilder.newColumn('Verifikator').withTitle('Verificator'),
            //DTColumnBuilder.newColumn('Submiter1').withTitle('Submiter'),
            //DTColumnBuilder.newColumn('Approver1').withTitle('Approver 1'),
            //DTColumnBuilder.newColumn('Approver2').withTitle('Approver 2'),

            //DTColumnBuilder.newColumn('executor').withTitle('Executor'),
            DTColumnBuilder.newColumn('ExecutorStatusName').withTitle('Executor Status'),
            DTColumnBuilder.newColumn(null).withTitle('Action').withClass('text-center').notSortable().renderWith(actionsHtml)
        ];

        me.vm.dtInstance = {};
        me.LoadComboPlant();
    };

    function actionsHtml(data, type, full, meta) {
        var isSubmited = data.Status !== "0" ? 'disabled="disabled"' : '';
        var clsPrint = isSubmited == '' ? 'disabled="disabled"' : '';
        var btnDownload = '<button class="btn btn-xs btn-primary" ' + clsPrint + ' onclick="angular.element(this).scope().downloadForm(\'' + full.Kode + '\')"><i class="fa fa-print"></i> Print</button>';

        return btnDownload;
    };

    me.downloadForm = function (NomorPengalihan) {
        $http.get(BASE_API + 'ProsesPengalihan/GetFormulir?NomorPengalihan=' + NomorPengalihan, { responseType: 'arraybuffer' }).success(function (response) {
            if (response !== null) {
                var file = new Blob([response], { type: 'application/pdf' });
                var fileURL = URL.createObjectURL(file);
                window.open(fileURL, '_blank');
                //var blob = new Blob([response], { type: 'application/pdf' });
                //saveAs(blob, 'FormulirPengalihan_' + NomorPengalihan + '.pdf');
            }
        }).error(function (err, status) {
            NotifBoxError(msgTitle, "Print document failed.");
        });
    };

    me.downloadPDF = function () {
        var startDate = me.startDate.includes('.') ? DateReformat(me.startDate) : me.startDate;
        var endDate = me.endDate.includes('.') ? DateReformat(me.endDate) : me.endDate;

        $http.get(api + 'gethistorypdf?plant=' + me.plant + '&startDate=' + startDate + '&endDate=' + endDate, { responseType: 'arraybuffer' }).success(function (response) {
            if (response !== null) {
                var file = new Blob([response], { type: 'application/pdf' });
                var fileURL = URL.createObjectURL(file);
                window.open(fileURL, '_blank');
            }
        }).error(function (err, status) {
            NotifBoxError(msgTitle, "Download document failed.");
        });
    };


    me.LoadComboPlant = function () {
        Select2Helper.GetDataForCombo(BASE_API + 'MasterCostCenter/DropdownPlantByUser').then(function (result) {
            me.listPlant = result.Dropdown;
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
        return IDNumberFormat(value, 2);
    };

    me.dataSource = function () {
        var startDate = me.startDate.includes('.') ? DateReformat(me.startDate) : me.startDate;
        var endDate = me.endDate.includes('.') ? DateReformat(me.endDate) : me.endDate;

        return {
            url: api + 'DataTables?Plant=' + me.plant + '&StartDate=' + startDate + '&EndDate=' + endDate,
            type: 'POST',
            accepts: "application/json",
            headers: authSvc.headers(),
            error: function (xhr, ajaxOptions, thrownError) {
                var msg = "Loading data failed.";
                NotifBoxErrorTable(msgTitle, msg, xhr.status, $state, authSvc);
            }
        };
    };

    me.rowCallback = function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('click');
        $('td', nRow).bind('click', function () {
            console.log($(this).text());
            if ($(this).text() != ' Print') {
                me.$apply(function () {
                    me.EditData(aData);
                });
            }
        });
        return nRow;
    };

    me.EditData = function (data) {
        me.isShowList = false;
        me.data = data;

        getApproval(data.Kode);

    };

    function getApproval(cc) {
        $http.post(BASE_API + 'ProsesPengalihan/ApprovalData?NomorPengalihan=' + cc)
            .success(function (result) {
                if (result.Success) {
                    me.Kurs = result.Data.Header.Kurs;
                    me.listApprove = result.Data.Detail;
                    me.listHistory = result.Data.History;
                    me.data.FMDocPosting = result.Data.Header.FMDocPosting;

                    var Receiver = [];
                    var Sender = [];
                    angular.forEach(me.listApprove, function (value, key) {
                        if (value.TipeCc === 'RECEIVER') {
                            Receiver.push(value);
                        } else {
                            Sender.push(value);
                        }
                    });
                    me.hdr.totalReceiver = sumFromArray(Receiver, 'Amount');
                    me.hdr.totalSender = sumFromArray(Sender, 'Amount');
                }
                else {
                    NotifBoxWarning('Warning', result.Message);
                }
            })
            .error(function (error, status) {
                NotifBoxError('Warning', status + " - " + error.Message);
            });
    }

    me.ShowList = function () {
        me.isShowList = true;
    };

    me.init();
});