<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixAssetView.aspx.cs" Inherits="new_FixAssetView" %>

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
                <td style="width: 62px">
                    <asp:Label ID="lblAssetNo" runat="server" Text="资产编号："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAssetNo" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                </td>
                <td style="width: 73px">
                    <asp:Label ID="lblAssetName" runat="server" Text="资产名称："></asp:Label>
                </td>
                <td style="width: 116px">
                    <asp:TextBox ID="txtAssetName" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td style="width: 66px">
                    <asp:Label ID="lblSpc" runat="server" Text="规格："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSpc" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 41px">
                    <asp:Label ID="lblType" runat="server" Text="类型："></asp:Label>
                </td>
                <td style="width: 180px">
                    <asp:DropDownList ID="dropType" runat="server" Width="70px"
                        AutoPostBack="true" onselectedindexchanged="dropType_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropDetailType" runat="server" Width="100px" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td style="width: 70px">
                    <asp:Label ID="lblEntity" runat="server" Text="入账公司："></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropEntity" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 62px">
                    <asp:Label ID="lblDate" runat="server" Text="入账日期："></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStdDate" runat="server" CssClass="SmallTextBox Date" Width="71px"></asp:TextBox>
                    &nbsp; —
                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="SmallTextBox Date" Width="71px"></asp:TextBox>
                </td>
                <td style="width: 66px">
                    &nbsp;<asp:Label ID="lblVoucher" runat="server" Text="入账凭证："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVoucher" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 41px">
                    <asp:Label ID="lblCost" runat="server" Text="原值："></asp:Label>
                </td>
                <td style="width: 180px">
                    <asp:TextBox ID="txtCost" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td style="width: 70px">
                    <asp:Label ID="lblSupplier" runat="server" Text="供应商："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSupplier" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 62px">
                    <asp:Label ID="lblReference" runat="server" Text="估价依据："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReference" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td style="width: 73px">
                    <asp:Label ID="lblCode" runat="server" Text="设备部编码："></asp:Label>
                </td>
                <td style="width: 116px">
                    <asp:TextBox ID="txtCode" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td style="width: 66px">
                    <asp:Label ID="lblComment" runat="server" Text="备注："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtComment" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td style="width: 41px">
                </td>
                <td colspan="3">
                    &nbsp;<asp:Button ID="Button4" runat="server" Text="查询" CssClass="SmallButton2" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAsset" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            DataKeyNames="AssetNo,isInc" PageSize="17" DataSourceID="obdsAsset" Width="2170px"
            OnRowCommand="MyRowCommand" CssClass="GridViewStyle GridViewRebuild">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:ButtonField CommandName="Edit" DataTextField="AssetNo" HeaderText="资产编号" ShowHeader="True">
                    <ControlStyle Font-Underline="True" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:ButtonField>
                <asp:BoundField HeaderText="资产名称" DataField="AssetName">
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="规格" DataField="fixas_spec">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="类型" DataField="fixtp_name">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="详细类型" DataField="fixtp_det_name">
                    <ItemStyle Width="120px" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="入帐公司" DataField="Entity">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="入帐日期" DataField="AssetDate" HtmlEncode="False" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="入帐凭证" DataField="Voucher">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="原值" DataField="AssetCost">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                 <asp:BoundField HeaderText="保养周期" DataField="fixas_maintenancePeriod">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商" DataField="Supplier">
                    <ItemStyle Width="200px" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField HeaderText="设备部编码" DataField="fixas_code">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="估价依据" DataField="fixas_reference">
                    <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="Comment"></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="obdsAsset" runat="server" SelectMethod="GetFixAsset" TypeName="TCPNEW.GetDataTcp">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtAssetNo" Name="AssetNo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="txtAssetName" Name="AssetName" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtSpc" Name="AssetSpec" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="dropType" DefaultValue="0" Name="TypeID" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="dropDetailType" DefaultValue="0" Name="DetailTypeID"
                    PropertyName="SelectedValue" Type="Int32" />
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
