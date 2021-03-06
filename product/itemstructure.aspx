<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.ItemStructure" CodeFile="ItemStructure.aspx.vb" %>

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
        <table cellspacing="0" cellpadding="0" width="984" border="0">
            <tr>
                <td align="left" colspan="3">
                    产品代码为&nbsp;&nbsp;<asp:Label 
                        ID="lblProdCode" runat="server" Font-Bold="True"></asp:Label>&nbsp; 的结构&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;创建日期:<asp:Label
                        ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 14px" align="left">
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="False" AutoPostBack="True" Text="显示全部结构">
                    </asp:CheckBox>
                </td>
                <td style="height: 14px" align="left">
                    <asp:Label ID="lblversion" Text="版本" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlversion" runat="server" Width="100px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td style="height: 14px" align="center">
                    <asp:TextBox ID="txtCode" runat="server" Width="200px" CssClass="SmallTextBox" MaxLength="50"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="BtnStruCopy" runat="server" Width="60px" CssClass="SmallButton3"
                        Text="拷贝结构"></asp:Button>
                        &nbsp;&nbsp;
                        <asp:Button ID="BtnStruModify" runat="server" Width="60px" CssClass="SmallButton3"
                        Text="修改结构"></asp:Button>
                        &nbsp;&nbsp;
                        <asp:Button ID="BtnStruHis" runat="server" Width="60px" CssClass="SmallButton3" Text="结构历史"></asp:Button>
                    &nbsp;<asp:Button ID="btnoutput" runat="server" CssClass="SmallButton3" Text="导出结构"></asp:Button>
                    &nbsp;
                    <asp:Button ID="BtnReturn" runat="server" CssClass="SmallButton3" Text="返回"></asp:Button>
                    &nbsp;<asp:Button ID="ButExcel" runat="server" CssClass="SmallButton3" Text="Excel"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgPart" runat="server" PagerStyle-ForeColor="#0033ff" PagerStyle-Font-Size="12pt"
            PagerStyle-BackColor="#99ffff" PagerStyle-Mode="NumericPages" PageSize="20"
            PagerStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" AutoGenerateColumns="False"
            CssClass="GridViewStyle" Width="2800px">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="ProdStruID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="partcode" SortExpression="partcode" HeaderText="部件号">
                    <HeaderStyle Width="300px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="300px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partqad" SortExpression="partqad" HeaderText="QAD">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="mtlock" SortExpression="mtlock" HeaderText="锁定">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" SortExpression="status" HeaderText="状态">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="type" SortExpression="type" HeaderText="分类">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="50px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="partQty" SortExpression="partQty" HeaderText="数量">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="70px" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
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
                    <HeaderStyle Width="2000px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="1660px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qadReplace" SortExpression="qadReplace" HeaderText="替代品QAD">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="200px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="pID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn Visible="false" DataField="cID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="false" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="true" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
