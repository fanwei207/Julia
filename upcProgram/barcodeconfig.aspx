<%@ Page Language="C#" AutoEventWireup="true" CodeFile="barcodeconfig.aspx.cs" Inherits="upcProgram_barcodeconfig" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="880">
            <tr>
                <td align="left" style="height: 20px">
                    客户名称:<asp:TextBox ID="txtDesc" runat="server" CssClass="smalltextbox" Width="150px"></asp:TextBox>
                    &nbsp; 固定码列:<asp:TextBox ID="txtCode" runat="server" CssClass="smalltextbox" Width="118px"></asp:TextBox>
                    &nbsp; 外箱条码前缀:<asp:TextBox ID="txtMax" runat="server" CssClass="smalltextbox" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp; 中箱条码前缀:<asp:TextBox ID="txtMid" runat="server" CssClass="smalltextbox"
                        Width="100px"></asp:TextBox><asp:TextBox ID="txtMin" runat="server" CssClass="smalltextbox"
                            Width="100px" Visible="False"></asp:TextBox>
                </td>
                <td style="height: 20px">
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="Add" OnClick="btnAdd_Click"
                        Width="42px"></asp:Button><asp:Button ID="btnsearch" runat="server" CssClass="SmallButton3"
                            OnClick="btnsearch_Click" Text="Search" Width="42px" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgCode" runat="server" PageSize="23" AllowPaging="True" AutoGenerateColumns="False"
            OnDeleteCommand="dgCode_DeleteCommand" OnPageIndexChanged="dgCode_PageIndexChanged"
            CssClass="GridViewStyle AutoPageSize" OnSelectedIndexChanged="dgCode_SelectedIndexChanged"
            Width="882px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="bcc_desc" SortExpression="bcc_desc" HeaderText="客户名称">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="200px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bcc_code" SortExpression="bcc_code" HeaderText="固定码列">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="150px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bcc_pre_max" SortExpression="bcc_pre_max" HeaderText="外箱条码前缀">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="150px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bcc_pre_mid" SortExpression="bcc_pre_mid" HeaderText="中箱条码前缀">
                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="150px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="bcc_pre_min" SortExpression="bcc_pre_min" HeaderText="Min-Prefix"
                    Visible="False">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="150px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False">
                    </ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn CommandName="Select" Text="<u>Edit</u>">
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonColumn>
                <asp:ButtonColumn CommandName="Delete" Text="<u>删除</u>">
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
