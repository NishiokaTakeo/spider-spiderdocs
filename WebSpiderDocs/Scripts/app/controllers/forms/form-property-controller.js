"use strict";

app.controller('FormPropertyCtrl', ['$scope', '$timeout', '$uibModalInstance', 'spiderdocsService', 'comService', '$uibModal', 'tagService', 'doc', 'types', 'attrs', 'comboitems', 'en_file_Status', 'en_Actions', 'notificationgroups', 'customviews', 'customviewdocuments', 'modalService', function ($scope, $timeout, $uibModalInstance, spiderdocsService, comService, $uibModal, tagService, doc, types, attrs, comboitems, en_file_Status, en_Actions, notificationgroups, customviews, customviewdocuments, modalService) {
  var $ctrl, checkfolder, disabledIf, init, openFolderTree, updateDoc, validateTogo;
  $ctrl = this;
  $ctrl.notificationgroups = notificationgroups;
  $ctrl.data = {
    customviews: []
  };

  init = function init() {
    var checked, i, len, vdoc, view; // $ctrl.data.customviews = customviews

    for (i = 0, len = customviews.length; i < len; i++) {
      view = customviews[i];

      checked = function () {
        var j, len1, results;
        results = [];

        for (j = 0, len1 = customviewdocuments.length; j < len1; j++) {
          vdoc = customviewdocuments[j];

          if (vdoc.id_custom_view === view.id) {
            results.push(vdoc);
          }
        }

        return results;
      }();

      $ctrl.data.customviews.push({
        id: view.id,
        text: view.name
      }); // checked: checked.length > 0

      if (checked.length > 0) {
        $ctrl.customview = $ctrl.data.customviews[$ctrl.data.customviews.length - 1];
      }
    } // $ctrl.data.customviews.push
    // 	id: view.id
    // 	text: view.name
    // 	checked: checked.length > 0


    $ctrl.data.customviews.unshift({
      id: 0,
      text: 'Local Database'
    }); // checked = false

    if ($ctrl.customview == null) {
      // $ctrl.customview = undefined
      $ctrl.customview = $ctrl.data.customviews[0];
    }

    $ctrl.defaultAttrs = $ctrl.doc.attrs;
    validateTogo();
  };

  $ctrl.attrchanged = function () {
    return disabledIf();
  };

  validateTogo = function validateTogo() {
    $ctrl.canGo = false;

    if (checkfolder()) {
      $ctrl.canGo = true;
    }

    return $ctrl.canGo;
  };

  openFolderTree = function openFolderTree(folders) {
    spiderdocsService.cache.foldersL1(0, en_Actions.CheckIn_Out).then(function (folders) {
      var nodes;
      nodes = comService.nodesBy(0, folders);
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
      // 			comService.nodesBy 0, folders
      // 		permission: ->
      // 			en_Actions.CheckIn_Out
      // ).result
    }).then(function () {
      var folder = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};
      $ctrl.name_folder = folder.text;
      $ctrl.doc.id_folder = folder.id;
      $ctrl.doc.name_folder = folder.text;
      $ctrl.validateTogo();
    }, function () {});
  };

  checkfolder = function checkfolder() {
    var hasError;
    hasError = $ctrl.doc.id_folder <= 0;
    return !hasError;
  };

  disabledIf = function disabledIf() {
    if (doc.id_status === en_file_Status.checked_out) {
      angular.element('.modal--property').find('input, select, textarea').prop('disabled', true);
    }
  };

  updateDoc = function updateDoc() {
    var _doc, opt;

    _doc = {};
    angular.copy($ctrl.doc, _doc);
    _doc.title = $ctrl.name;
    _doc.id_folder = $ctrl.doc.id_folder;
    _doc.id_docType = $ctrl.type.id;
    _doc.attrs = $ctrl.selectedAttrs;

    _doc.id_notification_group = function () {
      var i, len, ref, results;
      ref = $ctrl.notificationgroups;
      results = [];

      for (i = 0, len = ref.length; i < len; i++) {
        opt = ref[i];

        if (opt.checked === true) {
          results.push(opt.id);
        }
      }

      return results;
    }(); // _doc.customviews = ( item for item in $ctrl.data.customviews when item.checked is true )
    // _doc.customviews = if $ctrl.customview? and $ctrl.customview.id isnt 0 then _doc.customviews = [$ctrl.customview] else []


    _doc.customviews = $ctrl.customview != null ? _doc.customviews = [$ctrl.customview] : [];
    return _doc;
  };

  $ctrl.doc = {};
  angular.copy(doc, $ctrl.doc);
  $ctrl.types = types;
  $ctrl.type = types.find(function (x) {
    return x.id === doc.id_docType;
  }) || {
    id: 0
  };
  $ctrl.en_file_Status = en_file_Status;
  $ctrl.checkout = doc.id_status === en_file_Status.checked_out;
  $ctrl.canGo = false;
  $ctrl.selectedAttrs = {};
  $ctrl.defaultAttrs = {};
  init(); // $ctrl.customviewchanged = () ->
  // 	a = 'a'

  $ctrl.folderclicked = function () {
    openFolderTree([]);
  };

  $ctrl.ok = function () {
    var reflectedChange;
    reflectedChange = updateDoc();
    $uibModalInstance.close(reflectedChange);
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $ctrl.validateTogo = validateTogo;
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-property-controller.js.map
