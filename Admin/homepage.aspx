<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.homepage" CodeFile="homepage.aspx.vb" %>

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
        <table cellspacing="1" cellpadding="2" width="600">
            <tr>
                <td valign="top" align="left" width="300" colspan="2">
                    <b>Please select one of the pages as your homepage:</b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="500px" Height="430px"
                        BorderWidth="1">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        </asp:RadioButtonList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width: 150px" align="right">
                    <asp:Button ID="Button1" runat="server" Text="Clear" Width="120" CssClass="smallbutton2">
                    </asp:Button>
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Save" CssClass="smallbutton2"></asp:Button>
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
