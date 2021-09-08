app.controller 'formSaveGroupCtrl',
[
	'$scope'
	'$uibModalInstance'
	'spiderdocsService'
	'comService'
	'group'
(
	$scope
	$uibModalInstance
	spiderdocsService
	comService
	group
) ->


	init = ->
		$scope.title = if not group? then '[New Group]' else '[Edit Group - ' + group.name + ']'

		$scope.model =
			id:0
			group_name: ''
			obs: ''
			ordination: 1


		if group?
			angular.extend $scope.model, group

		return

	validateTogo = ->
		$scope.canGo = false

		for control in $scope.myform.$$controls
			control.$setDirty()
			control.$validate()

		if $scope.myform.$invalid is true
			Notify('Can not proceed. <br/>Please Fix error(s)', null, null, 'danger');

			return

		$scope.canGo = true


	$scope.ok = ->
		if not $scope.validateTogo()
			return

		deepCopiedGroup = angular.extend {}, group
		angular.extend deepCopiedGroup , $scope.model

		data =
			group: deepCopiedGroup

		$uibModalInstance.close data
		return

	$scope.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$scope.validateTogo = validateTogo


	$scope.model = {}
	$scope.data = {}



	init()
	return
]