"use strict";

app.directive('nodeTree', function () {
  return {
    template: '<node tk-node="node" ng-repeat="node in folders" ng-click="clickOnNT(node);" ></node>',
    replace: true,
    transclude: true,
    restrict: 'E',
    scope: {
      folders: '=ngModel',
      permission: '=tkPermission',
      clickOnNT: '&tkClick',
      menu: '=tkMenu',
      menuFnc: '=tkMenuFnc'
    },
    link: function link(scope, elm, attrs) {}
  };
}); // scope.clickb = function(a)
// {
// 	scope.clicka(a);
// }
// scope.clicka = function (node)
// {
// }

app.directive('node', ["$compile", "spiderdocsService", "comService", "en_Actions", function ($compile, spiderdocsService, comService, en_Actions) {
  return {
    restrict: 'E',
    replace: true,
    template: "<li class=\"parent_li\" context-menu menu-items=\"menu\" ng-show=\"node != null\">\n\t<span class=\"leaf\" ng-click=\"clickOnN(node);\">\n\t\t<i class=\"glyphicon--folder glyphicon-folder-close\"></i>\n\t\t{{node.text}}\n\t\t<i class=\"glyphicon {{ switcher( isLeaf(node), '', 'glyphicon-plus' )}}\"></i>\n\t</span>\n</li>",
    scope: {
      node: '=tkNode',
      clickOnN: '&tkClick',
      defaultFolderid: '@tkDefault',
      menu: '=tkMenu',
      menuFnc: '=tkMenuFnc'
    },
    link: function link(scope, elm, attrs) {
      var childFolders, def;
      console.log("Compile node tag. ".concat(scope.node.text));
      def = parseInt(scope.defaultFolderid || 0);

      if (scope.menuFnc != null) {
        angular.extend(scope, scope.menuFnc);
      }

      childFolders = function childFolders(_scope, _elm, _attr) {
        console.log("childFolders");
        console.log(_scope);

        if (_scope.node.children.length === 0) {
          return;
        }

        return spiderdocsService.cache.foldersL1(_scope.node.Id, _attr.permission || en_Actions.OpenRead).then(function (folders) {
          var childNode;
          _scope.folders = comService.nodesBy(_scope.node.Id, folders);
          _scope.permission = _attr.permission;
          childNode = $compile('<ul ><node-tree ng-model="folders" tk-permission="permission" ng-click="clickOnN(node);" tk-default="{{defaultFolderid}}" tk-menu="menu" tk-menu-fnc="menuFnc" ></node-tree></ul>')(_scope);

          _elm.append(childNode);
        });
      };

      parseInt(scope.node.Id) === parseInt(scope.defaultFolderid || 0) && $(elm).attr('data-selected-folder-id', scope.node.Id);
      $(elm).attr('data-folder-id', scope.node.Id);
      /*
      Open/Close a leaf 
      */

      $(elm).find('span.leaf').on('click', function (e) {
        $('[data-selected-folder-id]').removeAttr('data-selected-folder-id').removeClass('data-text');
        $(elm).attr('data-selected-folder-id', scope.node.Id);
        $(elm).attr('data-text', scope.node.text);

        if (scope.node.children.length > 0) {
          // showing children folders at the moment.
          if ($(elm).find('ul').is(':visible')) {
            $(elm).find('ul').hide('fast', function (e) {
              $(elm).find('ul').remove();
              $(elm).find('i').addClass('glyphicon-plus').removeClass('glyphicon-minus');
            });
            return;
          } else {
            childFolders(scope, elm, attrs).then(function () {
              $(elm).find('ui').show('fast');
              $(elm).find('i').addClass('glyphicon-minus').removeClass('glyphicon-plus');
            });
          }
        }
      });

      scope.switcher = function (booleanExpr, trueValue, falseValue) {
        if (booleanExpr) {
          return trueValue;
        } else {
          return falseValue;
        }
      };

      scope.isLeaf = function (_data) {
        if (_data != null && _data.children != null && _data.children.length === 0) {
          return true;
        }

        return false;
      };
    }
  };
}]);
//# sourceMappingURL=../../maps/app/directives/tree-directive.js.map
