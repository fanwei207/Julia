<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_Salary_Release.aspx.cs"
    Inherits="hr_Salary_Leaver" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function doSelect() {
            var dom = document.all;
            var el = event.srcElement;
            if (el.id.indexOf("chkAll") >= 0 && el.tagName == "INPUT" && el.type.toLowerCase() == "checkbox") {
                var ischecked = false;
                if (el.checked)
                    ischecked = true;
                for (i = 0; i < dom.length; i++) {
                    if (dom[i].id.indexOf("chkSingle") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox")
                        dom[i].checked = ischecked;
                }
            }
        }
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 800px">
            <tr>
                <td>
                    年<asp:TextBox ID="txtYear" runat="server" Width="36px"></asp:TextBox>
                    月:
                    <asp:TextBox ID="txtMonth" runat="server" Width="28px"></asp:TextBox>
                    &nbsp;&nbsp;工号：<asp:TextBox ID="txtUserNo" runat = "server" Width="60px"></asp:TextBox>
                    部门：<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="查询" Width="60px"
                        OnClick="btnQuery_Click" />
                </td>
                <td>
                    &nbsp; 发放至：年<asp:TextBox 
                        ID="txtYearR" runat="server" Width="36px"></asp:TextBox>
                    月:
                    <asp:TextBox ID="txtMonthR" runat="server" Width="28px"></asp:TextBox>&nbsp;&nbsp;方式:<asp:DropDownList 
                        ID="dropReleaseType" runat="server">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>现金</asp:ListItem>
                        <asp:ListItem>银行</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button
                        ID="btnRelease" runat="server" CssClass="SmallButton3" Text="发放" Width="60px"
                        OnClick="btnRelease_Click" />&nbsp; <asp:Button ID="btnCancelRelease" 
                        runat="server" CssClass="SmallButton3" Text="取消发放" Width="60px" 
                        onclick="btnCancelRelease_Click"/>

                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            OnRowDataBound="gv_RowDataBound" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
            PageSize="18" Width="800px" DataKeyNames="userID,releaseDate">
            <Columns>
                <asp:BoundField HeaderText="年月" DataField="leavDate">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工号" DataField="userNo">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="deptName">
                    <HeaderStyle Width="250px" />
                    <ItemStyle Width="250px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="发放年月" DataField="releaseDate">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="发放方式" DataField="releaseType">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" onclick="doSelect()" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSingle" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        </form>
    </div>
</body>
</html>
