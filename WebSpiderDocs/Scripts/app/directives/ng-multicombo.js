"use strict";

(function (window, document) {
  return angular.module('ng.multicombo', ['ng']).directive('ngMultiCombo', ['$window', '$timeout', function ($window, $timeout) {
    return {
      template: "<div class=\"form-field\">\n\t<label for=\"el-lead-source\" class=\"form-field__label\">\n\t\t{{title}}\n\t</label>\n\t<div class=\"form-field__select\">\n\t\t<div id=\"{{identify}}_sl\" class=\"leadStatus dd_chk_select\"\n\t\t\t\tstyle=\"height: 40px; padding: 8px 0px 0px 10px; background-position: 99% 10px; background-image: none;\">\n\t\t\t<div id=\"caption\">\n\t\t\t\tPlease Select\n\t\t\t</div>\n\t\t\t<div id=\"{{identify}}_dv\" class=\"dd_chk_drop\"\n\t\t\t\t\tstyle=\"position: absolute; top: 40px; display: none;\">\n\t\t\t\t<div id=\"checks\">\n\t\t\t\t\t<span ng-show=\"showAll\">\n\t\t\t\t\t\t<div class=\"form-field form-field--checkbox\" style=\"margin-bottom:20px\">\n\t\t\t\t\t\t\t<input type=\"checkbox\" name=\"customDDL-all{{identify4all}}_sll\" id=\"customDDL-all{{identify4all}}_sll\" ng-model=\"select\" ng-click=\"toggleSlect()\">\n\t\t\t\t\t\t\t<label for=\"customDDL-all{{identify4all}}_sll\" class=\"form-field__label\">\n\t\t\t\t\t\t\t\tSelect All\n\t\t\t\t\t\t\t</label>\n\t\t\t\t\t\t</div>\n\t\t\t\t\t</span>\n\t\t\t\t\t<span id=\"\" selectboxwidth=\"150px\">\n\t\t\t\t\t\t<div class=\"form-field form-field--checkbox\" ng-repeat=\"opt in source\">\n\n\t\t\t\t\t\t\t<input id=\"customDDL-{{toAlphabet(title)}}-opt_{{$index}}\"\n\t\t\t\t\t\t\t\t\ttype=\"checkbox\" name=\"customDDL-opt{{$index}}\" value=\"{{opt.id}}\" ng-model=\"opt.checked\" ng-change=\"onchanged(opt)\">\n\t\t\t\t\t\t\t<label for=\"customDDL-{{toAlphabet(title)}}-opt_{{$index}}\" class=\"form-field__label\">\n\t\t\t\t\t\t\t\t{{opt.text}}\n\t\t\t\t\t\t\t</label>\n\t\t\t\t\t\t\t<br>\n\t\t\t\t\t\t</div>\n\t\t\t\t\t</span>\n\t\t\t\t</div>\n\t\t\t</div>\n\t\t</div>\n\t</div>\n</div>",
      replace: true,
      transclude: true,
      restrict: 'E',
      scope: {
        title: '@tkTitle',
        source: '=ngModel',
        defaultChecked: '=tkDefault',
        showAll: '@tkShowAll',
        onchanged: '&onChanged',
        disabled: '=ngDisabled'
      },
      link: function link($scope, elm, attrs) {
        var identify;
        $scope.select = false;
        $scope.identify = identify = 'ddl' + String(Math.abs(Math.random())).replace('.', '');
        $scope.identify4all = Math.random().toString().replace('.', '').substring(1, 7);
        $timeout(function () {
          if ($scope.disabled !== true) {
            return ($window.identify = new DropDownScript(identify, '_dv', '_sl', function () {}, false, false, false)).init();
          }
        }, 0);

        $scope.toggleSlect = function () {
          var opt;

          (function () {
            var i, len, ref, results;
            ref = $scope.source;
            results = [];

            for (i = 0, len = ref.length; i < len; i++) {
              opt = ref[i];
              results.push(opt.checked = !$scope.select);
            }

            return results;
          })();
        };

        return $scope.toAlphabet = function (text) {
          var a;
          a = text.match(/[a-zA-Z]+/g).join('');
          a = a.toLowerCase();
          return a;
        };
      }
    };
  }]);
})(window, document);
//# sourceMappingURL=../../maps/app/directives/ng-multicombo.js.map
