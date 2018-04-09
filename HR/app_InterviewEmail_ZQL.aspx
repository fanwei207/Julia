<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_InterviewEmail_ZQL.aspx.cs" Inherits="HR_app_InterviewEmail_ZQL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                    公司名称：镇江强凌电子有限公司<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    http://www.tcpi.com 或 http://www.tcp-china.com<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    面试地址：江苏省镇江市学府路200号（江苏大学对面）<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    面试时间：<asp:TextBox ID="txtAppDate" runat="server" Width="100" CssClass="SmallTextBox Date" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtAppTime" runat="server"  Width="100" CssClass="SmallTextBox" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    附近交通：（建议先通过百度地图查找适合您的路线）<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    公交汽车：D1、3路、19路、D208路、60路、28路、29路、K201路等（到汝山站下往前300米，过红绿灯路口即到）<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    如有任何疑问您可以通过下列联系方式咨询我，祝您工作生活皆愉快！<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    如果不能前来请务必提前邮件或者电话通知我们取消面试或者另行安排面试时间，感谢您的配合！<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    -------------------------------------------------------------------------------------------------------------------------------------<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    公司：TCP强凌集团-人力资源中心-
                    <asp:TextBox ID="txtName" runat="server"  Width="50" CssClass="SmallTextBox" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    地址：<asp:TextBox ID="txtAddress" runat="server"  Width="350" CssClass="SmallTextBox" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    座机：<asp:TextBox ID="txtPhone" runat="server"  Width="100" CssClass="SmallTextBox" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox><br />
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

