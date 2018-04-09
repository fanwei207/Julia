<%@ Page Language="VB" AutoEventWireup="false" CodeFile="wo_WKspec.aspx.vb" Inherits="wo_cost_wo_WKspec" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1050px">
            <tr>
                <td>
                    &nbsp;地点<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                    </asp:DropDownList>
                    &nbsp; 完工日期<asp:TextBox ID="txb_date" runat="server" Width="80" TabIndex="1" Height="22"
                        CssClass="Date"></asp:TextBox>&nbsp; 加工单号<asp:TextBox ID="txb_wonbr" runat="server"
                            Width="90" TabIndex="2" Height="22"></asp:TextBox>&nbsp; 成本中心:
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
                    <asp:Button ID="btn_clear" runat="server" CssClass="SmallButton3" Text="清除" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" Text="返回" TabIndex="24">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_rct" runat="server" CssClass="SmallButton3" Text="保存" TabIndex="34"
                        ValidationGroup="Other"></asp:Button>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; 描述:
                    <asp:Label ID="lbl_desc" runat="server" Width="320"></asp:Label>&nbsp;
                </td>
            </tr>
        </table>
        <table id="table3" cellspacing="0" cellpadding="0" width="1050px">
            <tr>
                <td>
                    线<asp:DropDownList ID="dd_line" runat="server" Width="50px" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp; 用户组<asp:DropDownList ID="dd_group" runat="server" Width="120px">
                    </asp:DropDownList>
                    &nbsp; 工号<asp:TextBox ID="txb_no" runat="server" Width="50px" TabIndex="11" Height="22"></asp:TextBox>
                    工序<asp:TextBox ID="txb_proc" runat="server" Width="170" TabIndex="11" Height="22"></asp:TextBox>
                    工序定额<asp:TextBox ID="txb_procprice" runat="server" Width="50px" TabIndex="12" Height="22"
                        CssClass="Numeric"></asp:TextBox>
                    工序单价<asp:TextBox ID="txtUnitPrice" runat="server" Height="22" TabIndex="13" Width="50px"
                        CssClass="Numeric"></asp:TextBox><asp:Button ID="btn_add" runat="server" CssClass="SmallButton3"
                            Text="增加" TabIndex="20" ValidationGroup="Add"></asp:Button>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    数量<asp:TextBox ID="txb_qty" runat="server" Width="60" TabIndex="13" Height="22" CssClass="Numeric"></asp:TextBox>&nbsp;<asp:Button
                        ID="btn_assign" runat="server" CssClass="SmallButton3" Text="分配" TabIndex="20"
                        ValidationGroup="Save"></asp:Button>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_save" runat="server" CssClass="SmallButton3" Text="保存" TabIndex="24">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1050px" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="22" CssClass="GridViewStyle AutoPageSize">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="sid" HeaderText="序号" ReadOnly="true" SortExpression="sid">
                    <HeaderStyle Width="40px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
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
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="数量" HeaderStyle-Width="50">
                    <ItemTemplate>
                        <asp:TextBox ID="txb_qty" runat="server" Text='<%# Bind("proc_qty") %>' Width="50px"
                            Height="18"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Width="50px"></HeaderStyle>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="proc_pri" SortExpression="proc_pri" HeaderText="定额" ReadOnly="true"
                    Visible="false">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_adj" SortExpression="proc_adj" HeaderText="调整" Visible="false"
                    ReadOnly="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price2" SortExpression="proc_price2" HeaderText="工序定额"
                    ReadOnly="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price1" SortExpression="proc_price1" HeaderText="工时"
                    ReadOnly="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
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
                <asp:BoundColumn DataField="proc_unitPrice" SortExpression="wo_created" HeaderText="单价"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_cost_unitPrice" SortExpression="wo_created" HeaderText="总额"
                    ReadOnly="true">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;保存&lt;/u&gt;" CommandName="proc_save">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>删除</u>" CommandName="proc_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
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
