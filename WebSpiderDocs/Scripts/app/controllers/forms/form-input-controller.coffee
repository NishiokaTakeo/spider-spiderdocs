app.controller 'formInputCtrl', [
	'$uibModalInstance'
	'title'
	'message'
	'defInput'
	'reasonMinLen'
	'needsInput'
	'inputType'
(
	$uibModalInstance
	title
	message
	defInput
	reasonMinLen
	needsInput
	inputType
) ->
	$ctrl = this

	init = ->

	$ctrl.title = title
	$ctrl.message = message
	$ctrl.reasonMinumLen = reasonMinLen
	$ctrl.canGo = false
	$ctrl.input = defInput
	$ctrl.needsInput = needsInput
	$ctrl.inputType =  if inputType? then inputType else 'text'
	init()

	$ctrl.ok = ->
		$uibModalInstance.close $ctrl.input
		return

	$ctrl.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$ctrl.checklength = ->
		ok = false

		if $ctrl.input?
			ok = $ctrl.input.length >= $ctrl.reasonMinumLen
			
		ok

	$ctrl.validateTogo = ->
		$ctrl.canGo = false
		if $ctrl.needsInput
			if $ctrl.checklength()
				$ctrl.canGo = true
		else
			$ctrl.canGo = true
		$ctrl.canGo

	return
]