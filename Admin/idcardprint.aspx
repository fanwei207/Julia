<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.IDCardPrint" CodeFile="IDCardPrint.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body  >
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="2" width="630" bgcolor="white" border="0">
            <tr bgcolor="#d4d0c8" height="25">
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<font size="4">输入需要打印的员工工号范围：</font>
                </td>
            </tr>
            <tr bgcolor="#d4d0c8" height="25">
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<font size="2">举例如示：1，2，3，5-10，33</font>
                </td>
            </tr>
            <tr bgcolor="#d4d0c8" height="25">
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr bgcolor="#d4d0c8">
                <td style="height: 14px" align="center" width="630">
                    <asp:TextBox ID="txtCode" runat="server" Height="20px" Width="550px" TabIndex="0"></asp:TextBox>
                </td>
            </tr>
            <tr bgcolor="#d4d0c8" height="25">
                <td align="center">
                    <asp:Button ID="btnPrint" CssClass="smallbutton2" Text="胸卡打印" CausesValidation="True"
                        runat="server" TabIndex="0" Width="120"></asp:Button>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNew" CssClass="smallbutton2" Text="新胸卡打印" CausesValidation="true"
                        runat="server" TabIndex="0" Width="120" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
