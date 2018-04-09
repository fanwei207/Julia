<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Wo2_UsersStatistic.aspx.cs"
    Inherits="wo2_Wo2_UsersStatistic" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="860px">
            <tr>
                <td align="left">
                    ����<asp:TextBox ID="txtStart" runat="server" MaxLength="10" Width="90px" CssClass="Date"></asp:TextBox>&nbsp;
                    -- &nbsp;
                    <asp:TextBox ID="txtEnd" runat="server" MaxLength="10" Width="90px" CssClass="Date"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkClose" runat="server" Text="����" />
                </td>
                <td>
                    ����<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Width="60px" Text="��ѯ"
                        OnClick="btnSearch_Click" />
                &nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Width="60px" Text="����"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvUsers" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" runat="server" PageSize="22" Width="860px"
            OnPageIndexChanging="gvUsers_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="����" DataField="userNo">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="userName">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Department">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����" DataField="Workshop">
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������ʱA" DataField="worktime" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����СʱB" DataField="totalHours" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="����(A-B)" DataField="ghours">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="�������" DataField="workdays" DataFormatString="{0:N2}"
                    HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="������" DataField="Adays" DataFormatString="{0:N2}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="900px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="120px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������ʱ" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����Сʱ" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        </form>
        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </div>
</body>
</html>
