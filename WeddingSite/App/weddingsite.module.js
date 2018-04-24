/// <reference path="../Scripts/angular.js" />

(function () {
    'use strict';

    angular.module('WeddingSite', ['ui.router', 'ui.bootstrap', 'carousel'])

    .controller('HomeController', function () {
        var vm = this;

        vm.message = 'Hello World Wedding Site';
    });
}());