app.controller 'formReasonCtrl', ($uibModalInstance, title, message, reasonRequired, reasonMinLen) ->
	$ctrl = this

	init = ->

	$ctrl.title = title
	$ctrl.message = message
	$ctrl.needReason = reasonRequired
	$ctrl.reasonMinumLen = reasonMinLen
	$ctrl.canGo = false
	$ctrl.reason = ''
	init()

	$ctrl.ok = ->
		$uibModalInstance.close $ctrl.reason
		return

	$ctrl.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$ctrl.checklength = ->
		$ctrl.reason.length >= $ctrl.reasonMinumLen

	$ctrl.validateTogo = ->
		$ctrl.canGo = false
		if $ctrl.needReason
			if $ctrl.checklength()
				$ctrl.canGo = true
		else
			$ctrl.canGo = true
		$ctrl.canGo

	return
