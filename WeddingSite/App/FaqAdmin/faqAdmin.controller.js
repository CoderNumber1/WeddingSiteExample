; (function () {
  'use strict';

  angular.module('wedding.faqAdmin')
    .controller('FaqAdminController', FaqAdminController);

  FaqAdminController.$inject = ['faqAdminApi'];

  function FaqAdminController(faqAdminApi) {
    var vm = this;

    vm.unAnsweredQuestions = [];
    vm.faq = [];
    vm.questionMaxLength = 4000;

    vm.wontAnswer = wontAnswer;
    vm.answerQuestion = answerQuestion;
    vm.updateFaq = updateFaq;
    vm.deleteFaq = deleteFaq;

    function _init() {
      _loadPendingQuestions();
      _loadFaq();
    }

    _init();

    function _loadPendingQuestions() {
      faqAdminApi.getPendingQuestions()
        .then(function _loadPendingQuestionsSuccess(data) {
          vm.unAnsweredQuestions = data;
        });
    }

    function _loadFaq() {
      faqAdminApi.getFaq()
        .then(function _loadFaqSuccess(data) {
          vm.faq = data;
        });
    }

    function wontAnswer(question) {
      question.willAnswer = false;

      faqAdminApi.updateQuestion(question)
        .then(function _wontAnswerUpdateQuestionSuccess() {
          _loadPendingQuestions();
        });
    }

    function answerQuestion(pendingQuestion) {
      var answeredQuestion = {
        question: pendingQuestion.question,
        answer: pendingQuestion.answer
      };

      faqAdminApi.answerQuestion(answeredQuestion, pendingQuestion)
        .then(function _answerQuestionSuccess() {
          _loadPendingQuestions();
          _loadFaq();
        })
    }

    function updateFaq(faq) {
      faqAdminApi.updateFaq(faq)
        .then(function _updateFaqSuccess() {
          _loadFaq();
        });
    }

    function deleteFaq(faq) {
      faqAdminApi.deleteFaq(faq)
        .then(function _deleteFaqSuccess() {
          _loadFaq();
        });
    }
  }
}());