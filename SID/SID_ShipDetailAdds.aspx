<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_ShipDetailAdds.aspx.cs"
    Inherits="SID_SID_ShipDetailAdds" %>

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
    <div>
        <table style="width: 1000px;" cellspacing="0" cellpadding="4" class="table05">
            <tr>
                <td>
                    ϵ &nbsp; &nbsp; &nbsp;&nbsp; ��:
                </td>
                <td>
                    <asp:TextBox ID="txtSNO" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ���ϱ���:
                </td>
                <td>
                    <asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��������:
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtQtySet" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ֻ&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; ��:
                </td>
                <td>
                    <asp:TextBox ID="txtQtyPcs" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��:
                </td>
                <td>
                    <asp:TextBox ID="txtQtyBox" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;��:
                </td>
                <td>
                    <asp:TextBox ID="txtPkgs" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��&nbsp; �� &nbsp;��:
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtQA" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��&nbsp;&nbsp;��&nbsp;��:
                </td>
                <td>
                    <asp:TextBox ID="txtWO" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    ���۶���:
                </td>
                <td>
                    <asp:TextBox ID="txtNbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��:
                </td>
                <td>
                    <asp:TextBox ID="txtLine" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    TCP&nbsp; ����:
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtPO" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    �ͻ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtCustPart" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    �ͻ�����:
                </td>
                <td>
                    <asp:TextBox ID="txtFob" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;��:
                </td>
                <td>
                    <asp:TextBox ID="txtWeight" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��:
                </td>
                <td style="width: 108px">
                    <asp:TextBox ID="txtVolume" runat="server" CssClass="SmallTextBox Numeric" Width="100px"></asp:TextBox>
                </td>
                <td>
                    Fedex:
                </td>
                <td>
                    <asp:TextBox ID="txtFedx" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 23px">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ע:
                </td>
                <td colspan="5" style="height: 23px">
                    <asp:TextBox ID="txtMemo" runat="server" CssClass="SmallTextBox" Width="98%"></asp:TextBox>
                </td>
                <td colspan="5" align="center">
                    <asp:Button ID="btnAdd" runat="server" Width="60px" CssClass="SmallButton2" Text="����"
                        OnClick="btnAdd_Click" />&nbsp;&nbsp;
                    <asp:TextBox ID="txtDID" runat="server" Width="27px" Visible="False">0</asp:TextBox>
                    <asp:Button ID="btnBack" runat="server" Width="60px" CssClass="SmallButton2" Text="����"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShipdetails" runat="server" AutoGenerateColumns="False" DataKeyNames="SDID"
            Width="1600px" OnRowDeleting="gvShipdetails_RowDeleting" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="SNO" HeaderText="ϵ��" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                    <HeaderStyle Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="QAD" HeaderText="���ϱ���" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_set" HeaderText="��������" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_box" HeaderText="����" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="qa" HeaderText="�̼��" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="so_nbr" HeaderText="���۶���" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="so_line" HeaderText="�к�" ReadOnly="true">
                    <ItemStyle HorizontalAlign="center" Width="30px" />
                    <HeaderStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="wo" HeaderText="�����" ReadOnly="true">
                    <ItemStyle HorizontalAlign="center" Width="50px" />
                    <HeaderStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="po" HeaderText="TCP����" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="cust_part" HeaderText="�ͻ�����" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="weight" HeaderText="����" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="volume" HeaderText="���" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:CommandField DeleteText="<u>ɾ��</u>" ShowDeleteButton="True">
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:BoundField DataField="pkgs" HeaderText="����" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="qty_pcs" HeaderText="ֻ��" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fedx" HeaderText="Fedex" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="fob" HeaderText="�ͻ�����" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="memo" HeaderText="��ע" ReadOnly="true">
                    <HeaderStyle Width="300px" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
