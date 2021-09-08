"use strict";

app.controller('permissionGroupController', ['$scope', '$q', 'spiderdocsService', 'comService', '$uibModal', function ($scope, $q, spiderdocsService, comService, $uibModal) {
  var init;
  $scope.selected = {
    group: 0
  };
  $scope.data = {
    groups: [],
    users: []
  };
  $scope.view = {
    data: {
      users: [],
      groups: []
    }
  };

  init = function init() {
    return $q.all([spiderdocsService.GetGroupsAsync(), spiderdocsService.GetUsersAsync()]).then(function (res) {
      var groups, users;
      groups = res[0];
      users = res[1]; // spiderdocsService.GetGroupsAsync().then((res) ->
      // format(res)

      $scope.data.groups = groups;
      $scope.data.users = users;
      $scope.view.data.groups = groups;
      $scope.view.data.users = users;
      $scope.groupChange(groups[0]);
    }).then(function () {
      comService.loading(false);
    });
  };

  $scope.edit = function (group) {
    return save('edit', group);
  };

  $scope.new = function () {
    return save('new');
  };

  $scope.groupChange = function (group) {
    $scope.selected.group = group.id;
    return spiderdocsService.GetUserIdInGroupAsync(group.id).then(function (ids) {
      var _users, i, len, user;

      _users = [];
      angular.copy($scope.data.users, _users);

      for (i = 0, len = _users.length; i < len; i++) {
        user = _users[i];

        if (ids.includes(user.id)) {
          user.checked = true;
        }
      }

      return $scope.view.data.users = _users.sort(function (a, b) {
        if (a.checked === true) {
          return -1;
        } else {
          return 1;
        }
      });
    });
  };

  $scope.toggleSelected = function (user) {
    var group, i, idGroup, idUser, len, name, promise, ref;
    idGroup = $scope.selected.group;
    idUser = user.id;
    ref = $scope.data.groups;

    for (i = 0, len = ref.length; i < len; i++) {
      group = ref[i];

      if (group.id === $scope.selected.group) {
        name = group.group_name;
      }
    }

    promise = spiderdocsService.DeleteUserGroupAsync(idGroup, idUser);

    if (user.checked) {
      return promise.then(function (success) {
        if (success === true) {
          return spiderdocsService.AssignGroupAsync(idGroup, idUser);
        }
      }).then(function (success) {
        if (success === true) {
          return Notify("A user has been added to ".concat(name), null, null, 'success');
        } else {
          return Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }
      });
    } else {
      return promise.then(function (success) {
        return Notify("A user has been removed from ".concat(name), null, null, 'success');
      });
    }
  };

  return init();
}]);
//# sourceMappingURL=../../maps/app/controllers/permission-group-controller.js.map
