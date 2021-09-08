app = angular.module('app', [
	'angularMoment'
	'ngCookies'
	'ngStorage'
	'angular-loading-bar'
	'ui.bootstrap'
	'ngContextMenu'
	'ngSidebarJS'
	'ng.ckeditor'
	'ng.multicombo'
], ($locationProvider, $sceProvider) ->
	$locationProvider.html5Mode true
	#$sceProvider.enabled(false);
	# Completely disable SCE.  For demonstration purposes only!
	# Do not use in new projects or libraries.
	#$sceProvider.enabled(false);
	return
)
app.config [
	'$localStorageProvider'
	($localStorageProvider) ->
		# var serializer = angular.toJson;
		# var deserializer = angular.fromJson;
		dateObjPrefix = 'ngStorage-Type-[object Date]:'

		serializer = (v) ->
			if isDate(v) then dateObjPrefix + v.getTime() else angular.toJson(v)

		deserializer = (v) ->
			if v.startsWith(dateObjPrefix) then parseISOString(v) else angular.fromJson(v)

		isDate = (date) ->
			date and Object::toString.call(date) is '[object Date]' and !isNaN(date)

		parseISOString = (s) ->
			b = s.replace(dateObjPrefix, '').replace('"', '').split(/\D+/)
			new Date(parseInt(b[0]))
			#return (console.log("parse:" + new Date(b[0], b[1] -1, b[2], b[3], b[4], b[5], b[6])), new Date(b[0], b[1] -1, b[2], b[3], b[4], b[5], b[6]));

		$localStorageProvider.setSerializer serializer
		$localStorageProvider.setDeserializer deserializer
		return
]
app.constant('_', window._).run ($rootScope) ->
	$rootScope._ = window._
	return
app.constant('jQuery', window.jQuery).run ($rootScope) ->
	$rootScope.jQuery = window.jQuery
	return
app.run (amMoment) ->
	amMoment.changeLocale 'au'
	return
app.directive 'upload', [
	'$http'
	($http) ->
		{
			restrict: 'E'
			replace: true
			scope: {}
			require: '?ngModel'
			template: '<div class="asset-upload">Drag files here to upload</div>'
			link: (scope, element, attrs, ngModel) ->
				# Code goes here
				return

		}
]
app.directive 'ngConfirmClick', [ ->
	{ link: (scope, element, attr) ->
		msg = attr.ngConfirmClick or 'Are you sure?'
		clickAction = attr.confirmedClick
		element.bind 'click', (event) ->
			if window.confirm(msg)
				scope.$eval clickAction
			return
		return
	}
	]
app.directive 'ngEnter', ->
	(scope, element, attrs) ->
		element.bind 'keydown keypress', (event) ->
			if event.which is 13
				scope.$apply ->
					scope.$eval attrs.ngEnter, 'event': event
					return
				event.preventDefault()
			return
		return
# app.directive('ngContextMenu', ['$document', '$window', function ($document, $window) {
# 	// Runs during compile
# 	return {
# 		restrict: 'A',
# 		link: function ($scope, $element, $attr) {
# 			var contextMenuElm,
# 				$contextMenuElm,
# 				windowWidth = window.innerWidth,
# 				windowHeight = window.innerHeight,
# 				contextMenuWidth,
# 				contextMenuHeight,
# 				contextMenuLeftPos = 0,
# 				contextMenuTopPos = 0,
# 				$w = $($window),
# 				caretClass = {
# 					topRight: 'context-caret-top-right',
# 					topLeft: 'context-caret-top-left',
# 					bottomRight: 'context-caret-bottom-right',
# 					bottomLeft: 'context-caret-bottom-left'
# 				},
# 				menuItems = $attr.menuItems;
# 			function createContextMenu() {
# 				var fragment = document.createDocumentFragment();
# 				contextMenuElm = document.createElement('ul'),
# 					$contextMenuElm = $(contextMenuElm);
# 				contextMenuElm.setAttribute('id', 'context-menu');
# 				contextMenuElm.setAttribute('class', 'custom-context-menu');
# 				mountContextMenu($scope[menuItems], fragment);
# 				contextMenuElm.appendChild(fragment);
# 				document.body.appendChild(contextMenuElm);
# 				contextMenuWidth = $contextMenuElm.outerWidth(true);
# 				contextMenuHeight = $contextMenuElm.outerHeight(true);
# 			}
# 			function mountContextMenu(menuItems, fragment) {
# 				menuItems.forEach(function (_item) {
# 					var li = document.createElement('li');
# 					li.innerHTML = '<a>' + _item.label + ' <span class="right-caret"></span></a>';
# 					if (_item.action && _item.active) {
# 						li.addEventListener('click', function () {
# 							if (typeof $scope[_item.action] isnt 'function') return false;
# 							$scope[_item.action]($attr, $scope);
# 						}, false);
# 					}
# 					if (_item.divider) {
# 						addContextMenuDivider(fragment);
# 					}
# 					if (!_item.active) li.setAttribute('class', 'disabled');
# 					if (_item.subItems) {
# 						addSubmenuItems(_item.subItems, li, _item.active)
# 					}
# 					fragment.appendChild(li);
# 				});
# 			}
# 			function addSubmenuItems(subItems, parentLi, parentIsActive) {
# 				parentLi.setAttribute('class', 'dropdown-submenu')
# 				if (!parentIsActive) parentLi.setAttribute('class', 'disabled')
# 				var ul = document.createElement('ul')
# 				ul.setAttribute('class', 'dropdown-menu')
# 				mountContextMenu(subItems, ul)
# 				parentLi.appendChild(ul)
# 			}
# 			function addContextMenuDivider(fragment) {
# 				var divider = document.createElement('li');
# 				divider.className = 'divider'
# 				fragment.appendChild(divider);
# 			}
# 			/**
# 			 * Removing context menu DOM from page.
# 			 * @return {[type]} [description]
# 			 */
# 			function removeContextMenu() {
# 				$('.custom-context-menu').remove();
# 			}
# 			/**
# 			 * Apply new css class for right positioning.
# 			 * @param  {[type]} cssClass [description]
# 			 * @return {[type]}          [description]
# 			 */
# 			function updateCssClass(cssClass) {
# 				$contextMenuElm.attr('class', 'custom-context-menu');
# 				$contextMenuElm.addClass(cssClass);
# 			}
# 			/**
# 			 * [setMenuPosition description]
# 			 * @param {[type]} e       [event arg for finding clicked position]
# 			 * @param {[type]} leftPos [if menu has to be pointed to any pre-fixed element like caret or corner of box.]
# 			 * @param {[type]} topPos  [as above but top]
# 			 */
# 			function setMenuPosition(e, leftPos, topPos) {
# 				contextMenuLeftPos = leftPos || e.pageX;
# 				contextMenuTopPos = topPos - $w.scrollTop() || e.pageY - $w.scrollTop();
# 				if (window.innerWidth - contextMenuLeftPos < contextMenuWidth && window.innerHeight - contextMenuTopPos > contextMenuHeight) {
# 					contextMenuLeftPos -= contextMenuWidth;
# 					updateCssClass(caretClass.topRight);
# 				} else if (window.innerWidth - contextMenuLeftPos > contextMenuWidth && window.innerHeight - contextMenuTopPos > contextMenuHeight) {
# 					updateCssClass(caretClass.topLeft);
# 				} else if (windowHeight - contextMenuTopPos < contextMenuHeight && windowWidth - contextMenuLeftPos > contextMenuWidth) {
# 					contextMenuTopPos -= contextMenuHeight;
# 					updateCssClass(caretClass.bottomLeft);
# 				} else if (windowHeight - contextMenuTopPos < contextMenuHeight && windowWidth - contextMenuLeftPos < contextMenuWidth) {
# 					contextMenuTopPos -= contextMenuHeight;
# 					contextMenuLeftPos -= contextMenuWidth;
# 					updateCssClass(caretClass.bottomRight);
# 				}
# 				$contextMenuElm.css({
# 					left: contextMenuLeftPos,
# 					top: contextMenuTopPos
# 				}).addClass('context-caret shown');
# 			}
# 			/**
# 			 * CONTEXT MENU
# 			 * @param  {[type]} evt [description]
# 			 * @return {[type]}     [description]
# 			 */
# 			$element.on('contextmenu.dirContextMenu', function (evt) {
# 				evt.preventDefault();
# 				removeContextMenu();
# 				createContextMenu();
# 				/**
# 				 * If pointer node has specified, let the context menu
# 				 * apprear right below to that elem no matter
# 				 * where user clicks within that element.
# 				 */
# 				if ($attr.pointerNode) {
# 					var $pointer = $(this).find($attr.pointerNode);
# 					contextMenuLeftPos = $pointer.offset().left + ($pointer.outerWidth(true) / 2);
# 					contextMenuTopPos = $pointer.offset().top + $pointer.outerHeight(true);
# 					setMenuPosition(evt, contextMenuLeftPos, contextMenuTopPos);
# 				} else {
# 					setMenuPosition(evt);
# 				}
# 				$w.on('keydown.dirContextMenu', function (e) {
# 					if (e.keyCode is 27) {
# 						removeContextMenu();
# 					}
# 				})
# 			}); //END (on)click.dirContextMenu
# 			$document.off('click.dirContextMenu').on('click.dirContextMenu', function (e) {
# 				if (!$(e.target).is('.custom-context-menu') && !$(e.target).parents().is('.custom-context-menu')) {
# 					removeContextMenu();
# 				}
# 			});
# 			$w.off('scroll.dirContextMenu').on('scroll.dirContextMenu', function () {
# 				removeContextMenu();
# 			});
# 			$w.on('resize.dirContextMenu', function () {
# 				windowWidth = window.innerWidth;
# 				windowHeight = window.innerHeight;
# 				removeContextMenu();
# 			});
# 		}
# 	}
# }]);
app.filter 'toDate', (moment) ->
	(input) ->
		input = input or ''
		if moment(input) > moment('2000-01-01', 'YYYY-MM-DD', true) then moment(input).toDate() else null
app.filter 'dispNA', ->
	(input) ->
		input = input or ''
		if typeof input is 'object'
			for prop of input
				if ad.hasOwnProperty(prop)
					return input
		else if input isnt''
			return input
		'N/A'
app.filter 'dispNil', ->
	(input) ->
		input = input or ''
		if input is '' or input is undefined or input is null
			return null
		if typeof input is 'string' and (input.indexOf('1975-01-01') > -1 or input.indexOf('157737600000') > -1)
			return null
		if typeof input is 'object'
			return if moment(input) > moment('2000-01-01', 'YYYY-MM-DD', true) then input else null
		else if input isnt''
			return input
		''
app.filter 'strReplace', ->
	(input, from, to) ->
		input = input or ''
		from = from or ''
		to = to or ''
		input.replace new RegExp(from, 'g'), to
app.filter 'range', ->
	(input, total) ->
		total = parseInt(total)
		i = 0
		while i < total
			input.push i
			i++
		input

app.hex_to_ascii = (str1) ->
	hex = str1.toString()
	str = ''
	n = 0
	while n < hex.length
		str += String.fromCharCode(parseInt(hex.substr(n, 2), 16))
		n += 2
	str

###
    Custom functions
###

angular.isEmpty = (val) ->
	angular.isUndefined(val) or val is null or val is ''

angular.isDateValid = (date) ->
	if date is undefined or date is null
		return false
	if !moment(date).isValid()
		return
	if moment(date) > moment('2000-01-01', 'YYYY-MM-DD', true)
		return true
	false

angular.extractPath = (path) ->
	ext = if path.split('.').length > 1 then path.substring(path.lastIndexOf('.'), path.length) else ''
	withoutExt = path.split('.').slice(0, -1).join('.')
	{
		org: path
		nameWithoutExt: withoutExt
		extension: ext
	}

# First, checks if it isn't implemented yet.
if !String::format

	String::format = ->
		args = arguments
		@replace /{(\d+)}/g, (match, number) ->
			if typeof args[number] isnt'undefined' then args[number] else match
