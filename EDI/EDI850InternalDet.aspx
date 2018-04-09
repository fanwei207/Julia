<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI850InternalDet.aspx.cs"
    Inherits="EDI_EDI850InternalDet" %>

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
        <table cellspacing="0" cellpadding="0" width="700" bgcolor="white" border="0" style="margin-top: 4px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: 微软雅黑;">
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;">
                </td>
                <td align="right">
                    <asp:Button ID="btnBack" runat="server" Text="Close Window" OnClientClick="window.close();"
                        CssClass="SmallButton3" Width="120" />
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 4px;">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                        PageSize="20" OnPageIndexChanging="gvlist_PageIndexChanging">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="行号">
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("DET_Line")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="物料号">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblQadPart" runat="server" Text='<%# Bind("DET_PartNbr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="订单数量">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("DET_Qty")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位">
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("DET_Um")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位价格">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("DET_Price")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="需求日期">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("DET_ReqDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="截止日期">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("DET_DueDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DET_Error" HeaderText="错误信息">
                                <HeaderStyle Width="180px" />
                                <ItemStyle Width="180px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
