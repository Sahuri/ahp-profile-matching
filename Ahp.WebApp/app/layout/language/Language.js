"use strict";

angular.module('app').factory('Language', function ($rootScope, $http, APP_CONFIG) {

	function getLanguage(key, callback) {

		$http.get(APP_CONFIG.apiRootUrl + '/langs/' + key + '.json').success(function(data){

			callback(data);
			
		}).error(function(){

			$log.log('Error');
			callback([]);

		});

	}

	function getLanguages(callback) {

		$http.get(APP_CONFIG.apiRootUrl + '/languages.json').success(function(data){

			callback(data);
			
		}).error(function(){

			$log.log('Error');
			callback([]);

		});

	}

	function getLanguagePath() {
	    var key = $rootScope.currentLanguage == undefined ? 'id' : $rootScope.currentLanguage.key;
	    var path = APP_CONFIG.apiRootUrl + '/langs/' + key + '.json';

	    return path;
	}

	return {
		getLang: function(type, callback) {
			getLanguage(type, callback);
		},
		getLanguages:function(callback){
			getLanguages(callback);
		},
		getLanguagePath: function (key) {
		    return getLanguagePath(key);
		}
	}

});