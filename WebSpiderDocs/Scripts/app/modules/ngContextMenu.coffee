ngContextMenu = angular.module('ngContextMenu', [])

###
ngContextMenu.directive('cellHighlight', function() {
  return {
    restrict: 'C',
    link: function postLink(scope, iElement, iAttrs) {
      iElement.find('td')
        .mouseover(function() {
          $(this).parent('tr').css('opacity', '0.7');
        }).mouseout(function() {
          $(this).parent('tr').css('opacity', '1.0');
        });
    }
  };
});
###

ngContextMenu.directive 'contextMenu', [
	'$document'
	'$window'
	($document, $window) ->
		# Runs during compile
		{
			restrict: 'A'
			link: ($scope, $element, $attr) ->
				contextMenuElm = undefined
				$contextMenuElm = undefined
				windowWidth = window.innerWidth
				windowHeight = window.innerHeight
				contextMenuWidth = undefined
				contextMenuHeight = undefined
				contextMenuLeftPos = 0
				contextMenuTopPos = 0
				$w = $($window)
				caretClass =
					topRight: 'context-caret-top-right'
					topLeft: 'context-caret-top-left'
					bottomRight: 'context-caret-bottom-right'
					bottomLeft: 'context-caret-bottom-left'
				menuItems = $attr.menuItems

				# console.log "contextMenu, scope = "
				# console.log $scope

				# if $scope[menuItems].menuFnc?
				# 	angular.extend($scope,$scope[menuItems].menuFnc);

				###*
				# CONTEXT MENU
				# @param  {[type]} evt [description]
				# @return {[type]}     [description]
				###

				createContextMenu = ->
					fragment = document.createDocumentFragment()
					contextMenuElm = document.createElement('ul')
					$contextMenuElm = $(contextMenuElm)
					contextMenuElm.setAttribute 'id', 'context-menu'
					contextMenuElm.setAttribute 'class', 'custom-context-menu'
					mountContextMenu $scope[menuItems], fragment
					contextMenuElm.appendChild fragment
					document.body.appendChild contextMenuElm
					contextMenuWidth = $contextMenuElm.outerWidth(true)
					contextMenuHeight = $contextMenuElm.outerHeight(true)
					return
 
				mountContextMenu = (menuItems, fragment) ->

					menuItems.forEach (_item) ->
						li = document.createElement('li')
						li.innerHTML = '<a>' + _item.label + ' <span class="right-caret"></span></a>'
						# check if menu is active or not.
						# override by dynamic check.
						active = if typeof _item.active != 'function' then _item.active else _item.active($attr, $scope)

						if _item.action and active
							
							li.addEventListener 'click', (->
							
								if typeof $scope[_item.action] != 'function'
									return false

								$scope[_item.action] $attr, $scope
								removeContextMenu()
								return
							), false

						if _item.divider
							addContextMenuDivider fragment
						if !active
							li.setAttribute 'class', 'disabled'
						if _item.subItems
							addSubmenuItems _item.subItems, li, active
						fragment.appendChild li
						return
					return

				addSubmenuItems = (subItems, parentLi, parentIsActive) ->
					$li = angular.element(parentLi)
					parentLi.setAttribute 'class', 'dropdown-submenu'
					if !parentIsActive
						parentLi.setAttribute 'class', 'disabled'
					ul = document.createElement('ul')
					ul.setAttribute 'class', 'dropdown-menu'
					mountContextMenu subItems, ul
					$li.on 'mouseover.dirContextMenu', (evt) ->
						pos = $li.position()
						width = $li.width()
						$li.find('.dropdown-menu').css
							'display': 'block'
							'top': pos.top + 'px'
							'left': width + 'px'
						return
					$li.on 'mouseleave.dirContextMenu', (evt) ->
						$li.find('.dropdown-menu').css 'display': 'none'
						return
					parentLi.appendChild ul
					return

				addContextMenuDivider = (fragment) ->
					divider = document.createElement('li')
					divider.className = 'divider'
					fragment.appendChild divider
					return

				###*
				# Removing context menu DOM from page.
				# @return {[type]} [description]
				###

				removeContextMenu = ->
					$('.custom-context-menu').remove()
					return

				###*
				# Apply new css class for right positioning.
				# @param  {[type]} cssClass [description]
				# @return {[type]}          [description]
				###

				updateCssClass = (cssClass) ->
					$contextMenuElm.attr 'class', 'custom-context-menu'
					$contextMenuElm.addClass cssClass
					return

				###*
				# [setMenuPosition description]
				# @param {[type]} e       [event arg for finding clicked position]
				# @param {[type]} leftPos [if menu has to be pointed to any pre-fixed element like caret or corner of box.]
				# @param {[type]} topPos  [as above but top]
				###

				setMenuPosition = (e, leftPos, topPos) ->
					contextMenuLeftPos = leftPos or e.pageX
					contextMenuTopPos = topPos - $w.scrollTop() or e.pageY - $w.scrollTop()
					if window.innerWidth - contextMenuLeftPos < contextMenuWidth and window.innerHeight - contextMenuTopPos > contextMenuHeight
						contextMenuLeftPos -= contextMenuWidth
						updateCssClass caretClass.topRight
					else if window.innerWidth - contextMenuLeftPos > contextMenuWidth and window.innerHeight - contextMenuTopPos > contextMenuHeight
						updateCssClass caretClass.topLeft
					else if windowHeight - contextMenuTopPos < contextMenuHeight and windowWidth - contextMenuLeftPos > contextMenuWidth
						contextMenuTopPos -= contextMenuHeight
						updateCssClass caretClass.bottomLeft
					else if windowHeight - contextMenuTopPos < contextMenuHeight and windowWidth - contextMenuLeftPos < contextMenuWidth
						contextMenuTopPos -= contextMenuHeight
						contextMenuLeftPos -= contextMenuWidth
						updateCssClass caretClass.bottomRight
					$contextMenuElm.css(
						left: contextMenuLeftPos
						top: contextMenuTopPos).addClass 'context-caret shown'
					return

				$element.on 'contextmenu.dirContextMenu', (evt) ->

					if $scope[menuItems]? is true and $scope[menuItems].length > 0
						
						evt.stopPropagation()
						evt.preventDefault()
						removeContextMenu()
						createContextMenu()

						###*
						# If pointer node has specified, let the context menu
						# apprear right below to that elem no matter
						# where user clicks within that element.
						###

						if $attr.pointerNode
							$pointer = $(this).find($attr.pointerNode)
							contextMenuLeftPos = $pointer.offset().left + $pointer.outerWidth(true) / 2
							contextMenuTopPos = $pointer.offset().top + $pointer.outerHeight(true)
							setMenuPosition evt, contextMenuLeftPos, contextMenuTopPos
						else
							setMenuPosition evt

						$w.on 'keydown.dirContextMenu', (e) ->
							if e.keyCode == 27
								removeContextMenu()
							return

					return
				#END (on)click.dirContextMenu
				$document.off('click.dirContextMenu').on 'click.dirContextMenu', (e) ->
					if !$(e.target).is('.custom-context-menu') and !$(e.target).parents().is('.custom-context-menu')
						removeContextMenu()
					return
				$w.off('scroll.dirContextMenu').on 'scroll.dirContextMenu', ->
					removeContextMenu()
					return
				$w.on 'resize.dirContextMenu', ->
					windowWidth = window.innerWidth
					windowHeight = window.innerHeight
					removeContextMenu()
					return
				return

		}
]
