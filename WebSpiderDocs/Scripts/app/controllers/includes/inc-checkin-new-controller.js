"use strict";

app.controller('incCheckinNewCtrl', ["$scope", "$uibModal", "spiderdocsService", "comService", "en_Actions", "modalService", function ($scope, $uibModal, spiderdocsService, comService, en_Actions, modalService) {
  var $ctrl, bindAttr, bindNotificationGrp, bindTitle, bindType, checkfolder, deepcopy, init, openFolderTree;
  $ctrl = this;
  $ctrl.docs = []; // $ctrl.title = $scope.title

  $scope.title = 'Save as a New File';
  $ctrl.docTypes = $scope.docTypes;
  $ctrl.selectedAttr = {};
  $ctrl.canGo = false;
  $ctrl.validation = {};
  $ctrl.options = {
    saveAsPDF: false
  };
  $ctrl.canOCRable = false;
  $ctrl.notificationgroups = $scope.notificationgroups; // angular.copy $scope.docs, $ctrl.docs

  init = function init() {
    var doc, ocred; //selected = (for doc in $ctrl.docs when doc.selected is true)

    ocred = function () {
      var i, len, ref, results;
      ref = $ctrl.docs;
      results = [];

      for (i = 0, len = ref.length; i < len; i++) {
        doc = ref[i];

        if (comService.isOCRable(doc.title)) {
          results.push(doc);
        }
      }

      return results;
    }();

    if ($scope.settings.userGlobal.ocr && $scope.settings.userGlobal.default_ocr_import === true) {
      $ctrl.canOCRable = $ctrl.docs.length === ocred.length && ($ctrl.options.saveAsPDF = true);
    }
  };

  bindTitle = function bindTitle(docs) {
    docs.forEach(function (x) {
      x.title = x.title_withoutExt + x.extension;
    });
  };

  bindType = function bindType(docs) {
    var idType;
    idType = $ctrl.type ? $ctrl.type.id : 0;
    docs.forEach(function (x) {
      x.id_docType = idType;
    });
  };

  bindAttr = function bindAttr(docs) {
    $ctrl.selectedAttr = $ctrl.selectedAttr || [];
    docs.forEach(function (x) {
      x.attrs = $ctrl.selectedAttr;
    });
  };

  bindNotificationGrp = function bindNotificationGrp(docs) {
    var items, opt, selected;
    items = $ctrl.notificationgroups || [];

    selected = function () {
      var i, len, results;
      results = [];

      for (i = 0, len = items.length; i < len; i++) {
        opt = items[i];

        if (opt.checked === true) {
          results.push(opt.id);
        }
      }

      return results;
    }();

    docs.forEach(function (x) {
      x.id_notification_group = selected;
    });
  };

  openFolderTree = function openFolderTree(folders) {
    spiderdocsService.cache.foldersL1(0, en_Actions.CheckIn_Out).then(function (folders) {
      var nodes;
      nodes = comService.nodesBy(0, folders);
      return modalService.openfolder(nodes, en_Actions.CheckIn_Out);
    }).then(function () {
      var folder = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};
      $ctrl.name_folder = folder.text;
      $ctrl.validation.folder = folder;
      angular.forEach($ctrl.docs, function (item, key) {
        item.id_folder = folder.id;
      });
      $ctrl.validateTogo();
    }, function () {});
  };

  checkfolder = function checkfolder() {
    var hasError;
    hasError = $ctrl.docs.filter(function (doc) {
      doc.id_folder <= 0;
    });
    return hasError.length === 0;
  };

  $ctrl.folderclicked = function () {
    openFolderTree([]);
  };

  $ctrl.ok = function () {
    bindTitle($ctrl.docs);
    bindType($ctrl.docs);
    bindAttr($ctrl.docs);
    bindNotificationGrp($ctrl.docs);
    $scope.$emit('dialog-savenew-ok', {
      docs: $ctrl.docs,
      func: 'new',
      options: $ctrl.options
    });
  };

  $ctrl.cancel = function () {
    // $uibModalInstance.dismiss 'cancel'
    $scope.$emit('dialog-savenew-close');
  };

  $ctrl.validateTogo = function () {
    $ctrl.canGo = false;

    if (checkfolder()) {
      $ctrl.canGo = true;
    }

    return $ctrl.canGo;
  };

  deepcopy = function deepcopy(source, dest) {
    var _doc, doc, i, len;

    for (i = 0, len = source.length; i < len; i++) {
      doc = source[i];
      _doc = {};
      angular.copy(doc, _doc);
      _doc._ref = doc._ref;
      dest.push(_doc);
    }
  };

  deepcopy($scope.docs, $ctrl.docs);
  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/includes/inc-checkin-new-controller.js.map
