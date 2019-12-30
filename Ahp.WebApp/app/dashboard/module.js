"use strict";


angular.module('app.dashboard', [
    'ui.router',
    'ngResource'
])
    .config(function ($stateProvider) {

        $stateProvider
            .state('app.dashboard', {
                url: '/dashboard',
                data: {
                    title: 'Dashboard'
                },
                views: {
                    "content@app": {
                        templateUrl: 'app/dashboard/views/dashboard.html',
                        controller: 'DashboardController as ctrl'
                    }
                },
                resolve: {
                    scripts: function (lazyScript) {
                        return lazyScript.register([
                            'build/vendor.graphs.js'
                        ]);
                    }
                }
            })
    });
