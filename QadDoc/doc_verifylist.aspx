<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.doc_verifylist" CodeFile="doc_verifylist.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="980" style="margin-top: 2px;">
            <tr class="main_top">
                <td>
                    Category<asp:DropDownList ID="SelectTypeDropDown" runat="server" Width="350px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Text="Show All" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1420px" PagerStyle-HorizontalAlign="Center"
            HeaderStyle-Font-Bold="false" AutoGenerateColumns="False" GridLines="None" AllowPaging="True"
            PageSize="18" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" HorizontalAlign="Left" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="typeid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="cateid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="uid"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="No" DataField="No">
                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="DocName" DataField="DocName">
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="FileName" DataField="FileName">
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="Ver" DataField="DocVer">
                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Reason" HeaderStyle-Width="250">
                    <ItemTemplate>
                        <asp:TextBox ID="txtReason" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Width="250px"></HeaderStyle>
                </asp:TemplateColumn>
                <asp:ButtonColumn Text="<u>Detail</u>" HeaderText="Detail" CommandName="DetailBtn">
                    <HeaderStyle Width="40px" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle Width="40px" VerticalAlign="Middle" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center">
                    </ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>Pass</u>" HeaderText="Pass" CommandName="PassBtn">
                    <HeaderStyle Width="30px" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle Width="30px" VerticalAlign="Middle" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center">
                    </ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>NoPass</u>" HeaderText="NoPass" CommandName="NoPassBtn">
                    <HeaderStyle Width="40px" VerticalAlign="Middle"></HeaderStyle>
                    <ItemStyle Width="40px" VerticalAlign="Middle" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center">
                    </ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn HeaderText="Category" DataField="TypeName">
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn HeaderText="Type" DataField="CateName">
                    <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="Docid"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
