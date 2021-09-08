"use strict";

app.controller('formNewViewCtrl', ['$scope', '$timeout', '$uibModalInstance', 'spiderdocsService', 'comService', '$uibModal', 'sources', 'customviews', 'customview', '$location', function ($scope, $timeout, $uibModalInstance, spiderdocsService, comService, $uibModal, sources, customviews, customview, $location) {
  var _this = this;

  var $ctrl, init, ref, ref1, ref2, vendors;
  $ctrl = this;

  init = function init() {
    var i, len, ref, ref1, ref2, s;

    if ((customview != null ? (ref = customview.ref) != null ? ref.id_source : void 0 : void 0) > 0) {
      ref1 = $ctrl.view.data.sources;

      for (i = 0, len = ref1.length; i < len; i++) {
        s = ref1[i];

        if (s.id === (customview != null ? (ref2 = customview.ref) != null ? ref2.id_source : void 0 : void 0)) {
          $ctrl.view.models.source = s;
        }
      }
    } else {
      $ctrl.view.models.source = $ctrl.view.data.sources[0];
    }

    _this.validateTogo();
  };

  this.isadmin = function () {
    var ref;
    return ((ref = $location.search().role) != null ? ref.toLowerCase() : void 0) === 'admin';
  };

  this.validateTogo = function () {
    $ctrl.canGo = true;

    if (!this.nameOK(false)) {
      $ctrl.canGo = false;
      return;
    }

    if (!this.descriptionOK(false)) {
      $ctrl.canGo = false;
      return;
    }

    if (!this.sourceOK(false)) {
      $ctrl.canGo = false;
      return;
    }

    return $ctrl.canGo;
  };

  this.nameOK = function () {
    var message = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : true;
    var ans, i, len, ref, view;
    ans = false;
    ans = $ctrl.view.models.name != null && $ctrl.view.models.name !== '' ? true : false; // check name exists

    for (i = 0, len = customviews.length; i < len; i++) {
      view = customviews[i];

      if (view.name.trim() === $ctrl.view.models.name.trim() && (customview != null ? customview.ref : void 0) != null && view.id !== (customview != null ? (ref = customview.ref) != null ? ref.id : void 0 : void 0)) {
        ans = false;
      }
    }

    if (message === true && ans === false) {
      Notify('The name is not available. Use different.', null, null, 'warning');
    } // this.canGo = ans


    return ans;
  };

  this.descriptionOK = function () {
    var message = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : true;
    var ans;
    ans = $ctrl.view.models.description != null && $ctrl.view.models.description !== '' ? true : false;

    if (message === true && ans === false) {
      Notify('The description is not available. Use different.', null, null, 'warning');
    } // this.canGo = ans


    return ans;
  };

  this.sourceOK = function () {
    var message = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : true;
    var ans;
    ans = true;

    if (!($ctrl.view.models.source.id > 0)) {
      ans = false;
    }

    if (message === true && ans === false) {
      Notify('Please select location.', null, null, 'warning');
    } // this.canGo = ans


    return ans;
  };

  $ctrl.ok = function () {
    var webview;

    if (this.validateTogo() === true) {
      // $uibModalInstance.close $ctrl.view.models
      webview = {
        id: $ctrl.view.models.id,
        name: $ctrl.view.models.name,
        description: $ctrl.view.models.description,
        id_source: $ctrl.view.models.source.id
      };
      spiderdocsService.SaveCustomView(webview).then(function (_result) {
        return $uibModalInstance.close(_result);
      });
    }
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };

  this.manageNewSource = function () {
    $ctrl.view.managingDataLocation = !$ctrl.view.managingDataLocation;
    $ctrl.view.text.manageDataLocation = $ctrl.view.managingDataLocation === true ? 'Close' : 'Manage Data Location';
  };

  this.updateSource = function (source) {
    if ($ctrl.data.pssource != null) {
      $timeout.cancel($ctrl.data.pssource);
    }

    return $ctrl.data.pssource = $timeout(function () {
      var dbsource;
      dbsource = {
        name: '',
        data_source: '',
        sql_mode: null
      };

      if (source != null) {
        dbsource = angular.extend({}, source);
        dbsource.sql_mode = source.vendor.id;
      }

      return spiderdocsService.SaveCustomViewSourceAsync(dbsource).then(function (sources) {
        if (source == null) {
          // 	found = s for s in $ctrl.view.data.sources when s.id is source.id
          // 	angular.extend(found, source)
          // 	# .push sources[sources.length - 1];
          // else
          $ctrl.view.data.sources.push(sources[sources.length - 1]);
        }

        return $ctrl.data.pssource = null;
      });
    }, 400);
  };

  this.deleteSource = function (source) {
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
          return 'WARNING';
        },
        message: function message() {
          return "Are you sure to delete this source ? ( ".concat(source.name, " )");
        }
      }
    });
    modalInstance.result.then(function (result) {
      if (result === 'ok') {
        return spiderdocsService.DeleteCustomViewSourceAsync({
          id: source.id
        }).then(function (ans) {
          var _source;

          if ((ans != null ? ans.id : void 0) === 0) {
            $ctrl.view.data.sources = function () {
              var i, len, ref, results;
              ref = $ctrl.view.data.sources;
              results = [];

              for (i = 0, len = ref.length; i < len; i++) {
                _source = ref[i];

                if (_source.id !== source.id) {
                  results.push(_source);
                }
              }

              return results;
            }();

            Notify("'".concat(source.name, "' has been deleted!"), null, null, 'success');
          } else {
            Notify("Rejected. You might not have right permission to do that ( ".concat(source.name, " )."), null, null, 'danger');
          }
        });
      } else {}
    }, function () {});
  }; // this.validateTogo()
  // $log.info('Modal dismissed at: ' + new Date());


  this.vendorBy = function (id) {
    var i, len, v, vendor;

    for (i = 0, len = vendors.length; i < len; i++) {
      v = vendors[i];

      if (v.id === id) {
        vendor = v;
      }
    }

    return vendor;
  };

  $ctrl.canGo = false;
  vendors = [{
    id: 0,
    name: 'SQL Server'
  }, {
    id: 1,
    name: 'My SQL'
  }];
  $ctrl.view = {
    managingDataLocation: false,
    titlePrefix: 'New',
    data: {
      sources: sources,
      //[{name:'Local',val:1},{name:'DB',val:2}]
      // dbsources: [{name:'Azure',val:1},{name:'Amazon',val:2}]
      customviews: customviews,
      vendors: vendors
    },
    models: {
      id: (customview != null ? (ref = customview.ref) != null ? ref.id : void 0 : void 0) || 0,
      name: (customview != null ? (ref1 = customview.ref) != null ? ref1.name : void 0 : void 0) || '',
      description: (customview != null ? (ref2 = customview.ref) != null ? ref2.description : void 0 : void 0) || ''
    },
    // vendors: this.vendorBy( customview?.ref?.sql_mode or 0 )
    text: {
      manageDataLocation: 'Manage Data Location'
    }
  };
  $ctrl.data = {
    pssource: void 0
  };
  init();
}]);
app.filter('datasourceFilter', function () {
  return function (sources) {
    var i, len, results, source;
    results = [];

    for (i = 0, len = sources.length; i < len; i++) {
      source = sources[i];

      if (source.id > 1) {
        results.push(source);
      }
    }

    return results;
  };
});
//# sourceMappingURL=../../../maps/app/controllers/forms/form-new-view-controller.js.map
