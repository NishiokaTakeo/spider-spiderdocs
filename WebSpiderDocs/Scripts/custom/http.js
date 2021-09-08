
(function( global, factory){
    // Test back ground
    setInterval(function () {
        var portnumber = window.location.port;
        if (portnumber === "8088" || portnumber === "63163" || portnumber === "8089") {
            $("#lblTest").show();
        }
    }, 1000);
    factory(global, jQuery);
})(window != 'undefined' ? window : this, function(global,_$){

var jQuery = _$;

/**
 *
 * Define Global Functions
 * Default is json request.
 **/

var xhr = new Array();

function http(url, object, header, bAbort)
{
    if (xhr.includes(url) == false && bAbort)
    {
        xhr.push(url);
    }
    if (xhr[url] && bAbort)
    {
        xhr[url].abort();
        xhr[url] = null;
        Spin.start();
    }
    header = header || {};
    if (!Q) console.error("jsgrid_Custom.js is depend on Q. See mroe information https://github.com/kriskowal/q");
    //if ( object == undefined || !object ) header.type = 'GET';

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
                    Spin.stop();
                    deferred.reject(error.message);
                }
            };

    header = $.extend({},def,header)
    if( header.type.toLowerCase() == "get")
    {
        delete header.dataType       ;
        delete header.data ;
        delete header.contentType;
    }

    // if( header.type.toLowerCase() == "post")
    // {
    //     delete header.dataType;
    //     delete header.data ;
    //     delete header.contentType;
    //     header.contentType = "application/x-www-form-urlencoded; charset=UTF-8";

    //     var data = keys({a:1,b:2}); var body = "";
    //     for( var key in data)
    //         body = body + `${key}=${data[key]}&`;

    //     header.data = body
    // }

    xhr[url] = $.ajax(header);

	return deferred.promise;
}

function get(url,p)
{
    var deferred = Q.defer();

    $.get(url, p).done(function(data, status){
        deferred.resolve(data)
    });

    return deferred.promise;
}

global.$http = http;
global.$_get = get;
});


