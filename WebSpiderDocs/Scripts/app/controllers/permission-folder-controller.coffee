app.controller 'permissionFolderController',
[
	'$scope'
	'$q'
	'spiderdocsService'
	'comService'
	'$uibModal'
	'en_Actions'
	'en_FolderPermission'
	'modalService'
(
	$scope,
	$q,
	spiderdocsService,
	comService,
	$uibModal,
	en_Actions,
	en_FolderPermission
	modalService
) ->

	# $scope.rootMenu = 
	# 	[
	# 		label: 'Create Folder'
	# 		action: 'createFolder'
	# 		active: true
	# 	]

	$scope.selected =
		type: 0

	$scope.data =
		permissionTitles: []
		types: []
		attrs: []
		map: []
		# allowed: []
		users: []
		menu:{ items:[], func:{} }

	$scope.view =
		search: {}
		inheritance:
			is: false
			from: 'NONE'
		data:
			attrs:[]
			types:[]
			fieldType:[]
			allowed: []
			users: []
			permissions: []


	$scope.search = {}
	# $scope.explorer =
	# 	folders: []
	# 	folder_default: 0



	init = ->
		$q.all([
			spiderdocsService.GetGroupsAsync()
			spiderdocsService.GetAttributes()
			spiderdocsService.GetAttributeDocType()
			spiderdocsService.cache.fieldtypes()
			spiderdocsService.cache.users()
			spiderdocsService.cache.permissiontitles()
		])
		.then (res) ->
			group = res[0]
			attrs = res[1]
			map = res[2]
			fieldType = res[3]
			users = res[4]
			pertitles = res[5]

			$scope.data.types = group
			$scope.data.attrs = attrs
			$scope.data.map = map
			$scope.data.fieldType = fieldType
			$scope.data.users = users
			$scope.data.permissionTitles = pertitles

			$scope.view.data.groups = group.sort (a,b) ->  if a.group_name.toUpperCase() < b.group_name.toUpperCase() then -1 else 1
			$scope.view.data.attrs = attrs;



			$scope.groupChange(group[0])

			setupMenu()

			return
		.then ->
			comService.loading false
			return



	$scope.openSearchFolderForm = ->

		# spiderdocsService.cache.foldersL1(0, en_Actions.OpenRead).then((folders) ->
		spiderdocsService.cache.foldersL1(0).then((folders) ->
			nodes = comService.nodesBy 0,folders
			
			modalService.openfolder nodes,en_Actions.OpenRead,$scope.data.menu, true

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
			# 			en_Actions.OpenRead
			# 		menu: ->
			# 			$scope.data.menu
			# ).result
		).then (folder) ->
			if folder == undefined
				folder = {}

			$scope.view.search.name_folder = folder.text
			$scope.view.search.id_folder = folder.id

			# Updat Allowed and Permission section
			refreshDisplayOn(folder.id);


		return

	refreshDisplayOn = (id_folder) ->
		$q.all	[
				spiderdocsService.GetUsersByAsync {idFolder: id_folder}
				spiderdocsService.GetGroupsByAsync {idFolder: id_folder}
				spiderdocsService.GetInheritedFolderName(id_folder)
				spiderdocsService.SearchFoldersAsync([id_folder])
				]

		.then (data) ->
			inheritedFrom = data[2]
			folders = data[3]

			$scope.view.data.allowed = []

			for item in data[0]
				$scope.view.data.allowed.push {id:item.id, checked:false, name: item.name, type:'user'}

			for item in data[1]
				$scope.view.data.allowed.push {id:item.id, checked:false, name: item.group_name, type:'group'}

			$scope.view.data.permissions = []

			inherited = if not inheritedFrom? or inheritedFrom.length == 0 or inheritedFrom.toLowerCase() is 'none' then 'NONE' else inheritedFrom
			$scope.view.search.id_parent = folders[0].id_parent

			$scope.updateInherited(inherited);


	$scope.addGroupToFolder = (group) ->

		addToFolder('group',group);

	$scope.addUserToFolder = (user) ->
		addToFolder('user',user);

	# $scope.new = () ->
	# 	save('new');

	$scope.toggleInheritance = () ->
		spiderdocsService.GetInheritanceFolder($scope.view.search.id_folder)
		.then (folder) ->

			if folder.id is $scope.view.search.id_folder

				# // (Enable inheritance)

				message = 	"The folder \"#{ $scope.view.search.document_folder}\" will be INHERITED from a parent.\n\nAre you sure to make this?"
			else

				# // (Disable inheritance)

				message = 	 "The folder\" #{ $scope.view.search.document_folder}\" will NOT be inherit.\n\n Are you sure to make this?"


			modalInstance = $uibModal.open
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
						'Cannot redu'
					message: ->
						message


			modalInstance.result.then ((result) ->
				if result == 'ok'

					# // Delete current folder's permission
					spiderdocsService.ToggleInheritance $scope.view.search.id_folder
					.then () ->
						# PermissionController.DeleteAllPermission curFolder.id

						clearPermissions();

						refreshDisplayOn($scope.view.search.id_folder)

						# DisableEditableCtrs();


				return
			), ->
				# finish logic.

				# $log.info('Modal dismissed at: ' + new Date());
				return

			return


	$scope.updateInherited = (inherited) ->

		$scope.view.inheritance.from = inherited

		$scope.view.inheritance.is = inherited isnt '' and inherited isnt 'NONE'

	$scope.delete = (grpOrUsr) ->

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
					'Are you sure to delete selected item?'
				message: ->
					"The action cannot redo once deleted '#{grpOrUsr.name}'."
		)

		modalInstance.result.then (result) ->

			if result == 'ok'
				return spiderdocsService.DeletePermissionAsync( grpOrUsr.type, $scope.view.search.id_folder, grpOrUsr.id ).then((ans) ->

					if ans is ''

						$scope.view.data.allowed = $scope.view.data.allowed.filter ((u) -> not (u.type is grpOrUsr.type and u.id is grpOrUsr.id))


						Notify "Deleted successfully #{grpOrUsr.name}", null, null, 'success'

					else
						Notify ans, null, null, 'danger'

					comService.loading false
					return
				)
			else

			return

		return
	$scope.perNameBy = (id) ->
		d = $scope.data.permissionTitles
		permission = d[key] for key in Object.keys(d) when key.toLowerCase() is id.toString().toLowerCase()
		permission

	$scope.isDeny = (item) ->

		switch item
			when -1 then true
			when 2 then true
			else false

	$scope.isAllow = (item) ->

		switch item
			when 1 then true
			when 2 then true
			else false



	$scope.name_fieldType = (id) ->
		type = item.type for item in $scope.data.fieldType when item.id is id
		type

	$scope.groupChange = (group) ->

		# users = []
		$scope.view.data.users = []

		# angular.copy $scope.data.users, users


		spiderdocsService.GetUserIdInGroupAsync group.id
		.then (userIds) ->

			for user in $scope.data.users when userIds.includes(user.id)
				$scope.view.data.users.push(user)

		# _attrs = []
		# $scope.selected.group = group.id

		# angular.copy $scope.data.attrs, _attrs

		# for attr in _attrs #when attr.id_doc_group is group.id

		# 	attr.checked = false # set default
		# 	attr.duplicate_chk = false # set default

		# 	for map in $scope.data.map when map.id_doc_group is group.id and map.id_attribute is attr.id
		# 		attr.duplicate_chk = map?.duplicate_chk
		# 		attr.checked = true

		# $scope.view.data.attrs = _attrs.sort (a,b) ->
		# 		if a.checked is true then -1 else 1

		# $scope.data.map
		# spiderdocsService.GetUserIdInGroupAsync (group.id)
		# .then (ids) ->
		# 	_users = []
		# 	angular.copy $scope.data.attrs, _users

		# 	for user in _users when ids.includes(user.id)
		# 		user.checked = true

		# 	$scope.view.data.attrs = _users.sort (a,b) ->
		# 		if a.checked is true then -1 else 1

	$scope.toggleSelected = (item,old) ->
		checked = not item.checked

		if $scope.view.inheritance.from.toLowerCase() isnt 'none'
			clearPermissions()

			return

		for _item in $scope.view.data.allowed
			_item.checked = false

		item.checked = checked

		$scope.view.data.permissions = []

		# Populate Permissions
		spiderdocsService.GetPermissionsByGroupAndUser item.type, $scope.view.search.id_folder, item.id
		.then (permissions) ->

			for key in Object.keys(en_Actions)
				if 0 < en_Actions[key] < 100
					perfund = per for per in Object.keys(permissions) when per.toLowerCase() is key.toLowerCase()

					val = if perfund? then permissions[perfund] else 0
					# per = if perfund? then perfund[key] else en_Actions[key]

					# val = permissions[per]
					$scope.view.data.permissions.push
						key: key
						keyId: en_Actions[key]
						name: $scope.perNameBy(key)
						allow: $scope.isAllow(val)
						deny: $scope.isDeny(val)
						# permission:


			$scope.view.data.permissions


	$scope.permissionChanged = (type, per) ->

		permissions = permissionAPIParams()

		selected = a for a in $scope.view.data.allowed when a.checked is true

		apiCall = switch selected.type
			when 'group' then spiderdocsService.AddPermissionToGroup($scope.view.search.id_folder, selected.id, permissions)

			when 'user'  then spiderdocsService.AddPermissionToUser($scope.view.search.id_folder, selected.id, permissions)

		apiCall.then (res) ->
			Notify "Permission has been updated", null, null, 'success'

			spiderdocsService.clearAll 'db-folder'

	setupMenu = () =>
		
		#Menu Items Array
		$scope.data.menu = 
			items : [
				{
					label: 'Rename'
					action: 'renameFolder'
					active: true
				}
				{
					label: 'Add'
					action: 'addFolder'
					active: true
				}
				{
					label: 'Remove'
					action: 'removeFolder'
					active: true
				}
			]
			func:
				renameFolder: renameFolder
				addFolder: addFolder
				removeFolder: removeFolder



	en_FolderPermissionFom = (per) ->

		ans = if per.allow is false and per.deny is false then 0
		else if per.allow is true and per.deny is true then 2
		else if per.deny is true then -1
		else if per.allow is true then 1
		else 0;

	permissionAPIParams = () ->
		ans = {}
		# row = {}
		for p in $scope.view.data.permissions
			ans[p.keyId] = en_FolderPermissionFom(p)
			# ans.push row
			# row = {}
		ans

	updatePermission = (attr) ->
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


	addToFolder = (mode, grpOrUsr) ->

		if not $scope.view.search.id_folder? or  $scope.view.search.id_folder is 0
			Notify "Select folder first.", null, null, 'warning'
			return

		comService.loading true

		spiderdocsService.AddGroupOrUserToFolder mode, $scope.view.search.id_folder, grpOrUsr.id
		.then () ->
			comService.loading false

			exists = allowed for allowed in $scope.view.data.allowed when allowed.type is mode and allowed.id is grpOrUsr.id

			if exists?.id > 0
				return

			$scope.view.data.allowed = $scope.view.data.allowed.filter ((u) -> not (u.type is mode and u.id is grpOrUsr.id))

			$scope.view.data.allowed.push
				checked: false
				name: if mode is 'group' then grpOrUsr.group_name else grpOrUsr.name
				type: mode
				id: grpOrUsr.id

			if grpOrUsr.id > 0
				Notify "Added successfully #{(if mode is 'group' then grpOrUsr.group_name else grpOrUsr.name)}", null, null, 'success'
			else
				Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'


	clearPermissions = () ->
		$scope.view.data.permissions = []


	renameFolder = (arg, scope) ->
		
		console.log "renameFolder (#{scope.node.text}) is clicked."

		node = scope.node
		orign = scope.node.text


		console.debug scope.node

		modalService.input('Rename', '', node.text)

		.then (newname) ->
			
			if node.text is newname
				Notify 'Cancelled. New file name is same as original. ', null, null, 'warning'
				return

			comService.loading true
			node.text= newname;
			node.ref.document_folder = newname;

			if  node?.Id > 0 

				spiderdocsService.SaveFolder(node.ref).then((updated) ->
 
					if updated.id is node.ref.id and updated.document_folder is newname
						

						Notify 'The file has been renamed. \'' + oldName + '\' -> \'' + newname + '\'', null, null, 'success'
 						
						 #remove folder only
						spiderdocsService.cache.clearAll('db-folder')
					else
						
						Notify 'Rejected. You might not have right permission to do that OR there is same name.', null, null, 'danger'

						node.text = orign # back to orignal folder name

					comService.loading false 

					return
				).catch ->

					comService.loading false

					return

			return


	addFolder = (arg, scope) ->
		console.log 'addFolder is clicked'

		node = scope.node

		modalService.input('Rename', '', '').then (newname) ->
			
			found = item for item in node.children when item.text is newname
			if found?
				Notify 'Rejected. There is same name.', null, null, 'danger'

				return

			newnode = 
				id_parent: node.ref.id
				document_folder: newname

			spiderdocsService.SaveFolder(newnode).then((updated) ->

				if updated.id > 0 and updated.document_folder is newname
					

					Notify 'The file has been added. \'' + newname + '\' ', null, null, 'success'
					
					foldernode = comService.nodesBy(node.ref.id,[updated])
					
					node.children.push(foldernode[0])
					
					spiderdocsService.cache.clearAll();

				else
					
					Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'

				comService.loading false 

				return
			).catch ->

				comService.loading false

				return

	

	removeFolder = (arg, scope) ->
		console.log 'removeFolder is clicked'

		node = scope.node
		
		modalService.confirm('Spider Docs', 'Are you sure you want to delete this folder? ( This action will cancel the check out )')

		.then (result) ->

			if result is 'ok'
				comService.loading true

				spiderdocsService.RemoveFolderAsync(node.Id).then (ok) ->
					
					if ok is true
	
						Notify "The foler #{node.text} has been removed.", null, null, 'success'
						scope.node = null;

						spiderdocsService.cache.clearAll();						
					else
						
						Notify "Rejected. You might not have right permission to do that .", null, null, 'danger'

					comService.loading false 

		return

	init()
]

