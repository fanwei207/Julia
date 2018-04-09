<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mail_oprating.aspx.cs" Inherits="Mail_mail_oprating" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="./Notify/notifications.css" rel="stylesheet" />
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" src="./Notify/angular.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="./Notify/desktop-notify-min.js" type="text/javascript"></script>
    <script language="JavaScript" src="./Notify/desktop-notify.js" type="text/javascript"></script>
    <script language="JavaScript" src="js/Notification.js" type="text/javascript"></script>

    <script language="JavaScript" type="text/javascript">
        $(function () {
            $("#ifrmDetail").width($(window).width() - 470);
            $("#ifrmDetail").height($("#ifrmContainer").height() - 10);
            $(".linkDetail").click(function () {
                var _mailID = $(this).next().val();
                var _index = $(this).parent().parent().index() + 1;
                $("#ifrmDetail").attr("src", "mail_detail.aspx?tr=" + _index + "&m_UID=" + _mailID);
            });
            var interval;
            $("#chkFresh").click(function () {
                if ($(this).prop("checked") == true) {
                    interval = setInterval($.clickRecieve, 1000 * 30);
                }
                else {
                    window.clearInterval(interval);
                }
            });
            $("#btnNewMail").click(function () {
                var _src = "../Mail/mail_NewMail.aspx";
                $.window("写邮件", 850, 570, _src);
            });
            $("#btnGet").hide();
            CheckAndShowMail();
            function CheckAndShowMail() {
                $.ajax({
                    type: "POST",
                    url: "Ajax/Mail.ashx",
                    dataType: "JSON",
                    //  async: true,
                    data: { isDisposed: $("#dropStatus").val(), bdate: $("#txtBegin").val(), edate: $("#txtEnd").val(), uId: "<% =uId %>", bodyPath: "<% =bodyPath %>", attachPath: "<% =attPath %>" },
                    success: function (response) {
                        var data = response;
                        var trCss;
                        var reBody;
                        var table = document.getElementById("gvMail");
                        var mId = $("#gvMail>tbody:first").find("#gvMail_ctl01_hidDetail").val(); //获取gvMail第一行的MailID
                        var gvLen = $("#gvMail>tbody").children().length;   //获取gvMail的行数
                        var newMidFirst = "";
                        if (data.length > 1) {
                            newMidFirst = data[1].m_uid;
                        }
                        if (mId == newMidFirst && gvLen == (data.length - 1))//判断新获取的邮件是否和当前页面的相同，相同的话直接返回
                        {
                            // $("body", parent.parent.document).find("#divLoading").hide();
                            $.loading("none");
                            return false;
                        }
                        if (table.hasChildNodes()) {
                            table.innerHTML = "";
                        }
                        tbody = document.createElement("tbody");
                        var gvTr;
                        var gvTd;
                        var inlineTable;
                        var inlineTr;
                        var inlineTd;
                        var notes;
                        var span;
                        var mail;
                        var inlineTbody;
                        if (data.length > 0) {
                            var mailNum = data[0];
                            //  alert(mailNum.m_newMailNum);
                            $("#newMailNum").val(mailNum.m_newMailNum + "");
                            if (parseInt(mailNum.m_newMailNum) > 0) {
                                show("您好！您有" + mailNum.m_newMailNum + "封新邮件！");
                            }
                            for (var i = 1; i < data.length; i++) {
                                if (i % 2 == 0) {
                                    trCss = "GridViewRowStyle";
                                }
                                else {
                                    trCss = "GridViewAlternatingRowStyle";
                                }
                                var disposeId;
                                var hidID;
                                if (i < 10) {
                                    hidID = "gvMail_ctl0" + i + "_hidDetail";
                                    disposeId = "gvMail_ctl0" + i + "_lblDisposed";
                                }
                                else {
                                    hidID = "gvMail_ctl" + i + "_hidDetail";
                                    disposeId = "gvMail_ctl" + i + "_lblDisposed";
                                }
                                var click1 = "$(\"body\", parent.parent.document).find(\"#divLoading\").show();$(\"#" + disposeId + "\").removeAttr(\"style\");if($(\"#" + disposeId + "\").text()!=\"已回复\")$(\"#" + disposeId + "\").text(\"已读\"); var mail2=$(\"#" + hidID + "\").val();var _index = $(this).parent().parent().index() + 1;"
                                click1 += "$(\"#ifrmDetail\").attr(\"src\", \"mail_detail.aspx?tr=\" + _index + \"&m_UID=\" + mail2);";
                                mail = data[i];
                                gvTr = document.createElement("tr");
                                gvTr.setAttribute("class", trCss);
                                gvTr.setAttribute("onmouseover", "if($(this).attr(\"style\")==undefined)$(this).attr(\"style\",\"background-color:RGB(225,252,206);\");");
                                gvTr.setAttribute("onmouseout", "var color=$(this).css(\"background-color\");$(\"#tt\").val(color);if(color==\"rgb(225, 252, 206)\"){$(this).removeAttr(\"style\");}");
                                gvTr.setAttribute("onclick", click1 + "var $child=$(this).parent().children();$child.each(function(){if($(this).css(\"background-color\")==\"rgb(208, 251, 177)\") $(this).removeAttr(\"style\");});$(this).attr(\"style\",\"background-color:RGB(208,251,177);\");");
                                gvTd = document.createElement("td");
                                gvTd.setAttribute("align", "left");
                                gvTd.setAttribute("style", "width:390px;");
                                inlineTable = document.createElement("table");
                                inlineTbody = document.createElement("tbody");
                                inlineTable.setAttribute("cellpadding", "0");
                                inlineTable.setAttribute("cellspacing", "0");
                                inlineTable.setAttribute("style", "width: 100%; height: 100%; border: 0;font-size: 12px;");

                                inlineTr = document.createElement("tr");
                                inlineTd = document.createElement("td");
                                inlineTd.setAttribute("style", "width: 80%; border: 0; padding-bottom: 2px;");
                                notes = document.createTextNode(mail.m_fromName.replace("<b>", "").replace("</b>", ""));
                                inlineTd.appendChild(notes);
                                inlineTr.appendChild(inlineTd);

                                inlineTd = document.createElement("td");
                                inlineTd.setAttribute("style", "border: 0;");
                                //     notes = document.createTextNode(mail.m_receiveDate.substring(10));
                                var mDate = new Date(mail.m_receiveDate);
                                notes = document.createTextNode(getMDate(mail.m_receiveDate));
                                inlineTd.appendChild(notes);
                                inlineTr.appendChild(inlineTd);

                                inlineTd = document.createElement("td");
                                inlineTd.setAttribute("style", "border: 0;");
                                span = document.createElement("span");
                                span.setAttribute("id", disposeId);
                                if (mail.m_isDisposed.replace("<b>", "").replace("</b>", "") == "未读") {
                                    span.setAttribute("style", "color:blue;font-size:14px;font-weight:bold;");
                                }
                                notes = document.createTextNode(mail.m_isDisposed.replace("<b>", "").replace("</b>", ""));
                                span.appendChild(notes);
                                inlineTd.appendChild(span);
                                inlineTr.appendChild(inlineTd);

                                inlineTbody.appendChild(inlineTr);

                                inlineTr = document.createElement("tr");
                                inlineTd = document.createElement("td");
                                inlineTd.setAttribute("style", "width: 10%; border: 0;");
                                notes = document.createTextNode(mail.m_subject);
                                inlineTd.appendChild(notes);
                                inlineTr.appendChild(inlineTd);

                                inlineTd = document.createElement("td");
                                inlineTd.setAttribute("style", "border: 0;");
                                notes = document.createTextNode(getTime(mDate));
                                inlineTd.appendChild(notes);
                                inlineTr.appendChild(inlineTd);

                                inlineTbody.appendChild(inlineTr);
                                inlineTable.appendChild(inlineTbody);
                                gvTd.appendChild(inlineTable);
                                //                                gvTr.appendChild(gvTd);

                                //                                gvTd = document.createElement("td");
                                //                                gvTd.setAttribute("align", "center");
                                //                                gvTd.setAttribute("style", "width:60px;");
                                //                                var a = document.createElement("a");
                                //                                a.setAttribute("id", "link" + i);
                                //                                a.setAttribute("style", "color:blue;");
                                //                                a.setAttribute("href", "javascript:void(0);");
                                //                                var click = "$(\"body\", parent.parent.document).find(\"#divLoading\").show();$(\"#link" + i + "\").removeAttr(\"style\");$(\"#" + disposeId + "\").removeAttr(\"style\");$(\"#" + disposeId + "\").text(\"已读\");var _mailID = $(this).next().val(); var mail2=$(\"#" + hidID + "\").val();var _index = $(this).parent().parent().index() + 1;"
                                //                                click += "$(\"#ifrmDetail\").attr(\"src\", \"mail_detail.aspx?tr=\" + _index + \"&m_UID=\" + mail2);";
                                // alert(click);
                                //                                a.setAttribute("onclick", click);
                                //                                notes = document.createTextNode("查看");
                                //                                a.appendChild(notes);
                                //                                gvTd.appendChild(a);
                                var input = document.createElement("input");
                                input.setAttribute("type", "hidden");
                                input.setAttribute("id", hidID);
                                input.setAttribute("value", mail.m_uid);
                                gvTd.appendChild(input);
                                gvTr.appendChild(gvTd);
                                tbody.appendChild(gvTr);
                                table.appendChild(tbody);
                            }
                        }
                        // $("body", parent.parent.document).find("#divLoading").hide();
                        $.loading("none");
                    },
                    error: function (XMLHttpRequest, Error, F) {
                        $.loading("none");
                        //  $("body", parent.parent.document).find("#divLoading").hide();
                    }
                });
            }
            $("#btnGet").click(function () {
                CheckAndShowMail();
            });
            $("#btnReceive").click(function () {
                // $("body", parent.parent.document).find("#divLoading").show();
                $.loading("block");
                $("#newMailNum").val("0");
                CheckAndShowMail();
                if ($("#newMailNum").val() == "0") {
                    alert("没有新的邮件可收取！");
                }
                else {
                    $("#newMailNum").val("0");
                }

            });
            $("#btnSearch").click(function () {
                //  $("body", parent.parent.document).find("#divLoading").show();
                $.loading("block");
                CheckAndShowMail();
                //$("body", parent.parent.document).find("#divLoading").hide();
            });
            $.extend({
                clickRecieve: function () {
                    CheckAndShowMail();
                }
            });
            function getMDate(time) {
                var mDate = new Date(time);
                var mm = mDate.getMonth() + 1;
                var dd = mDate.getDate();
                if (mm < 10) {
                    mm = "0" + mm.toString();
                }
                if (dd < 10) {
                    dd = "0" + dd;
                }
                return mm + "-" + dd
            }
            function getTime(time) {
                var mDate = new Date(time);
                var hh = mDate.getHours();
                var mm = mDate.getMinutes();
                var ss = mDate.getSeconds();
                if (hh < 10) {
                    hh = "0" + hh;
                }
                if (mm < 10) {
                    mm = "0" + mm;
                }
                if (ss < 10) {
                    ss = "0" + ss;
                }
                return hh + ":" + mm + ":" + ss;
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
        <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%; border: 0;
            font-size: 12px;">
            <tr style="height: 1px;">
                <td style="width: 460px; text-align: right;">
                <input type="button" id="btnNewMail" class="SmallButton3" value="写邮件" style="margin-right:50px;"/>
                    <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="SmallButton3" 
                        style="margin-right:50px;" onclick="btnDelete_Click"/>
                    <input type="button" id="btnReceive" class="SmallButton3" value="接收"/>
                    <input type="hidden" id="newMailNum"  value="0"/>
                </td>
                <td id="ifrmContainer" rowspan="3">
                    <iframe id="ifrmDetail" frameborder="0" framespacing="0" marginheight="0" marginwidth="0"
                        style="overflow-y: auto; margin-left:0px; padding-left:0px; overflow-x: hidden;" runat="server"></iframe>
                </td>
            </tr>
            <tr style="height: 10px;">
                <td style="text-align: left; vertical-align: top;">
                    接收时间:<asp:TextBox ID="txtBegin" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    --<asp:TextBox ID="txtEnd" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    &nbsp;<asp:DropDownList ID="dropStatus" runat="server">
                        <asp:ListItem Value="0">未回复</asp:ListItem>
                        <asp:ListItem Value="1">未读</asp:ListItem>
                        <asp:ListItem Value="2">已读</asp:ListItem>
                    </asp:DropDownList>
                <input type="button" id="btnSearch" class="SmallButton3" value="查询"/>
               <asp:Label ID="lblDisposed" runat="server" Text="0" Visible="false"></asp:Label>
               <input type="checkbox" id="chkFresh"/>
                <label for="chkFresh">自动</label>
                </td>
            </tr>
            <tr>
            <td>
         <table width="450px" class="GridViewStyle" style="width:450px;border-collapse:collapse;">
         <tr class="GridViewHeaderStyle">
         <td align="center" style="width:200px">
         收件人
         </td>
         <td>
         &nbsp;
         </td>
         </tr>
         </table>
         <div style="width: 467px; height: 450px; overflow: auto; top: 0px; left: 0px; z-index: 1; text-align: left;">
        <table class="GridViewStyle " cellspacing="0" rules="all"  border="1" id="gvMail" style="width:450px;border-collapse:collapse;">
            </table>
            </div>
        </td>
            </tr>
        </table>
        </div>
        <input type="button" id="btnGet" value="测试"/>
    </form>
     <div class="warningMsg" data-ng-show="!isSupported" style="display: none;">
        Desktop notifications are currently not supported for your browser.
        Open the page in Chrome(version 23+), Safari(version6+), Firefox(with ff-html5notifications plugin installed) and IE9+.
    </div>
</body>
</html>
