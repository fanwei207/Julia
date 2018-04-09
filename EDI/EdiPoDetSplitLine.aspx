<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiPoDetSplitLine.aspx.cs" Inherits="EDI_EdiPoDetSplitLine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
        <tr>
            <td>
                原行号：
            <asp:Label ID="lblPoLine" runat="server"></asp:Label>
            </td>
            <td>
                客户零件号：
            <asp:Label ID="lblPartNbr" runat="server"></asp:Label>
            </td>
            <td>
                原QAD号：
            <asp:Label ID="lblQadPart" runat="server"></asp:Label>
            </td>
            <td>
                订单数量：
            <asp:Label ID="lblOrdQty" runat="server"></asp:Label>
            </td>
            <td>
                未分配数量：
            <asp:Label ID="lblSurplusQty" runat="server"></asp:Label>
                 <asp:Label ID="lbldate" runat="server" Visible="False"></asp:Label>
            </td>
            <td align="right">
                <asp:Button ID="btnSure" runat="server" Text="确认" CssClass="SmallButton2" OnClick="btnSure_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="6">
                 <asp:GridView ID="gvlist" runat="server" AllowPaging="false" AutoGenerateColumns="False"  Width="800px" 
                        CssClass="GridViewStyle" ShowFooter="true"
                        OnRowDeleting="gvlist_RowDeleting" OnRowCommand="gvlist_RowCommand" EmptyDataText="No data">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                 <Columns>
                <asp:TemplateField HeaderText="行号">
                    <ItemTemplate>
                        <asp:Label ID="lblLine" Text='<%# Eval("poLine") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLine" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="QAD">
                    <ItemTemplate>
                        <asp:Label ID="lblPart" Text='<%# Eval("qadPart") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPart" CssClass="Part" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="数量">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" Text='<%# Eval("ordQty") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtQty" CssClass="Numeric" Text='' runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="日期">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" Text='<%# Eval("date") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDate" Text='' runat="server" Enabled ="False"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CommandName="Delete" runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="linkAddCust" CommandName="AddLine" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
                 </Columns>
                 </asp:GridView>
            </td>

        </tr>
    </table>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
