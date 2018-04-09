<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_edit_a3" CodeFile="wo_edit_a3.aspx.vb" %>

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
        <asp:Panel ID="Panel2" runat="server" Width="980px" HorizontalAlign="Left" BorderWidth="1px"
            BorderColor="Black" Height="40px">
            <table id="table1" cellspacing="0" cellpadding="0" width="980">
                <tr>
                    <td>
                        &nbsp;地点<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                        </asp:DropDownList>
                        &nbsp; 加工单号<asp:TextBox ID="txb_wonbr" runat="server" Width="90" TabIndex="2" Height="22"></asp:TextBox>&nbsp;
                        成本中心:
                        <asp:Label ID="lbl_cc" runat="server" Width="40"></asp:Label>&nbsp;
                        <asp:Label ID="lbl_cc1" runat="server" Width="0" Visible="false"></asp:Label>
                        完工入库总数<asp:TextBox ID="txb_comp" runat="server" Width="45" TabIndex="14" Height="22"></asp:TextBox>&nbsp;
                        总工时<asp:TextBox ID="txb_hour" runat="server" Width="45" TabIndex="15" Height="22"></asp:TextBox>&nbsp;
                        <asp:Label ID="lbl_cost" runat="server" Width="80" Visible="false"></asp:Label>&nbsp;
                        <asp:Label ID="lbl_price" runat="server" Width="" Visible="false"></asp:Label>&nbsp;
                        <asp:Label ID="lbl_rate" runat="server" Width="" Visible="false"></asp:Label>&nbsp;
                        <asp:Label ID="lbl_type" runat="server" Width="" Visible="false"></asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btn_woload" runat="server" CssClass="SmallButton3" Text="查询" TabIndex="4">
                        </asp:Button>&nbsp;
                        <asp:Button ID="btn_export" runat="server" CssClass="SmallButton3" Text="导出" TabIndex="24">
                        </asp:Button>
                        <asp:Button ID="btn_edit1" runat="server" Width="60" CssClass="SmallButton3" Text="返回"
                            TabIndex="24"></asp:Button>
                        <asp:Button ID="btn_rct" runat="server" CssClass="SmallButton3" Text="保存" TabIndex="34">
                        </asp:Button>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp; 描述:
                        <asp:Label ID="lbl_desc" runat="server" Width="320"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;工序:<asp:DropDownList ID="dropWorkproc" runat="server" Width="360px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;日期<asp:TextBox ID="txtDate" runat="server" Width="70px" CssClass="Date"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="删除" Width="100px" CssClass="SmallButton3" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:DataGrid ID="Datagrid1" runat="server" 
            AutoGenerateColumns="False"  OnUpdateCommand="Edit_update"
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
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_duty" SortExpression="proc_duty" HeaderText="承担部门"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_nbr" SortExpression="proc_nbr" HeaderText="工单号"
                    ReadOnly="true">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_name" SortExpression="proc_name" HeaderText="工序"
                    ReadOnly="true">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_qty" SortExpression="proc_qty" HeaderText="数量" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_adj" SortExpression="proc_adj" HeaderText="调整">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price2" SortExpression="proc_price2" HeaderText="工序定额"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price1" SortExpression="proc_price1" HeaderText="工时"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_date_comp" SortExpression="wo_date_comp" HeaderText="日期"
                    ReadOnly="true">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_created" SortExpression="wo_created" HeaderText="创建人"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" HeaderText="编辑"
                    CancelText="<u>取消</u>" EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="70px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="proc_del">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="wo_line" SortExpression="wo_line" HeaderText="工段线" ReadOnly="true">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
