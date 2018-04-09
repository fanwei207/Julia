<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PCF_commitPassInquiryDet.aspx.cs" Inherits="price_PCF_commitPassInquiryDet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#chkAll").click(function () {
                $("#gv input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
            })
        })
    </script>
</head>
<body>
    <div>
        <form id="form1" runat="server">
            <table cellpadding="0" cellspacing="0">
                <tr>

                    <td style="width: 217px">QAD号<asp:TextBox ID="txtPart" runat="server" Width="90px" CssClass="SmallTextBox"
                        TabIndex="2"></asp:TextBox>(*)
                    </td>
                    <td>供应商<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox5" Width="100"
                        TabIndex="3"></asp:TextBox>(*)&nbsp;&nbsp;
                    </td>
                    <td align="left" style="width: 217px">供应商名称<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox5" Width="100"
                        TabIndex="3"></asp:TextBox>(*)&nbsp;&nbsp;
                    &nbsp;
                    </td>
                    <td align="left" style="width: 150px; height: 26px;">
                        <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2"
                            OnClick="btnSearch_Click" />
                        &nbsp; &nbsp;
                    <asp:Button ID="btnApply" runat="server" Text="提交" OnClick="btnApply_Click" CssClass="SmallButton2" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" AllowPaging="false" AutoGenerateColumns="False" PageSize="25"
                CssClass="GridViewStyle" runat="server" Width="1800px" DataKeyNames="PCF_ID">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input id="chkAll" type="checkbox">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="询价单号" DataField="PCF_IMID" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="QAD号" DataField="PCF_part" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="供应商" DataField="PCF_vender" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="供应商名称" DataField="PCF_venderName" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="核价" DataField="PCF_checkPrice" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="基本单位" DataField="PCF_ptum">
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="采购单位" DataField="PCF_um">
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="转换因子" DataField="PCF_changeFor">
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="核价备注" DataField="PCF_checkPriceBasis" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>

                    <asp:BoundField HeaderText="税率（%）" DataField="PCF_taxes" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="是否可抵扣" DataField="PCF_isDeductible" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="是否驳回" DataField="PCF_isCheckPricePass" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="驳回信息" DataField="PCF_checkPricePassReason" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="描述" DataField="PCF_desc" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="用途" DataField="PCF_purpose" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="规格" DataField="PCF_format" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="200px" />
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="描述1" DataField="PCF_desc1" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="描述2" DataField="PCF_desc2" ReadOnly="True">
                        <HeaderStyle HorizontalAlign="Center" Width="150px" />
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:BoundField>

                </Columns>
            </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
