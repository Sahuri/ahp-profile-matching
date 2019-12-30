'use strict';

angular.module('SmartAdmin.Forms').directive('smartTagsinput', function () {
    return {
        restrict: 'EA',
        require: 'ngModel',
        //compile: function (tElement, tAttributes) {
        //    tElement.removeAttr('smart-tagsinput data-smart-tagsinput');
        //    tElement.tagsinput();
        //}
        link: function (scope, tElement, tAttributes, ngModel) {
            tElement.removeAttr('smart-tagsinput data-smart-tagsinput');
            tElement.tagsinput();
        }

    }
});