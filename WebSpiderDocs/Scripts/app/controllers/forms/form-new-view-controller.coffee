app.controller 'formNewViewCtrl', [
	'$scope'
	'$timeout'
	'$uibModalInstance'
	'spiderdocsService'
	'comService'
	'$uibModal'
	'sources'
	'customviews'
	'customview'
	'$location'
	($scope,$timeout,$uibModalInstance, spiderdocsService, comService, $uibModal,sources,customviews,customview, $location) ->
		$ctrl = this

		init = =>
			if customview?.ref?.id_source > 0
				$ctrl.view.models.source = s for s in $ctrl.view.data.sources when s.id is customview?.ref?.id_source
			else
				$ctrl.view.models.source = $ctrl.view.data.sources[0]

			this.validateTogo()

			return
		@isadmin = ->
			$location.search().role?.toLowerCase() is 'admin'

		@validateTogo = ->
			$ctrl.canGo = true

			if not this.nameOK(false)
				$ctrl.canGo = false
				return

			if not this.descriptionOK(false)
				$ctrl.canGo = false
				return

			if not this.sourceOK(false)
				$ctrl.canGo = false
				return

			$ctrl.canGo

		@nameOK = (message = true) ->
			ans = false
			ans = if $ctrl.view.models.name? and $ctrl.view.models.name isnt '' then true else false


			# check name exists
			for view in customviews when view.name.trim() is $ctrl.view.models.name.trim() and ( customview?.ref? and view.id isnt customview?.ref?.id )
				ans = false

			if message is true and ans is false
				Notify 'The name is not available. Use different.',null,null,'warning'

			# this.canGo = ans
			ans

		@descriptionOK = (message = true) ->
			ans = if $ctrl.view.models.description? and $ctrl.view.models.description isnt '' then true else false

			if message is true and ans is false
				Notify 'The description is not available. Use different.', null, null, 'warning'

			# this.canGo = ans
			ans

		@sourceOK = (message = true) ->
			ans = true

			ans = false unless $ctrl.view.models.source.id > 0

			if message is true and ans is false
				Notify 'Please select location.', null, null, 'warning'

			# this.canGo = ans
			ans




		$ctrl.ok = ->

			if this.validateTogo() is true
				# $uibModalInstance.close $ctrl.view.models
				webview =
					id: $ctrl.view.models.id
					name: $ctrl.view.models.name
					description: $ctrl.view.models.description
					id_source: $ctrl.view.models.source.id

				spiderdocsService.SaveCustomView(webview).then (_result) =>
					$uibModalInstance.close _result

			return

		$ctrl.cancel = ->
			$uibModalInstance.dismiss 'cancel'
			return


		@manageNewSource = ->
			$ctrl.view.managingDataLocation = not $ctrl.view.managingDataLocation

			$ctrl.view.text.manageDataLocation = if $ctrl.view.managingDataLocation is true then 'Close' else 'Manage Data Location'
			return



		@updateSource = (source) ->

			if $ctrl.data.pssource? then $timeout.cancel($ctrl.data.pssource)

			$ctrl.data.pssource = $timeout( () ->

				dbsource = {name:'',data_source:'', sql_mode: null}
				if source?
					dbsource = angular.extend {}, source
					dbsource.sql_mode = source.vendor.id

				spiderdocsService.SaveCustomViewSourceAsync( dbsource )
				.then (sources) ->

					if not source?
					# 	found = s for s in $ctrl.view.data.sources when s.id is source.id
					# 	angular.extend(found, source)
					# 	# .push sources[sources.length - 1];
					# else
						$ctrl.view.data.sources.push sources[sources.length - 1];

					$ctrl.data.pssource = null

			, 400);


		@deleteSource = (source) ->

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
						'WARNING'
					message: ->
						"Are you sure to delete this source ? ( #{source.name} )"
			)
			modalInstance.result.then ((result) ->

				if result == 'ok'
					return spiderdocsService.DeleteCustomViewSourceAsync({ id: source.id } ).then((ans) ->
						if ans?.id is 0

							$ctrl.view.data.sources = ( _source for _source in $ctrl.view.data.sources when _source.id isnt source.id)

							Notify "'#{source.name}' has been deleted!", null, null, 'success'

						else
							Notify "Rejected. You might not have right permission to do that ( #{source.name} ).", null, null, 'danger'
						return
					)
				else

				return

			), ->
				# $log.info('Modal dismissed at: ' + new Date());
				return
			return

		# this.validateTogo()
		@vendorBy = (id) ->
			vendor = v for v in vendors when v.id is id
			vendor






		$ctrl.canGo = false
		vendors = [{id:0,name:'SQL Server'},{id:1,name:'My SQL'}];	#Replace this later to get them from the dataase;


		$ctrl.view =
			managingDataLocation: false
			titlePrefix: 'New'
			data:
				sources: sources #[{name:'Local',val:1},{name:'DB',val:2}]
				# dbsources: [{name:'Azure',val:1},{name:'Amazon',val:2}]
				customviews: customviews
				vendors: vendors
			models:
				id: customview?.ref?.id or 0
				name: customview?.ref?.name or ''
				description: customview?.ref?.description or ''
				# vendors: this.vendorBy( customview?.ref?.sql_mode or 0 )

			text:
				manageDataLocation: 'Manage Data Location'

		$ctrl.data =
			pssource: undefined

		init()

		return


]


app.filter 'datasourceFilter', 	() ->
		(sources) ->
			( source for source in sources when source.id > 1)