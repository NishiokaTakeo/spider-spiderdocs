// Any Contant variable 
var CONST = {
    origin : "http://" + window.location.host,
}

/**
 *
 * Define Global Functions
 * Default is json request.
 **/

function $http(url, object, header)
{
    header = header || {};
    if (!Q) console.error("jsgrid_Custom.js is depend on Q. See mroe information https://github.com/kriskowal/q");

	var deferred = Q.defer(); 

    var params = Fn.toDatabase(object); 

	var json_string = JSON.stringify(params);
    
    var def = {
                type: "POST",
                //url: (new RegExp(/^https?:\/\//).test(url)) ? url : url_header + url,
                url: (new RegExp(/^\//).test(url)) ? url_header + url : url,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: json_string,
                success :  function (result) {
                    if( this.dataType == "blob"||this.dataType == "arraybuffer") return deferred.resolve( result);
                    var json = result.d || result;

                    if (json) {
                        try {

                            var formated = Fn.toDatabase((typeof json == 'string' ? JSON.parse(json) : json));
                            deferred.resolve(formated);
                            //deferred.resolve((typeof json == 'string' ? JSON.parse(json) : json))
                        } catch (e) {
                            deferred.resolve(json)
                        }
                    }
                    else {
                        deferred.resolve({});
                    }

                },
                error: function (xhr, status, error) {
                    debugger;
                    deferred.reject(error.message);
                }
            };

    header = $.extend({},def,header)

	$.ajax(header);

	return deferred.promise;
}


var Fn = {
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
    QueryString : function(name, url) {
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

    Download: function(uri,name){
        var link = document.createElement("a");
        link.download = name;
        link.href = uri;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        delete link;
    },    

    GetAppInfo: function(filename)
    {
        var db = [
            {ext:".doc"     ,mime:"application/msword"},
            {ext:".dot"     ,mime:"application/msword"},
            {ext:".docx"     ,mime:"application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {ext:".dotx"     ,mime:"application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
            {ext:".docm"     ,mime:"application/vnd.ms-word.document.macroEnabled.12"},
            {ext:".dotm"     ,mime:"application/vnd.ms-word.template.macroEnabled.12"},
            {ext:".xls"     ,mime:"application/vnd.ms-excel"},
            {ext:".xlt"     ,mime:"application/vnd.ms-excel"},
            {ext:".xla"     ,mime:"application/vnd.ms-excel"},
            {ext:".xlsx"     ,mime:"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {ext:".xltx"     ,mime:"application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
            {ext:".xlsm"     ,mime:"application/vnd.ms-excel.sheet.macroEnabled.12"},
            {ext:".xltm"     ,mime:"application/vnd.ms-excel.template.macroEnabled.12"},
            {ext:".xlam"     ,mime:"application/vnd.ms-excel.addin.macroEnabled.12"},
            {ext:".xlsb"     ,mime:"application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
            {ext:".ppt"     ,mime:"application/vnd.ms-powerpoint"},
            {ext:".pot"     ,mime:"application/vnd.ms-powerpoint"},
            {ext:".pps"     ,mime:"application/vnd.ms-powerpoint"},
            {ext:".ppa"     ,mime:"application/vnd.ms-powerpoint"},
            {ext:".pptx"     ,mime:"application/vnd.openxmlformats-officedocument.presentationml.presentation"},
            {ext:".potx"     ,mime:"application/vnd.openxmlformats-officedocument.presentationml.template"},
            {ext:".ppsx"     ,mime:"application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
            {ext:".ppam"     ,mime:"application/vnd.ms-powerpoint.addin.macroEnabled.12"},
            {ext:".pptm"     ,mime:"application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
            {ext:".potm"     ,mime:"application/vnd.ms-powerpoint.template.macroEnabled.12"},
            {ext:".ppsm"     ,mime:"application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
            {ext:".mdb"     ,mime:"application/vnd.ms-access"},
            
            {ext:".au"	,mime:"audio/basic"},
            {ext:".avi"	,mime:"video/msvideo, video/avi, video/x-msvideo"},
            {ext:".bmp"	,mime:"image/bmp"},
            {ext:".bz2"	,mime:"application/x-bzip2"},
            {ext:".css"	,mime:"text/css"},
            {ext:".dtd"	,mime:"application/xml-dtd"},
            {ext:".es"	,mime:"application/ecmascript"},
            {ext:".exe"	,mime:"application/octet-stream"},
            {ext:".gif"	,mime:"image/gif"},
            {ext:".gz"	,mime:"application/x-gzip"},
            {ext:".hqx"	,mime:"application/mac-binhex40"},
            {ext:".html"	,mime:"text/html"},
            {ext:".jar"	,mime:"application/java-archive"},
            {ext:".jpg"	,mime:"image/jpeg"},
            {ext:".js"	,mime:"application/x-javascript"},
            {ext:".midi"	,mime:"audio/x-midi"},
            {ext:".mp3"	,mime:"audio/mpeg"},
            {ext:".mpeg"	,mime:"video/mpeg"},
            {ext:".ogg"	,mime:"audio/vorbis, application/ogg"},
            {ext:".pdf"	,mime:"application/pdf"},
            {ext:".pl"	,mime:"application/x-perl"},
            {ext:".png"	,mime:"image/png"},
            {ext:".ps"	,mime:"application/postscript"},
            {ext:".qt"	,mime:"video/quicktime"},
            {ext:".ra"	,mime:"audio/x-pn-realaudio, audio/vnd.rn-realaudio"},
            {ext:".ram"	,mime:"audio/x-pn-realaudio, audio/vnd.rn-realaudio"},
            {ext:".rdf"	,mime:"application/rdf, application/rdf+xml"},
            {ext:".rtf"	,mime:"application/rtf"},
            {ext:".sgml"	,mime:"text/sgml"},
            {ext:".sit"	,mime:"application/x-stuffit"},
            {ext:".sldx"	,mime:"application/vnd.openxmlformats-officedocument.presentationml.slide"},
            {ext:".svg"	,mime:"image/svg+xml"},
            {ext:".swf"	,mime:"application/x-shockwave-flash"},
            {ext:".tar.gz"	,mime:"application/x-tar"},
            {ext:".tgz"	,mime:"application/x-tar"},
            {ext:".tiff"	,mime:"image/tiff"},
            {ext:".tsv"	,mime:"text/tab-separated-values"},
            {ext:".txt"	,mime:"text/plain"},
            {ext:".wav"	,mime:"audio/wav, audio/x-wav"},
            {ext:".xlam"	,mime:"application/vnd.ms-excel.addin.macroEnabled.12"},
            {ext:".xml"	,mime:"application/xml"},
            {ext:".zip"	,mime:"application/zip, application/x-compressed-zip"},
        ];

        return db.find(function(x){  return x.ext.replace('.','') == filename.split('.').pop(); });
    },

    toDatabase: function (origin) {
        
        var root = _.cloneDeep(origin);
        
        //
        //var root = jQuery.extend({},origin);

        function _recurceble(child)
        {
            if (_.isArray(child))
            {
                for (var i = 0 ; i < child.length ; i++)
                    _recurceble(child[i]);

                return child;
            }
            
            for (var key in child) {
                // skip loop if the property is from prototype
                if (!child.hasOwnProperty(key)) continue;

                //if(jQuery.type(child) != "object" && jQuery.type(child) != "function" ) return child;

                var value = child[key];

                if(jQuery.type(value) == "object" || jQuery.type(value) == "function" ) return _recurceble(value);

                if(jQuery.type(value) == "string" )
                {
                    child[key] = ( /^\/Date\([0-9]+\)\/$/.test(value))? moment(value ).toDate() : value;
                }
            }

        }

        _recurceble(root);

        return root;
    }
};
