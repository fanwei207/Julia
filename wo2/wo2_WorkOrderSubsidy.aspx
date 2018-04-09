<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_WorkOrderSubsidy.aspx.cs"
    Inherits="wl_calendar" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="800">
            <tr>
                <td style="height: 23px">
                    部门<asp:DropDownList ID="ddl_dp" runat="server" Width="100px" AutoPostBack="True"
                        DataTextField="name" DataValueField="departmentID" OnSelectedIndexChanged="ddl_dp_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp; 工段<asp:DropDownList ID="ddl_ws" runat="server" Width="100px" AutoPostBack="True"
                        DataTextField="name" DataValueField="id" OnSelectedIndexChanged="ddl_ws_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp; &nbsp;成本中心<asp:TextBox ID="txb_workcenter" runat="server" Height="22" TabIndex="3"
                        Width="50"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; 工号<asp:TextBox ID="txb_userno" runat="server" Width="50" TabIndex="3"
                        Height="22"></asp:TextBox>
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;年<asp:TextBox ID="txb_year" runat="server"
                        Width="50" TabIndex="3" Height="22" MaxLength="4" Style="ime-mode: disabled"
                        onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"></asp:TextBox>
                    月<asp:TextBox ID="txb_month" runat="server" Width="30" TabIndex="3" Height="22" MaxLength="2"
                        Style="ime-mode: disabled" onkeypress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false;"></asp:TextBox>
                    <asp:CheckBox ID="chkIsValue" runat="server" AutoPostBack="True" OnCheckedChanged="chkIsValue_CheckedChanged"
                        Text="仅显示工时大于0" />
                </td>
                <td align="right" style="height: 23px">
                    <asp:Button ID="btn_search" runat="server" Width="60px" CssClass="SmallButton3" Text="查询"
                        TabIndex="4" OnClick="btn_search_Click"></asp:Button>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_export" runat="server" Width="60px" CssClass="SmallButton3" Text="导出"
                        TabIndex="4" OnClick="btn_export_Click"></asp:Button>&nbsp;&nbsp; &nbsp;&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWF" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="24" DataKeyNames="wo2_userID"
            Width="800px" OnPageIndexChanging="gvWF_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="795px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="160px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="160px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="160px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="160px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="工时" Width="160px" HorizontalAlign="Center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="wo2_userNo" HeaderText="工号">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="160px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="wo2_userName" HeaderText="姓名">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="160px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="deptName" HeaderText="部门">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="160px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="workshop" HeaderText="工段">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="160px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_subsidysum" HeaderText="工时">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="160px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
