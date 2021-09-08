"use strict";

app.controller('formReasonCtrl', ["$uibModalInstance", "title", "message", "reasonRequired", "reasonMinLen", function ($uibModalInstance, title, message, reasonRequired, reasonMinLen) {
  var $ctrl, init;
  $ctrl = this;

  init = function init() {};

  $ctrl.title = title;
  $ctrl.message = message;
  $ctrl.needReason = reasonRequired;
  $ctrl.reasonMinumLen = reasonMinLen;
  $ctrl.canGo = false;
  $ctrl.reason = '';
  init();

  $ctrl.ok = function () {
    $uibModalInstance.close($ctrl.reason);
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $ctrl.checklength = function () {
    return $ctrl.reason.length >= $ctrl.reasonMinumLen;
  };

  $ctrl.validateTogo = function () {
    $ctrl.canGo = false;

    if ($ctrl.needReason) {
      if ($ctrl.checklength()) {
        $ctrl.canGo = true;
      }
    } else {
      $ctrl.canGo = true;
    }

    return $ctrl.canGo;
  };
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-reason-controller.js.map
