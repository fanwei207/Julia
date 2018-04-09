<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustComplaint_SheetList.aspx.cs" Inherits="EDI_CustComplaint_SheetList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客户投诉-投诉单列表</title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top:10px;" align="center">
        <table>
            <tr>
                <td>投诉单号</td>
                <td>
                    <asp:TextBox ID="txtNo" runat="server" Width="100px" CssClass="SmallTextBox5"></asp:TextBox>
                </td>
                <td>客户</td>
                <td>
                    <asp:TextBox ID="txtCust" runat="server" Width="100px" CssClass="SmallTextBox5"></asp:TextBox>
                </td>
                <td>订单号</td>
                <td>
                    <asp:TextBox ID="txtOrder" runat="server" Width="100px" CssClass="SmallTextBox5"></asp:TextBox>
                </td>
                <td>创建日期</td>
                <td>
                    <asp:TextBox ID="txtCreateDate" runat="server" Width="100px" CssClass="SmallTextBox5 Date"></asp:TextBox>
                </td>
                <td>状态</td>
                <td>
                    <asp:DropDownList ID="ddlSatus" runat="server">
                        <asp:ListItem Text="--全部--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已完结" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未完结" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton3" Text="新建" OnClick="btnNew_Click" />
                </td>
                   <td>
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出" OnClick="btnExport_Click"  />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            DataKeyNames="ID,CustComp_No,CustComp_Customer,CustComp_OrderID,CustComp_Describe,CustComp_Finance
            ,CustComp_FinanceBy,CustComp_FinanceName,CustComp_FinanceDate,CustComp_AfterSaleService
            ,CustComp_AfterSaleServiceBy,CustComp_AfterSaleServiceName,CustComp_AfterSaleServiceDate
            ,CustComp_Factory,CustComp_ResponsiblePerson,CustComp_Payment,CustComp_Staus,CustComp_DetModeifyBy
            ,CustComp_DetModeifyName,CustComp_DetModeifyDate,createBy,createName,createDate,CustComp_IDType
            ,CustComp_DeptStatus,CustComp_DeptStatusBy,CustComp_DeptStatusName,CustComp_DeptStatusDate
            ,CustComp_DateCode,CustComp_DueDate" OnRowDataBound="gv_RowDataBound"
            OnRowDeleting="gv_RowDeleting" OnRowCommand="gv_RowCommand"
            AllowPaging="False" PageSize="20">
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
                        <asp:TableCell HorizontalAlign="center" Text="投诉单号" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="客户" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="原订单" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Date Code" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Due Date" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="问题描述" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建人" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建日期" Width="150px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField HeaderText="投诉单号" Visible="true">
                    <ItemStyle HorizontalAlign="Center" Width="110px" Font-Underline="True" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkNo" CssClass="no" runat="server" Text='<%# Bind("CustComp_No") %>'
                            CommandName="ViewEdit"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px"></HeaderStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="CustComp_Customer" HeaderText="客户">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CustComp_OrderID" HeaderText="原订单">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CustComp_DateCode" HeaderText="Date Code">
                    <HeaderStyle Width="70px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CustComp_DueDate" HeaderText="Due Date">
                    <HeaderStyle Width="70px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="CustComp_Describe" HeaderText="问题描述">
                    <HeaderStyle Width="260px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="260px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="tot" HeaderText="赔付总计($)">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Left"  ForeColor="Red" />
                </asp:BoundField>
                <asp:BoundField DataField="createName" HeaderText="创建人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="createDate" HeaderText="创建日期">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField Text="Detial" HeaderText="明细" CommandName="Detial">
                    <ControlStyle Font-Bold="False" Font-Underline="True" />
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
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
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
