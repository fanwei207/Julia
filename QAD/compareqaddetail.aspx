<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.CompareQADDetail" CodeFile="CompareQADDetail.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <font face="����"></font><font face="����"></font>
        <br>
        <table id="table1" cellspacing="0" cellpadding="0" width="480">
            <tr>
                <td colspan="3">
                    <b><font size="2">����� ��</font>
                        <asp:Label ID="Aparent" Width="200px" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td width="160">
                    <b><font size="2">����� ��</font>
                        <asp:Label ID="pson" Width="180px" runat="server"></asp:Label></b>
                </td>
                <td width="160">
                </td>
                <td width="160">
                    <b><font size="2">������ ��</font>
                        <asp:Label ID="rpson" Width="180px" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="10">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <b><font size="2">ԭ�� ��</font></b>
                    <asp:Label ID="reason" Width="440px" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" height="10">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <b><font size="2">���������</font></b>
                    <asp:Label ID="comment" runat="server" Width="440px"></asp:Label>
                </td>
            </tr>
        </table>
        <br>
        <br>
        <table id="table2" cellspacing="0" cellpadding="0" width="480" border="1">
            <tr bgcolor="#006699">
                <td align="center" width="160">
                    <b><font size="2" color="white">������</font></b>
                </td>
                <td align="center" width="160">
                    <b><font size="2" color="white">����ӹ���</font></b>
                </td>
                <td align="center" width="160">
                    <b><font size="2" color="white">����ɹ�</font></b>
                </td>
            </tr>
            <tr>
                <td align="center" bgcolor="#EEEEEE">
                    <asp:Label ID="localinv" runat="server"></asp:Label>
                </td>
                <td align="center" bgcolor="#EEEEEE">
                    <asp:Label ID="localwo" runat="server"></asp:Label>
                </td>
                <td align="center" bgcolor="#EEEEEE">
                    <asp:Label ID="localpo" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br>
        <br>
        <br>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="500px" AutoGenerateColumns="False"
            AllowPaging="true" PageSize="10">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn DataField="site" HeaderText="�ص�" ItemStyle-Width="10%"></asp:BoundColumn>
                <asp:BoundColumn DataField="Inv" HeaderText="QAD���" ItemStyle-Width="30%"></asp:BoundColumn>
                <asp:BoundColumn DataField="Wo" HeaderText="QAD�ӹ���" ItemStyle-Width="30%"></asp:BoundColumn>
                <asp:BoundColumn DataField="Po" HeaderText="QAD�ɹ�" ItemStyle-Width="30%"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid></form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
