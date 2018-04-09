<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fixas_maintainPlan.aspx.cs"
    Inherits="new_Fixas_maintainPlan" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
        <table style="margin: 0 auto; padding: 5px; width: 1000px;" border="0" cellpadding="0"
            cellspacing="0">
            <tr>
                <td style="width: 8%;">
                    <asp:Label ID="Label12" runat="server" Text="资产编号"></asp:Label>
                </td>
                <td style="width: 15%;">
                    <asp:TextBox ID="txbFixasNo" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                </td>
                <td style="width: 8%;">
                    <asp:Label ID="Label13" runat="server" Text="成本中心"></asp:Label>
                </td>
                <td style="width: 12%;">
                    <asp:DropDownList ID="dropCC" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 8%;">
                    <asp:Label ID="Label14" runat="server" Text="类型"></asp:Label>
                </td>
                <td style="width: 25%;">
                    <asp:DropDownList ID="dropTypes" runat="server" Width="100px" AutoPostBack="True"
                        OnSelectedIndexChanged="dropTypes_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="Label15" runat="server" Text="-"></asp:Label>
                    <asp:DropDownList ID="dropSubTypes" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 8%;">
                    <asp:Label ID="Label1" runat="server" Text="保养单"></asp:Label>
                </td>
                <td style="width: 15%;">
                    <asp:TextBox ID="txbMaintainOrder" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                </td>
                <td style="width: 8%;">
                    <asp:Label ID="Label2" runat="server" Text="保养状态"></asp:Label>
                </td>
                <td style="width: 12%;">
                    <asp:DropDownList ID="dropMaintainStatus" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="width: 9%;">
                    <asp:Label ID="Label3" runat="server" Text="计划保养时间"></asp:Label>
                </td>
                <td style="width: 24%;">
                    <asp:TextBox ID="txbMaintainDate1" runat="server" CssClass="SmallTextBox Date" 
                        Width="100px"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="txbMaintainDate2" runat="server" CssClass="SmallTextBox Date" 
                        Width="100px"></asp:TextBox>
                </td>
                <td style="width: 12%;">
                    <asp:Label ID="Label5" runat="server" Text="总计:"></asp:Label>
                    <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 12%;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="70px"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvMaintainPlan" runat="server" AutoGenerateColumns="False" Width="1600px"
            CssClass="GridViewStyle" AllowPaging="True" PageSize="25" GridLines="Vertical"
            OnPageIndexChanging="gvMaintainPlan_PageIndexChanging" OnRowCommand="gvMaintainPlan_RowCommand"
            OnRowDataBound="gvMaintainPlan_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1600px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="保养状态" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养单" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="资产编号" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="资产名称" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="供应商" Width="114px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="计划保养时间" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="保养描述" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="所在公司" Width="114px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="成本中心" Width="114px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="规格" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="类型" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="详细类型" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="入账公司" Width="114px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="入账日期" HorizontalAlign="Center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="ltnExport" Text="<u>导出</u>" ForeColor="Blue" Font-Size="12px" runat="server"
                            CommandArgument='<%# Eval("MaintainOrder") %>' CommandName="myExport" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="ltnDelete" Text="<u>删除</u>" ForeColor="Blue" Font-Size="12px" runat="server"
                            CommandArgument='<%# Eval("MaintainOrder") %>' CommandName="myDelete" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="MaintainStatus" HeaderText="保养状态">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="Label6" runat="server" Text="保养单"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="ltnMaintainOrder" Text='<%#Bind("MaintainOrder") %>' ForeColor="Blue"
                            Width="100px" Font-Size="12px" runat="server" CommandArgument='<%# Eval("MaintainOrder") %>'
                            CommandName="myMaintainOrder" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        资产编号
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.FixasNo")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        资产名称
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.FixasName")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        供应商
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.FixasSupplier")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="180px" />
                </asp:TemplateField>
                <asp:BoundField DataField="PlanMaintainDate" HeaderText="计划保养时间">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="MaintainDesc" HeaderText="保养描述">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        所在公司
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.Domain")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        成本中心
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.CC")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        规格
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.FixasDesc")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        类型
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.FixasType")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        详细类型
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.FixasSubType")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        入账公司
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.FixasEntity")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        入账日期
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "FixasInfo.FixasVouDate")%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
