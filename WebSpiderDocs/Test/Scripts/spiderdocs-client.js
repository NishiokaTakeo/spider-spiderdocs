(function (global, factory) {

    factory(global, Q, jQuery);

})(window != 'undefined' ? window : this, function (global, Q, $) {

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
            txt = "";
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

    app = function (userconf) {

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
            return;
        }

        if (!this.LoginedUser || this.LoginedUser.id == 0) this._login();
    }

    app.prototype.cached = function cached() {
        if (this._iconf.DisableAuth()) {
            return Q.fcall(function () { });
        } else {
            return $.extend(true, {}, this._cache);
        }
        //return $.extend(true,{},this._cache);
    }

    app.prototype._requestPath = function _requestPath(path) {
        return this._iconf.GetServerURL() + "/External/{0}/{1}".replace("{0}", path).replace("{1}", this.LoginedUser.id);
    }

    app.prototype.http = _http = function _http(url, object, header) {
        header = header || {};

        if (!Q) console.error("jsgrid_Custom.js is depend on Q. See mroe information https://github.com/kriskowal/q");

        var
            deferred = Q.defer(),

            params = Fn.toDatabase(object),

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

                if (json) {
                    try {

                        var formated = Fn.toDatabase((typeof json == 'string' ? JSON.parse(json) : json));
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
                debugger;
                deferred.reject(error.message);
            }
        };

        header = $.extend({}, def, header)

        $.ajax(header);

        return deferred.promise;
    }


    //---------------------------------------------------------------------------------
    // User authentication functions.
    //---------------------------------------------------------------------------------
    app.prototype._login = function _login() {
        if (!this._iconf.LoginName() || !this._iconf.LoginPassword()) {
            return;
        }

        var that = this;

        return this._cache = _http(this._iconf.GetServerURL() + '/Users/Login', {
            UserName: this._iconf.LoginID(),
            Password_Md5: md5(this._iconf.LoginPassword())
        })

            .then(function (context) {
                that.LoginedUser = context.User;
                qReady.resolve({});
                return context && context.User && context.User.id > 0;
            });

    }

    app.prototype.ready = (fnc = () => { }) => {
        debugger;
        qReady.promise.then(() => {
            debugger;
            fnc();
        });
    }

    //---------------------------------------------------------------------------------
    // This function searches documents as per Criteria and returns document information.
    //---------------------------------------------------------------------------------
    app.prototype.GetDocuments = function GetDocuments(criteria) {

        var that = this;
        return this.cached().then(function () {

            return _http(that._requestPath('GetDocuments'), criteria)

                .then(function (Result) {
                    return !Result ? [] : Result.Documents;
                });
        });
    }

    /**
     * This function returns URLs of requested documents.
     * Users can download the documents from the URLs.
     * @param {int array} versionIds - version ids which you want to get download-url
     * @return {Promise} .
     */
    app.prototype.GetDonloadUrls = function Export(versionIds) {
        var criteria = {
            'VersionIds': versionIds
        };
        var that = this;

        return this.cached()

            .then(function () {

                return _http(that._requestPath('Export'), criteria)

                    .then(function (Result) {
                        return !Result.Urls ? [] : Result.Urls;
                    });
            });
    }

    /**
     * Merge PDF from more than one document
     * @param {Array: int} version id. *
     * @param {Object: Document} Document Class.
     * @return {Promise} .
     */
    app.prototype.ToPdf = function ToPdf(versionIds, document) {
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
    app.prototype.GetFolders = function GetFolders(ids) {
		ids = ids || [];

        var that = this;
        return this.cached().then(function () {

			return _http(that._requestPath('GetFolders'), { FolderIDs: ids})

                .then(function (Result) {
                    return !Result ? [] : Result.Folders;
                });
        });
    }

	app.prototype.GetFoldersL1 = function GetFoldersL1(idParent) {

		var that = this;
		return this.cached().then(function () {

			return $http(that._requestPath('GetFoldersL1'), { idParent: idParent })

				.then(function (Result) {
					return !Result ? [] : Result.Folders;
				});
		});
	}

    //---------------------------------------------------------------------------------
    // This function searches documents as per Criteria and returns document information.
    //---------------------------------------------------------------------------------
    app.prototype.SaveFolder = function SaveFolder(folder) {

        var that = this;
        return this.cached().then(function () {

            return _http(that._requestPath('SaveFolder'), folder)

                .then(function (Result) {
                    return !Result ? [] : Result.Folder;
                });
        });
    }

    //---------------------------------------------------------------------------------
    // This function searches documents as per Criteria and returns document information.
    //---------------------------------------------------------------------------------
    app.prototype.GetDocumentTypes = function GetDocumentTypes() {

        var that = this;
        return this.cached().then(function () {

            return _http(that._requestPath('GetDocumentTypes'))

                .then(function (Result) {
                    return !Result ? [] : Result.DocumentTypes;
                });
        });
    }

    //---------------------------------------------------------------------------------
    // This function searches documents as per Criteria and returns document information.
    //---------------------------------------------------------------------------------
    app.prototype.GetAttributes = function GetAttributes() {

        var that = this;
        return this.cached().then(function () {

            return _http(that._requestPath('GetAttributes'))

                .then(function (Result) {
                    return !Result ? [] : Result.Attributes;
                });
        });
    }

    app.prototype.GetAttributeDocType = function GetAttributeDocType(id_doc_types) {
        id_doc_types = id_doc_types || [];
        if (typeof id_doc_types == "string") id_doc_types = [id_doc_types];

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
    app.prototype.GetAttributeComboboxItems = function GetAttributeComboboxItems(id_attr) {
        var params = {
            AttributeId: id_attr,
            ItemId: 0
        };

        var that = this;
        return this.cached().then(function () {

            return _http(that._requestPath('GetAttributeComboboxItems'), params)

                .then(function (Result) {
                    return !Result ? [] : Result.ComboboxItems;
                });
        });
    }

    /**
     * Get User information.
     * @param {string} username - name to login
     * @param {string} password - password with md5 to login
     * @return {Promise} .
     */
    app.prototype.GetUser = function GetUser(username, password) {
        var criteria = {
            'UserName': username,
            'Password': password
        };
        var that = this;

        return this.cached()

            .then(function () {

                return _http(that._requestPath('GetUser'), criteria)

                    .then(function (Result) {
                        return Result.User;
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
    app.prototype.UpdateUser = function UpdateUser(user, username, password) {
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
    app.prototype.GetHistories = function GetHistories(criteria) {
        var that = this;

        return this.cached()

            .then(function () {

                return _http(that._requestPath('GetHistories'), criteria)

                    .then(function (Result) {
                        return Result;
                    });
            });
    }


    //---------------------------------------------------------------------------------
    // Save a document which corresponds to given temp id.
    //---------------------------------------------------------------------------------
    app.prototype.Import = function Import(tempid, document) {

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
    app.prototype.SaveDoc = function SaveDoc(tempid, document) {

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

    /**
     * Upload File and Import at one method
     * @param {Object: Document} Document Class.
     * Be aware, combo item children is saved only this method. Import method doesn't save
     * @param {Object: Byte[] or Blob} actual file.
     * @return {Promise} .
     */
    app.prototype.RemoteImport = function UploadImport(url, document, combo) {
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

    /**
     * Check out documents.
     * The ordered FolderIds and DocIds must be paired.
     * @param {int[]: FolderIds} ids of folders.
     * @param {int[]: DocIds} id of documents.
     * @return {Promise} .
     */
    app.prototype.CheckOut = function CheckOut(docIds,folderIds) {
        var that = this;

        return this.cached().then(function () {

            return _http(that._requestPath('CheckOut'), { DocIds: docIds, FolderIds: folderIds})

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
    app.prototype.CancelCheckOut = function CancelCheckOut(docIds) {
        var that = this;

        return this.cached().then(function () {

            return _http(that._requestPath('CancelCheckOut'), { DocIds: docIds})

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
    app.prototype.Delete = function Delete(docIds = [], reason = "") {
        var that = this;

        return this.cached().then(function () {

            return _http(that._requestPath('Delete'), { DocumentIds: docIds, Reason: reason })

                .then(function (Result) {
                    return Result;
                });
        });
    }

    /**
    * Archive document logically.
    * @param {int[] : docIds} ids of documents you want to delete.
    * @return {Promise} .
    */
    app.prototype.Archive = function Archive(docIds = []) {
        var that = this;

        return this.cached().then(function () {

            return _http(that._requestPath('Archive'), { DocIds: docIds })

                .then(function (Result) {
                    return Result;
                });
        });
    }

    /**
    * UnArchive document logically.
    * @param {int[] : docIds} ids of documents you want to delete.
    * @return {Promise} .
    */
    app.prototype.UnArchive = function UnArchive(docIds = []) {
        var that = this;

        return this.cached().then(function () {

            return _http(that._requestPath('UnArchive'), { DocIds: docIds })

                .then(function (Result) {
                    return Result;
                });
        });
    }

    /*
        Utilities
     */
    app.prototype.Fn = {

        isNil: function (arg) {

            if (typeof arg === 'undefined' || arg === null || String(arg) === "null" || arg === '') {
                return true;
            } else if (typeof arg === 'object' && arg.length == 0) {
                return true;
            } else if (typeof arg === 'object' && Object.keys(arg).length == 0) {
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
            return (e.which == 8 || e.which == 46);
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
            ];

            return db.find(function (x) {
                return x.ext.replace('.', '') == filename.split('.').pop();
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

                    if ($.type(value) == "object" || $.type(value) == "function") return _recurceble(value);

                    if ($.type(value) == "string") {
                        child[key] = (/^\/Date\([0-9]+\)\/$/.test(value)) ? moment(value).toDate() : value;
                    }
                }

            }

            _recurceble(root);

            return root;
        }
    };

    //return app;

    global.SpiderDocsClient = app;

    });


(function (global, factory) {

    factory(global, Q, jQuery);

})(window != 'undefined' ? window : this, function (global, Q, $) {

    var app = {};

    app = function (client) {

        if (!client) {
            alert('need to pass client');
        }

        this.client = {}, this.client = client;
    }

    /**
	 * Download As Binary by specified criteria you passed
	 * @param {SpiderDocsModule.SearchCriteria} criteria - criteria you want to download
	 * @return {object} doc title, Blob.
	 */
    app.prototype.downloadAsBinary = function (criteria) {

        var app = {}, doc = {};
        var client = this.client;

        var q = (!Array.isArray(criteria) ?

							client.GetDocuments(criteria)

							.then(function (docs) {
							    //

							    doc = docs.pop();
                                app = client.Fn.GetAppInfo(doc.title);

							    return [doc.id_version];

							    //return docs.map(function(x){ return x.id_version;});
							})
						:
							Q.fcall(function () { return criteria; }));

        return q.then(function (id_versions) {
            return client.GetDonloadUrls(id_versions);
        })

				.then(function (urls) {

				    //
				    //show or download file from url
				    var url = urls.pop();
				    return client.http(url, {}, {
				        type: "GET",
				        dataType: "blob",
				        processData: false,
				        data: undefined,
				        contentType: client.Fn.GetAppInfo(url)
				    });
				})

				.then(function (binary) {
				    var blog = new Blob([binary], { type: binary.type });

				    return {
				        title: doc.title,
				        blob: blog
				    }
				});
    }

    //return app;
    global.SpiderDocHelper = app;

});

