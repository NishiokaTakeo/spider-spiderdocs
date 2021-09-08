app.service 'modalService', [
	'$q'
	'$uibModal'
	'_'
	'spiderdocsService'
	'comService'
	(
		$q
		$uibModal 
		_
		spiderdocsService
		comService
	) ->

		that = this
		pid_loading = 0
		pid_goto = 0

		@input = (title,message,defInput,reasonMinLen = 1,needsInput = true,inputType = 'text' ) =>

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
						title
					message: ->
						message
					defInput: ->
						defInput
					reasonMinLen: ->
						reasonMinLen
					needsInput: ->
						needsInput
					inputType:->
						inputType
				)
			
			modalInstance.result
		
		@confirm = (title, message, options = {}) =>

			modalInstance = $uibModal.open
				animation: true
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'scripts/app/views/form-confimation.html',
				controller: 'formConfimationCtrl'
				controllerAs: '$ctrl'
				backdrop: 'static'
				size: 'sm'
				resolve:
					title: ->
						title
					message: ->
						message
					options: ->
						options

			modalInstance.result

		@openfolder = (folders, permission, menu = {}, canCreate = false) =>
			modalInstance = $uibModal.open(
								animation: true
								ariaLabelledBy: 'modal-title'
								ariaDescribedBy: 'modal-body'
								templateUrl: 'scripts/app/views/form-folder-tree.html'
								controller: 'formFolderTreeCtrl'
								controllerAs: '$ctrl'
								backdrop: 'static'
								size: 'lg'
								resolve:
									folders: ->
										folders
									permission: ->
										permission
									menu: ->
										menu
									canCreate: ->
										canCreate
						)

			modalInstance.result

		
		
		
		@searchDocument = (doc, folders, types, permission, options = {}) ->

			modalInstance = $uibModal.open
				animation: true
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'scripts/app/views/form-search-documents.html'
				controller: 'formSearchDocumentsCtrl'
				controllerAs: '$ctrl'
				backdrop: 'static'
				size: 'lg'
				resolve:
					extension: ->
						if options.saveAsPDF then '.pdf' else doc.extension

					folders: ->
						comService.nodesBy 0, folders
					
					docTypes: ->
						_types = []
						angular.copy types, _types
						return _types

					permission: ->
						permission
			
			modalInstance.result


		@saveAsNew = (docs, settings) ->
			spiderdocsService.cache.settings()
			
			.then (settings) ->
				modalInstance = $uibModal.open

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
							docs
						title: ->
							'[Save New File]'
						docTypes: ->
							spiderdocsService.cache.doctypes()
						settings: ->
							spiderdocsService.cache.settings()
						notificationgroups: ->

							spiderdocsService.cache.notificationgroups().then (source) ->
								
								source.map (_source) ->

									id : _source.id
									text : _source.group_name
									checked : false

				modalInstance.result
			
			.then (data) ->
				# wfiles = data.docs
				func = data.func
				# reason = data?.reason
				# options = data?.options

				# promise = {}
				# comService.loading true

				# Notify 'The file has started to checkin.It might take little bit longer...', null, null, 'info'

				if func == 'new'
					console.log 'new was selected'
					# promise = $q.all(wfiles.map((doc) ->
					# 	spiderdocsService.SaveWorkSpaceFileToDbAsNewAsync doc.id_user_workspace, doc, options
					# ))
				else if func == 'ver'
					console.log 'ver was selected'
					# promise = $q.all(wfiles.map((doc) ->
					# 	spiderdocsService.SaveWorkSpaceFileToDbAsVerAsync doc.id_user_workspace, doc, reason, options
					# ))

				return data
				# promise.then (res) ->
				# 	docs = res

				# 	if docs.filter((x) ->
				# 		x.id > 0
				# 		)).length == wfiles.length
						
				# 		ids = wfiles.map (x) ->
				# 			x.id_user_workspace
						
						
				# 		$scope.data.docs = $scope.data.docs.filter (x) ->
				# 			!ids.includes(x.id)
						
						
				# 		Notify 'The file has been checked in. ', null, null, 'success'
				# 	else
				# 		Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'
					
				# 	comService.loading false
					
				# 	return
			.catch ->
				comService.loading false
				return





		return

]