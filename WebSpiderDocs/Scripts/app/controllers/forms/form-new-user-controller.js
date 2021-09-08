"use strict";

app.controller('formNewUserCtrl', ['$scope', '$uibModalInstance', 'spiderdocsService', 'comService', 'permissionLevels', 'user', function ($scope, $uibModalInstance, spiderdocsService, comService, permissionLevels, user) {
  var findPermissionBy, init, validateTogo;

  init = function init() {
    $scope.title = user == null ? '[New User]' : '[Edit User - ' + user.name + ']';

    if (user != null) {
      angular.extend($scope.model, user);
      $scope.model.access_level = findPermissionBy(user != null ? user.id_permission : void 0);
    } else {
      $scope.model = {
        access_level: findPermissionBy(user != null ? user.id_permission : void 0),
        name: '',
        password: '',
        login: ''
      };
    }
  }; // if not user? then $scope.data.access_level[1] else
  // $scope.model.access_level = $scope.data.access_level[1];


  $scope.checkName = function () {
    if ($scope.model.name == null) {
      $scope.model.name = '';
    }

    return $scope.model.name.length > 0;
  };

  $scope.checkPassword = function () {
    return $scope.model.password.length > 6 && $scope.model.password === $scope.model.password_confimation;
  };

  $scope.checkEmail = function () {
    return $scope.model.email.indexOf('@' && $scope.model.email.length > 4);
  };

  $scope.checkLogin = function () {
    return $scope.model.login.length > 4;
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

    if (!$scope.checkPassword()) {
      return;
    } // $scope.canGo = checkName() and checkPassword() and checkEmail() and checkLogin()


    return $scope.canGo = true;
  }; // $scope.doc = {}
  // angular.copy doc, $scope.doc
  // $scope.types = types
  // $scope.type = types.find((x) ->
  // 	x.id == doc.id_docType
  // ) or id: 0
  // $scope.en_file_Status = en_file_Status
  // $scope.checkout = doc.id_status == en_file_Status.checked_out
  // $scope.canGo = false
  // $scope.selectedAttrs = {}
  // $scope.defaultAttrs = {}
  // $scope.folderclicked = ->
  // 	openFolderTree []
  // 	return


  $scope.ok = function () {
    var data, deepCopiedUser;

    if (!$scope.validateTogo()) {
      return;
    }

    deepCopiedUser = angular.extend({}, user);
    angular.extend(deepCopiedUser, $scope.model);
    data = {
      user: deepCopiedUser
    };
    $uibModalInstance.close(data);
  };

  $scope.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  $scope.validateTogo = validateTogo;

  findPermissionBy = function findPermissionBy(id) {
    ans;
    var ans, i, len, level, ref;

    if (id != null) {
      ref = $scope.data.access_level;

      for (i = 0, len = ref.length; i < len; i++) {
        level = ref[i];

        if (level.id === id) {
          ans = level;
        }
      }
    } else {
      ans = $scope.data.access_level[1];
    }

    return ans;
  };

  $scope.model = {};
  $scope.data = {
    access_level: permissionLevels
  };
  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/forms/form-new-user-controller.js.map
