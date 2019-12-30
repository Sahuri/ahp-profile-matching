'use strict';

angular.module('app.prosespengalihan').controller('ApproverController', function ($scope, $http, $state, authSvc, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language, Select2Helper) {
    var me = $scope;
    var module = 'ProsesPengalihan/';
    var api = BASE_API + module;
    var msgTitle = 'Final Approval';

    me.init = function (obj) {
        me.data = {};
        me.Kurs = 1;
        me.hdr = {};
        me.hdr.totalSender = 0;
        me.hdr.totalReceiver = 0;
        me.isShowList = true;
        me.isNew = false;
        me.listHistory = {};
        me.hr = {};
        me.hr.reasons = '';
        me.Undo();

        var headers = authSvc.headers();
        headers.Accept = "application/json";

        obj.tableOptions = DTOptionsBuilder
            .newOptions()
            .withOption('order', [1, 'asc'])
            .withOption('ajax', {
                // Either you specify the AjaxDataProp here
                // dataSrc: 'data',
                url: api + 'DataTablesApprover',
                type: 'POST',
                accepts: "application/json",
                headers: headers,
                error: function (xhr, ajaxOptions, thrownError) {
                    NotifBoxErrorTable("Approval", xhr.responseText.Message, xhr.status, $state, authSvc);
                }
            })
            .withDataProp('data')
            .withOption('processing', false)
            .withOption('serverSide', true)
            .withOption('fnPreDrawCallback', ShowLoader)
            .withOption('fnDrawCallback', HideLoader)
            .withOption('order', ['0', 'desc'])
            //.withPaginationType('full_numbers')
            //Add Bootstrap compatibility
            .withDOM("<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-12 hidden-xs'l>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>")
            .withOption('rowCallback', me.rowCallback)

            .withOption('responsive', true)
            .withOption('scrollX', true)
            .withOption('scrollY', true)
            .withOption('scrollCollapse', true)
            .withOption('autoWidth', true)
            .withOption('colReorder', true)
            .withFixedColumns({
                rightColumns: 1
            })

            .withBootstrap()
            .withLanguageSource(Language.getLanguagePath());

        obj.tableColumns = [
            DTColumnBuilder.newColumn('Kode').withTitle('NO').withClass('text-primary'),
            DTColumnBuilder.newColumn('CreatedDate').withTitle('Request Date'),
            DTColumnBuilder.newColumn('Plant').withTitle('Plant'),
            DTColumnBuilder.newColumn('NamaCcPengusul').withTitle('Requester'),
            DTColumnBuilder.newColumn('Curr').withTitle('Curr'),
            DTColumnBuilder.newColumn('Amount').withTitle('Amount').withClass('text-right').renderWith(function (value) {
                return me.NumberFormatIDR(value);
            }),
            DTColumnBuilder.newColumn('NamaStatus').withTitle('Status'),
            DTColumnBuilder.newColumn('TypePengalihan').withTitle('Transfer Type'),

        ];
    };

    me.NumberFormatIDR = function (val) {
        var value = angular.copy(val);
        return IDNumberFormat(value, 2);
    };

    me.rowCallback = function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
        $('td', nRow).unbind('click');
        $('td', nRow).bind('click', function () {
            me.$apply(function () {
                me.EditData(aData);
            });
        });
        return nRow;
    };

    me.ShowList = function () {
        me.isShowList = true;
        me.isNew = false;
        
    };
       
    me.EditData = function (data) {
        me.isShowList = false;
        me.isNew = false;
        me.data = data;
        getApproval(data.Kode);

    };

    me.Undo = function () {
        me.data = {};
        me.isNew = true;
    };
        
    function getApproval(cc) {
        $http.post(api + 'ApprovalData?NomorPengalihan=' + cc)
            .success(function (result) {
                if (result.Success) {
                    me.Kurs = result.Data.Header.Kurs;
                    me.listApprove = result.Data.Detail;
                    me.listHistory = result.Data.History;
                    
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

    me.Approve = function () {
        if (this.frmApprover.$valid) {
            $.SmartMessageBox({
                title: "Approve",
                content: "Apakah anda yakin akan menyetujui dokumen ini ?",
                buttons: '[Cancel],[OK]',
                theme: 'bg-warning'
            },
            function (action) {
                if (action === "OK") {
                    msgTitle = 'Approved by Executor';
                    me.hr.reason = "";
                    $http.get(api + 'GetApprovalByApprover?NomorPengalihan=' + me.data.Kode + '&Reasons=' + me.hr.reasons)
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
                    return;
                }
                else {
                    return;
                }
            });
        }
    };

    me.Reject = function () {
        if (this.frmApprover.$valid) {
            $.SmartMessageBox({
                title: "Reject",
                content: "Apakah anda yakin akan menolak dokumen ini ?",
                buttons: '[Cancel],[OK]',
                input: "text",
                placeholder: "Enter reason",
                theme: 'bg-danger'
            },
            function (action, value) {
                if (action === "OK") {
                    if (isNullorEmpty(value) || value == 'undefined') {
                        alert("Please enter reason.");

                        return;
                    }
                    else {
                        msgTitle = 'Rejected';
                        $http.get(api + 'GetRejectedByApprover?NomorPengalihan=' + me.data.Kode + '&Reasons=' + value)
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

                        return;
                    }
                }
                else {
                    return;
                }
            });

            $("#txt1").val("");
            $("#bot2-Msg1").prop("disabled", true);
            $("#txt1").on("keyup", function (e) {
                if ($(this).val() == '') {
                    $("#bot2-Msg1").prop("disabled", true);
                }
                else {
                    $("#bot2-Msg1").prop("disabled", false);
                }
                e.preventDefault;
            });
        }
    };

    
    me.init(this);
});