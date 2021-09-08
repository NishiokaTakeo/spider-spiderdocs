app.service('spiderdocsService', function ($http,$q ,_ , $sessionStorage,en_Actions)
{
	var jsonHeader =  { contentType: "application/json; charset=utf-8" ,dataType: "json"};

	var Q=$q;
	var userconf = SpiderDocsConf;
	var that = this;

	//MD5 function
	var md5 = function (n) {
		function r(n, r) {
			var t = n[0],
				f = n[1],
				i = n[2],
				a = n[3];
			t = u(t, f, i, a, r[0], 7, -680876936), a = u(a, t, f, i, r[1], 12, -389564586), i = u(i, a, t, f, r[2], 17, 606105819), f = u(f, i, a, t, r[3], 22, -1044525330), t = u(t, f, i, a, r[4], 7, -176418897), a = u(a, t, f, i, r[5], 12, 1200080426), i = u(i, a, t, f, r[6], 17, -1473231341), f = u(f, i, a, t, r[7], 22, -45705983), t = u(t, f, i, a, r[8], 7, 1770035416), a = u(a, t, f, i, r[9], 12, -1958414417), i = u(i, a, t, f, r[10], 17, -42063), f = u(f, i, a, t, r[11], 22, -1990404162), t = u(t, f, i, a, r[12], 7, 1804603682), a = u(a, t, f, i, r[13], 12, -40341101), i = u(i, a, t, f, r[14], 17, -1502002290), f = u(f, i, a, t, r[15], 22, 1236535329), t = o(t, f, i, a, r[1], 5, -165796510), a = o(a, t, f, i, r[6], 9, -1069501632), i = o(i, a, t, f, r[11], 14, 643717713), f = o(f, i, a, t, r[0], 20, -373897302), t = o(t, f, i, a, r[5], 5, -701558691), a = o(a, t, f, i, r[10], 9, 38016083), i = o(i, a, t, f, r[15], 14, -660478335), f = o(f, i, a, t, r[4], 20, -405537848), t = o(t, f, i, a, r[9], 5, 568446438), a = o(a, t, f, i, r[14], 9, -1019803690), i = o(i, a, t, f, r[3], 14, -187363961), f = o(f, i, a, t, r[8], 20, 1163531501), t = o(t, f, i, a, r[13], 5, -1444681467), a = o(a, t, f, i, r[2], 9, -51403784), i = o(i, a, t, f, r[7], 14, 1735328473), f = o(f, i, a, t, r[12], 20, -1926607734), t = e(t, f, i, a, r[5], 4, -378558), a = e(a, t, f, i, r[8], 11, -2022574463), i = e(i, a, t, f, r[11], 16, 1839030562), f = e(f, i, a, t, r[14], 23, -35309556), t = e(t, f, i, a, r[1], 4, -1530992060), a = e(a, t, f, i, r[4], 11, 1272893353), i = e(i, a, t, f, r[7], 16, -155497632), f = e(f, i, a, t, r[10], 23, -1094730640), t = e(t, f, i, a, r[13], 4, 681279174), a = e(a, t, f, i, r[0], 11, -358537222), i = e(i, a, t, f, r[3], 16, -722521979), f = e(f, i, a, t, r[6], 23, 76029189), t = e(t, f, i, a, r[9], 4, -640364487), a = e(a, t, f, i, r[12], 11, -421815835), i = e(i, a, t, f, r[15], 16, 530742520), f = e(f, i, a, t, r[2], 23, -995338651), t = c(t, f, i, a, r[0], 6, -198630844), a = c(a, t, f, i, r[7], 10, 1126891415), i = c(i, a, t, f, r[14], 15, -1416354905), f = c(f, i, a, t, r[5], 21, -57434055), t = c(t, f, i, a, r[12], 6, 1700485571), a = c(a, t, f, i, r[3], 10, -1894986606), i = c(i, a, t, f, r[10], 15, -1051523), f = c(f, i, a, t, r[1], 21, -2054922799), t = c(t, f, i, a, r[8], 6, 1873313359), a = c(a, t, f, i, r[15], 10, -30611744), i = c(i, a, t, f, r[6], 15, -1560198380), f = c(f, i, a, t, r[13], 21, 1309151649), t = c(t, f, i, a, r[4], 6, -145523070), a = c(a, t, f, i, r[11], 10, -1120210379), i = c(i, a, t, f, r[2], 15, 718787259), f = c(f, i, a, t, r[9], 21, -343485551), n[0] = v(t, n[0]), n[1] = v(f, n[1]), n[2] = v(i, n[2]), n[3] = v(a, n[3])
		}

		function t(n, r, t, u, o, e) {
			return r = v(v(r, n), v(u, e)), v(r << o | r >>> 32 - o, t)
		}

		function u(n, r, u, o, e, c, f) {
			return t(r & u | ~r & o, n, r, e, c, f)
		}

		function o(n, r, u, o, e, c, f) {
			return t(r & o | u & ~o, n, r, e, c, f)
		}

		function e(n, r, u, o, e, c, f) {
			return t(r ^ u ^ o, n, r, e, c, f)
		}

		function c(n, r, u, o, e, c, f) {
			return t(u ^ (r | ~o), n, r, e, c, f)
		}

		function f(n) {
			var t, u = n.length,
				o = [1732584193, -271733879, -1732584194, 271733878];
			for (t = 64; t <= n.length; t += 64) r(o, i(n.substring(t - 64, t)));
			n = n.substring(t - 64);
			var e = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
			for (t = 0; t < n.length; t++) e[t >> 2] |= n.charCodeAt(t) << (t % 4 << 3);
			if (e[t >> 2] |= 128 << (t % 4 << 3), t > 55)
				for (r(o, e), t = 0; 16 > t; t++) e[t] = 0;
			return e[14] = 8 * u, r(o, e), o
		}

		function i(n) {
			var r, t = [];
			for (r = 0; 64 > r; r += 4) t[r >> 2] = n.charCodeAt(r) + (n.charCodeAt(r + 1) << 8) + (n.charCodeAt(r + 2) << 16) + (n.charCodeAt(r + 3) << 24);
			return t
		}

		function a(n) {
			for (var r = "", t = 0; 4 > t; t++) r += l[n >> 8 * t + 4 & 15] + l[n >> 8 * t & 15];
			return r
		}

		function h(n) {
			for (var r = 0; r < n.length; r++) n[r] = a(n[r]);
			return n.join("")
		}

		function d(n) {
			return h(f(n))
		}

		function v(n, r) {
			return n + r & 4294967295
		}

		function v(n, r) {
			var t = (65535 & n) + (65535 & r),
				u = (n >> 16) + (r >> 16) + (t >> 16);
			return u << 16 | 65535 & t
		}
		var l = "0123456789abcdef".split("");
		return "5d41402abc4b2a76b9719d911017c592" != d("hello"), d
	}();

	//var app = {};
	var _http = function () { };
	var qReady = Q.defer();

	this._cache = {};
	this._iconf = {};
	this.LoginedUser = {
		id: 0,
	};

	if (!userconf) {
		alert('need to pass authentication information');
	}

	this._iconf = userconf;
	// pretend as logined user if no use authentication.
	if (this._iconf.DisableAuth()) {
		this.LoginedUser.id = this._iconf.ID();

		qReady.resolve({});
		//return;
	}


	this.fcall = function (callback = function(){}) {

		var defer = $q.defer();
		//do some work async and on a callback
		setTimeout(function() {
			var data = callback() || undefined;

			defer.resolve(data);

		}, 10);

		return defer.promise;
	}

	this.cached = function () {
		return this.fcall(function () { });

		// if (this._iconf.DisableAuth()) {
		// 	return this.fcall(function () { });
		// } else {
		// 	return $.extend(true, {}, this._cache);
		// }
		//return $.extend(true,{},this._cache);
	}

	this._requestPath = function (path)
	{
		return this._iconf.GetServerURL() + "api/External/{0}".replace("{0}", path);
	}

	this.http = _http = function (url, object, header) {
		header = header || {};

		if (!Q) console.error("jsgrid_Custom.js is depend on Q. See mroe information https://github.com/kriskowal/q");

		var
			deferred = Q.defer(),

			params = that.Fn.toDatabase(object),

			json_string = JSON.stringify(params);

		var def = {
			type: "POST",
			//url: (new RegExp(/^https?:\/\//).test(url)) ? url : url_header + url,
			url: (new RegExp(/^\//).test(url)) ? CONST.origin + url : url,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: json_string,
			success: function (result) {

				if (this.dataType == "blob" || this.dataType == "arraybuffer") return deferred.resolve(result);

				var json = result.d || result;
				json = that.Fn.isJson(json) ? json.toString() : JSON.stringify(json);

				if (json) {
					try {
						var formated = that.Fn.toDatabase((typeof json === 'string' ? JSON.parse(json) : json));
						deferred.resolve(formated);
						//deferred.resolve((typeof json == 'string' ? JSON.parse(json) : json))
					} catch (e) {
						deferred.resolve(json)
					}
				} else {
					deferred.resolve({});
				}

			},
			error: function (xhr, status, error) {
				deferred.reject(error.message);
			}
		};

		header = $.extend({}, def, header)

		$.ajax(header);

		return deferred.promise;
	}

	this.get = _get = function (url, object) {

		var keys = Object.keys(object);

		if( keys.length > 0 ) { debugger; }

		var query = keys.length > 0 ?  keys.map( function(k) { return  k+'='+keys[k]; } ).join('&') : '';

		return _http(url + '?' + query, {}, {
			method: 'GET',dataType:undefined,type:'GET'
		});
	}

	//---------------------------------------------------------------------------------
	// User authentication functions.
	//---------------------------------------------------------------------------------
	// this._login = function _login() {
	// 	if (!this._iconf.LoginName() || !this._iconf.LoginPassword()) {
	// 		return;
	// 	}

	// 	var that = this;

	// 	return this._cache = _http(this._iconf.GetServerURL() + '/Users/Login', {
	// 		UserName: this._iconf.LoginID(),
	// 		Password_Md5: md5(this._iconf.LoginPassword())
	// 	})

	// 		.then(function (context) {
	// 			that.LoginedUser = context.User;
	// 			qReady.resolve({});
	// 			return context && context.User && context.User.id > 0;
	// 		});

	// }

	this.ready = function(fnc = function() { }) {

		qReady.promise.then(function() {
			fnc();
		});
	}

	this.GetSettings = function ()
	{

		var that = this;
		return this.cached().then(function () {

			return _get(that._requestPath('GetSettings'), {})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetMyProfileAsync = function()
	{
		var that = this;
		return this.cached().then(function () {

			return _get(that._requestPath('GetMyProfileAsync'), {})

				.then(function (Result) {
					return Result;
				});
		});
	}


	//---------------------------------------------------------------------------------
	// This function searches documents as per Criteria and returns document information.
	//---------------------------------------------------------------------------------
	this.GetDocuments = function (criteria) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetDocuments'), criteria)

				.then(function (Result) {
					return !Result ? [] : (Result.Documents || Result);
				});
		});
	}

	this.GetRecentDocuments = function () {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetRecentDocuments'), {})

				.then(function (Result) {
					return !Result ? [] : ( Result.Documents || Result);
				});
		});
	}

	this.GetUserWorkspaceDocuments = function () {

		//var that = this;
		return that.cached().then(function () {

			return _http(that._requestPath('GetUserWorkspaceDocuments') )

				.then(function (Result) {
					return (Result.Documents || Result || []);
				});
		});
	}
	this.GetPermissionLevels = function () {

		//var that = this;
		return that.cached().then(function () {

			return _http(that._requestPath('GetPermissionLevels') )

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveUserAsync = function (user) {

		//var that = this;
		return that.cached().then(function () {

			return _http(that._requestPath('SaveUserAsync'), { user: user } )

				.then(function (Result) {
					return Result;
				});
		});
	}


	/**
	 * This function returns URLs of requested documents.
	 * Users can download the documents from the URLs.
	 * @param {int array} versionIds - version ids which you want to get download-url
	 * @return {Promise} .
	 */
	this.GetDonloadUrls = function (versionIds) {
		var criteria = {
			'VersionIds': versionIds
		};
		// api mode

		criteria = Array.isArray(versionIds) ? versionIds : [versionIds];

		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('Export'), criteria)

					.then(function (Result) {
						return (Result.Urls || Result || []);
						//return !Result.Urls ? [] : (Result.Urls || Result);
					});
			});
	}


	this.GetDonloadUrls4UserWorkSpace = function (ids) {

		// api mode

		var criteria = Array.isArray(ids) ? ids : [ids];

		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('ExportUserWorkspace'), criteria)

					.then(function (Result) {
						return (Result.Urls || Result || []);
						//return !Result.Urls ? [] : (Result.Urls || Result);
					});
			});
	}

	this.AddGroupOrUserToFolder = function (to, idFolder, idGroupOrUser)
	{
		var args = { idFolder: idFolder,GroupId:0, UserId:0 };
		if(to === 'group')
		{
			args.GroupId = idGroupOrUser;
		}
		else if (to === 'user')
		{
			args.UserId = idGroupOrUser;
		}

		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('AddGroupOrUserToFolder'), args)

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.AddPermissionToGroup = function (idFolder, id, permissions)
	{
		var args = { idFolder: idFolder, GroupId: id, Permissions: permissions };

		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('AddPermission'), args)

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.AddPermissionToUser = function (idFolder, id, permissions)
	{
		var args = { idFolder: idFolder, UserId:id, Permissions: permissions  };

		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('AddPermission'), args)

					.then(function (Result) {
						return Result;
					});
			});
	}


	this.DeletePermissionAsync = function (to, idFolder, idGroupOrUser)
	{
		var args = { idFolder: idFolder,GroupId:0, UserId:0 };
		if(to === 'group')
		{
			args.GroupId = idGroupOrUser;
		}
		else if (to === 'user')
		{
			args.UserId = idGroupOrUser;
		}

		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('DeletePermissionAsync'), args)

					.then(function (Result) {
						return Result;
					});
			});
	}


	this.GetFolderPermissionTitlesAsync = function ()
	{
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetFolderPermissionTitlesAsync'), {})

					.then(function (Result) {
						return Result;
					});
			});
	}


	this.GetPermissionsByGroupAndUser = function (to, idFolder, idGroupOrUser)
	{
		var args = { idFolder: idFolder,GroupId:0, UserId:0 };
		if(to === 'group')
		{
			args.GroupId = idGroupOrUser;
		}
		else if (to === 'user')
		{
			args.UserId = idGroupOrUser;
		}

		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetPermissionsByGroupAndUser'), args)

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.GetInheritedFolderName = function (idFolder)
	{
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetInheritedFolderName'), {idFolder:idFolder})

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.GetInheritanceFolder = function (idFolder)
	{
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetInheritanceFolder'), {idFolder:idFolder})

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.ToggleInheritance = function (idFolder)
	{
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('ToggleInheritance'), {idFolder:idFolder})

					.then(function (Result) {
						return Result;
					});
			});
	}



	this.ExportAsPDF = function (versionIds) {
		var criteria = {
			'VersionIds': versionIds
		};
		// api mode

		criteria = Array.isArray(versionIds) ? versionIds : [versionIds];

		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('ExportAsPDF'), criteria)

					.then(function (Result) {
						return (Result.Urls || Result || []);
						//return !Result.Urls ? [] : (Result.Urls || Result);
					});
			});
	}



	/**
	 * Merge PDF from more than one document
	 * @param {Array: int} version id. *
	 * @param {Object: Document} Document Class.
	 * @return {Promise} .
	 */
	this.ToPdf = function (versionIds, document) {
		var that = this;

		var data = {

			VersionIds: versionIds,

			Document: document,

			//TextCombo: combo

		};

		return this.cached()

			.then(function () {

				return _http(that._requestPath('ToPdf'), data)

					.then(function (Result) {
						return Result;
					});
			});
	}

	//---------------------------------------------------------------------------------
	// This function searches documents as per Criteria and returns document information.
	//---------------------------------------------------------------------------------
	this.GetFolders = function (ids) {
		ids = ids || [];

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetFolders'), { FolderIDs: ids})

				.then(function (Result) {
					return !Result ? [] : (Result.Folders || Result);
				});
		});
	}

	this.GetFoldersL1 = function (idParent=0, permission= en_Actions.OpenRead) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetFoldersL1'), { idParent:idParent, Permission:permission })

				.then(function (Result) {
					return (Result.Folders||Result);
				});
		});
	}

	this.GetFoldersL1Async = function (idParent=0, permission= en_Actions.OpenRead) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetFoldersL1Async'), { idParent:idParent, Permission:permission })

				.then(function (Result) {
					return (Result.Folders||Result);
				});
		});
	}

	this.SearchFoldersAsync = function (folderIds) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('SearchFoldersAsync'), { FolderIds: folderIds })

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetGroupsByAsync = function (args) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetGroupsByAsync'), args)

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetGroupsAsync = function () {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetGroupsAsync'), { })

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetUserIdInGroupAsync = function (id_group) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetUserIdInGroupAsync'), { GroupId: id_group})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.DeleteUserGroupAsync = function (id_group,id_user) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('DeleteUserGroupAsync'), { GroupId: id_group, UserId: id_user})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.AssignGroupAsync = function (id_group,id_user) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('AssignGroupAsync'), { GroupId: id_group, UserId: id_user})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveGroupAsync = function (group) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('SaveGroupAsync'), { Group: group})

				.then(function (Result) {
					return Result;
				});
		});
	}



	this.DrillUpFoldersWithParentsFromAsync = function (idParent=0, permission = en_Actions.None) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('DrillUpFoldersWithParentsFromAsync'), { idParent:idParent, Permission:permission })

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetFolderPermissionsAsync = function (idFolder=0,id_user=0) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetFolderPermissionsAsync'), { idFolder:idFolder,UserId: id_user })

				.then(function (Result) {
					return Result;
				});
		});
	}


	//---------------------------------------------------------------------------------
	// This function searches documents as per Criteria and returns document information.
	//---------------------------------------------------------------------------------
	this.SaveFolder = function (folder) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('SaveFolder'), folder)

				.then(function (Result) {
					return !Result ? [] : (Result.Folder || Result);
				});
		});
	}

	this.RemoveFolderAsync = function (id_folder) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('RemoveFolderAsync'), {idFolder: id_folder})

				.then(function (Result) {
					return Result;
				});
		});
	}

	//---------------------------------------------------------------------------------
	// This function searches documents as per Criteria and returns document information.
	//---------------------------------------------------------------------------------
	this.GetDocumentTypes = function () {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetDocumentTypes'))

				.then(function (Result) {
					return !Result ? [] : (Result.DocumentTypes||Result);
				});
		});
	}

	this.SaveDocumentTypeAsync = function (id,name) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('SaveDocumentTypeAsync'),{DocumentType:{id:id, type:name}})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.RemoveDocumentTypeAsync = function (id) {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('RemoveDocumentTypeAsync'),{id:id})

				.then(function (Result) {
					return Result;
				});
		});
	}

	//---------------------------------------------------------------------------------
	// This function searches documents as per Criteria and returns document information.
	//---------------------------------------------------------------------------------
	this.GetAttributes = function () {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetAttributes'))

				.then(function (Result) {
					return !Result ? [] : (Result.Attributes||Result);
				});
		});
	}

	this.GetAttributeFieldTypesAsync = function () {

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetAttributeFieldTypesAsync'))

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetAttributeDocType = function (id_doc_types) {
		id_doc_types = id_doc_types || [];
		if( typeof id_doc_types == "string") {debugger}
		if (typeof id_doc_types == "string" || !Array.isArray(id_doc_types)) id_doc_types = [id_doc_types];

		var query = id_doc_types.length > 0 ? ("?DocTypeIds=" + id_doc_types.join('&DocTypeIds=')) : '';

		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetAttributeDocType') + query, {}, {
				method: 'GET'
			})

				.then(function (Result) {
					return !Result ? [] : Result;
				});
		});
	}

	//---------------------------------------------------------------------------------
	// This function returns combobox items for specified attribute id.
	//---------------------------------------------------------------------------------
	this.GetAttributeComboboxItems = function (id_attr) {
		var params = {
			AttributeId: id_attr,
			ItemId: 0
		};

		var that = this;
		return this.cached().then(function () {

			// return _http(that._requestPath('GetAttributeComboboxItems') + `/${0}/${params.AttributeId}/${params.ItemId}`, params)
			return _http(that._requestPath('GetAttributeComboboxItems') + '/0/'+ params.AttributeId + '/' + params.ItemId, params)

				.then(function (Result) {
					return !Result ? [] : (Result.ComboboxItems||Result);
				});
		});
	}

	/**
	 * Get User information.
	 * @param {string} username - name to login
	 * @param {string} password - password with md5 to login
	 * @return {Promise} .
	 */
	this.GetUserByLoginPasswordAsync = function (username, password) {
		var criteria = {
			'LoginName': username,
			'Password': password
		};
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetUserByLoginPasswordAsync'), criteria)

					.then(function (Result) {
						return Result;
					});
			});
	}



	/**
	 * Update User information.
	 * @param {Object: User} User Class to update
	 * @param {string} username - name to login
	 * @param {string} password - password with md5 to login
	 * @return {Promise} .
	 */
	this.UpdateUser = function (user, username, password) {
		var criteria = {
			'User': user,
			'UserName': username,
			'Password': password
		};
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('UpdateUser'), criteria)

					.then(function (Result) {
						return Result;
					});
			});
	}

	/**
	 * Get History information.
	 * @param {Object: SearchCriteria} SearchCriteria Class
	 * @return {Promise} .
	 */
	this.GetHistories = function (criteria) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetHistories'), criteria)

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.GetDetails = function (criteria) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetDetails'), criteria)

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.SaveAttributeAsync = function (attribute) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('SaveAttributeAsync'), {Attribute: attribute})

					.then(function (Result) {
						return Result;
					});
			});
	}

	//---------------------------------------------------------------------------------
	// Save a document which corresponds to given temp id.
	//---------------------------------------------------------------------------------
	this.Import = function (tempid, document) {

		var that = this;

		var data = {
			// give TempId always
			TempId: tempid,

			// to add new document, you need to give the following parameters to Document object.
			Document: document
		};

		return this.cached().then(function () {

			return _http(that._requestPath('Import'), data)

				.then(function (msg) {
					return msg;
				});
		});

	}

	//---------------------------------------------------------------------------------
	// Save a document which corresponds to given temp id.
	//---------------------------------------------------------------------------------
	this.SaveDoc = function (tempid, document) {

		var that = this;

		var data = {
			// give TempId always
			TempId: tempid,

			// to add new document, you need to give the following parameters to Document object.
			Document: document
		};

		return this.cached().then(function () {

			return _http(that._requestPath('SaveDoc'), data)

				.then(function (doc) {
					return doc;
				});
		});

	}
	
	this.ImportDbAsNew = function (tempid, document, options) {

		var that = this;

		var data = {
			// give TempId always
			TempId: tempid,

			// to add new document, you need to give the following parameters to Document object.
			Document: document,

			Options: options
		};

		return this.cached().then(function () {

			return _http(that._requestPath('ImportDbAsNew'), data)

				.then(function (doc) {
					return doc;
				});
		});

	}

	/**
	 * Upload File and Import at one method
	 * @param {Object: Document} Document Class.
	 * Be aware, combo item children is saved only this method. Import method doesn't save
	 * @param {Object: Byte[] or Blob} actual file.
	 * @return {Promise} .
	 */
	this.RemoteImport = function (url, document, combo) {
		var that = this;

		var data = {
			// give TempId always
			Url: url,

			// to add new document, you need to give the following parameters to Document object.
			Document: document,

			TextCombo: combo

		};

		return this.cached().then(function () {

			return _http(that._requestPath('RemoteImport'), data)

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.Upload = function(file)
	{
		var that = this;

		var formData = new FormData();
		formData.append("file1", file);

		var data = {
						cache : false,

						contentType : false,

						processData : false,

						data: formData
					};

		return this.cached().then(function () {

			return _http(that._requestPath('UploadFile'), {}, data)

				.then(function (TempId) {
					return TempId;
				});
		});
	}

	/*
	* Save file in Database as new ver.
	*/
	this.UploadFileToTemp = function(file, tempId)
	{
		var that = this;

		var formData = new FormData();
		formData.append("file1", file);
		formData.append("tempId", tempId);

		var data = {
						cache : false,

						contentType : false,

						processData : false,
						// contentType: "text/plain",
						// dataType: undefined,
						data: formData
					};

		return this.cached().then(function () {

			return _http(that._requestPath('UploadFileToTemp'), {}, data)

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveFile2TempAsync = function(id, idVersion)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('SaveFile2TempAsync'), {DocumentId: id, VersionId: idVersion})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveWorkSpaceFile2TempAsync = function(id)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('SaveWorkSpaceFile2TempAsync'), {Id: id})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveDMS2TempAsync = function(id)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('SaveDMS2TempAsync'), {DocumentId: id})

				.then(function (Result) {
					return Result;
				});
		});
	}


	this.SaveFileToUserWorkSpace = function(file)
	{
		var that = this;

		var formData = new FormData();
		formData.append("file1", file);

		var data = {
						cache : false,

						contentType : false,

						processData : false,

						data: formData
					};

		return this.cached().then(function () {

			return _http(that._requestPath('SaveFileToUserWorkSpace'), {}, data)

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveWorkSpaceFileToDbAsNewAsync= function(id, document, options)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('SaveWorkSpaceFileToDbAsNewAsync'), {Id: id, Document: document, Options: options})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveWorkSpaceFileToDbAsVerAsync= function(id, document,reason, options)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('SaveWorkSpaceFileToDbAsVerAsync'), {Id: id, Document: document, Reason:reason, Options: options})

				.then(function (Result) {
					return Result;
				});
		});
	}



	this.RenameUserWorkSpaceFile= function(id, newname)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('RenameUserWorkSpaceFile'), {Id: id, NewName: newname})

				.then(function (Result) {
					return Result;
				});
		});


	}

	this.RenameUserWorkSpaceFileAsync= function(id, newname)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('RenameUserWorkSpaceFileAsync'), {Id: id, NewName: newname})

				.then(function (Result) {
					return Result;
				});
		});
	}


	this.RenameDbFileAsync= function(id, newname)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('RenameDbFileAsync'), {DocumentId: id, NewName: newname})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetCustomViewSources = function()
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('GetCustomViewSourcesAsync'), {})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveCustomViewSourceAsync = function(source)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('SaveCustomViewSourceAsync'), {CustomViewSource: source})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetCustomViewDocumentsAsync = function(id_doc)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('GetCustomViewDocumentsAsync'), {DocumentId:id_doc})

				.then(function (Result) {
					return Result;
				});
		});
	}
	this.DeleteAllCustomViewDocumentAsync = function(id_doc)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('DeleteAllCustomViewDocumentAsync'), {DocumentId:id_doc})

				.then(function (Result) {
					return Result;
				});
		});
	}
	this.GetCustomViewsAsync = function()
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('GetCustomViewsAsync'), {})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SaveCustomView = function(args)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('SaveCustomViewAsync'), {CustomView: args })

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.DeleteCustomView = function(args)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('DeleteCustomViewAsync'), {CustomView: args })

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.DeleteCustomViewSourceAsync = function(args)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('DeleteCustomViewSourceAsync'), {CustomViewSource: args })

				.then(function (Result) {
					return Result;
				});
		});
	}


	/*
	* Save file in Database as new ver.
	*/
	this.SaveAsNewVer = function(docId, file, resaon)
	{
		var that = this;

		var formData = new FormData();
		formData.append("file1", file);
		formData.append("DocumentId", docId);
		formData.append("reason", resaon);

		var data = {
						cache : false,

						contentType : false,

						processData : false,
						// contentType: "text/plain",
						// dataType: undefined,
						data: formData
					};

		return this.cached().then(function () {

			return _http(that._requestPath('SaveAsNewVer'), {}, data)

				.then(function (Result) {
					return Result;
				});
		});
	}

	/*
		Check in a file as new ver for checked out document.
	*/
	this.CheckInAsNewVerAsync = function(id, reason)
	{
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('CheckInAsNewVerAsync'), {Id:id,Reason:reason})

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.ImportUserWorkSpaceFile = function(id, file)
	{
		var that = this;

		var formData = new FormData();
		formData.append("file1", file);
		formData.append("linkId", id);
		// formData.append("reason", resaon);

		var data = {
						cache : false,

						contentType : false,

						processData : false,
						// contentType: "text/plain",
						// dataType: undefined,
						data: formData
					};

		return this.cached().then(function () {

			return _http(that._requestPath('ImportUserWorkSpaceFile'), {}, data)

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.UpdateProperty = function (document) {

		var that = this;

		var data = {
			// to add new document, you need to give the following parameters to Document object.
			Document: document
		};

		return this.cached().then(function () {

			return _http(that._requestPath('UpdateProperty'), document)

				.then(function (Result) {
					return typeof Result === 'string' ? Result : '';
				});
		});

	}


	this.SendEmailAsync = function (tempId, emailForm ) {

		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('SendEmailAsync'), {TempId: tempId, EmailForm: emailForm})

				.then(function (Result) {
					return typeof Result === 'string' ? Result : '';
				});
		});

	}


	/**
	 * Check out documents.
	 * The ordered FolderIds and DocIds must be paired.
	 * @param {int[]: FolderIds} ids of folders.
	 * @param {int[]: DocIds} id of documents.
	 * @return {Promise} .
	 */
	 this.CheckOut = function (docIds) {
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('CheckOut'), Array.isArray(docIds) ? docIds :[docIds])

				.then(function (Result) {
					return Result;
				});
		});
	}

	/**
	 * Check out documents with footer.
	 * The ordered FolderIds and DocIds must be paired.
	 * @param {int[]: FolderIds} ids of folders.
	 * @param {int[]: DocIds} id of documents.
	 * @return {Promise} .
	 */
	this.CheckOutWithFooterAsync = function (docIds, footer) {
		var that = this;
		
		footer = footer || false;

		return this.cached().then(function () {

			return _http(that._requestPath('CheckOutWithFooterAsync'), {DocIds : Array.isArray(docIds) ? docIds :[docIds], Footer:footer})

				.then(function (Result) {
					return Result;
				});
		});
	}


	/**
	 * Cancel Check out documents.
	 * @param {int[]: DocIds} id of documents.
	 * @return {Promise} .
	 */
	this.CancelCheckOut = function (docIds) {
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('CancelCheckOut'), Array.isArray(docIds) ? docIds :[docIds])

				.then(function (Result) {
					return Result;
				});
		});
	}

	/**
	* Delete document logically.
	* @param {int[] : DocumentIds} ids of documents you want to delete.
	* @param {string: Reason} Reason for delete documents.
	* @return {Promise} .
	*/
	this.Delete = function (docIds = [], reason = "") {
		//var that = this;

		return that.cached().then(function () {

			return _http(that._requestPath('DeleteAsync'), { DocumentIds: docIds, Reason: reason })

				.then(function (Result) {
					return Result;
				});
		});
	}



	this.DeleteUserWorkSpaceFile = function (docIds = [], reason = "") {
		//var that = this;

		return that.cached().then(function () {

			return _http(that._requestPath('DeleteUserWorkSpaceFile'), { UserWorkSpaceIds: docIds, Reason: reason })

				.then(function (Result) {
					return Result;
				});
		});
	}

	/**
	 * Cancel Check out documents.
	 * @param {int[]: DocIds} id of documents.
	 * @return {Promise} .
	 */
	this.Archive = function (docIds) {
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('Archive'), Array.isArray(docIds) ? docIds :[docIds])

				.then(function (Result) {
					return typeof Result === 'string' ? Result : '';
				});
		});
	}

	this.CreateDMSLink = function (docIds) {
		var that = this;

		return this.cached().then(function () {

			return _http(that._requestPath('CreateDMSLink'), Array.isArray(docIds) ? docIds :[docIds])

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.SearchByDMS = function (file){

		var that = this;

		var formData = new FormData();
		formData.append("file1", file);

		var data = {
						cache : false,

						contentType : false,

						processData : false,

						data: formData
					};

		return this.cached().then(function () {

			return _http(that._requestPath('SearchByDMS'), {}, data)

				.then(function (Result) {
					return Result;
				});
		});
	}

	this.GetNotificationGroupsAsync = function (criteria = {})
	{
		var that = this;
		return this.cached().then(function () {

			return _http(that._requestPath('GetNotificationGroupsAsync'), criteria)

				.then(function (Result) {
					return !Result ? [] : Result;
				});
		});
	}

	this.GetReviewAsync = function (id_doc) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetReviewAsync'), {DocumentId: id_doc})

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.GetReviewUsersAsync = function (id_doc) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetReviewUsersAsync'), {DocumentId: id_doc})

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.GetUsersByAsync = function (args) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetUsersByAsync'), args)

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.GetUsersAsync = function () {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetUsersAsync'), {})

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.StartReviewAsync = function (id_doc, id_version, allow_checkout, id_users, deadline,comment) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('StartReviewAsync'), {DocumentId: id_doc, VersionId: id_version,allowCheckout:allow_checkout,UserIds: id_users, deadline:deadline,comment:comment })

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.FinishReviewAsync = function (id_doc, id_version, comment) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('FinishReviewAsync'), {DocumentId: id_doc, VersionId: id_version, comment:comment })

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.IsReviewOwnerAsync = function (id_doc) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('IsReviewOwnerAsync'), {DocumentId: id_doc })

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.GetReviewHistoryAsync = function (id_doc) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetReviewHistoryAsync'), {DocumentId: id_doc })

					.then(function (Result) {
						return Result;
					});
			});
	}

	this.UpdateDocumentTypeAttributeAsync = function (id_doc_type,id_attr,chkAttr,chkDup,atr_ids) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('UpdateDocumentTypeAttributeAsync'), {
						DocumentTypeId: id_doc_type,
						AttributeId: id_attr,
						AttributeCheck: chkAttr,
						DuplicationCheck: chkDup,
						AttributeIds: atr_ids
					})

					.then(function (Result) {
						return Result;
					});
			});
	}


	this.GetPreferenceAsync = function (obj) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('GetPreferenceAsync'), { Preference: obj})

					.then(function (Result) {
						return Result;
					});
			});
	}
	
	this.SavePreferenceAsync = function (obj) {
		var that = this;

		return this.cached()

			.then(function () {

				return _http(that._requestPath('SavePreferenceAsync'), { Preference: obj})

					.then(function (Result) {
						return Result;
					});
			});
	}



	/*
		Utilities
		*/
	this.Fn = {

		isNil: function (arg) {

			if (typeof arg === 'undefined' || arg === null || String(arg) === "null" || arg === '') {
				return true;
			} else if (typeof arg === 'object' && arg.length === 0) {
				return true;
			} else if (typeof arg === 'object' && Object.keys(arg).length === 0) {
				return true;
			}
			return false;
		},
		QueryString: function (name, url) {
			if (!url) {
				url = window.location.href;
			}
			name = name.replace(/[\[\]]/g, "\\$&");
			var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
				results = regex.exec(url);
			if (!results) return null;
			if (!results[2]) return '';
			return decodeURIComponent(results[2].replace(/\+/g, " "));
		},

		Download: function (uri, name) {
			var link = document.createElement("a");
			link.download = name;
			link.href = uri;
			document.body.appendChild(link);
			link.click();
			document.body.removeChild(link);
			delete link;
		},

		IsTyped4NumOrChar: function (e) {
			return (e.which <= 90 && e.which >= 48 || e.which >= 96 && e.which <= 105);
		},

		IsTyped4Del: function (e) {
			return (e.which === 8 || e.which === 46);
		},

		ToByteArray: function (base64text) {

			var raw = window.atob(base64text); //Decode a base-64 encoded string

			var rawLength = raw.length;
			var array = new Uint8Array(new ArrayBuffer(rawLength));

			for (i = 0; i < rawLength; i++) {
				array[i] = raw.charCodeAt(i);
			}
			return array;
		},

		isJson: function(str) {
			try {
				JSON.parse(str);
			} catch (e) {
				return false;
			}
			return true;
		},

		GetAppInfo: function (filename) {
			var db = [{
				ext: ".doc",
				mime: "application/msword"
			},
			{
				ext: ".dot",
				mime: "application/msword"
			},
			{
				ext: ".docx",
				mime: "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
			},
			{
				ext: ".dotx",
				mime: "application/vnd.openxmlformats-officedocument.wordprocessingml.template"
			},
			{
				ext: ".docm",
				mime: "application/vnd.ms-word.document.macroEnabled.12"
			},
			{
				ext: ".dotm",
				mime: "application/vnd.ms-word.template.macroEnabled.12"
			},
			{
				ext: ".xls",
				mime: "application/vnd.ms-excel"
			},
			{
				ext: ".xlt",
				mime: "application/vnd.ms-excel"
			},
			{
				ext: ".xla",
				mime: "application/vnd.ms-excel"
			},
			{
				ext: ".xlsx",
				mime: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
			},
			{
				ext: ".xltx",
				mime: "application/vnd.openxmlformats-officedocument.spreadsheetml.template"
			},
			{
				ext: ".xlsm",
				mime: "application/vnd.ms-excel.sheet.macroEnabled.12"
			},
			{
				ext: ".xltm",
				mime: "application/vnd.ms-excel.template.macroEnabled.12"
			},
			{
				ext: ".xlam",
				mime: "application/vnd.ms-excel.addin.macroEnabled.12"
			},
			{
				ext: ".xlsb",
				mime: "application/vnd.ms-excel.sheet.binary.macroEnabled.12"
			},
			{
				ext: ".ppt",
				mime: "application/vnd.ms-powerpoint"
			},
			{
				ext: ".pot",
				mime: "application/vnd.ms-powerpoint"
			},
			{
				ext: ".pps",
				mime: "application/vnd.ms-powerpoint"
			},
			{
				ext: ".ppa",
				mime: "application/vnd.ms-powerpoint"
			},
			{
				ext: ".pptx",
				mime: "application/vnd.openxmlformats-officedocument.presentationml.presentation"
			},
			{
				ext: ".potx",
				mime: "application/vnd.openxmlformats-officedocument.presentationml.template"
			},
			{
				ext: ".ppsx",
				mime: "application/vnd.openxmlformats-officedocument.presentationml.slideshow"
			},
			{
				ext: ".ppam",
				mime: "application/vnd.ms-powerpoint.addin.macroEnabled.12"
			},
			{
				ext: ".pptm",
				mime: "application/vnd.ms-powerpoint.presentation.macroEnabled.12"
			},
			{
				ext: ".potm",
				mime: "application/vnd.ms-powerpoint.template.macroEnabled.12"
			},
			{
				ext: ".ppsm",
				mime: "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"
			},
			{
				ext: ".mdb",
				mime: "application/vnd.ms-access"
			},

			{
				ext: ".au",
				mime: "audio/basic"
			},
			{
				ext: ".avi",
				mime: "video/msvideo, video/avi, video/x-msvideo"
			},
			{
				ext: ".bmp",
				mime: "image/bmp"
			},
			{
				ext: ".bz2",
				mime: "application/x-bzip2"
			},
			{
				ext: ".css",
				mime: "text/css"
			},
			{
				ext: ".dtd",
				mime: "application/xml-dtd"
			},
			{
				ext: ".es",
				mime: "application/ecmascript"
			},
			{
				ext: ".exe",
				mime: "application/octet-stream"
			},
			{
				ext: ".gif",
				mime: "image/gif"
			},
			{
				ext: ".gz",
				mime: "application/x-gzip"
			},
			{
				ext: ".hqx",
				mime: "application/mac-binhex40"
			},
			{
				ext: ".html",
				mime: "text/html"
			},
			{
				ext: ".jar",
				mime: "application/java-archive"
			},
			{
				ext: ".jpg",
				mime: "image/jpeg"
			},
			{
				ext: ".js",
				mime: "application/x-javascript"
			},
			{
				ext: ".midi",
				mime: "audio/x-midi"
			},
			{
				ext: ".mp3",
				mime: "audio/mpeg"
			},
			{
				ext: ".mpeg",
				mime: "video/mpeg"
			},
			{
				ext: ".ogg",
				mime: "audio/vorbis, application/ogg"
			},
			{
				ext: ".pdf",
				mime: "application/pdf"
			},
			{
				ext: ".pl",
				mime: "application/x-perl"
			},
			{
				ext: ".png",
				mime: "image/png"
			},
			{
				ext: ".ps",
				mime: "application/postscript"
			},
			{
				ext: ".qt",
				mime: "video/quicktime"
			},
			{
				ext: ".ra",
				mime: "audio/x-pn-realaudio, audio/vnd.rn-realaudio"
			},
			{
				ext: ".ram",
				mime: "audio/x-pn-realaudio, audio/vnd.rn-realaudio"
			},
			{
				ext: ".rdf",
				mime: "application/rdf, application/rdf+xml"
			},
			{
				ext: ".rtf",
				mime: "application/rtf"
			},
			{
				ext: ".sgml",
				mime: "text/sgml"
			},
			{
				ext: ".sit",
				mime: "application/x-stuffit"
			},
			{
				ext: ".sldx",
				mime: "application/vnd.openxmlformats-officedocument.presentationml.slide"
			},
			{
				ext: ".svg",
				mime: "image/svg+xml"
			},
			{
				ext: ".swf",
				mime: "application/x-shockwave-flash"
			},
			{
				ext: ".tar.gz",
				mime: "application/x-tar"
			},
			{
				ext: ".tgz",
				mime: "application/x-tar"
			},
			{
				ext: ".tiff",
				mime: "image/tiff"
			},
			{
				ext: ".tsv",
				mime: "text/tab-separated-values"
			},
			{
				ext: ".txt",
				mime: "text/plain"
			},
			{
				ext: ".wav",
				mime: "audio/wav, audio/x-wav"
			},
			{
				ext: ".xlam",
				mime: "application/vnd.ms-excel.addin.macroEnabled.12"
			},
			{
				ext: ".xml",
				mime: "application/xml"
			},
			{
				ext: ".zip",
				mime: "application/zip, application/x-compressed-zip"
			},
			{
				ext: ".eml",
				mime: "message/rfc822"
			},
			{
				ext: ".msg",
				mime: "message/rfc822"
			},
			];

			return db.find(function (x) {
				debugger
				return x.ext.replace('.', '') === filename.split('.').pop();
			});
		},

		toDatabase: function (origin) {
			var root = _.cloneDeep(origin);

			function _recurceble(child) {
				if (_.isArray(child)) {
					for (var i = 0; i < child.length; i++)
						_recurceble(child[i]);

					return child;
				}

				for (var key in child) {
					// skip loop if the property is from prototype
					if (!child.hasOwnProperty(key)) continue;

					//if($.type(child) != "object" && $.type(child) != "function" ) return child;

					var value = child[key];

					if ($.type(value) === "object" || $.type(value) === "function") return _recurceble(value);

					if ($.type(value) === "string") {
						child[key] = (/^\/Date\([0-9]+\)\/$/.test(value)) ? moment(value).toDate() : value;
					}
				}

			}

			_recurceble(root);

			return root;
		}
	};

	/*
		Cache
	*/
	this.cache =
	{
		keys:
		{
			foldersL1: function(idParent = 0, permission = 0){ 
				return 'db-folder_' + idParent + '_' + permission ;
			}
		},
		db: function(){ return $sessionStorage; },
		noop: function (data)
    	{
			var deferred = $q.defer();

			setTimeout( function() { return deferred.resolve(data);}, 1 );

			return deferred.promise;
		},
		
		/*
		 * Remove cache with prefix. If falsy value is passed, all cache removes without thrid party method. 
		 */
		clearAll: function(prefix)
		{
			var that = this;
			prefix = prefix || '';

			var keys = Object.keys(this.db()).filter( function(x) { return !x.includes('$') && x.includes(prefix); } );

			keys.forEach( function(key){
				that.clear(key);
				
				console.log("db cache ("+ key + ") has benn cleared.");
			}); 
 
		},

		clear: function(key)
		{
			if (key === undefined)
				$sessionStorage.$reset(key);
			else
				$sessionStorage.$resetAt(key);
		},

		settings: function()
		{
			var item = 'db-settings';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetSettings().then(function(res) { return  $sessionStorage[item] = _.cloneDeep(res) ;})
			}
		},

		doctypes: function()
		{
			var item = 'db-doctypes';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetDocumentTypes().then( function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},

		attrs: function()
		{
			var item = 'db-attrs';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetAttributes().then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},


		comboitems: function(idAttrs = 0 )
		{

			var item = 'db-comboitem_' + idAttrs;
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{

				return that.GetAttributeComboboxItems(idAttrs).then( function(res) { return $sessionStorage[item] = _.cloneDeep(res) ;});
			}
		},

		foldersL1: function(idParent = 0, permission = 0)
		{

			// var item = `db-folder_${idParent}_${permission}` ;
			var item = 'db-folder_' + idParent + '_' + permission ;
			var item2 = this.keys.foldersL1(idParent,permission) ;
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{

				return that.GetFoldersL1Async(idParent,permission).then( function(res) { return $sessionStorage[item] = _.cloneDeep(res) ;});
			}
		},

		myprofile: function()
		{
			var item = 'db-myprofile';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetMyProfileAsync().then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},

		notificationgroups: function()
		{
			var item = 'db-notification-groups';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetNotificationGroupsAsync().then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},

		users: function()
		{
			var item = 'db-users';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetUsersAsync().then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},

		reviewers: function(id_doc)
		{
			var item = 'db-reviewers_' + id_doc;
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetReviewUsersAsync(id_doc).then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},

		permissionLevels: function()
		{
			var item = 'db-permission-levels';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetPermissionLevels().then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},

		fieldtypes: function()
		{
			var item = 'db-field-types';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetAttributeFieldTypesAsync().then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},
		permissiontitles: function()
		{
			var item = 'db-permission-titles';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetFolderPermissionTitlesAsync().then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},

		customviews: function()
		{
			var item = 'db-customviews';
			if( item in this.db())
			{
				return this.noop(this.db()[item]);
			}
			else
			{
				return that.GetCustomViewsAsync().then(function(res) { return $sessionStorage[item] = _.cloneDeep(res);} )
			}
		},

	}

	//if (!this.LoginedUser || this.LoginedUser.id == 0) this._login();


});








