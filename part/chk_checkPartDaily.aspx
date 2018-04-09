<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chk_checkPartDaily.aspx.cs"
    Inherits="part_chk_checkPartDaily" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="text-align: center;">
    <div style="text-align: center;">
        <table style="margin: 0 auto; padding: 5px; width: 1000px; background-image: url(../images/banner01.jpg);"
            border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 40%;">
                    <asp:Label ID="Label3" runat="server" Text="�̵�����"></asp:Label>
                    <asp:TextBox ID="txbCheckedDate" runat="server" CssClass="SmallTextBox Date" Width="100px"></asp:TextBox>
                    -<asp:TextBox ID="txbCheckedDateEnd" runat="server" CssClass="SmallTextBox Date" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 35%;">
                    <asp:Label ID="Label2" runat="server" Text="��λ"></asp:Label>
                    <asp:DropDownList ID="dropLocs" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td style="width: 15%;">
                    <asp:Label ID="Label6" runat="server" Text="����:"></asp:Label>
                    <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 15%;">
                    <asp:Button ID="btnSearch" runat="server" Text="����" CssClass="SmallButton2" Width="70px"
                        OnClick="btnSearch_Click" />
                </td>
                <td style="width: 15%;">
                    <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton2" Width="70px"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style=" height:10px; color:Red;">
                    ע�⣺�ڵ����̵���ǰ��ϵͳ������ʾ�������</td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCheckedPart" runat="server" AutoGenerateColumns="False" Width="1500px"
            CssClass="GridViewStyle AutoPageSize" AllowPaging="True" PageSize="25" OnPageIndexChanging="gvCheckedPart_PageIndexChanging"
            OnRowDataBound="gvCheckedPart_RowDataBound">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" Height="15px" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1500px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="�ص�" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��λ" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="QAD��" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="120px" HorizontalAlign="center"></asp:TableCell>
                       
                        <asp:TableCell Text="�������" Width="70px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="�̵�����" Width="70px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="����ԭ��" Width="250px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" HorizontalAlign="Left"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="site" HeaderText="�ص�">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="loc" HeaderText="��λ">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="part" HeaderText="QAD��">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="code" HeaderText="������">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
               
                <asp:BoundField DataField="sysQty" HeaderText="�������">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="relQty" HeaderText="�̵�����">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="diff" HeaderText="����ԭ��">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="descs" HeaderText="����">
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:Label ID="lblcheckedName" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblkeepedName" runat="server" Text="" Visible="false"></asp:Label>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
