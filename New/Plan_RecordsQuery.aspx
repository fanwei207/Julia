<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan_RecordsQuery.aspx.cs"
    Inherits="Plan_RecordsQuery" %>

<%@ Import Namespace="Portal.Fixas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 985px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    <asp:TextBox ID="txtFixasNo" runat="server" CssClass="SmallTextBox4" TabIndex="1"
                        Width="87px"></asp:TextBox><asp:TextBox ID="txtFixasName" runat="server" CssClass="SmallTextBox4"
                            TabIndex="1" Width="133px"></asp:TextBox><asp:TextBox ID="txtFixasDesc" runat="server"
                                CssClass="SmallTextBox4" TabIndex="1" Width="66px"></asp:TextBox><asp:DropDownList
                                    ID="dropType" runat="server" Width="68px" DataTextField="fixtp_name" DataValueField="fixtp_id">
                                </asp:DropDownList>
                    <asp:DropDownList ID="dropEntity" runat="server" Width="66px" DataTextField="enti_name"
                        DataValueField="enti_id">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtFixasVouDate" runat="server" CssClass="SmallTextBox4 Date" TabIndex="1"
                        Width="66px"></asp:TextBox><asp:TextBox ID="txtFixasSupplier" runat="server" CssClass="SmallTextBox4"
                            TabIndex="1" Width="134px"></asp:TextBox><asp:TextBox ID="txtMaintor" runat="server"
                                CssClass="SmallTextBox4" TabIndex="1" Width="67px"></asp:TextBox><asp:TextBox ID="txtMaintDate"
                                    runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="65px"></asp:TextBox><asp:DropDownList
                                        ID="dropRepairItem" runat="server" DataTextField="Name" DataValueField="ID" Width="165px">
                                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        OnClick="btnQuery_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="21" OnPreRender="gvRDW_PreRender" DataKeyNames="ID"
            OnRowDataBound="gvRDW_RowDataBound" OnPageIndexChanging="gvRDW_PageIndexChanging"
            Width="985px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="985px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="资产编号" Width="80px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="资产名称" Width="120px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="规格" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="类型" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="入账公司" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="入账日期" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="供应商" Width="120px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="维修人" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="维修日期" Width="60px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="维护项目" Width="150px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="维护金额" Width="55px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="FixasNo" HeaderText="资产编号" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasName" HeaderText="资产名称" ReadOnly="True">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasDesc" HeaderText="规格" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasType" HeaderText="类型" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasEntity" HeaderText="入账公司" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasVouDate" HeaderText="入账日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FixasSupplier" HeaderText="供应商" ReadOnly="True">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="维护人">
                    <ItemTemplate>
                        <asp:Label ID="lbMaintor" runat="server" Text="<%# ((User)((Record)Container.DataItem).Maintor).Name %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="维护日期">
                    <ItemTemplate>
                        <asp:Label ID="lbMaintedDate" runat="server" Text="<%# ((User)((Record)Container.DataItem).Maintor).Date %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="维护项目">
                    <ItemTemplate>
                        <asp:Label ID="lbRepairItem" runat="server" Text="<%# ((RepairItem)((Record)Container.DataItem).RepairItem).Name %>"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Money" HeaderText="维护金额">
                    <HeaderStyle Width="55px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="55px" HorizontalAlign="Right" />
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
