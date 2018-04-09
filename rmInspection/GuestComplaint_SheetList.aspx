<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestComplaint_SheetList.aspx.cs" Inherits="rmInspection_GuestComplaint_SheetList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="complain.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 10px;" align="center">
            <table>
                <tr>
                    <td>投诉单号</td>
                    <td>
                        <asp:TextBox ID="txtNo" runat="server" Width="100px" CssClass="SmallTextBox5"></asp:TextBox>
                    </td>
                    <td>客户</td>
                    <td>
                        <asp:TextBox ID="txtGuest" runat="server" Width="100px" CssClass="SmallTextBox5"></asp:TextBox>
                    </td>
                    <%--            <td>订单号</td>
                <td>
                    <asp:TextBox ID="txtOrder" runat="server" Width="100px" CssClass="SmallTextBox5"></asp:TextBox>
                </td>--%>
                    <td>创建日期</td>
                    <td>
                        <asp:TextBox ID="txtCreateDate" runat="server" Width="100px" CssClass="SmallTextBox5 Date"></asp:TextBox>
                    </td>
                    <td>状态</td>
                    <td>
                        <asp:DropDownList ID="ddlSatus" runat="server" AutoPostBack="true">
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
                        <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="导出" OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                DataKeyNames="ID,GuestComplaintNo,GuestNo,GuestName,SeverityName,GuestLevel,ApproachNames,ReceivedDate,ProblemContent,createBy,createName,createDate,GuestComp_Staus
            "
                OnRowDataBound="gv_RowDataBound" Width="2300px"
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
                            <asp:TableCell HorizontalAlign="center" Text="客户代码" Width="150px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="客户名" Width="400px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="严重等级" Width="150px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="客户等级" Width="150px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="问题描述" Width="150px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="创建人" Width="150px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="创建日期" Width="150px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="投诉单号" Visible="true">
                        <ItemStyle HorizontalAlign="Center" Width="110px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkNo" CssClass="no" runat="server" Text='<%# Bind("GuestComplaintNo") %>' CommandArgument='<%# Bind("GuestComplaintNo") %>'
                                CommandName="ViewEdit" Style="text-decoration: solid"></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Underline="True" />
                        <HeaderStyle HorizontalAlign="Center" Width="110px"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="GuestNo" HeaderText="客户代码">
                        <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GuestName" HeaderText="客户名">
                        <HeaderStyle Width="250px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="250px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="PreNo" HeaderText="原订单">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>--%>
                    <asp:BoundField DataField="SeverityName" HeaderText="严重等级">
                        <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GuestLevel" HeaderText="客户等级">
                        <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ApproachNames" HeaderText="解决方式">
                        <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ReceivedDate" HeaderText="客诉接收日期">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProblemContent" HeaderText="问题描述">
                        <HeaderStyle Width="360px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="360px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createName" HeaderText="创建人">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="createDate" HeaderText="创建日期">
                        <HeaderStyle Width="110px" HorizontalAlign="Center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PreResult" HeaderText="初判结果">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PreResultName" HeaderText="处理人">
                        <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ResParty" HeaderText="责任方">
                        <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FinanceResult" HeaderText="财务结果">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FinanceResultName" HeaderText="处理人">
                        <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DecideApproach" HeaderText="赔偿方式">
                        <HeaderStyle Width="200px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DecideApproachName" HeaderText="处理人">
                        <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ManagerResult" HeaderText="总经理审批">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ManagerResultName" HeaderText="处理人">
                        <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FeedbackResult" HeaderText="反馈结果">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ShipPlanNo" HeaderText="出运单号">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ShipPartNo" HeaderText="物料号">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ShipNum" HeaderText="数量">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ShipDate" HeaderText="出运日期">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FeedbackResultName" HeaderText="处理人">
                        <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExecuteResult" HeaderText="结单结果">
                        <HeaderStyle Width="110px" HorizontalAlign="center" Font-Bold="False" />
                        <ItemStyle Width="110px" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExecuteName" HeaderText="处理人">
                        <HeaderStyle Width="120px" HorizontalAlign="Left" Font-Bold="False" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:ButtonField Text="Detail" HeaderText="明细" CommandName="Detail">
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
