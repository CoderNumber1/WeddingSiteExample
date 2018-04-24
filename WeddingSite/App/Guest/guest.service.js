; (function () {
    'use strict';

    angular.module('wedding.guest')
      .factory('guestApi', guestApiFactory);

    guestApiFactory.$inject = ['$http', '$q'];

    function guestApiFactory($http, $q) {

        var api = {
            getGuests: getGuests,
            updateGuest: updateGuest,
            addGuest: addGuest,
            deleteGuest: deleteGuest,
            generateGuestCode: generateGuestCode,
            generateAllGuestCodes: generateAllGuestCodes
        };

        return api;

        function getGuests() {
            var deferred = $q.defer();

            $http.get('/api/GuestApi')
              .success(function _getGuestsSuccess(data) {
                  deferred.resolve(data);
              })
              .error(function _getGuestsSuccess(reason) {
                  deferred.reject(reason);
              });

            return deferred.promise;
        }

        function updateGuest(guest) {
            var deferred = $q.defer();

            $http.put('/api/GuestApi/Update/' + guest.guestId, guest)
              .success(function _updateGuestSuccess(data) {
                  deferred.resolve(data);
              })
              .error(function _updateGuestError(reason) {
                  deferred.reject(reason);
              });

            return deferred.promise;
        }

        function addGuest(guest) {
            var deferred = $q.defer();

            $http.post('/api/GuestApi', guest)
              .success(function _addGuestSuccess(data) {
                  deferred.resolve(data);
              })
              .error(function _addGuestError(reason) {
                  deferred.reject(reason);
              });

            return deferred.promise;
        }

        function deleteGuest(guest) {
            var deferred = $q.defer();

            $http.delete('/api/GuestApi/' + guest.guestId)
              .success(function _deleteGuestSuccess(data) {
                  deferred.resolve(data);
              })
              .error(function _deleteGuestError(reason) {
                  deferred.reject(reason);
              });

            return deferred.promise;
        }

        function generateGuestCode(guest, useCount) {
            var deferred = $q.defer();

            $http.put('/api/GuestApi/GenerateCode', {
                guestId: guest != null ? guest.guestId : null,
                useCount: useCount
            })
                .success(function _generateGuestCodeSuccess(data) {
                    deferred.resolve(data);
                })
            .error(function _generateGuestCodeError(reason) {
                alert(reason);
                deferred.reject(reason);
            });

            return deferred.promise;
        }

        function generateAllGuestCodes() {
            var deferred = $q.defer();

            $http.put('/api/GuestApi/GenerateAllCodes')
                .success(function _generateGuestCodeSuccess(data) {
                    deferred.resolve(data);
                })
            .error(function _generateGuestCodeError(reason) {
                alert(reason);
                deferred.reject(reason);
            });

            return deferred.promise;
        }
    }
}());