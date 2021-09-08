"use strict";

app.controller('workspaceController', ['$scope', '$timeout', '$q', '$interval', 'spiderdocsService', 'comService', '$window', '$sessionStorage', '_', 'moment', '$http', '$uibModal', 'en_file_Status', '$localStorage', 'en_Actions', 'SidebarJS', 'en_Events', 'en_file_Sp_Status', 'en_ReviewAction', 'modalService', 'en_DoubleClickBehavior', function ($scope, $timeout, $q, $interval, spiderdocsService, comService, $window, $sessionStorage, _, moment, $http, $uibModal, en_file_Status, $localStorage, en_Actions, SidebarJS, en_Events, en_file_Sp_Status, en_ReviewAction, modalService, en_DoubleClickBehavior) {
  var afterLoad, applyLimitOffseet, applySortBy, canArchive, canCheckout, canDelete, canDiscard, canExportPDF, canImport, canRename, changeattrs, checkined, checkouted, drop, getCombo, getMaster, getMasterDepend, getRecentDocuments, idcustomview, ids4CustomViews, init, noop, pagenation_max_page, pages, parseAttributeCriterias, perItems, queryDocs, send, setDocs, setupDbViews, setupImportByDrag, storeMaxLength;
  $scope.incDbmenuCtrl = {};

  init = function init() {
    setupImportByDrag();
    return getMaster().then(getMasterDepend).then(function (res) {
      //$scope.data.docs = _.cloneDeep(res[0]);3 
      $scope.data.docTypes = _.cloneDeep(res[0]);
      $scope.data.attrs = _.cloneDeep(res[1]);
      $scope.data.comboitems = _.cloneDeep(res[2]);
      $scope.sStorage.docTypes = _.cloneDeep($scope.data.docTypes);
      $scope.sStorage.attrs = _.cloneDeep($scope.data.attrs);
      $scope.sStorage.comboitems = _.cloneDeep($scope.data.comboitems);
      $scope.data.customviews = _.cloneDeep(res[3]);
      setupDbViews($scope.data.customviews);
      return $scope.data.docTypes.map(function (type) {
        type.text = type.type;
        return type;
      });
    }).then(queryDocs).then(function () {
      // $scope.data.docs = _.cloneDeep(docs);
      $scope.showPageName();
      return pages(1);
    }).then(function () {
      comService.loading(false);
    }).then(afterLoad);
  };

  $scope.statusColour = function (doc) {
    var ans, suffix;
    ans = 'doc-status';

    if ($scope.isInReview(doc)) {
      // ordered by low priority
      suffix = 'in-review';
    }

    if ($scope.isOverdueReview(doc)) {
      suffix = 'overdue-review';
    }

    if ($scope.isCheckedout(doc)) {
      suffix = 'checkedout';
    }

    if ($scope.archived(doc)) {
      suffix = 'archived';
    }

    return "".concat(ans, "--").concat(suffix);
  };

  send = function send(email, tempId) {
    Notify('Sending an email. It might take long time...', null, null, 'info');
    spiderdocsService.SendEmailAsync(tempId, email).then(function (error) {
      if (error === '') {
        Notify('The file has sent to \'' + email.to + '\'', null, null, 'success');
      } else {
        Notify('Rejected. You might not have right permission to do that.', null, null, 'danger');
      }
    });
  };

  checkouted = function checkouted(doc) {
    if (typeof doc.id_status !== 'number') {
      $log.error('doc.id_status must be number');
      debugger;
    }

    return doc.id_status === en_file_Status.checked_out;
  };

  checkined = function checkined(doc) {
    return doc.id_status === en_file_Status.checked_in;
  };

  canDelete = function canDelete(arg, scope) {
    var doc;
    doc = scope.doc;
    return false === checkouted(doc);
  };

  canDiscard = function canDiscard(arg, scope) {
    var doc;
    doc = scope.doc;
    return checkouted(doc);
  };

  canArchive = function canArchive(arg, scope) {
    var doc;
    doc = scope.doc;
    return !checkouted(doc);
  };

  canImport = function canImport(arg, scope) {
    var doc;
    doc = scope.doc;
    return checkined(doc);
  };

  canCheckout = function canCheckout(arg, scope) {
    var doc;
    doc = scope.doc;
    return false === checkouted(doc) && doc.id_sp_status === en_file_Sp_Status.normal;
  };

  canRename = function canRename(arg, scope) {
    var doc;
    doc = scope.doc;
    return false === checkouted(doc);
  };

  canExportPDF = function canExportPDF(arg, scope) {
    var doc;
    doc = scope.doc;
    return comService.canExportExt(doc.extension);
  };

  getRecentDocuments = function getRecentDocuments() {
    return spiderdocsService.GetRecentDocuments().then(applySortBy).then(storeMaxLength).then(applyLimitOffseet).then(function (docs) {
      $scope.data.docs = docs;
      return docs;
    });
  };

  pagenation_max_page = function pagenation_max_page() {
    return Math.ceil($scope.doc_max_length / $scope.per_item.id);
  };

  pages = function pages(at) {
    var end, i, start;
    $scope.pages = [];
    start = at;
    end = start + $scope.pagenation_limit;

    if (end >= pagenation_max_page()) {
      end = pagenation_max_page() + 1;
      start = pagenation_max_page() - 5;
    }

    if (start < 1) {
      start = 1;
    }

    i = start;

    while (i < end) {
      $scope.pages.push(i);
      i++;
    }
  };

  queryDocs = function queryDocs() {
    var query;
    query = $scope.searchMode === 'search' || idcustomview() > 0 ? $scope.search() : getRecentDocuments();
    return query;
  }; //.then(storeMaxLength);


  changeattrs = function changeattrs() {
    var findAttr, generate, generateTage, registry_default_validation, registry_validationconf, typeids, valiconf, wrapInContainer;

    findAttr = function findAttr(id) {
      return $scope.data.attrs.find(function (a) {
        return a.id === id;
      }) || {};
    };

    generateTage = function generateTage(attr, config) {
      var $element, $wrap, db;
      $wrap = {};
      $element = {};

      switch (attr.id_type) {
        case 1:
          //1	Text Box
          $element = TgGn.Text(config);
          $wrap = wrapInContainer(attr.id_type, $element, config.id, attr.name);
          break;

        case 2:
          //2	Check Box
          $wrap = $('<div/>');
          break;
          $element = TgGn.CheckBox(config);
          $wrap = wrapInContainer(attr.id_type, $element, config.id, attr.name);
          break;

        case 3:
          //3	Date
          $element = TgGn.Date(config);
          $wrap = $element;
          break;
        //4	Combo Box
        //8	FixedCombo
        //10 ComboSingleSelect

        case 4:
        case 8:
        case 10:
        case 11:
          //11 FixedComboSingleSelect
          db = $scope.data.comboitems.filter(function (itm) {
            return itm.id_atb === attr.id;
          });
          $element = TgGn.Select({
            db: db,
            id: 'id',
            text: 'text',
            data: config.data
          });
          $element.attr('name', config.id);
          $wrap = wrapInContainer(attr.id_type, $element, config.id, attr.name);
          break;

        case 5:
        case 6:
        case 9:
          return;
      }

      return $wrap;
    };

    wrapInContainer = function wrapInContainer(id_type, $element, id, name) {
      var $cntinr, key2Append, wrapper;
      key2Append = '';
      $cntinr = '';
      wrapper = '';

      switch (id_type) {
        case 1:
          //1	Text Box
          wrapper = ['<div class="form-field ">', '<label for="{0}" class="form-field__label">{1}</label>', '<div class="js-key">', '</div>', '</div>'].join('').format(id, name);
          $cntinr = angular.element(wrapper);
          break;

        case 2:
          //2	Check Box
          wrapper = ['<div class="form-field form-field--checkbox">', '<label for="{0}" class="form-field__label" style="margin-top: 3.3rem;"><span>{1}</span></label>', '<div class="js-key">', '</div>', '</div>'].join('').format(id, name);
          $cntinr = angular.element(wrapper);
          break;

        case 3:
          //3	Date
          wrapper = ['<div class="form-field">', '<label for="{0}" class="form-field__label">{1}</label>', '<div class="form-field__date">', '<div class="js-key">', '</div>', '</div>', '</div>'].join('').format(id, name);
          $cntinr = angular.element(wrapper);
          break;
        //4	Combo Box
        //8	FixedCombo
        //10 ComboSingleSelect

        case 4:
        case 8:
        case 10:
        case 11:
          //11 FixedComboSingleSelect
          wrapper = ['<div class="form-field ">', '<label for="{0}" class="form-field__label">{1}</label>', '<div class="form-field__select form-field__select--blue js-key">', '</div>', '</div>'].join('').format(id, name);
          $cntinr = angular.element(wrapper);
          break;

        case 5:
        case 6:
        case 9:
          return;
      }

      $cntinr.find('.js-key').append($element);
      return $cntinr;
    };

    generate = function generate(attr_doctyp) {
      var $attr_palce, $cntinr, $element, attr, config, id; //var $tmplt = $('#template #field');

      $attr_palce = angular.element('.js-attribute-section');
      attr = findAttr(attr_doctyp.id_attribute);
      id = attr.name.replace(/\s/g, '');
      config = {
        id: id,
        data: {
          'id': attr.id,
          'id_type': attr.id_type
        }
      };
      $element = generateTage(attr, config);

      if (!$element) {
        return;
      }

      $cntinr = $element;

      if (attr.required === 1) {
        registry_validationconf(valiconf, config.id, attr.name, attr.id_type);
      }

      $attr_palce.append($cntinr);
    };

    registry_default_validation = function registry_default_validation(valiconf) {
      registry_validationconf(valiconf, 'folders', 'Folder', 4);
      registry_validationconf(valiconf, 'types', 'Document Type', 4);
      registry_validationconf(valiconf, 'path', '', -1);
    };

    registry_validationconf = function registry_validationconf(conf, id, name, type) {
      var msg;
      msg = 'Please Fill this field.';
      conf.rules[id] = {};
      conf.rules[id].required = true;
      conf.messages[id] = {};

      switch (type) {
        case 1:
          //1	Text Box
          msg = 'Please {0} {1}.'.format('enter', name);
          break;

        case 4:
          //1	Text Box
          msg = 'Please {0} {1}.'.format('enter', name);
          break;

        case -1:
          //1	Text Box
          msg = 'Please Take a photo.';
          break;

        default:
          msg = 'Please {0} {1}.'.format('select', name);
          break;
      }

      conf.messages[id].required = msg;
    };

    if (!$scope.data.docTypes[0]) {
      return;
    }

    $('.js-attribute-section').empty();
    typeids = $scope.data.docTypes.filter(function (x) {
      return x.checked;
    }).map(function (x) {
      return x.id;
    });
    valiconf = {
      rules: {},
      messages: {}
    };

    if (typeids.length === 0) {
      return;
    }

    return spiderdocsService.GetAttributeDocType(typeids).then(function (rslt) {
      var map;
      map = _.uniqBy(rslt.map(function (x) {
        return {
          id_attribute: x.id_attribute
        };
      }), 'id_attribute');
      registry_default_validation(valiconf);
      map.forEach(generate);
    });
  }; //deploy_validation(valiconf);


  parseAttributeCriterias = function parseAttributeCriterias() {
    var onoff2bit, values;
    values = angular.element('.js-attribute-section input, .js-attribute-section select').toArray().map(function (x) {
      var id, id_type, val;
      id = angular.element(x).attr('data-id');
      id_type = angular.element(x).attr('data-id_type');
      val = angular.element(x).val();
      return {
        Values: {
          id: id,
          id_type: id_type,
          atbValue: ['2'].includes(id_type) ? onoff2bit(val) : val
        }
      };
    });

    onoff2bit = function onoff2bit(val) {
      if (val === '1') {
        return 1;
      } else {
        return 0;
      }
    };

    return values.filter(function (x) {
      return x.Values.atbValue;
    });
  };

  perItems = function perItems(setting) {
    if (setting.id === '') {
      return;
    }

    return $scope.data.per_items.find(function (x) {
      return x.id === setting.per_items;
    });
  };

  storeMaxLength = function storeMaxLength(docs) {
    $scope.doc_max_length = docs.length;
    return docs;
  };

  applyLimitOffseet = function applyLimitOffseet(docs) {
    var curAt, num_per, start;
    docs = docs || [];
    curAt = $scope.cur_page - 1;
    num_per = $scope.per_item.id;
    start = curAt * num_per;

    if (docs.length < start + 1) {
      return docs.slice(start, docs.length);
    } else {
      return docs.slice(start, start + num_per);
    }
  };

  applySortBy = function applySortBy(docs) {
    return comService.sortBy(docs, $scope.sortby.key, $scope.sortby.asc);
  };

  setDocs = function setDocs(docs) {
    $scope.data.docs = docs;
  };

  getMaster = function getMaster()
  /*res*/
  {
    //noop(res)
    return $q.all([spiderdocsService.cache.doctypes(), spiderdocsService.cache.attrs()]);
  };

  getMasterDepend = function getMasterDepend(res) {
    return $q.all([noop(res[0]), noop(res[1]), // noop(res[2])
    $scope.sStorage.comboitems ? noop($scope.sStorage.comboitems) : getCombo(res[1]), spiderdocsService.GetCustomViewsAsync()]);
  };

  noop = function noop(data) {
    var deferred;
    deferred = $q.defer();
    setTimeout(function () {
      deferred.resolve(data);
    }, 1);
    return deferred.promise;
  };

  getCombo = function getCombo(attrs) {
    return $q.all(attrs.map(function (a) {
      return a.id;
    }).filter(function (a) {
      return a < 10000;
    }).map(function (id_attr) {
      return spiderdocsService.GetAttributeComboboxItems(id_attr);
    })).then(function (res) {
      var items;
      res = res || [];
      items = [];
      res.forEach(function (row) {
        if (row && row.length > 0) {
          row.forEach(function (x) {
            return items.push(x);
          });
        }
      });
      return items;
    });
  };

  afterLoad = function afterLoad() {
    $q.all([spiderdocsService.cache.settings(), spiderdocsService.cache.foldersL1(0, en_Actions.OpenRead)]).then(function (res) {
      var folders, settings;
      settings = res[0];
      folders = res[1];
      $scope.explorer.folders = comService.nodesBy(0, folders);
      $scope.lStorage.settings = _.cloneDeep(settings);
    });
  };

  $scope.data = {
    docs: [],
    per_items: [{
      id: 10,
      name: '10 p/page'
    }, {
      id: 15,
      name: '15 p/page'
    }, {
      id: 20,
      name: '20 p/page'
    }, {
      id: 50,
      name: '50 p/page'
    }, {
      id: 100,
      name: '100 p/page'
    }],
    attrs: []
  };
  $scope.search = {
    database_views: []
  };
  $scope.view = {
    pagetitle: ''
  };
  $scope.com = comService;
  $scope.explorer = {
    folders: [],
    folder_default: 0
  };
  $scope.cur_page = 1;
  $scope.pagenation_limit = 5;
  $scope.pages = [];
  $scope.total = 0;
  $scope.per_item = $scope.data.per_items[0];
  $scope.sortby = {
    key: 'id',
    asc: false
  };
  $scope.doc_max_length = 0;
  $scope.config = {};
  $scope.pid = {
    populateattr: 0,
    goto: 0
  };
  $scope.searchMode = 'recent'; // recent or search

  $scope.sStorage = $sessionStorage;
  $scope.lStorage = $localStorage;
  $scope.en_file_Status = en_file_Status; //Menu Items Array

  $scope.menus = [{
    label: 'Discard Checkout',
    action: 'discard',
    active: canDiscard
  }, {
    label: 'CheckOut',
    action: 'checkout',
    active: canCheckout
  }, {
    label: 'Import New Version',
    action: 'import',
    active: canImport
  }, {
    label: 'Export',
    // action: 'download'
    active: true,
    subItems: [{
      label: 'as User Workspace',
      action: 'checkout',
      active: true
    }, {
      label: 'as Copy of Original',
      action: 'download',
      active: true
    }, {
      label: 'as PDF',
      action: 'exportAsPDF',
      active: canExportPDF
    }]
  }, {
    label: 'Property',
    action: 'property',
    active: true
  }, {
    label: 'Send by e-mail',
    action: '',
    active: true,
    subItems: [{
      label: 'Original',
      action: 'emailOriginal',
      active: true
    }, {
      label: 'Document Link',
      action: 'emailDocumentLink',
      active: true
    }]
  }, {
    label: 'Review',
    action: 'review',
    active: true
  }, {
    label: 'Create Document Link',
    action: 'createDMS',
    active: true
  }, {
    label: 'Go to this folder',
    action: 'gotoFolder',
    active: true
  }, {
    label: 'Rename',
    action: 'rename',
    active: canRename
  }, {
    label: 'Details',
    action: 'details',
    active: true
  }, {
    label: '-------------------',
    action: '',
    active: false
  }, {
    label: 'Delete',
    action: 'delete',
    active: canDelete
  }, {
    label: 'Archive',
    action: 'archive',
    active: canArchive
  }];

  $scope.folderOpened = function () {
    //folderSidebarJS
    SidebarJS.toggle('menuSidebarJS');
  };

  $scope.openSearchFolderForm = function () {
    spiderdocsService.cache.foldersL1(0, en_Actions.OpenRead).then(function (folders) {
      var nodes;
      nodes = comService.nodesBy(0, folders);
      return modalService.openfolder(nodes, en_Actions.OpenRead);
    }).then(function (folder) {
      if (folder === void 0) {
        folder = {};
      }

      $scope.search.name_folder = folder.text;
      $scope.search.id_folder = folder.id;
    }, function () {});
  }; //$scope.property = function(doc)


  $scope.property = function (arg, scope) {
    var _doc, modalInstance;

    _doc = scope.doc;
    modalInstance = $uibModal.open({
      animation: true,
      windowClass: 'modal--property',
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-property.html',
      controller: 'FormPropertyCtrl',
      controllerAs: '$propCtrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        doc: function doc() {
          return _doc;
        },
        types: function types() {
          return $scope.sStorage.docTypes;
        },
        attrs: function attrs() {
          return $scope.sStorage.attrs;
        },
        comboitems: function comboitems() {
          return $scope.sStorage.comboitems;
        },
        notificationgroups: function notificationgroups() {
          var myGrp;
          myGrp = [];
          return spiderdocsService.GetNotificationGroupsAsync({
            DocumentId: _doc.id
          }).then(function (_myGrp) {
            myGrp = _myGrp.map(function (my) {
              return my.id;
            });
            return spiderdocsService.cache.notificationgroups();
          }).then(function (source) {
            return source.map(function (_source) {
              return {
                id: _source.id,
                text: _source.group_name,
                checked: myGrp.includes(_source.id)
              };
            });
          });
        },
        customviews: function customviews() {
          return spiderdocsService.GetCustomViewsAsync();
        },
        customviewdocuments: function customviewdocuments() {
          return spiderdocsService.GetCustomViewDocumentsAsync(_doc.id);
        }
      }
    });
    modalInstance.result.then(function (doc) {
      comService.loading(true);
      return spiderdocsService.UpdateProperty(doc).then(function (error) {
        var delall, j, len, ref, viewcod;

        if (error === '') {
          Notify('The document has been updated!', null, null, 'success');
          ref = doc.customviews;

          for (j = 0, len = ref.length; j < len; j++) {
            viewcod = ref[j]; // delall = viewcod for viewcod in doc.customviews when viewcod.checked is true

            delall = viewcod;
          }

          if (delall == null) {
            spiderdocsService.DeleteAllCustomViewDocumentAsync(doc.id);
            doc.customviews = [];
          } // $scope.search
          // 	DocIds: [doc.id]
          // .then (_docs) ->
          // 	origindoc = $scope.data.docs.find((x) ->
          // 		x.id == doc.id
          // 	)
          // 	angular.extend origindoc, _docs[0]
          // This does not necessary logic.


          $scope.searchDoc(doc.id, doc.customviews.length > 0 ? doc.customviews[0].id : void 0).then(function (_docs) {
            var origindoc;
            origindoc = $scope.data.docs.find(function (x) {
              return x.id === doc.id;
            });
            return angular.extend(origindoc, _docs[0]);
          });
        } else {
          Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
        }

        comService.loading(false);
      });
    }, function () {});
    modalInstance.rendered.then(function () {});
  };

  $scope.checkout = function (arg, scope) {
    var doc;
    doc = scope.doc;
    comService.loading(true);
    $scope.checkoutWithFooter(doc).then(function () {
      comService.loading(false);
    }).catch(function () {
      comService.loading(false);
    });
  };

  $scope.delete = function (arg, scope) {
    var _doc2, modalInstance;

    _doc2 = scope.doc;
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'delete-form.html',
      controller: 'deleteFormCtrl',
      controllerAs: '$propCtrl',
      backdrop: 'static',
      size: 'sm',
      resolve: {
        doc: function doc() {
          return _doc2;
        }
      }
    });
    modalInstance.result.then(function (reason) {
      spiderdocsService.Delete([_doc2.id], reason).then(function (ans) {
        if (true === angular.isString(ans) && angular.isEmpty(ans)) {
          $scope.data.docs = $scope.data.docs.filter(function (x) {
            return x.id !== _doc2.id;
          });
          Notify('The document has been deleted!', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that (' + _doc2.name_folder + ').', null, null, 'danger');
        }
      });
    }, function () {});
  }; // $log.info('Modal dismissed at: ' + new Date());


  $scope.discard = function (arg, scope) {
    var doc;
    doc = scope.doc;
    spiderdocsService.CancelCheckOut([doc.id]).then(function (err) {
      if (true === angular.isString(err) && angular.isEmpty(err)) {
        doc.id_status = en_file_Status.checked_in;
        Notify('The document has been discared checkout!', null, null, 'success');
      } else {
        Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
      }
    }).catch(function () {
      Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
    });
  };

  $scope.archive = function (arg, scope) {
    var doc, modalInstance;
    doc = scope.doc;
    modalInstance = modalService.confirm('Are you sure to archive this file (' + doc.title + ')?', 'The action cannot redo once archived the document.'); // modalInstance = $uibModal.open(
    // 	animation: true
    // 	ariaLabelledBy: 'modal-title'
    // 	ariaDescribedBy: 'modal-body'
    // 	templateUrl: 'form-confimation.html'
    // 	controller: 'formConfimationCtrl'
    // 	controllerAs: '$ctrl'
    // 	backdrop: 'static'
    // 	size: 'sm'
    // 	resolve:
    // 		title: ->
    // 			'Are you sure to archive this file (' + doc.title + ')?'
    // 		message: ->
    // 			'The action cannot redo once archived the document.'
    // )

    modalInstance.then(function (result) {
      if (result === 'ok') {
        return spiderdocsService.Archive([doc.id]).then(function (ans) {
          if (ans === '') {
            $scope.data.docs.find(function (x) {
              return x.id === doc.id;
            }).id_status = en_file_Status.archived;
            Notify('The document has been deleted!', null, null, 'success');
          } else {
            Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
          }
        });
      } else {
        Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
      }
    }, function () {});
  }; // $log.info('Modal dismissed at: ' + new Date());


  $scope.import = function (arg, scope) {
    var _doc3, modalInstance;

    _doc3 = scope.doc;
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
          return _doc3;
        },
        reasonNewVersion: function reasonNewVersion() {
          return $scope.lStorage.settings.public.reasonNewVersion;
        }
      }
    });
    modalInstance.result.then(function (result) {
      var file, reason;
      file = result[0];
      reason = result[1];
      comService.loading();
      spiderdocsService.SaveAsNewVer(_doc3.id, file, reason).then(function (_verDoc) {
        var origindoc;

        if (_verDoc.id === _doc3.id) {
          origindoc = $scope.data.docs.find(function (x) {
            return x.id === _doc3.id;
          });
          angular.extend(origindoc, _verDoc);
          Notify('The file has been upladed. ( v' + (_verDoc.version - 1) + ' -> v' + _verDoc.version + ')', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that (' + _doc3.name_folder + ').', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    }, function () {});
  };

  $scope.archived = function (doc) {
    return parseInt(doc.id_status) === en_file_Status.archived;
  };

  $scope.isInReview = function (doc) {
    return doc.id_sp_status === en_file_Sp_Status.review;
  };

  $scope.isOverdueReview = function (doc) {
    return doc.id_sp_status === en_file_Sp_Status.review_overdue;
  };

  $scope.isCheckedout = function (doc) {
    return doc.id_status === en_file_Status.checked_out;
  };

  $scope.gotoFolder = function (arg, scope) {
    var doc;
    comService.loading(true);
    doc = scope.doc; // var selectPath = [];

    spiderdocsService.DrillUpFoldersWithParentsFromAsync(doc.id_folder, en_Actions.OpenRead).then(function (folders) {
      $scope.explorer.folders = comService.nodesBy(0, folders, doc.id_folder);
      $scope.explorer.folder_default = doc.id_folder;
      comService.openFolder(doc.id_folder, folders, '[sidebarjs-name="folderSidebarJS"]');
      SidebarJS.toggle('folderSidebarJS');
      comService.loading(false);
    });
  };

  $scope.review = function (arg, scope) {
    var _checked, _doc4, modalInstance;

    _doc4 = scope.doc;
    _checked = '';
    modalInstance = $uibModal.open({
      animation: true,
      ariaLabelledBy: 'modal-title',
      ariaDescribedBy: 'modal-body',
      templateUrl: 'scripts/app/views/form-review.html',
      controller: 'formReviewCtrl',
      controllerAs: '$ctrl',
      backdrop: 'static',
      size: 'lg',
      resolve: {
        doc: function doc() {
          return _doc4;
        },
        currentReview: function currentReview() {
          return spiderdocsService.GetReviewAsync(_doc4.id);
        },
        isOwner: function isOwner() {
          return spiderdocsService.IsReviewOwnerAsync(_doc4.id);
        },
        permissions: function permissions() {
          return spiderdocsService.cache.myprofile().then(function (profile) {
            return spiderdocsService.GetFolderPermissionsAsync(_doc4.id_folder, profile.id);
          });
        },
        reviewHistory: function reviewHistory() {
          return spiderdocsService.GetReviewHistoryAsync(_doc4.id);
        },
        reviewUsers: function reviewUsers() {
          return spiderdocsService.cache.reviewers(_doc4.id).then(function (users) {
            var j, len, reviewUsers, reviewuser, user;
            reviewUsers = [];

            for (j = 0, len = users.length; j < len; j++) {
              user = users[j];
              reviewuser = {};
              reviewuser.id = user.id;
              reviewuser.text = user.name;
              reviewuser.checked = false;
              reviewUsers.push(reviewuser);
            }

            return reviewUsers;
          });
        },
        profile: function profile() {
          return spiderdocsService.cache.myprofile();
        },
        users: function users() {
          return spiderdocsService.cache.users();
        }
      }
    });
    modalInstance.result.then(function (data) {
      var response;
      comService.loading(true);
      _checked = data.checked;

      if (data.checked === 'start') {
        response = spiderdocsService.StartReviewAsync(_doc4.id, data.review.id_version, data.review.allow_checkout, data.review.id_users, data.review.deadline, data.review.owner_comment);
      } else if (data.checked === 'finish') {
        response = spiderdocsService.FinishReviewAsync(_doc4.id, data.review.id_version, data.review.owner_comment);
      }

      return response;
    }).then(function (_review) {
      var finalized, review;

      if (_review.id_doc === _doc4.id) {
        if (_checked === 'start') {
          Notify('The review has been started', null, null, 'success');
          $scope.data.docs.find(function (d) {
            return d.id === _doc4.id;
          }).id_sp_status = en_file_Sp_Status.review;
        } else if (_checked === 'finish') {
          Notify('The review has been finished', null, null, 'success');

          finalized = function () {
            var j, len, ref, results;
            ref = _review.review_users;
            results = [];

            for (j = 0, len = ref.length; j < len; j++) {
              review = ref[j];

              if (review.action === en_ReviewAction.Finalize) {
                results.push(review);
              }
            }

            return results;
          }();

          if (finalized.length === _review.review_users.length) {
            $scope.data.docs.find(function (d) {
              return d.id === _doc4.id;
            }).id_sp_status = en_file_Sp_Status.normal;
          }
        }
      } else {
        Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
      }

      comService.loading(false);
    }).catch(function () {
      comService.loading(false);
    });
    return;
  };

  $scope.createDMS = function (arg, scope) {
    var doc;
    doc = scope.doc;
    comService.loading(true); // Notify("Started downlaoding Document link file.", null, null, 'info');

    spiderdocsService.CreateDMSLink(doc.id).then(function (urls) {
      var url;

      if (urls.length > 0) {
        url = urls.pop();
        return comService.downloadBy(url);
      } else {
        Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
      }
    }).then(function () {
      Notify('Document link has been created. Check \'Download\' Folder.', null, null, 'success');
      comService.loading(false);
    }).catch(function () {
      alert('Comming soon');
      comService.loading(false);
    });
  };

  $scope.rename = function (arg, scope) {
    var doc, file, modalInstance, oldName;
    doc = scope.doc;
    file = void 0;
    modalInstance = void 0;
    oldName = void 0;
    file = angular.extractPath(doc.title);
    oldName = doc.title;
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
          return doc.title + ' will be renamed with following';
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
      new_fullname = void 0;
      new_fullname = newname + file.extension;

      if (oldName === new_fullname) {
        Notify('Cancelled. New file name is same as original. ', null, null, 'warning');
        return;
      }

      comService.loading(true);
      spiderdocsService.RenameDbFileAsync(doc.id, new_fullname).then(function (updated) {
        if (updated.id === doc.id) {
          $scope.data.docs.find(function (x) {
            return x.id === doc.id;
          }).title = new_fullname;
          Notify('The file has been renamed. \'' + oldName + '\' -> \'' + new_fullname + '\'', null, null, 'success');
        } else {
          Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    });
  };

  $scope.emailOriginal = function (arg, scope) {
    var doc, tempId;
    doc = scope.doc;
    tempId = '';
    spiderdocsService.SaveFile2TempAsync(doc.id, doc.id_latest_version).then(function (_tempId) {
      tempId = _tempId;
    });
    spiderdocsService.cache.myprofile().then(function (myprofile) {
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
            return doc.title;
          },
          body: function body() {
            return '';
          },
          attachments: function attachments() {
            var _attachments;

            _attachments = [];

            _attachments.push({
              filename: doc.title,
              extension: doc.extension.substr(1)
            });

            return _attachments;
          }
        }
      });
      modalInstance.result.then(function (email) {
        send(email, tempId);
      });
    });
  };

  $scope.emailDocumentLink = function (arg, scope) {
    var doc, tempId;
    doc = scope.doc;
    tempId = '';
    spiderdocsService.SaveDMS2TempAsync(doc.id).then(function (_tempId) {
      tempId = _tempId;
    });
    spiderdocsService.cache.myprofile().then(function (myprofile) {
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
            return 'Send as Document Link';
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
            var path;
            path = angular.extractPath(doc.title);
            return path.nameWithoutExt + '.dms';
          },
          body: function body() {
            return '';
          },
          attachments: function attachments() {
            var _attachments;

            _attachments = [];

            _attachments.push({
              filename: doc.title,
              extension: 'dms'
            });

            return _attachments;
          }
        }
      });
      modalInstance.result.then(function (email) {
        send(email, tempId);
      });
    });
  };

  $scope.prev = function () {
    if ($scope.cur_page <= 1) {
      $scope.cur_page = 1;
      return;
    } //$scope.per_item = 10;


    $scope.cur_page -= 1;
    comService.loading(true);
    return queryDocs().then(function () {
      return pages($scope.cur_page);
    }).then(function () {
      return comService.loading(false);
    });
  };

  $scope.page_at = function (page) {
    comService.loading(true);
    $scope.cur_page = page;
    return queryDocs().then(function () {
      return pages($scope.cur_page);
    }).then(function () {
      return comService.loading(false);
    });
  };

  $scope.next = function () {
    var max;
    max = pagenation_max_page();

    if ($scope.cur_page >= max) {
      pages($scope.cur_page = max);
      return;
    }

    $scope.cur_page += 1;
    comService.loading(true);
    return queryDocs().then(function () {
      return pages($scope.cur_page);
    }).then(function () {
      return comService.loading(false);
    });
  };

  $scope.change_per_item = function () {
    return queryDocs();
  };

  $scope.clearSearchCriteria = function () {
    angular.element('input, textarea').val('');
    $('[id^="customDDL-all"]').prop('checked', false);
    setupDbViews($scope.data.customviews); // for v in search.database_views
    // 	v.checked = false

    angular.element('input, textarea').trigger('change');
    $scope.data.docTypes.forEach(function (x) {
      return x.checked = false;
    });
    $scope.search.created_from = $scope.search.created_to = void 0;
    $scope.search.id_folder = '';
    $scope.search.name_folder = '';
    comService.loading(true);
    $scope.cur_page = 1;
    $scope.searchMode = 'recent';
    queryDocs().then(function () {
      pages(1);
      return changeattrs();
    }).then(function () {
      comService.loading(false);
    });
  };

  $scope.searchByDMS = function () {
    var modalInstance;
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
          return {
            title: 'Search by DMS',
            extension: '.dms'
          };
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
      spiderdocsService.SearchByDMS(file).then(function (found) {
        if (found.id > 0) {
          $scope.data.docs = [found];
        } else {
          //Notify(`The file has been upladed. `, null, null, 'success');
          Notify('Not found DMS file. It might be removed or you might not have right permission to do that .', null, null, 'danger');
        }

        comService.loading(false);
      }).catch(function () {
        comService.loading(false);
      });
    }, function () {});
  };

  $scope.searchby = function (criteria) {
    $scope.cur_page = 1;
    $scope.search(criteria);
  };

  $scope.search = function (_criteria) {
    var Criteria, createdFrom, createdTo, docid, docname, doctypeIds, folderid, ids_customview, keyword, ref;
    $scope.searchMode = 'search';
    Criteria = {
      Date: [{}],
      AttributeCriterias: {
        Attributes: []
      }
    };
    docid = $scope.search.id;
    keyword = $scope.search.keyword;
    docname = $scope.search.title;
    createdFrom = $scope.search.created_from;
    createdTo = $scope.search.created_to;
    folderid = $scope.search.id_folder;
    doctypeIds = (ref = $scope.data.docTypes.filter(function (f) {
      return f.checked;
    })) != null ? ref.map(function (t) {
      return t.id;
    }) : void 0;

    if (docid) {
      Criteria.DocIds = [docid];
    }

    if (docname) {
      Criteria.Titles = [docname];
    }

    if (doctypeIds.length > 0) {
      Criteria.DocTypeIds = doctypeIds;
    }

    if (keyword) {
      Criteria.Keywords = keyword;
    }

    if (folderid) {
      Criteria.FolderIds = [folderid];
    }

    if (createdFrom) {
      Criteria.Date[0].From = moment(createdFrom).format('YYYY-MM-DD 00:00:00');
    }

    if (createdTo) {
      Criteria.Date[0].To = moment(createdTo).format('YYYY-MM-DD 23:59:59');
    }

    Criteria.Date[0].From || (Criteria.Date[0].From = '2000-01-01 00:00:00');
    Criteria.Date[0].To || (Criteria.Date[0].To = '2100-12-31 23:59:59');
    Criteria.AttributeCriterias.Attributes = [].concat(parseAttributeCriterias()); // ids_customview = idcustomview();
    // if ids_customview > 0
    // 	Criteria.CustomViewIds = [ ids_customview ]

    ids_customview = ids4CustomViews();

    if (ids_customview.length > 0) {
      Criteria.CustomViewIds = ids_customview;
    }

    if (!Criteria) {
      return;
    }

    comService.loading(true);
    return spiderdocsService.GetDocuments(_criteria || Criteria).then(applySortBy).then(storeMaxLength).then(applyLimitOffseet).then(function (docs) {
      $scope.data.docs = docs;
      pages(1);
    }).then(function () {
      comService.loading(false);
    });
  }; // $scope.toArray = function(notes, spliter)
  // {
  //     return removeDuplicates(notes.split(spliter));
  // }


  $scope.searchDoc = function (docids, ids_customview) {
    var Criteria;
    Criteria = {
      DocIds: []
    };

    if (docids == null) {
      return;
    }

    if (ids_customview > 0) {
      Criteria.CustomViewIds = [ids_customview];
    }

    Criteria.DocIds = Array.isArray(docids) ? docids : [docids];
    return spiderdocsService.GetDocuments(Criteria);
  };

  $scope.sort = function (column) {
    var columns;
    columns = ['js--header__date', 'js--header__name_docType', 'js--header__version', 'js--header__name_folder', 'js--header__author', 'js--header__title', 'js--header__id'];
    $scope.sortby.asc = $scope.sortby.key !== column ? false : $scope.sortby.asc;
    return $scope.sortBy(column).then(function (docs) {
      var ref;
      return angular.element('.js--header__' + column).addClass((ref = $scope.sortby.asc) != null ? ref : {
        'btn-sort--asc': 'btn-sort--desc'
      });
    });
  };

  $scope.filterStatusSelectedAll = function (name) {
    var selectAll;
    selectAll = angular.element('#' + name).prop('checked');
    $scope.data.docTypes.forEach(function (x) {
      x.checked = selectAll;
    });
    changeattrs();
  };

  $scope.populateAttrs = function () {
    clearTimeout($scope.pid.populateattr);
    $scope.pid.populateattr = setTimeout(function () {
      changeattrs();
    }, 300);
  };

  $scope.download = function (arg, scope) {
    var doc, url;
    doc = scope.doc;
    comService.loading(true);
    url = '';
    spiderdocsService.GetDonloadUrls(doc.id_latest_version).then(function (urls) {
      if (urls === void 0 || urls.length === 0) {
        return;
      } //alert('File could not exported.');


      url = urls.pop();
      return $http({
        url: url,
        method: 'Get',
        data: {},
        responseType: 'blob'
      });
    }).then(function (response) {
      var downloadLink, file, fileURL, filename, winUrl;
      file = response.data;
      filename = response.config.url.split('/').pop();

      if (comService.browser().chrome) {
        winUrl = window.URL || window.webkitURL;
        downloadLink = angular.element('<a></a>');
        downloadLink.attr('href', winUrl.createObjectURL(file));
        downloadLink.attr('target', '_self');
        downloadLink.attr('download', filename);
        downloadLink[0].click();
      } else if (comService.browser().ie || comService.browser().edge) {
        window.navigator.msSaveOrOpenBlob(file, filename);
      } else {
        fileURL = URL.createObjectURL(file);
        window.open(fileURL, filename);
      }
    }).then(function () {
      comService.loading(false);
    }).catch(function () {
      Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
      comService.loading(false);
    });
  };

  $scope.exportAsPDF = function (arg, scope) {
    var doc, url;
    doc = scope.doc;
    comService.loading(true);
    url = '';
    spiderdocsService.ExportAsPDF(doc.id_latest_version).then(function (urls) {
      if (urls === void 0 || urls.length === 0) {
        return;
      } //alert('File could not exported.');


      url = urls.pop();
      return $http({
        url: url,
        method: 'Get',
        data: {},
        responseType: 'blob'
      });
    }).then(function (response) {
      var downloadLink, file, fileURL, filename, winUrl;
      file = response.data;
      filename = response.config.url.split('/').pop();

      if (comService.browser().chrome) {
        winUrl = window.URL || window.webkitURL;
        downloadLink = angular.element('<a></a>');
        downloadLink.attr('href', winUrl.createObjectURL(file));
        downloadLink.attr('target', '_self');
        downloadLink.attr('download', filename);
        downloadLink[0].click();
      } else if (comService.browser().ie || comService.browser().edge) {
        window.navigator.msSaveOrOpenBlob(file, filename);
      } else {
        fileURL = URL.createObjectURL(file);
        window.open(fileURL, filename);
      }
    }).then(function () {
      comService.loading(false);
    }).catch(function () {
      Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
      comService.loading(false);
    });
  };

  $scope.details = function (arg, scope) {
    var doc;
    doc = scope.doc;
    spiderdocsService.GetHistories({
      DocIds: [doc.id]
    }).then(function (histories) {
      var enVer, events, history, j, len, modalInstance, versions;
      enVer = ["Created", "Import", "Scanned", "New version", "Check-in", "Export", "Save from Outlook", "Rollback version", "Saved as New Version", "Update version"]; // events = (history for history in histories when not enVer.includes(history.event_name.trim()))
      // en_Events.Created
      // en_Events.NewVer
      // en_Events.Import
      // en_Events.SaveNewVer
      // en_Events.Scan
      // en_Events.Chkin
      // en_Events.UpVer

      for (j = 0, len = histories.length; j < len; j++) {
        history = histories[j];
        history.date = moment(history.date).format('l LT');
      }

      events = histories;

      versions = function () {
        var k, len1, results;
        results = [];

        for (k = 0, len1 = histories.length; k < len1; k++) {
          history = histories[k];

          if (enVer.includes(history.event_name.trim())) {
            results.push(history);
          }
        }

        return results;
      }();

      return modalInstance = $uibModal.open({
        animation: true,
        ariaLabelledBy: 'modal-title',
        ariaDescribedBy: 'modal-body',
        templateUrl: 'scripts/app/views/form-doc-details.html',
        controller: 'formDocDetailsCtrl',
        controllerAs: '$ctrl',
        backdrop: 'static',
        size: 'lg',
        resolve: {
          // doc: ->
          // 	doc
          title: function title() {
            return '[Document Details]';
          },
          historyVersions: function historyVersions() {
            return versions;
          },
          historyEvents: function historyEvents() {
            return events;
          },
          properties: function properties() {
            return spiderdocsService.GetDetails({
              Documentid: doc.id
            });
          }
        }
      });
    });
  };

  $scope.folderClick = function (node) {
    $interval(function () {
      var Criteria, elm, idFolder;
      elm = $('sidebarjs [data-selected-folder-id]')[0];
      idFolder = $(elm).attr('data-selected-folder-id');
      Criteria = {
        Date: [{}],
        AttributeCriterias: {
          Attributes: []
        }
      };

      if (idFolder) {
        Criteria.FolderIds = [idFolder];
      }

      $scope.searchby(Criteria);
    }, 100, 1);
  };

  $scope.sortBy = function (key) {
    $scope.sortby.key = key;
    $scope.sortby.asc = !$scope.sortby.asc;
    return queryDocs();
  };

  idcustomview = function idcustomview() {
    var ans, matched, path;
    path = $window.location.pathname;
    matched = path.toLowerCase().match(/workspace\/index\/([0-9]+)/);
    ans = matched != null ? matched[1] : 0;
    return parseInt(ans);
  };

  $scope.showCustomView = function () {
    return !(idcustomview() > 0);
  };

  $scope.showPageName = function () {
    return spiderdocsService.GetCustomViewsAsync().then(function (vies) {
      var j, len, results, v;

      if (idcustomview() === 0) {
        return $scope.view.pagetitle = 'Local Database';
      } else {
        results = [];

        for (j = 0, len = vies.length; j < len; j++) {
          v = vies[j];

          if (v.id === idcustomview()) {
            results.push($scope.view.pagetitle = v.name);
          }
        }

        return results;
      }
    });
  };

  $scope.checkoutWithFooter = function (doc, footer) {
    return spiderdocsService.CheckOutWithFooterAsync(doc.id, footer).then(function (err) {
      if (true === angular.isString(err) && angular.isEmpty(err)) {
        $scope.data.docs.find(function (x) {
          return x.id === doc.id;
        }).id_status = en_file_Status.checked_out;
        Notify('The document has been checkedout!', null, null, 'success');
        $interval(function () {
          $window.location.href = './workspace/local';
        }, 1200, 1);
      } else {
        Notify('Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger');
      }
    }).then(function () {
      comService.loading(false);
    }).catch(function () {
      comService.loading(false);
    });
  };

  $scope.dblclickOnDoc = function (doc) {
    console.log("double clicked on ".concat(doc.title));
    return spiderdocsService.GetPreferenceAsync().then(function (preference) {
      switch (preference.dblClickBehavior) {
        case en_DoubleClickBehavior.OpenToRead:
          return $scope.download({}, {
            doc: doc
          });

        case en_DoubleClickBehavior.CheckOut:
          if (canCheckout({}, {
            doc: doc
          })) {
            return $scope.checkout({}, {
              doc: doc
            });
          }

          break;

        case en_DoubleClickBehavior.CheckOutFooter:
          if (canCheckout({}, {
            doc: doc
          })) {
            return $scope.checkoutWithFooter(doc, true);
          }

          break;

        default:
          // $scope.checkout {},{doc: doc}, true
          return $scope.download({}, {
            doc: doc
          });
      }
    });
  };

  setupImportByDrag = function setupImportByDrag() {
    var leaves;

    leaves = function leaves() {
      clearTimeout($scope.pid.dragfile);
      $scope.pid.dragfile = setTimeout(function () {
        angular.element('#upload1').addClass('hidden');
      }, 100);
    };

    angular.element('html').on('dragover', function (e) {
      leaves();
      e.preventDefault();
      e.stopPropagation();
    }); // angular.element('body .screen').on 'mousedown', (e) ->
    // 	e.preventDefault()
    // 	e.stopPropagation()
    // 	return

    angular.element('html').on('dragenter', function (e) {
      angular.element('#upload1').removeClass('hidden');
      e.preventDefault();
      e.stopPropagation();
    });
    angular.element('#upload1').on('drop', drop);
  };

  drop = function drop(e) {
    var countByExpect, files;
    e.preventDefault();
    e.stopPropagation();

    if (e.originalEvent.dataTransfer) {
      files = e.originalEvent.dataTransfer.files;

      if (files.length > 0) {
        countByExpect = 0;
        $q.all(_.values(files).map(function (file) {
          return comService.noop(comService.file2Doc(file));
        })).then(function (docs) {
          return modalService.saveAsNew(docs); //.then(queryDocs)
        }).then(function (data) {
          var docs, func, options, promise, reason;
          docs = data.docs;
          func = data.func;
          reason = data != null ? data.reason : void 0;
          options = data != null ? data.options : void 0; // format(docs)
          // $scope.data.docs = docs

          countByExpect = docs.length;
          console.log(data);
          promise = {};
          comService.loading(true);
          Notify('The file has started to checkin.It might take little bit longer...', null, null, 'info');

          switch (func) {
            case 'new':
              console.log('new was selected');
              promise = $q.all(docs.map(function (doc) {
                return spiderdocsService.Upload(doc._ref).then(function (tempid) {
                  return spiderdocsService.ImportDbAsNew(tempid, doc, options);
                });
              }));
              break;

            case 'ver':
              console.log('ver was selected');
              promise = $q.all(docs.map(function (doc) {
                return spiderdocsService.SaveAsNewVer(doc.id, doc._ref, data.reason);
              }));
          }

          return promise;
        }).then(function (docs) {
          var countByActual, doc;

          countByActual = function () {
            var j, len, results;
            results = [];

            for (j = 0, len = docs.length; j < len; j++) {
              doc = docs[j];

              if (doc.id > 0) {
                results.push(doc);
              }
            }

            return results;
          }().length;

          if (countByExpect === countByActual) {
            Notify('File(s) have/has been saved into SpiderDocs', null, null, 'success');
          } else {
            Notify('Rejected. You might not have right permission to do that .', null, null, 'danger');
          }

          angular.element('#upload1').addClass('hidden');
          comService.loading(false);
          return $scope.clearSearchCriteria();
        }).catch(function () {
          comService.loading(false);
          angular.element('#upload1').addClass('hidden');
        });
      }
    }
  };

  setupDbViews = function setupDbViews(customviews) {
    var j, len, results, view;
    $scope.search.database_views = [{
      id: 0,
      text: 'Local',
      checked: true
    }];
    results = [];

    for (j = 0, len = customviews.length; j < len; j++) {
      view = customviews[j];
      results.push($scope.search.database_views.push({
        id: view.id,
        text: view.name,
        checked: true
      }));
    }

    return results;
  };

  ids4CustomViews = function ids4CustomViews(customviews) {
    var ids, j, len, ref, v;
    ids = [];
    ref = $scope.search.database_views;

    for (j = 0, len = ref.length; j < len; j++) {
      v = ref[j];

      if (v.checked === true) {
        ids.push(v.id);
      }
    }

    ids = $scope.search.database_views.length === ids.length ? [] : ids;
    return ids;
  };

  init();
}]);
app.controller('deleteFormCtrl', ["$uibModalInstance", "doc", function ($uibModalInstance, doc) {
  var $ctrl, init;
  $ctrl = this;

  init = function init() {};

  $ctrl.doc = doc;
  $ctrl.reason = '';
  init();

  $ctrl.ok = function () {
    $uibModalInstance.close($ctrl.reason);
  };

  $ctrl.cancel = function () {
    $uibModalInstance.dismiss('cancel');
  };
}]);
//# sourceMappingURL=../../maps/app/controllers/workspace-controller.js.map
