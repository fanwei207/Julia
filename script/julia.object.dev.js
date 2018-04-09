/*
Jquery扩展方法
*/
/*
*调用Cookie
*/
jQuery.cookie = function (name, value, options) {
    if (typeof value != "undefined") {
        options = options || {};
        if (value === null) {
            value = "";
            options = $.extend({}, options);
            options.expires = -1;
        }
        var expires = "";
        if (options.expires && (typeof options.expires == "number" || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == "number") {
                date = new Date();
                date.setTime(date.getTime() + options.expires * 24 * 60 * 60 * 1e3);
            } else {
                date = options.expires;
            }
            expires = "; expires=" + date.toUTCString();
        }
        var path = options.path ? "; path=" + options.path : "";
        var domain = options.domain ? "; domain=" + options.domain : "";
        var secure = options.secure ? "; secure" : "";
        document.cookie = [name, "=", encodeURIComponent(value), expires, path, domain, secure].join("");
    } else {
        var cookieValue = null;
        if (document.cookie && document.cookie != "") {
            var cookies = document.cookie.split(";");
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                if (cookie.substring(0, name.length + 1) == name + "=") {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};

/*
*调用自定义模态窗口
*优先winSrc！如果其为空，则winText
*/
jQuery.window = function (winTitle, winWidth, winHeight, winSrc, winText, bReresh) {
    if (typeof winTitle == "undefined" || winTitle == "") {
        winTitle = "模态展示窗口";
    }
    if (typeof winWidth == "undefined" || winWidth == "" || parseInt(winWidth) == 0) {
        winWidth = 800;
    }
    if (typeof winHeight == "undefined" || winHeight == "" || parseInt(winHeight) == 0) {
        winHeight = 600;
    }
    if (typeof winSrc == "undefined") {
        winSrc = "";
    }
    if (typeof bReresh == "undefined") {
        bReresh = false;
    }
    $.loading("block");
    var _bodyWidth = $(window).width();
    var _bodyHeight = $(document).height();
    // alert($(document).height());
    //如果有嵌套iframe，则取最外层的
    if ($("BODY", parent.parent.parent.document).size() > 0) {
        _bodyWidth = $("BODY", parent.parent.parent.document).width();
        _bodyHeight = $("BODY", parent.parent.parent.document).height();
    }
    var reg = /\d%$/;
    if (reg.test(winWidth)) {
        winWidth = _bodyWidth * winWidth.replace("%", "") / 100;
    }
    if (reg.test(winHeight)) {
        winHeight = _bodyHeight * winHeight.replace("%", "") / 100;
    }
    var _dialogWidth = winWidth > _bodyWidth ? _bodyWidth : winWidth;
    var _dialogHeight = winHeight > _bodyHeight ? _bodyHeight : winHeight;
    var _MaskDiv = $("<div id='j-modal-dialog'><div class='modal-dialog-mask'></div></div>");
    _MaskDiv.width(_bodyWidth);
    _MaskDiv.height("100%");
    var _modal_dialog_panel = $("<div class='modal-dialog-panel'></div>");
    _modal_dialog_panel.css({
        width: _dialogWidth,
        height: _dialogHeight,
        top: 30,
        //(_bodyHeight - _dialogHeight) / 2,
        left: (_bodyWidth - _dialogWidth) / 2
    });
    var _modal_dialog_header = $("<div class='modal-dialog-header'></div>");
    var _modal_dialog_title = $("<div class='modal-dialog-title'></div>").text(winTitle);
    var _modal_dialog_icon = $("<div class='modal-dialog-icon icon-folder'></div>");
    var _modal_dialog_tool = $("<div class='modal-dialog-tool'></div>");
    var _modal_dialog_close = $('<a href="javascript:void(0)" class="icon-no"></a>');
    _modal_dialog_close.click(function () {
        _MaskDiv.remove();
        if (bReresh) {
            $.loading("block");
            window.location.href = document.URL;
            $("body").load(function () {
                $.loading("none");
            });
        }
    });
    _modal_dialog_tool.append(_modal_dialog_close);
    _modal_dialog_header.append(_modal_dialog_title).append(_modal_dialog_icon).append(_modal_dialog_tool);
    var _modal_dialog_body = $("<div class='modal-dialog-body'></div>");
    var _modal_dialog_iframe = $("<iframe class='modal-dialog-iframe'></iframe>");
    _modal_dialog_iframe.attr({
        frameborder: 0,
        framespacing: 0,
        marginheight: 0,
        marginwidth: 0,
        src: winSrc
    });
    _modal_dialog_iframe.load(function () {
        $.loading("none");
    });
    if (winSrc != "") {
        _modal_dialog_body.css({
            height: _dialogHeight - 27
        }).append(_modal_dialog_iframe);
    } else {
        _modal_dialog_body.css({
            height: _dialogHeight - 27,
            overflow: "auto"
        }).html(winText);
        $.loading("none");
    }
    _modal_dialog_panel.append(_modal_dialog_header).append(_modal_dialog_body);
    _MaskDiv.append(_modal_dialog_panel);
    if ($("BODY", parent.parent.parent.document).size() > 0) {
        _MaskDiv.appendTo($("BODY", parent.parent.parent.document));
    } else {
        _MaskDiv.appendTo($("BODY"));
    }
};

//end window
jQuery.loading = function (status) {
    var _id = "j-object-loading";
    var _loading = $("BODY", parent.parent.parent.document).find("#j-object-loading");
    if (_loading.size() > 0) {
        _loading.css("display", status);
    } else {
        var _winHeight = $(window).height();
        var _winWidth = $(window).width();
        var _outDivStyle = {
            position: "absolute",
            left: "0",
            display: status,
            width: "100%",
            height: "100%",
            "background-color": "#E0ECFF",
            border: "0",
            "text-align": "center",
            top: "0",
            opacity: "0.8",
            filter: "alpha(opacity=80)",
            "z-index": "9999999"
        };
        var _innerDivStyle = {
            position: "relative",
            "margin-top": (_winHeight - 200) / 2 + "px",
            "margin-left": (_winWidth - 200) / 2 + "px",
            "float": "left",
            cursor: "wait",
            width: "200px",
            "line-height": "40px",
            "background-color": "#fff",
            border: "2px solid #ccc",
            color: "#000"
        };
        var _imgStyle = {
            position: "absolute",
            top: "12px",
            left: "3px"
        };
        var _divLoding = $("<div id='" + _id + "'><div id='j-object-loading-inner'><img id='j-object-loading-img' alt='' />Loading Data，Please Wait...</div></div>");
        _divLoding.css(_outDivStyle);
        _divLoding.find("#j-object-loading-inner").css(_innerDivStyle);
        _divLoding.find("#j-object-loading-img").attr("src", "../images/loading.gif").css(_imgStyle);
        $("BODY", parent.parent.parent.document).append(_divLoding);
    }
};

//end loading
/*
Jquery对象插件
*/
(function ($) {
    /* 
    * Name : Calculator
    * Version : 1.0
    * Copyright (c) 2013 Shanzm.tcp-china
    * Date : 2013-3-23
    * Decription : 数字输入器
    * 
    */
    $.fn.Calculator = function (options) {
        var defaults = {
            panelground: "#d2e0f0",
            //面板背景
            keyground: "#fff",
            //按键默认颜色
            keyout: "#9B96CA",
            //按键鼠标out颜色
            keyover: "#fff"
        };
        var opt = $.extend(defaults, options);
        var id = "JULIA-CALCULATOR";
        //保证ID的唯一性
        return this.each(function () {
            $("#" + id).remove();
            var target = $(this);
            var _html = '<div id="' + id + '" style="width: 180px; display:block; text-align:left; position:absolute; z-index:9999; background-color:' + opt.panelground + "; font-size:12px; " + '">';
            _html += "   <div>";
            _html += "      <ul>";
            _html += "          <li>1</li>";
            _html += "          <li>2</li>";
            _html += "          <li>3</li>";
            _html += "          <li>退格</li>";
            _html += "      </ul>";
            _html += "  </div>";
            _html += "  <div>";
            _html += "      <ul>";
            _html += "          <li>4</li>";
            _html += "          <li>5</li>";
            _html += "          <li>6</li>";
            _html += "          <li>&nbsp;</li>";
            _html += "      </ul>";
            _html += "  </div>";
            _html += "  <div>";
            _html += "      <ul>";
            _html += "          <li>7</li>";
            _html += "          <li>8</li>";
            _html += "          <li>9</li>";
            _html += "          <li>清空</li>";
            _html += "      </ul>";
            _html += "  </div>";
            _html += "  <div>";
            _html += "      <ul>";
            _html += "          <li>0</li>";
            _html += "          <li>.</li>";
            _html += "          <li>-</li>";
            _html += "          <li>关闭</li>";
            _html += "      </ul>";
            _html += "  </div>";
            _html += "</div>";
            $(target).after(_html);
            $("#" + id).css({
                left: $(target).offset().left + $(target).width() + 5 + "px",
                top: $(target).offset().top + "px"
            });
            $("#" + id + " DIV").css({
                width: "100%",
                "margin-top": "2px",
                "text-align": "center"
            });
            $("#" + id + " DIV UL").css({
                margin: "0px",
                padding: "0px",
                "line-height": "30px",
                "list-style": "none"
            });
            $("#" + id + " DIV UL LI").css({
                width: "40px",
                "background-color": opt.keyground,
                "float": "left",
                "margin-left": "2px",
                border: "solid 1px silver",
                cursor: "pointer"
            });
            $("#" + id).fadeIn();
            $("#" + id + " li").click(function () {
                /*
                1、两个空键不处理
                2、点击“.”时，如果目标控件值为空或是“-”，则补齐“0.”
                3、点击“-”时，清空原值，替换成“-”
                4、点击“清空”时，目标控件值设置为空
                5、如果MaxLength设置值了，需要考虑
                6、如果第二位输入的不是小数点，而第一位是0的话，去除0
                */
                if ($(this).html().length > 0) {
                    var maxLength = 9999;
                    if ($(target).attr("maxlength") != undefined && $(target).attr("maxlength") != null) {
                        maxLength = $(target).attr("maxlength");
                    }
                    if ($(this).html() == "&nbsp;") {
                        return false;
                    } else if ($(this).html() == ".") {
                        if ($(target).val().length == 0) {
                            $(target).val("0.");
                        } else if ($(target).val() == "-") {
                            $(target).val("-0.");
                        } else {
                            //已经存在.的时候，不操作
                            if ($(target).val().indexOf(".") == -1) {
                                $(target).val($(target).val() + $(this).html());
                            }
                        }
                    } else if ($(this).html() == "-") {
                        $(target).val("-");
                    } else if ($(this).html() == "清空") {
                        $(target).val("");
                    } else if ($(this).html() == "关闭") {
                        $("#" + id).remove();
                    } else if ($(this).html() == "退格") {
                        if ($(target).val().length > 0) {
                            $(target).val($(target).val().substring(0, $(target).val().length - 1));
                        }
                    } else {
                        if ($(target).val().length == 1 && $(target).val() == "0") {
                            $(target).val($(this).html());
                        } else {
                            $(target).val($(target).val() + $(this).html());
                        }
                    }
                    //超出的部分截除
                    if (maxLength <= $(target).val().length) {
                        $(target).val($(target).val().substring(0, maxLength));
                    }
                }
                //end if
                return false;
            }).hover(function () {
                $(this).css("background-color", opt.keyout);
            }, function () {
                $(this).css("background-color", opt.keyover);
            });
            //设置document事件，这样点击空白地方可以关闭日历。但内部每个事件必须禁止冒泡
            $(document).click(function (e) {
                $("#" + id).remove();
            });
        });
    };
    //end Calculator
    /***************************************/
    /************ 日期拾取器 ***************/
    /************ datePicker ***************/
    /**************************************/
    $.fn.datePicker = function (options) {
        var defaults = {
            language: "cn"
        };
        var opt = $.extend(defaults, options);
        var id = "JULIA-DATEPICKER";
        //保证ID的唯一性
        function renderHeader(year, month) {
            //固定头
            var header = '<div id="' + id + '" style="width: 280px; display:none; text-align: center; position: absolute;';
            header += '         background-color: #d2e0f0; font-size: 12px; z-index:9999;">';
            header += '           <table style="width: 100%; text-align: center;" cellpadding="0" cellspacing="0">';
            header += "               <tr>";
            header += '                   <td style="height: 25px">';
            header += '                       <span id="spanPreYear' + id + '" style="cursor: pointer;">&lt;&lt;</span></td>';
            header += '                   <td style="height: 25px" colspan="5">';
            if (opt.language == "cn") {
                header += '                       <span id="spanPreMonth' + id + '" style="cursor: pointer;">&lt;</span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanYear' + id + '">' + year + "</span>年&nbsp;<span";
                header += '                           style="cursor: pointer;" id="spanMonth' + id + '">' + month + '</span>月&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanNextMonth' + id + '"';
            } else {
                header += '                       <span id="spanPreMonth' + id + '" style="cursor: pointer;">&lt;</span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanYear' + id + '">' + year + "</span>&nbsp;-&nbsp;<span";
                header += '                           style="cursor: pointer;" id="spanMonth' + id + '">' + month + '</span>&nbsp;&nbsp;&nbsp;&nbsp;<span id="spanNextMonth' + id + '"';
            }
            header += '                               style="cursor: pointer;">&gt;</span></td>';
            header += '                   <td style="height: 25px">';
            header += '                       <span id="spanNextYear' + id + '" style="cursor: pointer;">&gt;&gt;</span></td>';
            header += "               </tr>";
            header += "               <tr class='header' style=\"background-color: Black; color: White;\">";
            if (opt.language == "cn") {
                header += "                   <td>日</td> <td>一</td> <td>二</td> <td>三</td> <td>四</td> <td>五</td> <td>六</td>";
            } else {
                header += "                   <td>Sun</td> <td>Mon</td> <td>Tues</td> <td>Wed</td> <td>Thur</td> <td>Fri</td> <td>Sat</td>";
            }
            header += "               </tr>";
            header += "           </table>";
            header += "       </div>";
            return header;
        }
        //日期主体
        function renderBody(year, month) {
            var today = new Date();
            //今天
            var target_year = year;
            var target_month = month;
            target_month = target_month - 1;
            var month_first_date = new Date(target_year, target_month, 1);
            var month_first_day = month_first_date.getDay();
            //第一天是周几
            var month_middle_date = new Date(target_year, target_month, 28);
            //每月28号的4天后，铁定是下一个月
            var month_last_date = new Date(month_middle_date.valueOf() + 4 * 24 * 60 * 60 * 1e3);
            month_last_date = new Date(month_last_date.getFullYear(), month_last_date.getMonth(), 1);
            month_last_date = new Date(month_last_date.valueOf() - 24 * 60 * 60 * 1e3);
            var month_last_day = month_last_date.getDay();
            //最后一天是周几
            //原理是：下个月第一天 - 上个月的第一天 - 1
            var during_days = 1 + (month_last_date.valueOf() - month_first_date.valueOf()) / (24 * 60 * 60 * 1e3);
            var begin = 0;
            var end = during_days;
            //如果第一天不是周天的话，则要取出上个月的日期
            if (month_first_day > 0) {
                begin -= month_first_day;
            }
            //如果最后一天不是周六的话，则要取出下个月的日期
            if (month_last_day < 6) {
                end += 6 - month_last_day;
            }
            var body = '<tr class="calender_week">';
            for (var i = begin; i < end; i++) {
                var dd = new Date(month_first_date.valueOf() + i * 24 * 60 * 60 * 1e3);
                //如果是周天的话，要换行
                if (dd.getDay() == 0) {
                    body += '</tr><tr class="calender_week">';
                    if (i < 0 || i >= during_days) {
                        body += '<td class="disable_day">' + dd.getDate() + "</td>";
                    } else {
                        body += "<td class=\"calender_day\" onmouseover=\"$(this).css('background-color', '#fff');\" onmouseout=\"$(this).css('background-color', '#d2e0f0');\">" + dd.getDate() + "</td>";
                    }
                } else {
                    if (i < 0 || i >= during_days) {
                        body += '<td class="disable_day" style=" border-left: solid 1px #fff;">' + dd.getDate() + "</td>";
                    } else {
                        if (dd.getFullYear() == today.getFullYear() && dd.getMonth() == today.getMonth() && dd.getDate() == today.getDate()) {
                            body += "<td class=\"calender_day calender_today\" style=\" border-left: solid 1px #fff;\" onmouseover=\"$(this).css('background-color', '#fff');\" onmouseout=\"$(this).css('background-color', '#d2e0f0');\">" + dd.getDate() + "</td>";
                        } else {
                            body += "<td class=\"calender_day\" style=\" border-left: solid 1px #fff;\" onmouseover=\"$(this).css('background-color', '#fff');\" onmouseout=\"$(this).css('background-color', '#d2e0f0');\">" + dd.getDate() + "</td>";
                        }
                    }
                }
            }
            body += "</tr>";
            return body;
        }
        return this.each(function () {
            $("#" + id).remove();
            var _target = $(this);
            var current_date = new Date();
            if ($(_target).val().length > 0) {
                current_date = new Date(Date.parse($(_target).val().replace(/-/g, "/")));
            }
            if (isNaN(current_date)) {
                if (opt.language == "cn") {
                    alert("日期格式只能是YYYY-MM-DD！例如：2013-4-15！");
                } else {
                    alert("Date Fomat:YYYY-MM-DD！Example：2013-4-15！");
                }
                return;
            }
            var year = current_date.getFullYear();
            var month = current_date.getMonth();
            $(_target).after(renderHeader(year, month + 1));
            if (/msie/.test(navigator.userAgent.toLowerCase())) {
                $("#" + id).css({
                    left: $(_target).offset().left + $(_target).width() + 5 + "px",
                    top: $(_target).offset().top + "px"
                });
            } else {
                $("#" + id).css({
                    left: $(_target).offset().left + $(_target).width() + 5 + "px",
                    top: $(_target).offset().top + "px"
                });
            }
            $("#" + id + " TABLE").append(renderBody(year, month + 1));
            $("#" + id + " .header TD").css({
                width: "40px",
                height: "24px"
            });
            $("#" + id + " .calender_week .disable_day").css({
                width: "40px",
                height: "24px",
                "border-top": "solid 1px #fff",
                "background-color": "Silver"
            });
            $("#" + id + " .calender_week .calender_day").css({
                width: "40px",
                height: "24px",
                "border-top": "solid 1px #fff",
                cursor: "pointer"
            });
            $("#" + id + " .calender_week .calender_today").css({
                width: "40px",
                height: "24px",
                border: "solid 1px red",
                cursor: "pointer"
            });
            $("#" + id).fadeIn();
            //$("#test").text($("#" + id).html());
            //绑定事件
            $("#spanPreYear" + id).click(function () {
                var now = new Date($("#spanYear" + id).text(), 0, 1);
                now = now.valueOf();
                now = now - 24 * 60 * 60 * 1e3;
                now = new Date(now);
                $("#spanYear" + id).text(now.getFullYear());
                //重新生成日期的Body
                var month = $("#spanMonth" + id).text();
                $("#" + id + " .calender_week").remove();
                $("#" + id + " TABLE").append(renderBody(now.getFullYear(), month));
                $("#" + id + " .header TD").css({
                    width: "40px",
                    height: "24px"
                });
                $("#" + id + " .calender_week .disable_day").css({
                    width: "40px",
                    height: "24px",
                    "border-top": "solid 1px #fff",
                    "background-color": "Silver"
                });
                $("#" + id + " .calender_week .calender_day").css({
                    width: "40px",
                    height: "24px",
                    "border-top": "solid 1px #fff",
                    cursor: "pointer"
                });
                $("#" + id + " .calender_week .calender_today").css({
                    width: "40px",
                    height: "24px",
                    border: "solid 1px red",
                    cursor: "pointer"
                });
                $("#" + id + " .calender_day").click(function () {
                    $(_target).val($("#spanYear" + id).text() + "-" + $("#spanMonth" + id).text() + "-" + $(this).text());
                    $("#" + id).remove();
                });
                return false;
            });
            $("#spanNextYear" + id).click(function () {
                var now = new Date($("#spanYear" + id).text(), 11, 31);
                now = now.valueOf();
                now = now + 24 * 60 * 60 * 1e3;
                now = new Date(now);
                $("#spanYear" + id).text(now.getFullYear());
                //重新生成日期的Body
                var month = $("#spanMonth" + id).text();
                $("#" + id + " .calender_week").remove();
                $("#" + id + " TABLE").append(renderBody(now.getFullYear(), month));
                $("#" + id + " .header TD").css({
                    width: "40px",
                    height: "24px"
                });
                $("#" + id + " .calender_week .disable_day").css({
                    width: "40px",
                    height: "24px",
                    "border-top": "solid 1px #fff",
                    "background-color": "Silver"
                });
                $("#" + id + " .calender_week .calender_day").css({
                    width: "40px",
                    height: "24px",
                    "border-top": "solid 1px #fff",
                    cursor: "pointer"
                });
                $("#" + id + " .calender_week .calender_today").css({
                    width: "40px",
                    height: "24px",
                    border: "solid 1px red",
                    cursor: "pointer"
                });
                $("#" + id + " .calender_day").click(function () {
                    var year = $("#spanYear" + id).text();
                    var month = $("#spanMonth" + id).text();
                    var day = $(this).text();
                    if (month.length == 1) {
                        month = "0" + month;
                    }
                    if (day.length == 1) {
                        day = "0" + day;
                    }
                    $(_target).val(year + "-" + month + "-" + day);
                    $("#" + id).remove();
                });
                return false;
            });
            $("#spanPreMonth" + id).click(function () {
                var now = new Date($("#spanYear" + id).text(), $("#spanMonth" + id).text() - 1, 1);
                now = now.valueOf();
                now = now - 24 * 60 * 60 * 1e3;
                now = new Date(now);
                $("#spanYear" + id).text(now.getFullYear());
                var month = now.getMonth();
                month = month + 1;
                $("#spanMonth" + id).text(month);
                //重新生成日期的Body
                $("#" + id + " .calender_week").remove();
                $("#" + id + " TABLE").append(renderBody(now.getFullYear(), month));
                $("#" + id + " .header TD").css({
                    width: "40px",
                    height: "24px"
                });
                $("#" + id + " .calender_week .disable_day").css({
                    width: "40px",
                    height: "24px",
                    "border-top": "solid 1px #fff",
                    "background-color": "Silver"
                });
                $("#" + id + " .calender_week .calender_day").css({
                    width: "40px",
                    height: "24px",
                    "border-top": "solid 1px #fff",
                    cursor: "pointer"
                });
                $("#" + id + " .calender_week .calender_today").css({
                    width: "40px",
                    height: "24px",
                    border: "solid 1px red",
                    cursor: "pointer"
                });
                $("#" + id + " .calender_day").click(function () {
                    var year = $("#spanYear" + id).text();
                    var month = $("#spanMonth" + id).text();
                    var day = $(this).text();
                    if (month.length == 1) {
                        month = "0" + month;
                    }
                    if (day.length == 1) {
                        day = "0" + day;
                    }
                    $(_target).val(year + "-" + month + "-" + day);
                    $("#" + id).remove();
                });
                return false;
            });
            $("#spanNextMonth" + id).click(function () {
                var now = new Date($("#spanYear" + id).text(), $("#spanMonth" + id).text() - 1, 28);
                now = now.valueOf();
                now = now + 4 * 24 * 60 * 60 * 1e3;
                //不管每月有多少天，从28号往后数四天，必然到下一个月
                now = new Date(now);
                $("#spanYear" + id).text(now.getFullYear());
                var month = now.getMonth();
                month = month + 1;
                $("#spanMonth" + id).text(month);
                //重新生成日期的Body
                $("#" + id + " .calender_week").remove();
                $("#" + id + " TABLE").append(renderBody(now.getFullYear(), month));
                $("#" + id + " .header TD").css({
                    width: "40px",
                    height: "24px"
                });
                $("#" + id + " .calender_week .disable_day").css({
                    width: "40px",
                    height: "24px",
                    "border-top": "solid 1px #fff",
                    "background-color": "Silver"
                });
                $("#" + id + " .calender_week .calender_day").css({
                    width: "40px",
                    height: "24px",
                    "border-top": "solid 1px #fff",
                    cursor: "pointer"
                });
                $("#" + id + " .calender_week .calender_today").css({
                    width: "40px",
                    height: "24px",
                    border: "solid 1px red",
                    cursor: "pointer"
                });
                $("#" + id + " .calender_day").click(function () {
                    var year = $("#spanYear" + id).text();
                    var month = $("#spanMonth" + id).text();
                    var day = $(this).text();
                    if (month.length == 1) {
                        month = "0" + month;
                    }
                    if (day.length == 1) {
                        day = "0" + day;
                    }
                    $(_target).val(year + "-" + month + "-" + day);
                    $("#" + id).remove();
                });
                return false;
            });
            $("#" + id + " .calender_day").click(function () {
                var year = $("#spanYear" + id).text();
                var month = $("#spanMonth" + id).text();
                var day = $(this).text();
                if (month.length == 1) {
                    month = "0" + month;
                }
                if (day.length == 1) {
                    day = "0" + day;
                }
                $(_target).val(year + "-" + month + "-" + day);
                $("#" + id).remove();
                return false;
            });
            $("#" + id).click(function () {
                return false;
            });
            //设置document事件，这样点击空白地方可以关闭日历。但内部每个事件必须禁止冒泡
            $(document).click(function (e) {
                $("#" + id).remove();
            });
        });
    };
    //end datePicker
    /***************************************/
    /************ 信息提示 ***************/
    /************ toolTip ***************/
    /**************************************/
    $.fn.toolTip = function (options) {
        var defaults = {
            title: "错误提示：",
            titleground: "Olive",
            //标题背景色
            text: "",
            //正文
            textground: "Silver"
        };
        var opt = $.extend(defaults, options);
        var id = "JULIA-TOOLTIP";
        //保证ID的唯一性
        return this.each(function () {
            $("#" + id).remove();
            var _target = $(this);
            var _html = '<div id="' + id + '" style="width:200px; border:solid 1px #000; position:absolute; background-color:' + opt.textground + ';">';
            _html += '    <div style=" border-bottom:solid 1px #000; font-size:12px; font-weight:bold; background-color:' + opt.titleground + '; text-align:left;">' + opt.title + "</div>";
            _html += '    <div style ="font-size:12px; height:70px; text-align:left;">';
            _html += '        <ul style="margin-left:5px; list-style:">';
            _html += opt.text;
            _html += "        </ul>";
            _html += "    </div>";
            _html += "</div>";
            $(_target).after(_html);
            if (/msie/.test(navigator.userAgent.toLowerCase())) {
                $("#" + id).css({
                    "margin-left": "1px",
                    "margin-top": $(_target).offset().top + "px"
                });
            } else {
                $("#" + id).css({
                    left: $(_target).offset().left + $(_target).width() + 2 + "px",
                    "margin-top": $(_target).offset().top + "px"
                });
            }
            $(_target).click(function () {
                $("#" + id).remove();
            });
        });
    };
    //end tootip
    /* 
    * Name : Buyer
    * Version : 1.0
    * Copyright (c) 2013 Shanzm.tcp-china
    * Date : 2013-9-13
    * Decription : 采购员
    * 
    */
    $.fn.Buyer = function (options) {
        var id = "JULIA-BUYER";
        //保证ID的唯一性
        function callBack(domain, target) {
            $("#" + id).find(".item").fadeOut("slow").remove();
            $.ajax({
                type: "get",
                url: "/Ajax/Buyer.ashx?domain=" + domain + "&rt=" + Date.toLocaleString(),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (json) {
                    $.each(json, function (i, data) {
                        if (domain.toUpperCase() == data.Domain) {
                            var html = "";
                            html += "    <tr class='item'>";
                            html += "        <td>" + data.Code + "</td>";
                            html += "        <td>" + data.Name + "</td>";
                            html += "        <td>" + data.Domain + "</td>";
                            html += "    </tr>";
                        }
                        $("#" + id + " TABLE").append(html);
                    });
                    $("#" + id).html($("#" + id).html());
                    $("#" + id + " .item TD").css({
                        "border-bottom": "1px dotted #b8d2f0"
                    });
                    $("#" + id).fadeIn("slow");
                    $("#" + id).find("#selDomain").change(function () {
                        callBack($(this).children("option:selected").val(), target);
                    });
                    //end change
                    $("#" + id + " TABLE TR:gt(0)").click(function () {
                        $(target).val($(this).find("TD:eq(0)").text());
                        return false;
                    }).hover(function () {
                        $(this).css("background-color", "#b8d2f0");
                    }, function () {
                        $(this).css("background-color", "#fff");
                    });
                },
                error: function () { }
            });
        }
        return this.each(function () {
            var target = $(this);
            x = $(target).offset().left;
            y = $(target).offset().top + $(target).height() + 4;
            //像这种一次性全部拉取的控件，如果之前有拉取过，就没必要再次拉取了
            if ($("#" + id).size() > 0) {
                $("#" + id).css("left", x).css("top", y).fadeIn("slow");
                return false;
            }
            var _html = '<div id="' + id + '" style="width: 230px; display:none; background-color:#fff; text-align:center; border:1px solid #51a8ec; position:absolute; font-size:12px; margin-top:2px; left:' + x + "px; top:" + y + 'px;">';
            _html += "<table style='text-align:center; line-height:20px; margin:1px; cursor:pointer;' cellpadding='2' cellspacing='0'>";
            _html += "    <tr class='header' style=' background: #51a8ec; color:#fff;'>";
            _html += "        <th style='width:80px;'>代码</th>";
            _html += "        <th style='width:100px;'>姓名</th>";
            _html += "        <th style='width:50px;'>";
            _html += "          <select id='selDomain' style='font-size:11px;'>";
            _html += "              <option>SZX</option>";
            _html += "              <option>ZQL</option>";
            _html += "              <option>YQL</option>";
            _html += "              <option>HQL</option>";
            _html += "          </select>";
            _html += "        </th>";
            _html += "    </tr>";
            _html += "</table>";
            _html += "</div>";
            $(target).after(_html);
            callBack("SZX", target);
            $("#" + id).click(function () {
                return false;
            });
            //end click
            //设置document事件，这样点击空白地方可以关闭日历。但内部每个事件必须禁止冒泡
            $(document).click(function (e) {
                $("#" + id).css("display", "none");
            });
        });
    };
    //end Buyer
    /*
    自动完成
    */
    $.fn.AutoComplete = function (options,textChangedMethod) {
        var id = "JULIA-AUTOCOMPLETE";
        //保证ID的唯一性
        var defaults = {
            cols: {},
            //要显示的列属性，可以是数组：cols: [{ width: "100px", name: "零件状态" }, { width: "200px", name: "零件描述"}]
            fields: {},
            //要显示的字段，可以是数组：fields: [{ val: "Code", align: "left" }, { val: "Desc", align: "left"}]
            eVals: {},
            //额外指定将列值显示到指定控件上。默认支持5列
            url: "",
            //Ajax路径
            val: 0,
            //最终要返回的列值
            isDyn: false,
            isImm: false,
            //即便是isDyn = false，如果此字段为真，也可以立即显示
            inputCls: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            $(this).click(function () {
                //删除
                $("#" + id).remove();
                var w = 0;
                //外层Div的宽度
                var target = $(this);
                x = $(target).offset().left;
                y = $(target).offset().top + $(target).height() + 4;
                var _html = '<div id="' + id + '" style=" display:none; background-color:#fff; text-align:center; border:1px solid #51a8ec; position:absolute; font-size:11px; margin-top:2px; left:' + x + "px; top:" + y + 'px; z-index:9999;">';
                _html += "<table style='text-align:center; line-height:15px; margin:1px; cursor:pointer;' cellpadding='1' cellspacing='1'>";
                _html += "    <tr style=' background: #51a8ec; color:#fff;'>";
                $.each(opt.cols, function (index, obj) {
                    _html += "        <th style='width:" + obj.width + ";'>" + obj.name + "</th>";
                    w += parseInt(obj.width.replace("px", ""));
                });
                _html += "    </tr>";
                var _inputVal = target.val();
                var _url = opt.url;
                if (opt.inputCls != "" && $("." + opt.inputCls).size() > 0) {
                    _inputVal = $("." + opt.inputCls).val();
                }
                //只有当需要立即显示的时候，才放置此参数
                if (opt.isImm) {
                    _url += "?req=" + _inputVal;
                }
                if (!opt.isDyn || opt.isImm) {
                    $.ajax({
                        type: "get",
                        url: _url,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (json) {
                            $("#" + id).find(".item").remove();
                            $.each(json, function (i, data) {
                                _html += "    <tr>";
                                $.each(opt.fields, function (index, obj) {
                                    _html += "        <td  style=' border-bottom:1px dotted #b8d2f0; text-align:" + obj.align + ";'>" + eval("data." + obj.val) + "</td>";
                                });
                                _html += "    </tr>";
                            });
                            //end each
                            _html += "</table>";
                            _html += "</div>";
                            $(target).after(_html);
                            $("#" + id).fadeIn("slow");
                            $("#" + id + " TABLE TR:gt(0)").click(function () {
                                $__this = $(this);
                                $(target).val($__this.find("TD:eq(" + opt.val + ")").text());
                                $(target).focus();
                                //如果需要额外指定其他列，则
                                $.each(opt.eVals, function (index, obj) {
                                    if ($("." + obj.targetCls).size() > 0) {
                                        if ($("." + obj.targetCls).prop("tagName") == "SPAN") {
                                            $("." + obj.targetCls).text($__this.find("TD:eq(" + obj.valCol + ")").text());
                                        } else {
                                            $("." + obj.targetCls).val($__this.find("TD:eq(" + obj.valCol + ")").text());
                                        }
                                    }
                                });
                                if (textChangedMethod != null) {
                                    textChangedMethod();
                                }
                                return false;
                            }).hover(function () {
                                $(this).css("background-color", "#b8d2f0");
                            }, function () {
                                $(this).css("background-color", "#fff");
                            });
                            //设置document事件，这样点击空白地方可以关闭日历。但内部每个事件必须禁止冒泡
                            $(document).click(function (e) {
                                $("#" + id).remove();
                            });
                        },
                        error: function () {
                            _html += "</table>";
                            _html += "</div>";
                            $(target).after(_html);
                        }
                    });
                } else {
                    _html += "</table>";
                    _html += "</div>";
                    $(target).after(_html);
                }
                //end if (!isDyn)
                $("#" + id).width(w);
                return false;
            });
            //end click
            //动态时的键盘输入事件
            $(this).keyup(function () {
                if (!opt.isDyn) {
                    return;
                }
                var target = $(this);
                //没有输入值时，不处理，节省资源
                if ($(this).val().length == 0) {
                    return false;
                }
                //删除行
                //$("#" + id).find(".item").remove();
                $.ajax({
                    type: "get",
                    url: opt.url + "?req=" + encodeURIComponent($(this).val()) + "&rt=" + new Date().getTime(),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (json) {
                        $("#" + id).find(".item").remove();
                        $.each(json, function (i, data) {
                            var html = "";
                            html += "<tr class='item' style=' border-bottom:1px dotted #b8d2f0;'>";
                            $.each(opt.fields, function (index, obj) {
                                if (typeof eval("data." + obj.val) != "undefined") {
                                    html += "        <td  style=' border-bottom:1px dotted #b8d2f0; text-align:" + obj.align + ";'>" + eval("data." + obj.val) + "</td>";
                                }
                            });
                            html += "</tr>";
                            //end each
                            $("#" + id + " TABLE").append(html);
                        });
                        //end each
                        $("#" + id).html($("#" + id).html());
                        $("#" + id).fadeIn("slow");
                        $("#" + id + " TABLE TR:gt(0)").click(function () {
                            $__this = $(this);
                            $(target).val($(this).find("TD:eq(" + opt.val + ")").text());
                            $(target).focus();
                            //如果需要额外指定其他列，则
                            $.each(opt.eVals, function (index, obj) {
                                if ($("." + obj.targetCls).size() > 0) {
                                    if ($("." + obj.targetCls).prop("tagName") == "SPAN") {
                                        $("." + obj.targetCls).text($__this.find("TD:eq(" + obj.valCol + ")").text());
                                    } else {
                                        $("." + obj.targetCls).val($__this.find("TD:eq(" + obj.valCol + ")").text());
                                    }
                                }
                            });
                            if (textChangedMethod != null) {
                                textChangedMethod();
                            }
                            return false;
                        }).hover(function () {
                            $(this).css("background-color", "#b8d2f0");
                        }, function () {
                            $(this).css("background-color", "#fff");
                        });
                        //设置document事件，这样点击空白地方可以关闭日历。但内部每个事件必须禁止冒泡
                        $(document).click(function (e) {
                            $("#" + id).remove();
                        });
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(XMLHttpRequest.status);
                        alert(XMLHttpRequest.readyState);
                        alert(textStatus);
                    }
                });
            });
        });
    };
    //end $.fn.AutoComplete
    /*
    GridView插件
    2014-01-05
    */
    $.fn.GridView = function (options) {
        var defaults = {
            rowOver: "#E1FCCE",
            rowSel: "#D0FBB1",
            colOver: "#E1FCCE",
            colSel: "#D0FBB1",
            allowRowMultiSel: false,
            allowColMultiSel: false,
            isResizable: false,
            isHeaderFixed: false,
            fixedHeight: 300,
            allowAutoResize: false,
            panelHeight: 500,
            showSummary: true,
            startIndex:0
        };
        var opt = $.extend(defaults, options);
        opt.fixedHeight = Math.max(opt.fixedHeight, 300);
        opt.panelHeight = Math.max(opt.panelHeight, 500);
        return this.each(function () {
            //临时，删掉julia.common.js中的定位层：PositionLocationDiv
            $("#PositionLocationDiv").remove();
            var $this = $(this);
            //GridView本身
            //判定是GridView还是DataGrid
            //GridView：外层是一个空的Div
            //DataGrid：没有外层
            $type = "gv";
            $top = 0;
            if ($this.prop("tagName") == "TABLE") {
                if ($this.parent().prop("tagName") == "DIV" && $this.prevAll().size() == 0 && $this.nextAll().size() == 0) {
                    $type = "gv";
                    $top = $this.parent().offset().top;
                } else {
                    $type = "dg";
                    $top = $this.offset().top;
                }
            } else {
                document.write("注意：该控件不符合使用本插件的条件！<br />本插件可供GridView、DataGrid服务器控件使用，或类似结构的Html组件使用。");
            }
            //自动调整
            if (opt.allowAutoResize) {
                opt.fixedHeight = Math.max(opt.fixedHeight, opt.panelHeight - $top);
            }
            /*
            功能准备：
            1、模仿Excel，当选中一列时，在页脚处显示汇总信息
            2、添加一个可下拉的标识位，以便表头固定时使用
            3、如果没有GridViewPagerStyle，则添加一个
            4、添加每页要显示的页码
            5、不论是GridView还是DataGrid，都在外层再套一个Div，为可拉伸和页头固定做准备

            GridViewInfoSummary：放置汇总信息    GridViewResizableFlag：可拉动标识
            */
            $this.wrap("<div><div></div></div>");
            $this.parent().css({
                width: $this.width() + (opt.isHeaderFixed ? 19 : 0)
            });
            //设置最外层Div的position
            if (opt.isHeaderFixed) {
                $this.parent().parent().css({
                    position: "relative",
                    width: $this.width() + (opt.isHeaderFixed ? 19 : 0),
                    "text-align": "left"
                });
            } else {
                $this.parent().parent().css({
                    width: $this.width() + (opt.isHeaderFixed ? 19 : 0)
                });
            }
            //如果是页头固定的，要可滚动
            //此时，不论内外层，都要设置固定高度。该高度取自于GridView前端的Height属性
            if (opt.isHeaderFixed) {
                $this.parent().css({
                    height: Math.max(opt.fixedHeight, 400) + "px",
                    overflow: "auto",
                    top: 0,
                    left: 0,
                    "z-index": 1,
                    "text-align": "left"
                });
            }
            //            alert("opt.fixedHeight:" + opt.fixedHeight + " $top:" + $top);
            //同时，去掉内层的高
            $this.css({
                height: opt.isHeaderFixed ? "" : "100%"
            });
            //            //没有GridViewPagerStyle的，加一个新的
            //            if ($(".GridViewPagerStyle", $(this)).size() == 0) {
            //                if ($type == "dg") {
            //                    $this.append('<tr class="GridViewPagerStyle"><td colspan="' + $(".GridViewHeaderStyle TH, .GridViewHeaderStyle TD", $this).size() + '"><span>1</span></td></tr>');
            //                } else {
            //                    $this.append('<tr class="GridViewPagerStyle"><td colspan="' + $(".GridViewHeaderStyle TH, .GridViewHeaderStyle TD", $this).size() + '"><table><tr><td><span>1</span></td></tr></table></td></tr>');
            //                }
            //            }
            //如果是dg的话，则要补齐页码的Table
            if ($type == "dg") {
                //由于dg的页码使用空格隔开的，所以得先去除空格
                var _o = $(".GridViewPagerStyle TD:eq(0)", $this).html();
                if (_o) {
                    $(".GridViewPagerStyle TD:eq(0)", $this).html($(".GridViewPagerStyle TD:eq(0)", $this).html().replace(/\&nbsp\;|\&nbsp/g, ""));
                }
                $(".GridViewPagerStyle TD:eq(0)", $this).append('<table border="0"><tr></tr></table>').find("SPAN, A").each(function () {
                    $(this).wrap("<td></td>").parent().appendTo($(".GridViewPagerStyle TD:eq(0) TABLE:eq(0) TR:eq(0)", $this));
                });
            }
            if (opt.allowAutoResize) {
                $(".GridViewPagerStyle TABLE:eq(0) TR:eq(0)", $this).prepend('<td style="vertical-align:middle; color:#000;"></td>').find("TD:eq(0)").html('Page<select id="GridViewPageCount" name="GridViewPageCount"><option value="30">30</option><option value="50">50</option><option value="100">100</option></select>&nbsp;');
            }
            $(".GridViewPagerStyle TABLE:eq(0)", $this).css({
                "float": "left"
            });
            if (opt.showSummary) {
                $(".GridViewPagerStyle TD:eq(0)", $this).append('<table style=" float:right; color:#000;"><td class="GridViewInfoSummary">Avg:NaN&nbsp;&nbsp;&nbsp;&nbsp;Count:NaN&nbsp;&nbsp;&nbsp;&nbsp;Sum:NaN&nbsp;&nbsp;</td><td style="width;10px; position:relative;"><img class="GridViewResizableFlag" alt="" src="" style="position:absolute; right:0; bottom:0; cursor:nw-resize; display:' + (opt.isResizable ? "block" : "none") + ' " /></td></table>');
            }
            //根据GridView的行数，确定第几项要被选中
            $(".GridViewPagerStyle #GridViewPageCount", $this).val($this.attr("PageSize"));
            //没有数据时，不继续
            //必须放在这里，因为上面是在做框架
            if ($(".GridViewRowStyle, .GridViewAlternatingRowStyle", $this).size() == 0) {
                return false;
            }
            //功能1：鼠标经过行时变色
            $(".GridViewRowStyle, .GridViewAlternatingRowStyle", $this).hover(function () {
                if ($(this).attr("rowSelected") == undefined || !$(this).attr("rowSelected")) {
                    $(this).addClass("GridViewRowOverStyle");
                }
            }, function () {
                $(this).removeClass("GridViewRowOverStyle");
            }).click(function () {
                startIndex = $(this).index();
                //alert(startIndex);
                //再次选中，则取消
                if ($(this).attr("rowSelected") == undefined) {
                    //人为的设置一个Sel属性，用于记录下当前选中的行
                    if (!opt.allowRowMultiSel) {
                        $("TR[rowSelected]", $(this).parent()).removeClass("GridViewRowSelectedStyle").removeAttr("rowSelected");
                    }
                    $(this).attr({
                        rowSelected: true
                    }).addClass("GridViewRowSelectedStyle");
                } else {
                    $(this).removeClass("GridViewRowSelectedStyle").removeAttr("rowSelected");
                }
            }).dblclick(function () {
                //双击只选中不取消
                if ($(this).attr("rowSelected") == undefined) {
                    //人为的设置一个Sel属性，用于记录下当前选中的行
                    if (!opt.allowRowMultiSel) {
                        $("TR[rowSelected]", $(this).parent()).removeClass("GridViewRowSelectedStyle").removeAttr("rowSelected");
                    }
                    $(this).attr({
                        rowSelected: true
                    }).addClass("GridViewRowSelectedStyle");
                } 
            });
            //end 功能1
            //功能4：表头固定
            if (opt.isHeaderFixed) {
                //创建Top
                $this.parent().before('<table class="GridViewStyle" cellspacing="0" border="1"></table>');
                $("TABLE:eq(0)", $this.parent().parent()).css({
                    position: "absolute",
                    width: $this.width() + 2 + "px",
                    top: 0,
                    left: 0,
                    "z-index": 2,
                    "border-collapse": "collapse"
                });
                //创建Foot
                $this.parent().after('<table class="GridViewStyle" cellspacing="0" border="0"></table>');
                $("TABLE:last", $this.parent().parent()).css({
                    position: "absolute",
                    width: $(this).width(),
                    bottom: 0,
                    left: 0,
                    "z-index": 2
                });
                //1、复制GridView的头栏到Top中
                $(".GridViewHeaderStyle", $this).clone().appendTo($("TABLE:eq(0)", $this.parent().parent()));
                //因为存在垂直滚动条，故，新增一个td用于中和滚动条多出的部分
                //如果单独依据GridViewHeaderStyle的宽度来设置的话，可能回错行！这是因为，数据行里面可能存在撑破或缩小的情况
                $("TABLE:eq(0) TR TH, TABLE:eq(0) TR TD", $this.parent().parent()).each(function () {
                    var ind4 = $(this).parent().find("TH, TD").index($(this));
                    $(this).width($(".GridViewRowStyle:eq(0) TD:eq(" + ind4 + ")", $this).width() + "px");
                });
                //2、移动Pager到Foot浮动层上 
                $(".GridViewPagerStyle", $this).appendTo($("TABLE:last", $this.parent().parent()));
                //3、加一些空行，同时去掉GridView的Height属性，否则在行数较少的时候，出现拉伸
                $this.after('<div style="width:' + $this.width() + "px; height:" + 50 + 'px; border-left: 1px solid #b8d2f0; border-right: 1px solid #b8d2f0; "></div>');
            }
            //功能2：鼠标经过列时变色，
            $(".GridViewHeaderStyle:first", $this.parent().parent()).on("mouseover", "TH", function () {
                if ($(this).attr("colSelected") == undefined || !$(this).attr("colSelected")) {
                    var index = $(this).parent().find("TH, TD").index($(this));
                    $(".GridViewHeaderStyle:last", $this).nextAll().not(".GridViewPagerStyle").each(function () {
                        $("TD:eq(" + index + ")", $(this)).addClass("GridViewColOverStyle");
                    });
                }
            }).on("mouseout", "TH", function () {
                var index = $(this).parent().find("TH, TD").index($(this));
                $(".GridViewHeaderStyle:last", $this).nextAll().not(".GridViewPagerStyle").each(function () {
                    $("TD:eq(" + index + ")", $(this)).removeClass("GridViewColOverStyle");
                });
            }).on("click", "TH", function () {
                //再次选中时，取消，所以加个“|| $(this).attr("colSelected" != undefined”
                var _flag = $(this).attr("colSelected") != undefined;
                if (!opt.allowColMultiSel || _flag) {
                    //选中的列，样式要去掉
                    $("TH[colSelected], TD[colSelected]", $(this).parent()).each(function () {
                        $(this).removeAttr("colSelected");
                        var ind1 = $(this).parent().find("TH, TD").index($(this));
                        $(".GridViewHeaderStyle:last", $this).nextAll().not(".GridViewPagerStyle").each(function () {
                            $("TD:eq(" + ind1 + ")", $(this)).removeClass("GridViewColSelectedStyle");
                        });
                    });
                    //再次选中时，重置统计信息
                    if (_flag && opt.showSummary) {
                        $(".GridViewPagerStyle .GridViewInfoSummary", $this.parent().parent()).html("Avg:NaN&nbsp;&nbsp;&nbsp;&nbsp;Count:NaN&nbsp;&nbsp;&nbsp;&nbsp;Sum:NaN&nbsp;&nbsp;");
                        return false;
                    }
                }
                var ind2 = $(this).parent().find("TH, TD").index($(this));
                $(this).attr({
                    colSelected: true
                });
                $(".GridViewHeaderStyle:last", $this).nextAll().not(".GridViewPagerStyle").each(function () {
                    $("TD:eq(" + ind2 + ")", $(this)).addClass("GridViewColSelectedStyle");
                });
                //汇总信息
                if (opt.showSummary) {
                    var _avg = 0;
                    var _count = 0;
                    var _sum = 0;
                    $("TH[colSelected], TD[colSelected]", $(this).parent()).each(function () {
                        var ind3 = $(this).parent().find("TH, TD").index($(this));
                        $(".GridViewHeaderStyle:last", $this).nextAll().not(".GridViewPagerStyle").each(function () {
                            _count += 1;
                            var val = parseFloat($("TD:eq(" + ind3 + ")", $(this)).text());
                            _sum += isNaN(val) ? 0 : val;
                        });
                    });
                    //保留2位小数
                    _sum = Math.round(_sum * 100) / 100;
                    _avg = Math.round(100 * _sum / _count) / 100;
                    $(".GridViewPagerStyle .GridViewInfoSummary", $this.parent().parent()).html("Avg:" + _avg + "&nbsp;&nbsp;&nbsp;&nbsp;Count:" + _count + "&nbsp;&nbsp;&nbsp;&nbsp;Sum:" + _sum + "&nbsp;&nbsp;");
                }
            });
            //end 功能2
            //功能3：改变大小
            var x = 0;
            var y = 0;
            var h = 0;
            var flag = false;
            $(".GridViewResizableFlag", $this.parent().parent()).mousedown(function (e) {
                //按下鼠标，记下坐标
                x = e.clientX;
                y = e.clientY;
                h = $this.parent().height();
                flag = true;
            }).mousemove(function (e) {
                //移动鼠标，改变大小
                if (flag) {
                    $this.parent().height(h + (e.clientY - y) + "px");
                }
            }).mouseup(function (e) {
                flag = false;
            }).mouseout(function (e) {
                flag = false;
            });
            $("BODY").mouseup(function (e) {
                flag = false;
            });
        });
    };
    //end fn.GridView
    /*
    右键contextPopup插件（COPY）
    2014-08-22
    */
    $.fn.contextPopup = function (menuData) {
        // Define default settings
        var settings = {
            contextMenuClass: "contextMenuPlugin",
            gutterLineClass: "gutterLine",
            headerClass: "header",
            seperatorClass: "divider",
            title: "",
            items: []
        };
        // merge them
        $.extend(settings, menuData);
        // Build popup menu HTML
        function createMenu(e) {
            var menu = $('<ul class="' + settings.contextMenuClass + '"><div class="' + settings.gutterLineClass + '"></div></ul>').appendTo(document.body);
            if (settings.title) {
                $('<li class="' + settings.headerClass + '"></li>').text(settings.title).appendTo(menu);
            }
            settings.items.forEach(function (item) {
                if (item) {
                    var rowCode = '<li><a href="#"><span></span></a></li>';
                    // if(item.icon)
                    //   rowCode += '<img>';
                    // rowCode +=  '<span></span></a></li>';
                    var row = $(rowCode).appendTo(menu);
                    if (item.icon) {
                        var icon = $("<img>");
                        icon.attr("src", item.icon);
                        icon.insertBefore(row.find("span"));
                    }
                    row.find("span").text(item.label);
                    if (item.action) {
                        row.find("a").click(function () {
                            item.action(e);
                        });
                    }
                } else {
                    $('<li class="' + settings.seperatorClass + '"></li>').appendTo(menu);
                }
            });
            menu.find("." + settings.headerClass).text(settings.title);
            return menu;
        }
        // On contextmenu event (right click)
        this.bind("contextmenu", function (e) {
            var menu = createMenu(e).show();
            var left = e.pageX + 5, /* nudge to the right, so the pointer is covering the title */
            top = e.pageY;
            if (top + menu.height() >= $(window).height()) {
                top -= menu.height();
            }
            if (left + menu.width() >= $(window).width()) {
                left -= menu.width();
            }
            // Create and show menu
            menu.css({
                zIndex: 1000001,
                left: left,
                top: top
            }).bind("contextmenu", function () {
                return false;
            });
            // Cover rest of page with invisible div that when clicked will cancel the popup.
            var bg = $("<div></div>").css({
                left: 0,
                top: 0,
                width: "100%",
                height: "100%",
                position: "absolute",
                zIndex: 1e6
            }).appendTo(document.body).bind("contextmenu click", function () {
                // If click or right click anywhere else on page: remove clean up.
                bg.remove();
                menu.remove();
                return false;
            });
            // When clicking on a link in menu: clean up (in addition to handlers on link already)
            menu.find("a").click(function () {
                bg.remove();
                menu.remove();
            });
            // Cancel event, so real browser popup doesn't appear.
            return false;
        });
        return this;
    };
})(jQuery);