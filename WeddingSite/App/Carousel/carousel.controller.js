/// <reference path="../../Scripts/angular.js" />

(function () {
    'use strict';

    angular.module('carousel')
        .controller('ImageSliderController', CarouselController)

    function CarouselController() {
        var vm = this;

        vm.interval = 5000;
        vm.images = [];

        function _init() {
            var imageNumbers = ['05', '06', '11', '15', '18', '19'];
            var i;

            for (i = 0; i < imageNumbers.length; i++) {
                vm.images.push({
                    image: '/Images/img' + imageNumbers[i] + '/img' + imageNumbers[i] + '_XL.png'
                });
            }
        }

        _init();
    }
}());