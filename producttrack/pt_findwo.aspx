<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pt_findwo.aspx.cs" Inherits="producttrack_pt_findwo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body style="background-color: Window">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table style="width: 1004px">
            <tr>
                <td>
                    Lot/Serial<asp:TextBox ID="txtLot" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>
                </td>
                <td>
                    Product Date<asp:TextBox ID="txtDate" runat="server" CssClass="SmallTextBox Date"
                        Width="100px"></asp:TextBox>
                    -<asp:TextBox ID="txtDate1" runat="server" CssClass="SmallTextBox Date" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="SmallButton3" Width="60px"
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWO" runat="server" AllowPaging="true" PageSize="25" AutoGenerateColumns="False"
            DataKeyNames="tr_lot" Width="984px" OnPageIndexChanging="gvWO_PageIndexChanging"
            OnRowCommand="gvWO_RowCommand" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="tr_domain" HeaderText="Domain">
                    <ItemStyle HorizontalAlign="center" Width="60px" />
                    <HeaderStyle HorizontalAlign="center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="tr_serial" HeaderText="Lot/Serial">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="tr_nbr" HeaderText="WO Nbr">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="tr_lot" HeaderText="WO Lot">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="tr_part" HeaderText="WO Part">
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="tr_qty_loc" HeaderText="WO Qty" DataFormatString="{0:#0}">
                    <ItemStyle HorizontalAlign="right" Width="80px" />
                    <HeaderStyle HorizontalAlign="center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="tr_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="RCT Date">
                    <ItemStyle HorizontalAlign="center" Width="80px" />
                    <HeaderStyle HorizontalAlign="center" Width="80px" />
                </asp:BoundField>
                <asp:ButtonField CommandName="Detail1" Text="<u>Detail</u>">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:BoundField DataField="" HeaderText="">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblmsg" runat="server" Visible="true" ForeColor="red"></asp:Label>
        </form>
        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
