/// <reference path="../../Scripts/angular.js" />

; (function () {
  'use strict';

  angular.module('wedding.faq')
    .factory('faqApi', faqApiFactory);

  faqApiFactory.$inject = ['$http', '$q'];

  function faqApiFactory($http, $q) {
    var faqApi = {
      getFaq: getFaq,
      askQuestion: askQuestion
    };

    function getFaq() {
      var deferred = $q.defer();

      $http.get('/api/Faqapi')
        .success(function (data) {
          deferred.resolve(data);
        })
        .error(function (reason) {
          deferred.reject(reason);
        });

      return deferred.promise;
    }

    function askQuestion(question) {
      var deferred = $q.defer();

      $http.post('/api/Faqapi', question)
        .success(function (data) {
          deferred.resolve(data);
        })
        .error(function (reason) {
          deferred.reject(reason)
        });

      return deferred.promise;
    }

    return faqApi;
  }
}());