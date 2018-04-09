<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WKpackage.aspx.vb" Inherits="wo_cost_WKpackage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
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
                <td colspan="2">
                    &nbsp;地点:<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                    </asp:DropDownList>
                    &nbsp; 生效日期:&nbsp;<asp:TextBox ID="txb_date" runat="server" Width="70" TabIndex="1"
                        Height="22"></asp:TextBox>&nbsp; 加工单号:&nbsp;<asp:TextBox ID="txb_wonbr" runat="server"
                            Width="90" TabIndex="2" Height="22"></asp:TextBox>&nbsp; ID:&nbsp;<asp:Label ID="lbl_ID"
                                runat="server" Width="70"></asp:Label>&nbsp; 成本中心:&nbsp;<asp:Label ID="lbl_cc" runat="server"
                                    Width="22"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lbl_cc1" runat="server" Width="0" Visible="false"></asp:Label>
                    入库总数:&nbsp;<asp:Label ID="lbl_comp" runat="server" Width="80"></asp:Label>&nbsp;
                    总工时:&nbsp;<asp:Label ID="lbl_hour" runat="server" Width="80"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_cost" runat="server" Width="80" Visible="false"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_price" runat="server" Width="" Visible="false"></asp:Label>&nbsp;
                    <asp:Label ID="lbl_type" runat="server" Width="" Visible="false"></asp:Label>&nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;零件号:
                    <asp:Label ID="lbl_part" runat="server" Width="100"></asp:Label>&nbsp; 工艺代码:<asp:Label
                        ID="lbl_rate" runat="server" Width="100"></asp:Label>&nbsp; 描述:
                    <asp:Label ID="lbl_desc" runat="server" Width="320"></asp:Label>&nbsp;
                </td>
                <td>
                    <asp:Button ID="btn_woload" runat="server" CssClass="SmallButton3" Text="查询" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_clear" runat="server" CssClass="SmallButton3" Text="清除" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton3" Text="返回" TabIndex="24"
                        Visible="false"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <table id="table3" cellspacing="0" cellpadding="0" width="1050px">
            <tr>
                <td>
                    用户组<asp:DropDownList ID="dd_group" runat="server" Width="120px">
                    </asp:DropDownList>
                    &nbsp; 工号<asp:TextBox ID="txb_no" runat="server" Width="60" TabIndex="11" Height="22"></asp:TextBox>&nbsp;
                    岗位<asp:DropDownList ID="dropPostion" runat="server" Width="100px" DataTextField="SOPValue"
                        DataValueField="SOPID">
                    </asp:DropDownList>
                    &nbsp; 工序<asp:TextBox ID="txb_proc" runat="server" Width="100px" TabIndex="11" Height="22"
                        Enabled="false"></asp:TextBox>&nbsp; 工序定额<asp:TextBox ID="txb_procprice" runat="server"
                            Width="60px" TabIndex="12" Height="22" Enabled="false"></asp:TextBox>&nbsp;
                    工序单价<asp:TextBox ID="txtUnitPrice" runat="server" Enabled="False" Height="22" TabIndex="13"
                        Width="60px"></asp:TextBox>
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" Text="增加" TabIndex="20"
                        ValidationGroup="Add"></asp:Button>&nbsp;
                </td>
                <td>
                    数量<asp:TextBox ID="txb_qty" runat="server" Width="60" TabIndex="13" Height="22"></asp:TextBox>&nbsp;
                    <asp:Button ID="btn_assign" runat="server" Width="60" CssClass="SmallButton3" Text="分配"
                        TabIndex="20" ValidationGroup="Save"></asp:Button>&nbsp;
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
                    <HeaderStyle Width="40px"></HeaderStyle>
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
                <asp:BoundColumn DataField="proc_nbr" SortExpression="proc_nbr" HeaderText="工单号"
                    ReadOnly="true">
                    <HeaderStyle Width="90px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_ID" SortExpression="proc_ID" HeaderText="ID" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_name" SortExpression="proc_name" HeaderText="工序"
                    ReadOnly="true">
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="分配比例" HeaderStyle-Width="50">
                    <ItemTemplate>
                        <asp:TextBox ID="txb_qty" runat="server" Text='<%# Bind("proc_rate") %>' Width="50px"
                            Height="18"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Width="50px"></HeaderStyle>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="proc_qty" SortExpression="proc_qty" HeaderText="数量" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_pcost" SortExpression="proc_pcost" HeaderText="系数"
                    ReadOnly="true" Visible="false">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_price" SortExpression="proc_price" HeaderText="工序定额"
                    ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_cost" SortExpression="proc_cost" HeaderText="工时"
                    ReadOnly="true" Visible="false">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
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
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_cost_unitPrice" SortExpression="wo_created" HeaderText="金额"
                    ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
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
