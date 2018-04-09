<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarFuelList.aspx.cs" Inherits="HR_CarFuelList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:10px;">
            <tr>
                <td>公司</td>
                <td>
                    <asp:DropDownList ID="ddlPlant" runat="server" Width="65px" AutoPostBack="True" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged">
                        <asp:ListItem Text="全部" Value="0">全部</asp:ListItem>
                        <asp:ListItem Text="SQL" Value="1">SQL</asp:ListItem>
                        <asp:ListItem Text="ZQL" Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Text="YQL" Value="5">YQL</asp:ListItem>
                        <asp:ListItem Text="HQL" Value="8">HQL</asp:ListItem>
                        <asp:ListItem Text="TCB" Value="11">TCB</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>车号</td>
                <td>
                    <asp:DropDownList ID="ddlCarNumber" runat="server" Width="85px" DataTextField="CarNumber" DataValueField="CarNumber" AutoPostBack="True" OnSelectedIndexChanged="ddlCarNumber_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>驾驶员</td>
                <td>
                    <asp:DropDownList ID="ddlDriver" runat="server" DataTextField="DriverName" DataValueField="DriverID" Width="65px" AutoPostBack="True" OnSelectedIndexChanged="ddlDriver_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>发车日期</td>
                <td>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="Date" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvFuelRecord" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False" 
        DataKeyNames="PlantCode" OnRowDataBound="gvFuelRecord_RowDataBound">
        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
        <RowStyle CssClass="GridViewRowStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
        <EmptyDataTemplate>
            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                GridLines="Vertical">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="姓名" Width="50px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="日期" Width="70px"></asp:TableCell>
			        <asp:TableCell HorizontalAlign="center" Text="车牌号" Width="150px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="加油数（升）" Width="80px"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="PlantCode" HeaderText="公司">
                <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="80px"  Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CreateName" HeaderText="姓名">
                <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CreateDate" HeaderText="日期">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CarNumber" HeaderText="车牌号">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="FuelNumber" HeaderText="加油数（升）">
                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:BoundField>      
        </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
