<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_JobSchedule.aspx.cs"
    Inherits="IT_IT_JobSchedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/Gannt.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .jc_schedule
        {
            width: 2420px;
            margin-top: 10px;
        }
        .jc_schedule .jc_yAxis
        {
            width: 20px;
        }
        .jc_schedule .jc_yAxis_up
        {
            width: 20px;
            border-right: 1px solid #000;
        }
        .jc_schedule .jc_yAxis_down
        {
            width: 20px;
            border-right: 1px solid #000;
        }
        .jc_schedule .jc_xAxis td
        {
            height: 10px;
            width: 200px;
            text-align: center;
            border-top: 1px solid #000;
            border-bottom: 1px solid #000;
            border-right: 1px solid #000;
        }
        .jc_schedule .jc_content_up td
        {
            height: 240px;
            width: 200px;
            vertical-align: bottom;
            padding-bottom: 5px;
        }
        .jc_schedule .jc_content_up td div
        {
            height: 240px;
            width: 100%;
            overflow-y: scroll;
            vertical-align: bottom;
        }
        .jc_schedule .jc_content_down td
        {
            height: 240px;
            width: 200px;
            vertical-align: top;
            padding-top: 5px;
        }
        .jc_schedule .jc_content_down td div
        {
            height: 240px;
            width: 100%;
            overflow-y: scroll;
        }
        .jc_schedule .JobName
        {
            line-height: 16px;
            text-align: left;
            margin-left: 2px;
            width: 200px;
            float: left;
            vertical-align: middle;
            cursor: pointer;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            padding-top: 5px;
        }
        .jc_schedule img
        {
            padding-right: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    &nbsp; 日期:<asp:TextBox ID="txtDate" runat="server" CssClass="Date"></asp:TextBox>
    <asp:CheckBox ID="chk" runat="server" Text="按执行时间显示" />
    （*勾选, 按计划时间显示;&nbsp; 未勾选, 按实际执行的时间显示）<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2"
        OnClick="btnQuery_Click" Text="Query" />
    <div style="width: 1000px; float: none;">
        <div style="width: 20px; float: left; height: 500px;">
            <table cellpadding="0" cellspacing="0" style="height: 100%;">
                <tr style="height: 260px;">
                    <td style="border-bottom: 1px solid #000; border-right: 1px solid #000;">
                        AM
                    </td>
                </tr>
                <tr>
                    <td style="border-right: 1px solid #000;">
                        PM
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 980px; overflow-x: scroll;">
            <table class="jc_schedule" cellpadding="0" cellspacing="0">
                <tr class="jc_content_up">
                    <% Response.Write(CONTENTUP); %>
                </tr>
                <tr class="jc_xAxis">
                    <td>
                        0
                    </td>
                    <td>
                        1
                    </td>
                    <td>
                        2
                    </td>
                    <td>
                        3
                    </td>
                    <td>
                        4
                    </td>
                    <td>
                        5
                    </td>
                    <td>
                        6
                    </td>
                    <td>
                        7
                    </td>
                    <td>
                        8
                    </td>
                    <td>
                        9
                    </td>
                    <td>
                        10
                    </td>
                    <td>
                        11
                    </td>
                    <td>
                        12
                    </td>
                </tr>
                <tr class="jc_content_down">
                    <% Response.Write(CONTENTDOWN); %>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
