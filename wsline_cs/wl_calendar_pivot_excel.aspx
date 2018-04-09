<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_calendar_pivot_excel.aspx.cs"
    Inherits="wsline_cs_wl_calendar_pivot_excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
</head>
<body>
    <asp:GridView ID="gv_hac" runat="server" AutoGenerateColumns="False" BorderColor="#999999"
        BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" Width="1440px"
        OnRowDataBound="gv_pv_RowDataBound">
        <RowStyle BackColor="#EEEEEE" Font-Names="Tahoma,Arial" Font-Size="8pt" HorizontalAlign="Center"
            Width="100px" Height="22px" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Tahoma,Arial" Font-Size="8pt"
            ForeColor="White" HorizontalAlign="Center" />
        <AlternatingRowStyle BackColor="Gainsboro" />
    </asp:GridView>
</body>
</html>
