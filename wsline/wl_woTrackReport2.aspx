<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wl_woTrackReport2.aspx.cs"
    Inherits="wsline_wl_woTrackReport2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin: 0 auto; width: 1200px; background-image: url(../images/banner01.jpg);"
            border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td rowspan="2" style="width: 480px">
                    <asp:Label ID="Label4" runat="server" Text="��˾"></asp:Label>
                    <asp:DropDownList ID="dropPlants" runat="server" Width="180px">
                    </asp:DropDownList>
                    &nbsp;&nbsp; &nbsp;
                    <asp:Label ID="Label5" runat="server" Text="������"></asp:Label>
                    <asp:TextBox ID="txbNbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 24px; width: 40px; text-align: right;">
                    <asp:Label ID="Label6" runat="server" Text="��·��"></asp:Label>
                </td>
                <td style="height: 24px; width: 180px; text-align: left;">
                    <asp:TextBox ID="pcbBeginDate" runat="server" CssClass="SmallTextBox Date" 
                        Width="80px"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="pcbEndDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                </td>
                <td style="height: 24px; width: 40px; text-align: right;">
                    <asp:Label ID="Label9" runat="server" Text="ë��"></asp:Label>
                </td>
                <td style="height: 24px; width: 180px; text-align: left;">
                    <asp:TextBox ID="mgBeginDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="mgEndDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                </td>
                <td rowspan="2" style="text-align: center;">
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" CssClass="SmallButton2" Width="70px"
                        OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton2" Width="70px"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 380px">
                    �´�����:<asp:TextBox ID="txtWoDueDate1" runat="server" 
                        CssClass="SmallTextBox Date" Width="75px"></asp:TextBox>
                    -<asp:TextBox ID="txtWoDueDate2" runat="server" CssClass="SmallTextBox Date" 
                        Width="75px"></asp:TextBox>
                    �������:<asp:TextBox ID="txtWoCompDate1" runat="server" 
                        CssClass="SmallTextBox Date" Width="75px"></asp:TextBox>
                    -<asp:TextBox ID="txtWoCompDate2" runat="server" CssClass="SmallTextBox Date" 
                        Width="75px"></asp:TextBox>
                </td>
                <td style="height: 24px; width: 40px; text-align: right;">
                    <asp:Label ID="Label10" runat="server" Text="����"></asp:Label>
                </td>
                <td style="height: 24px; width: 180px; text-align: left;">
                    <asp:TextBox ID="onlineBeginDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="onlineEndDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                </td>
                <td style="height: 24px; width: 40px; text-align: right;">
                    <asp:Label ID="Label11" runat="server" Text="����"></asp:Label>
                </td>
                <td style="height: 24px; width: 180px; text-align: left;">
                    <asp:TextBox ID="offlineBeginDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="-"></asp:Label>
                    <asp:TextBox ID="offlineEndDate" runat="server" CssClass="SmallTextBox Date" Width="80px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvWoTrackReports" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="25" EnableViewState="False" CssClass="GridViewStyle GridViewRebuild"
            OnPageIndexChanging="gvWoTrackReports_PageIndexChanging" 
            OnRowDataBound="gvWoTrackReports_RowDataBound" Width="1610px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1200px" CellPadding="0" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����ID" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�´�����" Width="70px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��·�巢��" Width="110px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="��·�巢�ϻ㱨��" Width="120px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="ë�ܷ���" Width="110px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="ë�ܷ��ϻ㱨��" Width="110px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="110px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="���߻㱨��" Width="100px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="110px" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="���߻㱨��" Width="100px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="nbr" HeaderText="������">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="lot" HeaderText="����ID">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="woPart" HeaderText="������">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="relDate" HeaderText="�´�����">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_status" HeaderText="״̬">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_type" HeaderText="����">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_ord" HeaderText="������">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_comp" HeaderText="�깤��">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="tr_qty_loc" HeaderText="�����">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="tr_date" HeaderText="�������">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="pcb" HeaderText="��·�巢��">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="pcbReporter" HeaderText="��·�巢�ϻ㱨��">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="mg" HeaderText="ë�ܷ���">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="mgReporter" HeaderText="ë�ܷ��ϻ㱨��">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="online" HeaderText="����">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="onlineReporter" HeaderText="���߻㱨��">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="offline" HeaderText="����">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="offlineReporter" HeaderText="���߻㱨��">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal runat="server" id="ltlAlert" EnableViewState="false"></asp:Literal>
    </script>
</body>
</html>
