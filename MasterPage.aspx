<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterPage.aspx.cs" Inherits="MasterPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CHINA PORTAL -
        <%
            if (Session["PlantCode"] != null)
            {
                Response.Write(this.GetPlants(Session["PlantCode"].ToString()));
            }
            else
            {
                Response.Redirect("Login.aspx?error=sessionout");
            }
        %>
    </title>
    <link media="all" href="css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="script/julia.menu.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        if (window.top != window.self) {
            window.top.location.href = window.self.location.href;
        }

        $(function () {

            //菜单
            $("#divNav UL").Menu({ width: "200px" });

            //初始化菜单
            if ($("#divNavHolder").css("margin-left") == "-180px") {

                $("#spanTrigleMenu", $("#divNavHolder")).html("&nbsp;>>>&nbsp;");
                $("#divNav", $("#divNavHolder")).hide();
            }


        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="tableContainer" style="width: 100%; height: 100%; overflow: hidden; background-color: #e2edef;
        border-bottom: 1px solid #cccccc; left: 0px; top: 0px;" cellpadding="0" cellspacing="0">
        <tr id="trTop" style="position: relative;">
            <td id="top" style="height: 56px;">
                <div id="top_logo" style="height: 56px; width: 431px;">
                </div>
                <div id="top_slogan" style="height: 56px;">
                </div>
                <div id="top_right" style="width: 45%; height: 56px;">
                    <table cellpadding="0" cellspacing="0" style="color: White; font-family: 微软雅黑, Arial;
                        font-weight: bold; float: right; vertical-align: middle; padding-right: 10px;
                        height: 80%;">
                        <tr>
                            <td rowspan="2">
                                <img alt="" src="images/login_admin.gif" style="padding-top: 7px;" />
                            </td>
                            <td style="vertical-align: bottom;">
                                <% if (Session["PlantCode"] != null)
                                   {
                                       Response.Write(this.GetPlants(Session["PlantCode"].ToString()));
                                   }
                                   else
                                   {
                                       Response.Redirect("Login.aspx?error=sessionout");
                                   }
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=WECOLMELABLE %>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="linkSignOut" runat="server"
                                    ForeColor="White" OnClick="linkSignOut_Click"><%=LOGOUTLABLE%></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <input id="HeightPixel" name="HeightPixel" type="hidden" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <!-- 左侧导航-->
                <div id="divNavHolder" style="width: 200px; height: 92%; float: left; margin-top: 0px;
                    background-color: #f1faf7; border-bottom: 1px solid #c4ccce; margin-left: <% Response.Write(Session["showMenu"]);
                    %>; vertical-align: top; z-index: 100;">
                    <div id="LeftNav" style="height: 28px; position: relative;">
                        <img alt="" src="images/zhuye.png" style="display: inline; vertical-align: middle;
                            line-height: 25px;" /><b>&nbsp;Main Menu</b> <span id="spanTrigleMenu" title="显示|隐藏菜单"
                                style="position: absolute; right: 0px; cursor: pointer;">&nbsp;<<<&nbsp;
                        </span>
                    </div>
                    <div id="divNav" style="margin-top: 0px; height: 92%;">
                        <% Response.Write(GetMenus(Session["uID"].ToString(), string.Empty)); %>
                        <ul id="Tutorials">
                            <li><a href='#'>教程--Tutorials</a></li></ul>
                    </div>
                </div>
                <!-- 显示、隐藏按钮-->
                <div style="width: 3px; height: 500px; float: left; margin-top: 0px; display: block;">
                </div>
                <!-- 右侧显示-->
                <div id="divHolder" style="margin-left: 1px; margin-right: 1px; height: 92%; border-left: solid 1px #b3b3b3;
                    background-color: #fff; border-right: solid 1px #b3b3b3; border-bottom: solid 1px #b3b3b3;
                    border-top: solid 1px #b3b3b3; overflow: hidden; z-index: -1;">
                    <div id="RightNav" class="RightNav_blue">
                        <ul id="ulTabs">
                            <li id="tab_desktop" doc="/Help/destop.doc"><a href="#" title="我的桌面" style="width: 100px;">
                                <span style="font-size: 12px; color: #262626; white-space: nowrap;">Desktop</span></a></li>
                        </ul>
                        <a style="border: none; float: right; line-height: 15px; margin-top: 3px; margin-right: 5px;
                            display: none;" id="exportExcel" href="public/exportExcel.aspx" target="_blank">
                            <img alt="" style="border: none;" src="images/EXCEL.ico" /></a> <a style="border: none;
                                float: right; line-height: 15px; margin-top: 3px; margin-right: 5px; display: block;"
                                id="isHelp" href="/Help/destop.doc">
                                <img alt="" style="border: none;" src="images/help.png" /></a>
                    </div>
                    <div id="ifrmHolder" style="border-style: none; border-color: inherit; border-width: 0;
                        background-color: #ffffff; overflow-y: auto; overflow-x: hidden; position: relative;
                        height: 532px; z-index: 0; left: 0px;">
                        <iframe id="ifrm_desktop" frameborder="0" framespacing="0" marginheight="0" marginwidth="0"
                            style="width: 100%; height: 100%; overflow-y: auto; overflow-x: hidden;" src="">
                        </iframe>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divFooter" class="footer">
                    Copyright&copy;2005-<%=DateTime.Now.Year.ToString() %>
                    Technical Consumer Products (China), All Rights Reserved
                </div>
            </td>
        </tr>
    </table>
    <div id="TutorialsDiv" style="position: absolute; top: 0; left: 0; display: <% Response.Write(Session["showTutorials"]);
        %>; width: 100%; height: 100%; background-color: #000; border: 0; text-align: center;
        top: 0; opacity: 0.8; z-index: 100; filter: alpha(opacity=95);" sys="<% Response.Write(Session["showTutorials"]);%>">
        <div style="position: relative; width: 800px; height: 600px; margin: 10px auto 0 auto;
            line-height: 40px; background-color: #fff; border: 2px solid #ccc; color: #000;">
            <div id="GuideImg" style="width: 576px; height: 418px; margin-top: 70px; text-align: left;
                display: block;">
                <img alt="教程" style="width: 100%; height: 100%;" index="1" src="/images/Tutorials/1.jpg" />
            </div>
            <div style="width: 100%; height: 20px;">
                <input id="btnPrev" type="button" class="SmallButton2" value="Prev" style="width: 100px;"
                    disabled="disabled" />
                <input id="btnNext" type="button" class="SmallButton2" value="Next" style="width: 100px;" />
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript">

        var h_top = $("#trTop").height(); //页头高度
        var h_footer = $("#divFooter").height(); //页脚高度
        var h_nav = $("#LeftNav").height(); //导航条高度

        var h = $(window).height() - 3;


        $("#ifrmHolder").height(h - h_top - h_footer - h_nav);
        $("#ifrm_desktop").height(h - h_top - h_footer - h_nav - 3);
        $("#HeightPixel").val(h - h_top - h_footer - h_nav - 3);

        //写入Cookie
        $.cookie("HeightPixel", $("#HeightPixel").val(), 24);
        $.cookie("WidthPixel", $("#ifrm_desktop").width(), 24);

        $("#ifrm_desktop").attr("src", "Desktop.aspx?HeightPixel=" + $("#HeightPixel").val());
        $("#ifrm_desktop").load(function () {
            $.loading("none");
        })

        $("#spanTrigleMenu").click(function () {

            if ($("#divNavHolder").css("margin-left") == "9px") {

                $(this).html("&nbsp;>>>&nbsp;");
                $("#divNav", $("#divNavHolder")).slideUp("slow"
                    , function () {

                        $("#divNavHolder").animate({ "margin-left": "-180px", "left": "-180px" }
                                , { duration: 1000 }
                        ).css("margin-left", "-180px");
                    }
                );

                $.ajax({
                    type: "POST",
                    url: "/Ajax/CloseMenu.ashx",
                    data: "req=-180px",
                    success: function (msg) { }
                });
                //end ajax
            }
            else {

                $(this).html("&nbsp;<<<&nbsp;");
                $("#divNavHolder").animate({ "margin-left": "9px", "left": "9px" }
                    , function () {

                        $("#divNav", $("#divNavHolder")).slideDown("slow");
                    }
                ).css("margin-left", "9px");

                $.ajax({
                    type: "POST",
                    url: "/Ajax/CloseMenu.ashx",
                    data: "req=9px",
                    success: function (msg) { }
                });
                //end ajax
            }
        });
        //end click

        //教程
        $("#Tutorials LI A").click(function () {

            $("#TutorialsDiv").show();

            return false;
        });

        $("#btnNext").click(function () {
            var index = parseInt($("#GuideImg IMG").attr("index"));

            if (index == 7) {
                //只有当users中的显示时才Ajax，否则直接Hide即可
                $("#TutorialsDiv").hide();

                if ($("#TutorialsDiv").attr("sys") == "block") {

                    $.ajax({
                        type: "POST",
                        url: "/Ajax/CloseTutorials.ashx",
                        data: "req=none",
                        success: function (msg) { }
                    });
                    //end ajax
                }
                return false;
            }

            index = index == 7 ? 7 : (index + 1);
            $("#GuideImg IMG").attr({ src: "/images/Tutorials/" + index + ".jpg", index: index });
            $("#GuideImg").show();
            $("#btnPrev").removeAttr("disabled");

            if (index == 7) {
                $(this).val("结  束");
            }
        })

        $("#btnPrev").click(function () {

            var index = parseInt($("#GuideImg IMG").attr("index"));
            index = index == 1 ? 1 : (index - 1);
            $("#GuideImg IMG").attr({ src: "/images/Tutorials/" + index + ".jpg", index: index });
            $("#btnNext").val("下一页");

            if (index == 1) {
                $(this).attr("disabled", "disabled");
            }
        })

    </script>
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
