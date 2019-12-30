"use strict";


angular.module('app.prosespengalihan', ['ui.router', 'datatables', 'datatables.bootstrap', 'ngHandsontable']);

angular.module('app.prosespengalihan').config(function ($stateProvider) {

    $stateProvider
        .state('app.prosespengalihan', {
            abstract: true,
            data: {
                title: 'Process Budget Transfer'
            }
        })
        .state('app.prosespengalihan.receiverbh', {
            url: '/budget-holder/receiverbh',
            data: {
                title: 'Budget Holder(Receiver) Approval'
            },
            views: {
                "content@app": {
                    controller: 'ReceiverBhController as ctrl',
                    templateUrl: 'app/proses-pengalihan/views/receiver-bh.html'
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
        .state('app.prosespengalihan.senderbh', {
            url: '/budget-holder/senderbh',
            data: {
                title: 'Budget Holder(Sender) Approval'
            },
            views: {
                "content@app": {
                    controller: 'SenderBhController as ctrl',
                    templateUrl: 'app/proses-pengalihan/views/sender-bh.html'
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
        .state('app.prosespengalihan.verificator', {
            url: '/budget-holder/verificator',
            data: {
                title: 'Verificator Approval'
            },
            views: {
                "content@app": {
                    controller: 'VerificatorController as ctrl',
                    templateUrl: 'app/proses-pengalihan/views/verificator.html'
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
        .state('app.prosespengalihan.submiter', {
            url: '/budget-holder/submiter',
            data: {
                title: 'Submiter Approval'
            },
            views: {
                "content@app": {
                    controller: 'SubmiterController as ctrl',
                    templateUrl: 'app/proses-pengalihan/views/submiter.html'
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
        .state('app.prosespengalihan.approver', {
            url: '/budget-holder/approver',
            data: {
                title: 'Final Approval'
            },
            views: {
                "content@app": {
                    controller: 'ApproverController as ctrl',
                    templateUrl: 'app/proses-pengalihan/views/approver.html'
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
        .state('app.prosespengalihan.executor', {
            url: '/budget-holder/executor',
            data: {
                title: 'Executor Approval'
            },
            views: {
                "content@app": {
                    controller: 'ExecutorController as ctrl',
                    templateUrl: 'app/proses-pengalihan/views/executor.html'
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
        .state('app.prosespengalihan.history', {
            url: '/budget-holder/history',
            data: {
                title: 'Budget Transfer History'
            },
            views: {
                "content@app": {
                    controller: 'HistoryController as ctrl',
                    templateUrl: 'app/proses-pengalihan/views/history.html'
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
});
