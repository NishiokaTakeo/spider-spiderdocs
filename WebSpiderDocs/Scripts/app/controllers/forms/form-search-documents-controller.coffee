app.controller 'formSearchDocumentsCtrl', (
	$scope,
	$uibModalInstance,
	$uibModal,
	# folders,
	spiderdocsService,
	comService,
	docTypes,
	en_Actions,
	extension
	modalService
	) ->

	$ctrl = this
	$ctrl.search = {}
	$ctrl.docs = []
	$ctrl.title = "Search document Form"
	$ctrl.docTypes = docTypes
	$ctrl.selectedAttr = []
	$ctrl.canGo = false
	$ctrl.validation = {}
	$ctrl.extension = extension



	init = ->
		$ctrl.searchDoc()
		return

	$ctrl.searchDoc = ->

		criteria =
			AttributeCriterias:
				Attributes: []

		attrs =  [].concat $ctrl.selectedAttr
		attrvalues = []
		docid = $ctrl.search.id
		docname = $ctrl.search.title
		folderid = $ctrl.search.id_folder
		type = $ctrl.search.type

		criteria.DocIds = [docid] if docid
		criteria.Titles = [docname] if docname
		criteria.FolderIds = [folderid] if folderid
		criteria.DocTypeIds = [type] if type
		criteria.Extensions = [extension] if extension

		attrs = ( Values: item for item in attrs when item.atbValue isnt '')
		criteria.AttributeCriterias.Attributes = attrs

		return if ! criteria


		comService.loading true

		spiderdocsService.GetDocuments criteria
			.then (docs) ->
				$ctrl.docs = docs
				return
			.then ->
				comService.loading false

		return

	$ctrl.selectChanged = (doc) ->

		_doc.selected = false for _doc in $ctrl.docs when _doc.id isnt doc.id

		$ctrl.validateTogo()

		return

	openFolderTree = ->
		spiderdocsService.cache.foldersL1(0, en_Actions.CheckIn_Out).then((_folders) ->

			nodes = comService.nodesBy 0,_folders
			
			modalService.openfolder nodes,en_Actions.CheckIn_Out

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
			# 			en_Actions.CheckIn_Out

			# ).result

		).then ((folder = {}) ->
			$ctrl.search.name_folder = folder.text
			# $ctrl.validation.folder = folder
			$ctrl.search.id_folder = folder.id

			# angular.forEach $ctrl.docs, (item, key) ->
			# 	item.id_folder = folder.id

			return

			$ctrl.validateTogo()
		), ->
		return

	checkfolder = ->
		hasError = $ctrl.docs.filter(doc) ->
			doc.id_folder <= 0

		hasError.length == 0

	$ctrl.folderclicked = ->
		openFolderTree []
		return

	$ctrl.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$ctrl.ok = ->
		$uibModalInstance.close (_doc for _doc in $ctrl.docs when _doc.selected is true)
		return


	$ctrl.validateTogo = ->

		$ctrl.canGo = false

		$ctrl.canGo = (_doc for _doc in $ctrl.docs when _doc.selected is true).length > 0

		# if checkfolder()
		# 	$ctrl.canGo = true

		$ctrl.canGo

		return


	init()

	return