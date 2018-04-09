<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_workOrderEnter4.aspx.cs"
    Inherits="wo2_wo2_workOrderEnter4" %>

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
        <div style="width: 980px; padding-bottom: 7px; padding-top: 2px; border: 1px solid black;
            text-align: left; height: 40px;">
            <table id="tbSearch" cellspacing="0" cellpadding="0" width="980">
                <tr>
                    <td colspan="2">
                        &nbsp;�ص�<asp:DropDownList ID="dropSite" runat="server" Width="70px">
                        </asp:DropDownList>
                        &nbsp; &nbsp;&nbsp;&nbsp; �ӹ�����<asp:TextBox ID="txtOrder" runat="server" Width="70px"
                            TabIndex="3"></asp:TextBox>&nbsp; �ӹ���ID
                        <asp:TextBox ID="txtOrderID" runat="server" Width="70px" TabIndex="4"></asp:TextBox>&nbsp;
                        &nbsp;��ˮ��
                        <asp:TextBox ID="txtLine" runat="server" Width="70px" TabIndex="5"></asp:TextBox>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; �ɱ�����:
                        <asp:Label ID="lbl_cc" runat="server" Width="40"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ���մ���: &nbsp;
                        <asp:Label ID="lblTec" runat="server" Width="180"> </asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;�����: &nbsp;
                        <asp:Label ID="lbl_part" runat="server" Width="100"></asp:Label>&nbsp; &nbsp;����:
                        &nbsp;
                        <asp:Label ID="lbl_desc" runat="server" Width="300"></asp:Label>&nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; �ƻ���:
                        <asp:Label ID="lblPlan" runat="server" Width="100"></asp:Label>
                        <asp:Label ID="lblWorkOrderID" runat="server" Visible="false"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Button ID="btn_woload" runat="server" CssClass="SmallButton3" Width="40px" Text="��ѯ"
                            TabIndex="6" OnClick="btn_woload_Click" ValidationGroup="Search"></asp:Button>&nbsp;
                        &nbsp;<asp:Button ID="btn_clear" runat="server" CssClass="SmallButton3" Width="40px"
                            Text="���" OnClick="btn_clear_Click" CausesValidation="false"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <table id="tbInput" cellspacing="0" cellpadding="0" width="980">
            <tr>
                <td style="height: 24px">
                    &nbsp;&nbsp;��Ч����<asp:TextBox ID="txtEffdate" CssClass="Date" runat="server" Width="80px"
                        MaxLength="10"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;����<asp:DropDownList ID="dropWorkproc" runat="server" TabIndex="7"
                        Width="140px" OnSelectedIndexChanged="dropWorkproc_SelectedIndexChanged" DataTextField="MOPValue"
                        DataValueField="MOPID" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;�û���<asp:DropDownList ID="dropWorkGroup" runat="server" TabIndex="8" Width="200px"
                        DataTextField="GroupValue" DataValueField="GroupID">
                    </asp:DropDownList>
                </td>
                <td align="right" style="height: 24px" rowspan="3">
                    <asp:Button ID="btnAdd" runat="server" Text="����" Width="40px" TabIndex="12" OnClientClick="return Addbtn_Click();"
                        CssClass="SmallButton3" OnClick="btnAdd_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="����" Width="40px" CssClass="SmallButton3"
                        OnClick="btnSave_Click" CausesValidation="false" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 2px">
                </td>
            </tr>
            <tr>
                <td style="height: 24px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����
                    <asp:TextBox ID="txtUserNo" runat="server" Width="80px" TabIndex="9"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;��λ<asp:DropDownList ID="dropPostion" runat="server" Width="160px"
                        TabIndex="10" DataTextField="SOPValue" DataValueField="SOPID">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;<asp:TextBox ID="txtSpecial" runat="server" Width="50px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�깤��<asp:TextBox ID="txtComp" runat="server"
                        Width="120px" TabIndex="10"></asp:TextBox>&nbsp;&nbsp;�깤������뿴ҳβ���ֲ���
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnldv" Style="overflow: auto" runat="server" Width="980px" HorizontalAlign="Left"
            BorderWidth="1px" BorderColor="Black" Height="420px">
            <asp:GridView ID="gvWorkOrder" AllowSorting="True" AutoGenerateColumns="False" CssClass="GridViewStyle"
                runat="server" PageSize="15" Width="970px" DataKeyNames="wo2_id" OnRowDeleting="gvWorkOrder_RowDeleting"
                OnRowCommand="gvWorkOrder_RowCommand" OnRowDataBound="gvWorkOrder_RowDataBound">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundField HeaderText="���" DataField="wo2_tid">
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="����" DataField="wo2_userNo">
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo2_userName" HeaderText="����">
                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo2_procName" HeaderText="����">
                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo2_postName" HeaderText="��λ">
                        <ItemStyle HorizontalAlign="Center" Width="130px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="�������">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDistribution" runat="server" Width="80px" Text='<%# Bind("wo2_Proportion") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="wo2_line_comp" HeaderText="�깤��" DataFormatString="{0:N2}"
                        HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo2_effdate" HeaderText="��Ч����" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="wo2_creatName" HeaderText="������">
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDVSave" runat="server" Text="<u>�޸�</u>" CommandName="myEdit" CausesValidation="false"
                                ForeColor="Black" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDVDelete" runat="server" Text="<u>ɾ��</u>" CommandName="Delete" CausesValidation="false"
                                ForeColor="Black" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <asp:Label ID="lblMes" runat="server" Text="* //--�깤��--//����˼ӹ����õ�����Ϊ���������Ҳ����֣����������ʵ�ʲ�������ȻΪ�üƻ��������깤��*"
            ForeColor="red"></asp:Label>
        </form>
    </div>
    <script type="text/javascript">
		    <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
