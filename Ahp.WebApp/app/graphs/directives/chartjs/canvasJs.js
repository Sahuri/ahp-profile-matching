'use strict';

angular.module('app.graphs').directive('canvasJs', function () {
    return {
        restrict: 'EAC',
        scope: {
            InitConfig: '=initConfig'
        },
        link: function (scope, element, attributes) {
            element.removeAttr('canvasJs');
            element.removeAttr('canvas-js');

            var chartContainer = element.attr('id');
            var chart = new CanvasJS.Chart(chartContainer, scope.InitConfig);
            chart.render();

            return chart;
        }
    }
});