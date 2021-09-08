app.controller 'registrationDocumentTypeController',
[
	'$scope'
	'$q'
	'spiderdocsService'
	'comService'
	'$uibModal'
(
	$scope,
	$q,
	spiderdocsService,
	comService,
	$uibModal
) ->

	$scope.selected =
		type: 0

	$scope.data =
		types: []
		attrs: []
		map: []

	$scope.view =
		data:
			attrs:[]
			types:[]
			fieldType:[]

	init = ->
		$q.all([
			spiderdocsService.GetDocumentTypes()
			spiderdocsService.GetAttributes()
			spiderdocsService.GetAttributeDocType()
			spiderdocsService.cache.fieldtypes()
		])
		.then (res) ->
			types = res[0]
			attrs = res[1]
			map = res[2]
			fieldType = res[3]

			$scope.data.types = types
			$scope.data.attrs = attrs
			$scope.data.map = map
			$scope.data.fieldType = fieldType

			$scope.view.data.types = types.sort (a,b) ->  if a.type.toUpperCase() < b.type.toUpperCase() then -1 else 1
			$scope.view.data.attrs = attrs;



			$scope.typeChange(types[0])

			return
		.then ->
			comService.loading false
			return

	$scope.edit = (type) ->
		save('edit',type);

	$scope.new = () ->
		save('new');


	$scope.delete = (doctype) ->

		modalInstance = $uibModal.open(
			animation: true
			ariaLabelledBy: 'modal-title'
			ariaDescribedBy: 'modal-body'
			templateUrl: 'form-confimation.html'
			controller: 'formConfimationCtrl'
			controllerAs: '$ctrl'
			backdrop: 'static'
			size: 'sm'
			resolve:
				title: ->
					'Are you sure to delete selected Document Type?'
				message: ->
					"The action cannot redo once deleted '#{doctype.type}'."
		)

		modalInstance.result.then (result) ->

			if result == 'ok'
				return spiderdocsService.RemoveDocumentTypeAsync( doctype.id ).then((ans) ->

					if ans is true

						$scope.data.types = $scope.data.types.filter (x) ->
							x.id != doctype.id

						angular.copy $scope.data.types, $scope.view.data.types

						Notify "The document Type has been deleted! (#{doctype.type})", null, null, 'success'

					else
						Notify "This Type of Document has already been used in some document. (#{doctype.type})", null, null, 'danger'
					return
				)
			else

			return

		return


	$scope.name_fieldType = (id) ->
		type = item.type for item in $scope.data.fieldType when item.id is id
		type

	$scope.typeChange = (type) ->
		_attrs = []
		$scope.selected.type = type.id

		angular.copy $scope.data.attrs, _attrs

		for attr in _attrs #when attr.id_doc_type is type.id

			attr.checked = false # set default
			attr.duplicate_chk = false # set default

			for map in $scope.data.map when map.id_doc_type is type.id and map.id_attribute is attr.id
				attr.duplicate_chk = map?.duplicate_chk
				attr.checked = true

		$scope.view.data.attrs = _attrs.sort (a,b) ->
				if a.checked is true then -1 else 1

		# $scope.data.map
		# spiderdocsService.GetUserIdInGroupAsync (group.id)
		# .then (ids) ->
		# 	_users = []
		# 	angular.copy $scope.data.attrs, _users

		# 	for user in _users when ids.includes(user.id)
		# 		user.checked = true

		# 	$scope.view.data.attrs = _users.sort (a,b) ->
		# 		if a.checked is true then -1 else 1

	$scope.toggleSelected = (attr,old) ->
		updatedDocumentTypeAttr (attr)
		.then (response)->
			if response is false then attr.checked = old

	$scope.toggleDupSelected = (attr,old) ->
		updatedDocumentTypeAttr (attr)
		.then (response)->
			if response is false then attr.duplicate_chk = old

	updatedDocumentTypeAttr = (attr) ->
		name_type = type.type for type in $scope.data.types when type.id is $scope.selected.type

		id_doc_type = $scope.selected.type
		id_attr = attr.id
		chkAtb = attr.checked
		chkDup = attr.duplicate_chk

		id_attrs = (attr.id for attr in $scope.view.data.attrs when attr.checked is true and attr.duplicate_chk is true)

		# return spiderdocsService.cache.noop(false)

		return spiderdocsService.UpdateDocumentTypeAttributeAsync(id_doc_type, id_attr, chkAtb, chkDup, id_attrs)
		.then (response) ->

			if response is true
				Notify "Attributes have been updated (Type: #{name_type})", null, null, 'success'


				# record = $scope.data.map.find (m) -> m.id_doc_type is id_doc_type and id_attribute is id_attr
				# if not record ?
				# 	$scope.data.map.push(
				# 		id_
				# 	)

			else
				error = """
						Cannot proceed because being duplications after the action by either:

						- duplication with Title in same folder
						- duplication with Title, Type across the folder

						Please, fix document first.
						"""
				Notify error, null, null, 'danger'

			response


	save = (mode, type) ->

		modalInstance = $uibModal.open(
			animation: true
			ariaLabelledBy: 'modal-title'
			ariaDescribedBy: 'modal-body'
			templateUrl: 'scripts/app/views/form-save-documenttype.html'
			controller: 'formSaveDocumentTypeCtrl'
			controllerAs: '$ctrl'
			backdrop: 'static'
			size: 'sm'
			resolve:
				doctype: ->
					type
		)

		modalInstance.result.then (data) ->
			comService.loading true

			doctype =
				id : data.doctype?.id ? 0
				type : data.doctype.type


			spiderdocsService.SaveDocumentTypeAsync doctype.id,doctype.type
			.then (_doctype) ->

				found = $scope.data.types.find ((u) -> u.id is _doctype.id)

				if found?
					angular.extend found, _doctype
				else
					$scope.data.types.push(_doctype)

				if _doctype.id > 0
					Notify "A Document Type has been added #{_doctype.type}", null, null, 'success'
				else
					Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'

				comService.loading false
	# 	return
		# ), ->
		#return

	init()
]
