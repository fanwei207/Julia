<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.perf_employee" CodeFile="perf_employee.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <table cellspacing="1" cellpadding="1" width="600">
            <tr>
                <td valign="top" align="left" width="450" colspan="2">
                    奖罚率<asp:TextBox ID="txb_rate" runat="server" CssClass="SmallTextBox Numeric"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" width="450" colspan="2">
                    部门<asp:DropDownList ID="dept" runat="server" Width="280px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="500px" Height="400px"
                        BorderWidth="1">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 170px">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="165px" RepeatDirection="Horizontal"
                        AutoPostBack="True">
                        <asp:ListItem>全部允许</asp:ListItem>
                        <asp:ListItem>全部取消</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="left">
                    <asp:Button ID="Button2" runat="server" Text="保存" CssClass="smallbutton2"></asp:Button><asp:Button
                        ID="Button1" runat="server" Text="返回" CssClass="smallbutton2"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
