; (function () {
  'use strict';

  angular.module('wedding.faqAdmin')
    .factory('faqAdminApi', faqAdminApiFactory);

  faqAdminApiFactory.$inject = ['$http', '$q'];

  function faqAdminApiFactory($http, $q) {

    var api = {
      getPendingQuestions: getPendingQuestions,
      updateQuestion: updateQuestion,
      answerQuestion: answerQuestion,
      getFaq: getFaq,
      updateFaq: updateFaq,
      deleteFaq: deleteFaq
    };

    return api;

    function getPendingQuestions() {
      var deferred = $q.defer();

      $http.get('/api/FaqApi/GetUnAnswered')
        .success(function _getPendingSuccess(data) {
          deferred.resolve(data);
        })
        .error(function _getPendingFail(reason) {
          deferred.reject(reason);
        });

      return deferred.promise;
    }

    function updateQuestion(question) {
      var deferred = $q.defer();

      $http.put('/api/FaqApi/UpdateUnAnswered', { id: question.id, question: question })
        .success(function _updateQuestionSuccess(data) {
          deferred.resolve(data);
        })
        .error(function _updateQuestionError(reason) {
          deferred.reject(reason);
        });

      return deferred.promise;
    }

    function answerQuestion(answeredQuestion, question) {
      var deferred = $q.defer();

      $http.post('/api/FaqApi/AnswerQuestion', { faq: answeredQuestion, askedQuestion: question })
        .success(function _answerQuestionSuccess(data) {
          deferred.resolve(data);
        })
        .error(function _answerQuestionError(reason) {
          deferred.reject(reason);
        });

      return deferred.promise;
    }

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

    function updateFaq(faq) {
      var deferred = $q.defer();

      $http.put('/api/Faqapi/' + faq.faqId, faq)
        .success(function _updateFaqSuccess(data) {
          deferred.resolve(data);
        })
        .error(function _updateFaqError(reason) {
          deferred.reject(reason);
        });

      return deferred.promise;
    }

    function deleteFaq(faq) {
      var deferred = $q.defer();

      $http.delete('/api/Faqapi', { id: faq.faqId })
        .success(function _deleteFaqSuccess(data) {
          deferred.resolve(data);
        })
        .error(function _deleteFaqError(reason) {
          deferred.reject(reason);
        });

      return deferred.promise;
    }
  }
}());