'use strict'

angular.module('SmartAdmin.Forms').directive('customNumberOnly', function ($parse, $timeout, lazyScript) {
    return {
        restrict: 'EA',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            lazyScript.register('build/vendor.ui.js').then(function () {
                var onEdit = false;
                element.on('keydown', function (evt, id) {
                    onEdit = true;
                });

                element.on('keypress', function (evt, id) {
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    //console.log(charCode);

                    if (charCode != 44 && charCode > 31 && charCode != 37 && charCode != 39 && charCode != 45 //&& charCode != 46
                        && (charCode < 48 || charCode > 57))
                        return false;

                    if (charCode == 44) {
                        if (element.val().indexOf(',') != -1) {
                            return false;
                        }
                    }

                    //if (charCode == 46) {
                    //    return false;
                    //}

                    if (charCode == 45) {

                        var el = $(this).get(0);
                        var pos = 0;
                        if ('selectionStart' in el) {
                            pos = el.selectionStart;
                        } else if ('selection' in document) {
                            el.focus();
                            var Sel = document.selection.createRange();
                            var SelLength = document.selection.createRange().text.length;
                            Sel.moveStart('character', -el.value.length);
                            pos = Sel.text.length - SelLength;
                        }
                        if (element.val().indexOf('-') != -1) {
                            return false;
                        }
                        else {
                            if (pos != 0) {
                                return false;
                            }
                        }
                    }

                    return true;
                });

                element.on('focus', function () {
                    //console.log("FOCUS");
                    var val = $(this).val().toString();
                    $(this).val(val.replace(/\./g, ''))
                });

                element.on('blur', function () {
                    var valOrigin = $(this).val().toString();

                    var val = $(this).val().toString();
                    val = NumberReformat(val);

                    if (val.indexOf('.') != -1) {
                        val = NumberFormat(val, 2, ',', '.');
                    }
                    else {
                        val = NumberFormat(val, 0, ',', '.');
                    }
                    $(this).val(val);

                    ngModel.$setViewValue(NumberReformat(valOrigin));
                })

                scope.$watch(attrs.ngModel, function (newValue, oldValue) {
                    if (!onEdit) {
                        var spiltArray = String(newValue).split("");

                        if (attrs.allowNegative == "false") {
                            if (spiltArray[0] == '-') {
                                newValue = newValue.replace("-", "");
                                ngModel.$setViewValue(newValue);
                                ngModel.$render();
                            }
                        }

                        if (attrs.allowDecimal == "false") {
                            //newValue = parseInt(newValue);
                            ngModel.$setViewValue(newValue);
                            ngModel.$render();
                        }

                        if (attrs.allowDecimal != "false") {
                            if (attrs.decimalUpto) {
                                var n = String(newValue).split(",");
                                if (n[1]) {
                                    var n2 = n[1].slice(0, attrs.decimalUpto);
                                    newValue = [n[0], n2].join(",");
                                    ngModel.$setViewValue(newValue);
                                    ngModel.$render();
                                }
                            }
                        }

                        if (spiltArray.length === 0) return;
                        if (spiltArray.length === 1 && (spiltArray[0] == '-' || spiltArray[0] === ',')) return;
                        if (spiltArray.length === 2 && newValue === '-,') return;

                        var val = NumberReformat(element.val());

                        if (val.indexOf('.') != -1) {
                            val = NumberFormat(val, 2, ',', '.');
                        }
                        else {
                            val = NumberFormat(val, 0, ',', '.');
                        }
                        element.val(val);

                        ngModel.$setViewValue(NumberReformat(element.val()));
                    }
                    /*Check it is number or not.*/
                    //if (isNaN(newValue)) {
                    //    //var valOldValue = oldValue.toString();
                    //    //if (valOldValue.indexOf('.') != -1) {
                    //    //    var val = NumberFormat(oldValue, 3, ',', '.');
                    //    //    ngModel.$setViewValue(val);
                    //    //}
                    //    //else {
                    //    //    var val = NumberFormat(oldValue, 0, ',', '.');
                    //    //    ngModel.$setViewValue(val);
                    //    //}
                    //    ngModel.$setViewValue(oldValue);
                    //    ngModel.$render();
                    //  }
                });
            });
            }
        };
    });