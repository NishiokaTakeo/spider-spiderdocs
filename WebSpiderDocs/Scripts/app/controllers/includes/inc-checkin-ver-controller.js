"use strict";

app.controller('incCheckinVerCtrl', ["$scope", "$uibModal", "spiderdocsService", "comService", "en_Actions", "modalService", function ($scope, $uibModal, spiderdocsService, comService, en_Actions, modalService) {
  var $ctrl, checkDocSelected, checkToOpenSearch, checkfolder, deepcopy, init, nameByUnselected, selectedDocs;
  $ctrl = this;
  $ctrl.docs = []; // $ctrl.title = $scope.title

  $scope.title = 'Save as a New Version of an existing document';
  $ctrl.docTypes = $scope.docTypes;
  $ctrl.selectedAttr = {};
  $ctrl.canGo = false;
  $ctrl.validation = {};
  $ctrl.needReason = true;
  $ctrl.reasonMinumLen = 10;
  $ctrl.reason = '';
  $ctrl.options = {
    saveAsPDF: false
  };
  $ctrl.canOCRable = false;

  init = function init() {
    var doc, i, len1, ocred, ref; //selected = (for doc in $ctrl.docs when doc.selected is true)

    ocred = function () {
      var i, len1, ref, results;
      ref = $ctrl.docs;
      results = [];

      for (i = 0, len1 = ref.length; i < len1; i++) {
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

    ref = $ctrl.docs; // select all as default 

    for (i = 0, len1 = ref.length; i < len1; i++) {
      doc = ref[i];
      doc.selected = true;
    }
  };

  checkToOpenSearch = function checkToOpenSearch() {
    var len;
    len = selectedDocs().length;
    return len === 1;
  };

  $ctrl.openSearchDoc = function () {
    var doc;

    if (checkToOpenSearch() === false) {
      return modalService.confirm("Error", "Please select only one doc.", {
        hideCancel: true
      });
    } else {
      doc = selectedDocs()[0];
      return spiderdocsService.cache.foldersL1(0, en_Actions.CheckIn_Out).then(function (folders) {
        return modalService.searchDocument(doc, folders, $ctrl.docTypes, en_Actions.CheckIn_Out, {
          saveAsPDF: $ctrl.options.saveAsPDF
        });
      }).then(function (docs) {
        var searched;
        searched = docs[0];
        $ctrl.bindSearchedDoc(searched);
        return $ctrl.validateTogo();
      });
    }
  };

  selectedDocs = function selectedDocs() {
    var doc, i, len1, ref, results;
    ref = $ctrl.docs;
    results = [];

    for (i = 0, len1 = ref.length; i < len1; i++) {
      doc = ref[i];

      if (doc.selected === true) {
        results.push(doc);
      }
    }

    return results;
  };

  $ctrl.bindSearchedDoc = function (searched) {
    var doc;
    doc = selectedDocs()[0];
    doc.verDoc = searched;
    return doc;
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
    var doc, i, len1, lenSelected, ref, unselected, ver, verDocs;

    lenSelected = function () {
      var i, len1, ref, results;
      ref = $ctrl.docs;
      results = [];

      for (i = 0, len1 = ref.length; i < len1; i++) {
        doc = ref[i];

        if (doc.selected === true) {
          results.push(doc);
        }
      }

      return results;
    }().length;

    ref = $ctrl.docs;

    for (i = 0, len1 = ref.length; i < len1; i++) {
      ver = ref[i];

      if ((ver != null ? ver.selected : void 0) !== true) {
        delete ver.verDoc;
      }
    }

    verDocs = function () {
      var j, len2, ref1, results;
      ref1 = $ctrl.docs;
      results = [];

      for (j = 0, len2 = ref1.length; j < len2; j++) {
        ver = ref1[j];

        if (!(ver != null ? ver.verDoc : void 0)) {
          continue;
        }

        ver.verDoc.id_user_workspace = ver.id_user_workspace;
        ver.verDoc._ref = ver._ref;
        results.push(ver.verDoc);
      }

      return results;
    }();

    if (lenSelected === verDocs.length) {
      $scope.$emit('dialog-savenew-ok', {
        docs: verDocs,
        func: 'ver',
        reason: $ctrl.reason,
        options: $ctrl.options
      });
    } else {
      unselected = nameByUnselected($ctrl.docs);
      Notify("You must search and specify for each documents you selected. (".concat(unselected, ") "), null, null, 'info');
    }
  };

  $ctrl.cancel = function () {
    // $uibModalInstance.dismiss 'cancel'
    $scope.$emit('dialog-savenew-close');
  };

  $ctrl.resetverDoc = function () {
    var backup, doc, i, len1, ref;
    ref = $ctrl.docs;

    for (i = 0, len1 = ref.length; i < len1; i++) {
      doc = ref[i];

      if (doc.extension !== doc.verDoc.extension) {
        delete doc.verDoc;
      }
    }

    $ctrl.validateTogo();
    return;
    backup = $ctrl.options.saveAsPDF; // if $ctrl.options.saveAsPDF then '.pdf' else $ctrl.docs[0].extension

    modalService.confirm("Confirmation", "This perform reset selected document.\nWould you like to proceed?").then(function (result) {
      var j, k, len2, len3, ref1, ref2, results;

      if (result === "ok") {
        ref1 = $ctrl.docs;
        results = [];

        for (j = 0, len2 = ref1.length; j < len2; j++) {
          doc = ref1[j];
          results.push(delete doc.verDoc);
        }

        return results;
      } else {
        $ctrl.options.saveAsPDF = backup;
        ref2 = $ctrl.docs;

        for (k = 0, len3 = ref2.length; k < len3; k++) {
          doc = ref2[k];

          if (doc.extension !== doc.verDoc.extension) {
            delete doc.verDoc;
          }
        }

        return $ctrl.validateTogo();
      }
    });
  };

  $ctrl.checklength = function () {
    return $ctrl.reason.length >= $ctrl.reasonMinumLen;
  };

  $ctrl.validateTogo = function () {
    $ctrl.canGo = false;

    if (checkDocSelected()) {
      if ($ctrl.needReason) {
        if ($ctrl.checklength()) {
          $ctrl.canGo = true;
        }
      } else {
        $ctrl.canGo = true;
      }
    }

    return $ctrl.canGo;
  };

  checkDocSelected = function checkDocSelected() {
    var doc, i, len1, ok, ref;
    ok = false;
    ref = $ctrl.docs;

    for (i = 0, len1 = ref.length; i < len1; i++) {
      doc = ref[i];

      if (doc.verDoc !== void 0) {
        ok = true;
      }
    }

    return ok;
  };

  deepcopy = function deepcopy(source, dest) {
    var _doc, doc, i, len1;

    for (i = 0, len1 = source.length; i < len1; i++) {
      doc = source[i];
      _doc = {};
      angular.copy(doc, _doc);
      _doc._ref = doc._ref;
      dest.push(_doc);
    }
  };

  nameByUnselected = function nameByUnselected(docs) {
    var i, len1, unselected, ver;
    unselected = '';

    for (i = 0, len1 = docs.length; i < len1; i++) {
      ver = docs[i];

      if ((ver != null ? ver.verDoc : void 0) === null) {
        unselected += ver.title + ", ";
      }
    }

    unselected = unselected.slice(0, -2);
    return unselected;
  };

  deepcopy($scope.docs, $ctrl.docs);
  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/includes/inc-checkin-ver-controller.js.map
