<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_SynStage856.aspx.cs"
    Inherits="SID_SynStage856" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
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
        <table cellspacing="0" cellpadding="0" width="750px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="right">
                    <asp:Label ID="lblInvNo" runat="server" Width="55px" CssClass="LabelRight" Text="发票号码:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtInvNo" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" CssClass="LabelRight" Text="出运单号:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtShipNo" runat="server" Width="120px" TabIndex="2"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDate" runat="server" Width="55px" CssClass="LabelRight" Text="出运日期:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtShipDate" runat="server" Width="90px" TabIndex="3" CssClass="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkAll" runat="server" TabIndex="3" Text="显示全部" AutoPostBack="true"
                        OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                <td align="Center">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="4" Text="查询"
                        Width="40px" OnClick="btnQuery_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSynStageList" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" PageSize="20"
            OnPreRender="gvSynStageList_PreRender" OnPageIndexChanging="gvSynStageList_PageIndexChanging"
            Width="750px" OnRowDataBound="gvSynStageList_RowDataBound" OnRowCommand="gvSynStageList_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="750px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="发票号码" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运单号" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="出运日期" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="周期章" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="同步操作" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="明细" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="InvoiceNo" HeaderText="发票号码">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipNo" HeaderText="出运单号">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="出运日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ZhangQiZhang" HeaderText="周期章">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:ButtonField Text="<u>同步操作</u>" CommandName="SynStage">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:ButtonField Text="<u>明细</u>" CommandName="Detail">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="false" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:BoundField DataField="SID_Container" HeaderText="Container" Visible="false"
                    HeaderStyle-Font-Bold="false" />
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
