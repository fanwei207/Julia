<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.soque_choose3" CodeFile="soque_choose3.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="javascript">
        function post() {
            window.opener.document.getElementById("txb_ccid").value = document.getElementById("txb_chooseid").value;
            window.opener.document.getElementById("txb_cc").value = document.getElementById("txb_choose").value;
            window.close();

        }		
           	
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="table1" cellpadding="0" cellspacing="0" width="400" align="center">
        <tr>
            <td align="right">
                <asp:Button ID="Button2" TabIndex="0" runat="server" Text="Close" CssClass="SmallButton3"
                    Width="60"></asp:Button>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Submit To£º
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txb_docid" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txb_chooseid" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="txb_choose" runat="server" Width="300px"></asp:TextBox>
                <input id="btn_ok" value="OK" class="SmallButton2" type="button" onclick="post()">
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="450" colspan="2" style="height: 20px">
                Company<asp:DropDownList ID="ddl_Company" runat="server" Width="200px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" width="450" colspan="2">
                Dept&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="ddl_department" runat="server" Width="180px" AutoPostBack="True">
                </asp:DropDownList>
                Stuff<asp:TextBox ID="txb_user" CssClass="SmallTextBox" Width="80px" runat="server"></asp:TextBox>
                <asp:Button ID="btn_search" CssClass="SmallButton3" runat="server" Text="Query"></asp:Button>
            </td>
        </tr>
        <tr>
            <td>
                <p style="color: #ff0000">
                    Mark:Dept and Stuff are required as a search condition.</p>
            </td>
        </tr>
        <tr>
            <td colspan="2">
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
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
