<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_GanntMstr.aspx.cs" Inherits="TSK_GanntMstr" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/Gannt.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            $(".TaskGannt tbody tr td").hover(
                function () {

                    if (!$(this).hasClass("GanntEmpty")) {
                        if (!$(this).hasClass("GanntSelected")) {
                            $(this).css("background-color", "#E1FCCE");
                        }
                    }
                }
                , function () {
                    if (!$(this).hasClass("GanntEmpty")) {
                        if (!$(this).hasClass("GanntSelected")) {
                            $(this).css("background-color", "#fff");
                        }
                    }
                }).click(function () {
                    if (!$(this).hasClass("GanntEmpty")) {
                        if ($(this).hasClass("GanntSelected")) {
                            $(this).removeClass("GanntSelected");
                        }
                        else {
                            $(".TaskGannt .GanntSelected").removeClass("GanntSelected").css("background-color", "#fff");
                            $(this).addClass("GanntSelected");
                        }
                    }
                })
            //end hover
            $("#spanShowDetail").click(function () {
                var _src = "../IT/TSK_ChargerDay.aspx";
                $.window("未结任务统计", 900, 550, _src);
            })
            //end click
            $("#spanShowTimeSum").click(function () {
                var _year = $("#dropYear").val();
                var _month = $("#dropMonth").val();
                var _src = "../IT/TSK_ChargerTime.aspx?year=" + _year + "&month=" + _month;
                $.window("任务时长统计", 900, 550, _src);
            })
            //end click

            $(".TaskDesc").hover(
                function () {
                    $(this).css("text-decoration", "underline");
                },
                function () {
                    $(this).css("text-decoration", "none");
                })
            .click(function () {
                var _year = $("#dropYear").val();
                var _month = $("#dropMonth").val();
                var _type = $("#dropType").val();

                var _src = "../IT/TSK_GanntDetail.aspx?year=" + _year + "&month=" + _month + "&id=" + $(this).attr("flag") + "&type=" + _type;
                $.window("任务处理进度", 1300, 600, _src);
            })
            //end click
            //显示被截断的文本
            $(".TaskGannt tbody tr td span").hover(function (e) {
                var x = e.pageX;
                var y = e.pageY;
                var div = $("<div id='TaskGanntTextShow'></div>");
                var text = $(this).html();
                $(this).append(div);
                $("#TaskGanntTextShow").html("<br />" + text + "<br />&nbsp;").css({ "width": "300px", "position": "absolute", "z-index": "99"
                                                        , "background-color": "silver", "white-space": "normal", "word-wrap": "break-word"
                                                        , "padding-left": "15px"
                                                        , "padding-right": "15px", "top": y + 10 + "px", "left": x + 10 + "px"
                });
                $("this").children("#TaskGanntTextShow").show();
            }, function () {
                $(this).children("#TaskGanntTextShow").remove();
            })


        })
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="dropYear" runat="server" Width="70px">
        </asp:DropDownList>
        年<asp:DropDownList ID="dropMonth" runat="server" Width="50px" AutoPostBack="True"
            OnSelectedIndexChanged="dropMonth_SelectedIndexChanged">
        </asp:DropDownList>
        月&nbsp; 
        <asp:DropDownList ID="dropType" runat="server" Width="50px" AutoPostBack="True"
            OnSelectedIndexChanged="dropType_SelectedIndexChanged">
            <asp:ListItem Value="Devp">开发</asp:ListItem>
            <asp:ListItem Value="Tracking">跟踪</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp; 责任人：<asp:DropDownList ID="dropUsers" runat="server" Width="100px" DataTextField="userName" AutoPostBack="True"
                        DataValueField="userID" OnSelectedIndexChanged="dropUsers_SelectedIndexChanged" >
                    </asp:DropDownList>
        &nbsp; &nbsp;<span id="spanShowDetail" style="cursor: pointer;"><u>未结任务统计</u></span>
        &nbsp; &nbsp;<span id="spanShowTimeSum" style="cursor: pointer;"><u>任务时长统计</u></span>
        &nbsp; &nbsp;<asp:LinkButton ID="LinkShowAllTasks" runat="server" 
            ForeColor="#000000" onclick="LinkShowAllTasks_Click"><u>任务统计表</u></asp:LinkButton>
        <div id="divGannt" runat="server">
        </div>
        <div style="width: 100%; height: 15px;">
            <ul style="list-style-type: none;">
                <li style="float: left; border: 1px solid #000; padding: 5px; margin: 5px;">A 开发</li>
                <li style="float: left; border: 1px solid #000; padding: 5px; margin: 5px;">B 测试</li>
                <li style="float: left; border: 1px solid #000; padding: 5px; margin: 5px;">C LOG</li>
                <li style="float: left; border: 1px solid #000; padding: 5px; margin: 5px;">D 更新</li>
                <li style="float: left; border: 1px solid #000; padding: 5px; margin: 5px;">E 跟踪</li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
