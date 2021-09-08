app.controller 'formConfimationCtrl',
[
	'$uibModalInstance'
	'title'
	'message'
	'options'
(
	$uibModalInstance
	title
	message
	options
) ->
	$ctrl = this

	init = ->

	$ctrl.title = title
	$ctrl.message = message
	$ctrl.options = options 

	$ctrl.ok = ->
		$uibModalInstance.close 'ok'
		return

	$ctrl.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	init()
]