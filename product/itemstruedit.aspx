<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ItemStruEdit" CodeFile="ItemStruEdit.aspx.vb" %>

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
        <table style="border-top-style: none; border-right-style: none; border-left-style: none;
            border-bottom-style: none" bordercolor="lightgrey" cellspacing="0" cellpadding="0"
            width="1004px" border="0">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="984px" align="center" bgcolor="white"
                        border="0">
                        <tr>
                            <td align="left" width="624" style="height: 21px">
                                产品代码为&nbsp;&nbsp;<asp:Label ID="lblProdCode" runat="server"></asp:Label>&nbsp;&nbsp;
                                的结构
                            </td>
                            <td align="right" style="height: 21px">
                                <asp:Button ID="BtnPartAddNew" runat="server" Width="60px" CssClass="SmallButton3"
                                    Text="增加部件"></asp:Button></FONT>&nbsp;
                                <asp:Button ID="BtnProdAddNew" runat="server" Width="60px" CssClass="SmallButton3"
                                    Text="增加产品"></asp:Button>&nbsp;
                                <asp:Button ID="BtnStruSave" runat="server" Width="60px" CssClass="SmallButton3"
                                    Text="保存结构"></asp:Button></FONT>&nbsp;
                                <asp:Button ID="BtnStruUpdate" runat="server" Width="60px" CssClass="SmallButton3"
                                    Text="结构升级"></asp:Button>&nbsp;
                                <asp:Button ID="Button1" runat="server" Width="60px" CssClass="SmallButton3" Text="删除结构">
                                </asp:Button>&nbsp;
                                <asp:Button ID="BtnReturn" runat="server" Text="返回" CssClass="SmallButton3"></asp:Button>
                            </td>
                        </tr>
                        <tr width="100%">
                            <td colspan="2">
                                <asp:Panel ID="Panel1" Style="overflow-y: scroll; overflow-x: scroll" runat="server"
                                    Height="230px" BorderColor="Black" BorderWidth="1px" Width="984px">
                                    <asp:DataGrid ID="dgPart" runat="server" Width="3550" AutoGenerateColumns="False"
                                        AllowPaging="false" PageSize="8" CssClass="GridViewStyle">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <ItemStyle CssClass="GridViewRowStyle" />
                                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ProdStruID" ReadOnly="True"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="partcode" SortExpression="partcode" HeaderText="部件号">
                                                <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="partQty" SortExpression="partQty" HeaderText="数量">
                                                <HeaderStyle Width="100px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn Text="<u>编辑</u>" CommandName="EditPartBtn">
                                                <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                            </asp:ButtonColumn>
                                            <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeletePartBtn">
                                                <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="partPos" SortExpression="partPos" HeaderText="位号">
                                                <HeaderStyle Width="200px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn DataTextField="partReplace" SortExpression="partReplace" HeaderText="替代品"
                                                CommandName="EditPartReplace">
                                                <HeaderStyle Width="200px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="partMemo" SortExpression="partMemo" HeaderText="备注">
                                                <HeaderStyle Width="200px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="partdesc" SortExpression="partdesc" HeaderText="部件描述">
                                                <HeaderStyle Width="2450px" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="2450px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="pID" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr width="100%">
                            <td colspan="6">
                                <asp:Panel ID="Panel2" Style="overflow-y: scroll; overflow-x: scroll" runat="server"
                                    Height="230px" BorderColor="Black" BorderWidth="1px" Width="984px">
                                    <asp:DataGrid ID="dgProd" runat="server" Width="3550px" AutoGenerateColumns="False"
                                        AllowPaging="false" PageSize="8" CssClass="GridViewStyle">
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <ItemStyle CssClass="GridViewRowStyle" />
                                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                                        <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
                                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="ProdStruID" ReadOnly="True"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="prodcode" SortExpression="prodcode" HeaderText="半成品编号">
                                                <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="prodQty" SortExpression="prodQty" HeaderText="数量">
                                                <HeaderStyle Width="100px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn Text="<u>编辑</u>" CommandName="EditProdBtn">
                                                <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                            </asp:ButtonColumn>
                                            <asp:ButtonColumn Text="<u>删除</u>" CommandName="DeleteProdBtn">
                                                <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                                                <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="prodPos" SortExpression="prodPos" HeaderText="位号">
                                                <HeaderStyle Width="200px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:ButtonColumn DataTextField="prodReplace" SortExpression="prodReplace" HeaderText="替代品"
                                                CommandName="EditProdReplace">
                                                <HeaderStyle Width="200px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="prodMemo" SortExpression="prodMemo" HeaderText="备注">
                                                <HeaderStyle Width="200px"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="proddesc" SortExpression="proddesc" HeaderText="半成品描述">
                                                <HeaderStyle Width="2450px" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="2450px"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="pID" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
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
