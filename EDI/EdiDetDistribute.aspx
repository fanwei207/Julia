<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiDetDistribute.aspx.cs"
    Inherits="EdiDetDistribute" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 3px;
            height: 26px;
        }

        .style2 {
            height: 26px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
            <table cellspacing="0" cellpadding="0" width="950" bgcolor="white" border="0" style="margin-top: 4px;">
                <tr style="background-image: url('../images/bg_tb2.jpg'); background-repeat: repeat-x; font-family: 微软雅黑;">
                    <td style="background-image: url('../images/bg_tb1.jpg'); background-repeat: no-repeat; background-position: left top;"
                        class="style1"></td>
                    <td class="style2">&nbsp;
                    </td>
                    <td align="right" class="style2">
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click"
                            CssClass="SmallButton2" Width="66px" />&nbsp;<asp:Button ID="btnClose" runat="server"
                                Text="关闭" OnClientClick="window.close();" CssClass="SmallButton2" Width="66px" />&nbsp;
                    </td>
                    <td style="background-image: url('../images/bg_tb3.jpg'); background-repeat: no-repeat;"
                        class="style1">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height: 4px;"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            PageSize="18" Width="921px" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
                            DataKeyNames="id,site,domain" CssClass="GridViewStyle">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="poLine" HeaderText="行号">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="partNbr" HeaderText="客户零件号">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ordQty" HeaderText="数量">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="um" HeaderText="单位">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="price" HeaderText="单价">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="reqDate" HeaderText="需求日期">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="dueDate" HeaderText="截止日期">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="qadPart" HeaderText="物料号">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    <HeaderStyle Width="80px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="To Site">
                                    <HeaderStyle Width="130px" HorizontalAlign="Center" />
                                    <ItemStyle Width="130px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:DropDownList ID="dropDomain" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dropDomain_SelectedIndexChanged1">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem>SZX</asp:ListItem>
                                            <asp:ListItem>ZQL</asp:ListItem>
                                            <asp:ListItem>YQL</asp:ListItem>
                                            <asp:ListItem>HQL</asp:ListItem>
                                            <asp:ListItem>ATL</asp:ListItem>
                                        </asp:DropDownList>
                                        -<asp:DropDownList ID="dropSite" runat="server">
                                            <asp:ListItem>1000</asp:ListItem>
                                        </asp:DropDownList>
                                        <input id="hSite" runat="server" type="hidden" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:DropDownList ID="drpDomain" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDomain_SelectedIndexChanged">
                                            <asp:ListItem>--</asp:ListItem>
                                            <asp:ListItem>SZX</asp:ListItem>
                                            <asp:ListItem>ZQL</asp:ListItem>
                                            <asp:ListItem>YQL</asp:ListItem>
                                            <asp:ListItem>HQL</asp:ListItem>
                                            <asp:ListItem>ATL</asp:ListItem>
                                        </asp:DropDownList>-
                                        <asp:DropDownList ID="drpSite" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpSite_SelectedIndexChanged">
                                            <asp:ListItem>1000</asp:ListItem>
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
