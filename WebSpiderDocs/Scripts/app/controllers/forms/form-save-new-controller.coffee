app.controller 'formSaveNewCtrl', (
$scope,
$uibModalInstance,
$uibModal,
docs,
title,
spiderdocsService,
comService,
docTypes,
en_Actions,
settings,
notificationgroups) ->

	# $scope.$ctrl = {}
	# $scope = this

	$scope.tab = 1
	$scope.title = title
	$scope.docTypes = docTypes
	$scope.canGo = false
	$scope.docs = docs
	$scope.settings = settings
	$scope.notificationgroups = notificationgroups

	$scope.$on 'dialog-savenew-close', (event, data) ->
		$uibModalInstance.dismiss 'cancel'
		return

	$scope.$on 'dialog-savenew-ok', (event, data) ->
		$uibModalInstance.close data
		return

	return

