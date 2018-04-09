﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_InterviewEmail.aspx.cs" Inherits="HR_app_InterviewEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:20px;">
    <table>
        <tr>
            <td>发件人：</td>
            <td><asp:TextBox ID="txtEmailFrom" runat="server" Width="650"></asp:TextBox></td>
        </tr>
        <tr>
            <td>收件人：</td>
            <td><asp:TextBox ID="txtEmailTo" runat="server" Width="650"></asp:TextBox></td>
        </tr>
        <tr>
            <td>主题：</td>
            <td><asp:TextBox ID="txtTopical" runat="server" Width="650"></asp:TextBox></td>
        </tr>
        <%--<tr>
            <td>内容：</td>
            <td><asp:TextBox ID="txtContent" runat="server" Width="510" Height="300" CssClass="SmallTextBox" TextMode="MultiLine"></asp:TextBox></td>
        </tr>--%>
        <tr>
            <td>内容</td>
            <td>
                <div style=" Width:650px; Height:360px; border:solid 1px Gray; font-size:14.5px;">
                    <asp:Label ID="labname" runat="server" Text="Label" Font-Size="14.5px"></asp:Label>
                    <asp:Label ID="labch" runat="server" Text="Label" Font-Size="14.5px"></asp:Label>，您好:<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    恭喜您在简历筛选中通过，即将进入初试，欢迎您来我公司面试。<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    公司名称：上海强凌电子有限公司<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    http://www.tcpi.com 或 http://www.tcp-china.com<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    面试地址：上海市松江区泗泾镇望东南路139号<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    面试时间：<asp:TextBox ID="txtAppDate" runat="server" Width="100" CssClass="SmallTextBox Date" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtAppTime" runat="server"  Width="100" CssClass="SmallTextBox" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    附近交通：（建议先通过百度地图查找适合您的路线）<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    （1）轨道9号线在泗泾站3号口出站，向左走，过马路（书报亭附近）转乘坐46路车到莘潮家居站（望东南路口）下车。 或者直接乘坐出租车，大概17元左右即可到达。<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    （2）乘沪松线公交车到叶星站下，沿沪松公路向回走到望东南路口，再沿望东南路走到139号。或者直接乘坐出租车，大概17元左右即可到达。<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    如有任何疑问您可以通过下列联系方式咨询我，祝您工作生活皆愉快！<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    如果不能前来请务必提前邮件或者电话通知我们取消面试或者另行安排面试时间，感谢您的配合！<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    -------------------------------------------------------------------------------------------------------------------------------------<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    公司：TCP强凌集团-人力资源中心-张女士<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    地址：上海市松江区泗泾镇望东南路139号<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    座机：021-67613241<br />
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnPost" runat="server" Text="发送"  CssClass="SmallButton2" 
                    onclick="btnPost_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="返回"  CssClass="SmallButton2" 
                    onclick="btnBack_Click" />
            </td>
        </tr>
    </table>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="true"></asp:Literal>
    </script>
    </form>
</body>
</html>