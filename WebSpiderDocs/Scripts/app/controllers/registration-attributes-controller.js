"use strict";

app.controller('registrationAttributesController', ['$scope', '$q', 'spiderdocsService', 'comService', '$uibModal', function ($scope, $q, spiderdocsService, comService, $uibModal) {
  var init, save;
  $scope.data = {
    attributes: [],
    types: []
  };
  $scope.model = {
    type: {}
  };

  init = function init() {
    var attrs, types;
    types = [];
    attrs = [];
    spiderdocsService.cache.clear('db-attrs');
    return $q.all([spiderdocsService.cache.fieldtypes(), spiderdocsService.cache.attrs()]).then(function (res) {
      var a, folderIds, i, j, k, len, len1, len2, opt, p, ref;
      types = res[0];
      attrs = res[1];

      for (i = 0, len = attrs.length; i < len; i++) {
        opt = attrs[i];
        opt.required_bool = opt.required === 1;
        opt.only_number_bool = opt.only_numbers === 1;
      }

      folderIds = [];

      for (j = 0, len1 = attrs.length; j < len1; j++) {
        a = attrs[j];
        ref = a.parameters;

        for (k = 0, len2 = ref.length; k < len2; k++) {
          p = ref[k];

          if (parseInt(p.id_folder) > 0) {
            folderIds.push(p.id_folder);
          }
        }
      }

      return spiderdocsService.SearchFoldersAsync(folderIds);
    }).then(function (folders) {
      var a, i, len;
      $scope.folders = folders;

      for (i = 0, len = attrs.length; i < len; i++) {
        a = attrs[i];
        a.mandatoryList = $scope.mandatoryList(a);
      }

      $scope.data.types = types;
      return $scope.data.attributes = attrs;
    }).then(function () {
      comService.loading(false);
    });
  };

  $scope.mandatoryList = function (attribute) {
    var ans, found, i, len, p, ref;
    ans = []; // ids = (p.id_folder for p in attribute.parameters)

    if (attribute.id === 83) {
      debugger;
    }

    ref = attribute.parameters;

    for (i = 0, len = ref.length; i < len; i++) {
      p = ref[i];
      found = $scope.folders.find(function (f) {
        return f.id === parseInt(p.id_folder);
      });

      if (found != null) {
        ans.push({
          name_folder: found.document_folder,
          required: p.required === 1 ? true : false
        });
      }
    } //ans


    console.log(ans);
    return ans;
  };

  $scope.find = function (db, key, dbkey) {
    var ans, i, j, len, len1, opt;

    if (dbkey != null) {
      for (i = 0, len = db.length; i < len; i++) {
        opt = db[i];

        if (opt[dbkey] === key) {
          ans = opt;
        }
      }
    } else {
      for (j = 0, len1 = db.length; j < len1; j++) {
        opt = db[j];

        if (opt.id === key) {
          ans = opt;
        }
      }
    }

    return ans;
  };

  $scope.edit = function (attribute) {
    save('edit', attribute);
  };

  $scope.new = function () {
    return save('new');
  };

  save = function save(mode, attr) {
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-save-attributes.html',
      controller: 'formSaveAttributesCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        attribute: function attribute() {
          return attr;
        },
        types: spiderdocsService.cache.fieldtypes()
      }
    }); // permissionLevels: ()->
    // 	spiderdocsService.GetPermissionLevels()

    modalInstance.result.then(function (data) {
      var attribute, ref, ref1, ref2, ref3, ref4;
      comService.loading(true);
      attribute = {
        id: (ref = (ref1 = data.attribute) != null ? ref1.id : void 0) != null ? ref : 0,
        name: data.attribute.name,
        id_type: data.attribute.type.id,
        max_lengh: data.attribute.max_lengh,
        only_numbers: data.attribute.only_numbers === 1 || data.attribute.only_numbers === true ? 1 : 0,
        // required : if data.attribute.required is 1 or data.attribute.required is true then 1 else 0
        period_research: (ref2 = data.attribute.period_research) != null ? ref2 : 90,
        appear_query: (ref3 = data.attribute.appear_query) != null ? ref3 : true,
        appear_input: (ref4 = data.attribute.appear_input) != null ? ref4 : true,
        parameters: data.attribute.parameters
      };
      spiderdocsService.SaveAttributeAsync(attribute).then(function (_attribute) {
        var found;
        found = $scope.data.attributes.find(function (u) {
          return u.id === _attribute.id;
        });

        if (found != null) {
          angular.extend(found, _attribute);
        } else {
          $scope.data.attributes.push(_attribute);
        }

        if (_attribute.id > 0) {
          Notify('A attribute has been added', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        return comService.loading(false);
      });
    }, function () {});
  };

  return init();
}]);
//# sourceMappingURL=../../maps/app/controllers/registration-attributes-controller.js.map
