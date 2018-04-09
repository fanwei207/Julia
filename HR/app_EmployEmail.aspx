<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_EmployEmail.aspx.cs" Inherits="HR_app_EmployEmail" %>

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
        <tr>
            <td>内容</td>
            <td>
                <div style=" Width:650px; Height:390px; border:solid 1px Gray; font-size:14.5px;">
                    <asp:Label ID="labname" runat="server" Text="Label" Font-Size="14.5px"></asp:Label>
                    <asp:Label ID="labch" runat="server" Text="Label" Font-Size="14.5px"></asp:Label>:<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    您好:<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    首先感谢您对强凌集团的信任和大力支持，我们非常荣幸的通知您：
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <ul>
                        <li>您的职位为<asp:TextBox ID="txtAppProc" runat="server"  Width="100" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>
                        ，直接汇报上级为<asp:TextBox ID="txtLeadership" runat="server"  Width="100" CssClass="SmallTextBo" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox></li>
                        <li>试用期为<asp:TextBox ID="txtAppDate" runat="server"  Width="100" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>
                        试用期薪资为<asp:TextBox ID="txtProMoney" runat="server"  Width="100" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>
                    </ul>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    请您带好（新员工报到须知）所列证明文件，于<asp:TextBox ID="txtArrYear" runat="server"  Width="50" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>年
                    <asp:TextBox ID="txtArrMonth" runat="server"  Width="50" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>月
                    <asp:TextBox ID="txtArrDay" runat="server"  Width="50" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>日上午前<br />
                    来我公司人力资源部门办理报到手续。如果资料齐全，我们将与您签订为期<asp:TextBox ID="txtAppYear" runat="server"  Width="50" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid">三</asp:TextBox>年的劳动<br />
                    合同/保密协议。<br /><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    相信您的加盟会给强凌集团带来新的活力，我们将一起努力，创造公司的未来，也愿您<br />
                    在强凌集团迎来辉煌的明天。<br />
                    <p style=" margin-left:450px;">强凌集团人力资源中心<br />
                    <asp:TextBox ID="txtEndYear" runat="server"  Width="30" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>年
                    <asp:TextBox ID="txtEndMonth" runat="server"  Width="30" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>月
                    <asp:TextBox ID="txtEndDay" runat="server"  Width="30" CssClass="SmallTextBox" style="text-align:center; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: solid"></asp:TextBox>日</p>
                    <p style=" color:Red;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;如果您对本录用通知函及附件中所列各事项确认无误并表示接受，请于3个工作日内回复此邮件，如果逾期公司未能收到您的回复或在面试和录用过程中提供的资料经调查与实际不符，本录用通知函将自动失效。</p>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnPost" runat="server" Text="发送"  CssClass="SmallButton2" onclick="btnPost_Click" 
                     />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="返回"  CssClass="SmallButton2" onclick="btnBack_Click" 
                     />
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
