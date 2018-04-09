<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_Devp.aspx.cs" Inherits="IT_TSK_Devp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
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
                .dblclick(function () {

                    var _src = "../IT/TSK_TaskList.aspx";
                    $.window("任务明细", 1000, 600, _src);
                })
            //end hover




        })

            

            function btnshow_onclick() {
                  var _src = "../IT/TSK_ChargerDay.aspx";
                  $.window("任务", 500, 300, _src);
            }
           
    </script>
    <style type="text/css">
        .TaskGannt
        {
            width: 1400px;
            border: 1px solid #000;
            font-size: 12px;
        }
        
        .TaskGannt thead tr
        {
            background-color: #d2e0f0;
            color: #000;
        }
        .TaskGannt thead tr td
        {
            width: 200px;
            text-align: center;
            height: 30px;
            border-left: 1px dotted #000;
        }
        
        .TaskGannt tbody tr td
        {
            height: 90px;
            text-align: center;
            vertical-align: top;
            border-top: 1px dotted #000;
            border-left: 1px dotted #000;
        }
 
        .TaskGannt tbody tr td div
        {
            width:100%;
            line-height:30px;
            position:relative;
        }
        
         .TaskGannt tbody tr td div div
        {
            float:right;
            width:5px;
            height:5px;
            background-color:Red;
        }

        .TaskGannt tbody tr td span
        {
            text-align:left;
            margin-left:5px;
            width:200px;
            float:left;
        }
        
        .TaskGannt tbody tr .GanntEmpty
        {
            background-color: #fff;
        }
        
        .TaskGannt tbody tr .GanntSelected
        {
            background-color: #D0FBB1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="dropYear" runat="server" Width="70px">
        </asp:DropDownList>
        年<asp:DropDownList ID="dropMonth" runat="server" Width="50px" 
            AutoPostBack="True" onselectedindexchanged="dropMonth_SelectedIndexChanged">
        </asp:DropDownList>
        月<input id="btnshow" type="button" value="show"  onclick="return btnshow_onclick()" />
        <div id="divGannt" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
