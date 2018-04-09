<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chk_maintainLocs.aspx.cs"
    Inherits="part_chk_maintainLocs" %>

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
                    if (dom[i].id.indexOf("chkLocs") >= 0 && dom[i].tagName == "INPUT" && dom[i].type.toLowerCase() == "checkbox")
                        dom[i].checked = ischecked;
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" style="text-align: center;">
    <div align="center">
        <table style="width: 800px; padding: 5px; margin: 0 auto;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 120px;">
                </td>
                <td style="width: 160px;">
                </td>
                <td style="width: 140px;">
                </td>
                <td style="width: 140px;">
                </td>
                <td style="text-align: center; width: 140px;">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="SmallButton2" Width="70px"
                        OnClick="btnSave_Click" />
                </td>
                <td style="text-align: center; width: 100px;">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" onclick="doSelect()" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvLocs" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="800px" OnRowDataBound="gvLocs_RowDataBound" DataKeyNames="isActive">
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="800px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="库位代码" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="库位名称" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="库位状态" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="库位地点" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="库位域" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="可盘点" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="loc" HeaderText="库位代码">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="descs" HeaderText="库位名称">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="160px" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="库位状态">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                </asp:BoundField>
                <asp:BoundField DataField="site" HeaderText="库位地点">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                </asp:BoundField>
                <asp:BoundField DataField="domain" HeaderText="库位域">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="可盘点">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkLocs" runat="server" Checked='<%# Bind("isActive") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" Height="15px" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle fixedheader" Wrap="false" />
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
