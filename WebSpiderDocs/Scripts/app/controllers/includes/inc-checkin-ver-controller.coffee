app.controller 'incCheckinVerCtrl', (
	$scope,
	$uibModal,
	spiderdocsService,
	comService,
	en_Actions,
	modalService) ->


	$ctrl = this
	$ctrl.docs = []

	# $ctrl.title = $scope.title
	$scope.title = 'Save as a New Version of an existing document'
	$ctrl.docTypes = $scope.docTypes
	$ctrl.selectedAttr = {}
	$ctrl.canGo = false
	$ctrl.validation = {}
	$ctrl.needReason = true
	$ctrl.reasonMinumLen = 10
	$ctrl.reason = ''
	$ctrl.options =
		saveAsPDF: false
	$ctrl.canOCRable = false;


	

	init = ->

		#selected = (for doc in $ctrl.docs when doc.selected is true)
		ocred = (doc for doc in $ctrl.docs when comService.isOCRable(doc.title))

		$ctrl.canOCRable = $ctrl.docs.length == ocred.length and $ctrl.options.saveAsPDF = true if $scope.settings.userGlobal.ocr and $scope.settings.userGlobal.default_ocr_import is true

		# select all as default 
		for doc in $ctrl.docs
			doc.selected = true

		return
	checkToOpenSearch = () ->
		len = selectedDocs().length

		len is 1 
		


	$ctrl.openSearchDoc =  ->

		if checkToOpenSearch() is false 
			
			modalService.confirm("Error","Please select only one doc.", {hideCancel:true})

		else 
			doc = selectedDocs()[0] # should be one.
			
			spiderdocsService.cache.foldersL1 0, en_Actions.CheckIn_Out
			
			.then (folders) ->

				modalService.searchDocument doc,folders,$ctrl.docTypes,en_Actions.CheckIn_Out,{saveAsPDF: $ctrl.options.saveAsPDF}

			.then (docs) ->

				searched = docs[0]
								
				$ctrl.bindSearchedDoc(searched)

				$ctrl.validateTogo()
	
	selectedDocs = () ->
		(doc for doc in $ctrl.docs when doc.selected is true)

		
	$ctrl.bindSearchedDoc = (searched) -> 
		
		doc = selectedDocs()[0]
		
		doc.verDoc = searched
		
		doc
		


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
		lenSelected = (doc for doc in $ctrl.docs when doc.selected is true ).length

		for ver in $ctrl.docs when ver?.selected isnt true
			delete ver.verDoc

		verDocs = for ver in $ctrl.docs when ver?.verDoc
			ver.verDoc.id_user_workspace = ver.id_user_workspace
			ver.verDoc._ref = ver._ref
			ver.verDoc


		if lenSelected is verDocs.length 

			$scope.$emit 'dialog-savenew-ok', docs: verDocs, func: 'ver', reason: $ctrl.reason, options: $ctrl.options

		else 

			unselected = nameByUnselected($ctrl.docs)

			Notify "You must search and specify for each documents you selected. (#{unselected}) ", null, null, 'info'

		return

	$ctrl.cancel = ->
		# $uibModalInstance.dismiss 'cancel'
		$scope.$emit 'dialog-savenew-close'
		return

	$ctrl.resetverDoc = ->

	
		for doc in $ctrl.docs
			delete doc.verDoc unless doc.extension is doc.verDoc.extension

		$ctrl.validateTogo()
		
		return

		backup = $ctrl.options.saveAsPDF
		# if $ctrl.options.saveAsPDF then '.pdf' else $ctrl.docs[0].extension
		
		modalService.confirm("Confirmation","""This perform reset selected document.
		Would you like to proceed?
		""")
		.then (result) ->
			if result is "ok"
				for doc in $ctrl.docs
					delete doc.verDoc

				
			else
				
				$ctrl.options.saveAsPDF = backup

				for doc in $ctrl.docs
					delete doc.verDoc unless doc.extension is doc.verDoc.extension

				$ctrl.validateTogo()
	
		return

	$ctrl.checklength = ->
		$ctrl.reason.length >= $ctrl.reasonMinumLen

	$ctrl.validateTogo = ->
		$ctrl.canGo = false

		if checkDocSelected()
			if $ctrl.needReason

				if $ctrl.checklength()

					$ctrl.canGo = true

			else
				$ctrl.canGo = true



		$ctrl.canGo

	checkDocSelected = ->
		ok = false

		for doc in $ctrl.docs
				ok = true if doc.verDoc isnt undefined

		ok

	deepcopy = (source, dest) -> 
		
		for doc in source
			_doc = {}
			angular.copy doc, _doc
			_doc._ref = doc._ref
			
			dest.push _doc

		return 

	nameByUnselected = (docs) ->
		unselected = ''
		
		for ver in docs when ver?.verDoc is null 
			unselected += ver.title + ", " 


		unselected = unselected.slice(0,-2)

		unselected


	deepcopy $scope.docs, $ctrl.docs

	init()

	return