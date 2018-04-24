/// <reference path="../../Scripts/angular.js" />

; (function () {
  'use strict';

  angular.module('wedding.faq')
    .controller('FaqController', FaqController);

  FaqController.$inject = ['faqApi'];

  function FaqController(faqApi) {
    var vm = this;

    vm.faq = null;
    vm.emailMaxLength = 50;
    vm.questionMaxLength = 2000;
    vm.email;
    vm.question;

    vm.askQuestion = askQuestion;

    function _init() {
      faqApi.getFaq()
        .then(function (faq) {
          vm.faq = faq;
        });
    }

    _init();

    function askQuestion() {
      vm.errors = [];

      if (!vm.email || (vm.email + '').match(/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/) <= 0) {
        vm.errors.push('Please provide a valid email.');
      }

      if(!vm.question){
        vm.errors.push('Please ask a qesution.');
      }

      if (!vm.errors.length) {
        faqApi.askQuestion({ replyEmail: vm.email, question: vm.question });
      }
    }
  }
}());