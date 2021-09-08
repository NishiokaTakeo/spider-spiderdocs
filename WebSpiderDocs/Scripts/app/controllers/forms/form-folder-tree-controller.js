"use strict";

app.controller('formFolderTreeCtrl', ['$uibModalInstance', 'folders', 'permission', 'en_Actions', 'menu', '$scope', 'modalService', 'spiderdocsService', 'canCreate', 'comService', function ($uibModalInstance, folders, permission, en_Actions, menu, $scope, modalSrv, spiderDocsSrv, canCreate, comService) {
  var $ctrl, init;
  $ctrl = this;

  init = function init() {}; // bindDragEvents();


  $ctrl.folders = folders;
  $ctrl.permission = permission || en_Actions.CheckIn_Out; // $ctrl.needReason = reasonNewVersion;
  // $ctrl.file = undefined;

  $ctrl.canGo = false; // $ctrl.reason = '';

  $ctrl.selected = {};
  $ctrl.canCreate = canCreate;
  $ctrl.menu = menu || {}; // $ctrl.createFolder = (attr, scope) => 
  // 	console.log attr,scope

  $ctrl.createAtRoot = function () {
    var cached, typed;
    typed = '';
    cached = [];
    return modalSrv.input("Create new folder", "Folder Name").then(function (text) {
      // console.log text
      typed = text;
      return spiderDocsSrv.cache.foldersL1();
    }).then(function (folders) {
      cached = folders;
      return spiderDocsSrv.SaveFolder({
        document_folder: typed
      });
    }).then(function (folder) {
      var _folder, found, i, len, nodes;

      for (i = 0, len = cached.length; i < len; i++) {
        _folder = cached[i];

        if (_folder.document_folder === typed) {
          found = _folder;
        }
      }

      if (found != null) {
        Notify("Rejected. It has been already there. (".concat(typed, ") "), null, null, 'danger');
      } else {
        if (folder.id > 0) {
          nodes = comService.nodesBy(0, [folder]);
          $ctrl.folders.push(nodes[0]);
          Notify('Folder has been created', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that.', null, null, 'danger');
        }
      }

      return spiderDocsSrv.cache.clearAll();
    });
  };

  init();

  $ctrl.ok = function () {
    $uibModalInstance.close($ctrl.selected);
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $ctrl.validateTogo = function () {
    var node, selected;
    $ctrl.canGo = false;
    selected = angular.element('.container--tree').find('[data-selected-folder-id]');
    node = {
      id: selected.attr('data-selected-folder-id'),
      text: selected.attr('data-text')
    };
    $ctrl.selected = node;
    $ctrl.canGo = parseInt(node.id) > 0;
    return $ctrl.canGo;
  };
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-folder-tree-controller.js.map
