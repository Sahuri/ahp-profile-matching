'use strict';

angular.module('app.dashboard').controller('DashboardController', function ($scope, $http, $interval, $state, authSvc, $filter, BASE_API) {
    var msgTitle = "Dashboard";
    var me = $scope;
    var module = 'Dashboard/';
    var api = BASE_API + module;

    me.init = function () {
        me.dataCountApproval = 0;
        me.dataCountPenjurian = 0;
        me.dataCountAudit = 0;
        me.dataCountNewReg = 0;
        countApproval();
        countPenjurian();
        countAudit();
        countNewReg();
    }

    function countApproval() {
        //var headers = authSvc.headers();
        //headers.Accept = "application/json";
        //$http.get(api + 'GetCountApproval').success(function (response) {
        //    me.dataCountApproval = response;
            
        //}).error(function (err, status) {
        //    console.log(err);
        //});
    }

    function countAudit() {
        //var headers = authSvc.headers();
        //headers.Accept = "application/json";
        //$http.get(api + 'GetCountAudit').success(function (response) {
        //    me.dataCountAudit = response;

        //}).error(function (err, status) {
        //    console.log(err);
        //});
    }

    function countNewReg() {
        //var headers = authSvc.headers();
        //headers.Accept = "application/json";
        //$http.get(api + 'GetCountNewReg').success(function (response) {
        //    me.dataCountNewReg = response;

        //}).error(function (err, status) {
        //    console.log(err);
        //});
    }

    function countPenjurian() {
        //var headers = authSvc.headers();
        //headers.Accept = "application/json";
        //$http.get(api + 'GetCountPenjurian').success(function (response) {
        //    me.dataCountPenjurian = response;

        //}).error(function (err, status) {
        //    console.log(err);
        //});
    }

    me.init();
});