/*
1、Js只负责生成Html
2、具体的功能则使用Jqueyr插件方式，进行功能设定，避免从后台绑定时失去功能
*/
var objDemo = new Object();

objDemo.TextBox = function () {
    var _obj = new Object();
    var _id = "wbox" + new Date().getTime();
    _obj = $("<span id='" + _id + "' class='j-demo-textbox j-demo-waitsaving'><strong id='" + _id + "_lable'>标签:</strong><input id='" + _id + "_text' type='text' style='background-color:#E1FCCE;' /><span class='close'>x</span></span>");
    _obj.css({ "cursor": "pointer", "position": "absolute" });
    _obj.find(".close").css({
        position: "absolute",
        top: "-12px",
        right: "0",
        display: "none"
    });
    _obj.find("input").css("width", "100px");
    return _obj;
};

// end TextBox
objDemo.Button = function () {
    var _obj = new Object();
    var _id = "btn" + new Date().getTime();
    _obj = $("<span id='" + _id + "' class='j-demo-button j-demo-waitsaving' style='line-height:25px;'><span id='" + _id + "_text' class='SmallButton3' style='width:60px;line-height:25px;'>button</span><span class='close'>x</span></span>");
    _obj.css({ "cursor": "pointer", "position": "absolute" });
    _obj.find(".close").css({
        position: "absolute",
        top: "-12px",
        right: "0",
        display: "none"
    });
    return _obj;
};

// end Button
objDemo.DropDownList = function () {
    var _obj = new Object();
    var _id = "drop" + new Date().getTime();
    _obj = $("<span id='" + _id + "' class='j-demo-dropdownlist j-demo-waitsaving'><strong id='" + _id + "_lable'>标签:</strong><select id='" + _id + "_select' style='background-color:#E1FCCE;'><option>value1</option><option>value2</option></select><span class='close'>x</span></span>");
    _obj.css({ "cursor": "pointer", "position": "absolute" });
    _obj.find(".close").css({
        position: "absolute",
        top: "-12px",
        right: "0",
        display: "none"
    });
    _obj.find("select").css("width", "70px");
    return _obj;
};

// end DropDownList
objDemo.CheckBox = function () {
    var _obj = new Object();
    var _id = "chk" + new Date().getTime();
    _obj = $("<span id='" + _id + "' class='j-demo-checkbox j-demo-waitsaving'><strong id='" + _id + "_lable'>标签:</strong><input id='" + _id + "_checkbox' type='checkbox' style='background-color:#E1FCCE;' /><span class='close'>x</span></span>");
    _obj.css({ "cursor": "pointer", "position": "absolute" });
    _obj.find(".close").css({
        position: "absolute",
        top: "-12px",
        right: "0",
        display: "none"
    });
    return _obj;
};

// end CheckBox
objDemo.RadioBox = function () {
    var _obj = new Object();
    var _id = "rad" + new Date().getTime();
    _obj = $("<span id='" + _id + "' class='j-demo-radiobox j-demo-waitsaving'><strong id='" + _id + "_lable'>标签:</strong><input id='" + _id + "_radiobox' type='radio' style='background-color:#E1FCCE;' /><span class='close'>x</span></span>");
    _obj.css({ "cursor": "pointer", "position": "absolute" });
    _obj.find(".close").css({
        position: "absolute",
        top: "-12px",
        right: "0",
        display: "none"
    });
    return _obj;
};

// end RadioBox
objDemo.FileUpload = function () {
    var _obj = new Object();
    var _id = "file" + new Date().getTime();
    _obj = $("<span id='" + _id + "' class='j-demo-fileupload j-demo-waitsaving'><strong id='" + _id + "_lable'>标签:</strong><input id='" + _id + "_file' type='file' style=' background-color:#E1FCCE;' /><span class='close'>x</span></span>");
    _obj.css({ "cursor": "pointer", "position": "absolute" });
    _obj.find(".close").css({
        position: "absolute",
        top: "-12px",
        right: "0",
        display: "none"
    });
    _obj.find("input").css("width", "400px");
    return _obj;
};

// end FileUpload
objDemo.Tabs = function () {
    var _obj = new Object();
    var _id = "tab" + new Date().getTime();
    _obj = $("<div id='" + _id + "' class='j-demo-tabs j-demo-waitsaving'><span class='close'>x</span></div>");
    _obj.css({
        border: "1px solid #b8d2f0",
        "text-align": "left",
        "position": "absolute"
    });
    var _objTabsNav = $("<table id='" + _id + "_nav' cellpadding='5' cellspacing='0'><tr></tr></table>");
    _objTabsNav.append("<td id='" + _id + "_nav_0' class='j-demo-tabitem j-demo-tabactive' menuId='0'>Tab1</td>");
    _objTabsNav.append("<td id='" + _id + "_nav_1' class='j-demo-tabitem' menuId='0'>Tab2</td>");
    _objTabsNav.find("td").css({
        width: "100px",
        border: "1px solid #b8d2f0",
        "text-align": "center",
        cursor: "pointer"
    });
    _obj.append(_objTabsNav);
    var _objTabsHolder = $("<div id='" + _id + "_holder' style='height: 100%; border-top: 1px solid #b8d2f0; border-left:0; border-right:0; border-bottom:0;'>这里的内容将通过Ajax获取</div>");
    _obj.append(_objTabsHolder);
    _obj.css("cursor", "pointer");
    _obj.find(".close").css({
        position: "absolute",
        top: "-12px",
        right: "0",
        display: "none"
    });
    return _obj;
};

// end Tabs
objDemo.TextArea = function () {
    var _obj = new Object();
    var _id = "txa" + new Date().getTime();
    _obj = $("<span id='" + _id + "' class='j-demo-textArea j-demo-waitsaving'><strong id='" + _id + "_lable'>标签:</strong><textarea id='" + _id + "_text' cols='20' rows='10' style='background-color:#E1FCCE;'></textarea><span class='close'>x</span></span>");
    _obj.css({ "cursor": "pointer", "position": "absolute"});
    _obj.find(".close").css({
        position: "absolute",
        top: "-12px",
        right: "0",
        display: "none"
    });
    _obj.find("strong").css("vertical-align", "top");
    _obj.find("textarea").css("width", "200px");
    _obj.find("textarea").css("height", "50px");
    return _obj;
};

// end TextArea
objDemo.GridView = function () {
    var _obj = new Object();
    var _id = "gv" + new Date().getTime();
    var rowCnt = 1;
    //由于通过lcr来判定列数，所以行数默认为1，便于编辑数据
    var colCnt = 8;
    _obj = $("<table id='" + _id + "' class='j-demo-gridview GridViewStyle j-demo-waitsaving'></table>");
    _obj.css({ "cursor": "pointer", "position": "absolute" });
    _obj.prop({
        cellspacing: "0",
        rules: "all",
        border: "1"
    });
    var _gridHeader = $("<tr class='GridViewHeaderStyle'></tr>");
    for (i = 0; i < colCnt; i++) {
        var _thHeader = $("<th id='" + _id + "_" + i + "' class='j-demo-gridviewcell' align='center' scope='col' style='width: 100px;'>ColName</th>");
        _gridHeader.append(_thHeader);
    }
    _obj.append(_gridHeader);
    for (row = 0; row < rowCnt; row++) {
        var _gridRow = $("<tr class='GridViewRowStyle'></tr>");
        for (col = 0; col < colCnt; col++) {
            var _tdRow = $("<td id=" + _id + "_" + row + "_" + col + " class='j-demo-gridviewcell' style='white-space:nowrap; text-align: center;'>Data</td>");
            _gridRow.append(_tdRow);
        }
        _obj.append(_gridRow);
    }
    var _gridPager = $("<tr class='GridViewPagerStyle'></tr>");
    var _tdPager = $("<td colspan='" + colCnt + "'><table border='0' style='width:100%;'><tr><td><span>1</span></td><td><a href='#'>2</a></td><td style='width:98%;'></td><td style='width:5px; text-align:right;'><span class='close' style='display:none;'>x</span></td></tr></table></td>");
    _gridPager.append(_tdPager);
    _obj.append(_gridPager);
    return _obj;
};

(function ($) {
    $.fn.TextBox = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            $(".close", $this).click(function () {
                $("#demoConfig").empty();
                $this.remove();
            });
            $this.hover(function () {
                $(".close", $this).show();
            }, function () {
                $(".close", $this).hide();
            }).click(function () {
                var _config = [{
                    target: $this.attr("id") + "_lable",
                    edit: false,
                    lable: "Name",
                    value: "TextBox"
                }, {
                    target: $this.attr("id") + "_lable",
                    edit: true,
                    lable: "Lable",
                    value: $("#" + $this.attr("id") + "_lable").text()
                }, {
                    target: $this.attr("id") + "_text",
                    edit: true,
                    lable: "Text",
                    value: $("#" + $this.attr("id") + "_text").val()
                }, {
                    target: $this.attr("id") + "_text",
                    edit: true,
                    lable: "Width",
                    value: $("#" + $this.attr("id") + "_text").width()
                }, {
                    target: $this.attr("id") + "_text",
                    edit: true,
                    lable: "TextMode",
                    value: $("#" + $this.attr("id")).find("input").size() == 1 ? "single" : "double"
                }, {
                    target: $this.attr("id") + "_text",
                    edit: true,
                    lable: "ReadOnly",
                    value: "false"
                }];
                $("#demoConfig").ConfigPanel({
                    sender: {
                        type: "TextBox",
                        id: $this.attr("id")
                    },
                    config: _config
                });
            }).draggable({
                containment: opt.containment,
                scroll: false,
                drag: function (event, ui) {
                    $(this).addClass("j-demo-waitsaving");
                }
            });
            return false;
        });
    };
    //end TextBox
    $.fn.DropDownList = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            $(".close", $this).click(function () {
                $("#demoConfig").empty();
                $this.remove();
            });
            $this.hover(function () {
                $(".close", $this).show();
            }, function () {
                $(".close", $this).hide();
            }).click(function () {
                var _selItems = "";
                $("select option", $this).each(function () {
                    _selItems += (_selItems == "" ? "" : ",") + $(this).val();
                });
                var _config = [{
                    target: $this.attr("id") + "_lable",
                    edit: false,
                    lable: "Name",
                    value: "DropDownList"
                }, {
                    target: $this.attr("id") + "_lable",
                    edit: true,
                    lable: "Lable",
                    value: $("#" + $this.attr("id") + "_lable").text()
                }, {
                    target: $this.attr("id") + "_select",
                    edit: true,
                    lable: "Width",
                    value: $("#" + $this.attr("id") + "_select").width()
                }, {
                    target: $this.attr("id") + "_select",
                    edit: true,
                    lable: "Items",
                    value: _selItems
                }];
                $("#demoConfig").ConfigPanel({
                    sender: {
                        type: "DropDownList",
                        id: $this.attr("id")
                    },
                    config: _config
                });
            }).draggable({
                containment: opt.containment,
                scroll: false,
                drag: function (event, ui) {
                    $(this).addClass("j-demo-waitsaving");
                }
            });
            return false;
        });
    };
    //end DropDownList
    $.fn.Button = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            $(".close", $this).click(function () {
                $("#demoConfig").empty();
                $this.remove();
            });
            $this.hover(function () {
                $(".close", $this).show();
            }, function () {
                $(".close", $this).hide();
            }).click(function () {
                var _config = [{
                    target: $this.attr("id") + "_lable",
                    edit: false,
                    lable: "Name",
                    value: "Button"
                }, {
                    target: $this.attr("id") + "_text",
                    edit: true,
                    lable: "Html",
                    value: $("#" + $this.attr("id") + "_text").html()
                }, {
                    target: $this.attr("id") + "_text",
                    edit: true,
                    lable: "Width",
                    value: $("#" + $this.attr("id") + "_text").width()
                }];
                $("#demoConfig").ConfigPanel({
                    sender: {
                        type: "Button",
                        id: $this.attr("id")
                    },
                    config: _config
                });
            }).draggable({
                containment: opt.containment,
                scroll: false,
                drag: function (event, ui) {
                    $(this).addClass("j-demo-waitsaving");
                }
            });
            return false;
        });
    };
    //end Button
    $.fn.CheckBox = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            $(".close", $this).click(function () {
                $("#demoConfig").empty();
                $this.remove();
            });
            $this.hover(function () {
                $(".close", $this).show();
            }, function () {
                $(".close", $this).hide();
            }).click(function () {
                var _config = [{
                    target: $this.attr("id") + "_lable",
                    edit: false,
                    lable: "Name",
                    value: "CheckBox"
                }, {
                    target: $this.attr("id") + "_lable",
                    edit: true,
                    lable: "Lable",
                    value: $("#" + $this.attr("id") + "_lable").text()
                }, {
                    target: $this.attr("id") + "_lable",
                    edit: true,
                    lable: "TextAlgin",
                    value: "left"
                }];
                $("#demoConfig").ConfigPanel({
                    sender: {
                        type: "CheckBox",
                        id: $this.attr("id")
                    },
                    config: _config
                });
            }).draggable({
                containment: opt.containment,
                scroll: false,
                drag: function (event, ui) {
                    $(this).addClass("j-demo-waitsaving");
                }
            });
            return false;
        });
    };
    //end CheckBox
    $.fn.RadioBox = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            $(".close", $this).click(function () {
                $("#demoConfig").empty();
                $this.remove();
            });
            $this.hover(function () {
                $(".close", $this).show();
            }, function () {
                $(".close", $this).hide();
            }).click(function () {
                var _config = [{
                    target: $this.attr("id") + "_lable",
                    edit: false,
                    lable: "Name",
                    value: "RadioBox"
                }, {
                    target: $this.attr("id") + "_lable",
                    edit: true,
                    lable: "Lable",
                    value: $("#" + $this.attr("id") + "_lable").text()
                }, {
                    target: $this.attr("id") + "_lable",
                    edit: true,
                    lable: "TextAlgin",
                    value: "left"
                }];
                $("#demoConfig").ConfigPanel({
                    sender: {
                        type: "RadioBox",
                        id: $this.attr("id")
                    },
                    config: _config
                });
            }).draggable({
                containment: opt.containment,
                scroll: false,
                drag: function (event, ui) {
                    $(this).addClass("j-demo-waitsaving");
                }
            });
            return false;
        });
    };
    //end RadioBox
    $.fn.FileUpload = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            $(".close", $this).click(function () {
                $("#demoConfig").empty();
                $this.remove();
            });
            $this.hover(function () {
                $(".close", $this).show();
            }, function () {
                $(".close", $this).hide();
            }).click(function () {
                var _config = [{
                    target: $this.attr("id") + "_lable",
                    edit: false,
                    lable: "Name",
                    value: "FileUpload"
                }, {
                    target: $this.attr("id") + "_lable",
                    edit: true,
                    lable: "Lable",
                    value: $("#" + $this.attr("id") + "_lable").text()
                }, {
                    target: $this.attr("id") + "_file",
                    edit: true,
                    lable: "Width",
                    value: $("#" + $this.attr("id") + "_file").width()
                }];
                $("#demoConfig").ConfigPanel({
                    sender: {
                        type: "FileUpload",
                        id: $this.attr("id")
                    },
                    config: _config
                });
            }).draggable({
                containment: opt.containment,
                scroll: false,
                drag: function (event, ui) {
                    $(this).addClass("j-demo-waitsaving");
                }
            });
            return false;
        });
    };
    //end FileUpload
    function TabItemClick(objItem, parentObj) {
        $(".j-demo-tabactive", $(parentObj)).removeClass("j-demo-tabactive");
        $(objItem).addClass("j-demo-tabactive");
        var _menuId = $(objItem).attr("menuid");
        //开发环境下，不调用
        if (window.location.pathname != "/IT/TSK_Demo.aspx") {
            if (parseInt(_menuId) > 0) {
                $("#" + $(parentObj).attr("id") + "_holder").empty().text("正在加载，请稍后......");
                $.ajax({
                    type: "get",
                    url: "../Ajax/DemoHtml.ashx?detId=" + _menuId,
                    cache: false,
                    error: function () {
                        alert("加载页面" + url + "时出错！");
                    },
                    success: function (msg) {
                        $("#" + $(parentObj).attr("id") + "_holder").empty().append(msg);
                    }
                });
            } else {
                $("#" + $(parentObj).attr("id") + "_holder").empty();
            }
        } else {
            var _config = [{
                target: $(objItem).attr("id"),
                edit: false,
                lable: "Name",
                value: "TabItem"
            }, {
                target: $(objItem).attr("id"),
                edit: false,
                lable: "Index",
                value: $(objItem).index()
            }, {
                target: $(objItem).attr("id"),
                edit: true,
                lable: "Lable",
                value: $(objItem).text()
            }, {
                target: $(objItem).attr("id"),
                edit: true,
                lable: "Width",
                value: $(objItem).width()
            }, {
                target: $(objItem).attr("id"),
                edit: true,
                lable: "MenuID",
                value: _menuId
            }];
            $("#demoConfig").ConfigPanel({
                sender: {
                    type: "TabItem",
                    id: $(parentObj).attr("id")
                },
                config: _config
            });
        }
        event.stopPropagation();
    }
    //end TabItemClick
    $.fn.Tabs = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            if (window.location.pathname == "/IT/TSK_Demo.aspx") {
                $(".close", $this).click(function () {
                    $("#demoConfig").empty();
                    $this.remove();
                });
                $this.hover(function () {
                    $(".close", $this).show();
                }, function () {
                    $(".close", $this).hide();
                }).click(function () {
                    var _config = [{
                        target: $this.attr("id") + "_lable",
                        edit: false,
                        lable: "Name",
                        value: "Tabs"
                    }, {
                        target: $this.attr("id") + "_nav",
                        edit: true,
                        lable: "Count",
                        value: $("#" + $this.attr("id") + "_nav").find("td").size()
                    }, {
                        target: $this.attr("id"),
                        edit: true,
                        lable: "Width",
                        value: $("#" + $this.attr("id")).width()
                    }, {
                        target: $this.attr("id"),
                        edit: true,
                        lable: "Height",
                        value: $("#" + $this.attr("id")).height()
                    }];
                    $("#demoConfig").ConfigPanel({
                        sender: {
                            type: "Tabs",
                            id: $this.attr("id")
                        },
                        config: _config
                    });
                }).draggable({
                    containment: opt.containment,
                    scroll: false,
                    drag: function (event, ui) {
                        $(this).addClass("j-demo-waitsaving");
                    }
                });
            }
            $(".j-demo-tabitem", $this).click(function () {
                TabItemClick($(this), $this);
            });
            return false;
        });
    };
    //end Tabs
    $.fn.TextArea = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            $(".close", $this).click(function () {
                $("#demoConfig").empty();
                $this.remove();
            });
            $this.hover(function () {
                $(".close", $this).show();
            }, function () {
                $(".close", $this).hide();
            }).click(function () {
                var _config = [{
                    target: $this.attr("id") + "_lable",
                    edit: false,
                    lable: "Name",
                    value: "TextArea"
                }, {
                    target: $this.attr("id") + "_lable",
                    edit: true,
                    lable: "Lable",
                    value: $("#" + $this.attr("id") + "_lable").text()
                }, {
                    target: $this.attr("id") + "_text",
                    edit: true,
                    lable: "Width",
                    value: $("#" + $this.attr("id") + "_text").width()
                }, {
                    target: $this.attr("id") + "_text",
                    edit: true,
                    lable: "Height",
                    value: $("#" + $this.attr("id") + "_text").height()
                }];
                $("#demoConfig").ConfigPanel({
                    sender: {
                        type: "TextArea",
                        id: $this.attr("id")
                    },
                    config: _config
                });
            }).draggable({
                containment: opt.containment,
                scroll: false,
                drag: function (event, ui) {
                    $(this).addClass("j-demo-waitsaving");
                }
            });
            return false;
        });
    };
    //end TextArea
    //提取GridView行单元格的单击事件，以便在Config的时候使用
    function GridViewRowClick(objRow, parentObj) {
        var _config = [{
            target: $(objRow).attr("id"),
            edit: false,
            lable: "Name",
            value: "GridViewRow"
        }, {
            target: $(objRow).attr("id"),
            edit: false,
            lable: "Cell",
            value: "(" + $(objRow).parent().index() + ", " + $(objRow).index() + ")"
        }, {
            target: $(objRow).attr("id"),
            edit: true,
            lable: "Html",
            value: $(objRow).html()
        }, {
            target: $(objRow).attr("id"),
            edit: true,
            lable: "Algin",
            value: $(objRow).css("text-align")
        }, {
            target: $(objRow).attr("id"),
            edit: true,
            lable: "vAlgin",
            value: $(objRow).css("vertical-align")
        }];
        $("#demoConfig").ConfigPanel({
            sender: {
                type: "GridViewRow",
                id: $(parentObj).attr("id")
            },
            config: _config
        });
        event.stopPropagation();
    }
    //end GridViewRowClick
    function GridViewHeaderClick(objHeader, parentObj) {
        var _config = [{
            target: $(objHeader).attr("id"),
            edit: false,
            lable: "Name",
            value: "GridViewHeader"
        }, {
            target: $(objHeader).attr("id"),
            edit: false,
            lable: "Cell",
            value: "(0, " + $(objHeader).index() + ")"
        }, {
            target: $(objHeader).attr("id"),
            edit: true,
            lable: "Html",
            value: $(objHeader).html()
        }, {
            target: $(objHeader).attr("id"),
            edit: true,
            lable: "Width",
            value: $(objHeader).width()
        }];
        $("#demoConfig").ConfigPanel({
            sender: {
                type: "GridViewHeader",
                id: $(parentObj).attr("id")
            },
            config: _config
        });
        event.stopPropagation();
    }
    //end GridViewHeaderClick
    $.fn.GridView = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
            var _gridHeader = $(".GridViewHeaderStyle", $this);
            _gridHeader.find("TH.j-demo-gridviewcell").click(function () {
                GridViewHeaderClick($(this), $this);
            });
            var _gridRow = $(".GridViewRowStyle", $this);
            _gridRow.find("TD.j-demo-gridviewcell").click(function () {
                GridViewRowClick($(this), $this);
            });
            $(".close", $this).click(function () {
                $("#demoConfig").empty();
                $this.remove();
            });
            $this.hover(function () {
                $(".close", $this).show();
            }, function () {
                $(".close", $this).hide();
            }).click(function () {
                var _config = [{
                    target: $this.attr("id") + "_lable",
                    edit: false,
                    lable: "Name",
                    value: "GridView"
                }, {
                    target: $this.attr("id"),
                    edit: false,
                    lable: "Width",
                    value: $this.width() + "px"
                }, {
                    target: $this.attr("id"),
                    edit: true,
                    lable: "Rows",
                    value: $(".GridViewRowStyle", $this).size()
                }, {
                    target: $this.attr("id"),
                    edit: true,
                    lable: "Columns",
                    value: $(".GridViewHeaderStyle TH", $this).size()
                }];
                $("#demoConfig").ConfigPanel({
                    sender: {
                        type: "GridView",
                        id: $this.attr("id")
                    },
                    config: _config
                });
            }).draggable({
                containment: opt.containment,
                scroll: false,
                drag: function (event, ui) {
                    $(this).addClass("j-demo-waitsaving");
                }
            });
        });
    };
    //end GridView
    /*
    ConfigPanel属性配置面板
    参数：[
    {target: '', edit: true, lable: 'Title', value: ''}, 
    {target: '', edit: false, lable: 'Width', value: ''}
    ]
    */
    $.fn.ConfigPanel = function (options) {
        var defaults = {
            sender: {
                type: "",
                id: ""
            },
            //事件来源的父级本体，不是本身
            config: [{
                target: "",
                edit: true,
                lable: "Title",
                value: ""
            }, {
                target: "",
                edit: false,
                lable: "Width",
                value: ""
            }]
        };
        var opt = $.extend(defaults, options);
        var _id = "config" + new Date().getTime();
        //设置属性函数集合
        function SetAttributeValue(parentId, targetId, attr, newObj) {
            var oldValue = newObj.attr("oldValue");
            if (oldValue != newObj.val()) {
                var _objParent = $("#" + parentId);
                var _objTarget = $("#" + targetId);
                if (attr == "Lable") {
                    if (newObj.val() == "") {
                        alert("Lable 不能留空！");
                        newObj.val(oldValue);
                        return false;
                    } else {
                        _objTarget.text(newObj.val());
                        newObj.attr("oldValue", newObj.val());
                        _objParent.addClass("j-demo-waitsaving");
                    }
                } else if (attr == "Width") {
                    if (isNaN(newObj.val())) {
                        alert("Width 必须是数字！");
                        newObj.val(oldValue);
                        return false;
                    } else if (parseInt(newObj.val()) < 1) {
                        alert("Width 必须是大于0的整数！");
                        newObj.val(oldValue);
                        return false;
                    } else {
                        _objTarget.width(newObj.val());
                        newObj.attr("oldValue", newObj.val());
                        _objParent.addClass("j-demo-waitsaving");
                    }
                } else if (attr == "Height") {
                    if (isNaN(newObj.val())) {
                        alert("Height 必须是数字！");
                        newObj.val(oldValue);
                        return false;
                    } else if (parseInt(newObj.val()) < 1) {
                        alert("Height 必须是大于0的整数！");
                        newObj.val(oldValue);
                        return false;
                    } else {
                        _objTarget.height(newObj.val());
                        newObj.attr("oldValue", newObj.val());
                        _objParent.addClass("j-demo-waitsaving");
                    }
                } else if (attr == "MenuID") {
                    if (isNaN(newObj.val())) {
                        alert("MenuID 必须是数字！");
                        newObj.val(oldValue);
                        return false;
                    } else if (parseInt(newObj.val()) < 0) {
                        alert("MenuID 必须是大于0的整数！");
                        newObj.val(oldValue);
                        return false;
                    } else {
                        _objTarget.attr("menuId", newObj.val());
                        newObj.attr("oldValue", newObj.val());
                        _objParent.addClass("j-demo-waitsaving");
                    }
                } else if (attr == "Text") {
                    _objTarget.val(newObj.val());
                    _objTarget.attr("value", newObj.val());
                    newObj.attr("oldValue", newObj.val());
                    _objParent.addClass("j-demo-waitsaving");
                } else if (attr == "Html") {
                    _objTarget.html(newObj.val());
                    newObj.attr("oldValue", newObj.val());
                    _objParent.addClass("j-demo-waitsaving");
                } else if (attr == "Items") {
                    if (newObj.val() == "") {
                        alert("Items 不能留空！");
                        newObj.val(oldValue);
                        return false;
                    } else {
                        var _selItems = newObj.val().split(",");
                        if (_selItems.length > 0) {
                            _objTarget.empty();
                            for (var i = 0; i < _selItems.length; i++) {
                                $("<option>" + _selItems[i] + "</option>").appendTo(_objTarget);
                            }
                            newObj.attr("oldValue", newObj.val());
                            _objParent.addClass("j-demo-waitsaving");
                        }
                    }
                }
            } else {
                return false;
            }
        }
        //end SetAttributeValue
        //设置属性函数集合
        function ChangeAttributeValue(parentId, targetId, attr, newObj) {
            var oldValue = newObj.attr("oldValue");
            if (oldValue != newObj.val()) {
                var _objParent = $("#" + parentId);
                var _objTarget = $("#" + targetId);
                if (attr == "Algin") {
                    _objTarget.css("text-align", newObj.val());
                    newObj.attr("oldValue", newObj.val());
                    _objParent.addClass("j-demo-waitsaving");
                } else if (attr == "vAlgin") {
                    _objTarget.css("vertical-align", newObj.val());
                    newObj.attr("oldValue", newObj.val());
                    _objParent.addClass("j-demo-waitsaving");
                } else if (attr == "TextMode") {
                    if (newObj.val() == "single") {
                        _objParent.find(".double_items").remove();
                    } else {
                        var _prevInput = _objParent.find("input");
                        var _doubleItems = $("<span class='double_items'><span>");
                        _doubleItems.text("-").append(_prevInput.clone());
                        _objParent.append(_doubleItems);
                    }
                    newObj.attr("oldValue", newObj.val());
                    _objParent.addClass("j-demo-waitsaving");
                } else if (attr == "ReadOnly") {
                    if (newObj.val() == "false") {
                        _objTarget.css("background-color", "#E1FCCE");
                    } else {
                        _objTarget.css("background-color", "Silver");
                    }
                    newObj.attr("oldValue", newObj.val());
                    _objParent.addClass("j-demo-waitsaving");
                } else if (attr == "TextAlgin") {
                    var _selectObj = _objParent.find("input");
                    var _close = _objParent.find(".close");
                    if (newObj.val() == "right") {
                        _objParent.empty().append(_selectObj).append(_objTarget).append(_close);
                    } else {
                        _objParent.empty().append(_objTarget).append(_selectObj).append(_close);
                    }
                    newObj.attr("oldValue", newObj.val());
                    _objParent.addClass("j-demo-waitsaving");
                }
            } else {
                return false;
            }
        }
        //end 
        return this.each(function () {
            var $this = $(this);
            $this.empty();
            $(".SelectedItem").removeClass("SelectedItem");
            var _panel = $("<table id='" + _id + "'><tr><td width='80px'>属性:</td><td width='120px'></td></tr></table>");
            _panel.prop({
                cellspacing: "0",
                rules: "all",
                border: "1",
                width: "100%"
            });
            $.each(opt.config, function (i, obj) {
                if (obj.edit) {
                    if (obj.lable == "Algin") {
                        _editObj = $("<tr><td>" + obj.lable + "</td><td><select style='background-color:#E1FCCE; width:120px;' sType='" + opt.sender.type + "' sId='" + opt.sender.id + "' target='" + obj.target + "' lable='" + obj.lable + "' oldValue='" + obj.value + "'><option>left</option><option>center</option><option>right</option></select></td></tr>");
                        _editObj.find("select").val(obj.value);
                        _editObj.find("select").change(function () {
                            ChangeAttributeValue($(this).attr("sId"), $(this).attr("target"), $(this).attr("lable"), $(this));
                        });
                    } else if (obj.lable == "vAlgin") {
                        _editObj = $("<tr><td>" + obj.lable + "</td><td><select style='background-color:#E1FCCE; width:120px;' sType='" + opt.sender.type + "' sId='" + opt.sender.id + "' target='" + obj.target + "' lable='" + obj.lable + "' oldValue='" + obj.value + "'><option>top</option><option>middle</option><option>bottom</option></select></td></tr>");
                        _editObj.find("select").val(obj.value);
                        _editObj.find("select").change(function () {
                            ChangeAttributeValue($(this).attr("sId"), $(this).attr("target"), $(this).attr("lable"), $(this));
                        });
                    } else if (obj.lable == "TextAlgin") {
                        _editObj = $("<tr><td>" + obj.lable + "</td><td><select style='background-color:#E1FCCE; width:120px;' sType='" + opt.sender.type + "' sId='" + opt.sender.id + "' target='" + obj.target + "' lable='" + obj.lable + "' oldValue='" + obj.value + "'><option>left</option><option>right</option></select></td></tr>");
                        _editObj.find("select").val(obj.value);
                        _editObj.find("select").change(function () {
                            ChangeAttributeValue($(this).attr("sId"), $(this).attr("target"), $(this).attr("lable"), $(this));
                        });
                    } else if (obj.lable == "TextMode") {
                        _editObj = $("<tr><td>" + obj.lable + "</td><td><select style='background-color:#E1FCCE; width:120px;' sType='" + opt.sender.type + "' sId='" + opt.sender.id + "' target='" + obj.target + "' lable='" + obj.lable + "' oldValue='" + obj.value + "'><option>single</option><option>double</option></select></td></tr>");
                        _editObj.find("select").val(obj.value);
                        _editObj.find("select").change(function () {
                            ChangeAttributeValue($(this).attr("sId"), $(this).attr("target"), $(this).attr("lable"), $(this));
                        });
                    } else if (obj.lable == "ReadOnly") {
                        _editObj = $("<tr><td>" + obj.lable + "</td><td><select style='background-color:#E1FCCE; width:120px;' sType='" + opt.sender.type + "' sId='" + opt.sender.id + "' target='" + obj.target + "' lable='" + obj.lable + "' oldValue='" + obj.value + "'><option>false</option><option>true</option></select></td></tr>");
                        _editObj.find("select").val(obj.value);
                        _editObj.find("select").change(function () {
                            ChangeAttributeValue($(this).attr("sId"), $(this).attr("target"), $(this).attr("lable"), $(this));
                        });
                    } else {
                        _editObj = $("<tr><td>" + obj.lable + "</td><td><input type='text' style='background-color:#E1FCCE; width:120px;' value='" + obj.value + "' sType='" + opt.sender.type + "' sId='" + opt.sender.id + "' target='" + obj.target + "' lable='" + obj.lable + "' oldValue='" + obj.value + "' /></td></tr>");
                        $("input", _editObj).blur(function () {
                            //有内部结构的，必须GridView之类的控件，需要特别指定父级，否则父级和目标保持一致
                            if ($(this).attr("sType") == "TextBox" || $(this).attr("sType") == "Button" || $(this).attr("sType") == "DropDownList" || $(this).attr("sType") == "CheckBox" || $(this).attr("sType") == "RadioBox" || $(this).attr("sType") == "GridViewHeader" || $(this).attr("sType") == "GridViewRow" || $(this).attr("sType") == "FileUpload" || $(this).attr("sType") == "TabItem" || $(this).attr("sType") == "TextArea") {
                                SetAttributeValue($(this).attr("target"), $(this).attr("target"), $(this).attr("lable"), $(this));
                            } else if ($(this).attr("sType") == "Tabs") {
                                if ($(this).val() != $(this).attr("oldValue")) {
                                    if ($(this).attr("lable") == "Width" || $(this).attr("lable") == "Height") {
                                        SetAttributeValue($(this).attr("target"), $(this).attr("target"), $(this).attr("lable"), $(this));
                                    } else if ($(this).attr("lable") == "Count") {
                                        if (isNaN($(this).val())) {
                                            alert("Count 必须是数字！");
                                            $(this).val($(this).attr("oldValue"));
                                        } else if (parseInt($(this).val()) < 1) {
                                            alert("Count 必须是大于0的整数！");
                                            $(this).val($(this).attr("oldValue"));
                                        } else {
                                            var _parentId = $(this).attr("sId");
                                            var _itemCount = $(".j-demo-tabitem", $("#" + _parentId)).size();
                                            if (parseInt($(this).val()) > _itemCount) {
                                                for (var i = 0; i < parseInt($(this).val()) - _itemCount; i++) {
                                                    var _newItem = $("<td id='" + _parentId + "_nav_" + (_itemCount + i) + "' class='j-demo-tabitem' menuId='0'>Tab</td>");
                                                    _newItem.css({
                                                        width: "100px",
                                                        border: "1px solid #b8d2f0",
                                                        "text-align": "center",
                                                        cursor: "pointer"
                                                    });
                                                    $(".j-demo-tabitem:last", $("#" + _parentId)).after(_newItem);
                                                    _newItem.click(function () {
                                                        TabItemClick($(this), $("#" + _parentId));
                                                    });
                                                }
                                                $(this).attr("oldValue", $(this).val());
                                            } else if (parseInt($(this).val()) < _itemCount) {
                                                for (var i = 0; i < _itemCount - parseInt($(this).val()); i++) {
                                                    $(".j-demo-tabitem:last", $("#" + _parentId)).remove();
                                                }
                                                $(this).attr("oldValue", $(this).val());
                                            }
                                        }
                                    }
                                }
                            } else if ($(this).attr("sType") == "GridView") {
                                if ($(this).attr("lable") == "Rows") {
                                    if ($(this).val() != $(this).attr("oldValue")) {
                                        if (isNaN($(this).val())) {
                                            alert("Rows 必须是数字！");
                                            $(this).val($(this).attr("oldValue"));
                                        } else if (parseInt($(this).val()) < 1) {
                                            alert("Rows 必须是大于0的整数！");
                                            $(this).val($(this).attr("oldValue"));
                                        } else {
                                            //和原行数比，多则从最后一行增加，少则从最后一行删除
                                            var _gvRows = $(".GridViewRowStyle", $("#" + $(this).attr("sId"))).size();
                                            var _newRows = parseInt($(this).val());
                                            if (_gvRows < _newRows) {
                                                for (var i = 0; i < _newRows - _gvRows; i++) {
                                                    var _gv = $("#" + $(this).attr("sId"));
                                                    var _lastRow = $(".GridViewRowStyle:last", _gv);
                                                    var _newRow = _lastRow.clone();
                                                    _newRow.find("TD").each(function () {
                                                        var _r = _gvRows + i;
                                                        var _c = $(this).index();
                                                        $(this).attr("id", $(this).attr("sId") + "_" + _r + "_" + _c);
                                                    }).click(function () {
                                                        GridViewRowClick($(this), _gv);
                                                    });
                                                    _lastRow.after(_newRow);
                                                }
                                                $(this).attr("oldValue", $(this).val());
                                            } else {
                                                for (var i = 0; i < _gvRows - _newRows; i++) {
                                                    var _lastTr = $(".GridViewRowStyle:last", $("#" + $(this).attr("sId")));
                                                    _lastTr.remove();
                                                }
                                                $(this).attr("oldValue", $(this).val());
                                            }
                                        }
                                    }
                                } else if ($(this).attr("lable") == "Columns") {
                                    if ($(this).val() != $(this).attr("oldValue")) {
                                        if (isNaN($(this).val())) {
                                            alert("Columns 必须是数字！");
                                            $(this).val($(this).attr("oldValue"));
                                        } else if (parseInt($(this).val()) < 2) {
                                            alert("Columns 最少2两列！");
                                            $(this).val($(this).attr("oldValue"));
                                        } else {
                                            var _gv = $("#" + $(this).attr("sId"));
                                            var _gvCols = $(".GridViewHeaderStyle TH", _gv).size();
                                            var _newCols = parseInt($(this).val());
                                            if (_gvCols < _newCols) {
                                                for (var i = 0; i < _newCols - _gvCols; i++) {
                                                    $(".GridViewHeaderStyle", _gv).each(function () {
                                                        var _newCell = $("<th class='j-demo-gridviewcell' id='" + _gv.attr("id") + "_" + (_gvCols + i) + "' align='center' scope='col' style='width: 100px;'>ColName</th>");
                                                        _newCell.click(function () {
                                                            GridViewHeaderClick($(this), _gv);
                                                        });
                                                        $(this).find("th:last").after(_newCell);
                                                    });
                                                    $(".GridViewRowStyle", _gv).each(function () {
                                                        var _newCell = $("<td class='j-demo-gridviewcell' id='" + _gv.attr("id") + "_" + $(this).index() + "_" + (_gvCols + i) + "' style='width: 100px; white-space:nowrap; text-align: center;'>Data</td>");
                                                        _newCell.click(function () {
                                                            GridViewRowClick($(this), _gv);
                                                        });
                                                        $(this).find("td:last").after(_newCell);
                                                    });
                                                }
                                                $(".GridViewPagerStyle TD:first", _gv).prop("colspan", _newCols);
                                                $(this).attr("oldValue", $(this).val());
                                            } else {
                                                for (var i = 0; i < _gvCols - _newCols; i++) {
                                                    var _lastTH = $(".GridViewHeaderStyle TH:last", _gv);
                                                    _lastTH.remove();
                                                    $(".GridViewRowStyle", _gv).each(function () {
                                                        var _lastTD = $(this).find("TD:last");
                                                        _lastTD.remove();
                                                    });
                                                }
                                                $(".GridViewPagerStyle TD:first", _gv).prop("colspan", _newCols);
                                                $(this).attr("oldValue", $(this).val());
                                            }
                                        }
                                    }
                                }
                            }
                        });
                    }
                    //end blur
                    _panel.append(_editObj);
                } else {
                    var _editObj = $("<tr><td>" + obj.lable + "</td><td>" + obj.value + "</td></tr>");
                    _panel.append(_editObj);
                }
            });
            $this.append(_panel);
        });
    };
    //end ConfigPanel
    //$.fn.Demo是一个模板
    $.fn.Demo = function (options) {
        var defaults = {
            containment: ""
        };
        var opt = $.extend(defaults, options);
        return this.each(function () {
            var $this = $(this);
        });
    };
})(jQuery);