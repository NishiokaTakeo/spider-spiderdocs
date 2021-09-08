String.prototype.hexEncode = function(){
    var hex, i;

    var result = "";
    for (i=0; i<this.length; i++) {
        hex = this.charCodeAt(i).toString(16);
        result += ("000"+hex).slice(-4);
    }

    return result
}

String.prototype.hexDecode = function(){
    var j;
    var hexes = this.match(/.{1,4}/g) || [];
    var back = "";
    for(j = 0; j<hexes.length; j++) {
        back += String.fromCharCode(parseInt(hexes[j], 16));
    }

    return back;
}

String.prototype.is_date_format = function () {

    var date_regex = /^([0-9]){4}\-([0-9]){1,2}\-([0-9]){1,2}.*$/;
    return date_regex.test(this);
}


/*
    Global Utility
*/
var Util = (function () {

    var _return = {};


    _return.is_date_format = function (v) {
        var date_regex = /^([0-9]){4}\-([0-9]){1,2}\-([0-9]){1,2}.*$/;
        return date_regex.test(v);
    }

    return _return;

})();
