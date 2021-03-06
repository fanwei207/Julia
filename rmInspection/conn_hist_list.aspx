<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.conn_hist_list" CodeFile="conn_hist_list.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1000px">
            <tr>
                <td>
                    &nbsp;&nbsp;申请人<asp:TextBox ID="txb_uname" TabIndex="0" runat="server" CssClass="SmallTextBox"
                        Width="60px" MaxLength="10"></asp:TextBox>&nbsp; 部门<asp:TextBox ID="txb_dept" TabIndex="0"
                            runat="server" CssClass="SmallTextBox" Width="100px" MaxLength="10"></asp:TextBox>&nbsp;
                    关键字<asp:TextBox ID="txtCode" TabIndex="0" runat="server" CssClass="SmallTextBox"
                        Width="200px" MaxLength="50"></asp:TextBox>&nbsp; 申请日期
                    <asp:TextBox ID="txtDate1" TabIndex="0" runat="server" CssClass="SmallTextBox Date"
                        Width="70px" MaxLength="10"></asp:TextBox>--
                    <asp:TextBox ID="txtDate2" TabIndex="0" runat="server" CssClass="SmallTextBox Date"
                        Width="70px" MaxLength="10"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" Text="查询" CssClass="SmallButton3">
                    </asp:Button>&nbsp;
                </td>
                <td>
                    <asp:Button ID="Button1" TabIndex="0" runat="server" Text="导出" CssClass="SmallButton3"
                        Visible="False"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1000px" AutoGenerateColumns="False"
            PagerStyle-HorizontalAlign="Center" AllowSorting="True" CellPadding="0" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="mstrid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="userid"></asp:BoundColumn>
                <asp:BoundColumn DataField="docuser" HeaderText="申请人">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdept" HeaderText="部门">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="doccont" HeaderText="内容">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdate" HeaderText="日期">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="doctaken" HeaderText="会签意见">
                    <HeaderStyle Width="350px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="350px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;详细&lt;/u&gt;" CommandName="docview">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
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
