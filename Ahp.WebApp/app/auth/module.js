"use strict";

angular.module('app.auth', [
    'ui.router', 'ui.bootstrap', 'datatables', 'datatables.bootstrap',
    //        ,
    //        'ezfb',
    //        'googleplus'
]).config(function ($stateProvider
    //        , ezfbProvider
    //        , GooglePlusProvider
) {
    //        GooglePlusProvider.init({
    //            clientId: authKeys.googleClientId
    //        });
    //
    //        ezfbProvider.setInitParams({
    //            appId: authKeys.facebookAppId
    //        });
    $stateProvider.state('realLogin', {
        url: '/real-login',

        views: {
            root: {
                templateUrl: "app/auth/login/login.html",
                controller: 'ExternalLoginCtrl'
            }
        },
        data: {
            title: 'Login',
            rootId: 'extra-page'
        }

    })

        .state('login', {
            url: '/login',
            views: {
                root: {
                    templateUrl: 'app/auth/views/login.html',
                    controller: 'LoginCtrl'
                }
            },
            data: {
                title: 'Login',
                htmlId: '' //'extr-page'
            },
            resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])

                }
            }
        })

        .state('register', {
            url: '/registrasi-anggota',
            views: {
                root: {
                    templateUrl: 'app/auth/views/register.html',
                    controller: 'RegisterController'
                }
            },
            data: {
                title: 'Registrasi Anggota',
                htmlId: 'extr-page'
            },
            params: {
                data: {},
                dropdown: {}
            },
            resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])
                }
            }
        })

        .state('activation', {
            url: '/aktivasi-anggota',
            views: {
                root: {
                    templateUrl: 'app/auth/views/member-activation.html',
                    controller: 'MemberActivationController as ctrl'
                }
            },
            data: {
                title: 'Aktivasi Anggota',
                htmlId: 'extr-page'
            },
            resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])
                }
            }
        })

        .state('displaymember', {
            url: '/profil-anggota',
            views: {
                root: {
                    templateUrl: 'app/auth/views/member-display.html',
                    controller: 'MemberDisplayController as ctrl'
                }
            },
            data: {
                title: 'Profil Anggota',
                htmlId: 'extr-page'
            },
            resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])
                }
            }
        })

        .state('changepassmember', {
            url: '/anggota/ubah-password',
            views: {
                root: {
                    templateUrl: 'app/auth/views/changepass-member.html',
                    controller: 'ChangePasswordController'
                }
            },
            data: {
                title: 'Ubah Password',
                htmlId: 'extr-page'
            },
            resolve: {
                srcipts: function (lazyScript) {
                    return lazyScript.register([
                        'build/vendor.ui.js'
                    ])
                }
            }
        })

        .state('forgotPassword', {
            url: '/forgot-password',
            views: {
                root: {
                    templateUrl: 'app/auth/views/forgot-password.html'
                }
            },
            data: {
                title: 'Forgot Password',
                htmlId: 'extr-page'
            }
        })

        .state('lock', {
            url: '/lock',
            views: {
                root: {
                    templateUrl: 'app/auth/views/lock.html'
                }
            },
            data: {
                title: 'Locked Screen',
                htmlId: 'lock-page'
            }
        })


}).constant('authKeys', {
    googleClientId: '',
    facebookAppId: ''
});
