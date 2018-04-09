<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcm_MadeInquiryList.aspx.cs"
    Inherits="price_pcm_MadeInquiryList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>
                    供应商：&nbsp;&nbsp;<asp:TextBox ID="txtVender" runat="server"  CssClass="SmallTextBox"  Width="100px">&nbsp;&nbsp;</asp:TextBox>
                </td>
                <td>
                    供应商名：&nbsp;&nbsp;<asp:TextBox ID="txtVenderName" runat="server"  CssClass="SmallTextBox" >&nbsp;&nbsp;</asp:TextBox>
                </td>
                <td>
                    QAD：&nbsp;&nbsp;<asp:TextBox ID="txtQAD" runat="server"  CssClass="SmallTextBox" Width="120px" >&nbsp;&nbsp;</asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSelect" runat="server" Text="查询" OnClick="btnSelect_Click" CssClass="SmallButton2" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvVenderList" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                        AllowPaging="true" PageSize="20" OnRowCommand="gvVenderList_RowCommand" OnRowDataBound="gvVenderList_RowDataBound"
                        OnPageIndexChanging="gvVenderList_PageIndexChanging" DataKeyNames="vender">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="417px"
                                CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商号" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="供应商名称" Width="300px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="未成单数量" Width="120px"></asp:TableCell>
                                     <asp:TableCell HorizontalAlign="center" Text="未完成询价单" Width="120px"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                    <asp:TableCell HorizontalAlign="Center" Text="没有找到数据" ColumnSpan="4"></asp:TableCell>
                                </asp:TableFooterRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField HeaderText="供应商" DataField="vender">
                                <HeaderStyle Width="80px" />
                                 <ItemStyle HorizontalAlign="Center"  />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="供应商名称" DataField="venderName">
                                <HeaderStyle Width="300px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="未成单数量">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnQuotation" runat="server" CommandName="lkbtnQuotation" CommandArgument='<%# Eval("vender") %>'
                                        Text='<%# Eval("countNotInquiry") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="未完成询价单">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnInquiry" runat="server" CommandName="lkbtnInquiry" CommandArgument='<%# Eval("vender") %>'
                                        Text='<%# Eval("countInquiry") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
