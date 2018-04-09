<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_DocumentInfo.aspx.cs"
    Inherits="SID_DocumentInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="HEAD1" runat="server">
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
        <table cellspacing="0" cellpadding="0" width="1010px" border="0" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="right">
                    <asp:Label ID="lblShipNo" runat="server" Width="60px" CssClass="LabelRight" Text="���˵���:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtShipNo" runat="server" Width="100px" TabIndex="1"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lblDate" runat="server" Width="50px" CssClass="LabelRight" Font-Bold="false"
                        Text="����:"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDate" runat="server" Width="100px" CssClass="Date" TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsCabin" runat="server" Text="�������" Checked="true" Width="80px" />
                </td>
                <td align="center">
                    <asp:Button ID="btnQuery" runat="server" Text="��ѯ" CssClass="SmallButton3" Width="60px"
                        OnClick="btnQuery_Click" TabIndex="3" Height="25px" />
                </td>
                <td>
                    <input id="filename1" style="width: 380px; height: 20px" type="file" size="45" name="filename1"
                        runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnImport" runat="server" CssClass="SmallButton2" 
                        onclick="btnImport_Click" Text="����" />
&nbsp;</td>
                <td>
                    <label id="here" onclick="submit();">
                        <a href="/docs/SID_DocImport.xls" target="blank"><font color="blue">����ģ��</font></a>
                    </label>
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvDocument" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild" PageSize="25"
            OnPreRender="gvDocument_PreRender" OnPageIndexChanging="gvDocument_PageIndexChanging"
            Width="1050px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1010px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="���˵���" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���ϱ���" Width="200px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="250px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ATL����" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ATL���" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="TCP����" Width="50px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="TCP���" Width="80px" HorizontalAlign="center"></asp:TableCell>
                         <asp:TableCell Text="���" Width="80px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="DocumentNo" HeaderText="���˵���">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderNo" HeaderText="������">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ItemNo" HeaderText="���ϱ���">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="ItemDesc" HeaderText="��������">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="Qty" HeaderText="ֻ��" DataFormatString="{0:#0}" HtmlEncode="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
               <asp:BoundField DataField="Sets_Qty" HeaderText="����" DataFormatString="{0:#0}" HtmlEncode="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
               <asp:BoundField DataField="SID_Ptype" HeaderText="�Ƽ�" DataFormatString="{0:#0}" HtmlEncode="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ATLPrice" HeaderText="ATL����" DataFormatString="{0:#0.000}"
                    HtmlEncode="false">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ATLAmount" HeaderText="ATL���" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TCPPrice" HeaderText="TCP����" DataFormatString="{0:#0.000}"
                    HtmlEncode="false">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="TCPAmount" HeaderText="TCP���" DataFormatString="{0:#0.00}"
                    HtmlEncode="false">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
               <asp:BoundField DataField="IsCabin" HeaderText="���" DataFormatString="{0:#0}" HtmlEncode="false">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script language="javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
