<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Wo2_workhoursdisplay2.aspx.cs"
    Inherits="Wo2_workhoursdisplay2" %>

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
        <table id="table1" cellspacing="0" cellpadding="0" width="1080px">
            <tr>
                <td align="left" style="width: 320px">
                    日期<asp:TextBox ID="txtStart" runat="server" MaxLength="10" Width="90px" CssClass="SmallTextBox Date"></asp:TextBox>
                    --
                    <asp:TextBox ID="txtEnd" runat="server" MaxLength="10" Width="90px" CssClass="SmallTextBox Date"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="chkClose" runat="server" Text="结算" />
                </td>
                <td>
                    部门<asp:DropDownList ID="dropDept" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    工&nbsp; 号&nbsp;<asp:TextBox ID="txtUserNo" runat="server" Width="80px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td style="width: 133px">
                    姓名&nbsp;<asp:TextBox ID="txtUserName" runat="server" Width="80px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td rowspan="1" align="left">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Width="80px" Text="查询"
                        OnClick="btnSearch_Click" />
                    &nbsp;
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Width="80px" Text="导出"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" style="width: 320px">
                    地点<asp:TextBox ID="txtSite" Width="60px" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    成本中心<asp:TextBox ID="txtCenter" Width="60px" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td>
                    加工单<asp:TextBox ID="txtWorkOrder" Width="80px" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td style="width: 133px">
                    &nbsp;&nbsp; ID&nbsp;
                    <asp:TextBox ID="txtID" Width="80px" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                </td>
                <td rowspan="1" align="left">
                    <asp:Button ID="BtnAll" runat="server" CssClass="SmallButton3" Text="工单工时导出" Width="80px"
                        OnClick="BtnAll_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvUsers" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
            runat="server" PageSize="18" Width="1220px" OnPageIndexChanging="gvUsers_PageIndexChanging"
            OnRowCommand="gvUsers_RowCommand" DataKeyNames="userID">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="部门" DataField="Department">
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工段" DataField="Workshop">
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="工号" DataField="userNo">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="姓名" DataField="userName">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="考勤小时" DataField="twork" DataFormatString="{0:N2}" HtmlEncode="false">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="总工时" DataField="totalHours" DataFormatString="{0:N2}"
                    HtmlEncode="false">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="有效总工时" DataField="HoursActive" DataFormatString="{0:N2}"
                    HtmlEncode="false">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="无考勤总工时" DataField="ghours" DataFormatString="{0:N2}"
                    HtmlEncode="false">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField HeaderText="工单工时" DataTextField="Wo2All" DataTextFormatString="&lt;u&gt;{0:N2}&lt;/u&gt;"
                    ItemStyle-ForeColor="black" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    CommandName="1">
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="70px"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField HeaderText="工单有效工时" DataTextField="Wo2Active" DataTextFormatString="&lt;u&gt;{0:N2}&lt;/u&gt;"
                    ItemStyle-ForeColor="black" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    CommandName="2">
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="70px"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField HeaderText="工单无考勤工时" DataTextField="Wo2Unuse" DataTextFormatString="&lt;u&gt;{0:N2}&lt;/u&gt;"
                    ItemStyle-ForeColor="black" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    CommandName="3">
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="70px"></ItemStyle>
                </asp:ButtonField>
                <asp:BoundField HeaderText="岗位补贴" DataField="Wo2Butie" DataFormatString="{0:N2}"
                    HtmlEncode="false">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:ButtonField HeaderText="计划外总工时" DataTextField="WoAll" DataTextFormatString="&lt;u&gt;{0:N2}&lt;/u&gt;"
                    ItemStyle-ForeColor="black" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    CommandName="4">
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="70px"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField HeaderText="计划外有效工时" DataTextField="WoActive" DataTextFormatString="&lt;u&gt;{0:N2}&lt;/u&gt;"
                    ItemStyle-ForeColor="black" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    CommandName="5">
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="70px"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField HeaderText="计划外无考勤工时" DataTextField="WoUnuse" DataTextFormatString="&lt;u&gt;{0:N2}&lt;/u&gt;"
                    ItemStyle-ForeColor="black" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center"
                    CommandName="6">
                    <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="70px"></ItemStyle>
                </asp:ButtonField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="tbGridView" Width="1090px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="部门" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工段" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工号" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="姓名" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤小时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="总工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="有效总工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="无考勤总工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单有效工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="工单无考勤工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划外总工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划外有效工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="计划外无考勤工时" Width="70px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        </form>
        <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
        </script>
    </div>
</body>
</html>
