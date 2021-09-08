app.controller 'workspaceController', 
[
	'$scope'
	'$timeout'
	'$q'
	'$interval'
	'spiderdocsService'
	'comService'
	'$window'
	'$sessionStorage'
	'_'
	'moment'
	'$http'
	'$uibModal'
	'en_file_Status'
	'$localStorage'
	'en_Actions'
	'SidebarJS'
	'en_Events'
	'en_file_Sp_Status'
	'en_ReviewAction'
	'modalService'
	'en_DoubleClickBehavior'
	(
		$scope
		$timeout
		$q
		$interval
		spiderdocsService
		comService
		$window
		$sessionStorage
		_
		moment
		$http
		$uibModal
		en_file_Status
		$localStorage
		en_Actions
		SidebarJS
		en_Events
		en_file_Sp_Status
		en_ReviewAction
		modalService
		en_DoubleClickBehavior
	) ->
		
		$scope.incDbmenuCtrl = {} 

		init = -> 

			setupImportByDrag()

			getMaster().then(getMasterDepend)
	
			.then (res) -> 
					
				#$scope.data.docs = _.cloneDeep(res[0]);3 
				
				$scope.data.docTypes = _.cloneDeep(res[0])
				$scope.data.attrs = _.cloneDeep(res[1])
				$scope.data.comboitems = _.cloneDeep(res[2])
				$scope.sStorage.docTypes = _.cloneDeep($scope.data.docTypes)
				$scope.sStorage.attrs = _.cloneDeep($scope.data.attrs)
				$scope.sStorage.comboitems = _.cloneDeep($scope.data.comboitems)
				$scope.data.customviews = _.cloneDeep(res[3])

				setupDbViews($scope.data.customviews)

				$scope.data.docTypes.map (type) ->
					type.text = type.type
					type

			.then queryDocs

			.then () =>
				# $scope.data.docs = _.cloneDeep(docs);
				$scope.showPageName()
				pages 1

			.then ->
				comService.loading false
				return

			.then afterLoad

		$scope.statusColour = (doc) ->
			ans = 'doc-status'

			# ordered by low priority
			suffix = 'in-review' if $scope.isInReview(doc)
			suffix = 'overdue-review' if $scope.isOverdueReview(doc)
			suffix = 'checkedout' if $scope.isCheckedout(doc)
			suffix = 'archived' if $scope.archived(doc)

			"#{ans}--#{suffix}"

		send = (email, tempId) ->
			Notify 'Sending an email. It might take long time...', null, null, 'info'
			spiderdocsService.SendEmailAsync(tempId, email).then (error) ->
				if error == ''
					Notify 'The file has sent to \'' + email.to + '\'', null, null, 'success'
				else
					Notify 'Rejected. You might not have right permission to do that.', null, null, 'danger'
				return
			return

		checkouted = (doc) ->
			if typeof doc.id_status != 'number'
				$log.error 'doc.id_status must be number'
				debugger
			doc.id_status == en_file_Status.checked_out

		checkined = (doc) ->
			doc.id_status == en_file_Status.checked_in

		canDelete = (arg, scope) ->
			doc = scope.doc
			false == checkouted(doc)

		canDiscard = (arg, scope) ->
			doc = scope.doc
			checkouted doc

		canArchive = (arg, scope) ->
			doc = scope.doc
			!checkouted(doc)

		canImport = (arg, scope) ->
			doc = scope.doc
			checkined doc

		canCheckout = (arg, scope) ->
			doc = scope.doc
			false == checkouted(doc) and doc.id_sp_status is en_file_Sp_Status.normal

		canRename = (arg, scope) ->
			doc = scope.doc
			false == checkouted(doc)

		canExportPDF = (arg, scope) ->
			doc = scope.doc

			comService.canExportExt(doc.extension)


		getRecentDocuments = ->
			spiderdocsService.GetRecentDocuments().then(applySortBy).then(storeMaxLength).then(applyLimitOffseet).then (docs) ->
				$scope.data.docs = docs
				docs

		pagenation_max_page = ->
			Math.ceil $scope.doc_max_length / $scope.per_item.id

		pages = (at) ->
			$scope.pages = []
			start = at
			end = start + $scope.pagenation_limit
			if end >= pagenation_max_page()
				end = pagenation_max_page() + 1
				start = pagenation_max_page() - 5
			if start < 1
				start = 1
			i = start
			while i < end
				$scope.pages.push i
				i++
			return

		queryDocs = ->

			query = if $scope.searchMode == 'search' or idcustomview() > 0 then $scope.search() else getRecentDocuments()
			query
			#.then(storeMaxLength);

		changeattrs = ->

			findAttr = (id) ->
				$scope.data.attrs.find((a) ->
					a.id == id
				) or {}

			generateTage = (attr, config) ->
				$wrap = {}
				$element = {}
				switch attr.id_type
					when 1
						#1	Text Box
						$element = TgGn.Text(config)
						$wrap = wrapInContainer(attr.id_type, $element, config.id, attr.name)
					when 2
						#2	Check Box
						$wrap = $('<div/>')
						break
						$element = TgGn.CheckBox(config)
						$wrap = wrapInContainer(attr.id_type, $element, config.id, attr.name)
					when 3
						#3	Date
						$element = TgGn.Date(config)
						$wrap = $element
					#4	Combo Box
					#8	FixedCombo
					#10 ComboSingleSelect
					when 4, 8, 10, 11
						#11 FixedComboSingleSelect
						db = $scope.data.comboitems.filter((itm) ->
							itm.id_atb == attr.id
						)
						$element = TgGn.Select(
							db: db
							id: 'id'
							text: 'text'
							data: config.data)
						$element.attr 'name', config.id
						$wrap = wrapInContainer(attr.id_type, $element, config.id, attr.name)
					when 5, 6, 9
						return
				$wrap

			wrapInContainer = (id_type, $element, id, name) ->
				key2Append = ''
				$cntinr = ''
				wrapper = ''
				switch id_type
					when 1
						#1	Text Box
						wrapper = [
							'<div class="form-field ">'
							'<label for="{0}" class="form-field__label">{1}</label>'
							'<div class="js-key">'
							'</div>'
							'</div>'
						].join('').format(id, name)
						$cntinr = angular.element(wrapper)
					when 2
						#2	Check Box
						wrapper = [
							'<div class="form-field form-field--checkbox">'
							'<label for="{0}" class="form-field__label" style="margin-top: 3.3rem;"><span>{1}</span></label>'
							'<div class="js-key">'
							'</div>'
							'</div>'
						].join('').format(id, name)
						$cntinr = angular.element(wrapper)
					when 3
						#3	Date
						wrapper = [
							'<div class="form-field">'
							'<label for="{0}" class="form-field__label">{1}</label>'
							'<div class="form-field__date">'
							'<div class="js-key">'
							'</div>'
							'</div>'
							'</div>'
						].join('').format(id, name)
						$cntinr = angular.element(wrapper)
					#4	Combo Box
					#8	FixedCombo
					#10 ComboSingleSelect
					when 4, 8, 10, 11
						#11 FixedComboSingleSelect
						wrapper = [
							'<div class="form-field ">'
							'<label for="{0}" class="form-field__label">{1}</label>'
							'<div class="form-field__select form-field__select--blue js-key">'
							'</div>'
							'</div>'
						].join('').format(id, name)
						$cntinr = angular.element(wrapper)
					when 5, 6, 9
						return
				$cntinr.find('.js-key').append $element
				$cntinr

			generate = (attr_doctyp) ->
				#var $tmplt = $('#template #field');
				$attr_palce = angular.element('.js-attribute-section')
				attr = findAttr(attr_doctyp.id_attribute)
				id = attr.name.replace(/\s/g, '')
				config =
					id: id
					data:
						'id': attr.id
						'id_type': attr.id_type
				$element = generateTage(attr, config)
				if !$element
					return
				$cntinr = $element
				if attr.required == 1
					registry_validationconf valiconf, config.id, attr.name, attr.id_type
				$attr_palce.append $cntinr
				return


			registry_default_validation = (valiconf) ->
				registry_validationconf valiconf, 'folders', 'Folder', 4
				registry_validationconf valiconf, 'types', 'Document Type', 4
				registry_validationconf valiconf, 'path', '', -1
				return

			registry_validationconf = (conf, id, name, type) ->
				msg = 'Please Fill this field.'
				conf.rules[id] = {}
				conf.rules[id].required = true
				conf.messages[id] = {}
				switch type
					when 1
						#1	Text Box
						msg = 'Please {0} {1}.'.format('enter', name)
					when 4
						#1	Text Box
						msg = 'Please {0} {1}.'.format('enter', name)
					when -1
						#1	Text Box
						msg = 'Please Take a photo.'
					else
						msg = 'Please {0} {1}.'.format('select', name)
						break
				conf.messages[id].required = msg
				return

			if !$scope.data.docTypes[0]
				return
			$('.js-attribute-section').empty()
			typeids = $scope.data.docTypes.filter((x) ->
				x.checked
			).map((x) ->
				x.id
			)
			valiconf =
				rules: {}
				messages: {}
			if typeids.length == 0
				return
			spiderdocsService.GetAttributeDocType(typeids).then (rslt) ->
				map = _.uniqBy(rslt.map((x) ->
					{ id_attribute: x.id_attribute }
				), 'id_attribute')
				registry_default_validation valiconf
				map.forEach generate
				#deploy_validation(valiconf);
				return

		parseAttributeCriterias = ->
			values = angular.element('.js-attribute-section input, .js-attribute-section select').toArray().map((x) ->
				id = angular.element(x).attr('data-id')
				id_type = angular.element(x).attr('data-id_type')
				val = angular.element(x).val()
				{ Values:
					id: id
					id_type: id_type
					atbValue: if [ '2' ].includes(id_type) then onoff2bit(val) else val }
			)

			onoff2bit = (val) ->
				if val == '1' then 1 else 0

			values.filter (x) ->
				x.Values.atbValue

		perItems = (setting) ->
			if setting.id == ''
				return
			$scope.data.per_items.find (x) ->
				x.id == setting.per_items

		storeMaxLength = (docs) ->
			$scope.doc_max_length = docs.length
			docs

		applyLimitOffseet = (docs) ->
			docs = docs or []
			curAt = $scope.cur_page - 1
			num_per = $scope.per_item.id
			start = curAt * num_per
			if docs.length < start + 1 then docs.slice(start, docs.length) else docs.slice(start, start + num_per)

		applySortBy = (docs) ->
			comService.sortBy(docs,$scope.sortby.key, $scope.sortby.asc)



		setDocs = (docs) ->
			$scope.data.docs = docs
			return

		getMaster = (###res###) ->
			$q.all [
				#noop(res)
				spiderdocsService.cache.doctypes()
				spiderdocsService.cache.attrs()
			]

		getMasterDepend = (res) ->
			$q.all [
				noop(res[0])
				noop(res[1])
				# noop(res[2])
				if $scope.sStorage.comboitems then noop($scope.sStorage.comboitems) else getCombo(res[1])
				spiderdocsService.GetCustomViewsAsync()
			]

		noop = (data) ->
			deferred = $q.defer()
			setTimeout (->
				deferred.resolve data
				return
			), 1
			deferred.promise

		getCombo = (attrs) ->
			$q.all(attrs.map((a) ->
				a.id
			).filter((a) ->
				a < 10000
			).map((id_attr) ->
				spiderdocsService.GetAttributeComboboxItems id_attr
			)).then (res) ->
				res = res or []
				items = []
				res.forEach (row) ->
					if row and row.length > 0
						row.forEach (x) ->
							items.push x
					return
				items

		afterLoad = ->
			$q.all([
				spiderdocsService.cache.settings()
				spiderdocsService.cache.foldersL1(0,en_Actions.OpenRead)
			]).then (res) ->
				settings = res[0]
				folders = res[1]
				$scope.explorer.folders = comService.nodesBy(0, folders)
				$scope.lStorage.settings = _.cloneDeep(settings)
				return
			return

		$scope.data =
			docs: []
			per_items: [
				{
					id: 10
					name: '10 p/page'
				}
				{
					id: 15
					name: '15 p/page'
				}
				{
					id: 20
					name: '20 p/page'
				}
				{
					id: 50
					name: '50 p/page'
				}
				{
					id: 100
					name: '100 p/page'
				}
			]
			attrs: []



		$scope.search =
			database_views:[]

		$scope.view =
			pagetitle: ''

		$scope.com=comService
		$scope.explorer =
			folders: []
			folder_default: 0
		$scope.cur_page = 1
		$scope.pagenation_limit = 5
		$scope.pages = []
		$scope.total = 0
		$scope.per_item = $scope.data.per_items[0]
		$scope.sortby =
			key: 'id'
			asc: false
		$scope.doc_max_length = 0
		$scope.config = {}
		$scope.pid =
			populateattr: 0
			goto: 0
		$scope.searchMode = 'recent'
		# recent or search
		$scope.sStorage = $sessionStorage
		$scope.lStorage = $localStorage
		$scope.en_file_Status = en_file_Status
		#Menu Items Array
		$scope.menus = [
			{
				label: 'Discard Checkout'
				action: 'discard'
				active: canDiscard
			}
			{
				label: 'CheckOut'
				action: 'checkout'
				active: canCheckout
			}
			{
				label: 'Import New Version'
				action: 'import'
				active: canImport
			}
			{
				label: 'Export'
				# action: 'download'
				active: true
				subItems: [
					{
						label: 'as User Workspace'
						action: 'checkout'
						active: true
					}
					{
						label: 'as Copy of Original'
						action: 'download'
						active: true
					}
					{
						label: 'as PDF'
						action: 'exportAsPDF'
						active: canExportPDF
					}
				]

			}
			{
				label: 'Property'
				action: 'property'
				active: true
			}
			{
				label: 'Send by e-mail'
				action: ''
				active: true
				subItems: [
					{
						label: 'Original'
						action: 'emailOriginal'
						active: true
					}
					{
						label: 'Document Link'
						action: 'emailDocumentLink'
						active: true
					}
				]
			}
			{
				label: 'Review'
				action: 'review'
				active: true
			}
			{
				label: 'Create Document Link'
				action: 'createDMS'
				active: true
			}
			{
				label: 'Go to this folder'
				action: 'gotoFolder'
				active: true
			}
			{
				label: 'Rename'
				action: 'rename'
				active: canRename
			}
			{
				label: 'Details'
				action: 'details'
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
				active: canDelete
			}
			{
				label: 'Archive'
				action: 'archive'
				active: canArchive
			}
		]
	
		$scope.folderOpened = ->
			#folderSidebarJS
			SidebarJS.toggle 'menuSidebarJS'
			return

		$scope.openSearchFolderForm = ->
			spiderdocsService.cache.foldersL1(0, en_Actions.OpenRead).then((folders) ->

				nodes = comService.nodesBy 0,folders
				
				modalService.openfolder nodes,en_Actions.OpenRead

			).then ((folder) ->
				if folder == undefined
					folder = {}
				$scope.search.name_folder = folder.text
				$scope.search.id_folder = folder.id
				return
			), ->
			return

		#$scope.property = function(doc)

		$scope.property = (arg, scope) ->
			doc = scope.doc
			modalInstance = $uibModal.open(
				animation: true
				windowClass: 'modal--property'
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'scripts/app/views/form-property.html'
				controller: 'FormPropertyCtrl'
				controllerAs: '$propCtrl'
				backdrop: 'static'
				size: 'sm'
				resolve:
					doc: ->
						doc
					types: ->
						$scope.sStorage.docTypes
					attrs: ->
						$scope.sStorage.attrs
					comboitems: ->
						$scope.sStorage.comboitems
					notificationgroups: ->
						myGrp = []
						spiderdocsService.GetNotificationGroupsAsync(DocumentId: doc.id).then((_myGrp) ->
							myGrp = _myGrp.map((my) ->
								my.id
						)
							spiderdocsService.cache.notificationgroups()
						).then (source) ->
							source.map (_source) ->
								{
									id: _source.id
									text: _source.group_name
									checked: myGrp.includes(_source.id)
								}
					customviews: ->
						spiderdocsService.GetCustomViewsAsync()
					customviewdocuments: ->
						spiderdocsService.GetCustomViewDocumentsAsync(doc.id)
			)
			modalInstance.result.then ((doc) ->

				comService.loading true

				spiderdocsService.UpdateProperty(doc).then (error) ->
					if error == ''
						Notify 'The document has been updated!', null, null, 'success'

						# delall = viewcod for viewcod in doc.customviews when viewcod.checked is true
						delall = viewcod for viewcod in doc.customviews

						if not delall?
							spiderdocsService.DeleteAllCustomViewDocumentAsync(doc.id)
							doc.customviews = []

						# $scope.search
						# 	DocIds: [doc.id]
						# .then (_docs) ->
						# 	origindoc = $scope.data.docs.find((x) ->
						# 		x.id == doc.id
						# 	)

						# 	angular.extend origindoc, _docs[0]

						# This does not necessary logic.
						$scope.searchDoc(doc.id, if doc.customviews.length > 0 then doc.customviews[0].id else undefined).then (_docs) ->

							origindoc = $scope.data.docs.find((x) ->
								x.id == doc.id
							)

							angular.extend origindoc, _docs[0]
					else
						Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'

					comService.loading false

					return

			), ->

				return
			modalInstance.rendered.then ->
				return

			return

		$scope.checkout = (arg, scope) ->
			doc = scope.doc
			comService.loading true
			
			$scope.checkoutWithFooter doc

			.then () ->
				comService.loading false
				return

			.catch () ->
				comService.loading false
				return

			return

		$scope.delete = (arg, scope) ->
			doc = scope.doc
			modalInstance = $uibModal.open(
				animation: true
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'delete-form.html'
				controller: 'deleteFormCtrl'
				controllerAs: '$propCtrl'
				backdrop: 'static'
				size: 'sm'
				resolve: doc: ->
					doc
			)
			modalInstance.result.then ((reason) ->
				spiderdocsService.Delete([ doc.id ], reason).then (ans) ->
					if true == angular.isString(ans) and angular.isEmpty(ans)
						$scope.data.docs = $scope.data.docs.filter((x) ->
							x.id != doc.id
						)
						Notify 'The document has been deleted!', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
					return
				return
			), ->
				# $log.info('Modal dismissed at: ' + new Date());
				return
			return

		$scope.discard = (arg, scope) ->
			doc = scope.doc
			spiderdocsService.CancelCheckOut([ doc.id ]).then((err) ->
				if true == angular.isString(err) and angular.isEmpty(err)
					doc.id_status = en_file_Status.checked_in
					Notify 'The document has been discared checkout!', null, null, 'success'
				else
					Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
				return
			).catch ->
				Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
				return
			return

		$scope.archive = (arg, scope) ->
			doc = scope.doc
			
			modalInstance = modalService.confirm('Are you sure to archive this file (' + doc.title + ')?', 'The action cannot redo once archived the document.')

			# modalInstance = $uibModal.open(
			# 	animation: true
			# 	ariaLabelledBy: 'modal-title'
			# 	ariaDescribedBy: 'modal-body'
			# 	templateUrl: 'form-confimation.html'
			# 	controller: 'formConfimationCtrl'
			# 	controllerAs: '$ctrl'
			# 	backdrop: 'static'
			# 	size: 'sm'
			# 	resolve:
			# 		title: ->
			# 			'Are you sure to archive this file (' + doc.title + ')?'
			# 		message: ->
			# 			'The action cannot redo once archived the document.'
			# )

			modalInstance.then ((result) ->
				if result == 'ok'
					return spiderdocsService.Archive([ doc.id ]).then((ans) ->
						if ans == ''
							$scope.data.docs.find((x) ->
								x.id == doc.id
							).id_status = en_file_Status.archived
							Notify 'The document has been deleted!', null, null, 'success'
						else
							Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
						return
					)
				else
					Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
				return
			), ->
				# $log.info('Modal dismissed at: ' + new Date());
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
						doc
					reasonNewVersion: ->
						$scope.lStorage.settings.public.reasonNewVersion
			)
			modalInstance.result.then ((result) ->
				file = result[0]
				reason = result[1]
				comService.loading()
				spiderdocsService.SaveAsNewVer(doc.id, file, reason).then((_verDoc) ->
					if _verDoc.id == doc.id
						origindoc = $scope.data.docs.find((x) ->
							x.id == doc.id
						)
						angular.extend origindoc, _verDoc
						Notify 'The file has been upladed. ( v' + (_verDoc.version - 1) + ' -> v' + _verDoc.version + ')', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
					comService.loading false
					return
				).catch ->
					comService.loading false
					return
				return
			), ->
			return

		$scope.archived = (doc) ->
			parseInt(doc.id_status) is en_file_Status.archived

		$scope.isInReview = (doc) ->
			doc.id_sp_status is en_file_Sp_Status.review

		$scope.isOverdueReview = (doc) ->
			doc.id_sp_status is en_file_Sp_Status.review_overdue

		$scope.isCheckedout = (doc) ->
			doc.id_status is en_file_Status.checked_out


		$scope.gotoFolder = (arg, scope) ->
			comService.loading true
			doc = scope.doc
			# var selectPath = [];
			spiderdocsService.DrillUpFoldersWithParentsFromAsync(doc.id_folder, en_Actions.OpenRead).then (folders) ->
				$scope.explorer.folders = comService.nodesBy(0, folders, doc.id_folder)
				$scope.explorer.folder_default = doc.id_folder
				comService.openFolder doc.id_folder, folders, '[sidebarjs-name="folderSidebarJS"]'
				SidebarJS.toggle 'folderSidebarJS'
				comService.loading false
				return
			return

		$scope.review = (arg, scope) ->

			doc = scope.doc
			_checked = ''

			modalInstance = $uibModal.open
				animation: true
				ariaLabelledBy: 'modal-title'
				ariaDescribedBy: 'modal-body'
				templateUrl: 'scripts/app/views/form-review.html'
				controller: 'formReviewCtrl'
				controllerAs: '$ctrl'
				backdrop: 'static'
				size: 'lg'
				resolve:
					doc: ->
						doc
					currentReview: ->
						spiderdocsService.GetReviewAsync doc.id
					isOwner: ->
						spiderdocsService.IsReviewOwnerAsync doc.id

					permissions: ->
						spiderdocsService.cache.myprofile()
							.then (profile) ->
								spiderdocsService.GetFolderPermissionsAsync(doc.id_folder, profile.id)

					reviewHistory:->
						spiderdocsService.GetReviewHistoryAsync doc.id

					reviewUsers: ->
						spiderdocsService.cache.reviewers(doc.id)
						.then (users) ->
							reviewUsers	= []
							for user in users
								reviewuser={}
								reviewuser.id = user.id
								reviewuser.text = user.name
								reviewuser.checked = false
								reviewUsers.push reviewuser

							reviewUsers
					profile: () ->
						spiderdocsService.cache.myprofile()

					users: () ->
						spiderdocsService.cache.users()

			modalInstance.result.then (data) ->

				comService.loading true

				_checked = data.checked
				if data.checked is 'start'
					response = spiderdocsService.StartReviewAsync doc.id, data.review.id_version, data.review.allow_checkout, data.review.id_users, data.review.deadline,data.review.owner_comment
				else if data.checked is 'finish'
					response = spiderdocsService.FinishReviewAsync doc.id, data.review.id_version, data.review.owner_comment

				response

			.then((_review) ->
					if _review.id_doc is doc.id

						if _checked is 'start'
							Notify 'The review has been started', null, null, 'success'

							$scope.data.docs.find (d)-> d.id is doc.id
								.id_sp_status = en_file_Sp_Status.review

						else if _checked is 'finish'
							Notify 'The review has been finished', null, null, 'success'

							finalized = (review for review in _review.review_users when review.action is en_ReviewAction.Finalize)

							if finalized.length is _review.review_users.length
								$scope.data.docs.find (d)-> d.id is doc.id
									.id_sp_status = en_file_Sp_Status.normal

					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'

					comService.loading false
					return
				).catch ->
					comService.loading false
					return
				return

			return

		$scope.createDMS = (arg, scope) ->
			doc = scope.doc
			comService.loading true
			# Notify("Started downlaoding Document link file.", null, null, 'info');
			spiderdocsService.CreateDMSLink(doc.id).then((urls) ->
				if urls.length > 0
					url = urls.pop()
					return comService.downloadBy(url)
				else
					Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
				return
			).then(->
				Notify 'Document link has been created. Check \'Download\' Folder.', null, null, 'success'
				comService.loading false
				return
			).catch ->
				alert 'Comming soon'
				comService.loading false
				return
			return

		$scope.rename = (arg, scope) ->
			doc = scope.doc
			file = undefined
			modalInstance = undefined
			oldName = undefined
			file = angular.extractPath(doc.title)
			oldName = doc.title
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
						doc.title + ' will be renamed with following'
					defInput: ->
						file.nameWithoutExt
					reasonMinLen: ->
						1
					needsInput: ->
						true
					inputType:->
						'text'
			)
			modalInstance.result.then (newname) ->
				new_fullname = undefined
				new_fullname = newname + file.extension
				if oldName == new_fullname
					Notify 'Cancelled. New file name is same as original. ', null, null, 'warning'
					return
				comService.loading true
				spiderdocsService.RenameDbFileAsync(doc.id, new_fullname).then((updated) ->
					if updated.id == doc.id
						$scope.data.docs.find((x) ->
							x.id == doc.id
						).title = new_fullname
						Notify 'The file has been renamed. \'' + oldName + '\' -> \'' + new_fullname + '\'', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'
					comService.loading false
					return
				).catch ->
					comService.loading false
					return
				return
			return

		$scope.emailOriginal = (arg, scope) ->
			doc = scope.doc
			tempId = ''
			spiderdocsService.SaveFile2TempAsync(doc.id, doc.id_latest_version).then (_tempId) ->
				tempId = _tempId
				return
			spiderdocsService.cache.myprofile().then (myprofile) ->
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
						title: ->
							'Send as Original'
						to: ->
							myprofile.email
						cc: ->
							''
						bcc: ->
							''
						subject: ->
							doc.title
						body: ->
							''
						attachments: ->
							_attachments = []
							_attachments.push
								filename: doc.title
								extension: doc.extension.substr(1)
							_attachments
				)
				modalInstance.result.then (email) ->
					send email, tempId
					return
				return
			return

		$scope.emailDocumentLink = (arg, scope) ->
			doc = scope.doc
			tempId = ''
			spiderdocsService.SaveDMS2TempAsync(doc.id).then (_tempId) ->
				tempId = _tempId
				return
			spiderdocsService.cache.myprofile().then (myprofile) ->
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
						title: ->
							'Send as Document Link'
						to: ->
							myprofile.email
						cc: ->
							''
						bcc: ->
							''
						subject: ->
							path = angular.extractPath(doc.title)
							path.nameWithoutExt + '.dms'
						body: ->
							''
						attachments: ->
							_attachments = []
							_attachments.push
								filename: doc.title
								extension: 'dms'
							_attachments
				)
				modalInstance.result.then (email) ->
					send email, tempId
					return
				return
			return

		$scope.prev = ->
			if $scope.cur_page <= 1
				$scope.cur_page = 1
				return
			#$scope.per_item = 10;
			$scope.cur_page -= 1
			comService.loading true
			queryDocs().then(->
				pages $scope.cur_page
			).then ->
				comService.loading false

		$scope.page_at = (page) ->
			comService.loading true
			$scope.cur_page = page
			queryDocs().then(->
				pages $scope.cur_page
			).then ->
				comService.loading false

		$scope.next = ->
			max = pagenation_max_page()
			if $scope.cur_page >= max
				pages $scope.cur_page = max
				return
			$scope.cur_page += 1
			comService.loading true
			queryDocs().then(->
				pages $scope.cur_page
			).then ->
				comService.loading false

		$scope.change_per_item = ->
			queryDocs()

		$scope.clearSearchCriteria = ->
			angular.element('input, textarea').val ''
			$('[id^="customDDL-all"]').prop 'checked', false

			setupDbViews($scope.data.customviews)

			# for v in search.database_views
			# 	v.checked = false

			angular.element('input, textarea').trigger 'change'
			$scope.data.docTypes.forEach (x) ->
				x.checked = false

			$scope.search.created_from = $scope.search.created_to = undefined
			$scope.search.id_folder = ''
			$scope.search.name_folder = ''
			comService.loading true
			$scope.cur_page = 1
			$scope.searchMode = 'recent'

			queryDocs().then(->
				pages 1
				changeattrs()
			).then ->
				comService.loading false
				return


			return

		$scope.searchByDMS = ->
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
						{
							title: 'Search by DMS'
							extension: '.dms'
						}
					reasonNewVersion: ->
						false
			)
			modalInstance.result.then ((result) ->
				file = result[0]
				comService.loading()
				Notify 'It might be take long time.', null, null, 'info'
				spiderdocsService.SearchByDMS(file).then((found) ->
					if found.id > 0
						$scope.data.docs = [ found ]
						#Notify(`The file has been upladed. `, null, null, 'success');
					else
						Notify 'Not found DMS file. It might be removed or you might not have right permission to do that .', null, null, 'danger'
					comService.loading false
					return
				).catch ->
					comService.loading false
					return
				return
			), ->
			return

		$scope.searchby = (criteria) ->
			$scope.cur_page = 1
			$scope.search criteria
			return

		$scope.search = (_criteria) ->
			$scope.searchMode = 'search'
			Criteria =
				Date: [ {} ]
				AttributeCriterias: Attributes: []
			docid = $scope.search.id
			keyword = $scope.search.keyword
			docname = $scope.search.title
			createdFrom = $scope.search.created_from
			createdTo = $scope.search.created_to
			folderid = $scope.search.id_folder
			doctypeIds = ($scope.data.docTypes.filter (f) -> f.checked)?.map (t) -> t.id

			if docid
				Criteria.DocIds = [ docid ]
			if docname
				Criteria.Titles = [ docname ]

			if doctypeIds.length > 0
				Criteria.DocTypeIds = doctypeIds

			if keyword
				Criteria.Keywords = keyword
			if folderid
				Criteria.FolderIds = [ folderid ]
			if createdFrom
				Criteria.Date[0].From = moment(createdFrom).format('YYYY-MM-DD 00:00:00')
			if createdTo
				Criteria.Date[0].To = moment(createdTo).format('YYYY-MM-DD 23:59:59')
			Criteria.Date[0].From or (Criteria.Date[0].From = '2000-01-01 00:00:00')
			Criteria.Date[0].To or (Criteria.Date[0].To = '2100-12-31 23:59:59')
			Criteria.AttributeCriterias.Attributes = [].concat(parseAttributeCriterias())

			# ids_customview = idcustomview();

			# if ids_customview > 0
			# 	Criteria.CustomViewIds = [ ids_customview ]
			ids_customview = ids4CustomViews()

			if ids_customview.length > 0
				Criteria.CustomViewIds = ids_customview


			if !Criteria
				return

			comService.loading true

			spiderdocsService.GetDocuments(_criteria or Criteria).then(applySortBy).then(storeMaxLength).then(applyLimitOffseet).then((docs) ->
				$scope.data.docs = docs
				pages 1
				return
			).then ->
				comService.loading false
				return

		# $scope.toArray = function(notes, spliter)
		# {
		#     return removeDuplicates(notes.split(spliter));
		# }

		$scope.searchDoc = (docids,ids_customview) ->
			Criteria =
				DocIds: []


			if not docids?
				return

			if ids_customview > 0
				Criteria.CustomViewIds = [ ids_customview ]

			Criteria.DocIds = if Array.isArray(docids) then docids else [docids]

			spiderdocsService.GetDocuments(Criteria)


		$scope.sort = (column) ->

			columns = ['js--header__date','js--header__name_docType','js--header__version','js--header__name_folder','js--header__author','js--header__title','js--header__id']

			$scope.sortby.asc = if $scope.sortby.key isnt column then false else $scope.sortby.asc

			$scope.sortBy(column).then (docs) ->
				angular.element( '.js--header__' + column ).addClass( $scope.sortby.asc ? 'btn-sort--asc' : 'btn-sort--desc' );


		$scope.filterStatusSelectedAll = (name) ->
			selectAll = angular.element('#' + name).prop('checked')
			$scope.data.docTypes.forEach (x) ->
				x.checked = selectAll
				return
			changeattrs()
			return

		$scope.populateAttrs = ->
			clearTimeout $scope.pid.populateattr
			$scope.pid.populateattr = setTimeout((->
				changeattrs()
				return
			), 300)
			return

		$scope.download = (arg, scope) ->
			doc = scope.doc
			comService.loading true
			url = ''
			spiderdocsService.GetDonloadUrls(doc.id_latest_version).then((urls) ->
				if urls == undefined or urls.length == 0
					#alert('File could not exported.');
					return
				url = urls.pop()
				$http
					url: url
					method: 'Get'
					data: {}
					responseType: 'blob'
			).then((response) ->
				file = response.data
				filename = response.config.url.split('/').pop()
				if comService.browser().chrome
					winUrl = window.URL or window.webkitURL
					downloadLink = angular.element('<a></a>')
					downloadLink.attr 'href', winUrl.createObjectURL(file)
					downloadLink.attr 'target', '_self'
					downloadLink.attr 'download', filename
					downloadLink[0].click()
				else if comService.browser().ie or comService.browser().edge
					window.navigator.msSaveOrOpenBlob file, filename
				else
					fileURL = URL.createObjectURL(file)
					window.open fileURL, filename
				return
			).then(->
				comService.loading false
				return
			).catch ->
				Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
				comService.loading false
				return
			return


		$scope.exportAsPDF = (arg, scope) ->
			doc = scope.doc
			comService.loading true
			url = ''
			spiderdocsService.ExportAsPDF(doc.id_latest_version).then((urls) ->
				if urls == undefined or urls.length == 0
					#alert('File could not exported.');
					return
				url = urls.pop()
				$http
					url: url
					method: 'Get'
					data: {}
					responseType: 'blob'
			).then((response) ->
				file = response.data
				filename = response.config.url.split('/').pop()
				if comService.browser().chrome
					winUrl = window.URL or window.webkitURL
					downloadLink = angular.element('<a></a>')
					downloadLink.attr 'href', winUrl.createObjectURL(file)
					downloadLink.attr 'target', '_self'
					downloadLink.attr 'download', filename
					downloadLink[0].click()
				else if comService.browser().ie or comService.browser().edge
					window.navigator.msSaveOrOpenBlob file, filename
				else
					fileURL = URL.createObjectURL(file)
					window.open fileURL, filename
				return
			).then(->
				comService.loading false
				return
			).catch ->
				Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
				comService.loading false
				return
			return


		$scope.details = (arg, scope) ->

			doc = scope.doc

			spiderdocsService.GetHistories
				DocIds: [doc.id]
			.then (histories) ->

				enVer = [
					"Created"
					"Import"
					"Scanned"
					"New version"
					"Check-in"
					"Export"
					"Save from Outlook"
					"Rollback version"
					"Saved as New Version"
					"Update version"
					# en_Events.Created
					# en_Events.NewVer
					# en_Events.Import
					# en_Events.SaveNewVer
					# en_Events.Scan
					# en_Events.Chkin
					# en_Events.UpVer
					]

				# events = (history for history in histories when not enVer.includes(history.event_name.trim()))
				for history in histories
					history.date = moment(history.date).format('l LT');
				events = histories

				versions = (history for history in histories when enVer.includes(history.event_name.trim()))


				modalInstance = $uibModal.open
					animation: true
					ariaLabelledBy: 'modal-title'
					ariaDescribedBy: 'modal-body'
					templateUrl: 'scripts/app/views/form-doc-details.html'
					controller: 'formDocDetailsCtrl'
					controllerAs: '$ctrl'
					backdrop: 'static'
					size: 'lg'
					resolve:
						# doc: ->
						# 	doc
						title: ->
							'[Document Details]'
						historyVersions: ->
							versions
						historyEvents: ->
							events
						properties: ->
							spiderdocsService.GetDetails({Documentid: doc.id})
			return


		$scope.folderClick = (node) ->
			$interval (->
				elm = $('sidebarjs [data-selected-folder-id]')[0]
				idFolder = $(elm).attr('data-selected-folder-id')
				Criteria =
					Date: [ {} ]
					AttributeCriterias: Attributes: []
				if idFolder
					Criteria.FolderIds = [ idFolder ]
				$scope.searchby Criteria
				return
			), 100, 1
			return

		$scope.sortBy = (key) ->

			$scope.sortby.key = key
			$scope.sortby.asc = not $scope.sortby.asc

			queryDocs();

		idcustomview = () =>
			path = $window.location.pathname;
			matched = path.toLowerCase().match(/workspace\/index\/([0-9]+)/);

			ans = if matched? then matched[1] else 0

			parseInt(ans)

		$scope.showCustomView = ->
			not (idcustomview() > 0 )

		$scope.showPageName = ->
			spiderdocsService.GetCustomViewsAsync()
			.then (vies) ->

				if idcustomview() is 0
					$scope.view.pagetitle = 'Local Database'
				else
					for v in vies when v.id is idcustomview()
						$scope.view.pagetitle = v.name
		
		$scope.checkoutWithFooter = (doc,footer) ->

			spiderdocsService.CheckOutWithFooterAsync(doc.id, footer).then((err) ->
				
				if true == angular.isString(err) and angular.isEmpty(err)
					$scope.data.docs.find((x) ->
						x.id == doc.id
					).id_status = en_file_Status.checked_out

					Notify 'The document has been checkedout!', null, null, 'success'

					$interval (->
						$window.location.href = './workspace/local'
						return
					), 1200, 1
				else

					Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'

				return

			).then(->
				comService.loading false
				return
			).catch ->
				comService.loading false
				return

	
		$scope.dblclickOnDoc = (doc) ->
			console.log "double clicked on #{doc.title}"

			spiderdocsService.GetPreferenceAsync() 
			
			.then (preference) ->

				switch preference.dblClickBehavior

					when en_DoubleClickBehavior.OpenToRead 
						
						$scope.download  {},{doc: doc}

					when en_DoubleClickBehavior.CheckOut
						
						if canCheckout {},{doc: doc}
							$scope.checkout {},{doc: doc}

					when en_DoubleClickBehavior.CheckOutFooter

						if canCheckout {},{doc: doc}

							$scope.checkoutWithFooter doc, true
							# $scope.checkout {},{doc: doc}, true
					else 
						$scope.download  {},{doc: doc}
					


		setupImportByDrag = ->
			
			leaves = ->
				
				clearTimeout $scope.pid.dragfile

				$scope.pid.dragfile = setTimeout( () ->

					angular.element('#upload1').addClass 'hidden'
					
					return
				, 100)

				return

			angular.element('html').on 'dragover', (e) ->
				
				leaves()

				e.preventDefault()
				e.stopPropagation()

				return
			
			# angular.element('body .screen').on 'mousedown', (e) ->

			# 	e.preventDefault()
			# 	e.stopPropagation()
				
			# 	return

			angular.element('html').on 'dragenter', (e) ->
				
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
					
					countByExpect = 0;
					
					$q.all _.values(files).map (file) ->
						
						comService.noop  comService.file2Doc file
						
					.then (docs) ->
				
						modalService.saveAsNew docs
						 
					#.then(queryDocs)
					
					.then (data) ->
						docs = data.docs
						func  = data.func
						reason = data?.reason
						options = data?.options						
						# format(docs)
						# $scope.data.docs = docs
						countByExpect = docs.length

						console.log data
						
						promise = {}
						comService.loading true

						Notify 'The file has started to checkin.It might take little bit longer...', null, null, 'info'


						switch func
							when 'new'
								console.log 'new was selected'

								promise = $q.all docs.map (doc) ->
											spiderdocsService.Upload doc._ref
											
											.then (tempid) ->
												spiderdocsService.ImportDbAsNew  tempid, doc, options
											

							when 'ver'
								console.log 'ver was selected'
								
								
								promise = $q.all docs.map (doc) ->
											spiderdocsService.SaveAsNewVer doc.id, doc._ref,  data.reason

						promise

					.then (docs) ->
					
						countByActual = ( doc for doc in docs when doc.id > 0).length


						if countByExpect is countByActual

							Notify 'File(s) have/has been saved into SpiderDocs', null, null, 'success'

						else 
							Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'

						

						angular.element('#upload1').addClass 'hidden'
						comService.loading false

						$scope.clearSearchCriteria()
					
					.catch ->

						comService.loading false

						angular.element('#upload1').addClass 'hidden'

						return
			return



		setupDbViews = (customviews) ->
			$scope.search.database_views = [{id:0,text:'Local',checked:true}]

			for view in customviews
				$scope.search.database_views.push
					id: view.id
					text: view.name
					checked: true

		ids4CustomViews = (customviews) ->

			ids = []
			for v in $scope.search.database_views when v.checked is true
				ids.push(v.id)

			ids = if $scope.search.database_views.length is ids.length then [] else ids

			ids

		init()

		return 
]
 
app.controller 'deleteFormCtrl', ($uibModalInstance, doc) ->
	$ctrl = this

	init = ->

	$ctrl.doc = doc
	$ctrl.reason = ''
	init()

	$ctrl.ok = ->
		$uibModalInstance.close $ctrl.reason
		return

	$ctrl.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	return