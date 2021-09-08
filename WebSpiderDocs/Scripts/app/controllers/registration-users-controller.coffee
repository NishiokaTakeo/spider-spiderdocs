app.controller 'registrationUserController',
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
		users: []


	init = ->
		spiderdocsService.GetUsersAsync().then((res) ->

			# format(res)

			$scope.data.users = res

			return
		).then ->
			comService.loading false
			return

	$scope.edit = (user) ->

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
					'[Current Password Confimation]'
				message: ->
					'Please enter current password'
				defInput: ->
					''
				reasonMinLen: ->
					1
				needsInput: ->
					true
				inputType:->
					'password'
		)
		modalInstance.result.then (password) ->

			comService.loading true
			spiderdocsService.GetUserByLoginPasswordAsync(user.login, password).then((found) ->
				if found.id > 0
					save('edit',user);
				else
					Notify 'Password is not correct.', null, null, 'danger'

				comService.loading false
				return
			).catch ->
				comService.loading false
				return
			return
		return






	$scope.new = () ->
		save('new');

	save = (mode,user) ->
		modalInstance = $uibModal.open(
			animation: true
			ariaLabelledBy: 'modal-title'
			ariaDescribedBy: 'modal-body'
			templateUrl: 'scripts/app/views/form-new-user.html'
			controller: 'formNewUserCtrl'
			controllerAs: '$ctrl'
			backdrop: 'static'
			size: 'sm'
			resolve:
				user: ->
					user

				permissionLevels: ()->
					spiderdocsService.GetPermissionLevels()

		)

		modalInstance.result.then ((data) ->
			comService.loading true

			user =
				id : data.user?.id ? 0
				login : data.user.login
				name : data.user.name
				id_permission : data.user.access_level.id
				email : data.user.email
				reviewer : data.user.reviewer
				active : data.user.active
				password : data.user.password
				name_computer : data.user.name_computer

			spiderdocsService.SaveUserAsync user
				.then (_user) ->

					# if mode is 'new'
					# 	$scope.data.users.push(_user);
					# else if mode is 'edit'
					found = $scope.data.users.find ((u) -> u.id is _user.id)

					if found?
						angular.extend found, _user
					else
						$scope.data.users.push(_user)

					if _user.id > 0
						Notify 'A user has been added', null, null, 'success'
					else
						Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'

					comService.loading false
			return
		), ->
		return

	init()
]
