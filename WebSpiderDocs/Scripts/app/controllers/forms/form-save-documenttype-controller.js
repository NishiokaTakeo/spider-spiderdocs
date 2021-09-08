"use strict";

app.controller('formSaveDocumentTypeCtrl', ['$scope', '$uibModalInstance', 'spiderdocsService', 'comService', 'doctype', function ($scope, $uibModalInstance, spiderdocsService, comService, doctype) {
  var init, validateTogo;

  init = function init() {
    $scope.title = doctype == null ? '[New Document Type]' : '[Edit Document Type - ' + doctype.type + ']';
    $scope.model = {
      id: 0,
      doctype: {}
    };

    if (doctype != null) {
      angular.extend($scope.model.doctype, doctype);
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
    var data, deepCopiedDoctype;

    if (!$scope.validateTogo()) {
      return;
    }

    deepCopiedDoctype = angular.extend({}, $scope.model.doctype);
    angular.extend(deepCopiedDoctype, $scope.model.doctype);
    data = {
      doctype: deepCopiedDoctype
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
//# sourceMappingURL=../../../maps/app/controllers/forms/form-save-documenttype-controller.js.map
