'use strict';

angular.module('app.graphs').directive('chartJs', function () {
    var baseWidth = 600;
    var baseHeight = 400;
    return {
        template: '<canvas id="currentChart" height="146"></canvas>',
        restrict: 'E',
        scope: {
            Options: '=options',
            Data: '=chartData',
            ChartType: '=chartType'
        },
        link: function (scope, element, attributes) {
            var createChart = function () {
                var createdChart;
                var chartContext = document.getElementById("currentChart").getContext("2d");

                switch (scope.ChartType) {
                    case 'line':
                        createdChart = new Chart(chartContext).Line(scope.Data, scope.Options);
                        break;
                    case 'pie':
                        console.log('OK');
                        createdChart = new Chart(chartContext).Pie(scope.Data, scope.Options);
                        break;
                    case 'bar':
                        createdChart = new Chart(chartContext).Bar(scope.Data, scope.Options);
                        break;
                    case 'radar':
                        createdChart = new Chart(chartContext).Radar(scope.Data, scope.Options);
                        break;
                    case 'polarArea':
                        createdChart = new Chart(chartContext).PolarArea(scope.Data, scope.Options);
                        break;
                    default:
                        break;
                }
                return createdChart;
            }
            scope.currentChart = createChart();
        }
    }
});