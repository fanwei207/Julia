<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPOrderSummary.aspx.cs" Inherits="new_MPOrderSummary" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1000px" runat="server"
            class="main_top">
            <tr>
                <td>
                    日期&nbsp;<asp:TextBox ID="txtStart" runat="server" Width="80px" CssClass="SmallTextBox Date"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtEnd" runat="server" Width="100px" CssClass="SmallTextBox Date"></asp:TextBox>
                    &nbsp;部门<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                    &nbsp;分类<asp:DropDownList ID="dropType" runat="server" Width="100px">
                    </asp:DropDownList>
                    &nbsp;供应商<asp:TextBox ID="txtSupplier" runat="server" Width="100px"></asp:TextBox>
                    &nbsp;<asp:RadioButton ID="rbAll" runat="server" GroupName="Type" Checked="true" />
                    All
                    <asp:RadioButton ID="rbRe" runat="server" GroupName="Type" />Receive
                    <asp:RadioButton ID="rbFin" runat="server" GroupName="Type" />Finance &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="btnSearch" TabIndex="0" runat="server" Text="查询" CssClass="SmallButton3"
                        OnClick="btnSearch_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSummary" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="20" DataSourceID="obdsSum"
            Width="1000px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField HeaderText="申请人" DataField="AName" ReadOnly="true">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="dname" ReadOnly="true">
                    <ItemStyle Width="150px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="申请物品" DataField="Part">
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="分类" DataField="Ptype">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="数量" DataField="Quantity" DataFormatString="{0:N2}">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="申请日期" DataField="creatdate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="单价" DataField="Price" DataFormatString="{0:N2}">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商" DataField="Supplier">
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="状态" DataField="pname">
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both">
                    <asp:TableRow>
                        <asp:TableCell Text="申请人" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="部门" Width="200px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="申请物品" Width="350px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="分类" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="数量" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="申请日期" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="单价" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="供应商" Width="200px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="lblAid" runat="server" Visible="false" Text="0"></asp:Label>
        <asp:ObjectDataSource ID="obdsSum" runat="server" SelectMethod="MPSummary" TypeName="MinorP.MinorPurchase">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtStart" Name="strStart" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtEnd" Name="strEnd" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="dropDept" Name="intDept" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropType" Name="intPtype" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtSupplier" Name="strSP" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="rbAll" Name="blAll" PropertyName="Checked" Type="Boolean" />
                <asp:ControlParameter ControlID="rbRe" Name="blRe" PropertyName="Checked" Type="Boolean" />
                <asp:ControlParameter ControlID="rbFin" Name="blFin" PropertyName="Checked" Type="Boolean" />
                <asp:SessionParameter Name="intPlant" SessionField="plantcode" Type="Int32" />
                <asp:SessionParameter DefaultValue="" Name="intCreat" SessionField="uid" Type="Int32" />
                <asp:ControlParameter ControlID="lblAid" DefaultValue="0" Name="intAid" PropertyName="Text"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
</body>
</html>
