<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pcb_edit.aspx.cs" Inherits="pcb_edit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <table cellspacing="0" cellpadding="0" style="width: 1053px">
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="81px"></asp:TextBox><asp:TextBox
                        ID="txtModel" runat="server" CssClass="smalltextbox" Width="85px"></asp:TextBox><asp:TextBox
                            ID="txtVersion" runat="server" CssClass="smalltextbox" Width="83px"></asp:TextBox><asp:TextBox
                                ID="txtGreen" runat="server" CssClass="smalltextbox" Width="83px"></asp:TextBox><asp:TextBox
                                    ID="txtSteel" runat="server" CssClass="smalltextbox" Width="85px"></asp:TextBox><asp:TextBox
                                        ID="txtCopper" runat="server" CssClass="smalltextbox" Width="81px"></asp:TextBox><asp:TextBox
                                            ID="txtContents" runat="server" CssClass="smalltextbox" Width="121px"></asp:TextBox><asp:DropDownList
                                                ID="dropEqu" runat="server" Width="64px">
                                                <asp:ListItem>ȫ��</asp:ListItem>
                                                <asp:ListItem>��ȷ��</asp:ListItem>
                                                <asp:ListItem>YES</asp:ListItem>
                                                <asp:ListItem>NO</asp:ListItem>
                                            </asp:DropDownList>
                    <asp:DropDownList ID="dropWorkShop" runat="server" Width="62px">
                        <asp:ListItem>ȫ��</asp:ListItem>
                        <asp:ListItem>��ȷ��</asp:ListItem>
                        <asp:ListItem>YES</asp:ListItem>
                        <asp:ListItem>NO</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtRmks" runat="server" CssClass="smalltextbox" Width="177px"></asp:TextBox>
                    <asp:Button ID="btnAdd" runat="server" CssClass="smallbutton2" Text="�½�" Width="36px"
                        OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnSearch" runat="server" CssClass="smallbutton2" Text="��ѯ" Width="36px"
                        OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExport" runat="server" CssClass="smallbutton2" Text="����" Width="36px"
                        OnClick="btnExport_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            PageSize="21" OnRowDataBound="gv_RowDataBound" DataKeyNames="pvk_id" OnRowCommand="gv_RowCommand"
            OnRowDeleting="gv_RowDeleting" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging" Width="1053px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField DataField="pvk_mod_date" HeaderText="�޸�����" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_model" HeaderText="��·��">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_ver" HeaderText="�汾">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_green" HeaderText="������">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_steel" HeaderText="����">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_copper" HeaderText="ͭ��">
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_content" HeaderText="�İ�����">
                    <HeaderStyle Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_equ" HeaderText="�豸ȷ��">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_workshop" HeaderText="����ȷ��">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="pvk_remarks" HeaderText="��ע">
                    <HeaderStyle Width="200px" />
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                </asp:BoundField>
                <asp:ButtonField CommandName="myEdit" Text="<u>�޸�</u>">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:CommandField DeleteText="<u>ɾ��</u>" ShowDeleteButton="True">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <FooterStyle HorizontalAlign="Right" />
        </asp:GridView>
        <asp:Label ID="lbID" runat="server" Width="31px" Visible="False">0</asp:Label>
        </form>
    </div>
    <script>
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
