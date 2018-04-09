<%@ Page Language="VB" AutoEventWireup="false" CodeFile="wo_workproEnter.aspx.vb"
    Inherits="wo_cost_wo_workproEnter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="2" cellpadding="2" width="960" bgcolor="white" border="0" id="tbTop"
            runat="server">
            <tr>
                <td align="left">
                    ��������&nbsp;<asp:TextBox ID="txtWpro" runat="server" Width="180px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; ��������&nbsp;<asp:TextBox ID="txtProName" runat="server" Width="200px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; ָ��&nbsp;<asp:TextBox ID="txtGline" runat="server" Width="100px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="��ѯ" CausesValidation="false">
                    </asp:Button>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Visible="true"></asp:Label>
                </td>
                <td align="right">
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="����"></asp:Button>
                    <asp:Button ID="btnExport" runat="server" CssClass="SmallButton3" Text="����" Visible="true">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="960px" AutoGenerateColumns="False"
            PageSize="22" AllowPaging="True" CssClass="GridViewStyle AutoPageSize">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <ItemStyle CssClass="GridViewRowStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundColumn DataField="woID" Visible="false" ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="woTec" HeaderText="��������" ReadOnly="true">
                    <HeaderStyle Width="100px"></HeaderStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="woProc" HeaderText="��������" ReadOnly="true">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="woGl" HeaderText="ָ��">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="woRdesc" HeaderText="��������" ReadOnly="true">
                    <HeaderStyle Width="200px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="woPdesc" HeaderText="�������" ReadOnly="true">
                    <HeaderStyle Width="280px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="woCreat" HeaderText="������" ReadOnly="true">
                    <HeaderStyle Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
                <asp:EditCommandColumn CancelText="<u>ȡ��</u>" EditText="<u>�༭</u>" UpdateText="<u>����</u>">
                    <ItemStyle HorizontalAlign="center" Width="60px"></ItemStyle>
                </asp:EditCommandColumn>
                <asp:ButtonColumn Text="<u>ɾ��</u>" CommandName="DeleteBtn">
                    <HeaderStyle Width="40px"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="userID" Visible="false" ReadOnly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="woDate" HeaderText="����" ReadOnly="true">
                    <HeaderStyle Width="60px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        <%-- Start Input--%>
        <table width="499px" align="center" visible="false" id="tbInput" runat="server" border="1">
            <tr>
                <td style="width: 80px" align="right">
                    ��������
                </td>
                <td>
                    <asp:TextBox ID="txtTec" runat="server" Width="140px" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 80px" align="right">
                    ��������
                </td>
                <td>
                    <asp:TextBox ID="txtWpr" runat="server" Width="300px" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 80px" align="right">
                    ָ��
                </td>
                <td>
                    <asp:TextBox ID="txtWGl" runat="server" Width="140px" TabIndex="3"></asp:TextBox>
                    <asp:Label ID="lbldesc" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblRdesc" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="����" Width="120px" CssClass="SmallButton2" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnback" runat="server" Text="����" Width="120px" CssClass="SmallButton2"
                        CausesValidation="false" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
