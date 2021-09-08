"use strict";

app.controller('registrationGroupController', ['$scope', 'spiderdocsService', 'comService', '$uibModal', function ($scope, spiderdocsService, comService, $uibModal) {
  var init, save;
  $scope.data = {
    groups: []
  };

  init = function init() {
    return spiderdocsService.GetGroupsAsync().then(function (res) {
      // format(res)
      $scope.data.groups = res;
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

  save = function save(mode, _group2) {
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-save-group.html',
      controller: 'formSaveGroupCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        group: function group() {
          return _group2;
        }
      }
    });
    modalInstance.result.then(function (data) {
      var ref, ref1;
      comService.loading(true);
      _group2 = {
        id: (ref = (ref1 = data.group) != null ? ref1.id : void 0) != null ? ref : 0,
        group_name: data.group.group_name,
        obs: data.group.obs,
        ordination: data.group.ordination
      }; // is_admin : data.group.is_admin

      spiderdocsService.SaveGroupAsync(_group2).then(function (_group) {
        var found;
        found = $scope.data.groups.find(function (u) {
          return u.id === _group.id;
        });

        if (found != null) {
          angular.extend(found, _group);
        } else {
          $scope.data.groups.push(_group);
        }

        if (_group.id > 0) {
          Notify('A group has been added', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        return comService.loading(false);
      });
    }, function () {});
  };

  return init();
}]);
//# sourceMappingURL=../../maps/app/controllers/registration-group-controller.js.map
