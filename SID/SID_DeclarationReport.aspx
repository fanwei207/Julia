<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_DeclarationReport.aspx.cs"
    Inherits="SID_DeclarationReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="400px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="left">
                    &nbsp;<asp:TextBox ID="txtYear" runat="server" Width="60px" CssClass="Numeric" TabIndex="1"
                        OnTextChanged="txtYear_TextChanged"></asp:TextBox>&nbsp;��&nbsp;
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="40px" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                        TabIndex="2">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;��&nbsp;
                    <asp:CheckBox ID="chkNoZero" AutoPostBack="true" Text="��0���" runat="server" OnCheckedChanged="chkNoZero_CheckedChanged"
                        TabIndex="3" />&nbsp;
                    <asp:CheckBox ID="chkIsSum" AutoPostBack="true" Text="��ʾ����" runat="server" TabIndex="4"
                        OnCheckedChanged="chkIsSum_CheckedChanged" />
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" Text="��ѯ" CssClass="SmallButton3" Width="60px"
                        OnClick="btnQuery_Click" TabIndex="5" Height="25px" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDeclaration" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" PageSize="25"
            OnPreRender="gvDeclaration_PreRender" OnPageIndexChanging="gvDeclaration_PageIndexChanging"
            Width="400px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="400px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="���ط�Ʊǰ׺" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ط�Ʊ��" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ط�Ʊ����" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ط�Ʊ���" Width="110px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="DeclarationPrefix" HeaderText="ǰ׺">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DeclarationNo" HeaderText="���ط�Ʊ��">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DeclarationShipDate" HeaderText="���ط�Ʊ����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DeclarationAmount" HeaderText="���ط�Ʊ���" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="110px" HorizontalAlign="Center" />
                    <ItemStyle Width="110px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvDeclarationSum" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle" PageSize="25" OnPreRender="gvDeclarationSum_PreRender"
            OnPageIndexChanging="gvDeclarationSum_PageIndexChanging" Width="400px">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="400px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="���ط�Ʊǰ׺" Width="130px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ط�ƱƱ��" Width="130px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ط�Ʊ���" Width="130px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="DeclarationPrefix" HeaderText="ǰ׺">
                    <HeaderStyle Width="130px" HorizontalAlign="Center" />
                    <ItemStyle Width="130px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DeclarationCount" HeaderText="���ط�ƱƱ��" DataFormatString="{0:#0}"
                    HtmlEncode="false">
                    <HeaderStyle Width="130px" HorizontalAlign="Center" />
                    <ItemStyle Width="130px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="DeclarationAmount" HeaderText="���ط�Ʊ���" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="130px" HorizontalAlign="Center" />
                    <ItemStyle Width="130px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
