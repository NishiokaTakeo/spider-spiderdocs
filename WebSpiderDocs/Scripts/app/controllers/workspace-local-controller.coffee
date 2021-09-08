app.controller 'workspaceLocalController', ( 
	$scope,
	$q,
	$interval,
	spiderdocsService,
	comService,
	$window,
	$sessionStorage,
	_,
	moment,
	$http,
	$uibModal) ->

		init = ->
			events()
			$interval reload, 1000 * 10
			queryDocs().then((res) ->

				format(res)

				$scope.data.docs = res
				return
			).then ->
				comService.loading false
				return

		canRename = (arg, scope) ->
			wfile = scope.doc
			wfile.id_version == 0

		checkin4ver = (doc, settings) ->
			modalInstance = $uibModal.open(
				animation: true
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'scripts/app/views/form-reason.html'
				controller: 'formReasonCtrl'
				controllerAs: '$ctrl'
				backdrop: 'static'
				size: 'sm'
				resolve:
					title: ->
						'[Save New Version]: ' + doc.filename
					message: ->
						'ID: ' + doc.id_doc + ',     Version: ' + (doc.version + 1)
					reasonRequired: ->
						settings.public.reasonNewVersion
					reasonMinLen: ->
						if settings.public.reasonNewVersion then 10 else 0
			)
			modalInstance.result.then ((reason) ->
				comService.loading true
				spiderdocsService.CheckInAsNewVerAsync(doc.id, reason).then((_verDoc) ->
					if _verDoc.id == doc.id
						$scope.data.docs = $scope.data.docs.filter((x) ->
							x.id != doc.id
						)
						Notify 'The file has been checked in. ', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'
					comService.loading false
					return
				).catch ->
					comService.loading false
					return
				return
			), ->
			return

		saveAsNew = (wdocs, settings) ->

			modalInstance = $uibModal.open(
				animation: true
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'scripts/app/views/form-save-new.html'
				controller: 'formSaveNewCtrl'
				controllerAs: '$ctrl'
				backdrop: 'static'
				size: 'lg'
				resolve:
					docs: ->
						convWfile2Doc wdocs
					title: ->
						'[Save New File]'
					docTypes: ->
						spiderdocsService.cache.doctypes()
					settings: ->
						spiderdocsService.cache.settings()
					notificationgroups: ->

						spiderdocsService.cache.notificationgroups().then( (source) ->
							source.map((_source) ->

								id : _source.id
								text : _source.group_name
								checked : false

							)
						)

			)
			modalInstance.result.then ((data) ->
				wfiles = data.docs
				func = data.func
				reason = data?.reason
				options = data?.options

				promise = {}
				comService.loading true

				Notify 'The file has started to checkin.It might take little bit longer...', null, null, 'info'

				if func == 'new'

					promise = $q.all(wfiles.map((doc) ->
						spiderdocsService.SaveWorkSpaceFileToDbAsNewAsync doc.id_user_workspace, doc, options
					))
				else if func == 'ver'
					promise = $q.all(wfiles.map((doc) ->
						spiderdocsService.SaveWorkSpaceFileToDbAsVerAsync doc.id_user_workspace, doc, reason, options
					))

				promise.then((res) ->
					docs = res
					if docs.filter(((x) ->
						x.id > 0
						)).length == wfiles.length
						ids = wfiles.map((x) ->
							x.id_user_workspace
						)
						$scope.data.docs = $scope.data.docs.filter((x) ->
							!ids.includes(x.id)
						)
						Notify 'The file has been checked in. ', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'
					comService.loading false
					return
				).catch ->
					comService.loading false
					return
				return
			), ->
			return

		convWfile2Doc = (_docs) ->
			docs = []
			angular.copy _docs, docs
			# transfer property values
			docs.forEach (x) ->
				x.id_user_workspace = x.id
				return
			docs.forEach (x) ->
				x.id = 0
				return
			docs.forEach (x) ->
				x.title = x.filename
				return
			docs.forEach (x) ->
				x.extension = if x.filename.split('.').length > 1 then x.filename.substring(x.filename.lastIndexOf('.'), x.filename.length) else ''
				return
			docs.forEach (x) ->
				x.id_latest_version = x.id_version
				return
			# delete not document object properties
			docs.forEach (x) ->
				delete x.id_doc
				return
			docs.forEach (x) ->
				delete x.filename
				return
			docs.forEach (x) ->
				delete x.filedata
				return
			docs.forEach (x) ->
				delete x.filehash
				return
			docs.forEach (x) ->
				delete x.created_date
				return
			docs.forEach (x) ->
				delete x.lastaccess_date
				return
			docs.forEach (x) ->
				delete x.lastmodified_date
				return
			docs.forEach (x) ->
				delete x.id_user
				return
			docs

		events = ->

			leaves = ->
				clearTimeout $scope.pid.dragfile
				$scope.pid.dragfile = setTimeout((->
					angular.element('#upload1').addClass 'hidden'
					return
				), 100)
				return

			angular.element('html').on 'dragover', (e) ->
				#angular.element('#upload1').removeClass('hidden');
				#console.log('dragover');
				leaves()
				e.preventDefault()
				e.stopPropagation()
				return

			angular.element('body .screen').on 'mousedown', (e) ->
				e.preventDefault()
				e.stopPropagation()
				return
			angular.element('html').on 'dragenter', (e) ->
				#console.log('ondragenter');
				angular.element('#upload1').removeClass 'hidden'
				e.preventDefault()
				e.stopPropagation()
				return
			angular.element('#upload1').on 'drop', drop
			return

		drop = (e) ->
			e.preventDefault()
			e.stopPropagation()
			if e.originalEvent.dataTransfer
				files = e.originalEvent.dataTransfer.files
				if files.length > 0
					comService.loading()
					$q.all(_.values(files).map((file) ->
						spiderdocsService.SaveFileToUserWorkSpace file
					)).then(queryDocs).then((docs) ->
						format(docs)
						$scope.data.docs = docs
						Notify 'File(s) have/has been saved into SpiderDocs', null, null, 'success'
						comService.loading false
						angular.element('#upload1').addClass 'hidden'
						return
					).catch ->
						comService.loading false
						angular.element('#upload1').addClass 'hidden'
						return
			return

		reload = ->

			queryDocs().then (docs) ->
				format(docs)
				$scope.data.docs = docs
				return
			return

		$scope.data = docs: []
		$scope.config = {}
		$scope.pid =
			populateattr: 0
			dragfile: 0
		$scope.sStorage = $sessionStorage
		#Menu Items Array
		$scope.menus = [
			{
				label: 'CheckIn'
				action: 'checkin'
				active: true
			}
			{
				label: 'Send by e-mail'
				action: ''
				active: true
				subItems: [
					label: 'Original'
					action: 'emailOriginal'
					active: true
					]
			}
			{
				label: 'Export'
				action: 'download'
				active: true
			}
			{
				label: 'Import'
				action: 'import'
				active: true
			}
			{
				label: 'Rename'
				action: 'rename'
				active: true
			}
			{
				label: '-------------------'
				action: ''
				active: false
			}
			{
				label: 'Delete'
				action: 'delete'
				active: true
			}
		]

		$scope.sortby =
			key: 'filename'
			asc: true

		$scope.dbmenus =[
			{p:'./index', n:'Local Database'},
			{p:'./local', n:'User Database'}
		]

		$scope.sort = (column) ->

			# #// reset icon beside column
			columns = ['js--header__id_doc','js--header__filename','js--header__filesizeText','js--header__lastmodified_date','js--header__created-date']

			$scope.sortby.asc = if $scope.sortby.key isnt column then false else $scope.sortby.asc

			$scope.sortBy(column).then (docs) ->
				# #// set icon
				angular.element( '.js--header__' + column ).addClass( $scope.sortby.asc ? 'btn-sort--asc' : 'btn-sort--desc' );

		$scope.sortBy = (key) ->

			$scope.sortby.key = key
			$scope.sortby.asc = not $scope.sortby.asc

			reload()


		$scope.delete = (arg, scope) ->
			doc = scope.doc
			comService.loading true
			spiderdocsService.DeleteUserWorkSpaceFile([ doc.id ], 'Deleted by webapp').then(->
				Notify 'The document has been deleted', null, null, 'success'
				#comService.loading(false);
				return
			).then(queryDocs).then((docs) ->
				format(docs)
				$scope.data.docs = docs
				#Notify("File(s) have/has been saved into SpiderDocs", null, null, 'success');
				comService.loading false
				return
			).catch ->
				comService.loading false
				return
			return

		$scope.download = (arg, scope) ->
			doc = scope.doc
			comService.loading true
			url = ''
			spiderdocsService.GetDonloadUrls4UserWorkSpace(doc.id).then((urls) ->
				if urls.length == 0
					alert 'File could not exported.'
					return
				url = urls.pop()
				comService.downloadBy url
				#return $http({url:url,method: 'Get', data:{},responseType:'blob'})
			).then(->
				comService.loading false
				return
			).catch ->
				alert 'Comming soon'
				comService.loading false
				return
			return

		$scope.import = (arg, scope) ->
			doc = scope.doc
			modalInstance = $uibModal.open(
				animation: true
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'scripts/app/views/form-import.html'
				controller: 'formImportCtrl'
				controllerAs: '$ctrl'
				backdrop: 'static'
				size: 'sm'
				resolve:
					doc: ->
						doc.title = doc.filename
						doc.extension = '.' + doc.title.split('.').pop()
						doc
					reasonNewVersion: ->
						false
			)
			modalInstance.result.then ((result) ->
				file = result[0]
				comService.loading()
				Notify 'It might be take long time.', null, null, 'info'
				spiderdocsService.ImportUserWorkSpaceFile(doc.id, file).then((_verDoc) ->
					if _verDoc.id == doc.id
						origindoc = $scope.data.docs.find((x) ->
							x.id == doc.id
						)
						angular.extend origindoc, _verDoc
						Notify 'The file has been upladed. ', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'
					comService.loading false
					return
				).catch ->
					comService.loading false
					return
				return
			), ->
			return

		$scope.checkin = (arg, scope) ->
			doc = scope.doc
			spiderdocsService.cache.settings().then (settings) ->
				if doc.id_version > 0
					checkin4ver doc, settings
				else
					saveAsNew [ doc ], settings
				return
			return

		$scope.rename = (arg, scope) ->
			wfile = scope.doc
			file = angular.extractPath(wfile.filename)
			oldName = wfile.filename
			modalInstance = $uibModal.open(
				animation: true
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'scripts/app/views/form-input.html'
				controller: 'formInputCtrl'
				controllerAs: '$ctrl'
				backdrop: 'static'
				size: 'sm'
				resolve:
					title: ->
						'[Rename]'
					message: ->
						wfile.filename + ' will be renamed with following'
					defInput: ->
						file.nameWithoutExt
					reasonMinLen: ->
						1
					needsInput: ->
						true
					inputType:->
						'text'
			)
			modalInstance.result.then ((newname) ->
				new_fullname = newname + file.extension

				if oldName == new_fullname
					Notify 'Cancelled. New file name is same as original. ', null, null, 'warning'
					return
				comService.loading true

				spiderdocsService.RenameUserWorkSpaceFileAsync(wfile.id, new_fullname).then((updated) ->

					if updated.id == wfile.id
						$scope.data.docs.find((x) ->
							x.id == wfile.id
						).filename = new_fullname

						Notify 'The file has been renamed. \'' + oldName + '\' -> \'' + new_fullname + '\'', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'


					comService.loading false

					return
				).catch ->
					comService.loading false
					return
				return
			), ->


			return


		$scope.emailOriginal = (arg, scope) ->

			doc = scope.doc
			tempId = ''

			spiderdocsService.SaveWorkSpaceFile2TempAsync(doc.id).then((_tempId) ->
				tempId = _tempId
			)

			spiderdocsService.cache.myprofile().then( (myprofile) ->
				modalInstance = $uibModal.open(
					animation: true
					ariaLabelledBy: 'modal-title'
					ariaDescribedBy: 'modal-body'
					templateUrl: 'scripts/app/views/form-email.html'
					controller: 'FormEmailCtrl'
					controllerAs: '$ctrl'
					backdrop: 'static'
					size: 'sm'
					resolve:
						title: () ->
							'Send as Original'

						to: () ->
							myprofile.email

						cc: () ->
							''

						bcc: () ->
							''

						subject: () ->

							doc.filename

						body: () ->

							''

						attachments: () ->

							_attachments = []

							_f = angular.extractPath doc.filename

							_attachments.push
								filename: doc.filename
								extension: _f.extension.substr(1)

							_attachments

				)

				modalInstance.result.then( (email) ->

					Notify("Sending an email. It might take long time...", null, null, 'info')

					spiderdocsService.SendEmailAsync(tempId, email)

					.then((error) ->

						if error is ''
							Notify("The file has sent to '" + email.to + "'", null, null, 'success')
						else
							Notify("Rejected. You might not have right permission to do that." , null, null, 'danger')

					)

				)
			)

		queryDocs = () ->
			spiderdocsService.GetUserWorkspaceDocuments().then(applySortBy)

		applySortBy = (docs) ->
			comService.sortBy(docs,$scope.sortby.key, $scope.sortby.asc)


		init()

		return

	format = (docs) ->
		for doc in docs
			size = parseInt(doc.filesize / 1024)
			doc.filesizeText = if size < 1 then '< 1 KB' else size + ' KB'

		return
