/// <reference path="../../Scripts/angular.js" />

; (function () {
  'use strict';

  angular.module('wedding.photo')
    .controller('PhotoController', PhotoController);

  PhotoController.$inject = ['photoApi', '$scope', '$modal'];

  function PhotoController(photoApi, $scope, $modal, loadedPhotos, parent) {
    var vm = this;

    vm.photos = null;
    vm.modalInstance = null;
    vm.open = open;
    //vm.ok = ok;

    function _init() {
      if (loadedPhotos) {
        vm.photos = loadedPhotos;
      }
      else {
        photoApi.getPhotoPackages('engagement-pictures')
          .then(function (packages) {
            vm.photos = packages;

            var i = 0;

            for (i = 0; i < vm.photos.length; i++) {
              var photoPackage = vm.photos[i];

              (function photoInit(photo) {
                photo.thumbImage = new Image();
                photo.thumbImage.onload = function photoLoaded() {
                  $scope.$applyAsync(function photoApply() {
                    photo.thumbImageSrc = photo.thumbImage.src;
                  });
                };
                photo.thumbImage.src = '/api/Photos/content/' + photo.libraryName + '/' + photo.thumbNail;
                photo.thumbImageSrc = '/App/Photo/photo.placeholder.gif';

                photo.image = new Image();
                photo.image.onload = function photoLoaded() {
                  $scope.$applyAsync(function photoApply() {
                    photo.imageSrc = photo.image.src;
                  });
                };
                photo.image.src = '/api/Photos/content/' + photo.libraryName + '/' + photo.extraLarge;
                photo.imageSrc = '/App/Photo/photo.placeholder.gif';


              }(photoPackage));
            }
          });
      }
    }

    function open(index) {
      vm.photos[index].active = true;

      vm.modalInstance = $modal.open({
        animation: true,
        templateUrl: '/App/Photo/photo.modalTemplate.html',
        controller: 'PhotoController',
        controllerAs: 'photoViewerCtrl',
        resolve: {
          loadedPhotos: function resolveLoadedPhotos() {
            return vm.photos;
          }
        }
      });
    }

    //function ok() {
    //  parent.modalInstance.close();
    //};

    _init();
  }
}());