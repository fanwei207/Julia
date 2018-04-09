<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FixIncMaintenance.aspx.cs"
    Inherits="FixIncMaintenance" %>

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
    <div align="center">
        <table id="tbFixAsset" runat="server" cellpadding="0" cellspacing="0">
            <%-- Start Asset --%>
            <tr id="trAsset1" runat="server">
                <td align="right">
                    <asp:Label ID="lblAssetNo" runat="server" Text="�ʲ���ţ�"></asp:Label>
                </td>
                <td style="width: 215px">
                    <asp:TextBox ID="txtAssetNo" runat="server" AutoPostBack="true" OnTextChanged="txtAssetNo_Changed"
                        MaxLength="8" TabIndex="1"></asp:TextBox>
                </td>
                <td colspan="4">
                    <asp:Label ID="lblAssetID" runat="server" Visible="false" Text="0"></asp:Label>
                </td>
            </tr>
            <tr id="trAsset2" runat="server">
                <td align="right">
                    <asp:Label ID="lblAssetName" runat="server" Text="�ʲ����ƣ�"></asp:Label>
                </td>
                <td style="width: 215px">
                    <asp:TextBox ID="txtAssetName" runat="server" TabIndex="2"></asp:TextBox>
                </td>
                <td align="right" style="width: 132px">
                    <asp:Label ID="lblAssetSpec" runat="server" Text="���"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAssetSpec" runat="server" TabIndex="3"></asp:TextBox>
                </td>
                <td align="right" style="width: 116px">
                    <asp:Label ID="lblType" runat="server" Text="���ͣ�"></asp:Label>
                </td>
                <td style="width: 234px">
                    <asp:DropDownList ID="dropType" runat="server" Width="80px" TabIndex="4" OnTextChanged="dropType_changed"
                        AutoPostBack="true"  >
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropDetailType" runat="server" Width="140px" TabIndex="4">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trAsset3" runat="server">
                <td align="right">
                    <asp:Label ID="lblEntity" runat="server" Text="���˹�˾��"></asp:Label>
                </td>
                <td style="width: 215px">
                    <asp:DropDownList ID="dropEntity" runat="server" Width="140px" TabIndex="5">
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 132px">
                    <asp:Label ID="lblVoucher" runat="server" Text="����ƾ֤��"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtVoucher" runat="server" TabIndex="6"></asp:TextBox>
                </td>
                <td align="right" style="width: 116px; height: 24px;">
                    <asp:Label ID="lblVdate" runat="server" Text="�������ڣ�"></asp:Label>
                </td>
                <td style="width: 234px; height: 24px;">
                    <asp:TextBox ID="txtVouchDate" runat="server" onpaste="return false" TabIndex="7"
                        CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
            </tr>
            <tr id="trAsset4" runat="server">
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblCost" runat="server" Text="ԭֵ��"></asp:Label>
                </td>
                <td style="width: 215px; height: 24px;">
                    <asp:TextBox ID="txtCost" runat="server" TabIndex="8"></asp:TextBox>
                </td>
                <td align="right" style="width: 132px; height: 24px;">
                    <asp:Label ID="lblSupplier" runat="server" Text="��Ӧ�̣�"></asp:Label>
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtSupplier" runat="server" TabIndex="9"></asp:TextBox>
                </td>
                <td align="right" style="width: 116px; height: 24px">
                    <asp:Label ID="lblReference" runat="server" Text=" �������ݣ�"></asp:Label>
                </td>
                <td style="width: 234px; height: 24px">
                    <asp:TextBox ID="txtReference" runat="server" TabIndex="10"></asp:TextBox>
                </td>
            </tr>
            <tr id="trAsset7" runat="server">
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblDepreciation" runat="server" Text="�۾����ޣ�"></asp:Label>
                </td>
                <td style="width: 215px; height: 24px">
                    <asp:TextBox ID="txtDiscount" runat="server" TabIndex="12"></asp:TextBox>(���¼���)
                </td>
                <td align="right" style="width: 132px; height: 24px">
                    <asp:Label ID="lblMachineCode" runat="server" Text="�豸�����룺"></asp:Label>
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtMachineCode" runat="server" TabIndex="12"></asp:TextBox>
                </td>
            </tr>
            <tr id="trAsset5" runat="server">
                <td align="right">
                    <asp:Label ID="lblComment" runat="server" Text="��ע��"></asp:Label>
                </td>
                <td style="width: 215px">
                    <asp:TextBox ID="txtcomment" runat="server" Height="100px" TabIndex="11"></asp:TextBox>
                </td>
                <td align="right" style="width: 132px">
                    &nbsp;
                </td>
                <td colspan="2" valign="top">
                    <table>
                        <tr>
                            <td>
                                <a id="thickbox" class="thickbox" href="" runat="server">
                                    <asp:Image ID="imgAsset" runat="server" Height="79px" Width="92px" ToolTip="����鿴ԭͼ"
                                        ImageUrl="~/images/fixset/nopic.PNG" /></a>
                            </td>
                            <td style="width: 1px" valign="bottom">
                                <asp:CheckBox ID="chkDelImg" runat="server" Text="ɾ��ͼƬ" Width="113px" Visible="False" />
                            </td>
                        </tr>
                    </table>
                    <input id="File" runat="server" style="width: 261px" type="file" visible="false" />
                </td>
                <td align="center" style="width: 234px">
                </td>
            </tr>
            <tr id="trAsset6" runat="server">
                <td align="right">
                    &nbsp;
                </td>
                <td style="width: 215px">
                </td>
                <td colspan="4" align="center">
                    <asp:Button ID="btnSaveAsset" runat="server" Width="80px" CssClass="SmallButton2"
                        Text="" OnClick="btnSaveAsset_Click" ValidationGroup="assetA" OnClientClick="javascript:return CheckAsset();"
                        TabIndex="13" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnEditAsset" runat="server" Width="80px" CssClass="SmallButton2"
                        Text="�༭" CausesValidation="false" Visible="false" OnClick="btnEditAsset_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnDelAsset" runat="server" Width="80px" CssClass="SmallButton2"
                        Text="ɾ��" OnClientClick="javascript:return confirm('��ȷ��Ҫɾ���� ');" CausesValidation="false"
                        Visible="false" OnClick="btnDelAsset_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnBackAsset" runat="server" Width="80px" CssClass="SmallButton2"
                        Text="����" CausesValidation="false" OnClick="btnBackAsset_Click" Visible="false" />
                </td>
            </tr>
            <%--End  Asset--%>
            <%-- Start Asset Increment--%>
            <tr id="trAssetIncrement1" runat="server">
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblIncrmentNo" runat="server" Text="��ţ�"></asp:Label>
                </td>
                <td style="width: 215px; height: 24px;">
                    <asp:TextBox ID="txtIncrementNo" runat="server" AutoPostBack="True" OnTextChanged="txtIncremnetNo_Changed"
                        MaxLength="10" TabIndex="14"></asp:TextBox>
                    <asp:Label ID="lblIncrementID" runat="server" Visible="False">0</asp:Label>
                </td>
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblAssetName1" runat="server" Text="�ʲ����ƣ�"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAssetName1" runat="server" TabIndex="2" Enabled="False" ReadOnly="True"></asp:TextBox>
                </td>
                <td align="right">
                </td>
            </tr>
            <tr id="trAssetIncrement2" runat="server">
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblIncrementName" runat="server" Text="���ƣ�"></asp:Label>
                </td>
                <td style="width: 215px; height: 24px;">
                    <asp:TextBox ID="txtIncrementName" runat="server" TabIndex="15"></asp:TextBox>
                </td>
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblType1" runat="server" Text="���ͣ�"></asp:Label>
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtType1" runat="server" TabIndex="2" Enabled="False" ReadOnly="True"></asp:TextBox>
                </td>
                <td style="height: 24px" align="right">
                    ��ϸ���ͣ�
                </td>
                <td style="width: 234px; height: 24px;">
                    <asp:TextBox ID="txtDetailType1" runat="server" TabIndex="2" Enabled="False" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr id="trAssetIncrement3" runat="server">
                <td align="right">
                    <asp:Label ID="lblIncrementEntity" runat="server" Text="���˹�˾��"></asp:Label>
                </td>
                <td style="width: 215px">
                    <asp:DropDownList ID="dropIncrementEntity" runat="server" Width="140px" TabIndex="16">
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 132px">
                    <asp:Label ID="lblIncrementVoucher" runat="server" Text="����ƾ֤��"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIncrementVoucher" runat="server" TabIndex="17"></asp:TextBox>
                </td>
                <td align="right" style="width: 116px">
                    <asp:Label ID="lblIncrementDate" runat="server" Text="�������ڣ�"></asp:Label>
                </td>
                <td style="width: 234px">
                    <asp:TextBox ID="txtIncrementDate" runat="server" CssClass="SmallTextBox Date" TabIndex="18"
                        onpaste="return false"></asp:TextBox>
                </td>
            </tr>
            <tr id="trAssetIncrement4" runat="server">
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblIncrementCost" runat="server" Text="ԭֵ��"></asp:Label>
                </td>
                <td style="width: 215px; height: 24px;">
                    <asp:TextBox ID="txtIncrementCost" runat="server" TabIndex="19"></asp:TextBox>
                </td>
                <td align="right" style="width: 132px; height: 24px;">
                    <asp:Label ID="lblIncrementSupplier" runat="server" Text="��Ӧ�̣�"></asp:Label>
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtIncrementSupplier" runat="server" TabIndex="20"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 24px">
                    <asp:Label ID="lblIncDiscount" runat="server" Text="�۾����ޣ�"></asp:Label><asp:TextBox
                        ID="txtIncDiscount" runat="server" TabIndex="12"></asp:TextBox>
                </td>
            </tr>
            <tr id="trAssetIncrement5" runat="server">
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblIncrementComment" runat="server" Text=" ��ע��"></asp:Label>
                </td>
                <td style="width: 215px; height: 24px;">
                    <asp:TextBox ID="txtIncrementComment" runat="server" TabIndex="21"></asp:TextBox>
                </td>
                <td colspan="4" align="center" style="height: 24px">
                    <asp:Button ID="btnIncrementSave" runat="server" Width="80px" CssClass="SmallButton2"
                        Text="" ValidationGroup="assetB" OnClick="btnIncrementSave_Click" OnClientClick="javascript:return CheckAssetIncrement();"
                        TabIndex="22" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnIncrementDel" runat="server" Width="80px" CssClass="SmallButton2"
                        Text="ɾ��" OnClientClick="javascript:return confirm('��ȷ��Ҫɾ���̶��ʲ���ֵ���� ');" CausesValidation="false"
                        Visible="false" OnClick="btnIncrementDel_Click" />
                    <asp:Button ID="btnIncrementBak" runat="server" Width="80px" CssClass="SmallButton2"
                        Text="����" CausesValidation="false" Visible="false" OnClick="btnIncrementBak_Click" />
                </td>
            </tr>
            <%-- End Asset Increment--%>
            <tr>
                <td colspan="6" style="height: 20px;">
                    <br />
                    <hr />
                </td>
            </tr>
            <%-- Start Asset Detail--%>
            <tr id="trAssetDetail1" runat="server">
                <td align="right">
                    <asp:Label ID="lblStartDate" runat="server" Text="��ʼ���ڣ�"></asp:Label>
                </td>
                <td style="width: 215px">
                    <asp:TextBox ID="txtStartDate" runat="server" onpaste="return false" TabIndex="23"
                        CssClass="SmallTextBox Date"></asp:TextBox>
                </td>
                <td style="width: 132px" align="right">
                    <asp:Label ID="lblDetailEntity" runat="server" Text="���ڹ�˾��"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropDetailEntity" runat="server" Width="140px" TabIndex="24"
                        OnTextChanged="dropDetailEntity_changed" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="width: 116px" align="right">
                    <asp:Label ID="lblDetailCenter" runat="server" Text="�ɱ����ģ�"></asp:Label>
                </td>
                <td style="width: 234px">
                    <asp:DropDownList ID="dropDetailCenter" runat="server" Width="200px" TabIndex="25">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trAssetDetail2" runat="server">
                <td align="right" style="height: 24px">
                    <asp:Label ID="lblDetailStatus" runat="server" Text="״̬��"></asp:Label>
                </td>
                <td style="width: 215px; height: 24px;">
                    <asp:DropDownList ID="dropDetailStatus" runat="server" Width="140px" TabIndex="26">
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 132px; height: 24px;">
                    <asp:Label ID="lblDetailSite" runat="server" Text="���õص㣺"></asp:Label>
                </td>
                <td style="height: 24px">
                    <asp:TextBox ID="txtDetailSite" runat="server" TabIndex="27"></asp:TextBox>
                </td>
                <td style="width: 116px" align="right">
                    <asp:Label ID="Label1" runat="server" Text="�����ߣ�"></asp:Label>
                </td>
                <td style="width: 234px">
                    <asp:DropDownList ID="dropLine" runat="server" Width="200px" TabIndex="25">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trAssetDetail3" runat="server">
                <td align="right">
                    <asp:Label ID="lblDetailComment" runat="server" Text="��ע��"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDetailComment" runat="server" Width="506px" TabIndex="29"></asp:TextBox>
                </td>
                <td align="right" style="width: 116px; height: 24px;">
                    <asp:Label ID="lblResponsibler" runat="server" Text="�����ˣ�"></asp:Label>
                </td>
                <td style="width: 234px; height: 24px;">
                    <asp:TextBox ID="txtResponsibler" runat="server" TabIndex="28"></asp:TextBox>
                    <asp:Button ID="btnAssetDetail" runat="server" Width="70px" CssClass="SmallButton2"
                        TabIndex="30" ValidationGroup="assetC" OnClick="btnAssetDetail_Click" />
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <%-- End Asset Detail--%>
        </table>
        <br />
        <asp:GridView ID="gvAssetDetail" runat="server" AutoGenerateColumns="False" Width="1000px"
            DataKeyNames="fixas_det_id" DataSourceID="obdsAssetDetail" AllowPaging="True"
            OnRowCommand="gvAssetDetail_RowCommand" OnRowCreated="gvAssetDetail_RowCreated"
            CssClass="GridViewStyle">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="fixas_det_startDate" HeaderText="��ʼ����" HtmlEncode="False"
                    DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="enti_name" HeaderText="���ڹ�˾">
                    <ItemStyle Width="60px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField DataField="fixctc_name" HeaderText="�ɱ�����">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="line_name" HeaderText="������">
                    <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixsta_name" HeaderText="״̬">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_site" HeaderText="���õص�">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_responsibler" HeaderText="������">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="fixas_det_comment" HeaderText="��ע">
                    <ItemStyle Width="140px" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandArgument='<%# DataBinder.Eval(Container,"RowIndex")%>'
                            CommandName="edt" Text="<u>�༭</u>"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Font-Underline="True" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelDetail" runat="server" Text="<u>ɾ��</u>" CommandName="Delete"
                            CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="enti_id" Visible="True" />
                <asp:BoundField DataField="fixctc_id" Visible="True" />
                <asp:BoundField DataField="fixsta_id" Visible="True" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="0" CellSpacing="0" CssClass="GridViewHeaderStyle">
                    <asp:TableRow>
                        <asp:TableCell Text="��ʼ����" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="���ڹ�˾" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Width="80px" Text="�ɱ�����" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="120px" Text="������" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="״̬" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="���õص�" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="100px" Text="������" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="240px" Text="��ע" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Width="60px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Label ID="lblfixid" runat="server" Text="0" Visible="False"></asp:Label>
        <asp:ObjectDataSource ID="obdsAssetDetail" runat="server" SelectMethod="GetAssetDetail"
            TypeName="TCPNEW.GetDataTcp" DeleteMethod="DelAssetDetailRecord">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblAssetID" DefaultValue="0" Name="ParentID" PropertyName="Text"
                    Type="Int32" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="fixas_det_id" Type="Int32" />
                <asp:SessionParameter Name="fixas_det_createdBy" SessionField="uid" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
