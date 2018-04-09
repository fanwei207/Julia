<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcb_confirm.aspx.cs" Inherits="pcb_confirm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 993px">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lb" runat="server" Width="32px"></asp:Label><asp:TextBox ID="txtDate"
                        runat="server" CssClass="smalltextbox Date" Width="81px"></asp:TextBox><asp:TextBox
                            ID="txtModel" runat="server" CssClass="smalltextbox" Width="85px"></asp:TextBox><asp:TextBox
                                ID="txtVersion" runat="server" CssClass="smalltextbox" Width="83px"></asp:TextBox><asp:TextBox
                                    ID="txtGreen" runat="server" CssClass="smalltextbox" Width="83px"></asp:TextBox><asp:TextBox
                                        ID="txtSteel" runat="server" CssClass="smalltextbox" Width="85px"></asp:TextBox><asp:TextBox
                                            ID="txtCopper" runat="server" CssClass="smalltextbox" Width="81px"></asp:TextBox><asp:TextBox
                                                ID="txtContents" runat="server" CssClass="smalltextbox" Width="123px"></asp:TextBox><asp:DropDownList
                                                    ID="dropEqu" runat="server" Width="64px">
                                                    <asp:ListItem>全部</asp:ListItem>
                                                    <asp:ListItem>待确认</asp:ListItem>
                                                    <asp:ListItem>YES</asp:ListItem>
                                                    <asp:ListItem>NO</asp:ListItem>
                                                </asp:DropDownList>
                    <asp:DropDownList ID="dropWorkShop" runat="server" Width="61px">
                        <asp:ListItem>全部</asp:ListItem>
                        <asp:ListItem>待确认</asp:ListItem>
                        <asp:ListItem>YES</asp:ListItem>
                        <asp:ListItem>NO</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnHave" runat="server" CssClass="smallbutton2" Text="有" Width="36px"
                        OnClick="btnHave_Click" />
                    <asp:Button ID="btnNone" runat="server" CssClass="smallbutton2" Text="无" Width="36px"
                        OnClick="btnNone_Click" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="smallbutton2" Text="查询" Width="36px"
                        OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExport" runat="server" CssClass="smallbutton2" Text="导出" Width="36px"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            PageSize="16" OnRowDataBound="gv_RowDataBound" DataKeyNames="pvk_id" AllowPaging="True"
            OnPageIndexChanging="gv_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemStyle Width="30px" />
                    <HeaderStyle Width="30px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkItem" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="pvk_mod_date" HeaderText="修改日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_model" HeaderText="线路板">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_ver" HeaderText="版本">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_green" HeaderText="绿油网">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_steel" HeaderText="钢网">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_copper" HeaderText="铜网">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_content" HeaderText="改板内容">
                    <HeaderStyle Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_equ" HeaderText="设备确认">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_workshop" HeaderText="车间确认">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="备注">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle Width="200px" />
                    <ItemTemplate>
                        <asp:TextBox ID="txtRmks" runat="server" CssClass="smalltextbox" Text='<%# Bind("pvk_remarks") %>'
                            Width="100%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle HorizontalAlign="Right" />
        </asp:GridView>
        <asp:Label ID="lbEqu" runat="server" Text="0" Visible="False"></asp:Label>
        </form>
    </div>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
