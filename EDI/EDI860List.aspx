<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI860List.aspx.cs" Inherits="EDI_EDI860List" %>

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
        <asp:Label ID="lblHrdId" runat="server" Text="1" Visible="False"></asp:Label>
        <table cellspacing="0" cellpadding="0" width="960" bgcolor="white" border="0" style="margin-top: 2px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; margin: 2px auto; font-family: Î¢ÈíÑÅºÚ;">
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;">
                </td>
                <td>
                    EDI Date:<asp:TextBox ID="txtStdDate" CssClass="Date" runat="server" Width="107px"></asp:TextBox>-<asp:TextBox
                        ID="txtEndDate" runat="server" Width="107px" CssClass="Date"></asp:TextBox>
                    <asp:Button ID="btnQuery" runat="server" Text="²éÑ¯" CssClass="SmallButton2" OnClick="btnQuery_Click"
                        Width="80" />
                </td>
                <td align="right">
                    <asp:Button ID="btnExcelExport" runat="server" Text="Export Excel" CssClass="SmallButton3"
                        OnClick="btnExcelExport_Click" Width="80" />
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;">
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 4px;">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvlist" runat="server" Width="950px" OnPageIndexChanging="gvlist_PageIndexChanging"
                        OnRowDataBound="gvlist_RowDataBound" PageSize="15" AutoGenerateColumns="False"
                        CssClass="GridViewStyle AutoPageSize" AllowSorting="True" AllowPaging="True"
                        OnRowCommand="gvlist_RowCommand">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoId" runat="server" Text='<%# Bind("id_edi_860_header")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Number">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("edi_860_PONumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tcp So Number">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("edi_860_FOB")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("edi_860_duedate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ship Via">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("edi_860_shipvia")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ship To">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("edi_860_shipto")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblErrorMsg" runat="server" Text='<%# Bind("eid_860_remarks")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbltype" runat="server" Text='<%# Bind("edi_860_delchgflag")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EDI Date">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("edi_860_date","{0:yyyy-MM-dd}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="btnFinish" runat="server" Text="Deleted" CommandName="delRec" CommandArgument='<%# Eval("id_edi_860_header") %>'
                                        CssClass="SmallButton2" OnClientClick="return confirm('Delete this 860 record , are you sure?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
