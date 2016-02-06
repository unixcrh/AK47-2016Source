if (!("lib" in window)) {
    window.lib = {}
}
jQuery(function ($) {
    window.lib.click_event = $.fn.tap ? "tap" : "click"
});

//菜单是否最小化状态
lib.is_min = function () {
    return $("#sidebar").hasClass("menu-min");
};

//菜单根节点<ul class="nav nav-list">
lib.root_menu = function () {
    return $("#sidebar").find(".nav-list");
}


lib.sidebar_collapsed = function (opened) {
    opened = opened || false;

    var $sidebar = $("#sidebar");
    var $icon = $("#sidebar-collapse").find('[class*="icon-"]');
    var icon_left = $icon.attr("data-icon-left");
    var icon_right = $icon.attr("data-icon-right");

    if (opened) {
        $sidebar.addClass("menu-min");
        $icon.removeClass(icon_left);
        $icon.addClass(icon_right);

        //ace.settings.set("sidebar", "collapsed");
    } else {
        $sidebar.removeClass("menu-min");
        $icon.removeClass(icon_right);
        $icon.addClass(icon_left);

        //ace.settings.unset("sidebar", "collapsed")
    }
};

lib.menuitem_event = function ($a) {
    var $ul_menu = lib.root_menu();
    var $ul_parent = $a.parents("ul");
    var mined = lib.is_min();

    $ul_menu.find("li.active").each(function () {
        var self = this;
        $ul_parent.each(function () {
            if ($(this).parent().get(0).id != self.id) {
                $(self).removeClass("active").removeClass("open");
            }
        });
    });
    
    $ul_parent.each(function () {
        if ($(this).hasClass("nav-list") == false) {
            $(this).parent().addClass("active").addClass("open");
        }
    });
    $a.parent().addClass("active");
};

lib.menu_event = function ($a) {
    var $ul_submenu = $a.next();
    if (!$ul_submenu || $ul_submenu.length == 0) {
        return false;
    }

    var $ul_menu = lib.root_menu();
    var mined = lib.is_min();
    var $ul = $a.parents("ul");

    $ul_menu.find("> .active > .submenu").each(function () {
        if ($(this) != $ul_submenu && !$(this).parent().hasClass("active")) {
            $(this).parent().removeClass("open").removeClass("active");
        }
    });

    $ul_submenu.slideToggle(200).parent().toggleClass("active").toggleClass("open");
    return false;
};

lib.handle_side_menu = function (options) {
    $("#menu-toggler").on(lib.click_event, function () {
        $("#sidebar").toggleClass("display");
        $(this).toggleClass("display");
        return false
    });

    $("#sidebar-collapse").on(lib.click_event, function () {
        var mined = $("#sidebar").hasClass("menu-min");
        lib.sidebar_collapsed(!mined);
    });

    $(".nav-list").on(lib.click_event, function ($event) {
        var $a = $($event.target).closest("a");
        if (!$a || $a.length == 0) {
            return;
        }

        if ($a.hasClass("dropdown-toggle")) {
            lib.menu_event($a);
        } else {
            lib.menuitem_event($a);
        }

        return options.callback($a);
    })
};

lib.handle_settings = function () {
    $("#ace-settings-btn").on(lib.click_event, function () {
        $(this).toggleClass("open");
        $("#ace-settings-box").toggleClass("open")
    });
};