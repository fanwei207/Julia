<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_AttendanceEntry.aspx.cs"
    Inherits="hr_AttendanceEntry" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <table runat="server" id="table1" cellspacing="0" cellpadding="1" width="800" align="center">
            <tr style="height: 20px">
                <td style="width: 2px" align="center">
                    &nbsp;
                </td>
                <td style="width: 150px" align="center">
                    工&nbsp;&nbsp;号:&nbsp;<asp:TextBox ID="txtUserNo" runat="server" CssClass="TextLeft"
                        Width="100px" MaxLength="10" TabIndex="1" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td style="width: 180px" align="center">
                    成本中心:&nbsp;<asp:DropDownList ID="ddlCenter" runat="server" Width="110px" TabIndex="2"
                        ValidationGroup="chkAll">
                    </asp:DropDownList>
                </td>
                <td style="width: 170px" align="center">
                    考勤日期:&nbsp;<asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date"
                        Width="110px" MaxLength="20" TabIndex="3" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td style="width: 100px" align="center">
                    类型:&nbsp;<asp:DropDownList ID="ddlType" runat="server" Width="50px" TabIndex="4">
                        <asp:ListItem Value="0" Text="--"></asp:ListItem>
                        <asp:ListItem Value="I" Text="上班"></asp:ListItem>
                        <asp:ListItem Value="O" Text="下班"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 100px" align="center">
                    类别:&nbsp;<asp:DropDownList ID="ddlUserType" runat="server" Width="50px" TabIndex="5">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px" align="center">
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton2" Width="40px"
                        TabIndex="6" CausesValidation="true" ValidationGroup="chkAll" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="40px"
                        TabIndex="7" CausesValidation="false" OnClick="btnSearch_Click" />&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvAttendance" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="25" DataKeyNames="AttendanceID,isManual"
            OnPreRender="gvAttendance_PreRender" OnRowDeleting="gvAttendance_RowDeleting"
            OnPageIndexChanging="gvAttendance_PageIndexChanging" OnRowDataBound="gvAttendance_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="table2" Width="800px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="工号" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="成本中心" Width="180px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤日期" Width="170px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考勤类型" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="考核类别" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="删除" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="AttendanceUserNo" HeaderText="工号">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Center" HeaderText="成本中心">
                    <HeaderStyle Width="180px" HorizontalAlign="Center" />
                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="AttendanceTime" HeaderText="考勤日期">
                    <HeaderStyle Width="170px" HorizontalAlign="Center" />
                    <ItemStyle Width="170px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AttendanceType" HeaderText="考勤类型">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UserType" HeaderText="考核类别">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="删除">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                            CommandName="Delete" OnClientClick=" if(confirm(‘Are you sure to delete this?’)){client_confirmed = true;} else{ client_confirmed = false; return false;} "></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/vbscript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
