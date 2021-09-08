"use strict";

app.controller('formImportCtrl', ["$uibModalInstance", "doc", "reasonNewVersion", function ($uibModalInstance, doc, reasonNewVersion) {
  var $ctrl, bindDragEvents, init;
  $ctrl = this;

  init = function init() {
    bindDragEvents();
  };

  bindDragEvents = function bindDragEvents() {
    angular.element('body').on('dragover', function (e) {
      e.preventDefault();
      e.stopPropagation();
    });
    angular.element('body').on('dragenter', function (e) {
      e.preventDefault();
      e.stopPropagation();
    });
  }; // setTimeout(function(){
  // 	angular.element('#upload1').on('drop', drop);
  // }, 1000)


  $ctrl.doc = {};
  angular.extend($ctrl.doc, doc);
  $ctrl.needReason = reasonNewVersion;
  $ctrl.file = void 0;
  $ctrl.canGo = false;
  $ctrl.reason = '';
  init();

  $ctrl.ok = function () {
    $uibModalInstance.close([$ctrl.file, $ctrl.reason]);
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $ctrl.deletePic = function () {
    $ctrl.file = void 0;
    $ctrl.validateTogo();
  };

  $ctrl.onDrop = function (e) {
    var files;
    e.preventDefault();
    e.stopPropagation();
    $ctrl.error = '';
    $ctrl.file = void 0;
    files = e.originalEvent.dataTransfer.files;

    if (files.length === 1 && files[0].name.split('.').pop().toLowerCase() === doc.extension.replace('.', '').toLowerCase()) {
      $ctrl.file = files[0];
      $ctrl.validateTogo();
    } else {
      //$ctrl.fileupload = "Click to delete";
      //$uibModalInstance.close(e);
      $ctrl.error = 'File must be same extension (' + $ctrl.doc.extension + ') and drop only one file.';
    }
  };

  $ctrl.checklength = function () {
    return $ctrl.reason.length >= 10;
  };

  $ctrl.validateTogo = function () {
    $ctrl.canGo = false;

    if ($ctrl.needReason) {
      if ($ctrl.file) {
        if ($ctrl.checklength()) {
          $ctrl.canGo = true;
        }
      }
    } else {
      if ($ctrl.file) {
        $ctrl.canGo = true;
      }
    }

    return $ctrl.canGo;
  };
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-import-controller.js.map
