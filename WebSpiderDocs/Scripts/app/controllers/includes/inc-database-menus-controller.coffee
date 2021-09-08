app.controller 'incDbMenuCtrl', [
	'$scope'
	'$uibModal'
	'spiderdocsService'
	'comService'
	(
	$scope
	$uibModal
	spiderdocsService
	comService
	) ->

		# parent =;
		# $scope.$parent.incDbmenuCtrl = $scope;

		$scope.dbmenus =[
			{p:'./index', n:'Primary Shared Database', id: null, cls: ['menu-item__label--shared-database']},
			{p:'./local', n:'My Database', id: null}
		]

		# $scope.title = $scope.title
		$scope.title = 'Save New Version'

		# angular.copy $scope.docs, $scope.docs

		init = ->

			spiderdocsService.GetCustomViewsAsync()

			.then (customviews) ->

				for view in customviews when view.name.toLowerCase() isnt 'local'
					$scope.dbmenus.push
						p: 'CustomView'
						n: view.name
						id: view.id
						ref: view

			return

		$scope.delete = (item) ->
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
						'DELETE VIEW'
					message: ->
						"Are you sure to delete this view? (#{item.n})"

			modalInstance.result.then ((ok) ->

				if ok is "ok"
					spiderdocsService.DeleteCustomView({id:item.id}).then (ans) ->
						if ans.inactive is true

							$scope.dbmenus = $scope.dbmenus.filter (x) ->
								x.id != item.id

							Notify 'The view has been deleted!', null, null, 'success'
						else
							Notify 'Rejected. You might not have right permission to do that.', null, null, 'danger'
						return


			), ->
				# $log.info('Modal dismissed at: ' + new Date());
				return
			return

		$scope.newView = (item) ->

			modalInstance = $uibModal.open
						animation: true
						# windowClass: 'modal--property'
						ariaLabelledBy: 'modal-title'
						ariaDescribedBy: 'modal-body'
						templateUrl: 'scripts/app/views/form-new-view.html'
						controller: 'formNewViewCtrl'
						controllerAs: '$ctrl'
						backdrop: 'static'
						size: 'sm'
						resolve:
							sources: ->
								spiderdocsService.GetCustomViewSources()
							customviews: ->
								spiderdocsService.GetCustomViewsAsync()
							customview: ->
								item

			modalInstance.result.then ((savedCustomView) ->

					if savedCustomView?.id > 0
						Notify 'The view has been saved!', null, null, 'success'

						updated =
								# p:'CustomView'
								n:savedCustomView.name
								id:savedCustomView.id
								ref: savedCustomView

						# $scope.$emit 'dbmenus-add', updated


						edited = menu for menu in $scope.dbmenus when menu.id is savedCustomView.id

						if edited?.id > 0
							angular.extend edited, updated
						else
							$scope.dbmenus.push updated

					else
						Notify 'Rejected. You might not have right permission to do that.', null, null, 'danger'

						return
					), ->

				return
			modalInstance.rendered.then ->
				return


		init()

		return
]