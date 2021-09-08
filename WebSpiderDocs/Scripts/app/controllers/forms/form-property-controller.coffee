app.controller 'FormPropertyCtrl', [
	'$scope'
	'$timeout'
	'$uibModalInstance'
	'spiderdocsService'
	'comService'
	'$uibModal'
	'tagService'
	'doc'
	'types'
	'attrs'
	'comboitems'
	'en_file_Status'
	'en_Actions'
	'notificationgroups'
	'customviews'
	'customviewdocuments'
	'modalService'
	(
	$scope
	$timeout
	$uibModalInstance
	spiderdocsService
	comService
	$uibModal
	tagService
	doc
	types
	attrs
	comboitems
	en_file_Status
	en_Actions
	notificationgroups
	customviews
	customviewdocuments
	modalService
	) ->
		$ctrl = this
		$ctrl.notificationgroups = notificationgroups
		$ctrl.data =
			customviews: []

		init = ->
			# $ctrl.data.customviews = customviews

			for view in customviews

				checked = (vdoc for vdoc in customviewdocuments when vdoc.id_custom_view is view.id)

				$ctrl.data.customviews.push
					id: view.id
					text: view.name
					# checked: checked.length > 0

				if checked.length > 0
					$ctrl.customview = $ctrl.data.customviews[$ctrl.data.customviews.length - 1 ]

				# $ctrl.data.customviews.push
				# 	id: view.id
				# 	text: view.name
				# 	checked: checked.length > 0

			$ctrl.data.customviews.unshift
				id:0
				text: 'Local Database'

				# checked = false
			if 	not $ctrl.customview?
				# $ctrl.customview = undefined
				$ctrl.customview = $ctrl.data.customviews[0]


			$ctrl.defaultAttrs = $ctrl.doc.attrs
			validateTogo()

			return

		$ctrl.attrchanged = () =>
			disabledIf()

		validateTogo = ->
			$ctrl.canGo = false
			if checkfolder()
				$ctrl.canGo = true
			$ctrl.canGo

		openFolderTree = (folders) ->
			spiderdocsService.cache.foldersL1(0, en_Actions.CheckIn_Out).then((folders) ->

				nodes = comService.nodesBy 0,folders
				
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
				# 			comService.nodesBy 0, folders
				# 		permission: ->
				# 			en_Actions.CheckIn_Out
				# ).result

			).then ((folder = {}) ->
				$ctrl.name_folder = folder.text
				$ctrl.doc.id_folder = folder.id
				$ctrl.doc.name_folder = folder.text
				$ctrl.validateTogo()
				return
			), ->
			return

		checkfolder = ->
			hasError = $ctrl.doc.id_folder <= 0
			!hasError

		disabledIf = ->
			if doc.id_status is en_file_Status.checked_out
				angular.element('.modal--property').find('input, select, textarea').prop 'disabled', true
			return

		updateDoc = ->
			_doc = {}
			angular.copy $ctrl.doc, _doc

			_doc.title = $ctrl.name
			_doc.id_folder = $ctrl.doc.id_folder
			_doc.id_docType = $ctrl.type.id
			_doc.attrs = $ctrl.selectedAttrs
			_doc.id_notification_group = ( opt.id for opt in $ctrl.notificationgroups when opt.checked is true)

			# _doc.customviews = ( item for item in $ctrl.data.customviews when item.checked is true )
			# _doc.customviews = if $ctrl.customview? and $ctrl.customview.id isnt 0 then _doc.customviews = [$ctrl.customview] else []
			_doc.customviews = if $ctrl.customview? then _doc.customviews = [$ctrl.customview] else []


			_doc


		$ctrl.doc = {}
		angular.copy doc, $ctrl.doc
		$ctrl.types = types
		$ctrl.type = types.find((x) ->
			x.id == doc.id_docType
		) or id: 0
		$ctrl.en_file_Status = en_file_Status
		$ctrl.checkout = doc.id_status == en_file_Status.checked_out
		$ctrl.canGo = false
		$ctrl.selectedAttrs = {}
		$ctrl.defaultAttrs = {}
		init()

		# $ctrl.customviewchanged = () ->
		# 	a = 'a'

		$ctrl.folderclicked = ->
			openFolderTree []
			return

		$ctrl.ok = ->
			reflectedChange = updateDoc()
			$uibModalInstance.close reflectedChange
			return

		$ctrl.cancel = ->
			$uibModalInstance.dismiss 'cancel'
			return

		$ctrl.validateTogo = validateTogo
		return
	]