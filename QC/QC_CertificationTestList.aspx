<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QC_CertificationTestList.aspx.cs" Inherits="QC_QC_CertificationTestList" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
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
                <td>认证检验单号</td>
                <td>
                    <asp:TextBox ID="txtNo" CssClass="SmallTextBox5 Param" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>工单号</td>
                <td>
                    <asp:TextBox ID="txtNbr" CssClass="SmallTextBox5 Param" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>ID</td>
                <td>
                    <asp:TextBox ID="txtLot" CssClass="SmallTextBox5 Param" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>检验结果</td>                
                <td>
                    <asp:DropDownList ID="ddlTestType" runat="server" CssClass="SmallTextBox5 Param">
                        <asp:ListItem Text="--全部--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不通过" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>状态</td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallTextBox5 Param">
                        <asp:ListItem Text="--全部--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="已保存" Value="0"></asp:ListItem>
                        <asp:ListItem Text="已完成" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton3" Text="新增" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            DataKeyNames="ID,QC_No,QC_Nbr,QC_Lot,QC_Part,QC_Domain,QC_Site,QC_Desc,QC_TestType,QC_TestDesc,Status,createBy,createName,createDate"                      
            AllowPaging="True" PageSize="20" OnRowDataBound="gv_RowDataBound" OnRowCommand="gv_RowCommand"
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
                <asp:BoundField DataField="QC_No" HeaderText="认证检验单号">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="工单号" Visible="true">
                    <ItemStyle HorizontalAlign="Center" Width="80px" Font-Underline="True" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkNbr" CssClass="no" runat="server" Text='<%# Bind("QC_Nbr") %>'
                            CommandName="ViewEdit"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="QC_Lot" HeaderText="ID">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="QC_Part" HeaderText="QAD">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="QC_Site" HeaderText="地点">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="QC_Domain" HeaderText="域">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                
                <asp:BoundField DataField="QC_Desc" HeaderText="描述">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="QC_TestType" HeaderText="检验结果">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <%--<asp:ButtonField Text="Detail" HeaderText="明细" CommandName="Detial">
                    <ControlStyle Font-Bold="False" Font-Underline="True" />
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>--%>
                <asp:BoundField DataField="Status" HeaderText="状态">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
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
