<%@ Page Language="C#" AutoEventWireup="true" CodeFile="access2.aspx.cs" Inherits="admin_access2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .fixedheader
        {
            position: relative;
            table-layout: fixed;
            top: expression(this.offsetParent.scrollTop);
            z-index: 10;
        }
        .cancelHeaderTop
        {
            margin-top: 0px;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        function doSelect() {
            var dom = document.all;
            var el = event.srcElement;
            if (el.id.indexOf("chkAll") >= 0 && el.tagName == "INPUT" && el.type.toLowerCase() == "checkbox") {
                var ischecked = false;
                if (el.checked)
                    ischecked = true;
                for (i = 0; i < dom.length; i++) {
                    if (dom[i].id.indexOf("chkAccess") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox")
                        dom[i].checked = ischecked;
                }
            }
        }
    </script>
</head>
<body style="text-align: center;">
    <form id="form1" runat="server">
    <div>
        <table style="margin: 0 auto; padding: 3px; width: 667px; background-image: url(../images/banner01.jpg);"
            border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 35%;">
                    <asp:Label ID="Label1" runat="server" Text="部门"></asp:Label>
                    <asp:DropDownList ID="dropDeparts" runat="server" Width="160px" AutoPostBack="True"
                        OnSelectedIndexChanged="dropDeparts_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 25%;">
                    <asp:Label ID="Label4" runat="server" Text="用户名"></asp:Label>
                    <asp:DropDownList ID="dropUsers" AutoPostBack="true" runat="server" Width="100px"
                        OnSelectedIndexChanged="dropUsers_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 14%;">
                </td>
                <td style="width: 15%;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 35%; height: 30px;">
                    <asp:Label ID="Label5" runat="server" Text="菜单"></asp:Label>
                    <asp:DropDownList ID="dropMenuRoots" AutoPostBack="true" runat="server" Width="160px"
                        OnSelectedIndexChanged="dropMenuRoots_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 25%; height: 30px;">
                    <asp:Label ID="Label7" runat="server" Text="数量:"></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 14%; text-align: center; height: 30px;">
                    <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton2" Width="70px"
                        OnClick="btnBack_Click" />
                </td>
                <td style="width: 15%; text-align: center; height: 30px;">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton2" Width="70px"
                        OnClick="btnSave_Click" />
                </td>
                <td style="text-align: center; height: 30px;">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" onclick="doSelect()" />
                </td>
            </tr>
        </table>
        <div style="height: 470px; width: 720px; margin: 3px auto; overflow: scroll; border: 1px solid black;">
            <asp:GridView ID="gvAccessRules" runat="server" AutoGenerateColumns="False"
                Width="700px" CssClass="GridViewStyle cancelHeaderTop" PageSize="25" DataKeyNames="moduleID,isAccessed"
                OnRowDataBound="gvAccessRules_RowDataBound">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                        GridLines="Vertical" Width="650px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="center" Text="名称-描述" Width="650px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="选择"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="moduleName" HeaderText="名称-描述">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="650px" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="650px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAccess" runat="server" Checked='<%# Bind("isAccessed") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
