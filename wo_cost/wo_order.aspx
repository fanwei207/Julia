<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_order" CodeFile="wo_order.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td>
                    地点<asp:DropDownList ID="dd_site" runat="server" Width="60px" AutoPostBack="true">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 提出部门<asp:DropDownList ID="dd_cc1" runat="server" Width="125px">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 加工部门<asp:DropDownList ID="dd_cc2" runat="server" Width="125px">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 承担部门<asp:DropDownList ID="dd_cc3" runat="server" Width="125px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_list" runat="server" CssClass="SmallButton3" Text="查询" TabIndex="4">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" runat="server" CssClass="SmallButton3" Text="导出" TabIndex="24">
                    </asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSup" runat="server" AutoPostBack="true" Width="125px"></asp:TextBox>
                    &nbsp;&nbsp; 承担供应商<asp:DropDownList ID="dd_supplier" runat="server" Width="180px">
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    加工单号<asp:TextBox ID="txb_nbr" runat="server" Width="90" TabIndex="3" Height="22"
                        MaxLength="12"></asp:TextBox>&nbsp; 类型<asp:DropDownList ID="dd_type" runat="server"
                            Width="90px">
                            <asp:ListItem Selected="True" Value="0" Text="--"></asp:ListItem>
                            <asp:ListItem Value="A" Text="计划外用工-A"></asp:ListItem>
                        </asp:DropDownList>
                    &nbsp; 数量<asp:TextBox ID="txb_qty" runat="server" Width="90" TabIndex="3" Height="22"
                        CssClass="Numeric"></asp:TextBox>&nbsp;
                    <asp:TextBox ID="txb_rtfile" runat="server" Width="425" TabIndex="4" Height="22"
                        Visible="false"></asp:TextBox>&nbsp;
                </td>
                <td>
                    <asp:Label ID="lbl_domain" runat="server" Width="0" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    工艺要求<asp:TextBox ID="txb_req" runat="server" Width="780" TabIndex="3" Height="22"
                        MaxLength="250"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_add" runat="server" CssClass="SmallButton3" Text="增加" TabIndex="8"
                        ValidationGroup="Add"></asp:Button>&nbsp;
                    <asp:Button ID="btn_cancel" runat="server" CssClass="SmallButton3" Text="取消" TabIndex="14">
                    </asp:Button>
                    <asp:Label ID="lbl_id" runat="server" Width="0" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1230px" AutoGenerateColumns="False"
            AllowPaging="true" PageSize="20" CssClass="GridViewStyle">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="id" SortExpression="id" HeaderText="" Visible="false" />
                <asp:BoundColumn DataField="wo_site" SortExpression="wo_site" HeaderText="地点">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cc1" SortExpression="wo_cc1" HeaderText="提出部门">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cc2" SortExpression="wo_cc2" HeaderText="加工部门">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_nbr" SortExpression="wo_nbr" HeaderText="加工单号">
                    <HeaderStyle Width="75px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="75px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_type" SortExpression="wo_type" HeaderText="类型">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_qty" SortExpression="wo_qty" HeaderText="数量">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_req" SortExpression="wo_req" HeaderText="工艺要求">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="&lt;u&gt;编辑&lt;/u&gt;" CommandName="proc_edit">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="proc_del">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="&lt;u&gt;打印&lt;/u&gt;" CommandName="proc_prn">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="proc_by" SortExpression="proc_by" HeaderText="创建">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="proc_date" SortExpression="proc_date" HeaderText="日期">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_cc_duty" SortExpression="wo_cc_duty" HeaderText="承担部门">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_supplier" SortExpression="wo_supplier" HeaderText="承担供应商">
                    <HeaderStyle Width="150px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_supplier" Visible="false" />
                <asp:BoundColumn DataField="wo_rtfile" Visible="false" />
                <asp:ButtonColumn Text="&lt;u&gt;附件&lt;/u&gt;" CommandName="proc_attach">
                    <HeaderStyle Width="30px"></HeaderStyle>
                    <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
