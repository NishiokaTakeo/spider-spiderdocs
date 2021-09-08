app.controller 'OptionsPreferenceController', [
	'$scope'
	'$q'
	'spiderdocsService'
	'comService'
	'en_DoubleClickBehavior'
	'modalService'
	(
		$scope,
		$q,
		spiderDocsSrv,
		comService,
		en_DoubleClickBehavior,
		modalService
	) ->
	 
		# $scope.data =
		# 	permissionTitles: []
		# 	types: []
		# 	attrs: []
		# 	map: []
		# 	# allowed: []
		# 	users: []
		# 	menu:{ items:[], func:{} }
		$scope.data = {}
		$scope.view =
			models: {}

  
		init = ->

			$q.all([
				
				spiderDocsSrv.GetPreferenceAsync()

			])
			.then (response) ->
				
				preference = response[0]
 
				preference = convertDateForView(preference)		

				angular.extend $scope.view.models, preference; 
				
				return
			.then ->
				comService.loading false
				return
   
 
		$scope.savePreference = () ->
			spiderDocsSrv.SavePreferenceAsync $scope.view.models
  
			.then (success) ->

				if success is true  
					Notify 'The change applied.', null, null, 'success'				
				else 
					Notify 'Rejected. You might not have right permission to do that .', null, null, 'danger'
	
		$scope.enabledMultiAddress = () ->
			$scope.view.models.feature_multiaddress

		convertDateForView = (preference)	 ->
			
			preference.dblClickBehavior = preference.dblClickBehavior.toString();

			preference




		init()
]