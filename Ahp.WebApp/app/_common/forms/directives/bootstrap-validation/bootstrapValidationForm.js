"use strict";

angular.module('SmartAdmin.Forms').directive('bootstrapValidationForm', function () {
    return {
        restrict: 'AE',
        replace: true,
        scope: {},
        compile: function (element, attributes) {
            return function (scope, form) {
                form.bootstrapValidator({
                    feedbackIcons: {
                        valid: 'glyphicon glyphicon-ok',
                        invalid: 'glyphicon glyphicon-remove',
                        validating: 'glyphicon glyphicon-refresh'
                    }
                });

                form.validate();
            };
        }
        //,link: function (scope, form) {
        //    alert('b');
        //}
    }
});