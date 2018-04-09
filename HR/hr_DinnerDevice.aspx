<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hr_DinnerDevice.aspx.cs"
    Inherits="hr_DinnerDevice" %>

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
        <table runat="server" id="table1" cellspacing="0" cellpadding="1" width="600" align="center">
            <tr style="height: 20px">
                <td style="width: 2px" align="center">
                    &nbsp;
                </td>
                <td style="width: 250px" align="center">
                    »úÆ÷±àºÅ:<asp:TextBox ID="txtDevice" runat="server" CssClass="TextLeft" Width="180px"
                        MaxLength="10" TabIndex="1" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td style="width: 250px" align="center">
                    Éè±¸±àºÅ:<asp:TextBox ID="txtSensor" runat="server" CssClass="TextLeft" Width="180px"
                        MaxLength="10" TabIndex="2" ValidationGroup="chkAll"></asp:TextBox>
                </td>
                <td style="width: 100px" align="center">
                    <asp:Button ID="btnAdd" runat="server" Text="Ôö¼Ó" CssClass="SmallButton3" Width="40px"
                        TabIndex="3" CausesValidation="true" ValidationGroup="chkAll" OnClick="btnAdd_Click" />&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="²éÑ¯" CssClass="SmallButton3" Width="40px"
                        TabIndex="4" CausesValidation="false" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDevice" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="18" DataKeyNames="DeviceID" OnPreRender="gvDevice_PreRender"
            OnRowDeleting="gvDevice_RowDeleting" OnPageIndexChanging="gvDevice_PageIndexChanging">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="table2" Width="600px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="»úÆ÷±àºÅ" Width="250px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Éè±¸±àºÅ" Width="250px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="É¾³ý" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="Device" HeaderText="»úÆ÷±àºÅ">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Sensor" HeaderText="Éè±¸±àºÅ">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>É¾³ý</u>" ForeColor="Black"
                            CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
