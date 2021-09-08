"use strict";

app.controller('permissionFolderController', ['$scope', '$q', 'spiderdocsService', 'comService', '$uibModal', 'en_Actions', 'en_FolderPermission', 'modalService', function ($scope, $q, spiderdocsService, comService, $uibModal, en_Actions, en_FolderPermission, modalService) {
  var addFolder, addToFolder, clearPermissions, en_FolderPermissionFom, init, permissionAPIParams, refreshDisplayOn, removeFolder, renameFolder, setupMenu, updatePermission; // $scope.rootMenu = 
  // 	[
  // 		label: 'Create Folder'
  // 		action: 'createFolder'
  // 		active: true
  // 	]

  $scope.selected = {
    type: 0
  };
  $scope.data = {
    permissionTitles: [],
    types: [],
    attrs: [],
    map: [],
    // allowed: []
    users: [],
    menu: {
      items: [],
      func: {}
    }
  };
  $scope.view = {
    search: {},
    inheritance: {
      is: false,
      from: 'NONE'
    },
    data: {
      attrs: [],
      types: [],
      fieldType: [],
      allowed: [],
      users: [],
      permissions: []
    }
  };
  $scope.search = {}; // $scope.explorer =
  // 	folders: []
  // 	folder_default: 0

  init = function init() {
    return $q.all([spiderdocsService.GetGroupsAsync(), spiderdocsService.GetAttributes(), spiderdocsService.GetAttributeDocType(), spiderdocsService.cache.fieldtypes(), spiderdocsService.cache.users(), spiderdocsService.cache.permissiontitles()]).then(function (res) {
      var attrs, fieldType, group, map, pertitles, users;
      group = res[0];
      attrs = res[1];
      map = res[2];
      fieldType = res[3];
      users = res[4];
      pertitles = res[5];
      $scope.data.types = group;
      $scope.data.attrs = attrs;
      $scope.data.map = map;
      $scope.data.fieldType = fieldType;
      $scope.data.users = users;
      $scope.data.permissionTitles = pertitles;
      $scope.view.data.groups = group.sort(function (a, b) {
        if (a.group_name.toUpperCase() < b.group_name.toUpperCase()) {
          return -1;
        } else {
          return 1;
        }
      });
      $scope.view.data.attrs = attrs;
      $scope.groupChange(group[0]);
      setupMenu();
    }).then(function () {
      comService.loading(false);
    });
  };

  $scope.openSearchFolderForm = function () {
    // spiderdocsService.cache.foldersL1(0, en_Actions.OpenRead).then((folders) ->
    spiderdocsService.cache.foldersL1(0).then(function (folders) {
      var nodes;
      nodes = comService.nodesBy(0, folders);
      return modalService.openfolder(nodes, en_Actions.OpenRead, $scope.data.menu, true); // $uibModal.open(
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
      // 			en_Actions.OpenRead
      // 		menu: ->
      // 			$scope.data.menu
      // ).result
    }).then(function (folder) {
      if (folder === void 0) {
        folder = {};
      }

      $scope.view.search.name_folder = folder.text;
      $scope.view.search.id_folder = folder.id; // Updat Allowed and Permission section

      return refreshDisplayOn(folder.id);
    });
  };

  refreshDisplayOn = function refreshDisplayOn(id_folder) {
    return $q.all([spiderdocsService.GetUsersByAsync({
      idFolder: id_folder
    }), spiderdocsService.GetGroupsByAsync({
      idFolder: id_folder
    }), spiderdocsService.GetInheritedFolderName(id_folder), spiderdocsService.SearchFoldersAsync([id_folder])]).then(function (data) {
      var folders, i, inherited, inheritedFrom, item, j, len, len1, ref, ref1;
      inheritedFrom = data[2];
      folders = data[3];
      $scope.view.data.allowed = [];
      ref = data[0];

      for (i = 0, len = ref.length; i < len; i++) {
        item = ref[i];
        $scope.view.data.allowed.push({
          id: item.id,
          checked: false,
          name: item.name,
          type: 'user'
        });
      }

      ref1 = data[1];

      for (j = 0, len1 = ref1.length; j < len1; j++) {
        item = ref1[j];
        $scope.view.data.allowed.push({
          id: item.id,
          checked: false,
          name: item.group_name,
          type: 'group'
        });
      }

      $scope.view.data.permissions = [];
      inherited = inheritedFrom == null || inheritedFrom.length === 0 || inheritedFrom.toLowerCase() === 'none' ? 'NONE' : inheritedFrom;
      $scope.view.search.id_parent = folders[0].id_parent;
      return $scope.updateInherited(inherited);
    });
  };

  $scope.addGroupToFolder = function (group) {
    return addToFolder('group', group);
  };

  $scope.addUserToFolder = function (user) {
    return addToFolder('user', user);
  }; // $scope.new = () ->
  // 	save('new');


  $scope.toggleInheritance = function () {
    return spiderdocsService.GetInheritanceFolder($scope.view.search.id_folder).then(function (folder) {
      var _message, modalInstance;

      if (folder.id === $scope.view.search.id_folder) {
        // // (Enable inheritance)
        _message = "The folder \"".concat($scope.view.search.document_folder, "\" will be INHERITED from a parent.\n\nAre you sure to make this?");
      } else {
        // // (Disable inheritance)
        _message = "The folder\" ".concat($scope.view.search.document_folder, "\" will NOT be inherit.\n\n Are you sure to make this?");
      }

      modalInstance = $uibModal.open({
        animation: true,
        ariaLabelledBy: 'modal-title',
        ariaDescribedBy: 'modal-body',
        templateUrl: 'form-confimation.html',
        controller: 'formConfimationCtrl',
        controllerAs: '$ctrl',
        backdrop: 'static',
        size: 'sm',
        resolve: {
          title: function title() {
            return 'Cannot redu';
          },
          message: function message() {
            return _message;
          }
        }
      });
      modalInstance.result.then(function (result) {
        if (result === 'ok') {
          // // Delete current folder's permission
          spiderdocsService.ToggleInheritance($scope.view.search.id_folder).then(function () {
            // PermissionController.DeleteAllPermission curFolder.id
            clearPermissions();
            return refreshDisplayOn($scope.view.search.id_folder);
          });
        }
      }, function () {}); // DisableEditableCtrs();
    });
  }; // finish logic.
  // $log.info('Modal dismissed at: ' + new Date());


  $scope.updateInherited = function (inherited) {
    $scope.view.inheritance.from = inherited;
    return $scope.view.inheritance.is = inherited !== '' && inherited !== 'NONE';
  };

  $scope.delete = function (grpOrUsr) {
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'form-confimation.html',
      controller: 'formConfimationCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        title: function title() {
          return 'Are you sure to delete selected item?';
        },
        message: function message() {
          return "The action cannot redo once deleted '".concat(grpOrUsr.name, "'.");
        }
      }
    });
    modalInstance.result.then(function (result) {
      if (result === 'ok') {
        return spiderdocsService.DeletePermissionAsync(grpOrUsr.type, $scope.view.search.id_folder, grpOrUsr.id).then(function (ans) {
          if (ans === '') {
            $scope.view.data.allowed = $scope.view.data.allowed.filter(function (u) {
              return !(u.type === grpOrUsr.type && u.id === grpOrUsr.id);
            });
            Notify("Deleted successfully ".concat(grpOrUsr.name), null, null, 'success');
          } else {
            Notify(ans, null, null, 'danger');
          }

          comService.loading(false);
        });
      } else {}
    });
  };

  $scope.perNameBy = function (id) {
    var d, i, key, len, permission, ref;
    d = $scope.data.permissionTitles;
    ref = Object.keys(d);

    for (i = 0, len = ref.length; i < len; i++) {
      key = ref[i];

      if (key.toLowerCase() === id.toString().toLowerCase()) {
        permission = d[key];
      }
    }

    return permission;
  };

  $scope.isDeny = function (item) {
    switch (item) {
      case -1:
        return true;

      case 2:
        return true;

      default:
        return false;
    }
  };

  $scope.isAllow = function (item) {
    switch (item) {
      case 1:
        return true;

      case 2:
        return true;

      default:
        return false;
    }
  };

  $scope.name_fieldType = function (id) {
    var i, item, len, ref, type;
    ref = $scope.data.fieldType;

    for (i = 0, len = ref.length; i < len; i++) {
      item = ref[i];

      if (item.id === id) {
        type = item.type;
      }
    }

    return type;
  };

  $scope.groupChange = function (group) {
    // users = []
    $scope.view.data.users = []; // angular.copy $scope.data.users, users

    return spiderdocsService.GetUserIdInGroupAsync(group.id).then(function (userIds) {
      var i, len, ref, results, user;
      ref = $scope.data.users;
      results = [];

      for (i = 0, len = ref.length; i < len; i++) {
        user = ref[i];

        if (userIds.includes(user.id)) {
          results.push($scope.view.data.users.push(user));
        }
      }

      return results;
    });
  }; // _attrs = []
  // $scope.selected.group = group.id
  // angular.copy $scope.data.attrs, _attrs
  // for attr in _attrs #when attr.id_doc_group is group.id
  // 	attr.checked = false # set default
  // 	attr.duplicate_chk = false # set default
  // 	for map in $scope.data.map when map.id_doc_group is group.id and map.id_attribute is attr.id
  // 		attr.duplicate_chk = map?.duplicate_chk
  // 		attr.checked = true
  // $scope.view.data.attrs = _attrs.sort (a,b) ->
  // 		if a.checked is true then -1 else 1
  // $scope.data.map
  // spiderdocsService.GetUserIdInGroupAsync (group.id)
  // .then (ids) ->
  // 	_users = []
  // 	angular.copy $scope.data.attrs, _users
  // 	for user in _users when ids.includes(user.id)
  // 		user.checked = true
  // 	$scope.view.data.attrs = _users.sort (a,b) ->
  // 		if a.checked is true then -1 else 1


  $scope.toggleSelected = function (item, old) {
    var _item, checked, i, len, ref;

    checked = !item.checked;

    if ($scope.view.inheritance.from.toLowerCase() !== 'none') {
      clearPermissions();
      return;
    }

    ref = $scope.view.data.allowed;

    for (i = 0, len = ref.length; i < len; i++) {
      _item = ref[i];
      _item.checked = false;
    }

    item.checked = checked;
    $scope.view.data.permissions = []; // Populate Permissions

    return spiderdocsService.GetPermissionsByGroupAndUser(item.type, $scope.view.search.id_folder, item.id).then(function (permissions) {
      var j, k, key, len1, len2, per, perfund, ref1, ref2, ref3, val;
      ref1 = Object.keys(en_Actions);

      for (j = 0, len1 = ref1.length; j < len1; j++) {
        key = ref1[j];

        if (0 < (ref2 = en_Actions[key]) && ref2 < 100) {
          ref3 = Object.keys(permissions);

          for (k = 0, len2 = ref3.length; k < len2; k++) {
            per = ref3[k];

            if (per.toLowerCase() === key.toLowerCase()) {
              perfund = per;
            }
          }

          val = perfund != null ? permissions[perfund] : 0; // per = if perfund? then perfund[key] else en_Actions[key]
          // val = permissions[per]

          $scope.view.data.permissions.push({
            key: key,
            keyId: en_Actions[key],
            name: $scope.perNameBy(key),
            allow: $scope.isAllow(val),
            deny: $scope.isDeny(val)
          });
        }
      } // permission:


      return $scope.view.data.permissions;
    });
  };

  $scope.permissionChanged = function (type, per) {
    var a, apiCall, i, len, permissions, ref, selected;
    permissions = permissionAPIParams();
    ref = $scope.view.data.allowed;

    for (i = 0, len = ref.length; i < len; i++) {
      a = ref[i];

      if (a.checked === true) {
        selected = a;
      }
    }

    apiCall = function () {
      switch (selected.type) {
        case 'group':
          return spiderdocsService.AddPermissionToGroup($scope.view.search.id_folder, selected.id, permissions);

        case 'user':
          return spiderdocsService.AddPermissionToUser($scope.view.search.id_folder, selected.id, permissions);
      }
    }();

    return apiCall.then(function (res) {
      Notify("Permission has been updated", null, null, 'success');
      return spiderdocsService.clearAll('db-folder');
    });
  };

  setupMenu = function setupMenu() {
    //Menu Items Array
    return $scope.data.menu = {
      items: [{
        label: 'Rename',
        action: 'renameFolder',
        active: true
      }, {
        label: 'Add',
        action: 'addFolder',
        active: true
      }, {
        label: 'Remove',
        action: 'removeFolder',
        active: true
      }],
      func: {
        renameFolder: renameFolder,
        addFolder: addFolder,
        removeFolder: removeFolder
      }
    };
  };

  en_FolderPermissionFom = function en_FolderPermissionFom(per) {
    var ans;
    return ans = per.allow === false && per.deny === false ? 0 : per.allow === true && per.deny === true ? 2 : per.deny === true ? -1 : per.allow === true ? 1 : 0;
  };

  permissionAPIParams = function permissionAPIParams() {
    var ans, i, len, p, ref;
    ans = {};
    ref = $scope.view.data.permissions; // row = {}

    for (i = 0, len = ref.length; i < len; i++) {
      p = ref[i];
      ans[p.keyId] = en_FolderPermissionFom(p);
    } // ans.push row
    // row = {}


    return ans;
  };

  updatePermission = function updatePermission(attr) {
    var chkAtb, chkDup, i, id_attr, id_attrs, id_doc_type, len, name_type, ref, type;
    ref = $scope.data.types;

    for (i = 0, len = ref.length; i < len; i++) {
      type = ref[i];

      if (type.id === $scope.selected.type) {
        name_type = type.type;
      }
    }

    id_doc_type = $scope.selected.type;
    id_attr = attr.id;
    chkAtb = attr.checked;
    chkDup = attr.duplicate_chk;

    id_attrs = function () {
      var j, len1, ref1, results;
      ref1 = $scope.view.data.attrs;
      results = [];

      for (j = 0, len1 = ref1.length; j < len1; j++) {
        attr = ref1[j];

        if (attr.checked === true && attr.duplicate_chk === true) {
          results.push(attr.id);
        }
      }

      return results;
    }(); // return spiderdocsService.cache.noop(false)


    return spiderdocsService.UpdateDocumentTypeAttributeAsync(id_doc_type, id_attr, chkAtb, chkDup, id_attrs).then(function (response) {
      var error;

      if (response === true) {
        Notify("Attributes have been updated (Type: ".concat(name_type, ")"), null, null, 'success');
      } else {
        // record = $scope.data.map.find (m) -> m.id_doc_type is id_doc_type and id_attribute is id_attr
        // if not record ?
        // 	$scope.data.map.push(
        // 		id_
        // 	)
        error = "Cannot proceed because being duplications after the action by either:\n\n- duplication with Title in same folder\n- duplication with Title, Type across the folder\n\nPlease, fix document first.";
        Notify(error, null, null, 'danger');
      }

      return response;
    });
  };

  addToFolder = function addToFolder(mode, grpOrUsr) {
    if ($scope.view.search.id_folder == null || $scope.view.search.id_folder === 0) {
      Notify("Select folder first.", null, null, 'warning');
      return;
    }

    comService.loading(true);
    return spiderdocsService.AddGroupOrUserToFolder(mode, $scope.view.search.id_folder, grpOrUsr.id).then(function () {
      var allowed, exists, i, len, ref;
      comService.loading(false);
      ref = $scope.view.data.allowed;

      for (i = 0, len = ref.length; i < len; i++) {
        allowed = ref[i];

        if (allowed.type === mode && allowed.id === grpOrUsr.id) {
          exists = allowed;
        }
      }

      if ((exists != null ? exists.id : void 0) > 0) {
        return;
      }

      $scope.view.data.allowed = $scope.view.data.allowed.filter(function (u) {
        return !(u.type === mode && u.id === grpOrUsr.id);
      });
      $scope.view.data.allowed.push({
        checked: false,
        name: mode === 'group' ? grpOrUsr.group_name : grpOrUsr.name,
        type: mode,
        id: grpOrUsr.id
      });

      if (grpOrUsr.id > 0) {
        return Notify("Added successfully ".concat(mode === 'group' ? grpOrUsr.group_name : grpOrUsr.name), null, null, 'success');
      } else {
        return Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
      }
    });
  };

  clearPermissions = function clearPermissions() {
    return $scope.view.data.permissions = [];
  };

  renameFolder = function renameFolder(arg, scope) {
    var node, orign;
    console.log("renameFolder (".concat(scope.node.text, ") is clicked."));
    node = scope.node;
    orign = scope.node.text;
    console.debug(scope.node);
    return modalService.input('Rename', '', node.text).then(function (newname) {
      if (node.text === newname) {
        Notify('Cancelled. New file name is same as original. ', null, null, 'warning');
        return;
      }

      comService.loading(true);
      node.text = newname;
      node.ref.document_folder = newname;

      if ((node != null ? node.Id : void 0) > 0) {
        spiderdocsService.SaveFolder(node.ref).then(function (updated) {
          if (updated.id === node.ref.id && updated.document_folder === newname) {
            Notify('The file has been renamed. \'' + oldName + '\' -> \'' + newname + '\'', null, null, 'success'); //remove folder only

            spiderdocsService.cache.clearAll('db-folder');
          } else {
            Notify('Rejected. You might not have right permission to do that OR there is same name.', null, null, 'danger');
            node.text = orign; // back to orignal folder name
          }

          comService.loading(false);
        }).catch(function () {
          comService.loading(false);
        });
      }
    });
  };

  addFolder = function addFolder(arg, scope) {
    var node;
    console.log('addFolder is clicked');
    node = scope.node;
    return modalService.input('Rename', '', '').then(function (newname) {
      var found, i, item, len, newnode, ref;
      ref = node.children;

      for (i = 0, len = ref.length; i < len; i++) {
        item = ref[i];

        if (item.text === newname) {
          found = item;
        }
      }

      if (found != null) {
        Notify('Rejected. There is same name.', null, null, 'danger');
        return;
      }

      newnode = {
        id_parent: node.ref.id,
        document_folder: newname
      };
      return spiderdocsService.SaveFolder(newnode).then(function (updated) {
        var foldernode;

        if (updated.id > 0 && updated.document_folder === newname) {
          Notify('The file has been added. \'' + newname + '\' ', null, null, 'success');
          foldernode = comService.nodesBy(node.ref.id, [updated]);
          node.children.push(foldernode[0]);
          spiderdocsService.cache.clearAll();
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    });
  };

  removeFolder = function removeFolder(arg, scope) {
    var node;
    console.log('removeFolder is clicked');
    node = scope.node;
    modalService.confirm('Spider Docs', 'Are you sure you want to delete this folder? ( This action will cancel the check out )').then(function (result) {
      if (result === 'ok') {
        comService.loading(true);
        return spiderdocsService.RemoveFolderAsync(node.Id).then(function (ok) {
          if (ok === true) {
            Notify("The foler ".concat(node.text, " has been removed."), null, null, 'success');
            scope.node = null;
            spiderdocsService.cache.clearAll();
          } else {
            Notify("Rejected. You might not have right permission to do that .", null, null, 'danger');
          }

          return comService.loading(false);
        });
      }
    });
  };

  return init();
}]);
//# sourceMappingURL=../../maps/app/controllers/permission-folder-controller.js.map
