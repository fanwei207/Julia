<%@ Page Language="VB" AutoEventWireup="false" CodeFile="costProcedure.aspx.vb" Inherits="admin_costProcedure" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="600">
            <tr>
                <td>
                    ��&nbsp;
                    <asp:TextBox ID="txtYear" runat="server" Width="60px" MaxLength="4"></asp:TextBox>&nbsp;&nbsp;
                    ��&nbsp;
                    <asp:DropDownList ID="dropmonth" runat="server" Width="50px" Font-Size="10pt" CssClass="smallbutton2">
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
        <asp:DropDownList ID="dropCost" runat="server" Visible="false">
        </asp:DropDownList>
                </td>
                <td align="left" style="width: 120px">
                    <asp:Button ID="BtnSearch" runat="server" Text="��ѯ" CssClass="SmallButton3"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                    �ɱ�����<asp:TextBox ID="txtCostCenter" Width="100px" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;��׼����<asp:TextBox ID="txtPrice" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="����" CssClass="SmallButton3"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" OnUpdateCommand="Edit_update" OnCancelCommand="Edit_cancel"
            Width="600px" AllowPaging="True" PageSize="25" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize" >
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="wo_id" ReadOnly="True" HeaderText="ID">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="costcenter" HeaderText="<b>�ɱ�����</b>" ReadOnly="true">
                    <HeaderStyle HorizontalAlign="Center" Width="130px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="130px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_desc" ReadOnly="True" HeaderText="<b>����</b>" ItemStyle-Width="200px">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="wo_price" HeaderText="<b>��׼����</b>">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="<u>����</u>" CancelText="<u>ȡ��</u>"
                    EditText="<u>�༭</u>">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="90px"></HeaderStyle>
                    <ItemStyle Wrap="False" Width="90px" HorizontalAlign="Center"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>ɾ��</u>" CommandName="DeleteBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="60px"></HeaderStyle>
                    <ItemStyle Width="60px" Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
            </Columns>
            <PagerStyle Font-Size="11pt" HorizontalAlign="Center" BackColor="White" Mode="NumericPages">
            </PagerStyle>
        </asp:DataGrid>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
