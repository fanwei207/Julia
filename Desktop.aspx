<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Desktop.aspx.cs" Inherits="Desktop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <link media="all" href="css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/highcharts.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            <% Response.Write(ReportScript); %>

            $(".icon, .newIcon, .noneIcon").on("click", function () {

                var id = $(this).attr("id");
                var text = $(this).text();
                var src = $(this).attr("href").replace("#", "");
                var helpDoc = $(this).attr("doc");

                if($(this).hasClass("newIcon")){
                    text = "任务列表";
                }

                if($(this).hasClass("noneIcon")){
                    id = $(this).attr("menuId");
                    text = $(this).attr("menuName");
                }

                if ($(parent.document).find("#ulTabs #tab_" + id).size() > 0) {
                    alert('Another page has already been opened!');
                    return false;
                }
                else if ($(parent.document).find("#ulTabs li").length == 6) {
                    alert('Only 6 tabs can be displayed at one time!');
                    return false;
                }

                $.loading("block");

                //先加标签，再加IFrame
                $(parent.document).find("#ulTabs").append("<li id='tab_" + id + "' class='tab_selected' doc='" + helpDoc + "'><a href='#' title='" + text + "'><span style='font-size:12px; color:#416aa3;white-space:nowrap; position:relative;'>&nbsp;&nbsp;" + text + "<strong id='close_" + id + "'>&nbsp;&nbsp;x&nbsp;</strong></span></a></li>");
                $(parent.document).find("#ifrmHolder").append("<iframe id=\"ifrm_" + id + "\" frameborder=\"0\" style=\"width: 100%;\" src='" + src + "'></iframe>");

                $(parent.document).find("#tab_desktop").removeClass("tab_selected");
                $(parent.document).find("#ifrm_desktop").hide();

                $(parent.document).find("#ifrm_" + id).load(function () {

                    $.loading("none");

                    $(this).height($(parent.document).find("#ifrmHolder").height());
                })
                //end load

                //显示帮助
                //alert(helpDoc);
                if (helpDoc != '' && helpDoc != ' ') {

                    $(parent.document).find("#isHelp").css("display", "block").attr("href", helpDoc);
                }
                else {

                    $(parent.document).find("#isHelp").css("display", "none");
                }

                /*
                关闭标签
                        
                1、销毁tab、iFrame
                2、前一个tab处于选中状态
                */
                $(parent.document).find("#ulTabs li #close_" + id).click(function () {

                    var curr_li = $(parent.document).find("#" + $(this).attr("id").replace("close_", "tab_"));
                    var curr_fr = $(parent.document).find("#" + $(this).attr("id").replace("close_", "ifrm_"));

                    //如果要关闭和当前正在显示的不一致，则显示当前，否则显示前一个
                    if ($(parent.document).find("iFrame:visible").attr("id") == curr_fr.attr("id")) {

                        curr_li.prev().addClass("tab_selected");
                        curr_fr.prev().show();

                        $.loading("none");
                    }
                    //销毁当前的iFrame
                    curr_li.remove();
                    curr_fr.remove();
                })
                //end click

                /*
                标签：单击
                */
                $(parent.document).find("#ulTabs li").click(function () {

                    $(parent.document).find("#ulTabs li").removeClass("tab_selected");
                    $(this).addClass("tab_selected");

                    /*
                    1、隐藏所有的iFrame
                    2、显示当前的，tab_[id] => ifrm_[id]
                    */
                    $(parent.document).find("iFrame").hide();
                    $(parent.document).find("#" + $(this).attr("id").replace("tab_", "ifrm_")).show();
                })
                //end click

            })
            // end dbclick


        });
            
    </script>
</head>
<body style="cursor: default;">
    <form id="form1" method="post" runat="server">
    <div id="divContainer" style="left: 0px; top: 0px;">
        <% Response.Write(BuildHomePageScripts()); %>
    </div>
    <div id="divReporter" style="border-top: 1px solid #dddddd; border-left: 1px solid #dddddd;
        border-right: 1px solid #dddddd; border-bottom: 2px solid #dddddd; height: 220px;
        width: 450px; position: absolute; right: 2px; top: 10px; font-family: 微软雅黑;">
        <div style="width: 450px; text-align: center; border-top: 1px solid #dddddd;">
            <h4 style="margin: 0;">
                通知</h4>
        </div>
        <div style="width: 450px; margin: auto;">
            请相关人员注意了!
        </div>
        <div style="width: 450px; margin: auto; text-align: right;">
            信息部 2013-12-31
        </div>
    </div>
    <div id="divReporter2" style="border-top: 1px solid #dddddd; border-left: 1px solid #dddddd;
        border-right: 1px solid #dddddd; border-bottom: 2px solid #dddddd; height: 300px;
        width: 450px; position: absolute; right: 2px; bottom: 0; font-family: 微软雅黑;">
        <div style="width: 450px; text-align: center; border-top: 1px solid #dddddd; display:none;">
            <h4 style="margin: 0;">
                通知</h4>
        </div>
        <div style="width: 450px; margin: auto;">
            <% Response.Write(ReportScript2); %>
        </div>
<%--        <div style="width: 450px; margin: auto; text-align: right;">
            <a class="noneIcon" href="#/rmInspection/conn_list.aspx" menuId="81500210">更多&gt;&gt;</a>
        </div>--%>
    </div>
    </form>
</body>
</html>
