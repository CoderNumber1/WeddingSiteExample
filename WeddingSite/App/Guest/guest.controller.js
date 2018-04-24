; (function () {
    'use strict';

    angular.module('wedding.guest')
      .controller('GuestController', GuestController);

    GuestController.$inject = ['guestApi', 'states'];

    function GuestController(guestApi, states) {
        var vm = this;

        vm.guests = [];
        vm.newGuest = {};
        vm.states = states;
        vm.rsvpOnly = false;

        vm.addGuest = addGuest;
        vm.updateGuest = updateGuest;
        vm.deleteGuest = deleteGuest;
        vm.genrateGuestCode = genrateGuestCode;
        vm.generateAllGuestCodes = generateAllGuestCodes;
        vm.filterRsvps = filterRsvps;

        function _init() {
            _loadGuests();
        }

        _init();

        function _loadGuests() {
            guestApi.getGuests()
              .then(function _loadGuestsSuccess(data) {
                  vm.guests = data;
              });
        }

        function addGuest() {
            guestApi.addGuest(vm.newGuest)
              .then(function _addGuestSuccess(data) {
                  _loadGuests();
              });
        }

        function updateGuest(guest) {
            guestApi.updateGuest(guest)
              .then(function _addGuestSuccess(data) {
                  _loadGuests();
              });
        }

        function deleteGuest(guest) {
            guestApi.deleteGuest(guest)
              .then(function _deleteGuestSuccess(data) {
                  _loadGuests();
              });
        }

        function genrateGuestCode(guest) {
            guestApi.generateGuestCode(guest)
            .then(function _generateGuestCodeSuccess(data) {
                _loadGuests();
            });
        }

        function generateAllGuestCodes() {
            guestApi.generateAllGuestCodes()
                .then(function _generateAllGuestCodesSuccess(data) {
                    _loadGuests();
                });
        }

        function filterRsvps(guest) {
            return !vm.rsvpOnly || guest.rsvpFlag;
        }
    }
}());