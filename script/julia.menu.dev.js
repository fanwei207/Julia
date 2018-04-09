(function ($) {
    $.fn.Menu = function (object) {
        var defaults = {
            width: "100px"
        };
        defaults = $.extend(defaults, object);
        //阻止冒泡
        function stopPropagation(e) {
            e = e || window.event;
            if (e.stopPropagation) {
                //W3C阻止冒泡方法
                e.stopPropagation();
            } else {
                e.cancelBubble = true;
            }
        }
        //end function
        var not_clicked = true;
        //尚未点击
        $(this).css({
            width: defaults.width
        });
        $("li").css({
            width: defaults.width
        });
        $("li:has(ul)", this).css({
            position: "relative",
            left: "0px"
        });
        //为有子菜单的项添加上定位方式
        $("li:has(ul)", this).find("ul").css({
            position: "absolute",
            width: defaults.width,
            left: defaults.width
        });
        //为有子菜单的项添加上定位方式
        $("li ul", this).hide();
        //第一次加载时隐藏子菜单
        this.each(function () {
            var topmenu = $("li:has(ul)", this);
            //找到有子菜单的li
            topmenu.hover(function () {
                var $folder = $(this);
                var $sub_folder = $folder.find("ul:eq(0)");
                //菜单过长的，要上移；但，如果子项高度少于10px，就返回！此子项为非法项，未找出原因
                var top = 0;
                if ($sub_folder.height() < 10) {
                    return false;
                }
                var h_window = $(window).height() - 3;
                //要减去边线
                if (h_window < $folder.offset().top + $sub_folder.height()) {
                    top = h_window - ($folder.offset().top + $sub_folder.height()) - 5;
                }
                //$("#divNav").append("<br />h_window:" + h_window + "  top:" + $folder.offset().top + "  height:" + $sub_folder.height());
                $sub_folder.css("top", top + "px");
                //目标对象没有运行动画
                if (!$sub_folder.is(":animated")) {
                    $sub_folder.css({
                        left: defaults.width
                    }).slideDown("normal");
                }
            }, //over
            function () {
                //目标对象
                var $target_menu = $(this).find("ul:eq(0)");
                $target_menu.slideUp("normal");
            });
            //end hover
            $("li", this).click(function (e) {
                if (not_clicked) {
                    if ($("a", this).attr("href") != null && $("a", this).attr("href") != "#" && $("a", this).attr("href") != "#null") {
                        //如果有页面正在打开，则不允许打开新的窗口，以免造成资源浪费
                        if ($("#j-objec-loding").css("display") == "block") {
                            not_clicked = false;
                            alert("另一个窗口正在打开中，请稍后...");
                            stopPropagation(e);
                            return false;
                        } else if ($("#ulTabs li").length > 6) //mark 1
                        {
                            not_clicked = false;
                            alert("最多只能同时打开6个标签!");
                            stopPropagation(e);
                            return false;
                        } else {
                            //刷新SesionKeepper.aspx，保证Session不容易丢失
                            var obj_current = $("a", this);
                            var id = obj_current.attr("id");
                            var helpDoc = obj_current.attr("doc");
                            /*
                            1、新追加的tab默认选中
                            2、其他tab取消选中
                            3、新追加的iframe默认显示
                            4、隐藏其他的iframe
                            5、已经存在的，要选中
                            6、我的桌面只要选中即可
                            */
                            $("#ulTabs li").removeClass("tab_selected");
                            $("iFrame").hide();
                            if ($("#tab_" + id).length == 0) {
                                /*
                                给IFrame添加等待事件
                                基本原理是：事先隐藏一个div，iFrame完成前显示，完成后隐藏即可
                                */
                                $.loading("block");
                                //先加标签，再加IFrame
                                $("#ulTabs").append("<li id='tab_" + id + "' class='tab_selected' doc='" + helpDoc + "'><a href='#' title='" + $(this).text() + "'><span style='font-size:12px; color:#416aa3;white-space:nowrap; position:relative;'>&nbsp;&nbsp;" + $(this).text() + "<strong id='close_" + id + "'>&nbsp;&nbsp;x&nbsp;</strong></span></a></li>");
                                $("#ifrmHolder").append('<iframe id="ifrm_' + id + '" frameborder="0" style="width: 100%;" src="' + obj_current.attr("href").replace("#", "") + "?HeightPixel=" + $("#HeightPixel").val() + '"></iframe>');
                                $("#ifrm_" + id).load(function () {
                                    $.loading("none");
                                    $("#ifrm_" + id).height($("#ifrmHolder").height());
                                });
                                //end load
                                //显示帮助
                                if (helpDoc != "" && helpDoc != " ") {
                                    $("#isHelp").css("display", "block").attr("href", helpDoc);
                                } else {
                                    $("#isHelp").css("display", "none");
                                }
                            } else {
                                //把标签选中
                                $("#tab_" + id).addClass("tab_selected");
                            }
                            $("#ifrm_" + id).show();
                            /*
                            关闭标签
                        
                            1、销毁tab、iFrame
                            2、前一个tab处于选中状态
                            */
                            $("#ulTabs li #close_" + id).click(function () {
                                var curr_li = $("#" + $(this).attr("id").replace("close_", "tab_"));
                                var curr_fr = $("#" + $(this).attr("id").replace("close_", "ifrm_"));
                                //如果要关闭和当前正在显示的不一致，则显示当前，否则显示前一个
                                if ($("iFrame:visible").attr("id") == curr_fr.attr("id")) {
                                    curr_li.prev().addClass("tab_selected");
                                    curr_fr.prev().show();
                                    $.loading("none");
                                }
                                //销毁当前的iFrame
                                curr_li.remove();
                                curr_fr.remove();
                            });
                            //end click
                            /*
                            标签：单击
                            */
                            $("#ulTabs li").click(function () {
                                $("#ulTabs li").removeClass("tab_selected");
                                $(this).addClass("tab_selected");
                                /*
                                1、隐藏所有的iFrame
                                2、显示当前的，tab_[id] => ifrm_[id]
                                */
                                $("iFrame").hide();
                                $("#" + $(this).attr("id").replace("tab_", "ifrm_")).show();
                                //显示帮助
                                if ($(this).attr("doc").replace(" ", "") != "") {
                                    $("#isHelp").css("display", "block").attr("href", $(this).attr("doc"));
                                } else {
                                    $("#isHelp").css("display", "none");
                                }
                                return false;
                            });
                        }
                    }
                }
                //end if
                not_clicked = false;
                stopPropagation(e);
                return false;
            }).hover(function () {
                not_clicked = true;
            }, function () {
                not_clicked = true;
            });
        });
        //end each
        return this;
    };
})(jQuery);