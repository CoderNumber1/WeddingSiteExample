/// <reference path="../../Scripts/angular.js" />

; (function () {
  'use strict';

  angular.module('wedding.photo')
    .factory('photoApi', photoApiFactory);

  photoApiFactory.$inject = ['$http', '$q'];

  function photoApiFactory($http, $q) {
    var faqApi = {
      getPhotoPackages: getPhotoPackages
    };

    function getPhotoPackages(library) {
      var deferred = $q.defer();

      $http.get('/api/photos/package/' + library)
        .success(function (data) {
          deferred.resolve(data);
        })
        .error(function (reason) {
          deferred.reject(reason);
        });

      return deferred.promise;
    }

    return faqApi;
  }
}());