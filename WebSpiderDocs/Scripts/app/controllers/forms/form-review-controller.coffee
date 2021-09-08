app.controller 'formReviewCtrl', [
	'$scope'
	'$uibModal'
	'$uibModalInstance'
	'comService'
	'spiderdocsService'
	'en_file_Status'
	'doc'
	'currentReview'
	'en_ReviewStaus'
	'permissions'
	'en_Actions'
	'reviewUsers'
	'$q'
	'isOwner'
	'en_file_Sp_Status'
	'reviewHistory'
	'profile'
	'users'
	'en_ReviewAction'
	(
	$scope,
	$uibModal,
	$uibModalInstance,
	comService,
	spiderdocsService,
	en_file_Status,
	doc,
	currentReview,
	en_ReviewStaus,
	permissions,
	en_Actions,
	reviewUsers,
	$q,
	isOwner,
	en_file_Sp_Status,
	reviewHistory
	profile
	users
	en_ReviewAction
	) ->

		$ctrl = this
		$scope.model=
			comment:''
			reviewers: reviewUsers
			owner_comment: currentReview.owner_comment
			owner_review: users.find((u) -> u.id is currentReview.owner_id)?.name
			review_history: reviewHistory

		$scope.doc = {}
		$scope.title = "Start New Review"

		$scope.canGo = false
		$scope.validation = {}

		angular.extend $scope.doc, doc


		$scope.tab = 1

		init = ->

			$scope.model.action = 'start' if $scope.hasNotStarted()

			if not $scope.hasNotStarted()
				$scope.model.action = 'finish' if $scope.hasNotReviewed()

			populateForms()

			$scope.validateTogo()
			return


		$scope.checkoutInReview = () ->

			if $scope.checkouted()
				alert('This has already checkedout')
				return

			comService.loading true

			spiderdocsService.CheckOut([ doc.id ]).then((err) ->
				if false == angular.isString(err)

					doc.id_status = en_file_Status.checked_out

					Notify 'The document has been checkedout!', null, null, 'success'
					$interval (->
						$window.location.href = './workspace/local'
						return
					), 1200, 1
				else
					Notify 'Rejected. You might not have right permission to do that (' + doc.name_folder + ').', null, null, 'danger'
				return
			).then(->
				comService.loading false
				return
			).catch ->
				comService.loading false
				return
			return

		$scope.download = () ->

			spiderdocsService.GetDonloadUrls(doc.id_latest_version)

			.then((urls) ->

					if( urls.length > 0 )

						url = urls.pop();


						Notify("A download has been started. Check 'Download' Folder.", null, null, 'success');

						return comService.downloadBy(url);

					else
						Notify("Rejected. You might not have right permission to do that ("+doc.name_folder+")." , null, null, 'danger');
			)
			.then(() ->

				comService.loading(false);

			)
			.catch(() ->
				comService.loading(false);
			)

		$scope.ok = ->
			reviewers = (reviewer for reviewer in $scope.model.reviewers when reviewer.checked is true) ? []

			pass =
				checked: $scope.model.action
				review:
					id_version: doc.id_latest_version
					allow_checkout: $scope.model.allow_checkout
					id_users: reviewers.map (u) -> u.id
					deadline: moment($scope.model.deadline).format('YYYY-MM-DD HH:mm:ss')
					owner_comment: $scope.model.comment

			if $scope.model.allow_checkout is true

				$q.all (spiderdocsService.GetFolderPermissionsAsync(doc.id_folder, reviewer.id) for reviewer in reviewers when reviewer.checked is true)
				.then (response) ->
					if (result for result in response when result[en_Actions.CheckIn_Out].value is 1).length is response.length
						$uibModalInstance.close pass

					else
						Notify 'All reviewers must have checkout permission.', null, null, 'danger'

			else
				$uibModalInstance.close pass

			return

		$scope.cancel = ->
			$uibModalInstance.dismiss 'cancel'
			return

		$scope.checklength = ->
			$scope.model.comment.length >= 10

		$scope.hasSelectedReviewers = ->
			checked = reviewer.checked for reviewer in $scope.model.reviewers when reviewer.checked is true

			checked ? false

		$scope.checkCheckoutPermission = () ->

			checked = ($scope.model.allow_checkout is true && $scope.hasPermission2Checout()) or ($scope.model.allow_checkout is false)

			checked ? false

		$scope.validateTogo = ->
			$scope.canGo = false
			if $scope.hasNotStarted()
				$scope.canGo = true if $scope.checklength() and $scope.hasSelectedReviewers() #and $scope.checkCheckoutPermission()
			else if $scope.hasNotReviewed()
				$scope.canGo = true if $scope.checklength()


			$scope.canGo

		$scope.checkouted = () ->
			if typeof doc.id_status != 'number'
				$log.error 'doc.id_status must be number'
				debugger

			doc.id_status == en_file_Status.checked_out

		# $scope.isOwner = () ->

		# $scope.isnotstartedreviewUnreviewed = () ->
		# 	currentReview.status is en_ReviewStaus.UnReviewed
		# $scope.hasPermission2Review = () ->
		# 	permissions[en_Actions.Review] is 1

		$scope.hasPermission2Checout = () ->
			ans = false
			if not $scope.checkouted() and not $scope.isArchived() and permissions[en_Actions.CheckIn_Out].value is 1

				if $scope.hasNotStarted() #or $scope.hasNotReviewed() and $scope.isReviewer()
					ans = true

			ans

		$scope.isArchived = () ->
			doc.id_status is en_file_Status.archived

		$scope.hasNotReviewed = () ->
			currentReview.status is en_ReviewStaus.UnReviewed

		$scope.hasNotStarted = () ->
			currentReview.status is en_ReviewStaus.NotInReview

		$scope.hasReviewed = () ->
			currentReview.status is en_ReviewStaus.Reviewed

		$scope.isViewedByOwner = () ->
			isOwner
		$scope.isReviewer = () ->
			id = currentReview.review_users.find((u) -> u.id_user is profile.id)?.id_user ? 0
			id > 0

		$scope.finishMyReview = () ->
			ans = false
			reviewer = currentReview.review_users.find((u) -> u.id_user is profile.id)
			if reviewer? and reviewer.action is en_ReviewAction.Finalize
				ans = true

			ans
		# not review owner and not reviewer
		$scope.isViewedByUser = () ->
			not isOwner and not $scope.isReviewer()

		$scope.isOverdueReview = () ->
			doc.id_sp_status is en_file_Sp_Status.review_overdue
		$scope.comment = (comment) ->
			$scope.currentComment = comment

		populateForms = () ->

			if $scope.hasNotStarted()
				$scope.model.deadline = new Date()
				# $scope.model.deadline_date = new Date()
				# $scope.model.deadline_time = new Date()
				# $scope.model.allow_checkout = false
				# $scope.model.allow_checkout = false
				# $scope.model.owner_comment
			else if $scope.hasNotReviewed()
				$scope.model.deadline = moment(currentReview.deadline).toDate()
			else if $scope.isViewedByOwner()
				$scope.model.deadline = moment(currentReview.deadline).toDate()
			else if $scope.isViewedByUser()
				$scope.model.deadline = moment(currentReview.deadline).toDate()
			else if $scope.isReviewer()
				$scope.model.deadline = moment(currentReview.deadline).toDate()


		init()

		return
	]