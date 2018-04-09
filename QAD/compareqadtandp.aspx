<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.CompareQADTAndP" CodeFile="CompareQADTAndP.aspx.vb" %>

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
        <form id="Form2" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1200px">
            <tr>
                <td>
                    处理号 ：
                    <asp:TextBox ID="dealID" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    父零件 ：<asp:TextBox ID="parents" TabIndex="0" runat="server" Width="150px" MaxLength="14"></asp:TextBox>
                </td>
                <td>
                    子零件 ：<asp:TextBox ID="son" TabIndex="1" runat="server" Width="150px" MaxLength="14"></asp:TextBox>
                </td>
                <td>
                    替换零件 ：<asp:TextBox ID="replaceson" TabIndex="2" runat="server" Width="150px" MaxLength="14"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="Search" OnClick="searchRecord" TabIndex="3" runat="server" Width="50"
                        Text="查询" CssClass="SmallButton3"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="wdatetime" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="pdatetime" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="1200px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="12" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                <asp:BoundColumn DataField="processID" HeaderText="处理号">
                    <ItemStyle Width="5%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="parent" HeaderText="父零件">
                    <ItemStyle Width="7%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="son" HeaderText="子零件">
                    <ItemStyle Width="7%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rson" HeaderText="替换">
                    <ItemStyle Width="7%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="reason" HeaderText="原因"></asp:BoundColumn>
                <asp:BoundColumn DataField="require" HeaderText="需求">
                    <ItemStyle Width="4%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="rat" HeaderText="次品率">
                    <ItemStyle Width="5%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="sqty" HeaderText="库存" Visible="False">
                    <ItemStyle Width="7%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="zqty" HeaderText="加工单" Visible="False">
                    <ItemStyle Width="7%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="yqty" HeaderText="采购" Visible="False">
                    <ItemStyle Width="7%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qadsqty" HeaderText="QAD库存">
                    <ItemStyle Width="6%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qadzqty" HeaderText="QAD加工单">
                    <ItemStyle Width="6%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qadyqty" HeaderText="QAD采购">
                    <ItemStyle Width="6%"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createby" HeaderText="处理人">
                    <ItemStyle Width="5%"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>详细</u>" CommandName="Detail">
                    <ItemStyle Width="4%" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>清除</u>" CommandName="delete">
                    <ItemStyle Width="4%" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
