<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.qad_bomviewdocNew"
    CodeFile="qad_bomviewdocNew.aspx.vb" %>

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
        <table id="Table1" runat="server" cellpadding="0" cellspacing="0" width="980px">
            <tr>
                <td>
                    BOM Code
                    <asp:TextBox ID="txb_bom_code" runat="server" Width="130px" CssClass="smalltextbox"
                        MaxLength="20"></asp:TextBox>
                    BOM Date
                    <asp:TextBox ID="txb_bom_date" runat="server" Width="70px" CssClass="smalltextbox Date"
                        MaxLength="10"></asp:TextBox>
                    Level
                    <asp:TextBox ID="txb_lel" runat="server" Width="50px" CssClass="smalltextbox"></asp:TextBox>
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="QAD Desc" Checked="True" AutoPostBack="True">
                    </asp:CheckBox>
                    <asp:CheckBox ID="Checkbox2" runat="server" Text="Associated Doc" Checked="True"
                        AutoPostBack="True"></asp:CheckBox>
                    <asp:Button ID="btn_search" runat="server" CssClass="SmallButton3" Width="70" Text="Search">
                    </asp:Button>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="SmallButton3" Text="Back"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="980px" AutoGenerateColumns="False"
            PageSize="23" AllowPaging="True" CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" ReadOnly="True" Visible="False" HeaderText="">
                    <HeaderStyle Width="0px"></HeaderStyle>
                    <ItemStyle Width="0px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" ReadOnly="True" HeaderText="No.">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="glevel" ReadOnly="True" HeaderText="Level">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gPart" ReadOnly="True" HeaderText="Item">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gqty" ReadOnly="True" HeaderText="Qty">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gCode" ReadOnly="True" HeaderText="Old Item">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="gDesc" ReadOnly="True" HeaderText="Description">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="glink" ReadOnly="True" HeaderText="Associated Doc">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle Width="200px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>Open</u>" HeaderText="" CommandName="docview">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="fid" ReadOnly="True" Visible="false" HeaderText="">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    <ItemStyle Width="20px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
