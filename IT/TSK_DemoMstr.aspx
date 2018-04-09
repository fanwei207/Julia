<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_DemoMstr.aspx.cs" Inherits="IT_TSK_DemoMstr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellpadding="0" cellspacing="0" style=" width:850px; text-align:left;">
            <tr>
                <td>
                    Title:<asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                &nbsp;Menu:<asp:TextBox ID="txtMenu" runat="server" Width="200px"></asp:TextBox>
                &nbsp;
                        &nbsp; status:<asp:DropDownList ID="ddlstatus" runat="server">
                        <asp:ListItem Selected="True" Value="0">Active </asp:ListItem>
                        <asp:ListItem Value="1">Closed</asp:ListItem>
                        <asp:ListItem Value="2">All</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" 
                        onclick="btnQuery_Click" Text="Query" />
                </td>
                <td align="right">
                <asp:Button ID="btn_New" runat="server" CssClass="SmallButton3" 
                         Text="New" onclick="btn_New_Click" />
                </td>
            </tr>
            
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="850px" OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table3" Width="850px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="demo_title" HeaderText="TiTle">
                    <HeaderStyle Width="500px" HorizontalAlign="Center" />
                    <ItemStyle Width="500px" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="dmd_menuName" HeaderText="MenuName">
                    <HeaderStyle Width="350px" HorizontalAlign="Center" />
                    <ItemStyle Width="350px" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>

               

            </Columns>
        </asp:GridView>
    </div>
    </form>
      <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
