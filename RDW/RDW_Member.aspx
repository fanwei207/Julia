<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.RDW_Member" CodeFile="RDW_Member.aspx.vb"
    Debug="true" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
        <table id="table1" cellspacing="0" cellpadding="0" width="1200">
            <tr>
                <td colspan="2">
                    Please review the documents and complete the task before the end date.
                </td>
            </tr>
            <tr>
                <td>
                    Show Mode<asp:DropDownList ID="ddlMode" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton2" Text="Export"
                        Width="50px" onclick="btnExport_Click"   />
                </td>
            </tr>
        </table>
        <table id="table2" cellspacing="0" cellpadding="0" width="1000">
            <tr>
                <td>
                        <asp:DataGrid ID="Datagrid1" runat="server" Width="1200px"
                            CellPadding="0" PagerStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false"
                            AutoGenerateColumns="False"
                            AllowPaging="true" PageSize="30" CssClass="GridViewStyle  GridViewRebuild" >
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <ItemStyle CssClass="GridViewRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" Font-Bold="False" />
                            <Columns>
                                <asp:BoundColumn Visible="false" DataField="g_mid" ReadOnly="true"></asp:BoundColumn>
                                <asp:BoundColumn Visible="false" DataField="g_did" ReadOnly="true"></asp:BoundColumn>
                                <asp:BoundColumn Visible="false" DataField="g_uid" ReadOnly="true"></asp:BoundColumn>
                                <asp:BoundColumn DataField="g_proj" SortExpression="g_proj" HeaderText="Project"
                                    ReadOnly="true">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left" Width="150px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_prodcode" SortExpression="g_prodcode" HeaderText="Product Code"
                                    ReadOnly="true">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_taskid" SortExpression="g_prodcode" HeaderText="StepNo."
                                    ReadOnly="true">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left" Width="40px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_step" SortExpression="g_step" HeaderText="StepName" ReadOnly="true">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left" Width="150px"></ItemStyle>
                                </asp:BoundColumn> 
                                <asp:BoundColumn DataField="g_start" SortExpression="g_start" HeaderText="StartDate"
                                    ReadOnly="true">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_end" SortExpression="g_end" HeaderText="EndDate" ReadOnly="true">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_creater" SortExpression="g_creater" HeaderText="Creator"
                                    ReadOnly="true">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_member" SortExpression="g_member" HeaderText="Member"
                                    ReadOnly="true">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:ButtonColumn Text="&lt;u&gt;Go&lt;/u&gt;" HeaderText="Task" CommandName="g_appr">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                </asp:ButtonColumn>
                                <asp:ButtonColumn Text="&lt;u&gt;View&lt;/u&gt;" HeaderText="Doc" CommandName="gobom">
                                    <HeaderStyle width="40px" horizontalalign="Center"   />
                                    <ItemStyle width="40px" horizontalalign="Center"  />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="g_proddesc" SortExpression="g_proddesc" HeaderText="Product Description"
                                    ReadOnly="true">
                                    <HeaderStyle Width="180px" HorizontalAlign="Center"></HeaderStyle> 
                                    <ItemStyle Width="180px" HorizontalAlign="left"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_sku" SortExpression="g_prodcode" HeaderText="SKU#"
                                    ReadOnly="true">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>

</body>
</html>
