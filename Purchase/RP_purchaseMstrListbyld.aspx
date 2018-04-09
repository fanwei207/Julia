<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RP_purchaseMstrListbyld.aspx.cs" Inherits="Purchase_rp_purchaseMstrLise" %>

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
    <style type="text/css">
        .auto-style1 {
            width: 67px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>公司</td>
                <td>
                    <asp:DropDownList ID="ddlPlant" runat="server" CssClass="SmallTextBox5" AutoPostBack="True" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                        <asp:ListItem Text="SZX" Value="1"></asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="2"></asp:ListItem>
                        <asp:ListItem Text="YQL" Value="5"></asp:ListItem>
                        <asp:ListItem Text="HQL" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>业务部门</td>
                <td>
                    <asp:DropDownList ID="ddlBusDept" runat="server" CssClass="SmallTextBox5" DataTextField="departmentname" DataValueField="departmentid">
                    </asp:DropDownList>
                </td>
                 <td>申请人</td>
                <td class="auto-style1">
                   <asp:TextBox ID="txt_user" runat="server" Width="72px" ></asp:TextBox>
                </td>
                 <td>申请人部门</td>
                <td class="auto-style1">
                   <asp:TextBox ID="txt_userdept" runat="server" Width="72px" ></asp:TextBox>
                </td>
                 <td>采购申请</td>
                <td>
                   <asp:TextBox ID="txt_no" runat="server" Width="95px"></asp:TextBox>
                </td>
                <td>QAD</td>
                <td>
                  <asp:TextBox ID="txt_qad" runat="server" Width="96px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton2" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td>
                    
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            DataKeyNames="ID,rp_No,rp_BusinessDept,rp_BusinessDeptName,createBy,createName,createDate,rp_plantCode,name,Status"                      
            AllowPaging="True" PageSize="30" OnPageIndexChanging="gv_PageIndexChanging" OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound">
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
                        <asp:TableCell HorizontalAlign="center" Text="采购单号" Width="80px"></asp:TableCell>
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
                <asp:BoundField DataField="name" HeaderText="部门">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="采购申请单" Visible="true">
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
               
              
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
