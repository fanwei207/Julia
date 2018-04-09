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
                    <asp:Label ID="lblAssetNo" runat="server" Text="�ʲ���ţ�"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAssetNo" runat="server" MaxLength="10" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblAssetName" runat="server" Text="�ʲ����ƣ�"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAssetName" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSpc" runat="server" Text="���"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSpc" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblType" runat="server" Text="���ͣ�"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropType" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblEntity" runat="server" Text="���˹�˾��"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropEntity" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDate" runat="server" Text="�������ڣ�"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtStdDate" runat="server" Width="71px" CssClass="smalltextbox Date"></asp:TextBox>
                    &nbsp; ��
                    <asp:TextBox ID="txtEndDate" runat="server" Width="71px" CssClass="smalltextbox Date"></asp:TextBox>
                </td>
                <td>
                    &nbsp;<asp:Label ID="lblVoucher" runat="server" Text="����ƾ֤��"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVoucher" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblCost" runat="server" Text="ԭֵ��"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCost" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSupplier" runat="server" Text="��Ӧ�̣�"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSupplier" runat="server" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReference" runat="server" Text="�������ݣ�"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReference" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblCode" runat="server" Text="�豸�����룺"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCode" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblComment" runat="server" Text="��ע��"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtComment" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td colspan="3">
                    &nbsp;<asp:Button ID="Button4" runat="server" Text="��ѯ" CssClass="SmallButton2" OnClick="Button1_Click" />
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
                <asp:BoundField DataField="AssetNo" HeaderText="�ʲ����">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="AssetName" HeaderText="�ʲ�����">
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_spec" HeaderText="���">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixtp_name" HeaderText="����">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Entity" HeaderText="���ʹ�˾">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="AssetDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="��������"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Voucher" HeaderText="����ƾ֤">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="AssetCost" HeaderText="ԭֵ">
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="Supplier" HeaderText="��Ӧ��">
                    <ItemStyle Width="200px" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_code" HeaderText="�豸������">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_reference" HeaderText="��������">
                    <ItemStyle Width="340px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_type" HeaderText="��������">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_user" HeaderText="������">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="��������"
                    HtmlEncode="False">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Comment" HeaderText="��ע" />
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
