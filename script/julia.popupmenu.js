$(function () {
    $("body").mouseup(function (e) {
        var _clientX = e.clientX;
        var _clientY = e.clientY + 5;
        var _menuStyle = {
            position: "absolute",
            left: _clientX + "px",
            top: _clientY + "px",
            "list-style": "none",
            padding: "1px",
            margin: "0px",
            "background-color": "#fff",
            border: "1px solid #999",
            width: "100px"
        };
        var _itemStyle = {
            margin: "0px",
            color: "#000",
            display: "block",
            cursor: "default",
            padding: "3px",
            "border-top": "1px solid #999",
            "background-color": "transparent"
        };
        var _itemHoverStyle = {
            "background-color": "#b6bdd2"
        };
        var _itemOutStyle = {
            "background-color": "transparent"
        };
        var _itemList = [{
            href: "../Mail/Check/mail_checkWo.aspx",
            icon: "../images/icons/information.png",
            text: "销售单检查"
        }, {
            href: "../Mail/Check/mail_checkSo.aspx",
            icon: "../images/icons/information.png",
            text: "加工单检查"
        }, {
            href: "../Mail/Check/mail_checkPo.aspx",
            icon: "../images/icons/information.png",
            text: "采购单检查"
        }];
        var _selText = window.getSelection();
        if (_selText != "") {
            //已存在的，移动；否则，就创建
            if ($("#j-popmenu").size() > 0) {
                $("#j-popmenu").css(_menuStyle);
            } else {
                var _popMenu = $("<ul id='j-popmenu' value=''><div></div></ul>");
                _popMenu.css(_menuStyle);
                _popMenu.attr("value", _selText);
                $.each(_itemList, function (i, obj) {
                    var _popItem = $("<li class='j-popmenu-item'><span class='j-popmenu-link' href='" + obj.href + "'><img class='j-popmenu-icon' src='" + obj.icon + "' /><span class='j-popmenu-text' style='width:100%;'>" + obj.text + "</span></span></li>");
                    _popItem.find(".j-popmenu-icon").css("vertical-align", "bottom");
                    _popItem.find(".j-popmenu-text").css("margin-left", "5px");
                    _popItem.css(_itemStyle);
                    _popItem.click(function () {
                        var _href = $(this).find(".j-popmenu-link").attr("href");
                        var _text = $(this).find(".j-popmenu-text").text();
                        var _value = $(this).parent().attr("value");
                        _href = _href + "?param=" + _value;
                        if (typeof _href != "undefined" && _href != "") {
                            $.window(_text, 800, 600, _href);
                        }
                        event.stopPropagation();
                    });
                    _popItem.mouseup(function (e) {
                        event.stopPropagation();
                    });
                    _popItem.hover(function () {
                        $(this).css(_itemHoverStyle);
                    }, function () {
                        $(this).css(_itemOutStyle);
                    });
                    _popMenu.append(_popItem);
                });
                $("body").append(_popMenu);
            }
        } else {
            $("#j-popmenu").remove();
        }
    });
});