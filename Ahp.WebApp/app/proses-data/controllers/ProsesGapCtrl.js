'use strict';

angular.module('app.proses').controller('ProsesGapController', function ($scope, $http, $state, $filter, authSvc, Select2Helper, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'nilaiscoring/';
    var api = BASE_API + module;
    var msgTitle = "Nilai GAP";

    me.init = function (obj) {
        me.vm = obj;
        me.data = {};
        me.calonKaryawanGap = {};
        me.isNew = false;
        me.posisi = "";
        me.periode = "";
        me.isShowList = 1;
        me.Undo();
        me.load(me.posisi, me.periode);
    };

    me.FindPosisi = function (item) {
        Select2Helper.GetDataForCombo(api + 'DropdownPosisi?term=' + item).then(function (result) {
            me.listPosisi = result.Dropdown;
        });
    };

    me.Undo = function () {
        Select2Helper.GetDataForCombo(api + 'DropdownPeriode').then(function (result) {
            me.listPeriode = result.Dropdown;

        });
        Select2Helper.GetDataForCombo(api + 'DropdownCalonKaryawan').then(function (result) {
            me.listKaryawan = result.Dropdown;

        });

        Select2Helper.GetDataForCombo(BASE_API + 'kriteria/DropdownNilaiTarget').then(function (result) {
            me.listNilai = result.Dropdown;
            
        });
    };

    me.SetCalonKaryawanGap = function () {
        var nilaiGapBobots = [];
        me.data.kriterias.forEach(function (val) {
            var nilaiGapBobot = {
                kodeKriteria: val.Kode,
                kriteria: val.Nama,
                nilaiGap: 0,
                nilaiBobot: 0,
                nilaiAlternatif:0,
                nilaiTarget: val.NilaiTarget
            };
            nilaiGapBobots.push(nilaiGapBobot);
        });
        me.calonKaryawanGap = {
            kodeKaryawan: "",
            namaKaryawan: "",
            kodeLowongan: "",
            periode:"",
            nilaiGapBobots: nilaiGapBobots
        };
    };


    me.NewData = function () {
        me.SetCalonKaryawanGap();
        me.isShowList = 2;
        me.isNew = true;
        me.Undo();
    };

    me.EditData = function (data) {
        me.FindPosisi(data.periode);
            me.calonKaryawanGap = data;
            me.isShowList = 2;
            me.isNew = false;
       
       
    };

    me.ShowList = function () {
        me.isShowList = 1;
        me.isNew = false;
    };

    me.ShowNext = function () {
        if (me.isShowList === 3) {
            me.isShowList = 1;
        } else {
            me.isShowList = 3;
        }
        
        me.isNew = false;
    };

    me.Save = function () {
        if (this.frmProsesGap.$valid) {
            msgTitle = 'Simpan Nilai Gap';
            if (me.isNew) {
                $http.post(api + 'Create', JSON.stringify(me.calonKaryawanGap))
                    .success(function (result) {
                        if (result.Success) {
                            NotifBoxSuccess(msgTitle, result.Message);
                            me.load(me.posisi, me.periode);
                            
                        }
                        else {
                            NotifBoxWarning(msgTitle, result.Message);
                        }
                    })
                    .error(function (error, status) {
                        NotifBoxError(msgTitle, status + " - " + error.Message);
                    });
            } else {
                msgTitle = 'Update Nilai Gap';
                $http.post(api + 'Update', JSON.stringify(me.calonKaryawanGap))
                    .success(function (result) {
                        if (result.Success) {
                            NotifBoxSuccess(msgTitle, result.Message);
                            me.load(me.posisi, me.periode);
                            
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

    me.load = function (posisi, periode) {
        $http.get(api + 'SpDataTables?posisi=' + posisi + '&periode=' + periode).success(function (response) {
                if (response !== null) {
                    me.data = response;
                    me.ShowList();
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