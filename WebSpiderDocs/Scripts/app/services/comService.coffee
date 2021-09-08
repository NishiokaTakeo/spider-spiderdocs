app.service 'comService', ($q, $location, $cookies, $http, $interval, _ ) ->
	that = this
	pid_loading = 0
	pid_goto = 0

	nodesBy = (id_parent, db, def) ->
		#var that = this;

		struct = (nodes) ->
			nodes = nodes or []
			nodes.map (n) ->
				{
					Id: n.id
					text: n.document_folder
					children: nodesBy(n.id, db, def)
					state:
						selected: if def > 0 and n.id == def then true else false
						expanded: false					
					ref: n
				}

		def = def or 0
		def = parseInt(def)
		nodes = db.filter((f) ->
			parseInt(f.id_parent) == parseInt(id_parent)
		)
		struct(nodes) or []

	@loading = (show) ->
		show = show != false
		if pid_loading > 0
			clearInterval pid_loading
		if show
			pid_loading = setTimeout((->
				angular.element('.loading').removeClass 'hidden'
				return
			), 1000)
		else
			pid_loading = setTimeout((->
				angular.element('.loading').addClass 'hidden'
				angular.element('.loading .loading-sheet').remove()
				pid_loading = 0
				return
			), 100)
		return

	@getCookieBy = (key = '') ->
		keyvalue = $cookies.get('userCookie').split('&').find((x) ->
			x.startsWith key
		)
		keyvalue.split('=')[1]

	@browser = ->
		# Opera 8.0+
		isOpera = ! !window.opr and ! !opr.addons or ! !window.opera or navigator.userAgent.indexOf(' OPR/') >= 0
		# Firefox 1.0+
		isFirefox = typeof InstallTrigger != 'undefined'
		# Safari 3.0+ "[object HTMLElementConstructor]"
		isSafari = /constructor/i.test(window.HTMLElement) or ((p) ->
			p.toString() == '[object SafariRemoteNotification]'
		)(!window['safari'] or safari.pushNotification)
		# Internet Explorer 6-11
		isIE = false or ! !document.documentMode
		# Edge 20+
		isEdge = !isIE and ! !window.StyleMedia
		# Chrome 1+
		isChrome = ! !window.chrome or ! !window.chrome.webstore or ! !window.chrome.runtime
		{
			opera: isOpera
			firefox: isFirefox
			safari: isSafari
			ie: isIE
			edge: isEdge
			chrome: isChrome
		}

	@nodesBy = (id_parent, db, def) ->
		nodesBy id_parent, db, def
		# def = def || 0;
		# def = parseInt(def)
		# var that = this;
		# function struct(nodes) {
		# 	nodes = nodes || [];
		# 	return nodes.map(function(n) {
		# 		return {
		# 			Id: n.id, text: n.document_folder, children: that.nodesBy(n.id, db, def), state: { selected: def > 0 && n.id === def ? true : false, expanded:false}  }
		# 	});
		# }
		# var nodes = db.filter(function(f) {return parseInt(f.id_parent) === parseInt(id_parent)});
		# return struct(nodes) || [];

	@downloadBy = (url) ->
		$http(
			url: url
			method: 'Get'
			data: {}
			responseType: 'blob').then (response) ->
			file = response.data
			filename = response.config.url.split('/').pop()
			if that.browser().chrome
				if window.URL
					url = window.URL
				else
					url = window.webkitURL
				#var url = (window.URL || window.webkitURL);
				downloadLink = angular.element('<a></a>')
				downloadLink.attr 'href', url.createObjectURL(file)
				downloadLink.attr 'target', '_self'
				downloadLink.attr 'download', filename
				downloadLink[0].click()
			else if that.browser().ie or that.browser().edge
				window.navigator.msSaveOrOpenBlob file, filename
			else
				fileURL = URL.createObjectURL(file)
				window.open fileURL, filename
			return

	@openFolder = (id_default, _folders, target) ->
		selectPath = []

		id_default = parseInt id_default

		makePath = (id, folders, out) ->

			find = (db, id) ->
				db.find((x) ->
					x.id == id
				) or {}

			out = out or []
			folders = folders or []
			if !id
				return
			out.push(id) and makePath(find(folders, id).id_parent, folders, out)

		makePath id_default, _folders, selectPath
		pid_goto = $interval((->
			$target = angular.element(target).find('li[data-folder-id="' + selectPath[selectPath.length - 1] + '"]')
			if selectPath.length > 0 and !$target.html()
				return
			next = selectPath.pop()
			if next
				$target.find('span.leaf').triggerHandler 'click'
			else
				clearInterval pid_goto
			return
		), 10, 100).$$intervalId
		return

	@validateEmail = (email) ->
		re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
		re.test String(email).toLowerCase()

	@extImages = [
			".png"
			".gif"
			".jpg"
			".jpeg"
			".bmp"
			".tif"
		]

	@extPDFs = [
			".pdf"
		]

	@isOCRable = (path) =>
		info = angular.extractPath path

		this.extImages.concat(this.extPDFs).includes( info.extension )

	@extPDFable = [
			".docx"
			".doc"
			".xls"
			".xlsx"
			".xlsm"
			".ppt"
			".pptx"
			".pps"
		]

	@canExportExt = (path) =>
		info = angular.extractPath path

		this.extPDFable.includes( info.extension )

	@sortBy = (data, key, asc) =>

		data = _.sortBy(data,[key])

		data = if not asc then data.reverse() else data

		data

	@noop = (data) ->
		deferred = $q.defer();

		setTimeout( (() -> deferred.resolve(data)), 1 )

		deferred.promise;
	

	@file2Doc = (file) ->
			doc = {}

			angular.copy file, doc
			
			# transfer property values
			doc.id = 0
			
			doc.title = file.name

			doc.extension = if file.name.split('.').length > 1 then file.name.substring(file.name.lastIndexOf('.'), file.name.length) else ''

			doc.id_latest_version = 0

			doc._ref = file
			
			doc

	# @folderNodeToFolder = (node) =>
	# 	folder = 

	return




