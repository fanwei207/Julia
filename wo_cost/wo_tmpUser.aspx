<%@ Page Language="VB" AutoEventWireup="false" CodeFile="wo_tmpUser.aspx.vb" Inherits="wo_cost_wo_tmpUser" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="930">
            <tr>
                <td>
                    ����&nbsp;<asp:TextBox ID="txtDate" runat="server" Width="80px" MaxLength="10" CssClass="Date"></asp:TextBox>
                    &nbsp;ȫ�� &nbsp;<asp:CheckBox ID="chkall" runat="server" Checked="true" />
                </td>
                <td>
                    �ɱ�����&nbsp;<asp:DropDownList ID="dropCenter" runat="server" Width="150px">
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <td>
                    ������ԱA����&nbsp;<asp:TextBox ID="txtNumberA" runat="server" Width="40px"></asp:TextBox>
                    &nbsp;������ԱB����&nbsp;<asp:TextBox ID="txtNumberB" runat="server" Width="40px"></asp:TextBox>
                    &nbsp;�����ɱ�����&nbsp;
                    <asp:TextBox ID="txtCenter" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="��ѯ" Width="60px" CssClass="SmallButton3"
                        CausesValidation="false" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="����" Width="60px" CssClass="SmallButton3" />
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="Datagrid1" runat="server" Width="930px" AllowPaging="true" PageSize="20"
            AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="ID" Visible="false" />
                <asp:BoundColumn DataField="Center" HeaderText="�ɱ�����" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundColumn DataField="Userdate" HeaderText="����" ItemStyle-Width="160" ItemStyle-HorizontalAlign="Center"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundColumn DataField="WorkerA" HeaderText="������ԱA����" ItemStyle-Width="180" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundColumn DataField="WorkerB" HeaderText="������ԱB����" ItemStyle-Width="180" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundColumn DataField="RelCenter" HeaderText="�����ɱ�����" ItemStyle-Width="180"
                    ItemStyle-HorizontalAlign="Right" />
                <asp:ButtonColumn Text="<u>ɾ��</u>" CommandName="Delete">
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        <br />
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
