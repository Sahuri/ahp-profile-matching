'use strict';

angular.module('app.layout').controller('LayoutController', function ($scope, $rootScope, $state, User, authSvc) {
    $scope.init = function () {
        $scope.isAuth = authSvc.authenticationData.isAuth;
        if (!$scope.isAuth)
            $state.go('login');

    }

    var $root = $('body');
    $scope.menuOnTop = localStorage.getItem('sm-menu-on-top') === 'true' || $root.hasClass('menu-on-top');

    localStorage.setItem('sm-menu-on-top', $scope.menuOnTop);
    $root.toggleClass('menu-on-top', $scope.menuOnTop);
    $rootScope.$broadcast('$smartLayoutMenuOnTop', $scope.menuOnTop);
    if ($scope.menuOnTop === 'true' || $scope.menuOnTop === true) {
        $scope.faIconMenu = 'fa fa-hand-o-up';
    }
    else {
        $scope.faIconMenu = 'fa fa-hand-o-left';
    }
    
    $scope.SetTopMenu = function (event) {
        $scope.menuOnTop = ($scope.menuOnTop === 'true' || $scope.menuOnTop === true) ? 'false' : 'true';

        localStorage.setItem('sm-menu-on-top', $scope.menuOnTop);
        $root.toggleClass('menu-on-top', $scope.menuOnTop);
        $rootScope.$broadcast('$smartLayoutMenuOnTop', $scope.menuOnTop);
        if ($scope.menuOnTop) $root.removeClass('minified');

        if ($scope.menuOnTop === 'true' || $scope.menuOnTop === true) {
            $scope.faIconMenu = 'fa fa-hand-o-up';
        }
        else {
            $scope.faIconMenu = 'fa fa-hand-o-left';
        }
    }

    $scope.logOut = function () {
        $.SmartMessageBox({
            title: "",
            content: "Are you sure to exit from AHP & Profile Matching?",
            buttons: '[OK][Batal]',
            theme: 'bg-danger'
        },
        function (action) {
            if (action === "OK") {
                User.initialized = "";
                User.userName = "";

                authSvc.logOut();
                $state.go('login');
            }
            else {
                return;
            }
        });
    }

    $scope.init();
});