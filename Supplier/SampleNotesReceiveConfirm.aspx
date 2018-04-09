<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SampleNotesReceiveConfirm.aspx.cs"
    Inherits="supplier_SampleReceiveNotesMaintain" %>

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
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellpadding="0" cellspacing="0" style="text-align: left;">
            <tr>
                <td>
                    &nbsp;������:
                </td>
                <td>
                    <asp:TextBox ID="txt_bosnbr" runat="server" Width="110px" ReadOnly="true" ForeColor="#808080"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;�������ڣ�
                </td>
                <td>
                    <asp:TextBox ID="txt_bosDate" runat="server" Width="144px" ReadOnly="true" ForeColor="#808080"></asp:TextBox>
                </td>
            </tr>
            <tr align="left">
                <td>
                    &nbsp;��Ӧ�̣�
                </td>
                <td>
                    <asp:TextBox ID="txt_vend" runat="server" Width="111px" ReadOnly="true" ForeColor="#808080"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;��Ӧ�����ƣ�
                </td>
                <td>
                    <asp:TextBox ID="txt_vendName" runat="server" Width="217px" ReadOnly="true" ForeColor="#808080"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;��&nbsp;ע��
                </td>
                <td colspan="4">
                    <asp:TextBox ID="txt_Bosrmks" runat="server" Width="598px" ReadOnly="true" ForeColor="#808080"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;�ջ�˵����
                </td>
                <td colspan="4" class="style1">
                    <asp:TextBox ID="txt_ReceiveNotes" runat="server" Width="598px"></asp:TextBox>
                </td>
            </tr>
            <tr> 
                <td class="style1">
                 
                </td>
                <td colspan="4" class="style1">
                  <asp:Label ID="lblState" runat="server" Text="����״̬����ȡ��" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="center">
                    <asp:CheckBox ID="chk_isVendConfirm" runat="server" Enabled="false" Text="��Ӧ��ȷ��"
                        Visible="False" />
                    &nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btn_Receive" runat="server" CssClass="SmallButton2" Text="ȫ���ջ�" OnClientClick="return confirm('��ȷ�����ж����о���ά���˲����ţ�\nȷ���ջ�����ʾ���Ͽ����յ����������г�����Ʒ���Ƿ������');"
                        OnClick="btn_Receive_Click" />
                    &nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Button ID="btn_Back" runat="server" CssClass="SmallButton2" Text="����" Width="65px"
                        OnClick="btn_Back_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table id="tb_bosdet" runat="server" style="text-align: center; width: 800px;">
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel1" runat="server" Width="800px" ScrollBars="Horizontal">
                        <asp:GridView ID="gv_det" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            CssClass="GridViewStyle" OnPageIndexChanging="gv_det_PageIndexChanging" DataKeyNames="bos_nbr,bos_det_line,bos_det_isReceipt"
                            OnRowDataBound="gv_det_RowDataBound" OnRowCommand="gv_det_RowCommand" PageSize="8"
                            Width="1200px" OnRowCancelingEdit="gv_det_RowCancelingEdit" OnRowEditing="gv_det_RowEditing"
                            OnRowUpdating="gv_det_RowUpdating" EnableModelValidation="True">
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <EmptyDataTemplate>
                                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" Width="1200px"
                                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="center" Text="�к�" Width="30px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="���Code" Width="150px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="���QAD" Width="90px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="����" Width="60px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="��������" Width="80px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="��ע"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="�����ĵ�" Width="50px"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                        <asp:TableCell HorizontalAlign="Center" Text="��������ϸ��¼" ColumnSpan="7"></asp:TableCell>
                                    </asp:TableFooterRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="bos_nbr" HeaderText="������" ReadOnly="true" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bos_det_line" HeaderText="�к�" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="������">
                                    <ItemTemplate>
                                        <%# Eval("bos_det_code")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_gvdetcode" Text='<%# Bind("bos_det_code") %>' runat="server"
                                            Width="180px" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="200px" HorizontalAlign="left" />
                                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="bos_det_qad" HeaderText="����QAD" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bos_det_qty" HeaderText="����" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    <ItemStyle HorizontalAlign="right" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bos_det_requireDate" HeaderText="��������" ReadOnly="true"
                                    DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="�ջ�">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_detRecieve" runat="server" CommandName="EditRecieve" Font-Underline="True"
                                            CommandArgument='<%# Container.DataItemIndex %>' ForeColor="Black">�ջ�</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="bos_det_receiptDate" HeaderText="�ջ�����" ReadOnly="true"
                                    DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="�����ĵ�">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_Doc" runat="server" CommandName="EditDoc" Font-Underline="True"
                                            CommandArgument='<%# Container.DataItemIndex %>' ForeColor="Black"> �鿴</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:CommandField EditText="�༭" ShowEditButton="True" CancelText="ȡ��" UpdateText="����">
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    <ControlStyle ForeColor="Black" Font-Underline="True" />
                                </asp:CommandField>
                                <asp:BoundField DataField="bos_det_rmks" HeaderText="��ע" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bos_det_techConfirm" HeaderText="��������" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bos_det_techConfirmDate" HeaderText="������������" ReadOnly="true"
                                    DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bos_det_qualityConfirm" HeaderText="�ʼ�����" ReadOnly="true">
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bos_det_qualityConfirmDate" HeaderText="�ʼ���������" ReadOnly="true"
                                    DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
