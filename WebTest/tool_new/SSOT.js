var SSOT = {};
SSOT.getCookieVal = function (offset) {
    var endstr = document.cookie.indexOf(";", offset);
    if (endstr == -1)
        endstr = document.cookie.length;
    var value = "";
    try {
        value = decodeURI(document.cookie.substring(offset, endstr))
    } catch (e) {
        value = unescape(document.cookie.substring(offset, endstr));
    }
    return value;
}

SSOT.getCookie = function(name) {
    var arg = name + "=";
    var alen = arg.length;
    var clen = document.cookie.length;
    var i = 0;
    while (i < clen) {
        var j = i + alen;
        if (document.cookie.substring(i, j) == arg)
            return SSOT.getCookieVal(j);
        i = document.cookie.indexOf(" ", i) + 1;
        if (i == 0)
            break;
    }
    return null;
}
SSOT.getuname = function () {
    var uname = SSOT.getCookie("31796_u9_uname");
    if (uname) {
        return uname;
    }
    return null;
}

SSOT.getuid = function () {
    var uname = SSOT.getCookie("9jn_u9_user");
    if (uname) {
        return uname.split("\t")[1];
    }
    return null;
}
