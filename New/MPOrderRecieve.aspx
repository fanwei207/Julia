<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MPOrderRecieve.aspx.cs" Inherits="new_MPOrderRecieve" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1000" runat="server" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    &nbsp;&nbsp;申请人<asp:TextBox ID="txt_uname" TabIndex="0" runat="server" Width="100px"
                        MaxLength="10"></asp:TextBox>&nbsp;&nbsp; 部门<asp:DropDownList ID="dropDept" runat="server"
                            Width="100px">
                        </asp:DropDownList>
                    &nbsp;&nbsp; 分类<asp:DropDownList ID="dropType" runat="server" Width="100px">
                    </asp:DropDownList>
                    &nbsp;&nbsp; 供应商<asp:TextBox ID="txtSupplier" runat="server" Width="120px"></asp:TextBox>&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" TabIndex="0" runat="server"
                        Text="查询" CssClass="SmallButton3" OnClick="btnSearch_Click"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvMPRv" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" runat="server" PageSize="20" DataSourceID="obdsR" DataKeyNames="Aid"
            Width="960px" OnRowCommand="gvMPRv_RowCommand">
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
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
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
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:ButtonField Text="收货" ItemStyle-ForeColor="black" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"
                    CommandName="1" />
                <asp:BoundField DataField="Pstatus" ReadOnly="True">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:BoundField DataField="userID" ReadOnly="True">
                    <ItemStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <FooterStyle CssClass="hidden" />
                </asp:BoundField>
                <asp:ButtonField Text="打印" ItemStyle-ForeColor="black" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"
                    CommandName="2" />
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
                        <asp:TableCell Width="40px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <br />
        <br />
        <table id="table2" cellspacing="0" cellpadding="0" width="800" style="font-size: 10pt"
            runat="server">
            <tr style="height: 23px" visible="false">
                <td style="height: 23px" colspan="2">
                    &nbsp;&nbsp;&nbsp; 申请人：&nbsp;<asp:Label ID="lblApper" runat="server" Width="100px"></asp:Label>
                    <asp:Label ID="lblAid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td style="height: 23px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 部门：&nbsp;<asp:Label ID="lblDept" runat="server"
                        Width="100px"></asp:Label>
                </td>
                <td style="height: 23px">
                    &nbsp;&nbsp;&nbsp;&nbsp;零件分类：&nbsp;<asp:Label ID="lblType" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数量：&nbsp;<asp:Label ID="lblQuantity"
                        runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;单价：&nbsp;<asp:Label
                        ID="lblprice" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td colspan="2">
                    零件描述：&nbsp;<asp:Label ID="lblPart" runat="server" Width="400px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;供应商：&nbsp;<asp:Label ID="lblSP" runat="server" Width="400px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总价：&nbsp;<asp:Label ID="lbltotal" runat="server"
                        Width="100px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <b>___________________________________________________________________</b>
                </td>
            </tr>
        </table>
        <br />
        <table id="table3" cellspacing="0" cellpadding="0" width="800" runat="server">
            <tr>
                <td>
                    收到数量：&nbsp;
                    <asp:TextBox ID="txtQtyR" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td visible="false">
                    接收部门：&nbsp;
                    <asp:DropDownList ID="dropDp" runat="server" Width="120px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 23px">
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnR" runat="server" Text="收货" CssClass="SmallButton2" OnClick="btnR_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton2" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="obdsR" runat="server" SelectMethod="MPRecieve" TypeName="MinorP.MinorPurchase">
            <SelectParameters>
                <asp:ControlParameter ControlID="txt_uname" Name="strUser" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="dropDept" Name="intDept" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropType" Name="intPtype" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtSupplier" Name="strSp" PropertyName="Text" Type="String" />
                <asp:SessionParameter Name="intPlant" SessionField="plantcode" Type="Int32" />
                <asp:SessionParameter Name="intUid" SessionField="uid" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
