<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI860Detail.aspx.cs" Inherits="EDI_EDI860Detail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
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
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" width="960" bgcolor="white" border="0" style="margin-top: 4px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: Î¢ÈíÑÅºÚ;">
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;
                    background-position: left top;">
                </td>
                <td align="right">
                    <asp:Button ID="btnBack" runat="server" Text="¹Ø±Õ´°¿Ú" Width="80" CssClass="SmallButton2"
                        OnClientClick="window.close();" />
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;
                    background-position: right top;">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 4px;">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="gvDetail" runat="server" Width="950" PageSize="15" AutoGenerateColumns="False"
                        AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gvDetail_PageIndexChanging"
                        OnRowDataBound="gvDetail_RowDataBound" OnRowCommand="gvDetail_RowCommand" DataKeyNames="det_860_DetFlag">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblPodId" runat="server" Text='<%# Bind("id_edi_860_detail")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Line">
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLine" runat="server" Text='<%# Bind("det_860_line")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Part">
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="130px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCusPart" runat="server" Text='<%# Bind("det_860_cus_part")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QAD Part">
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="130px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQadPart" runat="server" Text='<%# Bind("det_860_qad_part")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SKU">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSku" runat="server" Text='<%# Bind("det_860_sku")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <HeaderStyle Width="80" />
                                <ItemStyle Width="80" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("det_860_qty_ord")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <HeaderStyle Width="80" />
                                <ItemStyle Width="80" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("det_860_price")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UM">
                                <HeaderStyle Width="40" />
                                <ItemStyle Width="40" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUm" runat="server" Text='<%# Bind("det_860_EA")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbldue" runat="server" Text='<%# Bind("det_860_dueDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Require Date">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblreq" runat="server" Text='<%# Bind("det_860_reqDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="lbltype" runat="server" Text='<%# Bind("det_860_DetFlag")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Button ID="btnFinish" runat="server" Text="Finish" CommandName="finish" CommandArgument='<%# Eval("id_edi_860_detail") %>'
                                        CssClass="SmallButton2" />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Button ID="btnAdd" runat="server" Text="ADD" CommandName="add" CommandArgument='<%# Eval("id_edi_860_detail") %>'
                                        CssClass="SmallButton2" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsUpdate" runat="server" Text='<%# Bind("det_860_isUpdate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
