"use strict";

app.service('modalService', ['$q', '$uibModal', '_', 'spiderdocsService', 'comService', function ($q, $uibModal, _, spiderdocsService, comService) {
  var pid_goto, pid_loading, that;
  that = this;
  pid_loading = 0;
  pid_goto = 0;

  this.input = function (_title, _message, _defInput) {
    var _reasonMinLen = arguments.length > 3 && arguments[3] !== undefined ? arguments[3] : 1;

    var _needsInput = arguments.length > 4 && arguments[4] !== undefined ? arguments[4] : true;

    var _inputType = arguments.length > 5 && arguments[5] !== undefined ? arguments[5] : 'text';

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
          return _title;
        },
        message: function message() {
          return _message;
        },
        defInput: function defInput() {
          return _defInput;
        },
        reasonMinLen: function reasonMinLen() {
          return _reasonMinLen;
        },
        needsInput: function needsInput() {
          return _needsInput;
        },
        inputType: function inputType() {
          return _inputType;
        }
      }
    });
    return modalInstance.result;
  };

  this.confirm = function (_title2, _message2) {
    var _options = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : {};

    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-confimation.html',
      controller: 'formConfimationCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        title: function title() {
          return _title2;
        },
        message: function message() {
          return _message2;
        },
        options: function options() {
          return _options;
        }
      }
    });
    return modalInstance.result;
  };

  this.openfolder = function (_folders, _permission) {
    var _menu = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : {};

    var _canCreate = arguments.length > 3 && arguments[3] !== undefined ? arguments[3] : false;

    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-folder-tree.html',
      controller: 'formFolderTreeCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'lg',
      resolve: {
        folders: function folders() {
          return _folders;
        },
        permission: function permission() {
          return _permission;
        },
        menu: function menu() {
          return _menu;
        },
        canCreate: function canCreate() {
          return _canCreate;
        }
      }
    });
    return modalInstance.result;
  };

  this.searchDocument = function (doc, _folders2, types, _permission2) {
    var options = arguments.length > 4 && arguments[4] !== undefined ? arguments[4] : {};
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-search-documents.html',
      controller: 'formSearchDocumentsCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'lg',
      resolve: {
        extension: function extension() {
          if (options.saveAsPDF) {
            return '.pdf';
          } else {
            return doc.extension;
          }
        },
        folders: function folders() {
          return comService.nodesBy(0, _folders2);
        },
        docTypes: function docTypes() {
          var _types;

          _types = [];
          angular.copy(types, _types);
          return _types;
        },
        permission: function permission() {
          return _permission2;
        }
      }
    });
    return modalInstance.result;
  };

  this.saveAsNew = function (_docs, settings) {
    return spiderdocsService.cache.settings().then(function (settings) {
      var modalInstance;
      modalInstance = $uibModal.open({
        animation: true,
        ariaLabelledBy: 'modal-title',
        ariaDescribedBy: 'modal-body',
        templateUrl: 'scripts/app/views/form-save-new.html',
        controller: 'formSaveNewCtrl',
        controllerAs: '$ctrl',
        backdrop: 'static',
        size: 'lg',
        resolve: {
          docs: function docs() {
            return _docs;
          },
          title: function title() {
            return '[Save New File]';
          },
          docTypes: function docTypes() {
            return spiderdocsService.cache.doctypes();
          },
          settings: function settings() {
            return spiderdocsService.cache.settings();
          },
          notificationgroups: function notificationgroups() {
            return spiderdocsService.cache.notificationgroups().then(function (source) {
              return source.map(function (_source) {
                return {
                  id: _source.id,
                  text: _source.group_name,
                  checked: false
                };
              });
            });
          }
        }
      });
      return modalInstance.result;
    }).then(function (data) {
      var func; // wfiles = data.docs

      func = data.func; // reason = data?.reason
      // options = data?.options
      // promise = {}
      // comService.loading true
      // Notify 'The file has started to checkin.It might take little bit longer...', null, null, 'info'

      if (func === 'new') {
        console.log('new was selected'); // promise = $q.all(wfiles.map((doc) ->
        // 	spiderdocsService.SaveWorkSpaceFileToDbAsNewAsync doc.id_user_workspace, doc, options
        // ))
      } else if (func === 'ver') {
        console.log('ver was selected');
      } // promise = $q.all(wfiles.map((doc) ->
      // 	spiderdocsService.SaveWorkSpaceFileToDbAsVerAsync doc.id_user_workspace, doc, reason, options
      // ))


      return data; // promise.then (res) ->
      // 	docs = res
      // 	if docs.filter((x) ->
      // 		x.id > 0
      // 		)).length == wfiles.length
      // 		ids = wfiles.map (x) ->
      // 			x.id_user_workspace
      // 		$scope.data.docs = $scope.data.docs.filter (x) ->
      // 			!ids.includes(x.id)
      // 		Notify 'The file has been checked in. ', null, null, 'success'
      // 	else
      // 		Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'
      // 	comService.loading false
      // 	return
    }).catch(function () {
      comService.loading(false);
    });
  };
}]);
//# sourceMappingURL=../../maps/app/services/modalService.js.map
