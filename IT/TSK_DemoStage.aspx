<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_DemoStage.aspx.cs" Inherits="TSK_DemoStage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.obj.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function () {

            $("#demoList").hide().slideDown("slow");

            $("#demoList LI A").click(function () {
                var _id = $(this).attr("href");
                _id = _id.replace("#", "");

                $("#demoHolder").empty().append("Loading......");
                $.ajax({
                    type: "get",
                    url: "../Ajax/DemoHtml.ashx?detId=" + _id,
                    cache: false,
                    error: function () { alert('加载页面' + url + '时出错！'); },
                    success: function (msg) {
                        $("#demoHolder").empty().append(msg);
                        $(".j-demo-tabs").Tabs();
                        RegistClick();
                    }
                });
                //end ajax

            });
            //end click

            function RegistClick() {
                //转换#123：
                $("A[href^=##]").click(function () {
                    var _detId = $(this).attr("href").replace("##", "");

                    $("#demoHolder").empty().append("Loading......");
                    $.ajax({
                        type: "get",
                        url: "../Ajax/DemoHtml.ashx?detId=" + _detId,
                        cache: false,
                        error: function () { alert('加载页面' + url + '时出错！'); },
                        success: function (msg) {
                            $("#demoHolder").empty().append(msg);
                            RegistClick();
                        }
                    });
                    //end ajax
                });
                //end click
            }

        })
    </script>
    <style type="text/css">
        #demoList
        {
            list-style-type: none;
            margin: 0;
            padding: 0;
        }
        #demoList li
        {
            margin: 0 3px 3px 3px;
            padding: 0.4em;
            font-size: 1.4em;
            border: 1px solid #d3d3d3;
            background-color: #e6e6e6;
            font-weight: normal;
            color: #555555;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 550px;">
        <tr>
            <td style="width: 150px; vertical-align: top; text-align: center; border-right: 1px solid #000;">
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" /><br />
                <br />
                <ul id="demoList" runat="server">
                    
                </ul>
            </td>
            <td style="vertical-align: top;">
                <div id="demoHolder" style="width: 100%; height: 500px; background-color: #fff" runat="server">
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
