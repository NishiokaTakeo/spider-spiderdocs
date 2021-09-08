app.controller 'formSaveDocumentTypeCtrl',
[
	'$scope'
	'$uibModalInstance'
	'spiderdocsService'
	'comService'
	'doctype'
(
	$scope
	$uibModalInstance
	spiderdocsService
	comService
	doctype
) ->


	init = ->
		$scope.title = if not doctype? then '[New Document Type]' else '[Edit Document Type - ' + doctype.type + ']'

		$scope.model =
			id:0
			doctype: {}


		if doctype?
			angular.extend $scope.model.doctype, doctype

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

		deepCopiedDoctype = angular.extend {}, $scope.model.doctype
		angular.extend deepCopiedDoctype , $scope.model.doctype

		data =
			doctype: deepCopiedDoctype

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