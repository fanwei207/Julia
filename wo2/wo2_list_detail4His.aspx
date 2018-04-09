<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo2_list_detail4His"
    CodeFile="wo2_list_detail4His.aspx.vb" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td>
                    地点<asp:TextBox ID="txb_site" runat="server" Width="60" TabIndex="1" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    成本中心<asp:TextBox ID="txb_cc" runat="server" Width="60" TabIndex="2" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    加工单号<asp:TextBox ID="txb_wonbr" runat="server" Width="100" TabIndex="3" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    加工单ID<asp:TextBox ID="txb_woid" runat="server" Width="100" TabIndex="4" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    工艺代码<asp:TextBox ID="txb_part" runat="server" Width="120" TabIndex="5" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                </td>
            </tr>
            <tr>
                <td>
                    工单数量<asp:TextBox ID="txb_qty" runat="server" Width="80" TabIndex="10" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    工单标准<asp:TextBox ID="txb_a" runat="server" Width="80" TabIndex="11" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    完工入库<asp:TextBox ID="txb_comp" runat="server" Width="80" TabIndex="12" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    工单工时<asp:TextBox ID="txb_cost" runat="server" Width="80" TabIndex="13" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                    结算日期<asp:TextBox ID="txb_closedate" runat="server" Width="100" TabIndex="14" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td align="Left">
                    <asp:Button ID="btnModify" runat="server" Text="修改分配" Width="60px" CssClass="SmallButton3"
                        Visible="False" />
                </td>
            </tr>
            <tr>
                <td>
                    流水线<asp:TextBox ID="txb_line" runat="server" Width="100" TabIndex="20" ReadOnly="true"></asp:TextBox>&nbsp;
                </td>
                <td>
                </td>
                <td>
                </td>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btn_list" TabIndex="0" runat="server" CssClass="SmallButton3" Text="查询"
                        Visible="false"></asp:Button>&nbsp;
                    <asp:Button ID="btn_update" TabIndex="0" runat="server" CssClass="SmallButton3" Text="更新"
                        OnClientClick="javascript:return confirm('你确认要更新加工单的工时数据吗? ');" Visible="False">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" TabIndex="0" runat="server" CssClass="SmallButton3" Text="导出">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_back" TabIndex="0" runat="server" CssClass="SmallButton3" Text="返回">
                    </asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <table id="table2" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td>
                    <asp:Panel ID="Panel1" Style="overflow-x: auto" runat="server" Width="980px" HorizontalAlign="Left"
                        BorderWidth="1px" BorderColor="Black" Height="420px">
                        <asp:DataGrid ID="Datagrid1" runat="server" Width="2200px" BorderColor="#999999" 
                            AllowPaging="false" CssClass="GridViewStyle AutoPageSize" >
                            <ItemStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="proc_id"></asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="proc2_id"></asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_no" SortExpression="wo_no" HeaderText="工号">
                                    <HeaderStyle Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_name" SortExpression="wo_name" HeaderText="姓名">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_group" SortExpression="wo_group" HeaderText="用户组">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_proc" SortExpression="wo_proc" HeaderText="工序">
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_proc2" SortExpression="wo_proc2" HeaderText="工位">
                                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_procrate" SortExpression="wo_procrate" HeaderText="工位系数">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_rate" SortExpression="wo_rate" HeaderText="分配比例">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_std" SortExpression="wo_std" HeaderText="工序标准">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_cost" SortExpression="wo_cost" HeaderText="工序工时">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_qtycomp" SortExpression="wo_qtycomp" HeaderText="完工入库">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_create" SortExpression="wo_create" HeaderText="录入员">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_createdate" SortExpression="wo_createdate" HeaderText="创建日期">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_modi" SortExpression="wo_modi" HeaderText="修改人">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_modidate" SortExpression="wo_modidate" HeaderText="修改日期">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_line2" SortExpression="wo_line2" HeaderText="流水线"
                                    Visible="false">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_rate2" SortExpression="wo_rate2" HeaderText="系数">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_effdate" SortExpression="wo_effdate" HeaderText="生效日期">
                                    <HeaderStyle Width="60px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="wo_tec" SortExpression="wo_tec" HeaderText="工艺代码">
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblshow" runat="server" Font-Size="Medium" ForeColor="Red" Text="* 更新 表示更新该张加工单的所有汇报工序的标准工时 *"></asp:Label>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
