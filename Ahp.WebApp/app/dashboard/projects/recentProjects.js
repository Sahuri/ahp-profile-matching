"use strict";

angular.module('app').directive('recentProjects', function(){
    return {
        restrict: "EA",
        replace: true,
        templateUrl: "app/dashboard/projects/recent-projects.tpl.html",
        scope: true,
        link: function(scope, element){
            scope.projectDesc = "AHP & Profile Matching";
        }
    }
});