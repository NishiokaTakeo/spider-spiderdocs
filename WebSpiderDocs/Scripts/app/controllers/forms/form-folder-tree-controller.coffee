app.controller 'formFolderTreeCtrl',
[
	'$uibModalInstance'
	'folders'
	'permission'
	'en_Actions'
	'menu'
	'$scope'
	'modalService'
	'spiderdocsService'
	'canCreate'
	'comService'
(
	$uibModalInstance,
	folders,
	permission,
	en_Actions
	menu
	$scope
	modalSrv
	spiderDocsSrv
	canCreate
	comService 
) ->
	$ctrl = this

	init = ->
		# bindDragEvents();
		return

	$ctrl.folders = folders
	$ctrl.permission = permission or en_Actions.CheckIn_Out
	# $ctrl.needReason = reasonNewVersion;
	# $ctrl.file = undefined;
	$ctrl.canGo = false
	# $ctrl.reason = '';
	$ctrl.selected = {}
	$ctrl.canCreate = canCreate
  

	$ctrl.menu = menu or {}
	

	# $ctrl.createFolder = (attr, scope) => 
	# 	console.log attr,scope
	$ctrl.createAtRoot = () ->
		typed = ''
		cached = []
		
		modalSrv.input "Create new folder","Folder Name" 

		.then (text) ->
			# console.log text
			typed = text 	
	
			spiderDocsSrv.cache.foldersL1()
		
		.then (folders) -> 
			cached = folders

			spiderDocsSrv.SaveFolder 
				document_folder: typed 

		.then (folder) ->

			found = _folder for _folder in cached when _folder.document_folder is typed
			
			if found?

				Notify "Rejected. It has been already there. (#{typed}) ", null, null, 'danger'

			else 

				if folder.id > 0

					nodes = comService.nodesBy 0, [folder]
					$ctrl.folders.push( nodes[0] )

					Notify 'Folder has been created', null, null, 'success'

				else 
					Notify 'Rejected. You might not have right permission to do that.', null, null, 'danger'
				
			spiderDocsSrv.cache.clearAll()

			

	init()

	$ctrl.ok = ->
		$uibModalInstance.close $ctrl.selected
		return

	$ctrl.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$ctrl.validateTogo = ->
		$ctrl.canGo = false
		selected = angular.element('.container--tree').find('[data-selected-folder-id]')
		node =
			id: selected.attr('data-selected-folder-id')
			text: selected.attr('data-text')
		$ctrl.selected = node
		$ctrl.canGo = parseInt(node.id) > 0
		$ctrl.canGo

	return
]