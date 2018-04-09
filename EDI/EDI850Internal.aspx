<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI850Internal.aspx.cs" Inherits="EDI_EDI850Internal" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 924px;
            margin-top: 4px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: ΢���ź�;">
                <td style="width: 3px; background-image: url(../images/bg_tb1.jpg); background-repeat: no-repeat;">
                </td>
                <td>
                    <asp:RadioButton ID="rbNormal" runat="server" GroupName="gpType" Checked="true" Text="δ����QAD�Ķ���"
                        AutoPostBack="True" OnCheckedChanged="rbNormal_CheckedChanged" />&nbsp;
                    <asp:RadioButton ID="rbFinish" runat="server" GroupName="gpType" Text="�ѵ���QAD�еĶ���"
                        AutoPostBack="True" OnCheckedChanged="rbFinish_CheckedChanged" />
                    &nbsp;&nbsp;
                </td>
                <td>
                    ��ϵ��:<asp:TextBox ID="txtContact" runat="server" Width="98px" CssClass="SmallTextBox2"></asp:TextBox>����:<asp:TextBox
                        ID="txtDate" runat="server" Width="90px" CssClass="SmallTextBox2 Date"></asp:TextBox>(��������ʱѡ������)
                </td>
                <td align="center" style="width: 232px">
                    <asp:Button ID="btnRefresh" runat="server" Text="ˢ��" Width="30" CssClass="SmallButton3"
                        OnClick="btnRefresh_Click" />
                    <asp:Button ID="btnImport" runat="server" Text="����QAD" OnClick="btnImport_Click"
                        OnClientClick="return confirm('ȷ������QAD����?');" CssClass="SmallButton3" Width="50" />
                    <asp:Button ID="btnExportExcel" runat="server" Text="����Excel����" OnClick="btnExportExcel_Click"
                        CssClass="SmallButton3" Width="80" />
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 4px;">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" PageSize="20" OnRowDataBound="gvlist_RowDataBound"
                        OnPageIndexChanging="gvlist_PageIndexChanging" DataKeyNames="HRD_consignment"
                        CssClass="GridViewStyle AutoPageSize" OnRowCommand="gvlist_RowCommand">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <input id="chkImport" type="checkbox" name="chkImport" runat="server" value='<%#Eval("HRD_Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblPoId" runat="server" Text='<%# Bind("HRD_Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="�ɹ�������">
                                <HeaderStyle Width="85px" />
                                <ItemStyle Width="85px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("HRD_PoNbr")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="�ͻ�����">
                                <HeaderStyle Width="85px" />
                                <ItemStyle Width="85px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("HRD_ShipTo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��ֹ����">
                                <HeaderStyle Width="70px" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("HRD_DueDate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("HRD_Domain")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="�ص�">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("HRD_Site")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��ϵ��">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("HRD_PoContact")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="��ע">
                                <HeaderStyle Width="130px" />
                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("HRD_Rmks")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="������Ϣ" DataField="HRD_Error">
                                <HeaderStyle Width="240px" />
                                <ItemStyle Width="240px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="����">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CommandName="myUpdate" CssClass="smallbutton2"
                                        Text="����" Width="39px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="true" Value="" />
                    <asp:Label ID="lbltest" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
