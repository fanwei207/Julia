<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_LadingNum.aspx.cs" Inherits="SID_SID_LadingNum" %>

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
    <div ALIGN="center">
    <table>
        <tr>
            <td>出运单号：</td>
            <td><asp:TextBox ID="txtShipNum" runat="server" CssClass="smalltextbox"></asp:TextBox></td>
            <td>发票号：</td>
            <td><asp:TextBox ID="txtReceipt" runat="server" CssClass="smalltextbox"></asp:TextBox></td>
            <td><asp:Button ID="btnAdd" runat="server" Text="添加"  CssClass="SmallButton2" 
                    onclick="btnAdd_Click"/></td>
                        
        </tr>
        <tr>
            <td>提单日期：</td>
            <td><asp:TextBox ID="txtLadDate" runat="server"  CssClass="SmallTextBox Date"></asp:TextBox></td>
            <td></td><td></td>
            <td><asp:Button ID="btnReach" runat="server" Text="查询"  CssClass="SmallButton2" 
                    onclick="btnReach_Click"/></td>
        </tr>
        <%--<tr>
            <td>导入文件：</td>
            <td colspan="3"><input id="filename1" style="width: 370px; height: 22px" type="file" size="45" name="filename1"
                        runat="server" /></td>
            <td><asp:Button ID="btnImport" runat="server" Text="导入" CssClass="SmallButton2" 
                    onclick="btnImport_Click"/></td>
        </tr>--%>
    </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            onrowcancelingedit="gv_RowCancelingEdit" onrowediting="gv_RowEditing" 
            onrowupdating="gv_RowUpdating" onrowdeleting="gv_RowDeleting"
            DataKeyNames="id" style="margin-top:20px;">
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
                    <asp:TableCell HorizontalAlign="center" Text="出运单号" Width="80px"></asp:TableCell>
                    <asp:TableCell HorizontalAlign="center" Text="发票号" Width="80px"></asp:TableCell>
			        <asp:TableCell HorizontalAlign="center" Text="提单日期" Width="80px"></asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </EmptyDataTemplate>
        <Columns>
        <asp:BoundField DataField="id" HeaderText="ID">
            <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="SID_ShipNum" HeaderText="出运单号" ReadOnly="True">
            <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="SID_Receipt" HeaderText="发票号" ReadOnly="True">
            <HeaderStyle Width="80px" HorizontalAlign="Center" Font-Bold="False" />
            <ItemStyle Width="80px" HorizontalAlign="Center" />
        </asp:BoundField>
        
        <asp:TemplateField HeaderText="提单日期" >
            <EditItemTemplate>
            <asp:TextBox ID="txtLadDate" runat="server" CssClass="SmallTextBox Date" Text='<%# Bind("SID_LadDate") %>'
                Width="80px"></asp:TextBox>
            </EditItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" Width="80px" />
            <ItemTemplate>
                <%#Eval("SID_LadDate")%>
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
