'use strict';

angular.module('app.graphs').directive('chartjsPieChart', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attributes) {
            var pieOptions = {
                //Boolean - Whether we should show a stroke on each segment
                segmentShowStroke: true,
                //String - The colour of each segment stroke
                segmentStrokeColor: "#fff",
                //Number - The width of each segment stroke
                segmentStrokeWidth: 2,
                //Number - Amount of animation steps
                animationSteps: 100,
                //String - types of animation
                animationEasing: "easeOutBounce",
                //Boolean - Whether we animate the rotation of the Doughnut
                animateRotate: true,
                //Boolean - Whether we animate scaling the Doughnut from the centre
                animateScale: true,
                //Boolean - Re-draw chart on page resize
                responsive: true,
                //String - A legend template
                legendTemplate : "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>"
            };

            //pieOptions.legend = {
            //    display: true,
            //    position: 'top',
            //    fullWidth: true,
            //    reverse: true
            //};

            var pieData = [
                {
                    value: 14.94,
                    color:"rgba(119,167,251,0.9)",
                    highlight: "rgba(119,167,251,0.8)",
                    label: "WKP Lahendong"
                },
                {
                    value: 11.49,
                    color: "rgba(299,115,104,1)",
                    highlight: "rgba(299,115,104,0.8)",
                    label: "WKP Lumut Balai"
                },
                {
                    value: 20.69,
                    color: "rgba(251, 203, 67, 0.7)",
                    highlight: "rgba(251, 203, 67, 0.7)",
                    label: "WKP Hululais"
                }
                ,
                {
                    value: 17.24,
                    color: "rgba(52,182,122,1)",
                    highlight: "rgba(52,182,122,0.8)",
                    label: "WKP Ulubelu"
                },
                {
                    value: 35.64,
                    color: "rgba(241, 171, 104, 0.7)",
                    highlight: "rgba(241, 171, 104, 0.7)",
                    label: "WKP Kamojang"
                }
            ];

            // render chart
            var ctx = element[0].getContext("2d");
            var myNewChart = new Chart(ctx).Pie(pieData, pieOptions);
        }}
});