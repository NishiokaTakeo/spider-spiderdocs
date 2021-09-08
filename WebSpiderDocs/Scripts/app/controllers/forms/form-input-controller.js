"use strict";

app.controller('formInputCtrl', ['$uibModalInstance', 'title', 'message', 'defInput', 'reasonMinLen', 'needsInput', 'inputType', function ($uibModalInstance, title, message, defInput, reasonMinLen, needsInput, inputType) {
  var $ctrl, init;
  $ctrl = this;

  init = function init() {};

  $ctrl.title = title;
  $ctrl.message = message;
  $ctrl.reasonMinumLen = reasonMinLen;
  $ctrl.canGo = false;
  $ctrl.input = defInput;
  $ctrl.needsInput = needsInput;
  $ctrl.inputType = inputType != null ? inputType : 'text';
  init();

  $ctrl.ok = function () {
    $uibModalInstance.close($ctrl.input);
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $ctrl.checklength = function () {
    var ok;
    ok = false;

    if ($ctrl.input != null) {
      ok = $ctrl.input.length >= $ctrl.reasonMinumLen;
    }

    return ok;
  };

  $ctrl.validateTogo = function () {
    $ctrl.canGo = false;

    if ($ctrl.needsInput) {
      if ($ctrl.checklength()) {
        $ctrl.canGo = true;
      }
    } else {
      $ctrl.canGo = true;
    }

    return $ctrl.canGo;
  };
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-input-controller.js.map
