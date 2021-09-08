app.directive 'attribute', ($compile, $q, spiderdocsService, tagService, $interval) ->
	{
		restrict: 'E'
		replace: true
		template:"""
			<div>
			<div class="form-field ">
			<label for="lead-filter-rep" class="form-field__label">Document Type</label>
			<div class="form-field__select form-field__select--blue">
			<select name="lead-filter-rep" id="lead-filter-rep" ng-model="type" ng-options="opt.type for opt in types track by opt.id" ng-change="typeChanged()" ></select>
			</div>
			</div>
			<div class="js-attribute-section"></div>
			</div>
		"""
		scope:
			types: '=tkTypes'
			type: '=tkModel'
			selectedAttr: '=tkSelectedAttr'
			defaultAttrs: '=tkDefaultAttrs'
			onGenerated: '&onGenerated'
		link: (scope, elm, attrs) ->

			genAttrs = (target, typeId, myattrs = []) ->
				args =
					typeMapsAttrs: []
					comboitems: []

				twoDimensional2One = (res) ->
					res = res or []
					items = []
					res.forEach (row) ->
						if row and row.length > 0
							row.forEach (x) ->
								items.push x
								return
						return
					items

				spiderdocsService.GetAttributeDocType(typeId).then (_typeMapsAttrs) ->
					args.typeMapsAttrs = _typeMapsAttrs
					ids = []
					_typeMapsAttrs.forEach (x) ->
						if !ids.includes(x.id_attribute)
							ids.push x.id_attribute
						return
					$q.all ids.map((x) ->
						spiderdocsService.cache.comboitems x
					)

				.then(twoDimensional2One).then (_comboitems) ->
					args.comboitems = _comboitems
					spiderdocsService.cache.attrs()

				.then (_attrs) ->
					tagService.genAttrs target, typeId, args.typeMapsAttrs, _attrs, args.comboitems, myattrs

			scope.type = scope.type or {}
			$interval (->
				scope.selectedAttr = tagService.parseAttributeCriterias(elm.find('.js-attribute-section'))
				return
			), 300

			scope.typeChanged = ->
				genAttrs(elm.find('.js-attribute-section'), scope.type.id or 0, scope.defaultAttrs or []).then ->
					scope.onGenerated()
					return
				return

			scope.typeChanged()
			return

	}
