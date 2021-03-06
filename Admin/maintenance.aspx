<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.Maintenance" CodeFile="Maintenance.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <br />
        <table cellspacing="2" cellpadding="2" border="0">
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    公司名称(中):
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="CName" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="300px"
                        TabIndex="0"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    (英):
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="EName" runat="server" CssClass="SmallTextBox" MaxLength="100" Width="300px"
                        TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    代码:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="Ccode" runat="server" CssClass="SmallTextBox" MaxLength="10" Width="300px"
                        TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    地址:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="Address" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="300px"
                        TabIndex="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 16px" align="right">
                    邮编:
                </td>
                <td style="height: 16px">
                    <asp:TextBox ID="Zip" runat="server" CssClass="SmallTextBox" MaxLength="10" Width="250px"
                        TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 8px" align="right">
                    电话:
                </td>
                <td style="height: 8px">
                    <asp:TextBox ID="Phone" runat="server" CssClass="SmallTextBox" MaxLength="20" Width="250px"
                        TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 8px" align="right">
                    传真:
                </td>
                <td style="height: 8px">
                    <asp:TextBox ID="Fax" runat="server" CssClass="SmallTextBox" MaxLength="20" Width="250px"
                        TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 4px" align="right">
                    手机:
                </td>
                <td style="height: 4px">
                    <asp:TextBox ID="Mobile" runat="server" CssClass="SmallTextBox" MaxLength="20" Width="250px"
                        TabIndex="7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 10px" align="right">
                    电子邮件:
                </td>
                <td style="height: 10px">
                    <asp:TextBox ID="Email" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="250px"
                        TabIndex="8"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 100px; height: 28px">
                    &nbsp;
                </td>
                <td style="height: 28px" align="center" colspan="2">
                    <asp:Button ID="BtnSave" runat="server" CssClass="SmallButton2" Visible="True" TabIndex="9"
                        Text="保存"></asp:Button>
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
