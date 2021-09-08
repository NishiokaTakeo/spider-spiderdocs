"use strict";

app.controller('formSaveGroupCtrl', ['$scope', '$uibModalInstance', 'spiderdocsService', 'comService', 'group', function ($scope, $uibModalInstance, spiderdocsService, comService, group) {
  var init, validateTogo;

  init = function init() {
    $scope.title = group == null ? '[New Group]' : '[Edit Group - ' + group.name + ']';
    $scope.model = {
      id: 0,
      group_name: '',
      obs: '',
      ordination: 1
    };

    if (group != null) {
      angular.extend($scope.model, group);
    }
  };

  validateTogo = function validateTogo() {
    var control, i, len, ref;
    $scope.canGo = false;
    ref = $scope.myform.$$controls;

    for (i = 0, len = ref.length; i < len; i++) {
      control = ref[i];
      control.$setDirty();
      control.$validate();
    }

    if ($scope.myform.$invalid === true) {
      Notify('Can not proceed. <br/>Please Fix error(s)', null, null, 'danger');
      return;
    }

    return $scope.canGo = true;
  };

  $scope.ok = function () {
    var data, deepCopiedGroup;

    if (!$scope.validateTogo()) {
      return;
    }

    deepCopiedGroup = angular.extend({}, group);
    angular.extend(deepCopiedGroup, $scope.model);
    data = {
      group: deepCopiedGroup
    };
    $uibModalInstance.close(data);
  };

  $scope.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $scope.validateTogo = validateTogo;
  $scope.model = {};
  $scope.data = {};
  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-save-group-controller.js.map
