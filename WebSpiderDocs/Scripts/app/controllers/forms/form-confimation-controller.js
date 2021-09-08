"use strict";

app.controller('formConfimationCtrl', ['$uibModalInstance', 'title', 'message', 'options', function ($uibModalInstance, title, message, options) {
  var $ctrl, init;
  $ctrl = this;

  init = function init() {};

  $ctrl.title = title;
  $ctrl.message = message;
  $ctrl.options = options;

  $ctrl.ok = function () {
    $uibModalInstance.close('ok');
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  return init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-confimation-controller.js.map
