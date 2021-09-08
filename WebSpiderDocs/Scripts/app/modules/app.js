"use strict";

function _typeof(obj) { "@babel/helpers - typeof"; if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") { _typeof = function _typeof(obj) { return typeof obj; }; } else { _typeof = function _typeof(obj) { return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }; } return _typeof(obj); }

var app;
app = angular.module('app', ['angularMoment', 'ngCookies', 'ngStorage', 'angular-loading-bar', 'ui.bootstrap', 'ngContextMenu', 'ngSidebarJS', 'ng.ckeditor', 'ng.multicombo'], ["$locationProvider", "$sceProvider", function ($locationProvider, $sceProvider) {
  $locationProvider.html5Mode(true);
}]); //$sceProvider.enabled(false);
// Completely disable SCE.  For demonstration purposes only!
// Do not use in new projects or libraries.
//$sceProvider.enabled(false);

app.config(['$localStorageProvider', function ($localStorageProvider) {
  var dateObjPrefix, deserializer, isDate, parseISOString, serializer; // var serializer = angular.toJson;
  // var deserializer = angular.fromJson;

  dateObjPrefix = 'ngStorage-Type-[object Date]:';

  serializer = function serializer(v) {
    if (isDate(v)) {
      return dateObjPrefix + v.getTime();
    } else {
      return angular.toJson(v);
    }
  };

  deserializer = function deserializer(v) {
    if (v.startsWith(dateObjPrefix)) {
      return parseISOString(v);
    } else {
      return angular.fromJson(v);
    }
  };

  isDate = function isDate(date) {
    return date && Object.prototype.toString.call(date) === '[object Date]' && !isNaN(date);
  };

  parseISOString = function parseISOString(s) {
    var b;
    b = s.replace(dateObjPrefix, '').replace('"', '').split(/\D+/);
    return new Date(parseInt(b[0]));
  }; //return (console.log("parse:" + new Date(b[0], b[1] -1, b[2], b[3], b[4], b[5], b[6])), new Date(b[0], b[1] -1, b[2], b[3], b[4], b[5], b[6]));


  $localStorageProvider.setSerializer(serializer);
  $localStorageProvider.setDeserializer(deserializer);
}]);
app.constant('_', window._).run(["$rootScope", function ($rootScope) {
  $rootScope._ = window._;
}]);
app.constant('jQuery', window.jQuery).run(["$rootScope", function ($rootScope) {
  $rootScope.jQuery = window.jQuery;
}]);
app.run(["amMoment", function (amMoment) {
  amMoment.changeLocale('au');
}]);
app.directive('upload', ['$http', function ($http) {
  return {
    restrict: 'E',
    replace: true,
    scope: {},
    require: '?ngModel',
    template: '<div class="asset-upload">Drag files here to upload</div>',
    link: function link(scope, element, attrs, ngModel) {}
  };
}]); // Code goes here

app.directive('ngConfirmClick', [function () {
  return {
    link: function link(scope, element, attr) {
      var clickAction, msg;
      msg = attr.ngConfirmClick || 'Are you sure?';
      clickAction = attr.confirmedClick;
      element.bind('click', function (event) {
        if (window.confirm(msg)) {
          scope.$eval(clickAction);
        }
      });
    }
  };
}]);
app.directive('ngEnter', function () {
  return function (scope, element, attrs) {
    element.bind('keydown keypress', function (event) {
      if (event.which === 13) {
        scope.$apply(function () {
          scope.$eval(attrs.ngEnter, {
            'event': event
          });
        });
        event.preventDefault();
      }
    });
  };
}); // app.directive('ngContextMenu', ['$document', '$window', function ($document, $window) {
// 	// Runs during compile
// 	return {
// 		restrict: 'A',
// 		link: function ($scope, $element, $attr) {
// 			var contextMenuElm,
// 				$contextMenuElm,
// 				windowWidth = window.innerWidth,
// 				windowHeight = window.innerHeight,
// 				contextMenuWidth,
// 				contextMenuHeight,
// 				contextMenuLeftPos = 0,
// 				contextMenuTopPos = 0,
// 				$w = $($window),
// 				caretClass = {
// 					topRight: 'context-caret-top-right',
// 					topLeft: 'context-caret-top-left',
// 					bottomRight: 'context-caret-bottom-right',
// 					bottomLeft: 'context-caret-bottom-left'
// 				},
// 				menuItems = $attr.menuItems;
// 			function createContextMenu() {
// 				var fragment = document.createDocumentFragment();
// 				contextMenuElm = document.createElement('ul'),
// 					$contextMenuElm = $(contextMenuElm);
// 				contextMenuElm.setAttribute('id', 'context-menu');
// 				contextMenuElm.setAttribute('class', 'custom-context-menu');
// 				mountContextMenu($scope[menuItems], fragment);
// 				contextMenuElm.appendChild(fragment);
// 				document.body.appendChild(contextMenuElm);
// 				contextMenuWidth = $contextMenuElm.outerWidth(true);
// 				contextMenuHeight = $contextMenuElm.outerHeight(true);
// 			}
// 			function mountContextMenu(menuItems, fragment) {
// 				menuItems.forEach(function (_item) {
// 					var li = document.createElement('li');
// 					li.innerHTML = '<a>' + _item.label + ' <span class="right-caret"></span></a>';
// 					if (_item.action && _item.active) {
// 						li.addEventListener('click', function () {
// 							if (typeof $scope[_item.action] isnt 'function') return false;
// 							$scope[_item.action]($attr, $scope);
// 						}, false);
// 					}
// 					if (_item.divider) {
// 						addContextMenuDivider(fragment);
// 					}
// 					if (!_item.active) li.setAttribute('class', 'disabled');
// 					if (_item.subItems) {
// 						addSubmenuItems(_item.subItems, li, _item.active)
// 					}
// 					fragment.appendChild(li);
// 				});
// 			}
// 			function addSubmenuItems(subItems, parentLi, parentIsActive) {
// 				parentLi.setAttribute('class', 'dropdown-submenu')
// 				if (!parentIsActive) parentLi.setAttribute('class', 'disabled')
// 				var ul = document.createElement('ul')
// 				ul.setAttribute('class', 'dropdown-menu')
// 				mountContextMenu(subItems, ul)
// 				parentLi.appendChild(ul)
// 			}
// 			function addContextMenuDivider(fragment) {
// 				var divider = document.createElement('li');
// 				divider.className = 'divider'
// 				fragment.appendChild(divider);
// 			}
// 			/**
// 			 * Removing context menu DOM from page.
// 			 * @return {[type]} [description]
// 			 */
// 			function removeContextMenu() {
// 				$('.custom-context-menu').remove();
// 			}
// 			/**
// 			 * Apply new css class for right positioning.
// 			 * @param  {[type]} cssClass [description]
// 			 * @return {[type]}          [description]
// 			 */
// 			function updateCssClass(cssClass) {
// 				$contextMenuElm.attr('class', 'custom-context-menu');
// 				$contextMenuElm.addClass(cssClass);
// 			}
// 			/**
// 			 * [setMenuPosition description]
// 			 * @param {[type]} e       [event arg for finding clicked position]
// 			 * @param {[type]} leftPos [if menu has to be pointed to any pre-fixed element like caret or corner of box.]
// 			 * @param {[type]} topPos  [as above but top]
// 			 */
// 			function setMenuPosition(e, leftPos, topPos) {
// 				contextMenuLeftPos = leftPos || e.pageX;
// 				contextMenuTopPos = topPos - $w.scrollTop() || e.pageY - $w.scrollTop();
// 				if (window.innerWidth - contextMenuLeftPos < contextMenuWidth && window.innerHeight - contextMenuTopPos > contextMenuHeight) {
// 					contextMenuLeftPos -= contextMenuWidth;
// 					updateCssClass(caretClass.topRight);
// 				} else if (window.innerWidth - contextMenuLeftPos > contextMenuWidth && window.innerHeight - contextMenuTopPos > contextMenuHeight) {
// 					updateCssClass(caretClass.topLeft);
// 				} else if (windowHeight - contextMenuTopPos < contextMenuHeight && windowWidth - contextMenuLeftPos > contextMenuWidth) {
// 					contextMenuTopPos -= contextMenuHeight;
// 					updateCssClass(caretClass.bottomLeft);
// 				} else if (windowHeight - contextMenuTopPos < contextMenuHeight && windowWidth - contextMenuLeftPos < contextMenuWidth) {
// 					contextMenuTopPos -= contextMenuHeight;
// 					contextMenuLeftPos -= contextMenuWidth;
// 					updateCssClass(caretClass.bottomRight);
// 				}
// 				$contextMenuElm.css({
// 					left: contextMenuLeftPos,
// 					top: contextMenuTopPos
// 				}).addClass('context-caret shown');
// 			}
// 			/**
// 			 * CONTEXT MENU
// 			 * @param  {[type]} evt [description]
// 			 * @return {[type]}     [description]
// 			 */
// 			$element.on('contextmenu.dirContextMenu', function (evt) {
// 				evt.preventDefault();
// 				removeContextMenu();
// 				createContextMenu();
// 				/**
// 				 * If pointer node has specified, let the context menu
// 				 * apprear right below to that elem no matter
// 				 * where user clicks within that element.
// 				 */
// 				if ($attr.pointerNode) {
// 					var $pointer = $(this).find($attr.pointerNode);
// 					contextMenuLeftPos = $pointer.offset().left + ($pointer.outerWidth(true) / 2);
// 					contextMenuTopPos = $pointer.offset().top + $pointer.outerHeight(true);
// 					setMenuPosition(evt, contextMenuLeftPos, contextMenuTopPos);
// 				} else {
// 					setMenuPosition(evt);
// 				}
// 				$w.on('keydown.dirContextMenu', function (e) {
// 					if (e.keyCode is 27) {
// 						removeContextMenu();
// 					}
// 				})
// 			}); //END (on)click.dirContextMenu
// 			$document.off('click.dirContextMenu').on('click.dirContextMenu', function (e) {
// 				if (!$(e.target).is('.custom-context-menu') && !$(e.target).parents().is('.custom-context-menu')) {
// 					removeContextMenu();
// 				}
// 			});
// 			$w.off('scroll.dirContextMenu').on('scroll.dirContextMenu', function () {
// 				removeContextMenu();
// 			});
// 			$w.on('resize.dirContextMenu', function () {
// 				windowWidth = window.innerWidth;
// 				windowHeight = window.innerHeight;
// 				removeContextMenu();
// 			});
// 		}
// 	}
// }]);

app.filter('toDate', ["moment", function (moment) {
  return function (input) {
    input = input || '';

    if (moment(input) > moment('2000-01-01', 'YYYY-MM-DD', true)) {
      return moment(input).toDate();
    } else {
      return null;
    }
  };
}]);
app.filter('dispNA', function () {
  return function (input) {
    var prop;
    input = input || '';

    if (_typeof(input) === 'object') {
      for (prop in input) {
        if (ad.hasOwnProperty(prop)) {
          return input;
        }
      }
    } else if (input !== '') {
      return input;
    }

    return 'N/A';
  };
});
app.filter('dispNil', function () {
  return function (input) {
    input = input || '';

    if (input === '' || input === void 0 || input === null) {
      return null;
    }

    if (typeof input === 'string' && (input.indexOf('1975-01-01') > -1 || input.indexOf('157737600000') > -1)) {
      return null;
    }

    if (_typeof(input) === 'object') {
      if (moment(input) > moment('2000-01-01', 'YYYY-MM-DD', true)) {
        return input;
      } else {
        return null;
      }
    } else if (input !== '') {
      return input;
    }

    return '';
  };
});
app.filter('strReplace', function () {
  return function (input, from, to) {
    input = input || '';
    from = from || '';
    to = to || '';
    return input.replace(new RegExp(from, 'g'), to);
  };
});
app.filter('range', function () {
  return function (input, total) {
    var i;
    total = parseInt(total);
    i = 0;

    while (i < total) {
      input.push(i);
      i++;
    }

    return input;
  };
});

app.hex_to_ascii = function (str1) {
  var hex, n, str;
  hex = str1.toString();
  str = '';
  n = 0;

  while (n < hex.length) {
    str += String.fromCharCode(parseInt(hex.substr(n, 2), 16));
    n += 2;
  }

  return str;
};
/*
    Custom functions
*/


angular.isEmpty = function (val) {
  return angular.isUndefined(val) || val === null || val === '';
};

angular.isDateValid = function (date) {
  if (date === void 0 || date === null) {
    return false;
  }

  if (!moment(date).isValid()) {
    return;
  }

  if (moment(date) > moment('2000-01-01', 'YYYY-MM-DD', true)) {
    return true;
  }

  return false;
};

angular.extractPath = function (path) {
  var ext, withoutExt;
  ext = path.split('.').length > 1 ? path.substring(path.lastIndexOf('.'), path.length) : '';
  withoutExt = path.split('.').slice(0, -1).join('.');
  return {
    org: path,
    nameWithoutExt: withoutExt,
    extension: ext
  };
};

if (!String.prototype.format) {
  String.prototype.format = function () {
    var args;
    args = arguments;
    return this.replace(/{(\d+)}/g, function (match, number) {
      if (typeof args[number] !== 'undefined') {
        return args[number];
      } else {
        return match;
      }
    });
  };
}
//# sourceMappingURL=../../maps/app/modules/app.js.map
