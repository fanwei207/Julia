<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productchecklist.aspx.cs"
    Inherits="product_productchecklist" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="left">
        <table style="margin: 0 auto; padding: 5px; width: 1000px; background-image: url(../images/banner01.jpg);"
            border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 20%;">
                    <asp:Label ID="Label3" runat="server" Text="型号"></asp:Label>
                    <asp:TextBox ID="txbParentCode" runat="server" CssClass="SmallTextBox" Width="148px"></asp:TextBox>
                </td>
                <td style="width: 20%;">
                    <asp:Label ID="Label1" runat="server" Text="裸灯型号"></asp:Label>
                    <asp:TextBox ID="txbChildCode" runat="server" CssClass="SmallTextBox" Width="128px"></asp:TextBox>
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:CheckBox ID="chkCheckedItems" runat="server" Text="已质检的产品" AutoPostBack="True"
                        OnCheckedChanged="chkCheckedItems_CheckedChanged" />
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:Label ID="Label2" runat="server" Text="产品数量："></asp:Label>
                    <asp:Label ID="lblProdCount" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 15%; text-align: center;">
                    <asp:Button ID="btnSearch" runat="server" Text="查找" CssClass="SmallButton2" Width="70px"
                        OnClick="btnSearch_Click" />
                </td>
                <td style="width: 15%;">
                    <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton2" Width="70px"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCheckList" runat="server" AutoGenerateColumns="False"
            Width="1500px" CssClass="GridViewStyle" AllowPaging="True" PageSize="25" OnPageIndexChanging="gvCheckList_PageIndexChanging"
            OnRowDataBound="gvCheckList_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="1000px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="型号" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="分类" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="重量/KG" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="体积/M3" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="(裸灯)型号" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="(裸灯)重量" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="(裸灯)体积" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="(质检)重量" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="(质检)体积" Width="70px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="描述"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="ParentCode" HeaderText="型号">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CategoryName" HeaderText="分类">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalWeight" HeaderText="重量/KG">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalSize" HeaderText="体积/M3" DataFormatString="{0:F4}"
                    HtmlEncode="False">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ChildCode" HeaderText="(裸灯)型号">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ChildWeight" HeaderText="(裸灯)重量">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ChildSize" HeaderText="(裸灯)体积" DataFormatString="{0:F4}"
                    HtmlEncode="False">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalChkWeight" HeaderText="(质检)重量">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="TotalChkSize" HeaderText="(质检)体积" DataFormatString="{0:F4}"
                    HtmlEncode="False">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ParentDesc" HeaderText="描述">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Wrap="False" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
