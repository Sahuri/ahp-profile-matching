"use strict";


angular.module('app.laporan', ['ui.router', 'datatables', 'datatables.bootstrap']);

angular.module('app.laporan').config(function ($stateProvider) {

    $stateProvider
        .state('app.laporan', {
            abstract: true,
            data: {
                title: 'Report'
            }
        })

        .state('app.laporan.byfunction', {
            url: '/report/buget-transfer-function',
            data: {
                title: 'Budget Transfer by Function'
            },
            views: {
                "content@app": {
                    controller: 'ByFunctionController as ctrl',
                    templateUrl: 'app/laporan/views/by-function.html'
                }
            },
            resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])

                }
            }
        })
        .state('app.laporan.bygroup', {
            url: '/report/buget-transfer-cost-element-group',
            data: {
                title: 'Budget Transfer by Cost Element Group'
            },
            views: {
                "content@app": {
                    controller: 'ByGroupController as ctrl',
                    templateUrl: 'app/laporan/views/by-group.html'
                }
            },
            resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])

                }
            }
        })
        
});
