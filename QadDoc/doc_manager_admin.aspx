<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.doc_manager_admin" CodeFile="doc_manager_admin.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body >
    <div align="center">
        <form id="Form1" method="post" runat="server"> 
        <table cellspacing="1" cellpadding="1" width="600">
            <tr>
                <td valign="top" align="left" width="450" colspan="2" style="height: 20px">
                    Category
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="380px" Enabled="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" width="450" colspan="2" style="height: 20px">
                    Company
                    <asp:DropDownList ID="ddlCompany" runat="server" Width="380px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" width="450" colspan="2">
                    Department<asp:DropDownList ID="dept" runat="server" Width="280px" AutoPostBack="True">
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
                        <asp:ListItem>Select All</asp:ListItem>
                        <asp:ListItem>Unselect All</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="left">
                    <asp:Button ID="Button2" runat="server" Text="Save" CssClass="smallbutton2"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="btnclose" runat="server" Text="Back" CssClass="smallbutton2"></asp:Button>
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
