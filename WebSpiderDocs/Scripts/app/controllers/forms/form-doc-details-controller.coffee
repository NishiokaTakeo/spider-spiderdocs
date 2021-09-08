app.controller 'formDocDetailsCtrl', (
$scope,
$uibModal,
$uibModalInstance,
comService,
title,
historyVersions,
historyEvents,
properties
) -> 

	$ctrl = this
	# $scope.doc = {}
	$scope.versions = historyVersions
	$scope.events = historyEvents
	$scope.property = properties
	$scope.title = title

	$scope.canGo = true
	$ctrl.validation = {}

	angular.extend {}, properties, $scope.property
	$scope.tab = 1

	init = ->
		setupProperty()
		return


	$scope.ok = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$scope.cancel = ->
		return

	$scope.validateTogo = ->

		$scope.canGo

	setupProperty = () ->
		# $scope.property.eventText = $scope.property.event_name + ' ' + $scope.property.comments

		size = $scope.property.size / 1024
		$scope.property.sizeText = if size < 1 then '< 1 KB' else size + ' KB'

		$scope.property.dateText = if properties.date.startsWith('000') then 'There is no history' else moment(properties.date).format('l LT');
		$scope.property.reviewDeadLineText = if properties.reviewDeadLine.startsWith('000') then 'There is no history' else moment(properties.reviewDeadLine).format('l LT');

		$scope.property.reviewersText = if not $scope.property.reviewers? or $scope.property.reviewers is '' then 'There is no history' else $scope.property.reviewers

		return
	init()

	return
