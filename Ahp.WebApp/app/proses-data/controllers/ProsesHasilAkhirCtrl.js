'use strict';

angular.module('app.proses').controller('ProsesHasilAkhirController', function ($scope, $http, $state, $filter, authSvc, Select2Helper, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'nilaiscoring/';
    var api = BASE_API + module;
    var msgTitle = "Hasil Akhir";

    me.init = function (obj) {
        me.vm = obj;
        me.rankingScore = {};
        me.isShowList = 1;
        me.rankingPosisi = {};
        me.Undo();
        me.periode = '';
        me.posisi = '';
        me.jumlah = '';
        me.FindPosisi(null);
        me.loadRankingScore(me.periode);
        me.loadRankingPosisi(me.periode, me.posisi,me.jumlah);
    };

    me.FindPosisi = function (item) {
        Select2Helper.GetDataForCombo(api + 'DropdownPosisi?term=' + item).then(function (result) {
            me.listPosisi = result.Dropdown;
        });
    };

    me.FindJumlah = function (item) {
        Select2Helper.GetDataForCombo(api + 'DropdownJumlah?term=' + item).then(function (result) {
            me.listJumlah = result.Dropdown;
        });
    };


    me.Undo = function () {
        Select2Helper.GetDataForCombo(api + 'DropdownPeriode').then(function (result) {
            me.listPeriode = result.Dropdown;
        });
    };

     
    me.loadRankingScore = function (periode) {
        $http.get(api + 'SpRankingScore?periode=' + periode).success(function (response) {
                if (response !== null) {
                    me.rankingScore = response;
                   
                }
                }).error(function (err, status) {
                    NotifBoxError('Warning', err.Message);
                });
        };

    me.loadRankingPosisi = function (periode,posisi,jumlah) {
        $http.get(api + 'SpRankingPosisi?periode=' + periode+'&posisi='+posisi+'&jumlah='+jumlah).success(function (response) {
            if (response !== null) {
                me.rankingPosisi = response;

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