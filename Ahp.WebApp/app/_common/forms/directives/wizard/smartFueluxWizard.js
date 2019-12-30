'use strict';
//http://getfuelux.com/javascript.html#wizard-examples
//https://smartadmin-ng2.github.io/#/forms/dropzone

angular.module('SmartAdmin.Forms').directive('smartFueluxWizard', function () {
    return {
        restrict: 'EAC',
        scope: {
            smartWizardCallback: '&',
            CurrentStep: '=currentStep'
        },
        require: 'ngModel',
        link: function (scope, element, attributes, ngModel) {
            scope.$on('CURRENT_MEMO', function (event, data) {
                console.log(data);
                x = data;
            });

            var wizard = element.wizard();
            var wizardData = wizard.data('fu.wizard');
            var $wizard = $('#' + $('div.wizard').attr('id'));
            var $form = element.find('form');
            wizardData.currentStep = scope.CurrentStep + 1; //1;
            CURRENT_STEP = wizardData.currentStep;
           
            wizard.on('actionclicked.fu.wizard', function (e, data) {
                if (data.direction == 'next') {
                    var step = (wizardData.currentStep + 1);
                    CURRENT_STEP = data.step + 1;
                    //ngModel.$setViewValue(data.step + 1);

                    $('[data-step=' + step + ']', element).addClass('active').siblings('[data-step]').removeClass('active');
                    wizardData.currentStep = step;
                    scope.$emit('CURRENT_STEP', CURRENT_STEP);
                }
                if (data.direction == 'previous') {
                    $(element).click();
                    CURRENT_STEP = data.step - 1;
                    scope.$emit('CURRENT_STEP', CURRENT_STEP);
                    //ngModel.$setViewValue(data.step - 1);
                    //var step  = (wizardData.currentStep - 1);
                    //$('[data-step=' + step + ']', element).addClass('active').siblings('[data-step]').removeClass('active');
                }


                if ($form.data('validator')) {
                    if (!$form.valid()) {
                        $form.data('validator').focusInvalid();
                        e.preventDefault();
                    }
                }
            });

            wizard.on('finished.fu.wizard', function (e, data) {
                var formData = {};
                _.each($form.serializeArray(), function (field) {
                    formData[field.name] = field.value
                });
                if (typeof scope.smartWizardCallback() === 'function') {
                    scope.smartWizardCallback()(formData)
                }
            });

            wizard.on('stepclicked.fu.wizard', function (e, data) {
                console.log('CLICK');
            });

            element.on('click', '[data-step]', function (e) {
                var step = parseInt($(this).data('step'));
                scope.$emit('CURRENT_STEP', step);

                setStep(step);
                e.preventDefault();
            });

            function setStep(step) {
                $wizard.wizard('selectedItem', {
                    step: step
                });

                CURRENT_STEP = step;
                //ngModel.$setViewValue(step);

                $('[data-step=' + step + ']', element).addClass('active').siblings('[data-step]').removeClass('active');
                wizardData.currentStep = step;
                
            }
        }
    }
});