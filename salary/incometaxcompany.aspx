<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.IncometaxCompany" CodeFile="IncometaxCompany.aspx.vb" %>

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
        <table cellspacing="1" cellpadding="1" width="780" border="0">
            <tr>
                <td align="right">
                    &nbsp;&nbsp;&nbsp;&nbsp;<b>年月:</b>&nbsp;&nbsp;&nbsp;
                </td>
                <td align="left">
                    <asp:TextBox ID="TextBox1" runat="server" Width="50px" MaxLength="4">
                    </asp:TextBox>&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="50px" Font-Size="10pt"
                        CssClass="smallbutton2">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:Button ID="print" runat="server" Width="120px" Text="个人所得税导出" CssClass="SmallButton2"
                        OnClick="Prink_click"></asp:Button>
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
