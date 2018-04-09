<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_DeclarationList.aspx.cs"
    Inherits="SID_SID_DeclarationList" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="960px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td>
                    <asp:Label ID="lblShipNo" runat="server" Width="80px" CssClass="LabelRight" Text="���˵���:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtShipNo" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblVerfication" runat="server" Width="80px" CssClass="LabelRight"
                        Text="��������:" Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtVerfication" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="2" Text="��ѯ"
                        Width="50px" OnClick="btnQuery_Click" Height="25px" />
                </td>
                <td style="width: 5%" align="Center">
                    <asp:Button ID="btnViewDetail" runat="server" Width="40px" Text="��ϸ" ToolTip="�鿴�ͻ�������ϸ"
                        CssClass="SmallButton3" OnClick="btnViewDetail_Click" Height="25px" />
                </td>
                <td style="width: 8%" align="Right">
                    <asp:Label ID="lblDiff" runat="server" Width="60px" Text="�ۼƲ���:"></asp:Label>
                </td>
                <td style="width: 12%" align="Left">
                    <asp:Label ID="lblDiffValue" runat="server" Width="100px"></asp:Label>
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="24" OnPreRender="gvSID_PreRender"
            DataKeyNames="ShipNo" OnRowDataBound="gvSID_RowDataBound" OnPageIndexChanging="gvSID_PageIndexChanging"
            Width="960px" OnRowCommand="gvSID_RowCommand" OnRowDeleting="gvSID_RowDeleting">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="960px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="״̬" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���˱��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ջ���" Width="300px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Ŀ�ĸ�" Width="110px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ó�׷�ʽ" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���䷽ʽ" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���˵���" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ͬ����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�˵ֹ�" Width="90px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��ϸ" Width="40px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="Status" HeaderText="״̬">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipNo" HeaderText="���˱��">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="�ջ���">
                    <HeaderStyle Width="300px" HorizontalAlign="Center" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Harbor" HeaderText="Ŀ�ĸ�">
                    <HeaderStyle Width="110px" HorizontalAlign="Center" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Trade" HeaderText="ó�׷�ʽ">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipVia" HeaderText="���䷽ʽ">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="BL" HeaderText="���˵���">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="��������">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Verfication" HeaderText="��������">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Country" HeaderText="�˵ֹ�">
                    <HeaderStyle Width="90px" HorizontalAlign="Center" />
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField Text="<u>��ϸ</u>" CommandName="Detail">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:TemplateField>
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="<u>ɾ��</u>" ForeColor="Black"
                            CommandName="Delete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
