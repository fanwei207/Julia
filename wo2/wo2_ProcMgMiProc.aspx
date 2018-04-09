<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wo2_ProcMgMiProc.aspx.cs" Inherits="wo2_MgMiProc" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 602px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="margin-top: 20px;
            width: 800px;">
            <tr>
                <td class="style1">
                    ���:<asp:DropDownList ID="dropType" runat="server">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>LED</asp:ListItem>
                        <asp:ListItem>��װ</asp:ListItem>
                        <asp:ListItem>��·��</asp:ListItem>
                        <asp:ListItem>ë��</asp:ListItem>
                        <asp:ListItem>����</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;��˾:<asp:DropDownList ID="dropDomain" runat="server">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem>SZX</asp:ListItem>
                        <asp:ListItem>ZQL</asp:ListItem>
                        <asp:ListItem>YQL</asp:ListItem>
                        <asp:ListItem>HQL</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;����:<asp:TextBox ID="txtProc" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="87px"></asp:TextBox>
                    &nbsp;����:<asp:TextBox ID="txtProcCode" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="79px"></asp:TextBox>
                    ԭ����:<asp:TextBox ID="txtOrigProc" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="87px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="��ѯ" />
                &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3" OnClick="btnAdd_Click"
                        TabIndex="0" Text="����" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="p_id"
            CssClass="GridViewStyle" Width="800px" 
            OnRowDataBound="gvSort_RowDataBound" OnRowDeleting="gvSort_RowDeleting" 
            onrowediting="gv_RowEditing" onrowupdating="gv_RowUpdating" 
            onrowcancelingedit="gv_RowCancelingEdit">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="p_type" HeaderText="���" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="p_domain" HeaderText="��˾" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="p_procCode" HeaderText="�������">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="p_proc" HeaderText="����">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="p_orig_proc" HeaderText="ԭ����">
                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="p_position" HeaderText="λ��" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:CommandField ShowEditButton="True" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdelete" runat="server" CommandArgument='<%# Eval("p_id") %>'
                            CommandName="Delete" Font-Size="12px" Font-Underline="true" ForeColor="Blue"
                            Text="<u>ɾ��</u>"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
