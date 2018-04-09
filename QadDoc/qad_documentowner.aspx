<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_documentowner" CodeFile="qad_documentowner.aspx.vb" %>
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
    <form id="Form1" runat="server">
    <br />
    <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
        <tr>
            <td align="center" colspan="2">
                TO change the owner of the category
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:Button ID="Button2" TabIndex="0" runat="server" Text="Close" CssClass="SmallButton3"
                    Width="60"></asp:Button>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Owner
            </td>
            <td>
                <asp:TextBox ID="txb_owner" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                <asp:TextBox ID="txb_ownerid" runat="server" Style="display: none"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Change to
            </td>
            <td>
                <asp:TextBox ID="txb_docid" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txb_chooseid" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txb_choose" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                <asp:Button ID="btn_ok1" TabIndex="0" runat="server" Text="Save" CssClass="SmallButton3"
                    Width="60"></asp:Button>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Company
            </td>
            <td valign="top" align="left" width="450" colspan="2" style="height: 20px">
                <asp:DropDownList ID="ddl_Company" runat="server" Width="200px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Department
            </td>
            <td>
                <asp:DropDownList ID="ddl_department" runat="server" Width="200px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                User Name
            </td>
            <td>
                <asp:TextBox ID="txb_user" CssClass="SmallTextBox" Width="80px" runat="server"></asp:TextBox>
                <asp:Button ID="btn_search" CssClass="SmallButton3" runat="server" Text="Search"
                    Width="60"></asp:Button>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="300px" Height="300px"
                    BorderWidth="1">
                    <asp:CheckBoxList ID="cbl_user" runat="server" AutoPostBack="True">
                    </asp:CheckBoxList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="Button1" TabIndex="0" runat="server" Text="Close" CssClass="SmallButton3"
                    Width="60"></asp:Button>&nbsp;
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
