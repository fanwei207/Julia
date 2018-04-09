<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_FinCale.aspx.cs" Inherits="wo2_wo2_FinCale" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="HEAD1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 334px;">
            <tr>
                <td style="width: 400px;">
                    年<asp:DropDownList ID="dropYear" runat="server" Width="68px" DataTextField="ft_name"
                        DataValueField="ft_id" AutoPostBack="True" OnSelectedIndexChanged="dropYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;月<asp:DropDownList ID="dropMonth" runat="server" Width="66px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">01</asp:ListItem>
                        <asp:ListItem Value="2">02</asp:ListItem>
                        <asp:ListItem Value="3">03</asp:ListItem>
                        <asp:ListItem Value="4">04</asp:ListItem>
                        <asp:ListItem Value="5">05</asp:ListItem>
                        <asp:ListItem Value="6">06</asp:ListItem>
                        <asp:ListItem Value="7">07</asp:ListItem>
                        <asp:ListItem Value="8">08</asp:ListItem>
                        <asp:ListItem Value="9">09</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 369px;" align="right">
                    <asp:Button ID="btnAdd" runat="server" CausesValidation="False" CssClass="SmallButton2"
                        OnClick="btnAdd_Click" Text="保存" Width="56px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 769px">
                    <asp:GridView ID="gvlist" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                        CssClass="GridViewStyle AutoPageSize"  PageSize="18" Width="390px" DataKeyNames="wo2_year,wo2_month"
                        OnRowCommand="gvlist_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="Vertical" Width="390px">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="年" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="月" Width="60px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="开放" Width="50px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="创建人" Width="80px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="创建日期" Width="100px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="" Width="40px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="wo2_year" HeaderText="年">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_month" HeaderText="月">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="开放">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSinger" runat="server" AutoPostBack="True" Checked='<%# Eval("wo2_isOpen") %>' 
                                        oncheckedchanged="chkSinger_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="30px"/>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="wo2_createName" HeaderText="创建人">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="wo2_createDate" HeaderText="创建日期" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkdelete" Text="<u>删除</u>" ForeColor="Black" Font-Underline="True"
                                        Font-Size="12px" runat="server" CommandArgument='<%# Eval("wo2_year") + "," + Eval("wo2_month") %>'
                                        CommandName="del" />
                                </ItemTemplate>
                                <HeaderStyle Width="40px" Font-Bold="False" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
             <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
