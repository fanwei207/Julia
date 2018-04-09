<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_AttendanceCenter.aspx.cs"
    Inherits="hr_AttendanceCenter" %>

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
        <table runat="server" id="table1" cellspacing="0" cellpadding="1" width="650" align="center">
            <tr style="height: 20px">
                <td style="width: 2px" align="center">
                    &nbsp;
                </td>
                <td style="width: 250px" align="center">
                    成本中心:<asp:TextBox ID="txtCenter" runat="server" CssClass="TextLeft" Width="180px"
                        MaxLength="10" TabIndex="1" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td style="width: 150px" align="center">
                    员工工号:<asp:TextBox ID="txtUserNo" runat="server" CssClass="TextLeft" Width="80px"
                        MaxLength="10" TabIndex="2" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td style="width: 150px" align="center">
                    设备号:<asp:TextBox ID="txtSensor" runat="server" CssClass="TextLeft" Width="100px"
                        MaxLength="10" TabIndex="3" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td style="width: 100px" align="center">
                    <asp:Button ID="btnAdd" runat="server" Text="增加" CssClass="SmallButton2" Width="40px"
                        TabIndex="4" CausesValidation="true" ValidationGroup="chkAll" OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="SmallButton2" Width="40px"
                        TabIndex="5" CausesValidation="false" OnClick="btnSearch_Click" />&nbsp;
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCenter" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="18" DataKeyNames="CenterID" OnPreRender="gvCenter_PreRender"
            OnRowDeleting="gvCenter_RowDeleting" OnPageIndexChanging="gvCenter_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="table2" Width="650px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="成本中心" Width="250px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="员工工号" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="设备号" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="删除" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="Center" HeaderText="成本中心">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="UserNo" HeaderText="员工工号">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Sensor" HeaderText="设备号">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="删除">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>删除</u>" ForeColor="Black"
                            CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
