<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qad_bom_associatedDocsReport.aspx.cs"
    Inherits="QAD_qad_bom_associatedDocsReport" %>

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
    <div align="center">
        <div style="width: 1000px;">
            <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: left;">
                <tr>
                    <td style="width: 25%; padding-left: 2px;">
                        <asp:Label ID="Label1" runat="server" Text="QAD号"></asp:Label>
                        <asp:TextBox ID="txbQad" runat="server" CssClass="SmallTextBox Part" Width="170px"></asp:TextBox>
                    </td>
                    <td style="width: 35%;">
                        <asp:Label ID="Label2" runat="server" Text="出运日期"></asp:Label>
                        <asp:TextBox ID="txbBeginDate" runat="server" CssClass="SmallTextBox Date" Width="120px"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="-"></asp:Label>
                        <asp:TextBox ID="txbEndDate" runat="server" CssClass="SmallTextBox Date" Width="120px"></asp:TextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:CheckBox ID="chkHasDocs" runat="server" Text="仅有文档" AutoPostBack="True" OnCheckedChanged="chkHasDocs_CheckedChanged" />
                    </td>
                    <td style="width: 15%; text-align: right;">
                        <asp:Button ID="btnSearch" runat="server" Text="查找" CssClass="SmallButton2" Width="100px"
                            OnClick="btnSearch_Click" />
                    </td>
                    <td style="width: 15%; text-align: right; padding-right: 2px;">
                        <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton2" Width="100px"
                            OnClick="btnExport_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvBOM" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="1000px" AllowPaging="True" AllowSorting="True" PageSize="25" OnRowDataBound="gvBOM_RowDataBound"
                OnPageIndexChanging="gvBOM_PageIndexChanging">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="1000px" CellPadding="0" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="有无文档" Width="70px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="QAD号" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="老部件号" Width="120px" HorizontalAlign="Center"></asp:TableCell>
                            <asp:TableCell Text="QAD描述" HorizontalAlign="Center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="qadItemID" HeaderText="id" Visible="False" />
                    <asp:BoundField DataField="hasDocs" HeaderText="有无文档">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qad" HeaderText="QAD号">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="oldcode" HeaderText="老部件号">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qadDescs" HeaderText="QAD描述">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:TextBox ID="txbQad_bak" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txbBeginDate_bak" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txbEndDate_bak" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txbResultsCount" runat="server" Visible="false"></asp:TextBox>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
