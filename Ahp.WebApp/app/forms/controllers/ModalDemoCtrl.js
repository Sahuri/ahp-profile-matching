'use strict'

angular.module('app.forms').controller('ModalDemoCtrl', function ($scope, $uibModalInstance) {
    $scope.closeModal = function(){
        $uibModalInstance.dismiss('cancel');
    }
});
// Documentation https://angular-ui.github.io/bootstrap/#!#%2Fmodal