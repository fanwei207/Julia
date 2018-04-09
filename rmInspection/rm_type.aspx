<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rm_type.aspx.cs"
    Inherits="rm_type" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .LabelRight
        {
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="600px" bgcolor="white" border="0">
            <tr> 
                <td>
                    <asp:Label ID="lblCate" runat="server" Width="49px" CssClass="LabelRight" Text="类别:"
                        Font-Bold="False"></asp:Label>
                    <asp:TextBox ID="txtType" runat="server" Width="257px" TabIndex="1" MaxLength="30"
                        CssClass="smallTextBox"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Query"
                        Width="40px" OnClick="btnQuery_Click" />&nbsp;
                    <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" TabIndex="5" Text="New"
                        Width="40px" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="20" OnPageIndexChanging="gv_PageIndexChanging"
            OnRowDataBound="gv_RowDataBound" OnRowDeleting="gv_RowDeleting" OnRowCancelingEdit="gv_RowCancelingEdit"
             OnRowEditing = "gv_RowEditing" OnRowUpdating ="gv_RowUpdating"
            Width="620px" DataKeyNames="conn_typeid,conn_createdby">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" Width="620px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="类别" Width="30px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="创建人" Width="300px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                        <asp:TableCell Text="创建时间" Width="100px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell> 
                        <asp:TableCell Text="Del" Width="40px" HorizontalAlign="center" Font-Bold="false"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns> 
                <asp:BoundField DataField="conn_typename" HeaderText="类别">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="conn_createdName" HeaderText="创建者" ReadOnly="true">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>  
                <asp:BoundField DataField="conn_createdtime" HeaderText="创建时间" ReadOnly="true">
                    <HeaderStyle Width="120px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:HyperLinkField HeaderText="添加类型" Text="添加/查看" DataNavigateUrlFields="conn_typeid,conn_typename"
                    DataNavigateUrlFormatString="rm_subtype.aspx?typeid={0}&amp;typename={1}">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle Width="90px" Font-Bold="False" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                  <asp:CommandField ShowEditButton="True">
                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField> 
                <asp:CommandField ShowDeleteButton="True" DeleteText="Del">
                    <ControlStyle Font-Bold="False" Font-Underline="True" ForeColor="Black" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
