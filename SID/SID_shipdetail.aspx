<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_shipdetail.aspx.cs" Inherits="SID_SID_shipdetail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        <table style="width: 1000px;" cellspacing="0" cellpadding="4" class="table05">
            <tr>
                <td>
                    ϵͳ����:<asp:Label ID="lblPK" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��:<asp:Label ID="lblPKref" runat="server"
                        Width="100px"></asp:Label>
                </td>
                <td>
                    ���˵���:<asp:Label ID="lblnbr" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    ���䷽ʽ:<asp:Label ID="lblVia" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    װ��ص�:<asp:Label ID="lblsite" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    ��������:<asp:Label ID="lblShipDate" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    ��������:<asp:Label ID="lblOutDate" runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    ��װ����:<asp:Label ID="lblCtype" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    ��&nbsp;&nbsp;��&nbsp;&nbsp;��:<asp:Label ID="lblBox" runat="server"></asp:Label>
                </td>
                <td>
                    ��&nbsp;&nbsp;��&nbsp;&nbsp;��:<asp:Label ID="lblVolume" runat="server"></asp:Label>
                </td>
                <td>
                    ��&nbsp;&nbsp;��&nbsp;&nbsp;��:<asp:Label ID="lblWeight" runat="server"></asp:Label>
                </td>
                <td>
                    ��&nbsp;&nbsp;��&nbsp;&nbsp;��:<asp:Label ID="lblPrice" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    ��&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;��:<asp:Label ID="lblshipto" runat="server"
                        Width="280px"></asp:Label>
                </td>
                <td>
                    ��&nbsp;&nbsp;��&nbsp;&nbsp;��:<asp:Label ID="lblPkgs" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btnAdd" runat="server" Text="����" Width="60px" CssClass="SmallButton3"
                        OnClick="btnAdd_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="����" Width="60px" CssClass="SmallButton3"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvShipdetail" runat="server" AutoGenerateColumns="False" DataKeyNames="DID,SID_org_con"
            Width="1760px" OnRowCancelingEdit="gvShipdetail_RowCancelingEdit" OnRowDeleting="gvShipdetail_RowDeleting"
            OnRowEditing="gvShipdetail_RowEditing" OnRowUpdating="gvShipdetail_RowUpdating"
            OnRowCommand="gvShipdetail_RowCommand" OnRowDataBound="gvShipdetail_RowDataBound"
            CssClass="GridViewStyle">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="no" HeaderText="���" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="ϵ��">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSNO" runat="server" CssClass="SmallTextBox" Text='<%# Bind("SNO") %>'
                            Width="40px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <%#Eval("SNO")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="QAD" HeaderText="���ϱ���" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="��������">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtQtyset" runat="server" CssClass="SmallTextBox" Text='<%# Bind("qty_set") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("qty_set")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtbox" runat="server" CssClass="SmallTextBox" Text='<%# Bind("qty_box") %>'
                            Width="50px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <%#Eval("qty_box")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�̼��">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtqa" runat="server" CssClass="SmallTextBox" Text='<%# Bind("qa") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("qa")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="so_nbr" HeaderText="���۶���" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="so_line" HeaderText="�к�" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="�����">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtwo" runat="server" CssClass="SmallTextBox" Text='<%# Bind("wo") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("wo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TCP����">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtpo" runat="server" CssClass="SmallTextBox" Text='<%# Bind("po") %>'
                            Width="100px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemTemplate>
                        <%#Eval("po")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cust_part" HeaderText="�ͻ�����" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                    <HeaderStyle Width="150px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="����">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtweight" runat="server" CssClass="SmallTextBox" Text='<%# Bind("weight") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("weight")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="���">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtvolume" runat="server" CssClass="SmallTextBox" Text='<%# Bind("volume") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("volume")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�۸�">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtprice" runat="server" CssClass="SmallTextBox" Text='<%# Bind("price") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("price")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�۸�">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtprice1" runat="server" CssClass="SmallTextBox" Text='<%# Bind("price1") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Right" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("price1")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�۸�����">
                    <EditItemTemplate>
                        <asp:Label ID="lblptype" Text='<%# Bind("ptype") %>' runat="server" Visible="false"></asp:Label>
                        <asp:DropDownList ID="drpPtype" runat="server">
                            <asp:ListItem>--</asp:ListItem>
                            <asp:ListItem>SETS</asp:ListItem>
                            <asp:ListItem>PCS</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("ptype")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="<u>ȡ��</u>" DeleteText="<u>ɾ��</u>"
                    EditText="<u>�༭</u>" UpdateText="<u>����</u>">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>ɾ��</u>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="����">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtpkgs" runat="server" CssClass="SmallTextBox" Text='<%# Bind("pkgs") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("pkgs")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ֻ��">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtqtypcs" runat="server" CssClass="SmallTextBox" Text='<%# Bind("qty_pcs") %>'
                            Width="60px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <%#Eval("qty_pcs")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fedx" HeaderText="Fedex" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="fob" HeaderText="�ͻ�����" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:ButtonField CommandName="Adds" Text="<u>��ϸ</u>">
                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                    <ItemStyle Width="40px" HorizontalAlign="Center" ForeColor="Black" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                </asp:ButtonField>
                <asp:BoundField DataField="atl" HeaderText="ATL����" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="��ע">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtmemo" runat="server" CssClass="SmallTextBox" Text='<%# Bind("memo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    <ItemTemplate>
                        <%#Eval("memo")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
        <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
        </script>
    </div>
</body>
</html>
