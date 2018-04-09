<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.addproduct" CodeFile="addproduct.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css"> 
        .style4
        {
            width: 75px;
        }
        .style5
        {
            width: 179px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" style="height: 10px;">
            <tr>
                <td style="vertical-align: top;">
                    <div style="width: 500px;">
                        <fieldset>
                            <legend>������Ϣ��</legend>
                            <table>
                                <tr>
                                    <td class="style2">
                                        �ͺ�:*
                                    </td>
                                    <td class="style3">
                                        <asp:TextBox ID="gcode" runat="server" CssClass="SmallTextBox" MaxLength="50" Width="150px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="cMsg1" runat="server" ControlToValidate="gcode" Display="none"
                                            ErrorMessage="��Ʒ�ͺŲ���Ϊ�գ�" Width="50px"></asp:RequiredFieldValidator>
                                    </td>
                                    <td class="style4">
                                        ����:*
                                    </td>
                                    <td class="style5">
                                        <asp:TextBox ID="txtCat" runat="server" CssClass="SmallTextBox" MaxLength="10" 
                                            Width="140px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="cMsg2" runat="server" Display="none" ErrorMessage="��Ʒ���಻��Ϊ�գ�"
                                            ControlToValidate="txtCat" Width="100px"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        QAD��:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="txtQad" runat="server" CssClass="SmallTextBox" MaxLength="14" 
                                            Width="150px"></asp:TextBox>
                                             <input id="hidQad" type="hidden" runat="server" />
                                    </td>
                                    <td class="style4">
                                        �����ͻ�����:
                                    </td>
                                    <td class="style5">
                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="SmallTextBox" MaxLength="30"
                                            Width="141px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        ���:
                                    </td>
                                    <td class="style3">
                                        <asp:TextBox ID="txtSimple" runat="server" Height="20px" Width="150px" Wrap="false"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td class="style4">
                                        ��С�����:
                                    </td>
                                    <td class="style5">
                                        <asp:TextBox ID="txtMinQty" runat="server" CssClass="SmallTextBox" MaxLength="30"
                                            Width="141px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        ������</td>
                                    <td class="style3" colspan="3">
                                        <asp:TextBox ID="txtmpi" runat="server" Height="20px" Width="150px" Wrap="false"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        ״̬:
                                    </td>
                                    <td colspan="3">
                                        ʹ��:&nbsp;
                                        <asp:RadioButton ID="radNormal" runat="server" Enabled="True" Checked="True" GroupName="radGroup">
                                        </asp:RadioButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����:&nbsp;
                                        <asp:RadioButton ID="radTry" runat="server" Enabled="True" Checked="False" GroupName="radGroup">
                                        </asp:RadioButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ͣ��:&nbsp;
                                        <asp:RadioButton ID="radStop" runat="server" Enabled="True" Checked="False" GroupName="radGroup">
                                        </asp:RadioButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        ����:
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="note" runat="server" TextMode="MultiLine" Height="100px" Width="400px"
                                            Wrap="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="style2" colspan="4">
                                        &nbsp;
                                        <asp:Button ID="BtnModify" runat="server" CssClass="SmallButton2" Text="�޸�" Visible="False"
                                            Width="50" />
                                        &nbsp;
                                        <asp:Button ID="BtnAddNew" runat="server" CssClass="SmallButton2" Text="���" Visible="False"
                                            Width="50" />
                                        &nbsp;
                                        <asp:Button ID="BtnDelete" runat="server" CssClass="SmallButton2" Text="ɾ��" Visible="True"
                                            Width="50" />
                                        &nbsp;
                                        <asp:Button ID="BtnReturn" runat="server" CausesValidation="False" CssClass="SmallButton2"
                                            Text="����" Visible="true" Width="50" />
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td style="width: 3px;">
                </td>
                <td style="vertical-align: top;">
                    <div>
                        <fieldset>
                            <legend> ��ѡ����Ҫ��������Ŀ��</legend>
                            <table>
                                <tr>
                                    <td align="left" style="width: 150px;">
                                        <asp:GridView ID="gv" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            CssClass="GridViewStyle" DataKeyNames="ptt_id,isExists,isExists150" Width="450px">
                                            <RowStyle CssClass="GridViewRowStyle" />
                                            <HeaderStyle CssClass="GridViewHeaderStyle" Font-Bold="True" ForeColor="Black" />
                                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                            <FooterStyle CssClass="GridViewFooterStyle" />
                                            <PagerStyle CssClass="GridViewPagerStyle" />
                                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                            <EmptyDataTemplate>
                                                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                                    GridLines="Vertical" Width="420px">
                                                    <asp:TableRow>
                                                        <asp:TableCell HorizontalAlign="center" Text="NO." Width="30px"></asp:TableCell>
                                                        <asp:TableCell HorizontalAlign="center" Text="��Ŀ����" Width="100px"></asp:TableCell>
                                                        <asp:TableCell HorizontalAlign="center" Text="��Ʒ����" Width="200px"></asp:TableCell>
                                                        <asp:TableCell HorizontalAlign="center" Text="�ĵ���" Width="60px"></asp:TableCell>
                                                        <asp:TableCell HorizontalAlign="center" Width="30px"><input id="Checkbox1" type="checkbox" /></asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableFooterRow BackColor="white" ForeColor="Black">
                                                        <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" Text="�޷�����������Ϣ"></asp:TableCell>
                                                    </asp:TableFooterRow>
                                                </asp:Table>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="NO.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex +1 %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ptt_type" HeaderText="��Ŀ����" ReadOnly="True">
                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ptt_detail" HeaderText="��Ʒ����" ReadOnly="True">
                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="�ĵ���">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" onkeypress="if ((event.keyCode&lt;48 || event.keyCode&gt;57) &amp;&amp; event.keyCode!=46) event.returnValue=false;"
                                                            Style="ime-mode: disabled; text-align: right" Text='<%# Bind("itm_amount") %>'
                                                            Width="100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="true" HorizontalAlign="Center" Width="60px" />
                                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="100">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSinger" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="150">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSinger150" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
