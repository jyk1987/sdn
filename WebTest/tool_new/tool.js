jQuery(function () {
    jQuery(".v_webnav").mouseover(function () {
        jQuery(this).find(".v_allnav").addClass("v_active").next().show();
    }).mouseout(function () {
        jQuery(this).find(".v_allnav").removeClass("v_active").next().hide()
    });
})

var TOP_SSO = {};

TOP_SSO.cutString = function (str, len) {
    var strlen = 0;
    var s = "";
    for (var i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) > 128) {
            strlen += 2;
        } else {
            strlen++;
        }
        s += str.charAt(i);
        if (strlen >= len) {
            return s;
        }
    }
    return s;
}

TOP_SSO.getmsg = function () {
    var uid = SSOT.getuid();
    jQuery.getJSON("http://dev.uuu9.com/apps/SSOT/GetUserMsgNum.ashx?callback=?&uid=" + uid, { t: new Date().getTime() }, function (result) {
        jQuery("#satopmsgd").html("站内信(" + result.num + ")");
    });
}
TOP_SSO.display = function () {
    jQuery(".r_login_qian").hide();
    jQuery(".r_robin_hou").show();
    jQuery("#satopunamed").html(TOP_SSO.cutString(SSOT.getuname(),9));
    TOP_SSO.getmsg();
    jQuery("#satopimgd").attr("src", "http://bbs.uuu9.com/uc_server/avatar.php?uid=" + SSOT.getuid() + "&size=small");
    jQuery("#satopexit").attr("src", "http://u.uuu9.com/logging.php?ac=logout&referer=" + location.href);
}

jQuery("#satopreferer").val(location.href);
jQuery(".navbox_s > div").hide();
jQuery(".navbox_s > span").click(function () { jQuery(".navbox_s > div").toggle(); });
jQuery(".navbox_s > div > a").click(function () { jQuery(".navbox_s > div").toggle(); jQuery(".navbox_s > span").html(jQuery(this).html()); jQuery("#satoploginfield").val(jQuery(this).attr("pname")); });
if (SSOT.getuname()) {
    TOP_SSO.display();
}
function uuu9_as_top_check() {
    var content = "";
    jQuery("#ssotopform input").each(function (index) {
        content += jQuery(jQuery(this)).val() + "\n";
    });
    alert(content);

    if (jQuery("#satopusername").val() == "" || jQuery("#satoppassword").val() == "") {
        return false;
    }
    return true;
}