
((window, document) ->

	angular.module('ng.multicombo', ['ng']).directive('ngMultiCombo', ['$window','$timeout', ($window,$timeout) -> {

		template: """
				<div class="form-field">
					<label for="el-lead-source" class="form-field__label">
						{{title}}
					</label>
					<div class="form-field__select">
						<div id="{{identify}}_sl" class="leadStatus dd_chk_select"
								style="height: 40px; padding: 8px 0px 0px 10px; background-position: 99% 10px; background-image: none;">
							<div id="caption">
								Please Select
							</div>
							<div id="{{identify}}_dv" class="dd_chk_drop"
									style="position: absolute; top: 40px; display: none;">
								<div id="checks">
									<span ng-show="showAll">
										<div class="form-field form-field--checkbox" style="margin-bottom:20px">
											<input type="checkbox" name="customDDL-all{{identify4all}}_sll" id="customDDL-all{{identify4all}}_sll" ng-model="select" ng-click="toggleSlect()">
											<label for="customDDL-all{{identify4all}}_sll" class="form-field__label">
												Select All
											</label>
										</div>
									</span>
									<span id="" selectboxwidth="150px">
										<div class="form-field form-field--checkbox" ng-repeat="opt in source">

											<input id="customDDL-{{toAlphabet(title)}}-opt_{{$index}}"
													type="checkbox" name="customDDL-opt{{$index}}" value="{{opt.id}}" ng-model="opt.checked" ng-change="onchanged(opt)">
											<label for="customDDL-{{toAlphabet(title)}}-opt_{{$index}}" class="form-field__label">
												{{opt.text}}
											</label>
											<br>
										</div>
									</span>
								</div>
							</div>
						</div>
					</div>
				</div>
		"""
		replace: true
		transclude: true
		restrict: 'E'
		scope:
			title: '@tkTitle'
			source: '=ngModel'
			defaultChecked: '=tkDefault'
			showAll: '@tkShowAll'
			onchanged: '&onChanged'
			disabled: '=ngDisabled'

		link: ($scope,elm,attrs) ->

			$scope.select = false

			$scope.identify = identify = 'ddl' + String(Math.abs(Math.random())).replace('.','')
			$scope.identify4all = Math.random().toString().replace('.','').substring(1,7);

			$timeout( () ->
				if $scope.disabled isnt true
					($window.identify = new DropDownScript(identify,'_dv','_sl', () -> , false, false, false)).init()
			, 0);


			$scope.toggleSlect = () ->
				(opt.checked = not $scope.select for opt in $scope.source)
				return


			$scope.toAlphabet = (text) ->
				a = text.match(/[a-zA-Z]+/g).join('')
				a= a.toLowerCase()
				a


	}])



)(window, document)
