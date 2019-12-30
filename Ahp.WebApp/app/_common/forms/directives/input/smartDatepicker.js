"use strict";

angular.module('SmartAdmin.Forms').directive('smartDatepicker', function ($parse, $timeout) {
    return {
        restrict: 'A',
        scope: {
            options: '='
        },
        require: 'ngModel',
        link: function (scope, tElement, tAttributes, ngModel) {
            //tElement.removeAttr('smartDatepicker');

            var onSelectCallbacks = [];
            //var ngModel = $parse(tAttributes.ngModel);
            if (tAttributes.minRestrict) {
                onSelectCallbacks.push(function (selectedDate) {
                    $(tAttributes.minRestrict).datepicker('option', 'minDate', selectedDate);
                });
            }
            if (tAttributes.maxRestrict) {
                onSelectCallbacks.push(function (selectedDate) {
                    $(tAttributes.maxRestrict).datepicker('option', 'maxDate', selectedDate);
                });
            }

            ////Let others know Ahput changes to the data field
            //onSelectCallbacks.push(function (selectedDate) {
            //    //CVB - 07/14/2015 - Update the scope with the selected value
            //    tElement.triggerHandler("change");

            //    //CVB - 07/17/2015 - Update Bootstrap Validator
            //    var form = tElement.closest('form');

            //    if (typeof form.bootstrapValidator == 'function')
            //        form.bootstrapValidator('revalidateField', tElement.attr('name'));
            //});

            var options = {
                showOn: "both",
                //buttonImage: "",
                //buttonImageOnly: false,
                changeMonth: true,
                changeYear: true,
                buttonText: '<i class="fa fa-calendar"></i>',
                prevText: '<i class="fa fa-chevron-left cursor-pointer-hover"></i>',
                nextText: '<i class="fa fa-chevron-right cursor-pointer-hover"></i>',
                onSelect: function (selectedDate) {
                    //controller.$setViewValue(selectedDate);
                    //controller.$render();
                    //scope.$apply();

                    var valOrigin = $(this).val().toString();

                    var val = $(this).val().toString();
                    val = DateReformat(val);
                    val = DateFormat(val)
                    $(this).val(val);

                    ngModel.$setViewValue(DateReformat(valOrigin));

                    //console.log($(this).val());
                    //console.log(DateReformat(valOrigin))

                    angular.forEach(onSelectCallbacks, function (callback) {
                        callback.call(this, selectedDate);
                    })
                },
                autoclose: true,
                minDate: new Date(2000, 12 - 1, 1),
            };//, scope.options || {});

            if (tAttributes.minCurrentDate) options.minDate = 0;

            if (tAttributes.maxCurrentDate) options.maxDate = 0;

            if (tAttributes.numberOfMonths) options.numberOfMonths = parseInt(tAttributes.numberOfMonths);

            if (tAttributes.dateFormat) options.dateFormat = tAttributes.dateFormat;

            if (tAttributes.defaultDate) options.defaultDate = tAttributes.defaultDate;

            if (tAttributes.changeMonth) options.changeMonth = tAttributes.changeMonth == "true";

            if (tAttributes.changeYear) options.changeYear = tAttributes.changeYear == "true";

            tElement.datepicker(options)

            var onEdit = false;
            tElement.on('keydown', function (evt, id) {
                onEdit = true;
            });

            scope.$watch(tAttributes.ngModel, function (newVal, oldVal) {
                $timeout(function () {
                    tElement.datepicker("option", "disabled", newVal);
                    //tElement.dpSetDisabled(newVal);
                });

                var val = DateReformat(tElement.val());

                val = DateFormat(val);
                tElement.val(val);

                ngModel.$setViewValue(DateReformat(tElement.val()));

            }, true);

            var elem = tElement;
            var validateDate = function (e) {
                var v = elem.val();
                if (v != "") {
                    var dateReg = /^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](20)\d\d$/;
                    if (v.match(dateReg) === null) {
                        //elem.focus();
                        alert("Format tanggal yang diinput tidak benar. Mohon tanggal diisi dengan benar!");
                        e.preventDefault();
                        return;
                        //$.SmartMessageBox({
                        //    title: "Input Tangal",
                        //    content: "Format tanggal yang diinput tidak benar. Mohon tanggal diisi dengan benar!",
                        //    buttons: '[OK]',
                        //    theme: 'bg-danger'
                        //},
                        //    function (action) {
                        //        if (action === "OK") {
                        //            //elem.val("");
                        //            ////ngModel.assign(scope, "");
                        //            //ngModel.$setViewValue("");
                        //            //ngModel.$render();
                        //            //scope.$apply();

                        //            //e.preventDefault()
                        //            //return;
                        //        }
                        //    });
                    }
                    else {
                        var valOrigin = $(this).val().toString();
                        var val = $(this).val().toString();
                        val = DateReformat(val);
                        val = DateFormat(val, 2, ',', '.');
                        $(this).val(val);

                        ngModel.$setViewValue(DateReformat(valOrigin));
                    }
                }
            };

            if (elem.hasClass('hasDatepicker')) {
                setTimeout(function () {
                    elem.unbind('blur');
                    elem.bind('blur', validateDate);
                }, 1);
            } else {
                setTimeout(function () {
                    elem.unbind('blur');
                    elem.bind('blur', validateDate);
                }, 1)
            }
        }
    }
});