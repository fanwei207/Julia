<%@ Page Language="C#" AutoEventWireup="true" CodeFile="note_WorkLineReport.aspx.cs"
    Inherits="note_WorkLineReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="2" width="760px" border="0" class="main_top">
            <tr>
                <td align="left" width="40px">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="60px" AutoPostBack="True"
                        OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                <td align="right" width="50px">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="50px" CssClass="LabelRight" Text="车间:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" width="120px">
                    <asp:DropDownList ID="dropDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_workhome_SelectedIndexChanged"
                        Height="30px">
                    </asp:DropDownList>
                </td>
                <td align="right" width="50px">
                    工段:
                </td>
                <td align="left" width="120px">
                    <asp:DropDownList ID="dropWorkShop" runat="server" OnSelectedIndexChanged="ddl_workline_SelectedIndexChanged"
                        AutoPostBack="true" Height="30px">
                    </asp:DropDownList>
                </td>
                <td align="right" width="50px">
                    <asp:Literal ID="lt_duty" runat="server" Text="职责:"></asp:Literal>
                </td>
                <td align="left" width="120px">
                    <asp:DropDownList ID="dropDuty" runat="server" DataTextField="ntp_duty" DataValueField="ntp_id"
                        AutoPostBack="true" Width="120px" Height="30px" OnSelectedIndexChanged="dropDuty_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Button ID="btn_Save1" runat="server" Text="保存" Width="50px" Height="30px" OnClick="btn_Save1_Click" />
                    <input id="hidMstrID" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="8" valign="top" style="height: 55px">
                    <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Height="61px" 
                        Width="100%" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWorkLog" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="20"
            OnRowCommand="gvWorkLog_RowCommand" 
            OnRowDataBound="gvWorkLog_RowDataBound" DataKeyNames="ntt_id,ntd_id,ntd_sel,ntt_desc"
            Width="760px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="690px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="√" Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="序号" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ID" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="职责" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="事项" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="内容" Width="380px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ntt_index" HeaderText="序号">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ntp_duty" HeaderText="职责">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="ntp_type" HeaderText="事项">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="ntt_desc" HeaderText="内容">
                    <HeaderStyle Width="400px" HorizontalAlign="Center" />
                    <ItemStyle Width="400px" HorizontalAlign="left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
</body>
</html>
