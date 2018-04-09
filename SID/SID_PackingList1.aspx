<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_PackingList1.aspx.cs" Inherits="SID_PackingList1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="1060px" border="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblSysPKNo" runat="server" Width="100px" CssClass="LabelRight" Text="ϵͳ���˵���(PK):"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSysPKNo" runat="server" Width="80px" TabIndex="1" CssClass="smalltextbox"></asp:TextBox>
                </td>
<%--                <td align="right">
                    <asp:Label ID="lblSysPKRef" runat="server" Width="55px" CssClass="LabelRight" Text="�ο�(Ref):"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtSysPKRef" runat="server" Width="80px" TabIndex="2" CssClass="smalltextbox"></asp:TextBox>
                </td>--%>
                <td width="80px">
                    ��������:
                </td>
                <td align="right" width="200px">
                    <asp:TextBox ID="txtShipDate1" runat="server" CssClass="smalltextbox Date" TabIndex="3"
                        Width="80px"></asp:TextBox>-<asp:TextBox ID="txtShipDate2" runat="server" CssClass="smalltextbox Date"
                            TabIndex="3" Width="80px"></asp:TextBox>
                </td>
                <td Width="55px">
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" Text="���˵���:"></asp:Label>
                </td>
                <td Width="80px">
                    <asp:TextBox ID="txtShipNo" runat="server" Width="80px" TabIndex="3"></asp:TextBox>
                </td>
                <td Width="55px">
                    <asp:Label ID="lblSysPKRef" runat="server" Width="55px" CssClass="LabelRight" Text="��֤�۸�:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:DropDownList ID="ddl_pricestatus" runat="server" OnSelectedIndexChanged="ddl_pricestatus_SelectedIndexChanged" AutoPostBack = "true">
                        <asp:ListItem Value="0" Selected="True">δȷ��</asp:ListItem>
                        <asp:ListItem Value="1">��ȷ��</asp:ListItem>
                        <asp:ListItem Value="2">δȷ��-ATL</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Label ID="lblDomain" runat="server" Width="20px" CssClass="LabelRight" Text="��:"
                        Font-Bold="false"></asp:Label>
                </td>
                <td align="center">
                    <asp:TextBox ID="txtDomain" runat="server" Width="60px" TabIndex="3" CssClass="smalltextbox"></asp:TextBox>
                </td>
                <td align="Left">
                    &nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="4"
                        Text="��ѯ" Width="50px" OnClick="btnQuery_Click" />
                </td>
                <td align="Left">
                    &nbsp;<asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton2" TabIndex="5"
                        Text="��ӡ" Width="50px" OnClick="btnAddNew_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:CheckBox ID="chkAll" runat="server" Text="ȫѡ" Width="80px" AutoPostBack="True"
                        OnCheckedChanged="chkAll_CheckedChanged" />
                </td>
                <td>
                    <asp:Literal ID="lt_CombineNbr" runat="server" Text="�ϲ����˵���"></asp:Literal>
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txt_CombineNbr" runat="server" Width="500px" ReadOnly="true"></asp:TextBox>
                </td>
                <td colspan="7">
                    <asp:Button ID="btn_Confirm" runat="server" Text="ȷ��" 
                        onclick="btn_Confirm_Click" CssClass="SmallButton2"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Submit" runat="server" Text="�ϲ�"  CssClass="SmallButton2" 
                        onclick="btn_Submit_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_Cancel" runat="server" Text="ȡ��" 
                        onclick="btn_Cancel_Click" CssClass="SmallButton2"/>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvPacking" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle GridViewRebuild" PageSize="20" OnPreRender="gvSID_PreRender" OnRowCommand ="gvPacking_RowCommand"
            DataKeyNames="SID,Nbr" OnPageIndexChanging="gvPacking_PageIndexChanging" Width="1020px"
            OnRowDataBound="gvPacking_RowDataBound">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="1060px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="��" Width="20px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="ϵͳ���˵���" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ο�" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���˵���" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��֤��Ʊ" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="װ��ص�" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��������" Width="140px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��Ʊ����" Width="80px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���䷽ʽ" Width="70px" pxHorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��" Width="30px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="���˵��ϲ�" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="��֤" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="�ܾ�" Width="60px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Select" runat="server" Width="20px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PK" HeaderText="ϵͳ���˵���">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PKRef" HeaderText="�ο�">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Nbr" HeaderText="���˵���">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="shipno" HeaderText="��֤��Ʊ">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Site" HeaderText="װ��ص�">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ShipDate" HeaderText="��������" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OutDate" HeaderText="��������">
                    <HeaderStyle Width="160px" HorizontalAlign="Center" />
                    <ItemStyle Width="160px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PackingDate" HeaderText="��Ʊ����">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Via" HeaderText="���䷽ʽ">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField CommandName="Detail1" Text="<u>����</u>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                </asp:ButtonField>
                <asp:BoundField DataField="Shipto" HeaderText="����">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Domain" HeaderText="��">
                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="���˵��ϲ�">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkNbr" runat="server" CommandName="detail" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text='<%# Bind("NbrCombine") %>' CommandArgument='<%# Bind("Nbr") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="160px" />
                    <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:TemplateField>
<%--                <asp:BoundField DataField="NbrCombine" HeaderText="���˵��ϲ�">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server"
                            Enabled='<%# Eval("SID_org1_con") %>' Text='<%# Eval("SID_org1_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm1" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org2_con") %>' Text='<%# Eval("SID_org2_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm2" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�ܾ�">
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CausesValidation="False" CssClass="smallbutton2"
                            Enabled='<%# Eval("SID_org3_con") %>' Text='<%# Eval("SID_org3_uid") %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm3" />
                    </ItemTemplate>
                    <ControlStyle CssClass="smallbutton2" Font-Size="12px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
