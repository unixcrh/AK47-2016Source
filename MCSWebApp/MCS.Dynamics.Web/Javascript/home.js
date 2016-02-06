/// <reference path="jquery-1.7.2.min.js" />

// 减号图片  Images/jianhao.gif" />
// 加号图片  Images/jiahao.gif" />
$.fn.extend({
    toggleClick: function (options) {
        var defaults = {
            category: 0
        };
        options = $.extend(defaults, options);
        this.toggle(function () {
            //执行展开
            $("img.img_menu:eq(0)", $(this)).attr("src", "Images/jianhao.gif");
            var $create = $(this).data("creaet");
            if ($create) {
                $create.show();
                return;
            }
            $(this).createNavRoot();
            //$(this).data("isCreate",true);
        }, function () {
            //执行折叠
            var $create = $(this).data("creaet");
            $("img.img_menu:eq(0)", $(this)).attr("src", "Images/jiahao.gif");
            $create.hide();
        });


        return this;
    },
    createNavRoot: function (options) {
        var defaults = {
            category: 0
        };
        options = $.extend(defaults, options);
        var $this = this; //.attr({ id: "root_tow" })
        var $divTow = $("<div>").css({ "margin-left": "16px", display: "block" });  // 创建二级菜单 容器
        $.post("inc/ajaxResult.ashx", { ajaxAciont: "getRoot" }, function (data) {

            $.each(data, function () {
                var $table = $("<table>").attr({ "cellspacing": "0", "cellpadding": "0" }).css("cursor","hand");
                var $tbody = $("<tbody>");
                var $tr = $("<tr>");
                var $tdImg = $("<td>");
                var $imgMent = $("<img>").attr("src", "Images/jiahao.gif").addClass("img_menu");  // 创建 加  减 图片
                $imgMent.appendTo($tdImg);
                $tdImg.appendTo($tr);
                var $tdEmpety = $("<td>").css("width", "1px");   // 创建 空的td
                $tdEmpety.appendTo($tr);

                var $tdWenTd = $("<td>");  // 创建文件夹td
                var $spanWen = $("<span>").css({ overflow: "hidden", cursor: "hand", display: "inline-block" });
                var $imgWen = $("<img>").attr("src", "Images/wenjianjia.gif");  //创建文件夹 图片
                $imgWen.appendTo($spanWen);
                $spanWen.appendTo($tdWenTd);
                $tdWenTd.appendTo($tr);


                // 创建类别名称
                var $tdContent = $("<td>");
                $tdContent.attr({ "title": this.categoryName, "class": "pc-orgnode" });
                $tdContent.css("padding-left", "3px");
                $tdContent.text(this.rootName);
                $tdContent.appendTo($tr);

                $tr.appendTo($tbody);
                $tbody.appendTo($table);
                $table.appendTo($divTow).toggleClick();
            });
            $divTow.insertAfter($this);
            $this.data("creaet",$divTow);
        }, "json");



        return this;
    }
});



