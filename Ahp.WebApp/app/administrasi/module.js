"use strict";


angular.module('app.administrasi', [ 'ui.router', 'datatables', 'datatables.bootstrap']);

angular.module('app.administrasi').config(function ($stateProvider) {

    $stateProvider
        .state('app.administrasi', {
            abstract: true,
            data: {
                title: ''
            }
        })

        .state('app.administrasi.user', {
            url: '/administrasi/user',
            data: {
                title: 'Administrasi / User'
            },
            views: {
                "content@app": {
                    controller: 'UserController as ctrl',
                    templateUrl: 'app/administrasi/views/user.html'
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

        .state('app.administrasi.role', {
            url: '/administrasi/role',
            data: {
                title: 'Administrasi / Role'
            },
            views: {
                "content@app": {
                    controller: 'RoleController as ctrl',
                    templateUrl: 'app/administrasi/views/role.html'
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
        .state('app.administrasi.roleuser', {
            url: '/administrasi/roleuser',
            data: {
                title: 'Administrasi / Role User'
            },
            views: {
                "content@app": {
                    controller: 'RoleUserController as ctrl',
                    templateUrl: 'app/administrasi/views/role-user.html'
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
        .state('app.administrasi.menu', {
            url: '/administrasi/menu',
            data: {
                title: 'Administrasi / Menu'
            },
            views: {
                "content@app": {
                    controller: 'MenuController as ctrl',
                    templateUrl: 'app/administrasi/views/menu.html'
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
        .state('app.administrasi.tombol', {
            url: '/administrasi/tombol',
            data: {
                title: 'Administrasi / Tombol'
            },
            views: {
                "content@app": {
                    controller: 'TombolController as ctrl',
                    templateUrl: 'app/administrasi/views/tombol.html'
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
        .state('app.administrasi.hakakses', {
            url: '/administrasi/hak-akses',
            data: {
                title: 'Administrasi / Hak Akses'
            },
            views: {
                "content@app": {
                    controller: 'HakAksesController as ctrl',
                    templateUrl: 'app/administrasi/views/hak-akses.html'
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