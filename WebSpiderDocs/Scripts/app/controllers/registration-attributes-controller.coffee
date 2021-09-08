app.controller 'registrationAttributesController',
[
	'$scope'
	'$q'
	'spiderdocsService'
	'comService'
	'$uibModal'
(
	$scope
	$q
	spiderdocsService
	comService
	$uibModal
) ->

	$scope.data =
		attributes: []
		types: []

	$scope.model =
		type: {}

	init = ->

		types = []
		attrs = []

		spiderdocsService.cache.clear('db-attrs');

		$q.all [
			spiderdocsService.cache.fieldtypes()
			spiderdocsService.cache.attrs()
		]
		.then (res) ->
			types =  res[0]
			attrs = res[1]

			for opt in attrs
				opt.required_bool = opt.required is 1
				opt.only_number_bool = opt.only_numbers is 1

			folderIds = []
			for a in attrs
				for p in a.parameters when parseInt(p.id_folder) > 0
					folderIds.push p.id_folder

			spiderdocsService.SearchFoldersAsync(folderIds)

		.then (folders) ->
			$scope.folders = folders

			for a in attrs
				a.mandatoryList = $scope.mandatoryList(a)

			$scope.data.types = types
			$scope.data.attributes = attrs

		.then ->
			comService.loading false
			return


	$scope.mandatoryList = (attribute) ->
		ans = []
		# ids = (p.id_folder for p in attribute.parameters)
		if attribute.id is 83
			debugger

		for p in attribute.parameters
			found = $scope.folders.find (f) ->
				f.id is parseInt(p.id_folder)

			if found?
				ans.push
					name_folder: found.document_folder
					required: if p.required is 1 then true else false

		#ans
		console.log ans
		ans


	$scope.find  = (db, key, dbkey) ->

		if dbkey?
			ans = opt for opt in db when opt[dbkey] is key
		else
			ans = opt for opt in db when opt.id is key

		ans

	$scope.edit = (attribute) ->
		save('edit',attribute);
		return

	$scope.new = () ->
		save('new');

	save = (mode,attr) ->
		modalInstance = $uibModal.open(
			animation: true
			ariaLabelledBy: 'modal-title'
			ariaDescribedBy: 'modal-body'
			templateUrl: 'scripts/app/views/form-save-attributes.html'
			controller: 'formSaveAttributesCtrl'
			controllerAs: '$ctrl'
			backdrop: 'static'
			size: 'sm'
			resolve:
				attribute: ->
					attr
				types:
					spiderdocsService.cache.fieldtypes()

				# permissionLevels: ()->
				# 	spiderdocsService.GetPermissionLevels()

		)

		modalInstance.result.then ((data) ->
			comService.loading true

			attribute =
				id : data.attribute?.id ? 0
				name : data.attribute.name
				id_type : data.attribute.type.id
				max_lengh : data.attribute.max_lengh
				only_numbers : if data.attribute.only_numbers is 1 or data.attribute.only_numbers is true then 1 else 0
				# required : if data.attribute.required is 1 or data.attribute.required is true then 1 else 0
				period_research: data.attribute.period_research ? 90
				appear_query: data.attribute.appear_query ? true
				appear_input: data.attribute.appear_input ? true
				parameters: data.attribute.parameters

			spiderdocsService.SaveAttributeAsync attribute
				.then (_attribute) ->

					found = $scope.data.attributes.find ((u) -> u.id is _attribute.id)

					if found?
						angular.extend found, _attribute
					else
						$scope.data.attributes.push(_attribute)

					if _attribute.id > 0
						Notify 'A attribute has been added', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'

					comService.loading false
			return
		), ->
		return


	init()
]
