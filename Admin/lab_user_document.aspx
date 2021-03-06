<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.lab_user_document"
    CodeFile="lab_user_document.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var w;
        function selectCheckBox(type) {
            var dgObject = document.getElementById('DataGrid1');

            for (var i = 1; i <= (dgObject.rows.length - 1); i++) {
                if (type == 0) {
                    if (navigator.userAgent.indexOf("Firefox") != -1) { document.getElementById('DataGrid1__ctl' + (i + 1) + '_chkType').checked = false; document.getElementById('DataGrid1__ctl' + (i + 1) + '_chkpublish').checked = false; }
                    else {
                        document.getElementById('DataGrid1:_ctl' + (i + 1) + ':chkType').checked = false;
                        document.getElementById('DataGrid1:_ctl' + (i + 1) + ':chkpublish').checked = false;
                    }
                }
                else {
                    if (navigator.userAgent.indexOf("Firefox") != -1) { document.getElementById('DataGrid1__ctl' + (i + 1) + '_chkType').checked = true; }
                    else { document.getElementById('DataGrid1:_ctl' + (i + 1) + ':chkType').checked = true; }
                }
            }
        }
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="600">
            <tr>
                <td valign="top" align="left" width="450" colspan="2" style="height: 19px">
                    部门
                    <asp:DropDownList ID="ddlRole" runat="server" Width="180px" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 用户名<asp:DropDownList ID="ddlUser" AutoPostBack="True"
                        runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px;">
                    <asp:DataGrid ID="DataGrid1" runat="server" Width="300px" BorderWidth="1px" GridLines="None"
                        CellPadding="1" BackColor="White" BorderStyle="None" AutoGenerateColumns="False"
                        HeaderStyle-Font-Bold="false" AllowPaging="False" AllowSorting="True" PagerStyle-Mode="NextPrev">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="False" ReadOnly="True" DataField="id"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkType" runat="server" Checked='<%# Container.DataItem("chk") %>'
                                        Width="25px"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="docType" HeaderText="文档类型">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemStyle Width="150px" HorizontalAlign="center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="查看未公开" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px"
                                ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkpublish" runat="server" Width="25px" Checked='<%# Container.DataItem("ispublish") %>'>
                                    </asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td style="width: 170px">
                    <input type="button" id="selectAll" value="全部选择" style="width: 60px" class="SmallButton3"
                        onclick="selectCheckBox(1);" />&nbsp;
                    <input type="button" id="clearAll" value="全部清除" style="width: 60px" class="SmallButton3"
                        onclick="selectCheckBox(0);" />&nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="smallbutton2"></asp:Button>
                    <asp:Button ID="bntBack" runat="server" Text="返回" CssClass="smallbutton2"></asp:Button>
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
