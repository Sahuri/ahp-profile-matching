'use strict';

angular.module('app.proses').controller('PerbandinganKriteriaController', function ($scope, $http, $state, $filter, authSvc, Select2Helper, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'perbandingankriteria/';
    var api = BASE_API + module;
    var msgTitle = "Perbandingan Kriteria";

    me.init = function (obj) {
        me.vm = obj;
        me.data = {};
        me.matrix = {};
        me.Undo();
       
    };
    
    me.Undo = function () {
        me.load();
        me.isShowList = true;
        Select2Helper.GetDataForCombo(BASE_API + 'kriteria/DropdownNilaiTarget').then(function (result) {
            me.listKriteriaKiri = result.Dropdown;
            me.listKriteriaKanan = result.Dropdown;
        });
    };

    me.ShowList = function () {
        me.isShowList = true;
    };

    me.Save = function () {
        if (this.frmPerbandinganKriteria.$valid) {
            msgTitle = 'Perbandingan Kriteria';

            $http.post(api + 'Create', JSON.stringify(me.data))
                .success(function (result) {
                    NotifBoxSuccess(msgTitle, "Proses data...");
                    me.matrix = result;
     
                    me.isShowList = false;
                })
                .error(function (error, status) {
                    NotifBoxError(msgTitle, status + " - " + error.Message);
                });

        }
    };

    me.load = function () {
        $http.get(api + 'SpDataTables').success(function (response) {
                if (response !== null) {
                    me.data = response;
                }
                }).error(function (err, status) {
                    NotifBoxError('Warning', err.Message);
                });
        };

    
    me.NumberFormat = function (val) {
        var value = angular.copy(val);
        return IDNumberFormat(value, 4);
    };

    me.init(this);
});