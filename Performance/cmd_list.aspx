<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.cmd_list" CodeFile="cmd_list.aspx.vb" %>

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
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1000">
            <tr>
                <td>
                    &nbsp;&nbsp;责任人<asp:TextBox ID="txb_uname" TabIndex="0" runat="server" CssClass="SmallTextBox"
                        Width="150px" MaxLength="10"></asp:TextBox>&nbsp;关键字<asp:TextBox ID="txtCode" TabIndex="0"
                            runat="server" CssClass="SmallTextBox" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" Text="查询" CssClass="SmallButton3">
                    </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="false" Text="全部" AutoPostBack="true">
                    </asp:CheckBox>
                </td>
                <td>
                    <asp:Button ID="btnNew" TabIndex="0" runat="server" Text="新建" CssClass="SmallButton3">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_Export" TabIndex="0" runat="server" Text="导出" CssClass="SmallButton3">
                    </asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <table id="table2" cellspacing="0" cellpadding="0" width="1000">
            <tr>
                <td>
                    <asp:DataGrid ID="Datagrid1" runat="server" Width="1000px" CssClass="GridViewStyle">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="mstrid"></asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="userid"></asp:BoundColumn>
                            <asp:BoundColumn DataField="doccont" HeaderText="决议内容">
                                <HeaderStyle Width="350px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="left" Width="350px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="docdate" HeaderText="日期">
                                <HeaderStyle Width="55px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="doctaken" HeaderText="责任人">
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="docrep" HeaderText="执行情况描述">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="docclo" HeaderText="关闭日期">
                                <HeaderStyle Width="55px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="center" Width="55px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn Text="操作" CommandName="docact">
                                <HeaderStyle Width="40px"></HeaderStyle>
                                <ItemStyle Width="40px"></ItemStyle>
                            </asp:ButtonColumn>
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
