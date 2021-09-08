"use strict";

var format;
app.controller('workspaceLocalController', ["$scope", "$q", "$interval", "spiderdocsService", "comService", "$window", "$sessionStorage", "_", "moment", "$http", "$uibModal", function ($scope, $q, $interval, spiderdocsService, comService, $window, $sessionStorage, _, moment, $http, $uibModal) {
  var applySortBy, canRename, checkin4ver, convWfile2Doc, drop, events, init, queryDocs, reload, saveAsNew;

  init = function init() {
    events();
    $interval(reload, 1000 * 10);
    return queryDocs().then(function (res) {
      format(res);
      $scope.data.docs = res;
    }).then(function () {
      comService.loading(false);
    });
  };

  canRename = function canRename(arg, scope) {
    var wfile;
    wfile = scope.doc;
    return wfile.id_version === 0;
  };

  checkin4ver = function checkin4ver(doc, settings) {
    var modalInstance;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-reason.html',
      controller: 'formReasonCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        title: function title() {
          return '[Save New Version]: ' + doc.filename;
        },
        message: function message() {
          return 'ID: ' + doc.id_doc + ',     Version: ' + (doc.version + 1);
        },
        reasonRequired: function reasonRequired() {
          return settings.public.reasonNewVersion;
        },
        reasonMinLen: function reasonMinLen() {
          if (settings.public.reasonNewVersion) {
            return 10;
          } else {
            return 0;
          }
        }
      }
    });
    modalInstance.result.then(function (reason) {
      comService.loading(true);
      spiderdocsService.CheckInAsNewVerAsync(doc.id, reason).then(function (_verDoc) {
        if (_verDoc.id === doc.id) {
          $scope.data.docs = $scope.data.docs.filter(function (x) {
            return x.id !== doc.id;
          });
          Notify('The file has been checked in. ', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    }, function () {});
  };

  saveAsNew = function saveAsNew(wdocs, settings) {
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
          return convWfile2Doc(wdocs);
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
    modalInstance.result.then(function (data) {
      var func, options, promise, reason, wfiles;
      wfiles = data.docs;
      func = data.func;
      reason = data != null ? data.reason : void 0;
      options = data != null ? data.options : void 0;
      promise = {};
      comService.loading(true);
      Notify('The file has started to checkin.It might take little bit longer...', null, null, 'info');

      if (func === 'new') {
        promise = $q.all(wfiles.map(function (doc) {
          return spiderdocsService.SaveWorkSpaceFileToDbAsNewAsync(doc.id_user_workspace, doc, options);
        }));
      } else if (func === 'ver') {
        promise = $q.all(wfiles.map(function (doc) {
          return spiderdocsService.SaveWorkSpaceFileToDbAsVerAsync(doc.id_user_workspace, doc, reason, options);
        }));
      }

      promise.then(function (res) {
        var docs, ids;
        docs = res;

        if (docs.filter(function (x) {
          return x.id > 0;
        }).length === wfiles.length) {
          ids = wfiles.map(function (x) {
            return x.id_user_workspace;
          });
          $scope.data.docs = $scope.data.docs.filter(function (x) {
            return !ids.includes(x.id);
          });
          Notify('The file has been checked in. ', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    }, function () {});
  };

  convWfile2Doc = function convWfile2Doc(_docs) {
    var docs;
    docs = [];
    angular.copy(_docs, docs); // transfer property values

    docs.forEach(function (x) {
      x.id_user_workspace = x.id;
    });
    docs.forEach(function (x) {
      x.id = 0;
    });
    docs.forEach(function (x) {
      x.title = x.filename;
    });
    docs.forEach(function (x) {
      x.extension = x.filename.split('.').length > 1 ? x.filename.substring(x.filename.lastIndexOf('.'), x.filename.length) : '';
    });
    docs.forEach(function (x) {
      x.id_latest_version = x.id_version;
    }); // delete not document object properties

    docs.forEach(function (x) {
      delete x.id_doc;
    });
    docs.forEach(function (x) {
      delete x.filename;
    });
    docs.forEach(function (x) {
      delete x.filedata;
    });
    docs.forEach(function (x) {
      delete x.filehash;
    });
    docs.forEach(function (x) {
      delete x.created_date;
    });
    docs.forEach(function (x) {
      delete x.lastaccess_date;
    });
    docs.forEach(function (x) {
      delete x.lastmodified_date;
    });
    docs.forEach(function (x) {
      delete x.id_user;
    });
    return docs;
  };

  events = function events() {
    var leaves;

    leaves = function leaves() {
      clearTimeout($scope.pid.dragfile);
      $scope.pid.dragfile = setTimeout(function () {
        angular.element('#upload1').addClass('hidden');
      }, 100);
    };

    angular.element('html').on('dragover', function (e) {
      //angular.element('#upload1').removeClass('hidden');
      //console.log('dragover');
      leaves();
      e.preventDefault();
      e.stopPropagation();
    });
    angular.element('body .screen').on('mousedown', function (e) {
      e.preventDefault();
      e.stopPropagation();
    });
    angular.element('html').on('dragenter', function (e) {
      //console.log('ondragenter');
      angular.element('#upload1').removeClass('hidden');
      e.preventDefault();
      e.stopPropagation();
    });
    angular.element('#upload1').on('drop', drop);
  };

  drop = function drop(e) {
    var files;
    e.preventDefault();
    e.stopPropagation();

    if (e.originalEvent.dataTransfer) {
      files = e.originalEvent.dataTransfer.files;

      if (files.length > 0) {
        comService.loading();
        $q.all(_.values(files).map(function (file) {
          return spiderdocsService.SaveFileToUserWorkSpace(file);
        })).then(queryDocs).then(function (docs) {
          format(docs);
          $scope.data.docs = docs;
          Notify('File(s) have/has been saved into SpiderDocs', null, null, 'success');
          comService.loading(false);
          angular.element('#upload1').addClass('hidden');
        }).catch(function () {
          comService.loading(false);
          angular.element('#upload1').addClass('hidden');
        });
      }
    }
  };

  reload = function reload() {
    queryDocs().then(function (docs) {
      format(docs);
      $scope.data.docs = docs;
    });
  };

  $scope.data = {
    docs: []
  };
  $scope.config = {};
  $scope.pid = {
    populateattr: 0,
    dragfile: 0
  };
  $scope.sStorage = $sessionStorage; //Menu Items Array

  $scope.menus = [{
    label: 'CheckIn',
    action: 'checkin',
    active: true
  }, {
    label: 'Send by e-mail',
    action: '',
    active: true,
    subItems: [{
      label: 'Original',
      action: 'emailOriginal',
      active: true
    }]
  }, {
    label: 'Export',
    action: 'download',
    active: true
  }, {
    label: 'Import',
    action: 'import',
    active: true
  }, {
    label: 'Rename',
    action: 'rename',
    active: true
  }, {
    label: '-------------------',
    action: '',
    active: false
  }, {
    label: 'Delete',
    action: 'delete',
    active: true
  }];
  $scope.sortby = {
    key: 'filename',
    asc: true
  };
  $scope.dbmenus = [{
    p: './index',
    n: 'Local Database'
  }, {
    p: './local',
    n: 'User Database'
  }];

  $scope.sort = function (column) {
    var columns; // #// reset icon beside column

    columns = ['js--header__id_doc', 'js--header__filename', 'js--header__filesizeText', 'js--header__lastmodified_date', 'js--header__created-date'];
    $scope.sortby.asc = $scope.sortby.key !== column ? false : $scope.sortby.asc;
    return $scope.sortBy(column).then(function (docs) {
      var ref; // #// set icon

      return angular.element('.js--header__' + column).addClass((ref = $scope.sortby.asc) != null ? ref : {
        'btn-sort--asc': 'btn-sort--desc'
      });
    });
  };

  $scope.sortBy = function (key) {
    $scope.sortby.key = key;
    $scope.sortby.asc = !$scope.sortby.asc;
    return reload();
  };

  $scope.delete = function (arg, scope) {
    var doc;
    doc = scope.doc;
    comService.loading(true);
    spiderdocsService.DeleteUserWorkSpaceFile([doc.id], 'Deleted by webapp').then(function () {
      Notify('The document has been deleted', null, null, 'success'); //comService.loading(false);
    }).then(queryDocs).then(function (docs) {
      format(docs);
      $scope.data.docs = docs; //Notify("File(s) have/has been saved into SpiderDocs", null, null, 'success');

      comService.loading(false);
    }).catch(function () {
      comService.loading(false);
    });
  };

  $scope.download = function (arg, scope) {
    var doc, url;
    doc = scope.doc;
    comService.loading(true);
    url = '';
    spiderdocsService.GetDonloadUrls4UserWorkSpace(doc.id).then(function (urls) {
      if (urls.length === 0) {
        alert('File could not exported.');
        return;
      }

      url = urls.pop();
      return comService.downloadBy(url); //return $http({url:url,method: 'Get', data:{},responseType:'blob'})
    }).then(function () {
      comService.loading(false);
    }).catch(function () {
      alert('Comming soon');
      comService.loading(false);
    });
  };

  $scope.import = function (arg, scope) {
    var _doc, modalInstance;

    _doc = scope.doc;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-import.html',
      controller: 'formImportCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        doc: function doc() {
          _doc.title = _doc.filename;
          _doc.extension = '.' + _doc.title.split('.').pop();
          return _doc;
        },
        reasonNewVersion: function reasonNewVersion() {
          return false;
        }
      }
    });
    modalInstance.result.then(function (result) {
      var file;
      file = result[0];
      comService.loading();
      Notify('It might be take long time.', null, null, 'info');
      spiderdocsService.ImportUserWorkSpaceFile(_doc.id, file).then(function (_verDoc) {
        var origindoc;

        if (_verDoc.id === _doc.id) {
          origindoc = $scope.data.docs.find(function (x) {
            return x.id === _doc.id;
          });
          angular.extend(origindoc, _verDoc);
          Notify('The file has been upladed. ', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    }, function () {});
  };

  $scope.checkin = function (arg, scope) {
    var doc;
    doc = scope.doc;
    spiderdocsService.cache.settings().then(function (settings) {
      if (doc.id_version > 0) {
        checkin4ver(doc, settings);
      } else {
        saveAsNew([doc], settings);
      }
    });
  };

  $scope.rename = function (arg, scope) {
    var file, modalInstance, oldName, wfile;
    wfile = scope.doc;
    file = angular.extractPath(wfile.filename);
    oldName = wfile.filename;
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
          return '[Rename]';
        },
        message: function message() {
          return wfile.filename + ' will be renamed with following';
        },
        defInput: function defInput() {
          return file.nameWithoutExt;
        },
        reasonMinLen: function reasonMinLen() {
          return 1;
        },
        needsInput: function needsInput() {
          return true;
        },
        inputType: function inputType() {
          return 'text';
        }
      }
    });
    modalInstance.result.then(function (newname) {
      var new_fullname;
      new_fullname = newname + file.extension;

      if (oldName === new_fullname) {
        Notify('Cancelled. New file name is same as original. ', null, null, 'warning');
        return;
      }

      comService.loading(true);
      spiderdocsService.RenameUserWorkSpaceFileAsync(wfile.id, new_fullname).then(function (updated) {
        if (updated.id === wfile.id) {
          $scope.data.docs.find(function (x) {
            return x.id === wfile.id;
          }).filename = new_fullname;
          Notify('The file has been renamed. \'' + oldName + '\' -> \'' + new_fullname + '\'', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    }, function () {});
  };

  $scope.emailOriginal = function (arg, scope) {
    var doc, tempId;
    doc = scope.doc;
    tempId = '';
    spiderdocsService.SaveWorkSpaceFile2TempAsync(doc.id).then(function (_tempId) {
      return tempId = _tempId;
    });
    return spiderdocsService.cache.myprofile().then(function (myprofile) {
      var modalInstance;
      modalInstance = $uibModal.open({
        animation: true,
        ariaLabelledBy: 'modal-title',
        ariaDescribedBy: 'modal-body',
        templateUrl: 'scripts/app/views/form-email.html',
        controller: 'FormEmailCtrl',
        controllerAs: '$ctrl',
        backdrop: 'static',
        size: 'sm',
        resolve: {
          title: function title() {
            return 'Send as Original';
          },
          to: function to() {
            return myprofile.email;
          },
          cc: function cc() {
            return '';
          },
          bcc: function bcc() {
            return '';
          },
          subject: function subject() {
            return doc.filename;
          },
          body: function body() {
            return '';
          },
          attachments: function attachments() {
            var _attachments, _f;

            _attachments = [];
            _f = angular.extractPath(doc.filename);

            _attachments.push({
              filename: doc.filename,
              extension: _f.extension.substr(1)
            });

            return _attachments;
          }
        }
      });
      return modalInstance.result.then(function (email) {
        Notify("Sending an email. It might take long time...", null, null, 'info');
        return spiderdocsService.SendEmailAsync(tempId, email).then(function (error) {
          if (error === '') {
            return Notify("The file has sent to '" + email.to + "'", null, null, 'success');
          } else {
            return Notify("Rejected. You might not have right permission to do that.", null, null, 'danger');
          }
        });
      });
    });
  };

  queryDocs = function queryDocs() {
    return spiderdocsService.GetUserWorkspaceDocuments().then(applySortBy);
  };

  applySortBy = function applySortBy(docs) {
    return comService.sortBy(docs, $scope.sortby.key, $scope.sortby.asc);
  };

  init();
}]);

format = function format(docs) {
  var doc, i, len, size;

  for (i = 0, len = docs.length; i < len; i++) {
    doc = docs[i];
    size = parseInt(doc.filesize / 1024);
    doc.filesizeText = size < 1 ? '< 1 KB' : size + ' KB';
  }
};
//# sourceMappingURL=../../maps/app/controllers/workspace-local-controller.js.map
