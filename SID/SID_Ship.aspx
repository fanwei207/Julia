<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_Ship.aspx.cs" Inherits="SID_SID_Ship" %>

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
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="1180px">
            <tr>
                <td>
                    ϵͳ����<asp:TextBox ID="txtPK" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td class="style1">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��<asp:TextBox ID="txtPKref" runat="server"
                        CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ���˵���<asp:TextBox ID="txtnbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ���䷽ʽ<asp:TextBox ID="txtVia" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��װ����<asp:TextBox ID="txtCtype" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ���ڹ�˾<asp:TextBox ID="txtdomain" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    װ��ص�<asp:TextBox ID="txtsite" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td>
                    ��������<asp:TextBox ID="txtShipDate" runat="server" CssClass="SmallTextBox Date" Width="100px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td>
                    ��������<asp:TextBox ID="txtOutDate" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                </td>
                <td colspan="2">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��<asp:TextBox ID="txtshipto" runat="server"
                        CssClass="SmallTextBox" Width="280px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSID" runat="server" Visible="false"></asp:Label>
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton3" Text="����" Width="40"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" Text="����" Width="40"
                        OnClick="btnAdd_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;<asp:CheckBox ID="chkMianJian"
                        runat="server" Enabled="false" OnCheckedChanged="chkMianJian_CheckedChanged"
                        AutoPostBack="True" />
                </td>
                <td class="style1">
                    �������<asp:TextBox ID="txtInspectDate" runat="server" CssClass="SmallTextBox Date"
                        Enabled="False" onkeydown="event.returnValue=false;" onpaste="return false;"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    ����ص�<asp:TextBox ID="txtInspectSite" runat="server" CssClass="SmallTextBox" Enabled="False"
                        Width="100px"></asp:TextBox>
                </td>
                <td>
                    Ԥ������<asp:TextBox ID="txt_InspMatchDate" runat="server" CssClass="SmallTextBox Date"
                        Enabled="False" onkeydown="event.returnValue=false;" onpaste="return false;" Width="100px"></asp:TextBox>
                </td>
                <td>
                        ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��&nbsp;<asp:CheckBox ID="chkIsCabin"
                        runat="server" Enabled="false"
                        AutoPostBack="True" />

                    <asp:TextBox ID="txtOldShipDate" runat="server" CssClass="SmallTextBox" onkeydown="event.returnValue=false;"
                        onpaste="return false;" Visible="False" Width="100px"></asp:TextBox>
                </td>

                <td>
                    <asp:Button ID="btnSaveInsp" runat="server" CssClass="SmallButton3" Text="����" Width="40px"
                        OnClick="btnSaveInsp_Click" Visible="False" Height="26px" />
                    <asp:Button ID="btnClearInsp" runat="server" CssClass="SmallButton3" Text="���" Width="40"
                        OnClick="btnClearInsp_Click" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 20px">
                    <asp:RadioButton ID="rad1" runat="server" Text="δ����" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup" OnCheckedChanged="rad1_CheckedChanged"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad2" runat="server" Text="�ѱ���" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" OnCheckedChanged="rad2_CheckedChanged"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="rad3" runat="server" Text="���޸�" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" OnCheckedChanged="rad3_CheckedChanged"></asp:RadioButton>
                </td>
                <td style="height: 20px">
                    ��������<asp:TextBox ID="txtcreated" runat="server" CssClass="SmallTextBox Date" Width="100px"
                        onkeydown="event.returnValue=false;" onpaste="return false;"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 20px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��<asp:TextBox
                        ID="txtcreated1" runat="server" CssClass="SmallTextBox" Width="100px" onkeydown="event.returnValue=false;"
                        onpaste="return false;"></asp:TextBox>&nbsp;
                    <asp:CheckBox ID="chkInspectDate" runat="server" Text="�Ƿ�ӵ�������������Ȩ��" Visible="False" />
                </td>
                <td style="height: 20px">
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="��ѯ" Width="40"
                        OnClick="btnSearch_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_exportexcel" runat="server" CssClass="SmallButton3" 
                        Text="����" Width="40px"
                        OnClick="btn_exportexcel_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_exportexcelByTemp" runat="server" CssClass="SmallButton3" 
                        Text="�������˵�" Width="70px"
                        OnClick="btn_exportexcelByTemp_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShip" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="ID" OnRowCommand="gvShip_RowCommand" OnPageIndexChanging="gvShip_PageIndexChanging"
            OnRowDataBound="gvShip_RowDataBound" Width="1310px" CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="PK" HeaderText="ϵͳ����">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="PKref" HeaderText="�ο�">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="nbr" HeaderText="���˵���">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="via" HeaderText="���䷽ʽ">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ctype" HeaderText="��װ����">
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="site" HeaderText="װ��ص�">
                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipdate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="outdate" HeaderText="��������">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_date" HeaderText="�������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_site" HeaderText="����ص�">
                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="insp_matchdate" HeaderText="Ԥ������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="mj" HeaderText="���">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                 <asp:BoundField DataField="IsCabin" HeaderText="���">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit1"
                            Enabled='<%# Eval("SID_org_con") %>' Text="<u>�༭</u>" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:ButtonField CommandName="Detail1" Text="<u>��ϸ</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Del1"
                            Enabled='<%# Eval("SID_org_con") %>' Text="<u>ɾ��</u>" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:BoundField DataField="domain" HeaderText="��">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="״̬">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>

                <asp:TemplateField ShowHeader="False" HeaderText="֪ͨ">
                    <ItemTemplate>
                        <asp:CheckBoxList ID="cklist_domain" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value = "1">SZX</asp:ListItem>
                            <asp:ListItem Value = "2">ZQL</asp:ListItem>
                            <asp:ListItem Value = "5">YQL</asp:ListItem>
                            <asp:ListItem Value = "8">HQL</asp:ListItem>
                        </asp:CheckBoxList>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle ForeColor="Black" HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ȷ��">
                    <ItemTemplate>
                        <asp:Button ID="btn_check" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_isCK") %>' Text='<%# Eval("SID_isCK_createdBy") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="ConfirmC" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�޸�">
                    <ItemTemplate>
                        <asp:Button ID="btn_update" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_isUP") %>' Text='<%# Eval("SID_isUP_createdBy") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="ConfirmU" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:BoundField DataField="finisheddate" HeaderText="�깤����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="checkeddate" HeaderText="�ִ�����">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipto" HeaderText="����">
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="��֤">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org1_con") %>' Text='<%# Eval("SID_org1_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm1" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org2_con") %>' Text='<%# Eval("SID_org2_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm2" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
<%--               <asp:BoundField DataField="IsQC" HeaderText="�ʼ�">
                    <ItemStyle HorizontalAlign="Left" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_IsQC") %>' Text='<%# Eval("IsQCCheck") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm3" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                    <ItemStyle Width="30px" />
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
