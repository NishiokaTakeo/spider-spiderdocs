"use strict";

app.controller('registrationUserController', ['$scope', 'spiderdocsService', 'comService', '$uibModal', function ($scope, spiderdocsService, comService, $uibModal) {
  var init, save;
  $scope.data = {
    users: []
  };

  init = function init() {
    return spiderdocsService.GetUsersAsync().then(function (res) {
      // format(res)
      $scope.data.users = res;
    }).then(function () {
      comService.loading(false);
    });
  };

  $scope.edit = function (user) {
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-input.html',
      controller: 'formInputCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        title: function title() {
          return '[Current Password Confimation]';
        },
        message: function message() {
          return 'Please enter current password';
        },
        defInput: function defInput() {
          return '';
        },
        reasonMinLen: function reasonMinLen() {
          return 1;
        },
        needsInput: function needsInput() {
          return true;
        },
        inputType: function inputType() {
          return 'password';
        }
      }
    });
    modalInstance.result.then(function (password) {
      comService.loading(true);
      spiderdocsService.GetUserByLoginPasswordAsync(user.login, password).then(function (found) {
        if (found.id > 0) {
          save('edit', user);
        } else {
          Notify('Password is not correct.', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    });
  };

  $scope.new = function () {
    return save('new');
  };

  save = function save(mode, _user2) {
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-new-user.html',
      controller: 'formNewUserCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        user: function user() {
          return _user2;
        },
        permissionLevels: function permissionLevels() {
          return spiderdocsService.GetPermissionLevels();
        }
      }
    });
    modalInstance.result.then(function (data) {
      var ref, ref1;
      comService.loading(true);
      _user2 = {
        id: (ref = (ref1 = data.user) != null ? ref1.id : void 0) != null ? ref : 0,
        login: data.user.login,
        name: data.user.name,
        id_permission: data.user.access_level.id,
        email: data.user.email,
        reviewer: data.user.reviewer,
        active: data.user.active,
        password: data.user.password,
        name_computer: data.user.name_computer
      };
      spiderdocsService.SaveUserAsync(_user2).then(function (_user) {
        var found; // if mode is 'new'
        // 	$scope.data.users.push(_user);
        // else if mode is 'edit'

        found = $scope.data.users.find(function (u) {
          return u.id === _user.id;
        });

        if (found != null) {
          angular.extend(found, _user);
        } else {
          $scope.data.users.push(_user);
        }

        if (_user.id > 0) {
          Notify('A user has been added', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        return comService.loading(false);
      });
    }, function () {});
  };

  return init();
}]);
//# sourceMappingURL=../../maps/app/controllers/registration-users-controller.js.map
