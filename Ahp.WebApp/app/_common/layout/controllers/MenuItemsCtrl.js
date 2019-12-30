'use strict';

angular.module('SmartAdmin.Layout').controller('MenuItemsController', function ($scope, $http, $state, authSvc, Select2Helper, DTOptionsBuilder, DTColumnBuilder, BASE_API, Language) {
    var me = $scope;
    var module = 'administrasimenu/';
    var api = BASE_API + module;

    me.init = function (obj) {
        me.apiMenu = api + "getmenubyuser";
        //$http.get(api + "getmenubyuser")
        //.success(function (result) {
        //    me.data = {
        //        "items": [
        //            {
        //                "title": "Dashboard",
        //                "sref": "app.dashboard",
        //                "icon": "dashboard"
        //            },
        //            {
        //                "title": "Master Data",
        //                "href": "#",
        //                "icon": "book",
        //                "items": [
        //                    {
        //                        "title": "Lokasi",
        //                        "href": "#",
        //                        "icon": "folder-open",
        //                        "items": [
        //                            {
        //                                "title": "Provinsi",
        //                                "sref": "app.provinsi",
        //                                "icon": ""
        //                            },
        //                            {
        //                                "title": "Kabupaten",
        //                                "sref": "app.kabupaten",
        //                                "icon": ""
        //                            },
        //                            {
        //                                "title": "Kecamatan",
        //                                "sref": "app.kecamatan",
        //                                "icon": ""
        //                            },
        //                            {
        //                                "title": "Desa",
        //                                "sref": "app.desa",
        //                                "icon": ""
        //                            }
        //                        ]
        //                    }
        //                ]
        //            },
        //            {
        //                "title": "App Views",
        //                "href": "#",
        //                "icon": "puzzle-piece",
        //                "items": [
        //                    {
        //                        "title": "Projects",
        //                        "sref": "app.appViews.projects",
        //                        "icon": "file-text-o"
        //                    }
        //                ]
        //            }
        //        ]

        //    }

        //    //console.log(me.data);

        //    //me.data = {
        //    //    "items": result
        //    //};
        //    //console.log(me.data);
        //})
        //.error(function (error, status) {
        //    NotifBoxError("Error", status + " - " + error.Message);
        //});

    };


    me.init(this);
});