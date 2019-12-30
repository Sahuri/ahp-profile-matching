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
