<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_List.aspx.cs" Inherits="SID_SID_List" %>

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
        <table cellspacing="0" cellpadding="0" width="980px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkAll" runat="server" Text="ȫѡ" Width="60px" AutoPostBack="True"
                        OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                <td align="right">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="100px" CssClass="LabelRight" Text="ϵͳ���˵���(PK):"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSysPKNo" runat="server" Width="80px" TabIndex="1" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblSysPKRef" runat="server" Width="55px" CssClass="LabelRight" Text="�ο�(Ref):"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSysPKRef" runat="server" Width="80px" TabIndex="2" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right">
                    ��������:
                </td>
                <td align="right">
                    <asp:TextBox ID="txtShipDate1" runat="server" CssClass="smalltextbox Date" TabIndex="3"
                        Width="60px"></asp:TextBox>-<asp:TextBox ID="txtShipDate2" runat="server" CssClass="smalltextbox Date"
                            TabIndex="3" Width="60px"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" CssClass="LabelRight" Text="���˵���:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtShipNo" runat="server" Width="80px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDomain" runat="server" Width="20px" CssClass="LabelRight" Text="��:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDomain" runat="server" Width="60px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="Left">
                    &nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="4"
                        Text="��ѯ" Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td align="Left">
                    &nbsp;<asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton2" TabIndex="5"
                        Text="����" Width="50px" OnClick="btnAddNew_Click" />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvSID" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle AutoPageSize" PageSize="20" OnPreRender="gvSID_PreRender"
            DataKeyNames="SID" OnPageIndexChanging="gvSID_PageIndexChanging" Width="980px"
            OnRowDataBound="gvSID_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="��" Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ϵͳ���˵���" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ο�" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���˵���" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="װ��ص�" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���䷽ʽ" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="400px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��" Width="30px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PK" HeaderText="ϵͳ���˵���">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PKRef" HeaderText="�ο�">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Nbr" HeaderText="���˵���">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Site" HeaderText="װ��ص�">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OutDate" HeaderText="��������">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Via" HeaderText="���䷽ʽ">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Shipto" HeaderText="����">
                    <HeaderStyle Width="400px" HorizontalAlign="Center" />
                    <ItemStyle Width="400px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
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
