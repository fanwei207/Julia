<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiAddrEdit.aspx.cs" Inherits="EdiAddrEdit" %>

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
        <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 1260px;
            margin-top: 1px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: 微软雅黑;">
                <td style="background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;
                    background-position: right top;">
                    <asp:Button ID="btnCimload" runat="server" CssClass="SmallButton3" OnClick="Button1_Click"
                        Text="Cimload" />&nbsp;
                    <asp:Button ID="btnUpdateDB" runat="server" CssClass="SmallButton3" Enabled="False"
                        OnClick="btnUpdateDB_Click" Text="更新DB" />
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="height: 344px; vertical-align: top;">
                    <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="25" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
                        OnRowCancelingEdit="gvlist_RowCancelingEdit" OnRowEditing="gvlist_RowEditing"
                        CssClass="GridViewStyle AutoPageSize" OnRowUpdating="gvlist_RowUpdating">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="发货地址" ReadOnly="True" DataField="addr">
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="地址1">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLine1" runat="server" CssClass="SmallTextBox" Text='<%# Bind("ad_line1") %>'
                                        Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ad_line1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="地址2">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLine2" runat="server" CssClass="SmallTextBox" Text='<%# Bind("ad_line2") %>'
                                        Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ad_line2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="地址3">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLine3" runat="server" CssClass="SmallTextBox" Text='<%# Bind("ad_line3") %>'
                                        Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("ad_line3") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="城市">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="SmallTextBox" Text='<%# Bind("ad_city") %>'
                                        Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="130px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("ad_city") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="州">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtState" runat="server" CssClass="SmallTextBox" Text='<%# Bind("ad_state") %>'
                                        Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("ad_state") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="邮编">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtZip" runat="server" CssClass="SmallTextBox" Text='<%# Bind("ad_zip") %>'
                                        Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("ad_zip") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="客户代码">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCustCode" runat="server" CssClass="SmallTextBox" Text='<%# Bind("cusCode") %>'
                                        Width="100%"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("cusCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="客户名称" DataField="CustName" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:CommandField CancelText="取消" EditText="编辑" ShowEditButton="True" UpdateText="更新">
                                <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="发货地址" Width="70px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="地址1" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="地址2" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="地址3" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="城市" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="州" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="邮编" Width="70px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="客户代码" Width="70px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="客户名称" Width="200px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
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
