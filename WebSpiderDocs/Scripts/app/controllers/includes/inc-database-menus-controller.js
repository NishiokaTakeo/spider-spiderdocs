"use strict";

app.controller('incDbMenuCtrl', ['$scope', '$uibModal', 'spiderdocsService', 'comService', function ($scope, $uibModal, spiderdocsService, comService) {
  var init; // parent =;
  // $scope.$parent.incDbmenuCtrl = $scope;

  $scope.dbmenus = [{
    p: './index',
    n: 'Primary Shared Database',
    id: null,
    cls: ['menu-item__label--shared-database']
  }, {
    p: './local',
    n: 'My Database',
    id: null
  }]; // $scope.title = $scope.title

  $scope.title = 'Save New Version'; // angular.copy $scope.docs, $scope.docs

  init = function init() {
    spiderdocsService.GetCustomViewsAsync().then(function (customviews) {
      var i, len, results, view;
      results = [];

      for (i = 0, len = customviews.length; i < len; i++) {
        view = customviews[i];

        if (view.name.toLowerCase() !== 'local') {
          results.push($scope.dbmenus.push({
            p: 'CustomView',
            n: view.name,
            id: view.id,
            ref: view
          }));
        }
      }

      return results;
    });
  };

  $scope.delete = function (item) {
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
          return 'DELETE VIEW';
        },
        message: function message() {
          return "Are you sure to delete this view? (".concat(item.n, ")");
        }
      }
    });
    modalInstance.result.then(function (ok) {
      if (ok === "ok") {
        return spiderdocsService.DeleteCustomView({
          id: item.id
        }).then(function (ans) {
          if (ans.inactive === true) {
            $scope.dbmenus = $scope.dbmenus.filter(function (x) {
              return x.id !== item.id;
            });
            Notify('The view has been deleted!', null, null, 'success');
          } else {
            Notify('Rejected. You might not have right permission to do that.', null, null, 'danger');
          }
        });
      }
    }, function () {});
  }; // $log.info('Modal dismissed at: ' + new Date());


  $scope.newView = function (item) {
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      // windowClass: 'modal--property'
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-new-view.html',
      controller: 'formNewViewCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        sources: function sources() {
          return spiderdocsService.GetCustomViewSources();
        },
        customviews: function customviews() {
          return spiderdocsService.GetCustomViewsAsync();
        },
        customview: function customview() {
          return item;
        }
      }
    });
    modalInstance.result.then(function (savedCustomView) {
      var edited, i, len, menu, ref, updated;

      if ((savedCustomView != null ? savedCustomView.id : void 0) > 0) {
        Notify('The view has been saved!', null, null, 'success');
        updated = {
          // p:'CustomView'
          n: savedCustomView.name,
          id: savedCustomView.id,
          ref: savedCustomView
        };
        ref = $scope.dbmenus;

        for (i = 0, len = ref.length; i < len; i++) {
          menu = ref[i];

          if (menu.id === savedCustomView.id) {
            // $scope.$emit 'dbmenus-add', updated
            edited = menu;
          }
        }

        if ((edited != null ? edited.id : void 0) > 0) {
          return angular.extend(edited, updated);
        } else {
          return $scope.dbmenus.push(updated);
        }
      } else {
        Notify('Rejected. You might not have right permission to do that.', null, null, 'danger');
      }
    }, function () {});
    return modalInstance.rendered.then(function () {});
  };

  init();
}]);
//# sourceMappingURL=../../../maps/app/controllers/includes/inc-database-menus-controller.js.map
