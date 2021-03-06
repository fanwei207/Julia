<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ItemQadStructure" CodeFile="ItemQadStructure.aspx.vb" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="984" border="0">
            <tbody>
                <tr width="100%">
                    <td align="left" width="700" colspan="6">
                        QAD零件号为&nbsp;<font size='4'><asp:Label ID="lblProdCode" runat="server"></asp:Label></font>&nbsp;的结构&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;创建日期:<asp:Label
                            ID="Label1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 14px" align="left" width="250">
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="False" AutoPostBack="True" Text="显示全部结构">
                        </asp:CheckBox>
                    </td>
                    <td style="height: 14px" align="center" width="200">
                        <asp:Button ID="BtnTrans" runat="server" CssClass="SmallButton3" Text="转至QAD结构" Width="100px">
                        </asp:Button>&nbsp;
                        <asp:Button ID="BtnReturn" runat="server" CssClass="SmallButton3" Text="返回"></asp:Button>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:DataGrid ID="dgPart" runat="server" Width="2520px" PageSize="20"
            AutoGenerateColumns="False" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="ProdStruID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="partcode" SortExpression="partcode" HeaderText="部件号">
                    <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" SortExpression="status" HeaderText="状态">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type" SortExpression="type" HeaderText="分类">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partQty" SortExpression="partQty" HeaderText="数量">
                    <HeaderStyle Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partPos" SortExpression="partPos" HeaderText="位号">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partReplace" SortExpression="partReplace" HeaderText="替代品">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partMemo" SortExpression="partMemo" HeaderText="备注">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partdesc" SortExpression="partdesc" HeaderText="部件描述">
                    <HeaderStyle Width="1500px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="1500px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="pID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn Visible="false" DataField="cID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="false" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="true" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
