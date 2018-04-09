<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.whPlaceCategory" CodeFile="whPlaceCategory.aspx.vb" %>

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
        <asp:Table runat="server" Width="680px" ID="Table1" GridLines="None" BorderWidth="0"
            CellPadding="3" CellSpacing="0">
            <asp:TableRow Height="25px" BackColor="LightGrey">
                <asp:TableCell ColumnSpan="2">
                    &nbsp;&nbsp;选择仓库：
                    <asp:DropDownList runat="server" Width="120px" ID="drd_wh" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 选择库位：
                    <asp:DropDownList runat="server" Width="120px" ID="drd_whPlace" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button ID="btn_back" runat="server" Text="返回" CssClass="SmallButton2" Width="80px">
                    </asp:Button>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Height="20px">
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow VerticalAlign="Bottom" BackColor="LightGrey">
                <asp:TableCell HorizontalAlign="Left" Width="300px">所包含的类别：</asp:TableCell>
                <asp:TableCell Width="80px"></asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="3300px">可添加的类别：</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow BackColor="LightGrey">
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Panel runat="server" Style="overflow-y: scroll; overflow: auto;" HorizontalAlign="Left"
                        Width="300px" Height="400px" BorderColor="Black" BackColor="White" BorderWidth="1px"
                        ID="Panel1">
                        <asp:DataGrid ID="dg_CategoryIn" runat="server" Width="280px" AutoGenerateColumns="False"
                            CssClass="GridViewStyle" AllowPaging="false">
                            <ItemStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:BoundColumn DataField="categoryID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="name" HeaderText="类别">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="type" HeaderText="类型">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="选项">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckb_CategoryIn" runat="server" Width="30px"></asp:CheckBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </asp:Panel>
                    <br>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle">
                    <asp:Button ID="addBtn" Text="<<增加" CssClass="SmallButton2" Width="100px" runat="server">
                    </asp:Button>
                    <br>
                    <br>
                    <asp:Button ID="deleteBtn" Text="删除>>" CssClass="SmallButton2" Width="100px" runat="server">
                    </asp:Button>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                    <asp:Panel runat="server" Style="overflow-y: scroll; overflow: auto;" HorizontalAlign="Left"
                        Width="300px" Height="400px" BorderColor="Black" BackColor="White" BorderWidth="1px"
                        ID="Panel2">
                        <asp:DataGrid ID="dg_CategoryOut" runat="server" Width="280px" CssClass="GridViewStyle"
                            AutoGenerateColumns="False" AllowPaging="false">
                            <ItemStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:BoundColumn DataField="categoryID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="name" HeaderText="类别">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="type" HeaderText="类型">
                                    <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="选项">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckb_CategoryOut" runat="server" Width="30px"></asp:CheckBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </asp:Panel>
                    <br>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal> 
    </script>
</body>
</html>
