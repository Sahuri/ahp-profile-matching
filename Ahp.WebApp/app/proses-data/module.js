"use strict";


angular.module('app.proses', [
    'ui.router', 'datatables', 'datatables.bootstrap', 'datatables.fixedcolumns', 'datatables.fixedheader'
])
.config(function ($stateProvider) {

    $stateProvider
        .state('app.proses', {
            abstract: true,
            data: {
                title: 'Proses Data'
            }
        })
        .state('app.proses.nilaigap', {
            url: '/proses-data/nilai-gap',
            data: {
                title: 'Proses GAP'
            },
            views: {
                "content@app": {
                    controller: 'ProsesGapController as ctrl',
                    templateUrl: "app/proses-data/views/proses-gap.html"
                }
            }
        })
        .state('app.proses.hasilakhir', {
            url: '/proses-data/proses-hasil-akhir',
            data: {
                title: 'Laporan Hasil Akhir'
            },
            views: {
                "content@app": {
                    controller: 'ProsesHasilAkhirController as ctrl',
                    templateUrl: "app/proses-data/views/proses-hasil-akhir.html"
                }
            }
        })
        .state('app.proses.perbandingankriteria', {
            url: '/proses-data/perbandingan-kriteria',
            data: {
                title: 'Perbandingan Kriteria'
            },
            views: {
                "content@app": {
                    controller: 'PerbandinganKriteriaController as ctrl',
                    templateUrl: "app/proses-data/views/perbandingan-kriteria.html"
                }
            }
        });
});
