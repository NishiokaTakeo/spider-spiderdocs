"use strict";

app.controller('formDocDetailsCtrl', ["$scope", "$uibModal", "$uibModalInstance", "comService", "title", "historyVersions", "historyEvents", "properties", function ($scope, $uibModal, $uibModalInstance, comService, title, historyVersions, historyEvents, properties) {
  var $ctrl, init, setupProperty;
  $ctrl = this; // $scope.doc = {}

  $scope.versions = historyVersions;
  $scope.events = historyEvents;
  $scope.property = properties;
  $scope.title = title;
  $scope.canGo = true;
  $ctrl.validation = {};
  angular.extend({}, properties, $scope.property);
  $scope.tab = 1;

  init = function init() {
    setupProperty();
  };

  $scope.ok = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $scope.cancel = function () {};

  $scope.validateTogo = function () {
    return $scope.canGo;
  };

  setupProperty = function setupProperty() {
    var size; // $scope.property.eventText = $scope.property.event_name + ' ' + $scope.property.comments

    size = $scope.property.size / 1024;
    $scope.property.sizeText = size < 1 ? '< 1 KB' : size + ' KB';
    $scope.property.dateText = properties.date.startsWith('000') ? 'There is no history' : moment(properties.date).format('l LT');
    $scope.property.reviewDeadLineText = properties.reviewDeadLine.startsWith('000') ? 'There is no history' : moment(properties.reviewDeadLine).format('l LT');
    $scope.property.reviewersText = $scope.property.reviewers == null || $scope.property.reviewers === '' ? 'There is no history' : $scope.property.reviewers;
  };

  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-doc-details-controller.js.map
