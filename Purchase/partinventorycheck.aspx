<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.partInventoryCheck"
    CodeFile="partInventoryCheck.aspx.vb" %>

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
        <asp:Table ID="Table1" runat="server" Width="720px">
            <asp:TableRow>
                <asp:TableCell Text="仓库" HorizontalAlign="Right"></asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlWhPlace" Width="200px" runat="server" TabIndex="0" AutoPostBack="True">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Text="总数：">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="BtnADJAll" runat="server" CssClass="smallbutton3" Text="调整所有材料库存"
                        Width="100px"></asp:Button>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:DataGrid ID="dgCheck" runat="server" Width="700px" AutoGenerateColumns="False"
            AllowPaging="false" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="gsort" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="part_code" HeaderText="材料代码">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="category" HeaderText="分类">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="total_qty" HeaderText="库存总数">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sum_trans_qty" HeaderText="进出总数">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>调整</u>" CommandName="BtnOK">
                    <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="partID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="status" ReadOnly="True"></asp:BoundColumn>
            </Columns>
            <PagerStyle HorizontalAlign="Center"></PagerStyle>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
