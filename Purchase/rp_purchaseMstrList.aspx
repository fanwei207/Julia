<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rp_purchaseMstrList.aspx.cs" Inherits="Purchase_rp_purchaseMstrLise" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>公司</td>
                <td>
                    <asp:DropDownList ID="ddlPlant" runat="server" CssClass="SmallTextBox5 Param" AutoPostBack="True" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                        <asp:ListItem Text="SZX" Value="1"></asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="2"></asp:ListItem>
                        <asp:ListItem Text="YQL" Value="5"></asp:ListItem>
                        <asp:ListItem Text="HQL" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>采购申请编号</td>
                <td>
                    <asp:TextBox ID="txtQAD" CssClass="SmallTextBox5 Param" Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>部门</td>
                <td>
                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="Param" Width="115px"></asp:DropDownList>
                </td>
                <td>业务部门</td>
                <td>
                    <asp:DropDownList ID="ddlBusDept" runat="server" CssClass="SmallTextBox5 Param" DataTextField="departmentname" DataValueField="departmentid">
                    </asp:DropDownList>
                </td>
                <td>流程</td>
                <td>
                    <asp:DropDownList ID="ddlProc" runat="server" CssClass="SmallTextBox5 Param">
                        <asp:ListItem Text="--全部--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="待部门主管签字" Value="1"></asp:ListItem>
                        <asp:ListItem Text="待业务人员操作" Value="2"></asp:ListItem>
                        <asp:ListItem Text="待业务主管签字" Value="3"></asp:ListItem>
                        <asp:ListItem Text="待设备部签字" Value="4"></asp:ListItem>
                        <asp:ListItem Text="待询价" Value="5"></asp:ListItem>
                        <asp:ListItem Text="待副总签字" Value="6"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="txtNew" runat="server" CssClass="SmallButton2" Text="新增" OnClick="txtNew_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            DataKeyNames="ID,rp_No,rp_BusinessDept,rp_BusinessDeptName,createBy,createName,createDate,rp_plantCode,rp_deptID,rp_deptName,Status,supplierType"                      
            AllowPaging="True" PageSize="20" OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound"
             OnRowDeleting="gv_RowDeleting" OnPageIndexChanging="gv_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="公司" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="部门" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="采购申请编号" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="业务部门" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请人" Width="125px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="申请日期" Width="125px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="rp_plantCode" HeaderText="公司">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="rp_deptName" HeaderText="部门">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="采购申请编号" Visible="true">
                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Underline="True" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkNo" CssClass="no" runat="server" Text='<%# Bind("rp_No") %>'
                            CommandName="ViewEdit"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="rp_BusinessDeptName" HeaderText="业务部门">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="createName" HeaderText="申请人">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="createDate" HeaderText="申请日期">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField Text="Detail" HeaderText="明细" CommandName="Detial">
                    <ControlStyle Font-Bold="False" Font-Underline="True" />
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:BoundField DataField="Status" HeaderText="状态">
                    <HeaderStyle Width="170px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="170px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="删除">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                            CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
