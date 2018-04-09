<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tcpInventoryInfo.aspx.cs"
    Inherits="plan_tcpInventoryInfo" %>

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
    <form id="Form1" runat="server">
    <div align="center">
        <table id="table1" cellpadding="0" cellspacing="0" border="0" style="width: 1050px">
            <tr>
                <td align="left" style="width: 30px">
                    <asp:Label ID="lblInputNo" runat="server" Text="零件:"></asp:Label>
                </td>
                <td align="left" style="width: 172px">
                    <asp:TextBox ID="txtLiitm" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                </td>
                <td align="right" style="width: 55px">
                    <asp:Label ID="Label1" runat="server" Text="库位:"></asp:Label>
                </td>
                <td style="width: 172px">
                    <asp:TextBox ID="txtLocation" runat="server" MaxLength="10"></asp:TextBox>&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="SmallButton2" 
                        onclick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvFix" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Width="1050px" GridLines="Vertical" OnPageIndexChanging="gvFix_PageIndexChanging"
            CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CssClass="GridViewHeaderStyle" Width="1050px">
                    <asp:TableRow BackColor="#006699" ForeColor="White" >
                        <asp:TableCell Text="liitm" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="Location" Width="100px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="item" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="QAD" Width="100px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="BU" Width="60px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="Lot" Width="100px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="Primary Location (P/S)" Width="150px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="Last Receipt" Width="120px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="On Hand" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="On Transit" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="UnitPrice" Width="100px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="QAD Desc" Width="200px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="liitm" HeaderText="liitm">
                    <ItemStyle HorizontalAlign="left" Width="80px" />
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Location" HeaderText="Location">
                    <ItemStyle HorizontalAlign="left" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="item" HeaderText="item">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cp_part" HeaderText="QAD">
                    <ItemStyle HorizontalAlign="center" Width="100px" />
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BU" HeaderText="BU">
                    <ItemStyle HorizontalAlign="center" Width="40px" />
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Lot" HeaderText="Lot">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Primary Location (P/S)" HeaderText="P/S">
                    <ItemStyle HorizontalAlign="center" Width="30px" />
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Last Receipt" HeaderText="Last Receipt" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <ItemStyle HorizontalAlign="center" Width="80px" />
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="On Hand" HeaderText="On Hand">
                    <ItemStyle HorizontalAlign="right" Width="70px" />
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="On Transit" HeaderText="On Transit">
                    <ItemStyle HorizontalAlign="right" Width="50px" />
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice">
                    <ItemStyle HorizontalAlign="right" Width="80px" />
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="pt_desc" HeaderText="QAD Desc">
                    <ItemStyle HorizontalAlign="Left" Width="280px" />
                    <HeaderStyle Width="280px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
