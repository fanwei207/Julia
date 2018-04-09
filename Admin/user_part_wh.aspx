 <%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.user_part_wh" CodeFile="user_part_wh.aspx.vb" %>
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
        <asp:Table runat="server" CellSpacing="1" CellPadding="1" Width="600" ID="Table1">
            <asp:TableRow>
                <asp:TableCell VerticalAlign="top" HorizontalAlign="right" Width="450" ColumnSpan="2">
                    职务:
                    <asp:DropDownList ID="ddlRole" runat="server" Width="180px" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;用户名:
                    <asp:DropDownList ID="ddlUser" AutoPostBack="True" runat="server" Width="150px">
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="500px" Height="330px"
                        BorderWidth="1">
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Style="width: 170px">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="165px" RepeatDirection="Horizontal"
                        AutoPostBack="True">
                        <asp:ListItem>全部允许</asp:ListItem>
                        <asp:ListItem>全部取消</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="smallbutton2"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="bntBack" runat="server" Text="返回" CssClass="smallbutton2"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
