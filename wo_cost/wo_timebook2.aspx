<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.wo_timebook2" CodeFile="wo_timebook2.aspx.vb" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1002px">
            <tr>
                <td>
                    &nbsp;地点<asp:DropDownList ID="dd_site" runat="server" Width="50px">
                    </asp:DropDownList>
                    &nbsp; 工号<asp:TextBox ID="txtUserNo" runat="server" Width="50px"></asp:TextBox>&nbsp;
                    成本中心<asp:TextBox ID="txb_cc" runat="server" Width="70" TabIndex="2" Height="22"></asp:TextBox>&nbsp;
                    考勤日期<asp:TextBox ID="txb_date" runat="server" Width="70" TabIndex="3" Height="22"
                        MaxLength="10" CssClass="Date"></asp:TextBox>&nbsp; 上班时间<asp:TextBox ID="txb_start"
                            runat="server" Width="50" TabIndex="4" Height="22" MaxLength="4"></asp:TextBox>&nbsp;
                    下班时间<asp:TextBox ID="txb_end" runat="server" Width="50" TabIndex="5" Height="22"
                        MaxLength="4"></asp:TextBox>&nbsp; 休息小时<asp:TextBox ID="txb_rest" runat="server"
                            Width="50" TabIndex="6" Height="22"></asp:TextBox>&nbsp;
                </td>
                <td>
                    全月<asp:CheckBox ID="chkAll" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btn_woload" runat="server" CssClass="SmallButton3" Text="查询" TabIndex="10">
                    </asp:Button>&nbsp;
                    <asp:Button ID="btn_export" runat="server" CssClass="SmallButton3" Text="导出" TabIndex="24">
                    </asp:Button>
                    <asp:Button ID="btn_edit1" runat="server" CssClass="SmallButton3" Text="返回" TabIndex="24">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <table id="table3" cellspacing="0" cellpadding="0" width="1000px">
            <tr>
                <td>
                    用户组<asp:DropDownList ID="dd_group" runat="server" Width="220px" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp; 工号<asp:TextBox ID="txb_no" runat="server" Width="60" TabIndex="11" Height="22"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btn_add" runat="server" Width="60" CssClass="SmallButton3" Text="考勤汇报"
                        TabIndex="20"></asp:Button>&nbsp;
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="1000px" AllowPaging="true " PageSize="22"
            CssClass="GridViewStyle AutoPageSize" AutoGenerateColumns="False" OnUpdateCommand="Edit_update"
            OnCancelCommand="Edit_cancel">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="_id" SortExpression="_id" HeaderText="" Visible="false"
                    ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="r_No" HeaderText="序号" ReadOnly="true">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="user_no" HeaderText="工号" ReadOnly="true">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="user_name" HeaderText="姓名" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_cc" HeaderText="成本中心" ReadOnly="true">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_type" HeaderText="类型" ReadOnly="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_date" HeaderText="考勤日期" ReadOnly="true">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_start" HeaderText="上班时间">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_end" HeaderText="下班时间">
                    <HeaderStyle Width="70px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_work" SortExpression="_work" HeaderText="工作小时" ReadOnly="true">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_rest" HeaderText="休息小时">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_night" HeaderText="中夜班" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_created_date" HeaderText="日期" ReadOnly="true">
                    <HeaderStyle Width="55px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="55px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="_created" HeaderText="创建人" ReadOnly="true">
                    <HeaderStyle Width="45px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>保存</u>" CancelText="<u>取消</u>"
                    EditText="<u>编辑</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="&lt;u&gt;删除&lt;/u&gt;" CommandName="proc_del">
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
