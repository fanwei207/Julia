<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiHrdDistribute.aspx.cs"
    Inherits="EdiHrdDistribute" %>

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
        <table cellspacing="0" cellpadding="0" width="950" bgcolor="white" border="0">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: 微软雅黑;">
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;">
                </td>
                <td style="height: 25px">
                    订单日期:&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="75px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        Text="查询" Width="60px" />
                </td>
                <td style="height: 25px">
                    &nbsp;SZX：1000 &nbsp;&nbsp; ZQZ：3000
                </td>
                <td align="right" style="height: 25px">
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="SmallButton2"
                        Width="60px" />&nbsp;
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 4px;">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="10" Width="981px" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
                        DataKeyNames="id,domain" CssClass="GridViewStyle AutoPageSize">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoId" runat="server" Text='<%# Bind("id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO Number" SortExpression="poNbr">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("poNbr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tcp So Number" SortExpression="fob">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("fob")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date" SortExpression="dueDate">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("dueDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ship Via" SortExpression="shipVia">
                                <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("shipVia")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ship To" SortExpression="rmk">
                                <HeaderStyle Width="250px" HorizontalAlign="Center" />
                                <ItemStyle Width="250px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("rmk")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EDI Error Info" SortExpression="errMsg">
                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                <ItemStyle Width="200px" HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:Label ID="lblErrorMsg" runat="server" Text='<%# Bind("errMsg")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Domain">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="dropDomain" runat="server">
                                        <asp:ListItem>--</asp:ListItem>
                                        <asp:ListItem>SZX</asp:ListItem>
                                        <asp:ListItem>ZQL</asp:ListItem>
                                        <asp:ListItem>ZQZ</asp:ListItem>
                                        <asp:ListItem>ATL</asp:ListItem>
                                    </asp:DropDownList>
                                    <input id="hDomain" runat="server" type="hidden" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:DropDownList ID="drpDomain" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDomain_SelectedIndexChanged">
                                        <asp:ListItem>--</asp:ListItem>
                                        <asp:ListItem>SZX</asp:ListItem>
                                        <asp:ListItem>ZQL</asp:ListItem>
                                        <asp:ListItem>ZQZ</asp:ListItem>
                                        <asp:ListItem>ATL</asp:ListItem>
                                    </asp:DropDownList>
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <input id="chkImport" type="checkbox" name="chkImport" runat="server" value='<%#Eval("id") %>' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PoRecDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="接收日期"
                                HtmlEncode="False">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" />
                            </asp:BoundField>
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
