app.controller 'formNewUserCtrl',
[
	'$scope'
	'$uibModalInstance'
	'spiderdocsService'
	'comService'
	'permissionLevels'
	'user'
(
	$scope
	$uibModalInstance
	spiderdocsService
	comService
	permissionLevels
	user
) ->


	init = ->
		$scope.title = if not user? then '[New User]' else '[Edit User - ' + user.name + ']'

		if user?
			angular.extend $scope.model, user
			$scope.model.access_level = findPermissionBy(user?.id_permission)
		else
			$scope.model =
				access_level: findPermissionBy(user?.id_permission)
				name: ''
				password: ''
				login: ''

		# if not user? then $scope.data.access_level[1] else
		# $scope.model.access_level = $scope.data.access_level[1];

		return

	$scope.checkName = () ->
		$scope.model.name = '' if not $scope.model.name?
		$scope.model.name.length > 0

	$scope.checkPassword = () ->
		$scope.model.password.length > 6 and $scope.model.password is $scope.model.password_confimation
	$scope.checkEmail = () ->
		$scope.model.email.indexOf '@' and $scope.model.email.length > 4
	$scope.checkLogin = () ->
		$scope.model.login.length > 4

	validateTogo = ->
		$scope.canGo = false

		for control in $scope.myform.$$controls
			control.$setDirty()
			control.$validate()

		if $scope.myform.$invalid is true
			Notify('Can not proceed. <br/>Please Fix error(s)', null, null, 'danger');

			return

		if not $scope.checkPassword()
			return

		# $scope.canGo = checkName() and checkPassword() and checkEmail() and checkLogin()

		$scope.canGo = true


	# $scope.doc = {}
	# angular.copy doc, $scope.doc
	# $scope.types = types
	# $scope.type = types.find((x) ->
	# 	x.id == doc.id_docType
	# ) or id: 0
	# $scope.en_file_Status = en_file_Status
	# $scope.checkout = doc.id_status == en_file_Status.checked_out
	# $scope.canGo = false
	# $scope.selectedAttrs = {}
	# $scope.defaultAttrs = {}


	# $scope.folderclicked = ->
	# 	openFolderTree []
	# 	return

	$scope.ok = ->
		if not $scope.validateTogo()
			return

		deepCopiedUser = angular.extend {}, user
		angular.extend deepCopiedUser , $scope.model

		data =
			user: deepCopiedUser

		$uibModalInstance.close data
		return

	$scope.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$scope.validateTogo = validateTogo

	findPermissionBy = (id)->
		ans
		if id?
			ans = level for level in $scope.data.access_level when level.id is id
		else
			ans = $scope.data.access_level[1]

		ans

	$scope.model = {}
	$scope.data =
		access_level: permissionLevels



	init()
	return
]