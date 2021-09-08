

app.directive 'nodeTree', ->
	{
		template: '<node tk-node="node" ng-repeat="node in folders" ng-click="clickOnNT(node);" ></node>'
		replace: true
		transclude: true
		restrict: 'E'
		scope:
			folders: '=ngModel'
			permission: '=tkPermission'
			clickOnNT: '&tkClick'
			menu: '=tkMenu'
			
			
			menuFnc: '=tkMenuFnc'
		link: (scope, elm, attrs) ->
			# scope.clickb = function(a)
			# {
			# 	scope.clicka(a);
			# }
			# scope.clicka = function (node)
			# {
			# }
			return

	}
	
app.directive 'node', ($compile, spiderdocsService, comService, en_Actions) ->
	{
		restrict: 'E'
		replace: true		
		template: """
			<li class="parent_li" context-menu menu-items="menu" ng-show="node != null">
				<span class="leaf" ng-click="clickOnN(node);">
					<i class="glyphicon--folder glyphicon-folder-close"></i>
					{{node.text}}
					<i class="glyphicon {{ switcher( isLeaf(node), '', 'glyphicon-plus' )}}"></i>
				</span>
			</li>
			"""
		scope:
			node: '=tkNode'
			clickOnN: '&tkClick'
			defaultFolderid: '@tkDefault'
			menu: '=tkMenu'
			menuFnc: '=tkMenuFnc'
		link: (scope, elm, attrs) ->
			
			console.log("Compile node tag. #{scope.node.text}")

			def = parseInt(scope.defaultFolderid or 0)
			
			if scope.menuFnc?
				angular.extend(scope,scope.menuFnc);

			childFolders = (_scope, _elm, _attr) ->
				
				console.log("childFolders");
				console.log(_scope);

				if _scope.node.children.length == 0
					return

				spiderdocsService.cache.foldersL1(_scope.node.Id, _attr.permission or en_Actions.OpenRead).then (folders) ->

					_scope.folders = comService.nodesBy(_scope.node.Id, folders)
					_scope.permission = _attr.permission
					childNode = $compile('<ul ><node-tree ng-model="folders" tk-permission="permission" ng-click="clickOnN(node);" tk-default="{{defaultFolderid}}" tk-menu="menu" tk-menu-fnc="menuFnc" ></node-tree></ul>')(_scope)
					_elm.append childNode
					return

			parseInt(scope.node.Id) == parseInt(scope.defaultFolderid or 0) and $(elm).attr('data-selected-folder-id', scope.node.Id)
			$(elm).attr 'data-folder-id', scope.node.Id
			
			###
			Open/Close a leaf 
			###
			$(elm).find('span.leaf').on 'click', (e) ->
				$('[data-selected-folder-id]').removeAttr('data-selected-folder-id').removeClass 'data-text'
				$(elm).attr 'data-selected-folder-id', scope.node.Id
				$(elm).attr 'data-text', scope.node.text
				
				if scope.node.children.length > 0
					# showing children folders at the moment.
					if $(elm).find('ul').is(':visible')
						$(elm).find('ul').hide 'fast', (e) ->
							$(elm).find('ul').remove()
							$(elm).find('i').addClass('glyphicon-plus').removeClass 'glyphicon-minus'
							return
						return
					else
						childFolders(scope, elm, attrs).then ->
							$(elm).find('ui').show 'fast'
							$(elm).find('i').addClass('glyphicon-minus').removeClass 'glyphicon-plus'
							return
				return

			scope.switcher = (booleanExpr, trueValue, falseValue) ->
				if booleanExpr then trueValue else falseValue

			scope.isLeaf = (_data) ->
				if _data? and _data.children? and _data.children.length == 0
					return true
				false

			return

	}
