<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkShopInspectionReport.aspx.cs" Inherits="WorkShopInspectionReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        <table cellspacing="0" cellpadding="0" style="width: 1020px;">
            <tr>
                <td style=" width:30px;">
                    公司:
                </td>
                <td>
                    <asp:DropDownList ID="dropDomain" runat="server">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style=" width:30px;">
                    日期:
                </td>
                <td>
                    <asp:TextBox ID="txtstartDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>--
                    <asp:TextBox ID="txtendDate" CssClass="Date Param" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td style=" width:100px;">
                    员工工号/卡号:
                </td>
                <td Width="100px">
                    <asp:TextBox ID="txtUser" runat="server" Width="100px" CssClass="Param"></asp:TextBox>
                </td>
                <td align="right" width="60px">
                    <asp:Label ID="Label2" runat="server" Width="60px" CssClass="LabelRight" Text="巡检类别:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td width＝"100px" align"left">
                    <asp:DropDownList ID="dropType" runat="server" DataTextField="ntp_duty" DataValueField="ntp_id"
                        AutoPostBack="true" Width="100px" Height="30px">
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                </td>
                <td colspan="2">
                    <asp:Button ID="btn_export" runat="server" Text="导出" CssClass="SmallButton3" OnClick="btn_export_Click" />
                </td>
                <td width="200px"></td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="1020px" PageSize="20" DataKeyNames="id" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
            OnRowCommand="gv_RowCommand">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="id" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Right" Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="公司" DataField="ws_plantCode" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="日期" DataField="ws_createdDateTime" ReadOnly="True" 
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" HtmlEncode="False">
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="人员" DataField="ws_userName" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="内容" DataField="ws_inspectionContent" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="580px" />
                    <ItemStyle HorizontalAlign="Left" Width="580px" />
                </asp:BoundField>
            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
