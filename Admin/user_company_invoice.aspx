<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.User_Company_Invoice"
    CodeFile="User_Company_Invoice.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript">
        var w;
        function selectCheckBox(type) {
            var dgObject = document.getElementById('DataGrid1');

            for (var i = 1; i <= (dgObject.rows.length - 1); i++) {
                if (type == 0) {
                    document.getElementById('DataGrid1:_ctl' + (i + 1) + ':chkType').checked = false;
                }
                else {
                    document.getElementById('DataGrid1:_ctl' + (i + 1) + ':chkType').checked = true;
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
                <td valign="top" align="left" width="450" colspan="2" style="height: 20px">
                    公司
                    <asp:DropDownList ID="ddlCompany" runat="server" Width="380px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
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
                <td colspan="2">
                    <asp:DataGrid ID="DataGrid1" runat="server" Width="360" AllowPaging="False" AutoGenerateColumns="False"
                        CssClass="GridViewStyle">
                        <ItemStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundColumn Visible="False" ReadOnly="True" DataField="company_id"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkType" runat="server" Checked='<%# Container.DataItem("chk") %>'
                                        Width="25px"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                                <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="company_info" HeaderText="公司名称">
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle HorizontalAlign="left"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td style="width: 170px">
                    <input type="button" id="selectAll" value="全部选择" style="width: 60px" class="SmallButton3"
                        onclick="selectCheckBox(1);">&nbsp;
                    <input type="button" id="clearAll" value="全部清除" style="width: 60px" class="SmallButton3"
                        onclick="selectCheckBox(0);">&nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="smallbutton2"></asp:Button>
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="smallbutton2"></asp:Button>
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
