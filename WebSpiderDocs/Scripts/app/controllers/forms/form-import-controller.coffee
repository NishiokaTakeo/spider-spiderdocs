app.controller 'formImportCtrl', ($uibModalInstance, doc, reasonNewVersion) ->
	$ctrl = this

	init = ->
		bindDragEvents()
		return

	bindDragEvents = ->
		angular.element('body').on 'dragover', (e) ->
			e.preventDefault()
			e.stopPropagation()
			return
		angular.element('body').on 'dragenter', (e) ->
			e.preventDefault()
			e.stopPropagation()
			return
		# setTimeout(function(){
		# 	angular.element('#upload1').on('drop', drop);
		# }, 1000)
		return

	$ctrl.doc = {}

	angular.extend $ctrl.doc, doc
	$ctrl.needReason = reasonNewVersion
	$ctrl.file = undefined
	$ctrl.canGo = false
	$ctrl.reason = ''
	init()

	$ctrl.ok = ->
		$uibModalInstance.close [
			$ctrl.file
			$ctrl.reason
		]
		return

	$ctrl.cancel = ->
		$uibModalInstance.dismiss 'cancel'
		return

	$ctrl.deletePic = ->
		$ctrl.file = undefined
		$ctrl.validateTogo()
		return

	$ctrl.onDrop = (e) ->
		e.preventDefault()
		e.stopPropagation()
		$ctrl.error = ''
		$ctrl.file = undefined
		files = e.originalEvent.dataTransfer.files
		if files.length == 1 and files[0].name.split('.').pop().toLowerCase() == doc.extension.replace('.', '').toLowerCase()
			$ctrl.file = files[0]
			$ctrl.validateTogo()
			#$ctrl.fileupload = "Click to delete";
			#$uibModalInstance.close(e);
		else
			$ctrl.error = 'File must be same extension (' + $ctrl.doc.extension + ') and drop only one file.'
		return

	$ctrl.checklength = ->
		$ctrl.reason.length >= 10

	$ctrl.validateTogo = ->
		$ctrl.canGo = false
		if $ctrl.needReason
			if $ctrl.file
				if $ctrl.checklength()
					$ctrl.canGo = true
		else
			if $ctrl.file
				$ctrl.canGo = true
		$ctrl.canGo

	return
