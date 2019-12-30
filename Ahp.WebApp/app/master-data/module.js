"use strict";


angular.module('app.masterdata', ['ui.router', 'datatables', 'datatables.bootstrap', 'ngHandsontable']);

angular.module('app.masterdata').config(function ($stateProvider) {

    $stateProvider
        .state('app.masterdata', {
            abstract: true,
            data: {
                title: 'Master Data'
            }
        })
        

        .state('app.masterdata.calonkaryawan', {
            url: '/master-data/calonkaryawan',
            data: {
                title: 'Calon Karyawan'
            },
            views: {
                "content@app": {
                    controller: 'CalonKaryawanController as ctrl',
                    templateUrl: 'app/master-data/views/calon-karyawan.html'
                }
            }
            , resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])

                }
            }
        })
        .state('app.masterdata.lowongan', {
            url: '/master-data/lowongan',
            data: {
                title: 'Lowongan'
            },
            views: {
                "content@app": {
                    controller: 'LowonganController as ctrl',
                    templateUrl: 'app/master-data/views/lowongan.html'
                }
            }
            , resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ]);

                }
            }
        })
        .state('app.masterdata.kriteria', {
            url: '/master-data/kriteria',
            data: {
                title: 'Kriteria'
            },
            views: {
                "content@app": {
                    controller: 'KriteriaController as ctrl',
                    templateUrl: 'app/master-data/views/kriteria.html'
                }
            }
            , resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ]);

                }
            }
        })
        
});
