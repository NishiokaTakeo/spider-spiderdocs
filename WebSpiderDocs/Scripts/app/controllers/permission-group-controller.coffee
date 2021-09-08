app.controller 'permissionGroupController',
[
	'$scope'
	'$q'
	'spiderdocsService'
	'comService'
	'$uibModal'
(
	$scope,
	$q,
	spiderdocsService,
	comService,
	$uibModal
) ->
	$scope.selected =
		group: 0

	$scope.data =
		groups: []
		users:[]

	$scope.view =
		data:
			users:[]
			groups:[]

	init = ->
		$q.all([
			spiderdocsService.GetGroupsAsync(),
			spiderdocsService.GetUsersAsync()
		])
		.then (res) ->
			groups = res[0]
			users = res[1]
			# spiderdocsService.GetGroupsAsync().then((res) ->

			# format(res)

			$scope.data.groups = groups
			$scope.data.users = users

			$scope.view.data.groups = groups;
			$scope.view.data.users = users;

			$scope.groupChange(groups[0])

			return
		.then ->
			comService.loading false
			return

	$scope.edit = (group) ->
		save('edit',group);

	$scope.new = () ->
		save('new');

	$scope.groupChange = (group) ->

		$scope.selected.group = group.id

		spiderdocsService.GetUserIdInGroupAsync (group.id)
		.then (ids) ->
			_users = []
			angular.copy $scope.data.users, _users

			for user in _users when ids.includes(user.id)
				user.checked = true

			$scope.view.data.users = _users.sort (a,b) ->
				if a.checked is true then -1 else 1

	$scope.toggleSelected = (user) ->
		idGroup = $scope.selected.group
		idUser = user.id

		name = group.group_name for group in $scope.data.groups when group.id is $scope.selected.group

		promise = spiderdocsService.DeleteUserGroupAsync(idGroup,idUser)

		if user.checked
			promise.then (success) ->
				if success is true then spiderdocsService.AssignGroupAsync(idGroup,idUser)
			.then (success) ->

				if success is true
					Notify "A user has been added to #{name}", null, null, 'success'
				else
					Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'
		else
			promise.then (success) ->
				Notify "A user has been removed from #{name}", null, null, 'success'

	init()
]
