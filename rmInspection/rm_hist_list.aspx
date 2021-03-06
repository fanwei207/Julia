<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.rm_hist_list" CodeFile="rm_hist_list.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1000px">
            <tr>
                <td>
                    &nbsp;&nbsp;地点
                    <asp:DropDownList ID="ddl_site" runat="server" DataTextField="si_sitedomain" DataValueField="si_site">
                    </asp:DropDownList>
                    &nbsp; 分类<asp:DropDownList ID="ddl_type" runat="server" DataTextField="conn_typename"
                        DataValueField="conn_typeid">
                    </asp:DropDownList>
                    &nbsp;&nbsp;检验员<asp:TextBox ID="txb_uname" TabIndex="0" runat="server" CssClass="SmallTextBox"
                        Width="60px" MaxLength="10"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txb_dept" TabIndex="0" runat="server" CssClass="SmallTextBox" Width="100px"
                        MaxLength="10" Visible="false"></asp:TextBox>&nbsp; 内容<asp:TextBox ID="txtCode" TabIndex="0"
                            runat="server" CssClass="SmallTextBox" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                    创建日期
                    <asp:TextBox ID="txtDate1" TabIndex="0" runat="server" CssClass="SmallTextBox" Width="65px"
                        MaxLength="12"></asp:TextBox>--
                    <asp:TextBox ID="txtDate2" TabIndex="0" runat="server" CssClass="SmallTextBox" Width="65px"
                        MaxLength="12"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" Text="历史查询" CssClass="SmallButton3"
                        Width="80"></asp:Button>&nbsp;
                </td>
                <td>
                    <asp:Button ID="Button1" TabIndex="0" runat="server" Text="Excel" CssClass="SmallButton3"
                        Width="60"></asp:Button>&nbsp;
                    <asp:Button ID="Button3" TabIndex="0" runat="server" Text="删除报表" CssClass="SmallButton3"
                        Width="60" Visible="false"></asp:Button>&nbsp;
                    <asp:Button ID="Button2" TabIndex="0" runat="server" Text="返回" CssClass="SmallButton3">
                    </asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1000px" AutoGenerateColumns="False"
            AllowPaging="True" CssClass="GridViewStyle AutoPageSize" PageSize="20">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="mstrid"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="userid"></asp:BoundColumn>
                <asp:BoundColumn DataField="conn_site" SortExpression="conn_site" HeaderText="地点">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="conn_typename" SortExpression="conn_typename" HeaderText="类别">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docuser" SortExpression="docuser" HeaderText="检验员">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdept" SortExpression="docdept" HeaderText="部门" Visible="false">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="doccont" SortExpression="doccont" HeaderText="内容">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="docdate" SortExpression="docdate" HeaderText="日期时间">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="doctaken" SortExpression="doctaken" HeaderText="会签意见">
                    <HeaderStyle Width="350px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="350px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;详细&lt;/u&gt;" CommandName="docview">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="True" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
