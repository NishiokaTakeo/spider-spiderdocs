"use strict";

app.controller('registrationDocumentTypeController', ['$scope', '$q', 'spiderdocsService', 'comService', '$uibModal', function ($scope, $q, spiderdocsService, comService, $uibModal) {
  var init, save, updatedDocumentTypeAttr;
  $scope.selected = {
    type: 0
  };
  $scope.data = {
    types: [],
    attrs: [],
    map: []
  };
  $scope.view = {
    data: {
      attrs: [],
      types: [],
      fieldType: []
    }
  };

  init = function init() {
    return $q.all([spiderdocsService.GetDocumentTypes(), spiderdocsService.GetAttributes(), spiderdocsService.GetAttributeDocType(), spiderdocsService.cache.fieldtypes()]).then(function (res) {
      var attrs, fieldType, map, types;
      types = res[0];
      attrs = res[1];
      map = res[2];
      fieldType = res[3];
      $scope.data.types = types;
      $scope.data.attrs = attrs;
      $scope.data.map = map;
      $scope.data.fieldType = fieldType;
      $scope.view.data.types = types.sort(function (a, b) {
        if (a.type.toUpperCase() < b.type.toUpperCase()) {
          return -1;
        } else {
          return 1;
        }
      });
      $scope.view.data.attrs = attrs;
      $scope.typeChange(types[0]);
    }).then(function () {
      comService.loading(false);
    });
  };

  $scope.edit = function (type) {
    return save('edit', type);
  };

  $scope.new = function () {
    return save('new');
  };

  $scope.delete = function (doctype) {
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
          return 'Are you sure to delete selected Document Type?';
        },
        message: function message() {
          return "The action cannot redo once deleted '".concat(doctype.type, "'.");
        }
      }
    });
    modalInstance.result.then(function (result) {
      if (result === 'ok') {
        return spiderdocsService.RemoveDocumentTypeAsync(doctype.id).then(function (ans) {
          if (ans === true) {
            $scope.data.types = $scope.data.types.filter(function (x) {
              return x.id !== doctype.id;
            });
            angular.copy($scope.data.types, $scope.view.data.types);
            Notify("The document Type has been deleted! (".concat(doctype.type, ")"), null, null, 'success');
          } else {
            Notify("This Type of Document has already been used in some document. (".concat(doctype.type, ")"), null, null, 'danger');
          }
        });
      } else {}
    });
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

  $scope.typeChange = function (type) {
    var _attrs, attr, i, j, len, len1, map, ref;

    _attrs = [];
    $scope.selected.type = type.id;
    angular.copy($scope.data.attrs, _attrs); //when attr.id_doc_type is type.id

    for (i = 0, len = _attrs.length; i < len; i++) {
      attr = _attrs[i];
      attr.checked = false; // set default

      attr.duplicate_chk = false; // set default

      ref = $scope.data.map;

      for (j = 0, len1 = ref.length; j < len1; j++) {
        map = ref[j];

        if (!(map.id_doc_type === type.id && map.id_attribute === attr.id)) {
          continue;
        }

        attr.duplicate_chk = map != null ? map.duplicate_chk : void 0;
        attr.checked = true;
      }
    }

    return $scope.view.data.attrs = _attrs.sort(function (a, b) {
      if (a.checked === true) {
        return -1;
      } else {
        return 1;
      }
    });
  }; // $scope.data.map
  // spiderdocsService.GetUserIdInGroupAsync (group.id)
  // .then (ids) ->
  // 	_users = []
  // 	angular.copy $scope.data.attrs, _users
  // 	for user in _users when ids.includes(user.id)
  // 		user.checked = true
  // 	$scope.view.data.attrs = _users.sort (a,b) ->
  // 		if a.checked is true then -1 else 1


  $scope.toggleSelected = function (attr, old) {
    return updatedDocumentTypeAttr(attr).then(function (response) {
      if (response === false) {
        return attr.checked = old;
      }
    });
  };

  $scope.toggleDupSelected = function (attr, old) {
    return updatedDocumentTypeAttr(attr).then(function (response) {
      if (response === false) {
        return attr.duplicate_chk = old;
      }
    });
  };

  updatedDocumentTypeAttr = function updatedDocumentTypeAttr(attr) {
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

  save = function save(mode, type) {
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-save-documenttype.html',
      controller: 'formSaveDocumentTypeCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        doctype: function doctype() {
          return type;
        }
      }
    });
    return modalInstance.result.then(function (data) {
      var doctype, ref, ref1;
      comService.loading(true);
      doctype = {
        id: (ref = (ref1 = data.doctype) != null ? ref1.id : void 0) != null ? ref : 0,
        type: data.doctype.type
      };
      return spiderdocsService.SaveDocumentTypeAsync(doctype.id, doctype.type).then(function (_doctype) {
        var found;
        found = $scope.data.types.find(function (u) {
          return u.id === _doctype.id;
        });

        if (found != null) {
          angular.extend(found, _doctype);
        } else {
          $scope.data.types.push(_doctype);
        }

        if (_doctype.id > 0) {
          Notify("A Document Type has been added ".concat(_doctype.type), null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        return comService.loading(false);
      });
    });
  }; // 	return
  // ), ->
  //return


  return init();
}]);
//# sourceMappingURL=../../maps/app/controllers/registration-documenttype-controller.js.map
