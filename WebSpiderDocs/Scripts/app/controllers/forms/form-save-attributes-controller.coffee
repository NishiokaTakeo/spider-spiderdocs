app.controller 'formSaveAttributesCtrl',
[
	'$scope'
	'$uibModal'
	'$uibModalInstance'
	'spiderdocsService'
	'comService'
	'attribute'
	'types'
	'en_Actions'
	'modalService'
(
	$scope
	$uibModal
	$uibModalInstance
	spiderdocsService
	comService
	attribute
	types
	en_Actions
	modalService
) ->


	init = ->
		$scope.title = if not attribute? then '[New Attribute]' else '[Edit Attribute - ' + attribute.name + ']'

		$scope.model =
			name:''
			type: findTypeBy(attribute?.id_type)
			max_lengh: 20
			only_numbers: false
			required: false
			position:1
			parameters:[]
			selectedFolder: {id:0,text:'--- Default ---'}

		if attribute?
			angular.extend $scope.model, attribute
			$scope.model.type = findTypeBy(attribute?.id_type)
			$scope.model.parameters = convertParameters(attribute.parameters)

		if not findParameter(0)?
			addNewRequire(0)

		return

	$scope.checkName = () ->
		$scope.model.name = '' if not $scope.model.name?
		$scope.model.name.length > 0


	validateTogo = ->
		$scope.canGo = false

		for control in $scope.myform.$$controls
			control.$setDirty()
			control.$validate()

		if $scope.myform.$invalid is true
			Notify('Can not proceed. <br/>Please Fix error(s)', null, null, 'danger');

			return

		$scope.canGo = true


	$scope.openFolderTree = (folders = []) ->
		spiderdocsService.cache.foldersL1(0, en_Actions.None).then((_folders) ->

			nodes = comService.nodesBy 0,_folders
			
			modalService.openfolder nodes,en_Actions.None

			# $uibModal.open(
			# 	animation: true
			# 	ariaLabelledBy: 'modal-title'
			# 	ariaDescribedBy: 'modal-body'
			# 	templateUrl: 'scripts/app/views/form-folder-tree.html'
			# 	controller: 'formFolderTreeCtrl'
			# 	controllerAs: '$ctrl'
			# 	backdrop: 'static'
			# 	size: 'lg'
			# 	resolve:
			# 		folders: ->
			# 			comService.nodesBy 0, _folders
			# 		permission: ->
			# 			en_Actions.None
			# ).result

		).then ((folder = {}) ->

			$scope.model.name_folder = folder.text
			$scope.model.selectedFolder = folder

			#update mandatory
			if not findParameter(folder.id)?
				addNewRequire(folder.id)

			restoreRequire(folder.id)

			return
		), ->
		return

	$scope.ok = ->
		if not $scope.validateTogo()
			return

		deepCopiedAttr = angular.extend {}, attribute
		angular.extend deepCopiedAttr , $scope.model

		parameter =
			id_folder: $scope.model.selectedFolder.id
			required: $scope.model.required

		for param in $scope.model.parameters
			param.required = if param.required is true then 1 else 0

		deepCopiedAttr.parameters = $scope.model.parameters.filter (p) -> p.required is 1 or parseInt(p.id_folder) is 0

		data =
			attribute: deepCopiedAttr

		$uibModalInstance.close data
		return

	$scope.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$scope.validateTogo = validateTogo

	$scope.updateRequire = () ->

		id_folder = $scope.model.selectedFolder?.id ? 0

		found = findParameter(id_folder)

		if not found?
			addNewRequire(id_folder)

		found.required = $scope.model.required

	restoreRequire = (id_folder) ->

		found = findParameter(id_folder)

		$scope.model.required = found.required

	findParameter = (id_folder) ->
		found = $scope.model.parameters.find (p) ->
				p.id_folder is id_folder

		found

	addNewRequire = (id_folder) ->
		$scope.model.required = false # set to default
		adding = {id_folder: id_folder, required: false}
		$scope.model.parameters.push(adding)

	convertParameters = (params) ->
		for p in params
			p.required = if p.required is 1 then true else false
			p.id_folder = p.id_folder.toString()

		params

	findTypeBy = (id)->
		ans
		if id?
			ans = type for type in $scope.data.types when type.id is id
		else
			ans = $scope.data.types[0]

		ans

	$scope.model = {}
	$scope.data =
		types: types



	init()
	return
]