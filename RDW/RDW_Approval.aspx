<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.RDW_Approval" CodeFile="RDW_Approval.aspx.vb"
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
                    Please review the documents and make a approval.
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
                        <asp:DataGrid ID="Datagrid1" runat="server" Width="1200px" CellPadding="0" PagerStyle-HorizontalAlign="Center"
                            HeaderStyle-Font-Bold="false" AutoGenerateColumns="False" 
                            AllowPaging="True" PageSize="30" CssClass="GridViewStyle GridViewRebuild">
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <ItemStyle CssClass="GridViewRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" Font-Bold="False" />
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="g_mid" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="g_did" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="g_uid" ReadOnly="True">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_proj" SortExpression="g_proj" HeaderText="Project"
                                    ReadOnly="True">
                                    <HeaderStyle Width="150px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_prodcode" SortExpression="g_prodcode" HeaderText="Product Code"
                                    ReadOnly="true">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_taskid" SortExpression="g_taskid" HeaderText="StepNo."
                                    ReadOnly="true">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left" Width="40px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_step" SortExpression="g_step" HeaderText="StepName"
                                    ReadOnly="true">
                                    <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="left" Width="150px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_start" SortExpression="g_start" HeaderText="StartDate"
                                    ReadOnly="True">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_end" SortExpression="g_end" HeaderText="EndDate" ReadOnly="True">
                                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_creater" SortExpression="g_creater" HeaderText="Creator"
                                    ReadOnly="True">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_approver" SortExpression="g_approver" HeaderText="Approver"
                                    ReadOnly="True">
                                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:ButtonColumn Text="&lt;u&gt;Go&lt;/u&gt;" HeaderText="Approve" CommandName="g_appr">
                                    <HeaderStyle Width="45px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="45px" HorizontalAlign="Center"></ItemStyle>
                                </asp:ButtonColumn>
                                <asp:ButtonColumn Text="&lt;u&gt;View&lt;/u&gt;" HeaderText="Doc" CommandName="gobom">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                                </asp:ButtonColumn>
                                <asp:BoundColumn DataField="g_prodcode" SortExpression="g_prodcode" HeaderText="SKU#"
                                    ReadOnly="True">
                                    <HeaderStyle Width="120px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="g_proddesc" SortExpression="g_proddesc" HeaderText="Product Description"
                                    ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
