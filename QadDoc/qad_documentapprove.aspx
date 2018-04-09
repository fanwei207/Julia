<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_documentapprove"
    CodeFile="qad_documentapprove.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="1002">
            <tr class="main_top">
                <td>
                    Category<asp:DropDownList ID="SelectTypeDropDown" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                    Type<asp:DropDownList ID="SelectCateDropDown" runat="server" Width="200px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    Key Word<asp:TextBox ID="txb_search" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>
                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Text="Show All" />
                    <asp:Button ID="btn_search" runat="server" CssClass="SmallButton3" Text="Search"
                        Width="100px"></asp:Button>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DgDoc" runat="server" Width="1600px" CssClass="GridViewStyle AutoPageSize"
            PageSize="18" AutoGenerateColumns="False" AllowPaging="True">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:ButtonColumn Visible="False" Text="&gt;" CommandName="select">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn Visible="False" DataField="docid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="typeid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="cateid"></asp:BoundColumn>
                <asp:BoundColumn DataField="typename" HeaderText="Category">
                    <HeaderStyle Width="180px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="180px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="catename" HeaderText="Type">
                    <HeaderStyle Width="170px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="170px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="filename" SortExpression="filename" HeaderText="FileName">
                    <HeaderStyle Width="250px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="DocName">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="version" SortExpression="version" HeaderText="Ver">
                    <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn DataTextField="checkedname" HeaderText="IsAppr" CommandName="Appr">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle Width="50px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="checkeddate" SortExpression="checkeddate" HeaderText="Date">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="preview" HeaderText="View">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;List&lt;/u&gt;" DataTextField="old" HeaderText="P.Ver"
                    CommandName="oldview">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="isall" SortExpression="isall" HeaderText="AllItem">
                    <HeaderStyle Width="25px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="associated_item" HeaderText="Assoc">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" SortExpression="description" HeaderText="Description">
                    <HeaderStyle HorizontalAlign="Left" Width="820px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="820px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
