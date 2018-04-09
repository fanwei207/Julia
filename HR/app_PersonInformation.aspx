<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_PersonInformation.aspx.cs" Inherits="HR_app_PersonInformation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <form id="form1" runat="server">
    <div  ALIGN="center">
        <table>
            <tr>
                <td>工号：</td>
                <td><asp:TextBox ID="txtUserNo" runat="server" CssClass="smalltextbox" style="width:100px;"></asp:TextBox></td>
                <td>联系电话：</td>
                <td><asp:TextBox ID="txtPhone" runat="server" CssClass="smalltextbox" style="width:100px;"></asp:TextBox></td>
                <td><asp:Button ID="btnReach" runat="server" Text="添加"  CssClass="SmallButton2" onclick="btnReach_Click" /></td>                        
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            onrowcancelingedit="gv_RowCancelingEdit" onrowediting="gv_RowEditing" 
            onrowupdating="gv_RowUpdating" onrowdeleting="gv_RowDeleting"
            DataKeyNames="id" style="margin-top:20px; margin-right: 0px;">
        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
        <RowStyle CssClass="GridViewRowStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <HeaderStyle CssClass="GridViewHeaderStyle" />
        <EmptyDataTemplate>
            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                GridLines="Vertical">
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="center" Text="公司" Width="80px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="姓名" Width="80px"></asp:TableCell>
			        <asp:TableCell HorizontalAlign="center" Text="联系电话" Width="80px"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
            <asp:BoundField HeaderText="公司" DataField="plantcode"  ReadOnly="True">
                <HeaderStyle Width="60px" />
                <ItemStyle Width="60px" Height="25px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="姓名" DataField="username"  ReadOnly="True">
                <HeaderStyle Width="40px" />
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="联系电话" >
            <EditItemTemplate>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="SmallTextBox" Text='<%# Bind("phone") %>'
                Width="80px"></asp:TextBox>
            </EditItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" Width="80px" />
            <ItemTemplate>
                <%#Eval("phone")%>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                <HeaderStyle Width="70px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
            <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                <HeaderStyle Width="40px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                <ControlStyle Font-Bold="False" Font-Size="12px" />
            </asp:CommandField>
        </Columns>
        </asp:GridView>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
