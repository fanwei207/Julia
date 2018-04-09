<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_ResumeEmail.aspx.cs" Inherits="HR_app_ResumeEmail" %>

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
        <asp:Label ID="labCompany" runat="server" Text="公司"></asp:Label>
        <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
        <asp:Label ID="labDepartment" runat="server" Text="部门"></asp:Label>
        <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
        <asp:Label ID="labprocess" runat="server" Text="岗位"></asp:Label>
        <asp:TextBox ID="txtProcess" runat="server"></asp:TextBox>
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
            <td>抄送至：</td>
            <td><asp:TextBox ID="txtEmailTo1" runat="server" Width="650"></asp:TextBox></td>
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
                <div style=" Width:650px; Height:380px; border:solid 1px Gray; font-size:14.5px;" > 
                    <asp:TextBox ID="txtEmaliContent" runat="server"  Width="648px" Height="200px"   TextMode="MultiLine"
                        style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none">请删除此内容后再编辑邮件内容</asp:TextBox>
                    <asp:TextBox ID="txtText" runat="server"  Width="649px" Height="25px" 
                        style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"></asp:TextBox>
                    <div id="divUserInfo" runat="server">
                    </div>
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

