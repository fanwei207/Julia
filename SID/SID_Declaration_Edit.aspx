<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_Declaration_Edit.aspx.cs"
    Inherits="SID_Declaration_Edit" %>

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
        <div style="width: 884px;" class="table04_container">
            <tr align="left" runat="server">
                <td align="left" runat="server">
                    <asp:Button ID="btn_back" runat="server" Text="����" 
                onclick="btn_back_Click" />
                </td>
            </tr>
            <asp:GridView ID="gvSID" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                Width="980px" CssClass="GridViewStyle" PageSize="20" OnPreRender="gvSID_PreRender"
                OnPageIndexChanging="gvSID_PageIndexChanging" ShowFooter="true" OnRowCancelingEdit="gvSID_RowCancelingEdit"
                OnRowEditing="gvSID_RowEditing" OnRowUpdating="gvSID_RowUpdating" OnRowDataBound="gvSID_RowDataBound"
                DataKeyNames="SNO,sid_did">
                <RowStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="880px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="�к�" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="���˵���" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="ϵ��" Width="40px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="ϵ��˵��" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="������" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="��ֻ��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="������" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="�ܼ���" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="��ë��" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="�ܾ���" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="�����" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="�ܼ�ֵ" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="�ܼ�ֵ" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="����" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="ƽ����" Width="50px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="�༭" Width="80px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="���" Width="40px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="sid_did" HeaderText="�к�" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SNBR" HeaderText="���˵���" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SNO" HeaderText="ϵ��" ReadOnly="true">
                        <HeaderStyle Width="40px" HorizontalAlign="Center" />
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SCode" HeaderText="ϵ��˵��" ReadOnly="true">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QtySet" HeaderText="������" DataFormatString="{0:#0}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QtyPcs" HeaderText="��ֻ��" DataFormatString="{0:#0}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QtyBox" HeaderText="������" DataFormatString="{0:#0}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QtyPkgs" HeaderText="�ܼ���" DataFormatString="{0:#0}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
<%--                    <asp:TemplateField HeaderText="ë��">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWeight" runat="server" CssClass="SmallTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Weight","{0:#0.00}") %>'
                                Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container, "DataItem.Weight", "{0:#0.00}")%>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="Weight" HeaderText="ë��" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Net" HeaderText="����" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
   <%--                 <asp:BoundField DataField="TotalNet" HeaderText="�ܾ���" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>--%>
                    <%--<asp:TemplateField HeaderText="����">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNet" runat="server" CssClass="SmallTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.Net","{0:#0.00}") %>'
                                Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container, "DataItem.Net","{0:#0.00}") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="Volume" HeaderText="���" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Old_Price" HeaderText="ԭ��ֵ" DataFormatString="{0:#0.00000}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
      <%--              <asp:BoundField DataField="New_Price" HeaderText="������ֵ" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="������ֵ">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="SmallTextBox" Text='<%# DataBinder.Eval(Container, "DataItem.New_Price", "{0:#0.00000}") %>'
                                Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container, "DataItem.New_Price", "{0:#0.00000}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FixAmount" HeaderText="������ֵ" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Diff" HeaderText="����" DataFormatString="{0:#0.00}" HtmlEncode="false"
                        ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AvgPrice" HeaderText="ƽ����" DataFormatString="{0:#0.00}"
                        HtmlEncode="false" ReadOnly="true">
                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                        <ItemStyle Width="50px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:CommandField ShowEditButton="True" CancelText="<u>ȡ��</u>" DeleteText="<u>ɾ��</u>"
                        EditText="<u>�༭</u>" UpdateText="<u>����</u>" ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </div>
        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
