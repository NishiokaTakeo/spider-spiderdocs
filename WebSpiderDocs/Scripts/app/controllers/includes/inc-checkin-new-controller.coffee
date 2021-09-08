app.controller 'incCheckinNewCtrl', (
$scope,
$uibModal,
spiderdocsService,
comService,
en_Actions,
modalService
) ->

	$ctrl = this
	$ctrl.docs = []

	# $ctrl.title = $scope.title
	$scope.title = 'Save as a New File'
	$ctrl.docTypes = $scope.docTypes
	$ctrl.selectedAttr = {}
	$ctrl.canGo = false
	$ctrl.validation = {}
	$ctrl.options =
		saveAsPDF: false

	$ctrl.canOCRable = false;
	$ctrl.notificationgroups = $scope.notificationgroups

	# angular.copy $scope.docs, $ctrl.docs
	


	init = ->

		#selected = (for doc in $ctrl.docs when doc.selected is true)
		ocred = (doc for doc in $ctrl.docs when comService.isOCRable(doc.title))

		$ctrl.canOCRable = $ctrl.docs.length == ocred.length and $ctrl.options.saveAsPDF = true if $scope.settings.userGlobal.ocr and $scope.settings.userGlobal.default_ocr_import is true

		return

	bindTitle = (docs) ->
		docs.forEach (x) ->
			x.title = x.title_withoutExt + x.extension
			return
		return

	bindType = (docs) ->
		idType = if $ctrl.type then $ctrl.type.id else 0
		docs.forEach (x) ->
			x.id_docType = idType
			return
		return

	bindAttr = (docs) ->
		$ctrl.selectedAttr = $ctrl.selectedAttr or []
		docs.forEach (x) ->
			x.attrs = $ctrl.selectedAttr
			return
		return

	bindNotificationGrp = (docs) ->
		items = $ctrl.notificationgroups or []
		selected = (opt.id for opt in items when opt.checked is true)
		docs.forEach (x) ->
			x.id_notification_group = selected
			return

		return

	openFolderTree = (folders) ->
		spiderdocsService.cache.foldersL1(0, en_Actions.CheckIn_Out).then((folders) ->
			
			nodes = comService.nodesBy 0,folders
			
			modalService.openfolder nodes,en_Actions.CheckIn_Out
			
		).then ((folder = {}) ->
			$ctrl.name_folder = folder.text
			$ctrl.validation.folder = folder
			angular.forEach $ctrl.docs, (item, key) ->
				item.id_folder = folder.id
				return
			$ctrl.validateTogo()
			return
		), ->
		return

	checkfolder = ->
		hasError = $ctrl.docs.filter((doc) ->
			doc.id_folder <= 0
			return
		)
		hasError.length == 0

	$ctrl.folderclicked = ->
		openFolderTree []
		return

	$ctrl.ok = ->
		bindTitle $ctrl.docs
		bindType $ctrl.docs
		bindAttr $ctrl.docs
		bindNotificationGrp $ctrl.docs
		$scope.$emit 'dialog-savenew-ok', docs: $ctrl.docs, func: 'new', options: $ctrl.options
		return

	$ctrl.cancel = ->
		# $uibModalInstance.dismiss 'cancel'
		$scope.$emit 'dialog-savenew-close'
		return

	$ctrl.validateTogo = ->
		$ctrl.canGo = false
		if checkfolder()
			$ctrl.canGo = true
		$ctrl.canGo


	deepcopy = (source, dest) -> 
		
		for doc in source
			_doc = {}
			angular.copy doc, _doc
			_doc._ref = doc._ref
			
			dest.push _doc

		return 


	deepcopy $scope.docs, $ctrl.docs

	init()

	return
