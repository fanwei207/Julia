﻿!function (a) { a.fn.Menu = function (b) { function d(a) { a = a || window.event, a.stopPropagation ? a.stopPropagation() : a.cancelBubble = !0 } var e, c = { width: "100px" }; return c = a.extend(c, b), e = !0, a(this).css({ width: c.width }), a("li").css({ width: c.width }), a("li:has(ul)", this).css({ position: "relative", left: "0px" }), a("li:has(ul)", this).find("ul").css({ position: "absolute", width: c.width, left: c.width }), a("li ul", this).hide(), this.each(function () { var b = a("li:has(ul)", this); b.hover(function () { var f, b = a(this), d = b.find("ul:eq(0)"), e = 0; return d.height() < 10 ? !1 : (f = a(window).height() - 3, f < b.offset().top + d.height() && (e = f - (b.offset().top + d.height()) - 5), d.css("top", e + "px"), d.is(":animated") || d.css({ left: c.width }).slideDown("normal"), void 0) }, function () { var b = a(this).find("ul:eq(0)"); b.slideUp("normal") }), a("li", this).click(function (b) { var c, f, g; if (e && null != a("a", this).attr("href") && "#" != a("a", this).attr("href") && "#null" != a("a", this).attr("href")) { if ("block" == a("#j-objec-loding").css("display")) return e = !1, alert("另一个窗口正在打开中，请稍后..."), d(b), !1; if (a("#ulTabs li").length > 6) return e = !1, alert("最多只能同时打开6个标签!"), d(b), !1; c = a("a", this), f = c.attr("id"), g = c.attr("doc"), a("#ulTabs li").removeClass("tab_selected"), a("iFrame").hide(), 0 == a("#tab_" + f).length ? (a.loading("block"), a("#ulTabs").append("<li id='tab_" + f + "' class='tab_selected' doc='" + g + "'><a href='#' title='" + a(this).text() + "'><span style='font-size:12px; color:#416aa3;white-space:nowrap; position:relative;'>&nbsp;&nbsp;" + a(this).text() + "<strong id='close_" + f + "'>&nbsp;&nbsp;x&nbsp;</strong></span></a></li>"), a("#ifrmHolder").append('<iframe id="ifrm_' + f + '" frameborder="0" style="width: 100%;" src="' + c.attr("href").replace("#", "") + "?HeightPixel=" + a("#HeightPixel").val() + '"></iframe>'), a("#ifrm_" + f).load(function () { a.loading("none"), a("#ifrm_" + f).height(a("#ifrmHolder").height()) }), "" != g && " " != g ? a("#isHelp").css("display", "block").attr("href", g) : a("#isHelp").css("display", "none")) : a("#tab_" + f).addClass("tab_selected"), a("#ifrm_" + f).show(), a("#ulTabs li #close_" + f).click(function () { var b = a("#" + a(this).attr("id").replace("close_", "tab_")), c = a("#" + a(this).attr("id").replace("close_", "ifrm_")); a("iFrame:visible").attr("id") == c.attr("id") && (b.prev().addClass("tab_selected"), c.prev().show(), a.loading("none")), b.remove(), c.remove() }), a("#ulTabs li").click(function () { return a("#ulTabs li").removeClass("tab_selected"), a(this).addClass("tab_selected"), a("iFrame").hide(), a("#" + a(this).attr("id").replace("tab_", "ifrm_")).show(), "" != a(this).attr("doc").replace(" ", "") ? a("#isHelp").css("display", "block").attr("href", a(this).attr("doc")) : a("#isHelp").css("display", "none"), !1 }) } return e = !1, d(b), !1 }).hover(function () { e = !0 }, function () { e = !0 }) }), this } } (jQuery);