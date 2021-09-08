"use strict";

app.controller('formSaveAttributesCtrl', ['$scope', '$uibModal', '$uibModalInstance', 'spiderdocsService', 'comService', 'attribute', 'types', 'en_Actions', 'modalService', function ($scope, $uibModal, $uibModalInstance, spiderdocsService, comService, attribute, types, en_Actions, modalService) {
  var addNewRequire, convertParameters, findParameter, findTypeBy, init, restoreRequire, validateTogo;

  init = function init() {
    $scope.title = attribute == null ? '[New Attribute]' : '[Edit Attribute - ' + attribute.name + ']';
    $scope.model = {
      name: '',
      type: findTypeBy(attribute != null ? attribute.id_type : void 0),
      max_lengh: 20,
      only_numbers: false,
      required: false,
      position: 1,
      parameters: [],
      selectedFolder: {
        id: 0,
        text: '--- Default ---'
      }
    };

    if (attribute != null) {
      angular.extend($scope.model, attribute);
      $scope.model.type = findTypeBy(attribute != null ? attribute.id_type : void 0);
      $scope.model.parameters = convertParameters(attribute.parameters);
    }

    if (findParameter(0) == null) {
      addNewRequire(0);
    }
  };

  $scope.checkName = function () {
    if ($scope.model.name == null) {
      $scope.model.name = '';
    }

    return $scope.model.name.length > 0;
  };

  validateTogo = function validateTogo() {
    var control, i, len, ref;
    $scope.canGo = false;
    ref = $scope.myform.$$controls;

    for (i = 0, len = ref.length; i < len; i++) {
      control = ref[i];
      control.$setDirty();
      control.$validate();
    }

    if ($scope.myform.$invalid === true) {
      Notify('Can not proceed. <br/>Please Fix error(s)', null, null, 'danger');
      return;
    }

    return $scope.canGo = true;
  };

  $scope.openFolderTree = function () {
    var folders = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : [];
    spiderdocsService.cache.foldersL1(0, en_Actions.None).then(function (_folders) {
      var nodes;
      nodes = comService.nodesBy(0, _folders);
      return modalService.openfolder(nodes, en_Actions.None); // $uibModal.open(
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
      // 			en_Actions.None
      // ).result
    }).then(function () {
      var folder = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};
      $scope.model.name_folder = folder.text;
      $scope.model.selectedFolder = folder; //update mandatory

      if (findParameter(folder.id) == null) {
        addNewRequire(folder.id);
      }

      restoreRequire(folder.id);
    }, function () {});
  };

  $scope.ok = function () {
    var data, deepCopiedAttr, i, len, param, parameter, ref;

    if (!$scope.validateTogo()) {
      return;
    }

    deepCopiedAttr = angular.extend({}, attribute);
    angular.extend(deepCopiedAttr, $scope.model);
    parameter = {
      id_folder: $scope.model.selectedFolder.id,
      required: $scope.model.required
    };
    ref = $scope.model.parameters;

    for (i = 0, len = ref.length; i < len; i++) {
      param = ref[i];
      param.required = param.required === true ? 1 : 0;
    }

    deepCopiedAttr.parameters = $scope.model.parameters.filter(function (p) {
      return p.required === 1 || parseInt(p.id_folder) === 0;
    });
    data = {
      attribute: deepCopiedAttr
    };
    $uibModalInstance.close(data);
  };

  $scope.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $scope.validateTogo = validateTogo;

  $scope.updateRequire = function () {
    var found, id_folder, ref, ref1;
    id_folder = (ref = (ref1 = $scope.model.selectedFolder) != null ? ref1.id : void 0) != null ? ref : 0;
    found = findParameter(id_folder);

    if (found == null) {
      addNewRequire(id_folder);
    }

    return found.required = $scope.model.required;
  };

  restoreRequire = function restoreRequire(id_folder) {
    var found;
    found = findParameter(id_folder);
    return $scope.model.required = found.required;
  };

  findParameter = function findParameter(id_folder) {
    var found;
    found = $scope.model.parameters.find(function (p) {
      return p.id_folder === id_folder;
    });
    return found;
  };

  addNewRequire = function addNewRequire(id_folder) {
    var adding;
    $scope.model.required = false; // set to default

    adding = {
      id_folder: id_folder,
      required: false
    };
    return $scope.model.parameters.push(adding);
  };

  convertParameters = function convertParameters(params) {
    var i, len, p;

    for (i = 0, len = params.length; i < len; i++) {
      p = params[i];
      p.required = p.required === 1 ? true : false;
      p.id_folder = p.id_folder.toString();
    }

    return params;
  };

  findTypeBy = function findTypeBy(id) {
    ans;
    var ans, i, len, ref, type;

    if (id != null) {
      ref = $scope.data.types;

      for (i = 0, len = ref.length; i < len; i++) {
        type = ref[i];

        if (type.id === id) {
          ans = type;
        }
      }
    } else {
      ans = $scope.data.types[0];
    }

    return ans;
  };

  $scope.model = {};
  $scope.data = {
    types: types
  };
  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-save-attributes-controller.js.map
