"use strict";

// folders,
app.controller('formSearchDocumentsCtrl', ["$scope", "$uibModalInstance", "$uibModal", "spiderdocsService", "comService", "docTypes", "en_Actions", "extension", "modalService", function ($scope, $uibModalInstance, $uibModal, spiderdocsService, comService, docTypes, en_Actions, extension, modalService) {
  var $ctrl, checkfolder, init, openFolderTree;
  $ctrl = this;
  $ctrl.search = {};
  $ctrl.docs = [];
  $ctrl.title = "Search document Form";
  $ctrl.docTypes = docTypes;
  $ctrl.selectedAttr = [];
  $ctrl.canGo = false;
  $ctrl.validation = {};
  $ctrl.extension = extension;

  init = function init() {
    $ctrl.searchDoc();
  };

  $ctrl.searchDoc = function () {
    var attrs, attrvalues, criteria, docid, docname, folderid, item, type;
    criteria = {
      AttributeCriterias: {
        Attributes: []
      }
    };
    attrs = [].concat($ctrl.selectedAttr);
    attrvalues = [];
    docid = $ctrl.search.id;
    docname = $ctrl.search.title;
    folderid = $ctrl.search.id_folder;
    type = $ctrl.search.type;

    if (docid) {
      criteria.DocIds = [docid];
    }

    if (docname) {
      criteria.Titles = [docname];
    }

    if (folderid) {
      criteria.FolderIds = [folderid];
    }

    if (type) {
      criteria.DocTypeIds = [type];
    }

    if (extension) {
      criteria.Extensions = [extension];
    }

    attrs = function () {
      var i, len, results;
      results = [];

      for (i = 0, len = attrs.length; i < len; i++) {
        item = attrs[i];

        if (item.atbValue !== '') {
          results.push({
            Values: item
          });
        }
      }

      return results;
    }();

    criteria.AttributeCriterias.Attributes = attrs;

    if (!criteria) {
      return;
    }

    comService.loading(true);
    spiderdocsService.GetDocuments(criteria).then(function (docs) {
      $ctrl.docs = docs;
    }).then(function () {
      return comService.loading(false);
    });
  };

  $ctrl.selectChanged = function (doc) {
    var _doc, i, len, ref;

    ref = $ctrl.docs;

    for (i = 0, len = ref.length; i < len; i++) {
      _doc = ref[i];

      if (_doc.id !== doc.id) {
        _doc.selected = false;
      }
    }

    $ctrl.validateTogo();
  };

  openFolderTree = function openFolderTree() {
    spiderdocsService.cache.foldersL1(0, en_Actions.CheckIn_Out).then(function (_folders) {
      var nodes;
      nodes = comService.nodesBy(0, _folders);
      return modalService.openfolder(nodes, en_Actions.CheckIn_Out); // $uibModal.open(
      // 	animation: true
      // 	ariaLabelledBy: 'modal-title'
      // 	ariaDescribedBy: 'modal-body'
      // 	templateUrl: 'scripts/app/views/form-folder-tree.html'
      // 	controller: 'formFolderTreeCtrl'
      // 	controllerAs: '$ctrl'
      // 	backdrop: 'static'
      // 	size: 'lg'
      // 	resolve:
      // 		folders: ->
      // 			comService.nodesBy 0, _folders
      // 		permission: ->
      // 			en_Actions.CheckIn_Out
      // ).result
    }).then(function () {
      var folder = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};
      $ctrl.search.name_folder = folder.text; // $ctrl.validation.folder = folder

      $ctrl.search.id_folder = folder.id;
      return; // angular.forEach $ctrl.docs, (item, key) ->
      // 	item.id_folder = folder.id

      return $ctrl.validateTogo();
    }, function () {});
  };

  checkfolder = function checkfolder() {
    var hasError;
    hasError = $ctrl.docs.filter(doc)(function () {
      return doc.id_folder <= 0;
    });
    return hasError.length === 0;
  };

  $ctrl.folderclicked = function () {
    openFolderTree([]);
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $ctrl.ok = function () {
    var _doc;

    $uibModalInstance.close(function () {
      var i, len, ref, results;
      ref = $ctrl.docs;
      results = [];

      for (i = 0, len = ref.length; i < len; i++) {
        _doc = ref[i];

        if (_doc.selected === true) {
          results.push(_doc);
        }
      }

      return results;
    }());
  };

  $ctrl.validateTogo = function () {
    var _doc;

    $ctrl.canGo = false;

    $ctrl.canGo = function () {
      var i, len, ref, results;
      ref = $ctrl.docs;
      results = [];

      for (i = 0, len = ref.length; i < len; i++) {
        _doc = ref[i];

        if (_doc.selected === true) {
          results.push(_doc);
        }
      }

      return results;
    }().length > 0; // if checkfolder()
    // 	$ctrl.canGo = true


    $ctrl.canGo;
  };

  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-search-documents-controller.js.map
