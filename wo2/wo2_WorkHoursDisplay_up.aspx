<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_WorkHoursDisplay_up.aspx.cs"
    Inherits="wo2_WorkHoursDisplay_up" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" style="width: 1180px;">
            <tr>
                <td align="left">
                    日期:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtStart" runat="server" MaxLength="10" Width="80px" CssClass="Date"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtEnd" runat="server" MaxLength="10" Width="80px" CssClass="Date"></asp:TextBox>
                    <asp:CheckBox ID="chkClose" runat="server" Text="结算" />
                </td>
                <td>
                    部门:
                </td>
                <td>
                    <asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    工号:
                </td>
                <td>
                    <asp:TextBox ID="txtUserNo" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    姓名:
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td rowspan="1" align="center">
                    <asp:Button ID="btnSearch" runat="server" Width="60px" CssClass="SmallButton3" Text="查询"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    地点:
                </td>
                <td>
                    <asp:TextBox ID="txtSite" Width="80px" runat="server"></asp:TextBox>
                </td>
                <td>
                    成本中心:
                </td>
                <td>
                    <asp:TextBox ID="txtCenter" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>
                    加工单:
                </td>
                <td>
                    <asp:TextBox ID="txtWorkOrder" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>
                    ID号:
                </td>
                <td>
                    <asp:TextBox ID="txtID" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td rowspan="1" align="center">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle"
            runat="server" PageSize="21" Width="1180px" OnPageIndexChanging="gv_PageIndexChanging"
            OnRowCommand="gv_RowCommand" DataKeyNames="userID" OnRowCreated="gv_RowCreated">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="工号" DataField="userNo">
                    <ItemStyle Width="50px" ForeColor="Black" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName">
                    <ItemStyle Width="60px" ForeColor="Black" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="Department">
                    <ItemStyle Width="180px" ForeColor="Black" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工段" DataField="Workshop">
                    <ItemStyle Width="100px" ForeColor="Black" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:ButtonField HeaderText="Wo2(工时)" DataTextField="Wo2_cost" DataTextFormatString="{0:N2}"
                    CommandName="1">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:ButtonField>
                <asp:ButtonField HeaderText="Wo(工时)" DataTextField="Wo_cost" DataTextFormatString="{0:N2}"
                    CommandName="4">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:ButtonField>
                <asp:BoundField HeaderText="计划外(工时)" DataField="Wo_cost_unplan" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:ButtonField HeaderText="合计(工时)" DataTextField="cost_hours" DataTextFormatString="{0:N2}">
                    <ItemStyle ForeColor="Black" HorizontalAlign="Right" Width="80px" />
                </asp:ButtonField>
                <asp:ButtonField HeaderText="Wo2(单价)" DataTextField="wo2_cost_unitPrice" DataTextFormatString="{0:N2}"
                    CommandName="1">
                    <ItemStyle ForeColor="Black" HorizontalAlign="Right" Width="80px" />
                </asp:ButtonField>
                <asp:ButtonField HeaderText="Wo(单价)" DataTextField="wo_cost_unitPrice" DataTextFormatString="{0:N2}"
                    CommandName="4">
                    <ItemStyle ForeColor="Black" HorizontalAlign="Right" Width="80px" />
                </asp:ButtonField>
                <asp:BoundField HeaderText="计划外(单价)" DataField="wo_cost_unitPrice_unplan" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="合计(单价)" DataField="cost_unitPrice" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="调整金额" DataField="cost_adj" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工资合计" DataField="cost" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="80px" ForeColor="Black" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="1180px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="180px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Wo2(工时)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Wo(工时)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划外(工时)" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="合计(工时)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Wo2(单价)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Wo(单价)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划外(单价)" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="合计(单价)" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="调整金额" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工资合计" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        </form>
        <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
        </script>
    </div>
</body>
</html>
