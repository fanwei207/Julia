<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkLineReport.aspx.cs" Inherits="IT_WorkLineReport" %>

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
        <table cellspacing="0" cellpadding="0" width="680px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="left" width="60px">
                    <asp:CheckBox ID="chkAll" runat="server" Text="全选" Width="60px" AutoPostBack="True"
                        OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                <td align="right" width="60px">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="50px" CssClass="LabelRight" Text="车间:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left" width="160px">
                    <asp:DropDownList ID="ddl_workhome" runat="server" AutoPostBack = "true" onselectedindexchanged="ddl_workhome_SelectedIndexChanged">
                    </asp:DropDownList>
                <td align="right" width="60px">
                    产线:
                </td>
                <td align="left" width="160px">
                    <asp:DropDownList ID="ddl_workline" runat="server" OnSelectedIndexChanged="ddl_workline_SelectedIndexChanged" AutoPostBack = "true">
                    </asp:DropDownList>
                </td>
                <td align="right" width="60px">
                    职责:
                </td>
                <td align="left" width="160px">
                        <asp:DropDownList ID="dropDuty" runat="server" DataTextField="ntp_duty" DataValueField="ntp_id" AutoPostBack="true"
                                                Width="100px" 
                            onselectedindexchanged="dropDuty_SelectedIndexChanged">
                                            </asp:DropDownList>
                </td>
                <td align="left">
                     <asp:Button ID="btn_Save1" runat="server" Text="保存" Width="50px" 
                         onclick="btn_Save1_Click"  />
                </td>
            </tr>
            <tr>
            <td colspan="7">&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td colspan="8" valign="top" style="height:55px" >
                    备注&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox runat="server" ID="txt_remark" TextMode="MultiLine" Height="30px" Width="540px">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWorkLog" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="20"  OnRowCommand ="gvWorkLog_RowCommand"  OnRowDataBound="gvWorkLog_RowDataBound"
            DataKeyNames="nt_id">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="680px" CellPadding="-1" CellSpacing="0" runat="server"
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
                <asp:BoundField DataField="ntt_id" HeaderText="序号">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="nt_id" HeaderText="ID">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ntp_duty" HeaderText="职责">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="ntp_type" HeaderText="事项">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="ntt_desc" HeaderText="内容">
                    <HeaderStyle Width="380px" HorizontalAlign="Center" />
                    <ItemStyle Width="380px" HorizontalAlign="left" />
                </asp:BoundField>

            </Columns>
        </asp:GridView>

        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
