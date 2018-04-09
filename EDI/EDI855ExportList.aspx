<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI855ExportList.aspx.cs"
    Inherits="EDI_EDI855ExportList" %>

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
        <table cellspacing="0" cellpadding="0" width="740px" bgcolor="white" border="0" style="margin-top: 4px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: 微软雅黑;">
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;
                    background-position: left top;">
                </td>
                <td width="854">
                    <asp:RadioButton ID="rb855Accept" runat="server" AutoPostBack="True" Checked="true"
                        GroupName="gpType" Text="855 Accept" OnCheckedChanged="rb855Accept_CheckedChanged" />
                    <asp:RadioButton ID="rb855Change" runat="server" AutoPostBack="True" Checked="false"
                        GroupName="gpType" Text="855 Change" OnCheckedChanged="rb855Change_CheckedChanged" />
                    <asp:RadioButton ID="rb865Accept" Text="865 Accept" runat="server" AutoPostBack="True"
                        GroupName="gpType" OnCheckedChanged="rb865Accept_CheckedChanged" />
                </td>
                <td align="right" width="100">
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="SmallButton3"
                        Width="60" OnClick="btnExport_Click" />
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;
                    background-position: right top;">
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 4px;">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        name="gvlist" OnPageIndexChanging="gvlist_PageIndexChanging" OnRowDataBound="gvlist_RowDataBound"
                        PageSize="18" Width="740px" CssClass="GridViewStyle AutoPageSize">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoId" runat="server" Text='<%# Bind("id_edi_so_mstr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="JDE PO NO.">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("edi_so_po")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="JDE SO NO.">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("edi_so_cust_po")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QAD SO NO.">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("edi_so_nbr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("edi_so_due_date","{0:yyyy-MM-dd}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ship Via">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("edi_so_ship_via")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QAD Domain">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("edi_so_domain")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QAD Site">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("edi_so_site")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EDI Type">
                                <HeaderStyle Width="150px" />
                                <ItemStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("edi_so_status")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Export">
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <input id="chkExport" runat="server" name="chkExport" type="checkbox" value='<%#Eval("id_edi_so_mstr") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="true" Value="" />
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
