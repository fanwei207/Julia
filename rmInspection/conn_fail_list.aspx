<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.conn_fail_list" CodeFile="conn_fail_list.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1000">
            <tr>
                <td>
                    &nbsp;&nbsp;姓名<asp:TextBox ID="txb_uname" TabIndex="0" runat="server" MaxLength="10"
                        Width="50px" CssClass="SmallTextBox"></asp:TextBox>&nbsp; 部门<asp:TextBox ID="txb_dept"
                            TabIndex="0" runat="server" MaxLength="10" Width="70px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    关键字<asp:TextBox ID="txtCode" TabIndex="0" runat="server" MaxLength="50" Width="70px"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp; 申请日期
                    <asp:TextBox ID="txtDate1" TabIndex="0" runat="server" MaxLength="12" Width="65px"
                        CssClass="SmallTextBox"></asp:TextBox>--
                    <asp:TextBox ID="txtDate2" TabIndex="0" runat="server" MaxLength="12" Width="65px"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp; 小时差<asp:TextBox ID="Textbox1" TabIndex="0"
                            runat="server" Width="25px" CssClass="SmallTextBox"></asp:TextBox>&nbsp;
                    公司<asp:DropDownList ID="ddl_Company" runat="server" Width="200px" AutoPostBack="true"
                        Enabled="False">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询">
                    </asp:Button>&nbsp;
                </td>
                <td>
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" Width="70" CssClass="SmallButton3"
                        Text="导出扣分清单"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="980px" AutoGenerateColumns="False"
            CssClass="GridViewStyle" AllowPaging="True" PageSize="20">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="mstrid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="userid"></asp:BoundColumn>
                <asp:BoundColumn DataField="docuser" HeaderText="姓名">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdept" HeaderText="部门">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="doccont" HeaderText="内容">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdate" HeaderText="提交日期">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdate1" HeaderText="回签日期">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdate2" HeaderText="小时差">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docmark" HeaderText="扣分">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdate3" HeaderText="关闭日期">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;详细&lt;/u&gt;" CommandName="docview">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
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
