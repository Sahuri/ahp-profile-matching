
"use strict";

angular.module('app.forms').controller('FormLayoutsCtrl', function ($scope, $uibModal, $log) {

    $scope.openModal = function () {
        //var $modalInstance = $uibModal.open({
        //    templateUrl: 'app/forms/views/form-layout-modal.html',
        //    controller: 'ModalDemoCtrl' 
        //});

        //var parentElem = parentSelector ?
        //angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
        var modalInstance = $uibModal.open({
            animation: this.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'app/forms/views/form-layout-modal.html',
            controller: 'ModalDemoCtrl',
            //appendTo: parentElem,
        });

        modalInstance.result.then(function () {
            $log.info('Modal closed at: ' + new Date());

        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });


    };

    $scope.registration = {};

    $scope.$watch('registration.date', function(changed){
        console.log('registration model changed', $scope.registration)
    })


});