"use strict";

app.controller('formSaveNewCtrl', ["$scope", "$uibModalInstance", "$uibModal", "docs", "title", "spiderdocsService", "comService", "docTypes", "en_Actions", "settings", "notificationgroups", function ($scope, $uibModalInstance, $uibModal, docs, title, spiderdocsService, comService, docTypes, en_Actions, settings, notificationgroups) {
  // $scope.$ctrl = {}
  // $scope = this
  $scope.tab = 1;
  $scope.title = title;
  $scope.docTypes = docTypes;
  $scope.canGo = false;
  $scope.docs = docs;
  $scope.settings = settings;
  $scope.notificationgroups = notificationgroups;
  $scope.$on('dialog-savenew-close', function (event, data) {
    $uibModalInstance.dismiss('cancel');
  });
  $scope.$on('dialog-savenew-ok', function (event, data) {
    $uibModalInstance.close(data);
  });
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-save-new-controller.js.map
