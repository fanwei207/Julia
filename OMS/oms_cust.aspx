<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_cust.aspx.cs" Inherits="oms_cust" %>

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
        <table style="width: 680px">
            <tr>
                <td align="left">
                    Cust Code：<asp:TextBox ID="txtCustCode" runat="server" Width="60px" CssClass="Param"
                        MaxLength="8"></asp:TextBox>
                    &nbsp;Cust Name：
                    <asp:TextBox ID="txtCust" runat="server" Width="203px" CssClass="Param"></asp:TextBox>
                    (*provide matchable search)&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Query" CssClass="SmallButton3" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            PageSize="20" AllowPaging="True" Width="750px" 
            OnRowCommand="gv_RowCommand" OnPageIndexChanging="gv_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="680px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Cust Code" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Cust Name" Width="500px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ad_addr" HeaderText="Cust Code">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_name" HeaderText="Cust Name">
                    <HeaderStyle Width="500px" HorizontalAlign="Center" />
                    <ItemStyle Width="500px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDetail" runat="server" CommandName="Detail"><u>Detail</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkFs" runat="server" CommandName="FS"><u>Factory Status</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkForecast" runat="server" CommandName="Forecast"><u>Forecast</u></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
