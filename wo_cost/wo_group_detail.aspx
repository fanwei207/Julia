<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_group_detail" CodeFile="wo_group_detail.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
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
                <td valign="top" align="left" width="450" colspan="2" style="height: 20px">
                    地点
                    <asp:DropDownList ID="dd_site" runat="server" Width="345px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" width="450" colspan="2">
                    成本中心<asp:DropDownList ID="dd_cc" runat="server" Width="120px" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 用户组<asp:DropDownList ID="dd_group" AutoPostBack="True"
                        runat="server" Width="150px">
                    </asp:DropDownList>
                    <asp:Label ID="lbl_all" runat="server" Visible="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" width="450" colspan="2">
                    用户组员所属部门<asp:DropDownList ID="dd_dept" runat="server" Width="280px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="lbl_qty" runat="server" Visible="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" width="450" colspan="2">
                    所属工段<asp:DropDownList ID="dropWorkshop" runat="server" Width="140px" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    所属工种<asp:DropDownList ID="dropWorkType" runat="server" Width="140px" AutoPostBack="true">
                    </asp:DropDownList>
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
                <td align="center">
                    <asp:Button ID="Button2" runat="server" Text="保存" CssClass="smallbutton2"></asp:Button>
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
