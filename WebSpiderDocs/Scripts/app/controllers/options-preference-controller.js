"use strict";

app.controller('OptionsPreferenceController', ['$scope', '$q', 'spiderdocsService', 'comService', 'en_DoubleClickBehavior', 'modalService', function ($scope, $q, spiderDocsSrv, comService, en_DoubleClickBehavior, modalService) {
  var convertDateForView, init; // $scope.data =
  // 	permissionTitles: []
  // 	types: []
  // 	attrs: []
  // 	map: []
  // 	# allowed: []
  // 	users: []
  // 	menu:{ items:[], func:{} }

  $scope.data = {};
  $scope.view = {
    models: {}
  };

  init = function init() {
    return $q.all([spiderDocsSrv.GetPreferenceAsync()]).then(function (response) {
      var preference;
      preference = response[0];
      preference = convertDateForView(preference);
      angular.extend($scope.view.models, preference);
    }).then(function () {
      comService.loading(false);
    });
  };

  $scope.savePreference = function () {
    return spiderDocsSrv.SavePreferenceAsync($scope.view.models).then(function (success) {
      if (success === true) {
        return Notify('The change applied.', null, null, 'success');
      } else {
        return Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
      }
    });
  };

  $scope.enabledMultiAddress = function () {
    return $scope.view.models.feature_multiaddress;
  };

  convertDateForView = function convertDateForView(preference) {
    preference.dblClickBehavior = preference.dblClickBehavior.toString();
    return preference;
  };

  return init();
}]);
//# sourceMappingURL=../../maps/app/controllers/options-preference-controller.js.map
