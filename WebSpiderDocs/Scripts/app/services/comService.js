"use strict";

app.service('comService', ["$q", "$location", "$cookies", "$http", "$interval", "_", function ($q, $location, $cookies, $http, $interval, _) {
  var _this = this;

  var _nodesBy, pid_goto, pid_loading, that;

  that = this;
  pid_loading = 0;
  pid_goto = 0;

  _nodesBy = function nodesBy(id_parent, db, def) {
    var nodes, struct; //var that = this;

    struct = function struct(nodes) {
      nodes = nodes || [];
      return nodes.map(function (n) {
        return {
          Id: n.id,
          text: n.document_folder,
          children: _nodesBy(n.id, db, def),
          state: {
            selected: def > 0 && n.id === def ? true : false,
            expanded: false
          },
          ref: n
        };
      });
    };

    def = def || 0;
    def = parseInt(def);
    nodes = db.filter(function (f) {
      return parseInt(f.id_parent) === parseInt(id_parent);
    });
    return struct(nodes) || [];
  };

  this.loading = function (show) {
    show = show !== false;

    if (pid_loading > 0) {
      clearInterval(pid_loading);
    }

    if (show) {
      pid_loading = setTimeout(function () {
        angular.element('.loading').removeClass('hidden');
      }, 1000);
    } else {
      pid_loading = setTimeout(function () {
        angular.element('.loading').addClass('hidden');
        angular.element('.loading .loading-sheet').remove();
        pid_loading = 0;
      }, 100);
    }
  };

  this.getCookieBy = function () {
    var key = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : '';
    var keyvalue;
    keyvalue = $cookies.get('userCookie').split('&').find(function (x) {
      return x.startsWith(key);
    });
    return keyvalue.split('=')[1];
  };

  this.browser = function () {
    var isChrome, isEdge, isFirefox, isIE, isOpera, isSafari; // Opera 8.0+

    isOpera = !!window.opr && !!opr.addons || !!window.opera || navigator.userAgent.indexOf(' OPR/') >= 0; // Firefox 1.0+

    isFirefox = typeof InstallTrigger !== 'undefined'; // Safari 3.0+ "[object HTMLElementConstructor]"

    isSafari = /constructor/i.test(window.HTMLElement) || function (p) {
      return p.toString() === '[object SafariRemoteNotification]';
    }(!window['safari'] || safari.pushNotification); // Internet Explorer 6-11


    isIE = false || !!document.documentMode; // Edge 20+

    isEdge = !isIE && !!window.StyleMedia; // Chrome 1+

    isChrome = !!window.chrome || !!window.chrome.webstore || !!window.chrome.runtime;
    return {
      opera: isOpera,
      firefox: isFirefox,
      safari: isSafari,
      ie: isIE,
      edge: isEdge,
      chrome: isChrome
    };
  };

  this.nodesBy = function (id_parent, db, def) {
    return _nodesBy(id_parent, db, def);
  }; // def = def || 0;
  // def = parseInt(def)
  // var that = this;
  // function struct(nodes) {
  // 	nodes = nodes || [];
  // 	return nodes.map(function(n) {
  // 		return {
  // 			Id: n.id, text: n.document_folder, children: that.nodesBy(n.id, db, def), state: { selected: def > 0 && n.id === def ? true : false, expanded:false}  }
  // 	});
  // }
  // var nodes = db.filter(function(f) {return parseInt(f.id_parent) === parseInt(id_parent)});
  // return struct(nodes) || [];


  this.downloadBy = function (url) {
    return $http({
      url: url,
      method: 'Get',
      data: {},
      responseType: 'blob'
    }).then(function (response) {
      var downloadLink, file, fileURL, filename;
      file = response.data;
      filename = response.config.url.split('/').pop();

      if (that.browser().chrome) {
        if (window.URL) {
          url = window.URL;
        } else {
          url = window.webkitURL;
        } //var url = (window.URL || window.webkitURL);


        downloadLink = angular.element('<a></a>');
        downloadLink.attr('href', url.createObjectURL(file));
        downloadLink.attr('target', '_self');
        downloadLink.attr('download', filename);
        downloadLink[0].click();
      } else if (that.browser().ie || that.browser().edge) {
        window.navigator.msSaveOrOpenBlob(file, filename);
      } else {
        fileURL = URL.createObjectURL(file);
        window.open(fileURL, filename);
      }
    });
  };

  this.openFolder = function (id_default, _folders, target) {
    var _makePath, selectPath;

    selectPath = [];
    id_default = parseInt(id_default);

    _makePath = function makePath(id, folders, out) {
      var find;

      find = function find(db, id) {
        return db.find(function (x) {
          return x.id === id;
        }) || {};
      };

      out = out || [];
      folders = folders || [];

      if (!id) {
        return;
      }

      return out.push(id) && _makePath(find(folders, id).id_parent, folders, out);
    };

    _makePath(id_default, _folders, selectPath);

    pid_goto = $interval(function () {
      var $target, next;
      $target = angular.element(target).find('li[data-folder-id="' + selectPath[selectPath.length - 1] + '"]');

      if (selectPath.length > 0 && !$target.html()) {
        return;
      }

      next = selectPath.pop();

      if (next) {
        $target.find('span.leaf').triggerHandler('click');
      } else {
        clearInterval(pid_goto);
      }
    }, 10, 100).$$intervalId;
  };

  this.validateEmail = function (email) {
    var re;
    re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  };

  this.extImages = [".png", ".gif", ".jpg", ".jpeg", ".bmp", ".tif"];
  this.extPDFs = [".pdf"];

  this.isOCRable = function (path) {
    var info;
    info = angular.extractPath(path);
    return _this.extImages.concat(_this.extPDFs).includes(info.extension);
  };

  this.extPDFable = [".docx", ".doc", ".xls", ".xlsx", ".xlsm", ".ppt", ".pptx", ".pps"];

  this.canExportExt = function (path) {
    var info;
    info = angular.extractPath(path);
    return _this.extPDFable.includes(info.extension);
  };

  this.sortBy = function (data, key, asc) {
    data = _.sortBy(data, [key]);
    data = !asc ? data.reverse() : data;
    return data;
  };

  this.noop = function (data) {
    var deferred;
    deferred = $q.defer();
    setTimeout(function () {
      return deferred.resolve(data);
    }, 1);
    return deferred.promise;
  };

  this.file2Doc = function (file) {
    var doc;
    doc = {};
    angular.copy(file, doc); // transfer property values

    doc.id = 0;
    doc.title = file.name;
    doc.extension = file.name.split('.').length > 1 ? file.name.substring(file.name.lastIndexOf('.'), file.name.length) : '';
    doc.id_latest_version = 0;
    doc._ref = file;
    return doc;
  };
}]); // @folderNodeToFolder = (node) =>
// 	folder =
//# sourceMappingURL=../../maps/app/services/comService.js.map
