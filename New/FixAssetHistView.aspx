<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetHistView.aspx.cs"
    Inherits="new_FixAssetHistView" %>

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
    <form id="Form1" runat="server">
    <div align="left">
        <table width="1000px">
            <tr>
                <td>
                    <asp:Label ID="lblAssetNo" runat="server" Text="资产编号："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAssetNo" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblAssetName" runat="server" Text="资产名称："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAssetName" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSpc" runat="server" Text="规格："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSpc" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblType" runat="server" Text="类型："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropType" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblEntity" runat="server" Text="入账公司："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropEntity" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDate" runat="server" Text="入账日期："></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStdDate" runat="server" Width="71px" CssClass="smalltextbox Date"></asp:TextBox>
                    &nbsp; —
                    <asp:TextBox ID="txtEndDate" runat="server" Width="71px" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<asp:Label ID="lblVoucher" runat="server" Text="入账凭证："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVoucher" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblCost" runat="server" Text="原值："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCost" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSupplier" runat="server" Text="供应商："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSupplier" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReference" runat="server" Text="估价依据："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReference" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblCode" runat="server" Text="设备部编码："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblComment" runat="server" Text="备注："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtComment" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td colspan="3">
                    &nbsp;<asp:Button ID="Button4" runat="server" Text="查询" CssClass="SmallButton2" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAsset" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="AssetNo" DataSourceID="obdsAssetHist" PageSize="17" Width="2000px"
            CssClass="GridViewStyle AutoPageSize" OnRowDataBound="gvAsset_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="AssetNo" HeaderText="资产编号">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="AssetName" HeaderText="资产名称">
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_spec" HeaderText="规格">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_name" HeaderText="类型">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Entity" HeaderText="入帐公司">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="AssetDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="入帐日期"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Voucher" HeaderText="入帐凭证">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="AssetCost" HeaderText="原值">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Supplier" HeaderText="供应商">
                    <ItemStyle Width="200px" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_code" HeaderText="设备部编码">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_reference" HeaderText="估价依据">
                    <ItemStyle Width="340px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_type" HeaderText="操作类型">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_user" HeaderText="操作人">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="操作日期"
                    HtmlEncode="False">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Comment" HeaderText="备注" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsAssetHist" runat="server" SelectMethod="GetFixAssetHist"
            TypeName="TCPNEW.GetDataTcp">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtAssetNo" Name="AssetNo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtAssetName" Name="AssetName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtSpc" Name="AssetSpec" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="dropType" DefaultValue="0" Name="TypeID" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropEntity" DefaultValue="0" Name="EntityID" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtStdDate" DefaultValue="" Name="StdDate" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtEndDate" Name="EndDate" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtVoucher" Name="Voucher" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtCost" Name="ACost" PropertyName="Text" Type="Decimal" />
                <asp:ControlParameter ControlID="txtSupplier" Name="ASupplier" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtReference" Name="AReference" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtCode" Name="ACode" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtComment" Name="AComment" PropertyName="Text"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
