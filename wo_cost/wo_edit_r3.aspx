<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_edit_r3" CodeFile="wo_edit_r3.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="970px">
            <tr>
                <td colspan="2">
                    &nbsp;地点<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                    </asp:DropDownList>
                    &nbsp; 加工单号<asp:TextBox ID="txb_wonbr" runat="server" Width="70" TabIndex="2" Height="22"></asp:TextBox>&nbsp;
                    加工单ID<asp:TextBox ID="txb_woid" runat="server" Width="70" TabIndex="3" Height="22"></asp:TextBox>&nbsp;
                    成本中心:
                    <asp:Label ID="lbl_cc" runat="server" Width="40"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_cc1" runat="server" Width="0" Visible="false"></asp:Label>
                    完工入库:
                    <asp:Label ID="lbl_comp" runat="server" Width="80"></asp:Label>&nbsp; 工单工费:
                    <asp:Label ID="lbl_cost" runat="server" Width="80"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_price" runat="server" Width="" Visible="false"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_rate" runat="server" Width="" Visible="false"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;零件号:
                    <asp:Label ID="lbl_part" runat="server" Width="130"></asp:Label>&nbsp; 描述:
                    <asp:Label ID="lbl_desc" runat="server" Width="320"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:Button ID="btn_woload" runat="server" CssClass="SmallButton3" Text="查询" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" runat="server" CssClass="SmallButton3" Text="导出" TabIndex="24">
                    </asp:Button>
                    <asp:Button ID="btn_edit1" runat="server" Width="80" CssClass="SmallButton3" Text="返回"
                        TabIndex="24"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;工序:<asp:DropDownList ID="dropWorkproc" runat="server" Width="360px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;日期<asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox Date"
                        Width="70px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="删除" Width="100px" CssClass="SmallButton3" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" AutoGenerateColumns="False" OnUpdateCommand="Edit_update"
            OnCancelCommand="Edit_cancel" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" SortExpression="id" HeaderText="" Visible="false"
                    ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="user_id" SortExpression="user_id" HeaderText="工号" ReadOnly="true">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="user_name" SortExpression="user_name" HeaderText="姓名"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_cc" SortExpression="proc_cc" HeaderText="成本中心" ReadOnly="true">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_nbr" SortExpression="proc_nbr" HeaderText="工单号"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_id" SortExpression="proc_id" HeaderText="工单ID" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_name" SortExpression="proc_name" HeaderText="工序"
                    ReadOnly="true">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_qty" SortExpression="proc_qty" HeaderText="数量" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_adj" SortExpression="proc_adj" HeaderText="调整">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price2" SortExpression="proc_price2" HeaderText="工序定额"
                    ReadOnly="true">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price1" SortExpression="proc_price1" HeaderText="工时"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_date_comp" SortExpression="wo_date_comp" HeaderText="日期"
                    ReadOnly="true">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_created" SortExpression="wo_created" HeaderText="创建人"
                    ReadOnly="true">
                    <HeaderStyle Width="45px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" HeaderText="编辑"
                    CancelText="<u>取消</u>" EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="70px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="proc_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="wo_line" SortExpression="wo_line" HeaderText="工段线" ReadOnly="true">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
