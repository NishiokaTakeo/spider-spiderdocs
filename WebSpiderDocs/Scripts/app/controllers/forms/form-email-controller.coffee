app.controller 'FormEmailCtrl', (
	$uibModalInstance,
	spiderdocsService,
	comService,
	$uibModal,
	tagService,
	title,
	subject,
	body,
	to,
	cc,
	bcc,
	attachments,
	en_file_Status,
	en_Actions) ->

		$ctrl = this
		$ctrl.errors = []
		$ctrl.title = title
		$ctrl.email =
			subject: subject
			to: to
			cc: cc
			bcc: bcc
			attachments: attachments  ? []
			body: ''

		$ctrl.show =
			cc: false
			bcc: false

		init = ->
			validateTogo()
			return

		validateTogo = ->
			$ctrl.canGo = false
			if checkForm()
				$ctrl.canGo = true

			$ctrl.canGo


		checkForm = ->
			$ctrl.errors = []
			hasError = true

			if !!$ctrl.email.subject and !!$ctrl.email.to and !!$ctrl.email.body
				hasError = false

			if $ctrl.email
				for email in $ctrl.email.to.split(';') #when email isnt ''
					hasError = not comService.validateEmail(email)
					$ctrl.errors.push("Email format must be valid.") if hasError is true

			!hasError

		$ctrl.en_file_Status = en_file_Status
		$ctrl.canGo = false
		$ctrl.selectedAttrs = {}
		$ctrl.defaultAttrs = {}
		init()

		$ctrl.folderclicked = ->
			openFolderTree []
			return

		$ctrl.ok = ->
			_to = (to for to in $ctrl.email.to.split(';') when !!to) ? []
			_cc =  (cc for cc in $ctrl.email.cc.split(';') when !!cc) ? []
			_bcc = (bcc for bcc in $ctrl.email.bcc.split(';') when !!bcc) ? []

			form =
				to: _to
				cc: _cc
				bcc: _bcc
				subject: $ctrl.email.subject
				body: $ctrl.email.body
				attachments: (img.filename for img in $ctrl.email.attachments)

			$uibModalInstance.close form
			return

		$ctrl.cancel = ->
			$uibModalInstance.dismiss 'cancel'
			return

		$ctrl.validateTogo = validateTogo
		return
