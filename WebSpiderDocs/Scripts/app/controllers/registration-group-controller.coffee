app.controller 'registrationGroupController',
[
	'$scope',
	'spiderdocsService',
	'comService',
	'$uibModal',
(
	$scope,
	spiderdocsService,
	comService,
	$uibModal
) ->

	$scope.data =
		groups: []


	init = ->
		spiderdocsService.GetGroupsAsync().then((res) ->

			# format(res)

			$scope.data.groups = res

			return
		).then ->
			comService.loading false
			return

	$scope.edit = (group) ->
		save('edit',group);

	$scope.new = () ->
		save('new');

	save = (mode, group) ->
		modalInstance = $uibModal.open(
			animation: true
			ariaLabelledBy: 'modal-title'
			ariaDescribedBy: 'modal-body'
			templateUrl: 'scripts/app/views/form-save-group.html'
			controller: 'formSaveGroupCtrl'
			controllerAs: '$ctrl'
			backdrop: 'static'
			size: 'sm'
			resolve:
				group: ->
					group
		)

		modalInstance.result.then ((data) ->
			comService.loading true

			group =
				id : data.group?.id ? 0
				group_name : data.group.group_name
				obs : data.group.obs
				ordination : data.group.ordination
				# is_admin : data.group.is_admin

			spiderdocsService.SaveGroupAsync group
				.then (_group) ->

					found = $scope.data.groups.find ((u) -> u.id is _group.id)

					if found?
						angular.extend found, _group
					else
						$scope.data.groups.push(_group)

					if _group.id > 0
						Notify 'A group has been added', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'

					comService.loading false
			return
		), ->
		return

	init()
]
