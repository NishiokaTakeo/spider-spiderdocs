"use strict";

app.directive('attribute', ["$compile", "$q", "spiderdocsService", "tagService", "$interval", function ($compile, $q, spiderdocsService, tagService, $interval) {
  return {
    restrict: 'E',
    replace: true,
    template: "<div>\n<div class=\"form-field \">\n<label for=\"lead-filter-rep\" class=\"form-field__label\">Document Type</label>\n<div class=\"form-field__select form-field__select--blue\">\n<select name=\"lead-filter-rep\" id=\"lead-filter-rep\" ng-model=\"type\" ng-options=\"opt.type for opt in types track by opt.id\" ng-change=\"typeChanged()\" ></select>\n</div>\n</div>\n<div class=\"js-attribute-section\"></div>\n</div>",
    scope: {
      types: '=tkTypes',
      type: '=tkModel',
      selectedAttr: '=tkSelectedAttr',
      defaultAttrs: '=tkDefaultAttrs',
      onGenerated: '&onGenerated'
    },
    link: function link(scope, elm, attrs) {
      var genAttrs;

      genAttrs = function genAttrs(target, typeId) {
        var myattrs = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : [];
        var args, twoDimensional2One;
        args = {
          typeMapsAttrs: [],
          comboitems: []
        };

        twoDimensional2One = function twoDimensional2One(res) {
          var items;
          res = res || [];
          items = [];
          res.forEach(function (row) {
            if (row && row.length > 0) {
              row.forEach(function (x) {
                items.push(x);
              });
            }
          });
          return items;
        };

        return spiderdocsService.GetAttributeDocType(typeId).then(function (_typeMapsAttrs) {
          var ids;
          args.typeMapsAttrs = _typeMapsAttrs;
          ids = [];

          _typeMapsAttrs.forEach(function (x) {
            if (!ids.includes(x.id_attribute)) {
              ids.push(x.id_attribute);
            }
          });

          return $q.all(ids.map(function (x) {
            return spiderdocsService.cache.comboitems(x);
          }));
        }).then(twoDimensional2One).then(function (_comboitems) {
          args.comboitems = _comboitems;
          return spiderdocsService.cache.attrs();
        }).then(function (_attrs) {
          return tagService.genAttrs(target, typeId, args.typeMapsAttrs, _attrs, args.comboitems, myattrs);
        });
      };

      scope.type = scope.type || {};
      $interval(function () {
        scope.selectedAttr = tagService.parseAttributeCriterias(elm.find('.js-attribute-section'));
      }, 300);

      scope.typeChanged = function () {
        genAttrs(elm.find('.js-attribute-section'), scope.type.id || 0, scope.defaultAttrs || []).then(function () {
          scope.onGenerated();
        });
      };

      scope.typeChanged();
    }
  };
}]);
//# sourceMappingURL=../../maps/app/directives/attribute-directive.js.map
